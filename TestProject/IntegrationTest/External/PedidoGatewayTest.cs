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
    public partial class VeiculoGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly SqlServerTestFixture _sqlserverTest;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoGatewayTest(IntegrationTestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async void InserirComDadosValidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange            
            var idVeiculo = Guid.NewGuid();
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente
                IdVeiculo = idVeiculo,
                Status = enmVeiculoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            //Act
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            var result = await _veiculoGateway.InsertAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _veiculoGateway.CommitAsync();
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
        public async Task InserirComDadosInvalidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {

            ///Arrange
            var idVeiculo = Guid.NewGuid();
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente não passando outros campos para dar erro
                IdVeiculo = idVeiculo,
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            //Act
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            var result = await _veiculoGateway.InsertAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _veiculoGateway.CommitAsync();
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
        public async void AlterarComDadosValidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente
                IdVeiculo = idVeiculo,
                Status = enmVeiculoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            var result = await _veiculoGateway.InsertAsync(revendaDeVeiculos);
            await _veiculoGateway.CommitAsync();

            //Alterando
            revendaDeVeiculos.StatusPagamento = enmVeiculoStatusPagamento.PROCESSANDO.ToString();

            var dbEntity = await _veiculoGateway.FirstOrDefaultWithIncludeAsync(x => x.VeiculoFotos, x => x.IdVeiculo == revendaDeVeiculos.IdVeiculo);

            //Act
            await _veiculoGateway.UpdateAsync(dbEntity, revendaDeVeiculos);
            await _veiculoGateway.UpdateAsync(revendaDeVeiculos);

            try
            {
                await _veiculoGateway.CommitAsync();
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
        public async void AlterarComDadosInvalidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente
                IdVeiculo = idVeiculo,
                Status = enmVeiculoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            var result = await _veiculoGateway.InsertAsync(revendaDeVeiculos);
            await _veiculoGateway.CommitAsync();

            //Alterando
            revendaDeVeiculos.Status = null;
            revendaDeVeiculos.StatusPagamento = null;

            var dbEntity = await _veiculoGateway.FirstOrDefaultWithIncludeAsync(x => x.VeiculoFotos, x => x.IdVeiculo == revendaDeVeiculos.IdVeiculo);

            //Act
            await _veiculoGateway.UpdateAsync(dbEntity, revendaDeVeiculos);
            await _veiculoGateway.UpdateAsync(revendaDeVeiculos);

            //Assert
            try
            {
                await _veiculoGateway.CommitAsync();
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
        public async void DeletarVeiculo(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange            
            var idVeiculo = Guid.NewGuid();
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente
                IdVeiculo = idVeiculo,
                Status = enmVeiculoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            await _veiculoGateway.InsertAsync(revendaDeVeiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            await _veiculoGateway.DeleteAsync(idVeiculo);

            //Assert
            try
            {
                await _veiculoGateway.CommitAsync();
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
        public async void ConsultarVeiculoPorId(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange            
            var idVeiculo = Guid.NewGuid();
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items,

                //Campos preenchidos automaticamente
                IdVeiculo = idVeiculo,
                Status = enmVeiculoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmVeiculoStatusPagamento.PENDENTE.ToString()
            };

            foreach (var item in items)
            {
                item.IdVeiculoItem = Guid.NewGuid();
                item.IdVeiculo = idVeiculo;
            }

            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            await _veiculoGateway.InsertAsync(revendaDeVeiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            var result = await _veiculoGateway.FindByIdAsync(idVeiculo);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculo(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> Veiculos)
        {
            ///Arrange
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());

            //Act
            var result = await _veiculoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoComCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in veiculos)
                revendaDeVeiculos.StatusPagamento = enmVeiculoStatusPagamento.APROVADO.ToString();

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10, ObjFilter = veiculos.ElementAt(0) };

            //Act
            var result = await _veiculoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoSemCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in veiculos)
                revendaDeVeiculos.StatusPagamento = enmVeiculoStatusPagamento.APROVADO.ToString();

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            var result = await _veiculoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a base que lista todos os veiculos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculos(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var _veiculoGateway = new BaseGateway<Veiculo>(_sqlserverTest.GetDbContext());
            foreach (var revendaDeVeiculos in veiculos)
                revendaDeVeiculos.StatusPagamento = enmVeiculoStatusPagamento.APROVADO.ToString();

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            var result = await _veiculoGateway.GetItemsAsync();

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
