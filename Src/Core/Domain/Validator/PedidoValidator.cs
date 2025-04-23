using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class VeiculoValidator : AbstractValidator<Entities.Veiculo>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public VeiculoValidator()
        {
            RuleFor(c => c.IdDispositivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.VeiculoFotos).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.VeiculoFotos).SetValidator(x => new VeiculoItemValidator());
        }
    }
}
