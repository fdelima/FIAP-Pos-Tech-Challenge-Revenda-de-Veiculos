﻿//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
//using System.Linq.Expressions;

//namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities
//{
//    public partial class Notificacao : IDomainEntity
//    {
//        /// <summary>
//        /// Retorna a regra de validação a ser utilizada na inserção.
//        /// </summary>
//        public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
//        {
//            return x => false;
//        }

//        /// <summary>
//        /// Retorna a regra de validação a ser utilizada na atualização.
//        /// </summary>
//        public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
//        {
//            return x => false;
//        }

//        public Guid IdNotificacao { get; set; }

//        public DateTime Data { get; set; }

//        public string Mensagem { get; set; } = null!;

//        public Guid IdDispositivo { get; set; }

//    }
//}
