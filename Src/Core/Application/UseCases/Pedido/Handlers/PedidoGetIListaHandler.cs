using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Handlers
{
    public class VeiculoGetIListaHandler : IRequestHandler<VeiculoGetListaCommand, PagingQueryResult<Domain.Entities.Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VeiculoGetIListaHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Veiculo>> Handle(VeiculoGetListaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetListaAsync(command.Filter);
        }
    }
}
