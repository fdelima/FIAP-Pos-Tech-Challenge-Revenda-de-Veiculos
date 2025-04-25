using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models
{
    /// <summary>
    /// Fábrica de commads result
    /// </summary>
    public static class ModelResultFactory
    {
        /// <summary>
        /// Retorna um commad result com as mensagemns de sucesso do insert
        /// </summary>
        public static ModelResult<TEntity> InsertSucessResult<TEntity>(TEntity model)
        {
            var result = new ModelResult<TEntity>(model);
            result.AddMessage(BusinessMessages.InsertSucess<TEntity>());
            return result;
        }

        /// <summary>
        /// Retorna um commad result com as mensagens de sucesso do update
        /// </summary>
        public static ModelResult<TEntity> UpdateSucessResult<TEntity>(TEntity model)
        {
            var result = new ModelResult<TEntity>(model);
            result.AddMessage(BusinessMessages.UpdateSucess<TEntity>());
            return result;
        }

        /// <summary>
        /// Retorna um result com as mensagens de sucesso do delete
        /// </summary>
        public static ModelResult<TEntity> DeleteSucessResult<TEntity>(TEntity? model = default)
        {
            var result = model is null ? new ModelResult<TEntity>() : new ModelResult<TEntity>(model);
            result.AddMessage(BusinessMessages.DeleteSucess<TEntity>());
            return result;
        }

        /// <summary>
        /// Retorna um commad result com o erro de duplicidade
        /// </summary>
        public static ModelResult<TEntity> DuplicatedResult<TEntity>()
        {
            var result = new ModelResult<TEntity>();
            result.AddError(BusinessMessages.DuplicatedError<TEntity>());
            return result;
        }

        /// <summary>
        /// Retorna um commad result com o erro de não encontrato
        /// </summary>
        public static ModelResult<TEntity> NotFoundResult<TEntity>()
        {
            var result = new ModelResult<TEntity>();
            result.AddError(BusinessMessages.NotFoundError<TEntity>());
            return result;
        }

        /// <summary>
        /// Retorna um commad result com a mensagem de sucesso generico
        /// </summary>
        public static ModelResult<TEntity> SucessResult<TEntity>()
        {
            var result = new ModelResult<TEntity>();
            result.AddMessage(BusinessMessages.GenericSucess());
            return result;
        }

        /// <summary>
        /// Retorna um commad result com a mensagem de sucesso generico
        /// </summary>
        public static ModelResult<TEntity> SucessResult<TEntity>(TEntity model)
        {
            var result = new ModelResult<TEntity>(model);
            result.AddMessage(BusinessMessages.GenericSucess());
            return result;
        }

        /// <summary>
        /// Retorna um commad resut novo
        /// </summary>
        public static ModelResult<TEntity> None<TEntity>()
        {
            return new ModelResult<TEntity>();
        }

        /// <summary>
        /// Retorna um result com as mensagens
        /// </summary>
        public static ModelResult<TEntity> Message<TEntity>(string message)
        {
            var result = new ModelResult<TEntity>();
            result.AddMessage(message);
            return result;
        }

        /// <summary>
        /// Retorna um result com os erros informados
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ModelResult<TEntity> Error<TEntity>(params string[] errors)
        {
            var result = new ModelResult<TEntity>();
            foreach (string error in errors) result.AddError(error);
            return result;
        }

        internal static ModelResult<TEntity> DeleteFailResult<TEntity>(params string[] errors)
        {
            var result = new ModelResult<TEntity>();
            result.AddMessage(ErrorMessages.DeleteDatabaseError<TEntity>());
            foreach (string error in errors) result.AddError(error);
            return result;
        }
    }
}