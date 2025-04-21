using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces
{
    /// <summary>
    /// Interface regulamentando os métodos que precisam ser impementados pelos repositórios
    /// </summary>
    /// <typeparam name="TEntity">Objeto relacional do bd mapeado</typeparam>
    public interface IGateways<TEntity> where TEntity : IDomainEntity
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
        /// Insere o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Insere o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<T> InsertAsync<T>(T entity);

        /// <summary>
        /// Insere os objetos no bd
        /// </summary>
        /// <param name="entities">Objetos da entidade relacional do bd mapeado</param>
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Atualiza o objeto no bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Atualiza o objeto no bd
        /// </summary>
        /// <param name="oldEntity">Objeto relacional do bd mapeado antigo</param>
        /// <param name="NewEntity">Objeto relacional do bd mapeado novo</param>
        Task UpdateAsync(TEntity oldEntity, TEntity NewEntity);

        /// <summary>
        /// Atualiza os objetos no bd
        /// </summary>
        /// <param name="entities">Objetos relacionais do bd mapeado</param>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deleta os registro do bd de acordo com a condição informada
        /// </summary>
        /// <param name="expression">condição</param>
        Task DeleteasyncByExpression(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Deleta o registro no bd pelo id informado
        /// </summary>
        /// <param name="id">id do registro no bd</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Deleta o objeto do bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deleta os objetos do bd
        /// </summary>
        /// <param name="entities">Objetos da entidade relacional do bd mapeado</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Efetivas as alterações salvando no banco de dados
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// Retorna o objeto do bd
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        ValueTask<TEntity?> FindByIdAsync(Guid id);

        /// <summary>
        /// Retorna o objeto do bd
        /// </summary>
        /// <param name="tableInclude">Tabela a ser incluida</param>
        /// <param name="whereExpression">Condição que filtra os itens a serem retornados</param>
        ValueTask<TEntity?> FirstOrDefaultWithIncludeAsync<TEntityToInclude>(Expression<Func<TEntity, ICollection<TEntityToInclude>>> tableInclude, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Retorna os objetos do bd
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<TEntity, object>> sortProp);

        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="whereExpression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(IPagingQueryParam filter,
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> sortProp);

        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="filter">condição</param>
        /// <param name="tableInclude">Tabela a ser incluida</param>
        /// <param name="whereExpression">Condição que filtra os itens a serem retornados</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync<TEntityToInclude>(
            IPagingQueryParam filter,
            Expression<Func<TEntity, ICollection<TEntityToInclude>>> tableInclude,
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> sortProp);

        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        /// <param name="whereExpression">Condição que filtra os itens a serem retornados</param>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Retorna os objetos que atendem a expression do bd
        /// </summary>
        ValueTask<PagingQueryResult<TEntity>> GetItemsAsync();

        /// <summary>
        /// Retorna verdadeiro ou falso para expressão informada
        /// </summary>
        /// <param name="filter">condição</param>
        ValueTask<bool> Any(Expression<Func<IDomainEntity, bool>> filter);
    }
}