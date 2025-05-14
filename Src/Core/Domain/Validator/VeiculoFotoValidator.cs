using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class VeiculoFotoValidator : AbstractValidator<VeiculoFotoEntity>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public VeiculoFotoValidator()
        {
            RuleFor(c => c.IdVeiculo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Imagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
