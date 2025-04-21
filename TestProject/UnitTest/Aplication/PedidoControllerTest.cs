using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.Controllers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Pedido> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoControllerTest()
        {
            _configuration = Substitute.For<IConfiguration>();
            _mediator = Substitute.For<IMediator>();
            _validator = new PedidoValidator();
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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(revendaDeVeiculos);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.PostAsync(revendaDeVeiculos);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idPedido, revendaDeVeiculos);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idPedido, revendaDeVeiculos);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarPedido(Guid idPedido)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarPedidoPorId(Guid idPedido)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos), 1, 1)));

            //Act
            var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos), 1, 1)));

            //Act
            var result = await aplicationController.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarPedidoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarPedidoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange

            filter = null;
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.GetItemsAsync(filter, sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
        }


        /// <summary>
        /// Testa a listar pedidos válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarPedidosValidos(PagingQueryParam<Pedido> filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos), 1, 1)));

            //Act
            var result = await aplicationController.GetListaAsync(filter);

            //Assert
            Assert.DoesNotContain(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
        }

        /// <summary>
        /// Testa a listar pedidos inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 3)]
        public async Task ListarPedidosInvalidos(PagingQueryParam<Pedido> filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos), 1, 1)));

            //Act
            var result = await aplicationController.GetListaAsync(filter);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
