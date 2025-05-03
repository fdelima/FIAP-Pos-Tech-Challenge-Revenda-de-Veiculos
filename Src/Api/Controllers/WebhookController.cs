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
    /// Controller dos Pagamentos dos Veiculos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class WebhookController : ApiController<VeiculoPagamento>
    {
        private readonly IVeiculoController _controller;

        /// <summary>
        ///  Controller dos Pagamentos dos Veiculos cadastrados
        /// </summary>
        public WebhookController(IVeiculoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Cadastra um novo pagamento para um Veiculo.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Veiculo cadastrado.</returns>
        /// <response code="200">Pagamento Veiculo inserido com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Veiculo.</response>
        [HttpPost("veiculo/pagamento")]
        [ProducesResponseType(typeof(ModelResult<VeiculoPagamento>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(VeiculoPagamento model)
        {
            return ExecuteCommand(await _controller.PostPagamentoAsync(model));
        }
    }
}