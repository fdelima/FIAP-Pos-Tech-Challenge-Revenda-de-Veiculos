//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
//using FluentValidation;
//using NSubstitute;
//using System.Linq.Expressions;
//using TestProject.MockData;

//namespace TestProject.UnitTest.Domain
//{
//    /// <summary>
//    /// Classe de teste.
//    /// </summary>
//    public partial class VeiculoItemTest
//    {
//        private readonly IGateways<VeiculoFoto> _gatewayVeiculoItemMock;
//        private IValidator<VeiculoFoto> _validator;

//        /// <summary>
//        /// Construtor da classe de teste.
//        /// </summary>
//        public VeiculoItemTest()
//        {
//            _gatewayVeiculoItemMock = Substitute.For<IGateways<VeiculoFoto>>();
//            _validator = new VeiculoItemValidator();
//        }

//        /// <summary>
//        /// Testa a inserção com dados válidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
//        public async Task InserirComDadosValidos(DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = Guid.NewGuid(),
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Act
//            var result = await domainService.InsertAsync(veiculoItem);

//            //Assert
//            Assert.True(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a inserção com dados inválidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
//        public async Task InserirComDadosInvalidos(DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = Guid.NewGuid(),
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Act
//            var result = await domainService.InsertAsync(veiculoItem);

//            //Assert
//            Assert.False(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a alteração com dados válidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
//        public async Task AlterarComDadosValidos(Guid idVeiculoItem, DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = idVeiculoItem,
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Mockando retorno do metodo interno do UpdateAsync
//            _gatewayVeiculoItemMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoFoto, ICollection<object>>>>(), Arg.Any<Expression<Func<VeiculoFoto, bool>>>()).
//                Returns(new ValueTask<VeiculoFoto>(veiculoItem));

//            //Mockando retorno do metodo interno do UpdateAsync
//            _gatewayVeiculoItemMock.UpdateAsync(Arg.Any<VeiculoFoto>())
//                .Returns(Task.FromResult(veiculoItem));

//            //Act
//            var result = await domainService.UpdateAsync(veiculoItem);

//            //Assert
//            Assert.True(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a alteração com dados inválidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
//        public async Task AlterarComDadosInvalidos(Guid idVeiculoItem, DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = idVeiculoItem,
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Mockando retorno do metodo interno do UpdateAsync
//            _gatewayVeiculoItemMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoFoto, ICollection<object>>>>(), Arg.Any<Expression<Func<VeiculoFoto, bool>>>())
//                .Returns(new ValueTask<VeiculoFoto>(veiculoItem));

//            //Act
//            var result = await domainService.UpdateAsync(veiculoItem);

//            //Assert
//            Assert.False(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a consulta por id
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
//        public async Task DeletarVeiculoItem(Guid idVeiculoItem, DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = idVeiculoItem,
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Mockando retorno do metodo interno do FindByIdAsync
//            _gatewayVeiculoItemMock.FindByIdAsync(idVeiculoItem)
//                .Returns(new ValueTask<VeiculoFoto>(veiculoItem));

//            _gatewayVeiculoItemMock.DeleteAsync(idVeiculo)
//                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

//            //Act
//            var result = await domainService.DeleteAsync(idVeiculoItem);

//            //Assert
//            Assert.True(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a consulta por id
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
//        public async Task ConsultarVeiculoItemPorIdComDadosValidos(Guid idVeiculoItem, DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = idVeiculoItem,
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };
//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            // Mockando retorno do método interno do FindByIdAsync
//            _gatewayVeiculoItemMock.FindByIdAsync(idVeiculoItem)
//                .Returns(new ValueTask<VeiculoFoto>(veiculoItem));

//            //Act
//            var result = await domainService.FindByIdAsync(idVeiculoItem);

//            //Assert
//            Assert.NotNull(result);
//            Assert.True(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a consulta por id
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
//        public async Task ConsultarVeiculoItemPorIdComDadosInvalidos(Guid idVeiculoItem, DateTime data, Guid idVeiculo, Guid idProduto, int quantidade)
//        {
//            //Arrange
//            var veiculoItem = new VeiculoFoto
//            {
//                IdVeiculoItem = idVeiculoItem,
//                Data = data,
//                IdVeiculo = idVeiculo,
//                IdProduto = idProduto,
//                Quantidade = quantidade
//            };

//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Act
//            var result = await domainService.FindByIdAsync(idVeiculoItem);

//            //Assert
//            Assert.False(result.IsValid);
//        }

//        /// <summary>
//        /// Testa a consulta Valida
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
//        public async Task ConsultarVeiculoItem(IPagingQueryParam filter, List<VeiculoFoto> items)
//        {
//            //Arrange
//            var domainService = new VeiculoFotoService(_gatewayVeiculoItemMock, _validator);

//            //Mockando retorno do metodo interno do GetItemsAsync
//            _gatewayVeiculoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoFoto>>(),
//                    Arg.Any<Expression<Func<VeiculoFoto, object>>>())
//                .Returns(new ValueTask<PagingQueryResult<VeiculoFoto>>(new PagingQueryResult<VeiculoFoto>(new List<VeiculoFoto>(items))));

//            //Act
//            var result = await domainService.GetItemsAsync(filter, x => x.IdVeiculoItem.Equals(items.ElementAt(0).IdVeiculoItem));

//            //Assert
//            Assert.True(result.Content.Any());
//        }

//        /// <summary>
//        /// Testa a consulta com condição de pesquisa
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
//        public async Task ConsultarVeiculoItemComCondicao(IPagingQueryParam filter, List<VeiculoFoto> items)
//        {
//            //Arrange
//            var param = new PagingQueryParam<VeiculoFoto>() { CurrentPage = 1, Take = 10 };

//            //Mockando retorno do metodo interno do GetItemsAsync
//            _gatewayVeiculoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoFoto>>(),
//                    Arg.Any<Expression<Func<VeiculoFoto, bool>>>(),
//                    Arg.Any<Expression<Func<VeiculoFoto, object>>>())
//                .Returns(new ValueTask<PagingQueryResult<VeiculoFoto>>(new PagingQueryResult<VeiculoFoto>(new List<VeiculoFoto>(items))));

//            //Act
//            var result = await _gatewayVeiculoItemMock.GetItemsAsync(filter, x => x.IdVeiculoItem.Equals(items.ElementAt(0).IdVeiculoItem), x => x.Data);

//            //Assert
//            Assert.True(result.Content.Any());
//        }

//        /// <summary>
//        /// Testa a consulta sem condição de pesquisa
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
//        public async Task ConsultarVeiculoItemSemCondicao(IPagingQueryParam filter, IEnumerable<VeiculoFoto> items)
//        {
//            ///Arrange

//            //Mockando retorno do metodo interno do GetItemsAsync
//            _gatewayVeiculoItemMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoFoto>>(),
//                    Arg.Any<Expression<Func<VeiculoFoto, object>>>())
//                .Returns(new ValueTask<PagingQueryResult<VeiculoFoto>>(new PagingQueryResult<VeiculoFoto>(new List<VeiculoFoto>(items))));

//            //Act
//            var result = await _gatewayVeiculoItemMock.GetItemsAsync(filter, x => x.Data);

//            //Assert
//            Assert.True(result.Content.Any());
//        }

//        #region [ Xunit MemberData ]

//        /// <summary>
//        /// Mock de dados
//        /// </summary>
//        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
//        {
//            switch (tipo)
//            {
//                case enmTipo.Inclusao:
//                    if (dadosValidos)
//                        return VeiculoItemMock.ObterDadosValidos(quantidade);
//                    else
//                        return VeiculoItemMock.ObterDadosInvalidos(quantidade);
//                case enmTipo.Alteracao:
//                    if (dadosValidos)
//                        return VeiculoItemMock.ObterDadosValidos(quantidade)
//                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
//                    else
//                        return VeiculoItemMock.ObterDadosInvalidos(quantidade)
//                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
//                case enmTipo.Consulta:
//                    if (dadosValidos)
//                        return VeiculoItemMock.ObterDadosConsultaValidos(quantidade);
//                    else
//                        return VeiculoItemMock.ObterDadosConsultaInValidos(quantidade);
//                default:
//                    return null;
//            }
//        }

//        #endregion
//    }
//}
