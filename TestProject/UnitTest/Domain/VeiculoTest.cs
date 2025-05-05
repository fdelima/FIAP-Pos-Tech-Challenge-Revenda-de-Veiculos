using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace TestProject.UnitTest.Domain
{
    public class VeiculoTests
    {
        [Fact]
        public void InsertDuplicatedRule_SameRenavam_ReturnsTrue()
        {
            // Arrange
            var veiculo1 = new Veiculo
            {
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };
            var veiculo2 = new Veiculo
            {
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };

            // Act
            var rule = veiculo1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(veiculo2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void InsertDuplicatedRule_DifferentRenavam_ReturnsFalse()
        {
            // Arrange
            var veiculo1 = new Veiculo
            {
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };
            var veiculo2 = new Veiculo
            {
                Renavam = "XYZ987654321",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };

            // Act
            var rule = veiculo1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(veiculo2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentIdSameRenavam_ReturnsTrue()
        {
            // Arrange
            var veiculo1 = new Veiculo
            {
                IdVeiculo = Guid.NewGuid(),
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };
            var veiculo2 = new Veiculo
            {
                IdVeiculo = Guid.NewGuid(),
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };

            // Act
            var rule = veiculo1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(veiculo2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_SameIdSameRenavam_ReturnsFalse()
        {
            // Arrange
            var veiculo1 = new Veiculo
            {
                IdVeiculo = Guid.NewGuid(),
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };
            var veiculo2 = new Veiculo
            {
                IdVeiculo = veiculo1.IdVeiculo,
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };

            // Act
            var rule = veiculo1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(veiculo2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentIdDifferentRenavam_ReturnsFalse()
        {
            // Arrange
            var veiculo1 = new Veiculo
            {
                IdVeiculo = Guid.NewGuid(),
                Renavam = "ABC123456789",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };
            var veiculo2 = new Veiculo
            {
                IdVeiculo = Guid.NewGuid(),
                Renavam = "XYZ987654321",
                Marca = "Fiat",
                Modelo = "Uno",
                Placa = "XYZ-9876",
                Status = "Disponível"
            };

            // Act
            var rule = veiculo1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(veiculo2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void Entity_PropertiesAreSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var veiculo = new Veiculo
            {
                IdVeiculo = id,
                Marca = "Fiat",
                Modelo = "Uno",
                AnoFabricacao = 2020,
                AnoModelo = 2021,
                Placa = "XYZ-9876",
                Renavam = "1234567890123",
                Preco = 35000.50m,
                Thumb = "image.png",
                Status = "Disponível"
            };

            // Assert
            Assert.Equal(veiculo.IdVeiculo, id);
            Assert.Equal("Fiat", veiculo.Marca);
            Assert.Equal("Uno", veiculo.Modelo);
            Assert.Equal(2020, veiculo.AnoFabricacao);
            Assert.Equal(2021, veiculo.AnoModelo);
            Assert.Equal("XYZ-9876", veiculo.Placa);
            Assert.Equal("1234567890123", veiculo.Renavam);
            Assert.Equal(35000.50m, veiculo.Preco);
            Assert.Equal("image.png", veiculo.Thumb);
            Assert.Equal("Disponível", veiculo.Status);
        }
    }
}
