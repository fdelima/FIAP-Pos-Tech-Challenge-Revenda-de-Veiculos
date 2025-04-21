using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class PedidoItemService : BaseService<PedidoItem>
    {
        /// <summary>
        /// Lógica de negócio referentes ao item do revendaDeVeiculos.
        /// </summary>
        /// <param name="gateway">Gateway de item do revendaDeVeiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public PedidoItemService(IGateways<PedidoItem> gateway, IValidator<PedidoItem> validator)
            : base(gateway, validator)
        {
        }

        /// <summary>
        /// Regras para inserção do item do revendaDeVeiculos.
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(PedidoItem entity, string[]? businessRules = null)
        {
            entity.IdPedidoItem = entity.IdPedidoItem.Equals(default) ? Guid.NewGuid() : entity.IdPedidoItem;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
