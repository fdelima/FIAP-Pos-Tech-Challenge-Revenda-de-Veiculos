using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class PedidoService : BaseService<Entities.Pedido>, IPedidoService
    {
        protected readonly IGateways<Notificacao> _notificacaoGateway;

        /// <summary>
        /// Lógica de negócio referentes ao revendaDeVeiculos.
        /// </summary>
        /// <param name="gateway">Gateway de revendaDeVeiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="dispositivoGateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="clienteGateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="produtoGateway">Gateway de produto a ser injetado durante a execução</param>
        public PedidoService(IGateways<Entities.Pedido> gateway,
            IValidator<Entities.Pedido> validator,
            IGateways<Notificacao> notificacaoGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
        }

        /// <summary>
        /// Regra para carregar o revendaDeVeiculos e seus itens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            Entities.Pedido? result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Entities.Pedido>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para inserção do revendaDeVeiculos
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Entities.Pedido entity, string[]? businessRules = null)
        {
            List<string> lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdPedido = entity.IdPedido.Equals(default) ? Guid.NewGuid() : entity.IdPedido;

            //TODO:Há Resolver...
            //if (!await _dispositivoGateway.Any(x => ((Dispositivo)x).IdDispositivo.Equals(entity.IdDispositivo)))
            //    lstWarnings.Add(BusinessMessages.NotFoundInError<Dispositivo>(entity.IdDispositivo));

            //TODO:Há Resolver...
            //if (!await _clienteGateway.Any(x => ((Cliente)x).IdCliente.Equals(entity.IdCliente)))
            //    lstWarnings.Add(BusinessMessages.NotFoundInError<Cliente>(entity.IdDispositivo));

            entity.DataStatusPedido = entity.Data = DateTime.Now;
            entity.Status = enmPedidoStatus.RECEBIDO.ToString();

            entity.DataStatusPagamento = DateTime.Now;
            entity.StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString();

            foreach (PedidoItem itemPedido in entity.PedidoItems)
            {
                itemPedido.IdPedido = entity.IdPedido;
                itemPedido.IdPedidoItem = itemPedido.IdPedidoItem.Equals(default) ? Guid.NewGuid() : itemPedido.IdPedidoItem;
                itemPedido.Data = DateTime.Now;
                //TODO:Há Resolver...
                //if (!await _produtoGateway.Any(x => ((Produto)x).IdProduto.Equals(itemPedido.IdProduto)))
                //    lstWarnings.Add(BusinessMessages.NotFoundInError<Produto>(entity.IdDispositivo));
            }

            await _gateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido recebido."
            });

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do revendaDeVeiculos e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Entities.Pedido entity, string[]? businessRules = null)
        {
            Entities.Pedido? dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == entity.IdPedido);

            //TODO:Há Resolver...
            //if (dbEntity == null)
            //    return ModelResultFactory.NotFoundResult<Produto>();

            for (int i = 0; i < dbEntity.PedidoItems.Count; i++)
            {
                PedidoItem item = dbEntity.PedidoItems.ElementAt(i);
                if (!entity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                    dbEntity.PedidoItems.Remove(dbEntity.PedidoItems.First(x => x.IdPedidoItem.Equals(item.IdPedidoItem)));
            }

            for (int i = 0; i < entity.PedidoItems.Count; i++)
            {
                PedidoItem item = entity.PedidoItems.ElementAt(i);
                if (!dbEntity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                {
                    item.IdPedidoItem = item.IdPedidoItem.Equals(default) ? Guid.NewGuid() : item.IdPedidoItem;
                    dbEntity.PedidoItems.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Regra para retornar os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async ValueTask<PagingQueryResult<Entities.Pedido>> GetListaAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Desc";
            return await _gateway.GetItemsAsync(filter, x => x.Status != enmPedidoStatus.FINALIZADO.ToString(), o => o.Data);
        }
    }
}
