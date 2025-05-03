using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Messages;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Models;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;

namespace TestProject.UnitTest.Domain
{
    public partial class DomainTest
    {

        [Fact]
        public void StringExtensionTest()
        {
            //Arrange
            const string valor = "ToSnakeCaseTest";
            const string expectedResult = "to_snake_case_test";
            //Act
            string? result = StringExtension.ToSnakeCase(valor);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void UtiTest()
        {
            //Arrange
            const string valor = "FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities";

            //Act
            Type[] result = Util.GetTypesInNamespace(valor);

            //Assert
            Assert.Contains(typeof(Veiculo), result);
            Assert.Contains(typeof(VeiculoFoto), result);
        }

        [Fact]
        public void DuplicatedResultTest()
        {
            //Arrange
            var veiculo = new Veiculo { Marca = "FIAP", Modelo = "Teste", AnoFabricacao = 2023, AnoModelo = 2023, Placa = "ABC1234", Renavam = "12345678901234567", Preco = 100000, Status = enmVeiculoStatus.VITRINE.ToString() };
            
            //Act
            var resut = ModelResultFactory.DuplicatedResult(veiculo);

            //Assert
            Assert.Contains(BusinessMessages.DuplicatedError<Veiculo>(), resut.ListErrors());
        }

        [Fact]
        public void NoneResultTest()
        {
            //Arrange
            //Act
            var resut = ModelResultFactory.None();

            //Assert
            Assert.True(resut.ListErrors().Count() == 0);
            Assert.True(resut.ListMessages().Count() == 0);
        }

        [Fact]
        public void MessageResultTest()
        {
            //Arrange
            const string msg = "mensagem";

            //Act
            var resut = ModelResultFactory.Message(new { }, msg);

            //Assert
            Assert.Contains(msg, resut.ListMessages());
        }

        [Fact]
        public void ErrorResultTest()
        {
            //Arrange
            const string msg = "erro";

            //Act
            var resut = ModelResultFactory.Error(new { }, msg);

            //Assert
            Assert.Contains(msg, resut.ListErrors());
        }
    }
}
