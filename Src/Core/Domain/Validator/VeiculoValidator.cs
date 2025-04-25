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
            RuleFor(c => c.Marca).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Modelo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.AnoFabricacao).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.AnoModelo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Placa).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Renavam).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Preco).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Status).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Fotos).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.VeiculoFotos).SetValidator(x => new VeiculoItemValidator());
        }
    }
}
