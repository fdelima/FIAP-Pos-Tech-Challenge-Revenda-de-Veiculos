using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class VeiculoService : BaseService<Veiculo>, IVeiculoService
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
        public VeiculoService(IGateways<Veiculo> gateway,
            IValidator<Veiculo> validator)
            : base(gateway, validator) { }

        /// <summary>
        /// Regra para carregar o revendaDeVeiculos e suas fotos.
        /// </summary>
        public async override Task<ModelResult<Veiculo>> FindByIdAsync(Guid Id)
        {
            Veiculo? veiculo = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == Id);

            if (veiculo == null) return ModelResultFactory.NotFoundResult(veiculo ?? default!);

            if (veiculo.Status.Equals(enmVeiculoStatus.VENDIDO))
            {
                Veiculo? r1 = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Pagamentos, x => x.IdVeiculo == Id);
                if (r1 != null) veiculo.Pagamentos = r1.Pagamentos;
            }

            return ModelResultFactory.NotFoundResult(veiculo ?? default!);
        }

        /// <summary>
        /// Regras para inserção do revendaDeVeiculos
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult<Veiculo>> InsertAsync(Veiculo entity, string[]? businessRules = null)
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
        public async override Task<ModelResult<Veiculo>> UpdateAsync(Veiculo entity, string[]? businessRules = null)
        {
            Veiculo? veiculo = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == entity.IdVeiculo);

            if (veiculo == null) return ModelResultFactory.NotFoundResult(veiculo ?? default!);

            for (int i = 0; i < veiculo.Fotos.Count; i++)
            {
                VeiculoFoto item = veiculo.Fotos.ElementAt(i);
                if (!entity.Fotos.Any(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)))
                    veiculo.Fotos.Remove(veiculo.Fotos.First(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)));
            }

            for (int i = 0; i < entity.Fotos.Count; i++)
            {
                VeiculoFoto item = entity.Fotos.ElementAt(i);
                if (!veiculo.Fotos.Any(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)))
                {
                    item.IdVeiculoFoto = item.IdVeiculoFoto.Equals(default) ? Guid.NewGuid() : item.IdVeiculoFoto;
                    veiculo.Fotos.Add(item);
                }
            }

            await _gateway.UpdateAsync(veiculo, entity);
            return await base.UpdateAsync(veiculo, businessRules);
        }

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<Veiculo>> GetVehiclesForSaleAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VITRINE.ToString(), o => o.Preco);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<Veiculo>> GetVehiclesSoldAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VENDIDO.ToString(), o => o.Preco);
        }
    }
}
