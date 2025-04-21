using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class PedidoValidator : AbstractValidator<Entities.Pedido>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public PedidoValidator()
        {
            RuleFor(c => c.IdDispositivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.PedidoItems).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.PedidoItems).SetValidator(x => new PedidoItemValidator());
        }
    }
}
