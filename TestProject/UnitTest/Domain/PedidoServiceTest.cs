using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class VeiculoServiceTest
    {
        private readonly IGateways<Veiculo> _gatewayVeiculoMock;
        private readonly IValidator<Veiculo> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoServiceTest()
        {
            _validator = new VeiculoValidator();
            _gatewayVeiculoMock = Substitute.For<IGateways<Veiculo>>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Act
            var result = await domainService.InsertAsync(revendaDeVeiculos);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Act
            var result = await domainService.InsertAsync(revendaDeVeiculos);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Veiculo, ICollection<VeiculoFoto>>>>(), Arg.Any<Expression<Func<Veiculo, bool>>>())
                .Returns(new ValueTask<Veiculo>(revendaDeVeiculos));

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.UpdateAsync(Arg.Any<Veiculo>())
                .Returns(Task.FromResult(revendaDeVeiculos));

            //Act
            var result = await domainService.UpdateAsync(revendaDeVeiculos);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Veiculo, ICollection<VeiculoFoto>>>>(), Arg.Any<Expression<Func<Veiculo, bool>>>())
                .Returns(new ValueTask<Veiculo>(revendaDeVeiculos));

            //Act
            var result = await domainService.UpdateAsync(revendaDeVeiculos);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarVeiculo(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayVeiculoMock.FindByIdAsync(idVeiculo)
                .Returns(new ValueTask<Veiculo>(revendaDeVeiculos));

            _gatewayVeiculoMock.DeleteAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idVeiculo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorIdComDadosValidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Veiculo, ICollection<VeiculoFoto>>>>(), Arg.Any<Expression<Func<Veiculo, bool>>>())
                .Returns(new ValueTask<Veiculo>(revendaDeVeiculos));

            //Act
            var result = await domainService.FindByIdAsync(idVeiculo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorIdComDadosInvalidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Act
            var result = await domainService.FindByIdAsync(idVeiculo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculo(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> Veiculos)
        {
            ///Arrange
            var domainService = new VeiculoService(_gatewayVeiculoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Veiculo>>(),
                Arg.Any<Expression<Func<Veiculo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(Veiculos))));


            //Act
            var result = await domainService.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoComCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> Veiculos)
        {
            ///Arrange
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Veiculo>>(),
                Arg.Any<Expression<Func<Veiculo, bool>>>(),
                Arg.Any<Expression<Func<Veiculo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(Veiculos))));

            //Act
            var result = await _gatewayVeiculoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoSemCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> Veiculos)
        {
            ///Arrange

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(Veiculos))));

            //Act
            var result = await _gatewayVeiculoMock.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                    if (dadosValidos)
                        return VeiculoMock.ObterDadosValidos(quantidade);
                    else
                        return VeiculoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return VeiculoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return VeiculoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return VeiculoMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return VeiculoMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
