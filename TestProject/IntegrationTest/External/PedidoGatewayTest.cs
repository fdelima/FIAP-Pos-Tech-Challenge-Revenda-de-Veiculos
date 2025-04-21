using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Gateways;
using System.Linq.Expressions;
using TestProject.Infra;
using TestProject.MockData;

namespace TestProject.IntegrationTest.External
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly SqlServerTestFixture _sqlserverTest;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoGatewayTest(IntegrationTestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async void InserirComDadosValidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange            
            var idPedido = Guid.NewGuid();
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente
                IdPedido = idPedido,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            //Act
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {

            ///Arrange
            var idPedido = Guid.NewGuid();
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente não passando outros campos para dar erro
                IdPedido = idPedido,
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            //Act
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(false);
            }
            catch (InvalidOperationException)
            {
                Assert.True(true);
            }

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async void AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente
                IdPedido = idPedido,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(revendaDeVeiculos);
            await _pedidoGateway.CommitAsync();

            //Alterando
            revendaDeVeiculos.StatusPagamento = enmPedidoStatusPagamento.PROCESSANDO.ToString();

            var dbEntity = await _pedidoGateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == revendaDeVeiculos.IdPedido);

            //Act
            await _pedidoGateway.UpdateAsync(dbEntity, revendaDeVeiculos);
            await _pedidoGateway.UpdateAsync(revendaDeVeiculos);

            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async void AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente
                IdPedido = idPedido,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(revendaDeVeiculos);
            await _pedidoGateway.CommitAsync();

            //Alterando
            revendaDeVeiculos.Status = null;
            revendaDeVeiculos.StatusPagamento = null;

            var dbEntity = await _pedidoGateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == revendaDeVeiculos.IdPedido);

            //Act
            await _pedidoGateway.UpdateAsync(dbEntity, revendaDeVeiculos);
            await _pedidoGateway.UpdateAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(false);
            }
            catch (InvalidOperationException)
            {
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 1)]
        public async void DeletarPedido(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange            
            var idPedido = Guid.NewGuid();
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente
                IdPedido = idPedido,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            await _pedidoGateway.InsertAsync(revendaDeVeiculos);
            await _pedidoGateway.CommitAsync();

            //Act
            await _pedidoGateway.DeleteAsync(idPedido);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 1)]
        public async void ConsultarPedidoPorId(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange            
            var idPedido = Guid.NewGuid();
            var revendaDeVeiculos = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items,

                //Campos preenchidos automaticamente
                IdPedido = idPedido,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdPedidoItem = Guid.NewGuid();
                item.IdPedido = idPedido;
            }

            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            await _pedidoGateway.InsertAsync(revendaDeVeiculos);
            await _pedidoGateway.CommitAsync();

            //Act
            var result = await _pedidoGateway.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedido(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in pedidos)
                revendaDeVeiculos.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();

            await _pedidoGateway.InsertRangeAsync(pedidos);
            await _pedidoGateway.CommitAsync();

            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10, ObjFilter = pedidos.ElementAt(0) };

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in pedidos)
                revendaDeVeiculos.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();

            await _pedidoGateway.InsertRangeAsync(pedidos);
            await _pedidoGateway.CommitAsync();

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a base que lista todos os pedidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarPedidos(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in pedidos)
                revendaDeVeiculos.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();

            await _pedidoGateway.InsertRangeAsync(pedidos);
            await _pedidoGateway.CommitAsync();

            //Act
            var result = await _pedidoGateway.GetItemsAsync();

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
