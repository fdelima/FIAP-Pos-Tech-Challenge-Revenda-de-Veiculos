using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class VeiculoValidator : AbstractValidator<VeiculoEntity>
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
            RuleFor(c => c.Status)
                .Must(x => (new List<string>(Enum.GetNames(typeof(enmVeiculoStatus)))).Count(e => e.Equals(x)) > 0)
                .WithMessage("Status permitidos: " + string.Join(",", Enum.GetNames(typeof(enmVeiculoStatus))));
            RuleFor(c => c.Fotos).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.Fotos).SetValidator(x => new VeiculoFotoValidator());
        }
    }
}
