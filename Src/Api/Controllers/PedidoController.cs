using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Pedidos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class PedidoController : ApiController
    {
        private readonly IPedidoController _controller;

        /// <summary>
        /// Construtor do controller dos Pedidos cadastrados
        /// </summary>
        public PedidoController(IPedidoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Pedido>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        [HttpGet("Lista")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Pedido>> GetLista(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetListaAsync(param);
        }

        /// <summary>
        /// Recupera o Pedido cadastrado pelo seu Id
        /// </summary>
        /// <returns>Pedido encontrada</returns>
        /// <response code="200">Pedido encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Pedido cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _controller.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os Pedidos cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Pedidos</param>
        /// <returns>Retorna as Pedidos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Pedidos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Pedidos cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Pedido>> Consult(PagingQueryParam<Pedido> param)
        {
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Pedido cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Pedido.</response>
        [HttpPost("Checkout")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Pedido model)
        {
            return ExecuteCommand(await _controller.PostAsync(model));
        }

        /// <summary>
        /// Altera o Pedido cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Pedido.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Pedido model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Altera o Stauts de pagamento do Pedido cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <param name="statusPagamento">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Pedido.</response>
        [HttpPut("ReceberStatusPagamento")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, enmPedidoStatusPagamento statusPagamento)
        {
            return ExecuteCommand(await _controller.AlterarStatusPagamento(id, statusPagamento));
        }

        /// <summary>
        /// Deleta o Pedido cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ExecuteCommand(await _controller.DeleteAsync(id));
        }
    }
}