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
        public async void InserirComDadosValidos(string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            //Act
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            VeiculoEntity result = await _veiculoGateway.InsertAsync(veiculo);

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
        public async Task InserirComDadosInvalidos(string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            //Act
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            VeiculoEntity result = await _veiculoGateway.InsertAsync(veiculo);

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
        public async void AlterarComDadosValidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = idVeiculo,
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            VeiculoEntity result = await _veiculoGateway.InsertAsync(veiculo);
            await _veiculoGateway.CommitAsync();

            //Alterando
            veiculo.Preco = veiculo.Preco * 1.58m;

            VeiculoEntity? dbEntity = await _veiculoGateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == veiculo.IdVeiculo);

            //Act
            await _veiculoGateway.UpdateAsync(dbEntity, veiculo);
            await _veiculoGateway.UpdateAsync(veiculo);

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
        public async void AlterarComDadosInvalidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = idVeiculo,
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            VeiculoEntity result = await _veiculoGateway.InsertAsync(veiculo);
            await _veiculoGateway.CommitAsync();

            //Alterando
            veiculo.Status = null;
            veiculo.Placa = null;
            veiculo.Renavam = null;

            VeiculoEntity? dbEntity = await _veiculoGateway.FirstOrDefaultWithIncludeAsync(x => x.Fotos, x => x.IdVeiculo == veiculo.IdVeiculo);

            //Act
            await _veiculoGateway.UpdateAsync(dbEntity, veiculo);
            await _veiculoGateway.UpdateAsync(veiculo);

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
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
        public async void DeletarVeiculo(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = idVeiculo,
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            await _veiculoGateway.InsertAsync(veiculo);
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
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
        public async void ConsultarVeiculoPorId(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
            {
                IdVeiculo = idVeiculo,
                Marca = marca,
                Modelo = modelo,
                AnoFabricacao = anoFabricacao,
                AnoModelo = anoModelo,
                Placa = placa,
                Renavam = renavam,
                Preco = preco,
                Status = status,
                Thumb = thumb,
                Fotos = fotos
            };

            foreach (VeiculoFotoEntity item in fotos)
            {
                item.IdVeiculoFoto = Guid.NewGuid();
                item.IdVeiculo = veiculo.IdVeiculo;
            }

            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());
            await _veiculoGateway.InsertAsync(veiculo);
            await _veiculoGateway.CommitAsync();

            //Act
            VeiculoEntity? result = await _veiculoGateway.FindByIdAsync(idVeiculo);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculo(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> Veiculos)
        {
            ///Arrange
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());

            //Act
            PagingQueryResult<VeiculoEntity> result = await _veiculoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoComCondicao(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10, ObjFilter = veiculos.ElementAt(0) };

            //Act
            PagingQueryResult<VeiculoEntity> result = await _veiculoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoSemCondicao(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            PagingQueryResult<VeiculoEntity> result = await _veiculoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a listar veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosAVenda(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            PagingQueryResult<VeiculoEntity> result = await _veiculoGateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VITRINE.ToString(), o => o.Preco);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a listar veiculos vendidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosVendidos(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            BaseGateway<VeiculoEntity> _veiculoGateway = new BaseGateway<VeiculoEntity>(_sqlserverTest.GetDbContext());

            await _veiculoGateway.InsertRangeAsync(veiculos);
            await _veiculoGateway.CommitAsync();

            //Act
            PagingQueryResult<VeiculoEntity> result = await _veiculoGateway.GetItemsAsync(filter, x => x.Status == enmVeiculoStatus.VENDIDO.ToString(), o => o.Preco);

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
