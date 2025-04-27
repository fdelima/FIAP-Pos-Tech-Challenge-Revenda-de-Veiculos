using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoPagamentoPostHandler : IRequestHandler<VeiculoPagamentoPostCommand, ModelResult<Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoPagamentoPostHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<Veiculo>> Handle(VeiculoPagamentoPostCommand command, CancellationToken cancellationToken = default)
        {
            ModelResult<Veiculo> result = await _service.FindByIdAsync(command.IdVeiculo);

            if (result.IsValid)
            {
                Veiculo veiculo = result.Model;
                veiculo.Status = enmVeiculoStatus.VENDIDO.ToString();

                veiculo.Pagamentos = new List<VeiculoPagamento>
                {
                    new VeiculoPagamento
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
