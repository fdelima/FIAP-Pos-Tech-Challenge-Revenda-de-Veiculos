using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class VeiculoPagamentoValidator : AbstractValidator<VeiculoPagamento>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public VeiculoPagamentoValidator()
        {
            RuleFor(c => c.IdVeiculo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Data).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.ValorRecebido).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Banco).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Conta).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.CpfCnpj).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
