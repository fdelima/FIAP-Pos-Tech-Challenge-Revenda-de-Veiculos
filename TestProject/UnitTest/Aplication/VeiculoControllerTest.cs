using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.Controllers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Vehicle.Commands;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;
using MediatR;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class VeiculoControllerTest
    {
        private readonly IMediator _mediator;
        private readonly IValidator<VeiculoEntity> _validator;
        private readonly IValidator<VeiculoPagamentoEntity> _PagamentoValidator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = new VeiculoValidator();
            _PagamentoValidator = new VeiculoPagamentoValidator();
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
                item.IdVeiculo = veiculo.IdVeiculo;

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.PostAsync(veiculo);

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

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.PostAsync(veiculo);

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

            foreach (VeiculoFotoEntity item in fotos)
                item.IdVeiculo = idVeiculo;

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.PutAsync(idVeiculo, veiculo);

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

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.PutAsync(idVeiculo, veiculo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
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

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.DeleteAsync(idVeiculo);

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

            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Mockando retorno da busca do veiculo.
            _mediator.Send(Arg.Any<VeiculoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoPagamentoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            ModelResult<VeiculoPagamentoEntity> result = await aplicationController.PostPagamentoAsync(pagamento);

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
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(veiculo)));

            //Act
            ModelResult<VeiculoEntity> result = await aplicationController.FindByIdAsync(idVeiculo);

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
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos), 1, 1)));

            //Act
            PagingQueryResult<VeiculoEntity> result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);

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
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos), 1, 1)));

            //Act
            PagingQueryResult<VeiculoEntity> result = await aplicationController.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarVeiculoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange

            filter = null;
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Act
            try
            {
                PagingQueryResult<VeiculoEntity> result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);
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
        public async Task ConsultarVeiculoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange

            filter = null;
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);

            //Act
            try
            {
                PagingQueryResult<VeiculoEntity> result = await aplicationController.GetItemsAsync(filter, sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
        }


        /// <summary>
        /// Testa a listar veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosAVenda(PagingQueryParam<VeiculoEntity> filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetVehiclesForSaleCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos), 1, 1)));

            //Act
            PagingQueryResult<VeiculoModel> result = await aplicationController.GetVehiclesForSaleAsync(filter);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmVeiculoStatus.VITRINE.ToString()));
        }

        /// <summary>
        /// Testa a listar veiculos a vendidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosVendidos(PagingQueryParam<VeiculoEntity> filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoController aplicationController = new VeiculoController(_mediator, _validator, _PagamentoValidator);
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetVehiclesForSaleCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos), 1, 1)));

            //Act
            PagingQueryResult<VeiculoModel> result = await aplicationController.GetVehiclesForSaleAsync(filter);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmVeiculoStatus.VENDIDO.ToString()));
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
