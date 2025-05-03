using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Veiculos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class VeiculoController : ApiController<Veiculo>
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
        /// Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        [HttpGet("Venda")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Veiculo>> GetVehiclesForSaleAsync(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Veiculo> param = new PagingQueryParam<Veiculo>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetVehiclesForSaleAsync(param);
        }

        /// <summary>
        /// Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro.
        /// </summary>
        [HttpGet("Vendidos")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Veiculo>> GetVehiclesSoldAsync(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Veiculo> param = new PagingQueryParam<Veiculo>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetVehiclesSoldAsync(param);
        }

        /// <summary>
        /// Recupera o Veiculo cadastrado pelo seu Id
        /// </summary>
        /// <returns>Veiculo encontrada</returns>
        /// <response code="200">Veiculo encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Veiculo cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.BadRequest)]
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
        /// Cadastra um novo Veiculo.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Veiculo.</response>
        [HttpPost()]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Veiculo model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Veiculo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Veiculo cadastrado.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Veiculo deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Veiculo.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult<Veiculo>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ExecuteCommand(await _controller.DeleteAsync(id));
        }
    }
}