using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Services;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Validator;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class VeiculoServiceTest
    {
        private readonly IGateways<VeiculoEntity> _gatewayVeiculoMock;
        private readonly IValidator<VeiculoEntity> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoServiceTest()
        {
            _validator = new VeiculoValidator();
            _gatewayVeiculoMock = Substitute.For<IGateways<VeiculoEntity>>();
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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Act
            ModelResult<VeiculoEntity> result = await domainService.InsertAsync(veiculo);

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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Act
            ModelResult<VeiculoEntity> result = await domainService.InsertAsync(veiculo);

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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoEntity, ICollection<VeiculoFotoEntity>>>>(), Arg.Any<Expression<Func<VeiculoEntity, bool>>>())
                .Returns(new ValueTask<VeiculoEntity>(veiculo));

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.UpdateAsync(Arg.Any<VeiculoEntity>())
                .Returns(Task.FromResult(veiculo));

            //Act
            ModelResult<VeiculoEntity> result = await domainService.UpdateAsync(veiculo);

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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoEntity, ICollection<VeiculoFotoEntity>>>>(), Arg.Any<Expression<Func<VeiculoEntity, bool>>>())
                .Returns(new ValueTask<VeiculoEntity>(veiculo));

            //Act
            ModelResult<VeiculoEntity> result = await domainService.UpdateAsync(veiculo);

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
                Fotos = new List<VeiculoFotoEntity>(fotos)
            };

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoEntity, ICollection<VeiculoFotoEntity>>>>(), Arg.Any<Expression<Func<VeiculoEntity, bool>>>())
                .Returns(new ValueTask<VeiculoEntity>(veiculo));

            _gatewayVeiculoMock.DeleteAsync(idVeiculo)
                .Returns(Task.FromResult(ModelResultFactory.DeleteSucessResult<VeiculoEntity>()));

            //Act
            ModelResult<VeiculoEntity> result = await domainService.DeleteAsync(idVeiculo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorIdComDadosValidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayVeiculoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<VeiculoEntity, ICollection<VeiculoFotoEntity>>>>(), Arg.Any<Expression<Func<VeiculoEntity, bool>>>())
                .Returns(new ValueTask<VeiculoEntity>(veiculo));

            //Act
            ModelResult<VeiculoEntity> result = await domainService.FindByIdAsync(idVeiculo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarVeiculoPorIdComDadosInvalidos(Guid idVeiculo, string marca, string modelo, int anoFabricacao, int anoModelo,
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

            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Act
            ModelResult<VeiculoEntity> result = await domainService.FindByIdAsync(idVeiculo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculo(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoEntity>>(),
                Arg.Any<Expression<Func<VeiculoEntity, object>>>())
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));


            //Act
            PagingQueryResult<VeiculoEntity> result = await domainService.GetItemsAsync(filter, sortProp);

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
            PagingQueryParam<VeiculoEntity> param = new PagingQueryParam<VeiculoEntity>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoEntity>>(),
                Arg.Any<Expression<Func<VeiculoEntity, bool>>>(),
                Arg.Any<Expression<Func<VeiculoEntity, object>>>())
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            PagingQueryResult<VeiculoEntity> result = await _gatewayVeiculoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            PagingQueryResult<VeiculoEntity> result = await _gatewayVeiculoMock.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }


        /// <summary>
        /// Testa a consulta veiculos a venda
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculosAVenda(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoEntity>>(),
                Arg.Any<Expression<Func<VeiculoEntity, bool>>>(),
                Arg.Any<Expression<Func<VeiculoEntity, object>>>())
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            PagingQueryResult<VeiculoEntity> result = await domainService.GetVehiclesForSaleAsync(filter);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmVeiculoStatus.VITRINE.ToString()));
        }

        /// <summary>
        /// Testa a consulta veiculos vendidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarVeiculosVendidos(IPagingQueryParam filter, Expression<Func<VeiculoEntity, object>> sortProp, IEnumerable<VeiculoEntity> veiculos)
        {
            ///Arrange
            VeiculoService domainService = new VeiculoService(_gatewayVeiculoMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayVeiculoMock.GetItemsAsync(Arg.Any<PagingQueryParam<VeiculoEntity>>(),
                Arg.Any<Expression<Func<VeiculoEntity, bool>>>(),
                Arg.Any<Expression<Func<VeiculoEntity, object>>>())
                .Returns(new ValueTask<PagingQueryResult<VeiculoEntity>>(new PagingQueryResult<VeiculoEntity>(new List<VeiculoEntity>(veiculos))));

            //Act
            PagingQueryResult<VeiculoEntity> result = await domainService.GetVehiclesSoldAsync(filter);

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
                default:
                    return null;
            }
        }

        #endregion

    }
}
