using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.Controllers
{
    /// <summary>
    /// Regras da aplicação referente ao revendaDeVeiculos
    /// </summary>
    public class PedidoController : IPedidoController
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Pedido> _validator;

        public PedidoController(IConfiguration configuration, IMediator mediator,
            IValidator<Domain.Entities.Pedido> validator)
        {
            _configuration = configuration;
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.Pedido entity)
        {
            ModelResult ValidatorResult = new ModelResult(entity);

            FluentValidation.Results.ValidationResult validations = _validator.Validate(entity);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            return await Task.FromResult(ValidatorResult);
        }

        /// <summary>
        /// Envia a entidade para inserção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.Pedido entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Pedido");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                PedidoPostCommand command = new(entity,
                    _configuration["revendaDeVeiculos-baseadress"] ?? "",
                    _configuration["micro-servico-pagamento-baseadress"] ?? "");
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para atualização ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.Pedido entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Pedido");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                PedidoPutCommand command = new(id, entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para deleção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> DeleteAsync(Guid id)
        {
            PedidoDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            PedidoFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Pedido>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Pedido, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            PedidoGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades que atendem a expressão de filtro 
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Pedido>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Pedido, bool>> expression, Expression<Func<Domain.Entities.Pedido, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            PedidoGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async Task<PagingQueryResult<Domain.Entities.Pedido>> GetListaAsync(PagingQueryParam<Domain.Entities.Pedido> param)
        {
            PedidoGetListaCommand command = new(param);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Alterar o status de pagamento do revendaDeVeiculos
        /// </summary>
        public async Task<ModelResult> AlterarStatusPagamento(Guid id, enmPedidoStatusPagamento statusPagamento)
        {
            PedidoAlterarStatusPagamentoCommand command = new(id, statusPagamento, _configuration["micro-servico-producao-baseadress"] ?? "");
            return await _mediator.Send(command);
        }
    }
}
