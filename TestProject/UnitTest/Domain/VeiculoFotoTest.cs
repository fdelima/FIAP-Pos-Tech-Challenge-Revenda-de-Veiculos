using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace TestProject.UnitTest.Domain
{
    public class VeiculoFotoTests
    {
        [Fact]
        public void InsertDuplicatedRule_SameVeiculoIdAndImagem_ReturnsTrue()
        {
            // Arrange
            Guid veiculoId = Guid.NewGuid();
            string imagem = "foto1.png";
            VeiculoFotoEntity foto1 = new VeiculoFotoEntity { IdVeiculo = veiculoId, Imagem = imagem };
            VeiculoFotoEntity foto2 = new VeiculoFotoEntity { IdVeiculo = veiculoId, Imagem = imagem };

            // Act
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = foto1.InsertDuplicatedRule().Compile();
            bool isDuplicated = rule(foto2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void InsertDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange
            string imagem = "foto1.png";
            VeiculoFotoEntity foto1 = new VeiculoFotoEntity { IdVeiculo = Guid.NewGuid(), Imagem = imagem };
            VeiculoFotoEntity foto2 = new VeiculoFotoEntity { IdVeiculo = Guid.NewGuid(), Imagem = imagem };

            // Act
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = foto1.InsertDuplicatedRule().Compile();
            bool isDuplicated = rule(foto2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_SameVeiculoIdAndImagem_ReturnsTrue()
        {
            // Arrange
            Guid veiculoId = Guid.NewGuid();
            string imagem = "foto1.png";
            VeiculoFotoEntity foto1 = new VeiculoFotoEntity { IdVeiculoFoto = Guid.NewGuid(), IdVeiculo = veiculoId, Imagem = imagem };
            VeiculoFotoEntity foto2 = new VeiculoFotoEntity { IdVeiculoFoto = Guid.NewGuid(), IdVeiculo = veiculoId, Imagem = imagem };

            // Act
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = foto1.AlterDuplicatedRule().Compile();
            bool isDuplicated = rule(foto2);

            // Assert
            Assert.True(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange
            string imagem = "foto1.png";
            VeiculoFotoEntity foto1 = new VeiculoFotoEntity { IdVeiculo = Guid.NewGuid(), Imagem = imagem };
            VeiculoFotoEntity foto2 = new VeiculoFotoEntity { IdVeiculo = Guid.NewGuid(), Imagem = imagem };

            // Act
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = foto1.AlterDuplicatedRule().Compile();
            bool isDuplicated = rule(foto2);

            // Assert
            Assert.False(isDuplicated);
        }

        [Fact]
        public void Entity_PropertiesAreSetCorrectly()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid idVeiculo = Guid.NewGuid();
            VeiculoFotoEntity veiculo = new VeiculoFotoEntity
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
