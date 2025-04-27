using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]

    /// <summary>
    /// Mensagens padrões de negócio
    /// </summary>
    public struct BusinessMessages
    {
        /// <summary>
        /// Retorna mensagem genérica de sucesso
        /// </summary>
        public static string GenericSucess() => "Operação realizada com sucesso.";

        /// <summary>
        /// Retorna mensagem genérica de sucesso para insert
        /// </summary>
        public static string InsertSucess<TEntity>() => GenericSucess();

        /// <summary>
        /// Retorna mensagem genérica de sucesso para update
        /// </summary>
        public static string UpdateSucess<TEntity>() => GenericSucess();

        /// <summary>
        /// Retorna mensagem genérica de sucesso para delete
        /// </summary>
        public static string DeleteSucess<TEntity>() => GenericSucess();

        /// <summary>
        /// Retorna mensagem de erro - registro em duplicidade
        /// </summary>
        public static string DuplicatedError<TEntity>() => $"Já existe um registro {typeof(TEntity).Name}.";

        /// <summary>
        /// Retorna mensagem de erro - registro não encontrado
        /// </summary>
        public static string NotFoundError<TEntity>() => $"Nenhum registro de {typeof(TEntity).Name} encontrado.";

        /// <summary>
        /// Retorna mensagem de erro - registro não encontrado
        /// </summary>
        public static string NotFoundInError<TEntity>(Guid id) => $"{typeof(TEntity).Name} informado {id} não encontrado.";

    }
}