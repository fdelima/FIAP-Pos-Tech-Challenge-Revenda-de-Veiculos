using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoPagamentoPostHandler : IRequestHandler<VeiculoPagamentoPostCommand, ModelResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoPagamentoPostHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Domain.Entities.Veiculo>> Handle(VeiculoPagamentoPostCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _service.FindByIdAsync(command.IdVeiculo);

            if (result.IsValid)
            {
                var veiculo = result.Model;
                veiculo.Status = enmVeiculoStatus.VENDIDO.ToString();

                veiculo.Pagamentos = new List<Domain.Entities.VeiculoPagamento>
                {
                    new Domain.Entities.VeiculoPagamento
                    {
                        Data = command.Data,
                        ValorRecebido = command.ValorRecebido,
                        Banco = command.Banco,
                        Conta = command.Conta,
                        CpfCnpj = command.CpfCnpj
                    }
                };

                return await _service.UpdateAsync(veiculo);
            }

            return result;

        }
    }
}
