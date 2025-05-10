using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.Controllers
{
    /// <summary>
    /// Regras da aplicação referente ao revendaDeVeiculos
    /// </summary>
    public class VeiculoController : IVeiculoController
    {
        private readonly IMediator _mediator;
        private readonly IValidator<VeiculoEntity> _validator;
        private readonly IValidator<VeiculoPagamentoEntity> _pagamentoValidator;

        public VeiculoController(IMediator mediator,
            IValidator<VeiculoEntity> validator,
            IValidator<VeiculoPagamentoEntity> pagamentoValidator)
        {
            _mediator = mediator;
            _validator = validator;
            _pagamentoValidator = pagamentoValidator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult<VeiculoEntity>> ValidateAsync(VeiculoEntity entity)
        {
            ModelResult<VeiculoEntity> ValidatorResult = new ModelResult<VeiculoEntity>(entity);

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
        public virtual async Task<ModelResult<VeiculoEntity>> PostAsync(VeiculoEntity entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<VeiculoEntity> ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                VeiculoPostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Cadastra um novo pagamento para um Veiculo.
        /// </summary>
        public virtual async Task<ModelResult<VeiculoPagamentoEntity>> PostPagamentoAsync(VeiculoPagamentoEntity entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo Pagamento");

            ModelResult<VeiculoPagamentoEntity> ValidatorResult = new ModelResult<VeiculoPagamentoEntity>(entity);

            FluentValidation.Results.ValidationResult validations = _pagamentoValidator.Validate(entity);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            ModelResult<VeiculoEntity> result = await FindByIdAsync(entity.IdVeiculo);
            if (result.IsValid)
            {
                VeiculoPagamentoPostCommand command = new(result.Model, entity);
                result = await _mediator.Send(command);
            }

            ValidatorResult.AddMessage(result.Messages);
            ValidatorResult.AddError(result.Errors);
            ValidatorResult.AddMessage(result.Messages);

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para atualização ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult<VeiculoEntity>> PutAsync(Guid id, VeiculoEntity entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Veiculo");

            ModelResult<VeiculoEntity> ValidatorResult = await ValidateAsync(entity);

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
        public virtual async Task<ModelResult<VeiculoEntity>> DeleteAsync(Guid id)
        {
            VeiculoDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<VeiculoEntity>> FindByIdAsync(Guid id)
        {
            VeiculoFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<VeiculoEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp)
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
        public virtual async ValueTask<PagingQueryResult<VeiculoEntity>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<VeiculoEntity, bool>> expression, Expression<Func<VeiculoEntity, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            VeiculoGetItemsCommand command = new(filter, sortProp, expression);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public virtual async Task<PagingQueryResult<VeiculoModel>> GetVehiclesForSaleAsync(PagingQueryParam<VeiculoEntity> filter)
        {
            VeiculoGetVehiclesForSaleCommand command = new(filter);
            PagingQueryResult<VeiculoEntity> items = await _mediator.Send(command);
            return new PagingQueryResult<VeiculoModel>([.. items.Content], items.NumberOfElements, items.Take);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        public virtual async Task<PagingQueryResult<VeiculoModel>> GetVehiclesSoldAsync(PagingQueryParam<VeiculoEntity> filter)
        {
            VeiculoGetVehiclesSoldCommand command = new(filter);
            PagingQueryResult<VeiculoEntity> items = await _mediator.Send(command);
            return new PagingQueryResult<VeiculoModel>([.. items.Content], items.NumberOfElements, items.Take);
        }

        /// Consulta os veículos cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<PagingQueryResult<VeiculoModel>> ConsultListItemsAsync(PagingQueryParam<VeiculoModel> filter)
        {
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>
            {
                CurrentPage = filter.CurrentPage,
                ObjFilter = (VeiculoEntity)filter.ObjFilter,
                SortProperty = filter.SortProperty,
                SortDirection = filter.SortDirection,
                Take = filter.Take
            };
            PagingQueryResult<VeiculoEntity> items = await ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
            return new PagingQueryResult<VeiculoModel>([.. items.Content], items.NumberOfElements, items.Take);
        }

        /// <summary>
        /// Retorna os veículos cadastrados no sistema paginado.
        /// </summary>
        public virtual async Task<PagingQueryResult<VeiculoModel>> GetListItemsAsync(int currentPage, int take)
        {
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = currentPage, Take = take };
            PagingQueryResult<VeiculoEntity> items = await GetItemsAsync(param, param.SortProp());
            return new PagingQueryResult<VeiculoModel>([.. items.Content], items.NumberOfElements, items.Take);
        }
    }
}
