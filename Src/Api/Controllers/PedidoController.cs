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
    /// Controller dos Veiculos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class VeiculoController : ApiController
    {
        private readonly IVeiculoController _controller;

        /// <summary>
        /// Construtor do controller dos Veiculos cadastrados
        /// </summary>
        public VeiculoController(IVeiculoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Retorna os Veiculos cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Veiculo>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Veiculo> param = new PagingQueryParam<Veiculo>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Retorna os Veiculos cadastrados
        /// A lista de veiculos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Veiculos mais antigos primeiro e mais novos depois;
        /// 3. Veiculos com status Finalizado não devem aparecer na lista.
        /// </summary>
        [HttpGet("Lista")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Veiculo>> GetLista(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Veiculo> param = new PagingQueryParam<Veiculo>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetListaAsync(param);
        }

        /// <summary>
        /// Recupera o Veiculo cadastrado pelo seu Id
        /// </summary>
        /// <returns>Veiculo encontrada</returns>
        /// <response code="200">Veiculo encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Veiculo cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _controller.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os Veiculos cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Veiculos</param>
        /// <returns>Retorna as Veiculos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Veiculos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Veiculos cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Veiculo>> Consult(PagingQueryParam<Veiculo> param)
        {
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Veiculo cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Veiculo.</response>
        [HttpPost("Checkout")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Veiculo model)
        {
            return ExecuteCommand(await _controller.PostAsync(model));
        }

        /// <summary>
        /// Altera o Veiculo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Veiculo cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Veiculo.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Veiculo model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Altera o Stauts de pagamento do Veiculo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Veiculo cadastrado.</param>
        /// <param name="statusPagamento">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Veiculo.</response>
        [HttpPut("ReceberStatusPagamento")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, enmVeiculoStatusPagamento statusPagamento)
        {
            return ExecuteCommand(await _controller.AlterarStatusPagamento(id, statusPagamento));
        }

        /// <summary>
        /// Deleta o Veiculo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Veiculo cadastrado.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Veiculo.</response>
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