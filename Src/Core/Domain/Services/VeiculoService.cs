using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services
{
    public class VeiculoService : BaseService<Entities.Veiculo>, IVeiculoService
    {
        protected readonly IGateways<Entities.Notificacao> _notificacaoGateway;

        /// <summary>
        /// Lógica de negócio referentes ao revendaDeVeiculos.
        /// </summary>
        /// <param name="gateway">Gateway de revendaDeVeiculos a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="dispositivoGateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="clienteGateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="produtoGateway">Gateway de produto a ser injetado durante a execução</param>
        public VeiculoService(IGateways<Entities.Veiculo> gateway,
            IValidator<Entities.Veiculo> validator,
            IGateways<Entities.Notificacao> notificacaoGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
        }

        /// <summary>
        /// Regra para carregar o revendaDeVeiculos e seus itens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            Entities.Veiculo? result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.VeiculoFotos, x => x.IdVeiculo == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Entities.Veiculo>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para inserção do revendaDeVeiculos
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Entities.Veiculo entity, string[]? businessRules = null)
        {
            List<string> lstWarnings = new List<string>();

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            entity.IdVeiculo = entity.IdVeiculo.Equals(default) ? Guid.NewGuid() : entity.IdVeiculo;

            entity.DataStatusVeiculo = entity.Data = DateTime.Now;
            entity.Status = enmVeiculoStatus.RECEBIDO.ToString();

            entity.DataStatusPagamento = DateTime.Now;
            entity.StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString();

            foreach (VeiculoFoto itemVeiculo in entity.VeiculoFotos)
            {
                itemVeiculo.IdVeiculo = entity.IdVeiculo;
                itemVeiculo.IdVeiculoItem = itemVeiculo.IdVeiculoItem.Equals(default) ? Guid.NewGuid() : itemVeiculo.IdVeiculoItem;
                itemVeiculo.Data = DateTime.Now;
            }

            await _gateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Veiculo recebido."
            });

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do revendaDeVeiculos e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Entities.Veiculo entity, string[]? businessRules = null)
        {
            Entities.Veiculo? dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.VeiculoFotos, x => x.IdVeiculo == entity.IdVeiculo);

            //TODO:Há Resolver...
            //if (dbEntity == null)
            //    return ModelResultFactory.NotFoundResult<Produto>();

            for (int i = 0; i < dbEntity.VeiculoFotos.Count; i++)
            {
                VeiculoFoto item = dbEntity.VeiculoFotos.ElementAt(i);
                if (!entity.VeiculoFotos.Any(x => x.IdVeiculoItem.Equals(item.IdVeiculoItem)))
                    dbEntity.VeiculoFotos.Remove(dbEntity.VeiculoFotos.First(x => x.IdVeiculoItem.Equals(item.IdVeiculoItem)));
            }

            for (int i = 0; i < entity.VeiculoFotos.Count; i++)
            {
                VeiculoFoto item = entity.VeiculoFotos.ElementAt(i);
                if (!dbEntity.VeiculoFotos.Any(x => x.IdVeiculoItem.Equals(item.IdVeiculoItem)))
                {
                    item.IdVeiculoItem = item.IdVeiculoItem.Equals(default) ? Guid.NewGuid() : item.IdVeiculoItem;
                    dbEntity.VeiculoFotos.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Regra para retornar os Veiculos cadastrados
        /// A lista de veiculos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Veiculos mais antigos primeiro e mais novos depois;
        /// 3. Veiculos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async ValueTask<PagingQueryResult<Entities.Veiculo>> GetListaAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Desc";
            return await _gateway.GetItemsAsync(filter, x => x.Status != enmVeiculoStatus.FINALIZADO.ToString(), o => o.Data);
        }
    }
}
