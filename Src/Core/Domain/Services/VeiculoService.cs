using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class VeiculoService : BaseService<VeiculoEntity>, IVeiculoService
    {
        /// <summary>
        /// Lógica de negócio referentes ao veiculo.
        /// </summary>
        /// <param name="gateway">Gateway de veiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public VeiculoService(IGateways<VeiculoEntity> gateway,
            IValidator<VeiculoEntity> validator)
            : base(gateway, validator) { }

        /// <summary>
        /// Regra para carregar o veiculo e suas fotos.
        /// </summary>
        public async override Task<ModelResult<VeiculoEntity>> FindByIdAsync(Guid Id)
        {
            VeiculoEntity? veiculo = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == Id);

            if (veiculo == null) return ModelResultFactory.NotFoundResult<VeiculoEntity>();

            if (veiculo.Status.Equals(enmVeiculoStatus.VENDIDO.ToString()))
            {
                VeiculoEntity? r1 = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Pagamentos, x => x.IdVeiculo == Id);
                if (r1 != null) veiculo.Pagamentos = r1.Pagamentos;
            }

            return ModelResultFactory.SucessResult(veiculo);
        }

        /// <summary>
        /// Regras para inserção do veiculo
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult<VeiculoEntity>> InsertAsync(VeiculoEntity entity, string[]? businessRules = null)
        {
            List<string> lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdVeiculo = entity.IdVeiculo.Equals(default) ? Guid.NewGuid() : entity.IdVeiculo;

            foreach (Entities.VeiculoFotoEntity fotoVeiculo in entity.Fotos)
            {
                fotoVeiculo.IdVeiculo = entity.IdVeiculo;
                fotoVeiculo.IdVeiculoFoto = fotoVeiculo.IdVeiculoFoto.Equals(default) ? Guid.NewGuid() : fotoVeiculo.IdVeiculoFoto;
            }

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do veiculo e suas dependências.
        /// </summary>
        public async override Task<ModelResult<VeiculoEntity>> UpdateAsync(VeiculoEntity entity, string[]? businessRules = null)
        {
            VeiculoEntity? veiculo = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == entity.IdVeiculo);

            if (veiculo == null) return ModelResultFactory.NotFoundResult<VeiculoEntity>();

            for (int i = 0; i < veiculo.Fotos.Count; i++)
            {
                VeiculoFotoEntity item = veiculo.Fotos.ElementAt(i);
                if (!entity.Fotos.Any(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)))
                    veiculo.Fotos.Remove(veiculo.Fotos.First(x => x.IdVeiculoFoto.Equals(item.IdVeiculoFoto)));
            }

            for (int i = 0; i < entity.Fotos.Count; i++)
            {
                VeiculoFotoEntity item = entity.Fotos.ElementAt(i);
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
        /// Regras base para deleção.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async override Task<ModelResult<VeiculoEntity>> DeleteAsync(Guid Id, string[]? businessRules = null)
        {
            VeiculoEntity? entity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo.Equals(Id));

            ModelResult<VeiculoEntity> validatorResult;

            if (entity == null)
                validatorResult = ModelResultFactory.NotFoundResult<VeiculoEntity>();
            else
                validatorResult = new ModelResult<VeiculoEntity>(entity);

            if (businessRules != null)
                validatorResult.AddError(businessRules);

            if (!validatorResult.IsValid)
                return validatorResult;

            try
            {
                //Removendo as fotos do veiculo, porém não os pagamentos.
                //Caso tenha pagamentos, não é possível remover o veiculo.
                entity.Fotos.Clear();

                await _gateway.DeleteAsync(Id);
                await _gateway.CommitAsync();
                return ModelResultFactory.DeleteSucessResult<VeiculoEntity>();
            }
            catch (Exception ex)
            {
                return ModelResultFactory.DeleteFailResult(entity ?? default!, ex.Message);
            }

        }

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<VeiculoEntity>> GetVehiclesForSaleAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VITRINE.ToString(), o => o.Preco);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async ValueTask<PagingQueryResult<VeiculoEntity>> GetVehiclesSoldAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Asc";
            return await _gateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VENDIDO.ToString(), o => o.Preco);
        }
    }
}
