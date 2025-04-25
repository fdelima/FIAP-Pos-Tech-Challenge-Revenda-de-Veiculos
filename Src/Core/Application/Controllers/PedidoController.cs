using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
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
    public class VeiculoController : IVeiculoController
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Veiculo> _validator;

        public VeiculoController(IConfiguration configuration, IMediator mediator,
            IValidator<Domain.Entities.Veiculo> validator)
        {
            _configuration = configuration;
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult<TEntity>> ValidateAsync(Domain.Entities.Veiculo entity)
        {
            ModelResult<TEntity> ValidatorResult = new ModelResult<TEntity>(entity);

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
        public virtual async Task<ModelResult<TEntity>> PostAsync(Domain.Entities.Veiculo entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<TEntity> ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                VeiculoPostCommand command = new(entity,
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
        public virtual async Task<ModelResult<TEntity>> PutAsync(Guid id, Domain.Entities.Veiculo entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<TEntity> ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                VeiculoPutCommand command = new(id, entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para deleção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> DeleteAsync(Guid id)
        {
            VeiculoDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> FindByIdAsync(Guid id)
        {
            VeiculoFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Veiculo>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Veiculo, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            VeiculoGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades que atendem a expressão de filtro 
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Veiculo>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Veiculo, bool>> expression, Expression<Func<Domain.Entities.Veiculo, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            VeiculoGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna os Veiculos cadastrados
        /// A lista de veiculos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Veiculos mais antigos primeiro e mais novos depois;
        /// 3. Veiculos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async Task<PagingQueryResult<Domain.Entities.Veiculo>> GetListaAsync(PagingQueryParam<Domain.Entities.Veiculo> param)
        {
            VeiculoGetVehiclesForSaleCommand command = new(param);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Alterar o status de pagamento do revendaDeVeiculos
        /// </summary>
        public async Task<ModelResult<TEntity>> AlterarStatusPagamento(Guid id, enmVeiculoStatusPagamento statusPagamento)
        {
            VeiculoPagamentoPostCommand command = new(id, statusPagamento, _configuration["micro-servico-producao-baseadress"] ?? "");
            return await _mediator.Send(command);
        }
    }
}
