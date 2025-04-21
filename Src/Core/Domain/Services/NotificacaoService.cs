using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class NotificacaoService : BaseService<Notificacao>
    {
        /// <summary>
        /// Lógica de negócio referentes a notificação.
        /// </summary>
        /// <param name="gateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public NotificacaoService(IGateways<Notificacao> gateway, IValidator<Notificacao> validator)
            : base(gateway, validator)
        {
        }


        /// <summary>
        /// Regras para inserção da notificacao
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Notificacao entity, string[]? businessRules = null)
        {
            entity.IdNotificacao = entity.IdNotificacao.Equals(default) ? Guid.NewGuid() : entity.IdNotificacao;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
