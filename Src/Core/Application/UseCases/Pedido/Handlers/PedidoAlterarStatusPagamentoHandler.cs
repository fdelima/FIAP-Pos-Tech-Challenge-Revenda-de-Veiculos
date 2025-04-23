using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoAlterarStatusPagamentoHandler : IRequestHandler<VeiculoAlterarStatusPagamentoCommand, ModelResult>
    {
        private readonly IVeiculoService _service;

        public VeiculoAlterarStatusPagamentoHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(VeiculoAlterarStatusPagamentoCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _service.FindByIdAsync(command.Id);

            if (result.IsValid)
            {
                var revendaDeVeiculos = (Domain.Entities.Veiculo)result.Model;
                revendaDeVeiculos.StatusPagamento = command.StatusPagamento.ToString();
                result = await _service.UpdateAsync(revendaDeVeiculos);

                if (result.IsValid)
                {
                    try
                    {
                        var producaoClient = Util.GetClient(command.MicroServicoProducaoBaseAdress);

                        HttpResponseMessage response = await producaoClient.PostAsJsonAsync(
                            "api/producao/revendaDeVeiculos/InserirRecebido", revendaDeVeiculos);

                        if (!response.IsSuccessStatusCode)
                            result.AddMessage("Não foi possível enviar revendaDeVeiculos para produção.");
                    }
                    catch (Exception)
                    {
                        result.AddMessage("Falha ao conectar a produção.");
                    }
                }
            }

            return result;
        }
    }
}
