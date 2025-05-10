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
            string placa, string renavam, decimal preco, string status, string thumb, ICollection<VeiculoFotoEntity> fotos)
        {
            ///Arrange
            VeiculoEntity veiculo = new VeiculoEntity
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

            VeiculoPostCommand command = new VeiculoPostCommand(veiculo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            VeiculoPostHandler handler = new VeiculoPostHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
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

            VeiculoPostCommand command = new VeiculoPostCommand(veiculo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<VeiculoEntity>()));

            //Act
            VeiculoPostHandler handler = new VeiculoPostHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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

            VeiculoPutCommand command = new VeiculoPutCommand(idVeiculo, veiculo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            VeiculoPutHandler handler = new VeiculoPutHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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

            VeiculoPutCommand command = new VeiculoPutCommand(idVeiculo, veiculo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<VeiculoEntity>()));

            //Act
            VeiculoPutHandler handler = new VeiculoPutHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarVeiculo(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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
            VeiculoDeleteCommand command = new VeiculoDeleteCommand(idVeiculo);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            VeiculoDeleteHandler handler = new VeiculoDeleteHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task PagamentoVieculo(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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
                item.IdVeiculo = idVeiculo;

            VeiculoPagamentoEntity pagamento = new VeiculoPagamentoEntity
            {
                IdVeiculo = idVeiculo,
                Banco = "Banco do Brasil",
                Conta = "1234",
                CpfCnpj = "12345678900",
                Data = DateTime.Now,
                ValorRecebido = 100000
            };

            VeiculoPagamentoPostCommand command = new VeiculoPagamentoPostCommand(veiculo, pagamento);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(veiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            VeiculoPagamentoPostHandler handler = new VeiculoPagamentoPostHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorId(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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

            VeiculoFindByIdCommand command = new VeiculoFindByIdCommand(idVeiculo);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            VeiculoFindByIdHandler handler = new VeiculoFindByIdHandler(_service);
            ModelResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculoComCondicao(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };
            VeiculoGetItemsCommand command = new VeiculoGetItemsCommand(filter, sortProp, param.ConsultRule());

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoEntity>>(),
                Arg.Any<Expression<Func<VeiculoEntity, bool>>>(),
                Arg.Any<Expression<Func<VeiculoEntity, object>>>())
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            VeiculoGetItemsHandler handler = new VeiculoGetItemsHandler(_service);
            PagingQueryResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

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
            VeiculoGetItemsCommand command = new VeiculoGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            VeiculoGetItemsHandler handler = new VeiculoGetItemsHandler(_service);
            PagingQueryResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a lista de veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosAVenda(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoGetVehiclesForSaleCommand command = new VeiculoGetVehiclesForSaleCommand(filter);

            //Mockando retorno do serviço de domínio.
            _service.GetVehiclesForSaleAsync(filter)
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            VeiculoGetVehiclesForSaleHandler handler = new VeiculoGetVehiclesForSaleHandler(_service);
            PagingQueryResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a lista de veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosVendidos(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoGetVehiclesSoldCommand command = new VeiculoGetVehiclesSoldCommand(filter);

            //Mockando retorno do serviço de domínio.
            _service.GetVehiclesSoldAsync(filter)
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            VeiculoGetVehiclesSoldHandler handler = new VeiculoGetVehiclesSoldHandler(_service);
            PagingQueryResult<VeiculoEntity> result = await handler.Handle(command, CancellationToken.None);

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
