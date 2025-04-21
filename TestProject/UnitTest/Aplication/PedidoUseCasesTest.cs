using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoUseCasesTest
    {
        private readonly IPedidoService _service;
        private const string microServicoCadastroBaseAdress = "http://localhost:8082/";
        private const string microServicoProducaoBaseAdress = "http://localhost:8084/";
        private const string microServicoPagamentoBaseAdress = "http://localhost:8086/";
        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoUseCasesTest()
        {
            _service = Substitute.For<IPedidoService>();
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

            var command = new PedidoPostCommand(revendaDeVeiculos, microServicoCadastroBaseAdress, microServicoPagamentoBaseAdress);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Act
            var handler = new PedidoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPostCommand(revendaDeVeiculos, microServicoCadastroBaseAdress, microServicoPagamentoBaseAdress);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            var handler = new PedidoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPutCommand(idPedido, revendaDeVeiculos);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new PedidoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPutCommand(idPedido, revendaDeVeiculos);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            var handler = new PedidoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração do status do pagamento com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarStatusPagamentoComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var command = new PedidoAlterarStatusPagamentoCommand(idPedido, enmPedidoStatusPagamento.APROVADO, microServicoProducaoBaseAdress);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Act
            var handler = new PedidoAlterarStatusPagamentoHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração do status do pagamento com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarStatusPagamentoComDadosInValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var command = new PedidoAlterarStatusPagamentoCommand(idPedido, enmPedidoStatusPagamento.APROVADO, microServicoProducaoBaseAdress);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(revendaDeVeiculos)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Act
            var handler = new PedidoAlterarStatusPagamentoHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarPedido(Guid idPedido)
        {
            ///Arrange
            var command = new PedidoDeleteCommand(idPedido);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new PedidoDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var command = new PedidoFindByIdCommand(idPedido);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(revendaDeVeiculos)));

            //Act
            var handler = new PedidoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
            var command = new PedidoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            var handler = new PedidoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var command = new PedidoGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            var handler = new PedidoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a lista de pedidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarPedidos(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var command = new PedidoGetListaCommand(filter);

            //Mockando retorno do serviço de domínio.
            _service.GetListaAsync(filter)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            var handler = new PedidoGetIListaHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
