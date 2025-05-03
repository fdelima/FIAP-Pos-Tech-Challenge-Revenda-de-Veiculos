using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Handlers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class VeiculoUseCasesTest
    {
        private readonly IVeiculoService _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoUseCasesTest()
        {
            _service = Substitute.For<IVeiculoService>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
            {
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

            var command = new VeiculoPostCommand(veiculo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            var handler = new VeiculoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
            {
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

            var command = new VeiculoPostCommand(veiculo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Veiculo>()));

            //Act
            var handler = new VeiculoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
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

            var command = new VeiculoPutCommand(idVeiculo, veiculo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            var handler = new VeiculoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
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

            var command = new VeiculoPutCommand(idVeiculo, veiculo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Veiculo>()));

            //Act
            var handler = new VeiculoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarVeiculo(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
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
            var command = new VeiculoDeleteCommand(idVeiculo);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            var handler = new VeiculoDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorId(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFoto> fotos)
        {
            ///Arrange
            var veiculo = new Veiculo
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

            var command = new VeiculoFindByIdCommand(idVeiculo);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            var handler = new VeiculoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoComCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };
            var command = new VeiculoGetItemsCommand(filter, sortProp, param.ConsultRule());

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Veiculo>>(),
                Arg.Any<Expression<Func<Veiculo, bool>>>(),
                Arg.Any<Expression<Func<Veiculo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos))));

            //Act
            var handler = new VeiculoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var command = new VeiculoGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos))));

            //Act
            var handler = new VeiculoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a lista de veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosAVenda(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var command = new VeiculoGetVehiclesForSaleCommand(filter);

            //Mockando retorno do serviço de domínio.
            _service.GetVehiclesForSaleAsync(filter)
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos))));

            //Act
            var handler = new VeiculoGetVehiclesForSaleHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a lista de veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosVendidos(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var command = new VeiculoGetVehiclesSoldCommand(filter);

            //Mockando retorno do serviço de domínio.
            _service.GetVehiclesSoldAsync(filter)
                .Returns(new ValueTask<PagingQueryResult<Veiculo>>(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos))));

            //Act
            var handler = new VeiculoGetVehiclesSoldHandler(_service);
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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return VeiculoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return VeiculoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
