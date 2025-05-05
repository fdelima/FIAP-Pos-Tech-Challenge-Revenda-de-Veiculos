using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Microsoft.VisualBasic;

namespace FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.TestProject.UnitTest.Domain
{
    public class VeiculoPagamentoTest
    {
        [Fact]
        public void InsertDuplicatedRule_SameVeiculoIdValorCpfCnpj_ReturnsTrue()
        {
            // Arrange  
            var veiculoId = Guid.NewGuid();
            var valor = 1000.00m;
            var cpfCnpj = "123.456.789-00";
            var pagamento1 = new VeiculoPagamento
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            var pagamento2 = new VeiculoPagamento
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            var rule = pagamento1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(pagamento2);

            // Assert  
            Assert.True(isDuplicated);
        }

        [Fact]
        public void InsertDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange  
            var valor = 1000.00m;
            var cpfCnpj = "123.456.789-00";
            var pagamento1 = new VeiculoPagamento
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            var pagamento2 = new VeiculoPagamento
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            var rule = pagamento1.InsertDuplicatedRule().Compile();
            var isDuplicated = rule(pagamento2);

            // Assert  
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_SameVeiculoIdValorCpfCnpj_ReturnsTrue()
        {
            // Arrange  
            var veiculoId = Guid.NewGuid();
            var valor = 1000.00m;
            var cpfCnpj = "123.456.789-00";
            var pagamento1 = new VeiculoPagamento
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            var pagamento2 = new VeiculoPagamento
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            var rule = pagamento1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(pagamento2);

            // Assert  
            Assert.True(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange  
            var valor = 1000.00m;
            var cpfCnpj = "123.456.789-00";
            var pagamento1 = new VeiculoPagamento
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            var pagamento2 = new VeiculoPagamento
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            var rule = pagamento1.AlterDuplicatedRule().Compile();
            var isDuplicated = rule(pagamento2);

            // Assert  
            Assert.False(isDuplicated);
        }
        [Fact]
        public void Entity_PropertiesAreSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var idVeiculo = Guid.NewGuid();
            var data = DateTime.Now;
            var veiculo = new VeiculoPagamento
            {
                IdVeiculoPagamento = id,
                IdVeiculo = idVeiculo,
                Data = data,
                ValorRecebido = 1000.00m,
                Banco = "Banco Itaú",
                Conta = "12345-6",
                CpfCnpj = "123.456.789-00"
            };

            // Assert
            Assert.Equal(veiculo.IdVeiculoPagamento, id);
            Assert.Equal(veiculo.IdVeiculo, idVeiculo);
            Assert.Equal(veiculo.Data, data);
            Assert.Equal(1000.00m, veiculo.ValorRecebido);
            Assert.Equal("Banco Itaú", veiculo.Banco);
            Assert.Equal("12345-6", veiculo.Conta);
            Assert.Equal("123.456.789-00", veiculo.CpfCnpj);
        }

    }
}
