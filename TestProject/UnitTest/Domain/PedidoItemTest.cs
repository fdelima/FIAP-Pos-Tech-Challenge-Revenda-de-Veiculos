using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
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
    public partial class PedidoItemTest
    {
        private readonly IGateways<PedidoItem> _gatewayPedidoItemMock;
        private IValidator<PedidoItem> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoItemTest()
        {
            _gatewayPedidoItemMock = Substitute.For<IGateways<PedidoItem>>();
            _validator = new PedidoItemValidator();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = Guid.NewGuid(),
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Act
            var result = await domainService.InsertAsync(pedidoItem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = Guid.NewGuid(),
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Act
            var result = await domainService.InsertAsync(pedidoItem);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idPedidoItem, DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = idPedidoItem,
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoItemMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<PedidoItem, ICollection<object>>>>(), Arg.Any<Expression<Func<PedidoItem, bool>>>()).
                Returns(new ValueTask<PedidoItem>(pedidoItem));

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoItemMock.UpdateAsync(Arg.Any<PedidoItem>())
                .Returns(Task.FromResult(pedidoItem));

            //Act
            var result = await domainService.UpdateAsync(pedidoItem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idPedidoItem, DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = idPedidoItem,
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoItemMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<PedidoItem, ICollection<object>>>>(), Arg.Any<Expression<Func<PedidoItem, bool>>>())
                .Returns(new ValueTask<PedidoItem>(pedidoItem));

            //Act
            var result = await domainService.UpdateAsync(pedidoItem);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarPedidoItem(Guid idPedidoItem, DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = idPedidoItem,
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoItemMock.FindByIdAsync(idPedidoItem)
                .Returns(new ValueTask<PedidoItem>(pedidoItem));

            _gatewayPedidoItemMock.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idPedidoItem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoItemPorIdComDadosValidos(Guid idPedidoItem, DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = idPedidoItem,
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };
            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            // Mockando retorno do método interno do FindByIdAsync
            _gatewayPedidoItemMock.FindByIdAsync(idPedidoItem)
                .Returns(new ValueTask<PedidoItem>(pedidoItem));

            //Act
            var result = await domainService.FindByIdAsync(idPedidoItem);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoItemPorIdComDadosInvalidos(Guid idPedidoItem, DateTime data, Guid idPedido, Guid idProduto, int quantidade)
        {
            //Arrange
            var pedidoItem = new PedidoItem
            {
                IdPedidoItem = idPedidoItem,
                Data = data,
                IdPedido = idPedido,
                IdProduto = idProduto,
                Quantidade = quantidade
            };

            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Act
            var result = await domainService.FindByIdAsync(idPedidoItem);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoItem(IPagingQueryParam filter, List<PedidoItem> items)
        {
            //Arrange
            var domainService = new PedidoItemService(_gatewayPedidoItemMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<PedidoItem>>(),
                    Arg.Any<Expression<Func<PedidoItem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<PedidoItem>>(new PagingQueryResult<PedidoItem>(new List<PedidoItem>(items))));

            //Act
            var result = await domainService.GetItemsAsync(filter, x => x.IdPedidoItem.Equals(items.ElementAt(0).IdPedidoItem));

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoItemComCondicao(IPagingQueryParam filter, List<PedidoItem> items)
        {
            //Arrange
            var param = new PagingQueryParam<PedidoItem>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<PedidoItem>>(),
                    Arg.Any<Expression<Func<PedidoItem, bool>>>(),
                    Arg.Any<Expression<Func<PedidoItem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<PedidoItem>>(new PagingQueryResult<PedidoItem>(new List<PedidoItem>(items))));

            //Act
            var result = await _gatewayPedidoItemMock.GetItemsAsync(filter, x => x.IdPedidoItem.Equals(items.ElementAt(0).IdPedidoItem), x => x.Data);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoItemSemCondicao(IPagingQueryParam filter, IEnumerable<PedidoItem> items)
        {
            ///Arrange

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<PedidoItem>>(),
                    Arg.Any<Expression<Func<PedidoItem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<PedidoItem>>(new PagingQueryResult<PedidoItem>(new List<PedidoItem>(items))));

            //Act
            var result = await _gatewayPedidoItemMock.GetItemsAsync(filter, x => x.Data);

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
                        return PedidoItemMock.ObterDadosValidos(quantidade);
                    else
                        return PedidoItemMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return PedidoItemMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return PedidoItemMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return PedidoItemMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return PedidoItemMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion
    }
}
