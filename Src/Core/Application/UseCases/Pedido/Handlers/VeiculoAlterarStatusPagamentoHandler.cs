using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoAlterarStatusPagamentoHandler : IRequestHandler<VeiculoAlterarStatusPagamentoCommand, ModelResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoAlterarStatusPagamentoHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Domain.Entities.Veiculo>> Handle(VeiculoAlterarStatusPagamentoCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command);
            var result = await _service.FindByIdAsync(command.IdVeiculo);

            if (result.IsValid)
            {
                var veiculo = result.Model;
                veiculo.Status = enmVeiculoStatus.VENDIDO.ToString();
                result = await _service.UpdateAsync(veiculo);

                if (result.IsValid)
                {
                    try
                    {
                        var producaoClient = Util.GetClient(command.MicroServicoProducaoBaseAdress);

                        HttpResponseMessage response = await producaoClient.PostAsJsonAsync(
                            "api/producao/revendaDeVeiculos/InserirRecebido", veiculo);

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
