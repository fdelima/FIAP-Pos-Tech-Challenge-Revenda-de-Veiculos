using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class VeiculoService : BaseService<Entities.Veiculo>, IVeiculoService
    {
        /// <summary>
        /// Lógica de negócio referentes ao revendaDeVeiculos.
        /// </summary>
        /// <param name="gateway">Gateway de revendaDeVeiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="dispositivoGateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="clienteGateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="produtoGateway">Gateway de produto a ser injetado durante a execução</param>
        public VeiculoService(IGateways<Entities.Veiculo> gateway,
            IValidator<Entities.Veiculo> validator)
            : base(gateway, validator) { }

        /// <summary>
        /// Regra para carregar o revendaDeVeiculos e suas fotos.
        /// </summary>
        public async override Task<ModelResult<Entities.Veiculo>> FindByIdAsync(Guid Id)
        {
            var result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == Id);
            if (result != null && result.Status.Equals(enmVeiculoStatus.VENDIDO))
            {
                var r1 = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Pagamentos, x => x.IdVeiculo == Id);
                if (r1 != null) result.Pagamentos = r1.Pagamentos;
            }

            if (result == null)
                return ModelResultFactory.NotFoundResult<Entities.Veiculo>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para inserção do revendaDeVeiculos
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult<Entities.Veiculo>> InsertAsync(Entities.Veiculo entity, string[]? businessRules = null)
        {
            List<string> lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdVeiculo = entity.IdVeiculo.Equals(default) ? Guid.NewGuid() : entity.IdVeiculo;

            foreach (Entities.VeiculoFoto fotoVeiculo in entity.Fotos)
            {
                fotoVeiculo.IdVeiculo = entity.IdVeiculo;
                fotoVeiculo.IdVeiculoFoto = fotoVeiculo.IdVeiculoFoto.Equals(default) ? Guid.NewGuid() : fotoVeiculo.IdVeiculoFoto;
            }

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do revendaDeVeiculos e suas dependências.
        /// </summary>
        public async override Task<ModelResult<Entities.Veiculo>> UpdateAsync(Entities.Veiculo entity, string[]? businessRules = null)
        {
            var dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == entity.IdVeiculo);

            if (dbEntity == null)
                return ModelResultFactory.NotFoundResult<Entities.Veiculo>();

            for (int i = 0; i < dbEntity.Fotos.Count; i++)
            {
                var item = dbEntity.Fotos.ElementAt(i);
                if (!entity.Fotos.Any(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)))
                    dbEntity.Fotos.Remove(dbEntity.Fotos.First(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)));
            }

            for (int i = 0; i < entity.Fotos.Count; i++)
            {
                var item = entity.Fotos.ElementAt(i);
                if (!dbEntity.Fotos.Any(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)))
                {
                    item.IdVeiculoFoto = item.IdVeiculoFoto.Equals(default) ? Guid.NewGuid() : item.IdVeiculoFoto;
                    dbEntity.Fotos.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<Entities.Veiculo>> GetVehiclesForSaleAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VITRINE.ToString(), o => o.Preco);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<Entities.Veiculo>> GetVehiclesSoldAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VENDIDO.ToString(), o => o.Preco);
        }
    }
}
