﻿using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    /// <summary>
    /// Interface regulamentando os métodos que precisam ser impementados pelos serviços
    /// </summary>
    public interface IService<TEntity> where TEntity : IDomainEntity
    {
        /// <summary>
        /// Inicia uma transação no banco de dados.
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Adiciona a transação ao contexto do banco de dados.
        /// </summary>
        /// <param name="transaction"></param>
        public void UseTransaction(IDbContextTransaction transaction);

        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<ModelResult> ValidateAsync(TEntity entity);

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<ModelResult> InsertAsync(TEntity entity, string[]? businessRules = null);

        /// <summary>
        /// Atualiza o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        Task<ModelResult> UpdateAsync(TEntity entity, string[]? businessRules = null);

        /// <summary>
        /// Deleta o objeto
        /// </summary>
        /// <param name="id">id do objeto relacional do bd mapeado</param>
        Task<ModelResult> DeleteAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Retorna o objeto do bd
        /// </summary>
        /// <param name="id">id do objeto relacional do bd mapeado</param>
        Task<ModelResult> FindByIdAsync(Guid id);

        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, object>> sortProp);


        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> sortProp);

    }
}
