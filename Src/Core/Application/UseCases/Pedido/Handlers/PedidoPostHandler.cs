using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoPostHandler : IRequestHandler<VeiculoPostCommand, ModelResult>
    {
        private readonly IVeiculoService _service;

        public VeiculoPostHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(VeiculoPostCommand command, CancellationToken cancellationToken = default)
        {
            var warnings = new List<string>();
            try
            {
                var cadastroClient = Util.GetClient(command.MicroServicoCadastroBaseAdress);

                HttpResponseMessage response =
                    await cadastroClient.GetAsync($"api/cadastro/Cliente/{command.Entity.IdCliente}");

                if (!response.IsSuccessStatusCode)
                    warnings.Add($"Cliente {command.Entity.IdCliente} não encontrado!");

                response = await cadastroClient.GetAsync($"api/cadastro/Dispositivo/{command.Entity.IdDispositivo}");

                if (!response.IsSuccessStatusCode)
                    warnings.Add($"Dispositivo {command.Entity.IdDispositivo} não encontrado!");

                foreach (var produto in command.Entity.VeiculoFotos)
                {
                    response = await cadastroClient.GetAsync($"api/cadastro/Produto/{produto.IdProduto}");

                    if (!response.IsSuccessStatusCode)
                        warnings.Add($"Produto {produto.IdProduto} não encontrado.");
                }
            }
            catch (Exception ex)
            {
                warnings.Add($"Falha ao conectar ao cadastro. Detalhes: {ex.ToString()}");
            }

            var result = await _service.InsertAsync(command.Entity, command.BusinessRules);

            if (result.IsValid)
            {
                try
                {
                    var pagamentoClient = Util.GetClient(command.MicroServicoPagamentoBaseAdress);

                    HttpResponseMessage response =
                     await pagamentoClient.PostAsJsonAsync("api/Pagamento/Veiculo", result.Model);

                    if (!response.IsSuccessStatusCode)
                        result.AddMessage($"Não foi possível enviar revendaDeVeiculos para o pagamento.");
                }
                catch (Exception ex)
                {
                    result.AddMessage($"Falha ao conectar ao pagamento.  Detalhes: {ex.Message}");
                }
            }

            foreach (var item in warnings)
                result.AddMessage(item);

            return result;
        }
    }
}
