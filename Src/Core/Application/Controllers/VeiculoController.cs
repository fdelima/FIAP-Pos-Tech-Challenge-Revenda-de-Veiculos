using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
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
        private readonly IMediator _mediator;
        private readonly IValidator<Veiculo> _validator;

        public VeiculoController(IMediator mediator,
            IValidator<Veiculo> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult<Veiculo>> ValidateAsync(Veiculo entity)
        {
            ModelResult<Veiculo> ValidatorResult = new ModelResult<Veiculo>(entity);

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
        public virtual async Task<ModelResult<Veiculo>> PostAsync(Veiculo entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<Veiculo> ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                VeiculoPostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para atualização ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult<Veiculo>> PutAsync(Guid id, Veiculo entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<Veiculo> ValidatorResult = await ValidateAsync(entity);

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
        public virtual async Task<ModelResult<Veiculo>> DeleteAsync(Guid id)
        {
            VeiculoDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<Veiculo>> FindByIdAsync(Guid id)
        {
            VeiculoFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Veiculo>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp)
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
        public virtual async ValueTask<PagingQueryResult<Veiculo>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Veiculo, bool>> expression, Expression<Func<Veiculo, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            VeiculoGetItemsCommand command = new(filter, sortProp, expression);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async Task<PagingQueryResult<Veiculo>> GetVehiclesForSaleAsync(PagingQueryParam<Veiculo> filter)
        {
            VeiculoGetVehiclesForSaleCommand command = new(filter);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public async Task<PagingQueryResult<Veiculo>> GetVehiclesSoldAsync(PagingQueryParam<Veiculo> filter)
        {
            VeiculoGetVehiclesSoldCommand command = new(filter);
            return await _mediator.Send(command);
        }
    }
}
