using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace TestProject.UnitTest.Domain
{
    public class VeiculoFotoTests
    {
        [Fact]
        public void InsertDuplicatedRule_SameVeiculoIdAndImagem_ReturnsTrue()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var imagem = "foto1.png";
            var foto1 = new VeiculoFoto { IdVeiculo = veiculoId, Imagem = imagem };
            var foto2 = new VeiculoFoto { IdVeiculo = veiculoId, Imagem = imagem };

            // Act
            var rule = foto1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(foto2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void InsertDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange
            var imagem = "foto1.png";
            var foto1 = new VeiculoFoto { IdVeiculo = Guid.NewGuid(), Imagem = imagem };
            var foto2 = new VeiculoFoto { IdVeiculo = Guid.NewGuid(), Imagem = imagem };

            // Act
            var rule = foto1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(foto2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_SameVeiculoIdAndImagem_ReturnsTrue()
        {
            // Arrange
            var veiculoId = Guid.NewGuid();
            var imagem = "foto1.png";
            var foto1 = new VeiculoFoto { IdVeiculoFoto = Guid.NewGuid(),  IdVeiculo = veiculoId, Imagem = imagem };
            var foto2 = new VeiculoFoto { IdVeiculoFoto = Guid.NewGuid(), IdVeiculo = veiculoId, Imagem = imagem };

            // Act
            var rule = foto1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(foto2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange
            var imagem = "foto1.png";
            var foto1 = new VeiculoFoto { IdVeiculo = Guid.NewGuid(), Imagem = imagem };
            var foto2 = new VeiculoFoto { IdVeiculo = Guid.NewGuid(), Imagem = imagem };

            // Act
            var rule = foto1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(foto2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void Entity_PropertiesAreSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var idVeiculo = Guid.NewGuid();
            var veiculo = new VeiculoFoto
            {
                IdVeiculoFoto = id,
                IdVeiculo = idVeiculo,
                Imagem = "image.png"
            };
            

            // Assert
            Assert.Equal(veiculo.IdVeiculoFoto, id);
            Assert.Equal(veiculo.IdVeiculo, idVeiculo);
            Assert.Equal("image.png", veiculo.Imagem);
        }

    }
}
