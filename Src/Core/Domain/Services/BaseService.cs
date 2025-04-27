using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    /// <summary>
    /// Base da Lógica de negócio e regras da aplicação.
    /// </summary>
    public class BaseService<TEntity> : IService<TEntity> where TEntity : class, IDomainEntity
    {
        //Gateway a ser injetado durante a execução
        protected readonly IGateways<TEntity> _gateway;

        //Validador a ser injetado durante a execução
        protected readonly IValidator<TEntity> _validator;

        public BaseService(IGateways<TEntity> gateway, IValidator<TEntity> validator)
        {
            _gateway = gateway;
            _validator = validator;
        }

        /// <summary>
        /// Reposnsável por solicitar ao gateway o inicio de uma transação no banco de dados.
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction()
          => _gateway.BeginTransaction();

        /// <summary>
        /// Reposnsável por solicitar ao gateway a adição da transação ao contexto do banco de dados.
        /// </summary>
        /// <param name="transaction"></param>
        public void UseTransaction(IDbContextTransaction transaction)
          => _gateway.UseTransaction(transaction);

        /// <summary>
        /// Aplica as regras de validação da entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult<TEntity>> ValidateAsync(TEntity entity)
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
        /// Regras base para inserção.
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public virtual async Task<ModelResult<TEntity>> InsertAsync(TEntity entity, string[]? businessRules = null)
        {
            ModelResult<TEntity> ValidatorResult = await ValidateAsync(entity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = entity.InsertDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _gateway.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult(entity));
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _gateway.InsertAsync(entity);
                await _gateway.CommitAsync();
                return ModelResultFactory.InsertSucessResult(entity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Regras base para atualização.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> UpdateAsync(TEntity entity, string[]? businessRules = null)
        {
            ModelResult<TEntity> ValidatorResult = await ValidateAsync(entity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = entity.AlterDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _gateway.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult(entity));
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _gateway.UpdateAsync(entity);
                await _gateway.CommitAsync();
                return ModelResultFactory.UpdateSucessResult(entity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Regras base para atualização comparando as entidades.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> UpdateAsync(TEntity oldEntity, TEntity NewEntity, string[]? businessRules = null)
        {
            ModelResult<TEntity> ValidatorResult = await ValidateAsync(NewEntity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = NewEntity.AlterDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _gateway.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult(NewEntity));
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _gateway.UpdateAsync(oldEntity, NewEntity);
                await _gateway.CommitAsync();
                return ModelResultFactory.UpdateSucessResult(NewEntity);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Regras base para deleção.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> DeleteAsync(Guid Id, string[]? businessRules = null)
        {
            TEntity? entity = await _gateway.FindByIdAsync(Id);

            ModelResult<TEntity> validatorResult;

            if (entity == null)
                validatorResult = ModelResultFactory.NotFoundResult(entity ?? default!);
            else
                validatorResult = new ModelResult<TEntity>(entity ?? default!);

            if (businessRules != null)
                validatorResult.AddError(businessRules);

            if (!validatorResult.IsValid)
                return validatorResult;

            try
            {
                await _gateway.DeleteAsync(Id);
                await _gateway.CommitAsync();
                return ModelResultFactory.DeleteSucessResult(entity ?? default!);
            }
            catch (Exception ex)
            {
                return ModelResultFactory.DeleteFailResult(entity ?? default!, ex.Message);
            }

        }

        /// <summary>
        /// Regras base para retorna uma entidade por id.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult<TEntity>> FindByIdAsync(Guid Id)
        {
            TEntity? result = await _gateway.FindByIdAsync(Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult(result ?? default!);

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras base para retornar uma lista paginada de entidades.
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, object>> sortProp)
            => await _gateway.GetItemsAsync(filter, sortProp);


        /// <summary>
        /// Regras base para retornar uma lista paginada de entidades e filtrada.
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> sortProp)
            => await _gateway.GetItemsAsync(filter, expression, sortProp);

    }
}
