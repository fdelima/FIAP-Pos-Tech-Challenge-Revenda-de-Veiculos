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
    public partial class PedidoServiceTest
    {
        private readonly IGateways<Pedido> _gatewayPedidoMock;
        private readonly IValidator<Pedido> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoServiceTest()
        {
            _validator = new PedidoValidator();
            _gatewayPedidoMock = Substitute.For<IGateways<Pedido>>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

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
        public async Task InserirComDadosInvalidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

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
        public async Task AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(revendaDeVeiculos));

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.UpdateAsync(Arg.Any<Pedido>())
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
        public async Task AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(revendaDeVeiculos));

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
        public async Task DeletarPedido(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(revendaDeVeiculos));

            _gatewayPedidoMock.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(revendaDeVeiculos));

            //Act
            var result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosInvalidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            var result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedido(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));


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
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            var result = await _gatewayPedidoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            var result = await _gatewayPedidoMock.GetItemsAsync(filter, sortProp);

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
                        return PedidoMock.ObterDadosValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
