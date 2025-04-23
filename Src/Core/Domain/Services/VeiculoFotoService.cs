using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class VeiculoFotoService : BaseService<Entities.VeiculoFoto>
    {
        /// <summary>
        /// Lógica de negócio referentes ao item do revendaDeVeiculos.
        /// </summary>
        /// <param name="gateway">Gateway de item do revendaDeVeiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public VeiculoFotoService(IGateways<Entities.VeiculoFoto> gateway, IValidator<VeiculoFoto> validator)
            : base(gateway, validator)
        {
        }

        /// <summary>
        /// Regras para inserção do item do revendaDeVeiculos.
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Entities.VeiculoFoto entity, string[]? businessRules = null)
        {
            entity.IdVeiculoItem = entity.IdVeiculoItem.Equals(default) ? Guid.NewGuid() : entity.IdVeiculoItem;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
