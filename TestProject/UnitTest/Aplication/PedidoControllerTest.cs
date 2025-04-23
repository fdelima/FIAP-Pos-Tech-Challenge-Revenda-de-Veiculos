using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.Controllers;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Application.UseCases.Veiculo.Commands;
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
    public partial class VeiculoControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Veiculo> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoControllerTest()
        {
            _configuration = Substitute.For<IConfiguration>();
            _mediator = Substitute.For<IMediator>();
            _validator = new VeiculoValidator();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoPostCommand>())
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
        public async Task InserirComDadosInvalidos(Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

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
        public async Task AlterarComDadosValidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idVeiculo, revendaDeVeiculos);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idVeiculo, Guid idDispositivo, ICollection<VeiculoFoto> items)
        {
            ///Arrange
            var revendaDeVeiculos = new Veiculo
            {
                IdVeiculo = idVeiculo,
                IdDispositivo = idDispositivo,
                VeiculoFotos = items
            };

            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idVeiculo, revendaDeVeiculos);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarVeiculo(Guid idVeiculo)
        {
            ///Arrange
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idVeiculo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarVeiculoPorId(Guid idVeiculo)
        {
            ///Arrange
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idVeiculo);

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
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos), 1, 1)));

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
        public async Task ConsultarVeiculoSemCondicao(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos), 1, 1)));

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
        public async Task ConsultarVeiculoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

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
        public async Task ConsultarVeiculoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange

            filter = null;
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);

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
        /// Testa a listar veiculos válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ListarVeiculosValidos(PagingQueryParam<Veiculo> filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos), 1, 1)));

            //Act
            var result = await aplicationController.GetListaAsync(filter);

            //Assert
            Assert.DoesNotContain(result.Content, x => x.Status.Equals(enmVeiculoStatus.FINALIZADO.ToString()));
        }

        /// <summary>
        /// Testa a listar veiculos inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 3)]
        public async Task ListarVeiculosInvalidos(PagingQueryParam<Veiculo> filter, Expression<Func<Veiculo, object>> sortProp, IEnumerable<Veiculo> veiculos)
        {
            ///Arrange
            var aplicationController = new VeiculoController(_configuration, _mediator, _validator);
            var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<VeiculoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Veiculo>(new List<Veiculo>(veiculos), 1, 1)));

            //Act
            var result = await aplicationController.GetListaAsync(filter);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmVeiculoStatus.FINALIZADO.ToString()));
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
