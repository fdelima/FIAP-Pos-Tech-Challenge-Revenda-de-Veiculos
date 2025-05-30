﻿using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers
{
    public class VeiculoPutHandler : IRequestHandler<VeiculoPutCommand, ModelResult<VeiculoEntity>>
    {
        private readonly IVeiculoService _service;

        public VeiculoPutHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<ModelResult<VeiculoEntity>> Handle(VeiculoPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
