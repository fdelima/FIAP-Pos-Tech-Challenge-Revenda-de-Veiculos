using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;

namespace TestProject.UnitTest.Domain
{
    public class VeiculoPagamentoTest
    {
        [Fact]
        public void InsertDuplicatedRule_SameVeiculoIdValorCpfCnpj_ReturnsTrue()
        {
            // Arrange  
            Guid veiculoId = Guid.NewGuid();
            decimal valor = 1000.00m;
            string cpfCnpj = "123.456.789-00";
            VeiculoPagamentoEntity pagamento1 = new VeiculoPagamentoEntity
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            VeiculoPagamentoEntity pagamento2 = new VeiculoPagamentoEntity
            {
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = pagamento1.InsertDuplicatedRule().Compile();
            bool isDuplicated = rule(pagamento2);

            // Assert  
            Assert.True(isDuplicated);
        }

        [Fact]
        public void InsertDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange  
            decimal valor = 1000.00m;
            string cpfCnpj = "123.456.789-00";
            VeiculoPagamentoEntity pagamento1 = new VeiculoPagamentoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            VeiculoPagamentoEntity pagamento2 = new VeiculoPagamentoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = pagamento1.InsertDuplicatedRule().Compile();
            bool isDuplicated = rule(pagamento2);

            // Assert  
            Assert.False(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_SameVeiculoIdValorCpfCnpj_ReturnsTrue()
        {
            // Arrange  
            Guid veiculoId = Guid.NewGuid();
            decimal valor = 1000.00m;
            string cpfCnpj = "123.456.789-00";
            VeiculoPagamentoEntity pagamento1 = new VeiculoPagamentoEntity
            {
                IdVeiculoPagamento = Guid.NewGuid(),
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            VeiculoPagamentoEntity pagamento2 = new VeiculoPagamentoEntity
            {
                IdVeiculoPagamento = Guid.NewGuid(),
                IdVeiculo = veiculoId,
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = pagamento1.AlterDuplicatedRule().Compile();
            bool isDuplicated = rule(pagamento2);

            // Assert  
            Assert.True(isDuplicated);
        }

        [Fact]
        public void AlterDuplicatedRule_DifferentVeiculoId_ReturnsFalse()
        {
            // Arrange  
            decimal valor = 1000.00m;
            string cpfCnpj = "123.456.789-00";
            VeiculoPagamentoEntity pagamento1 = new VeiculoPagamentoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };
            VeiculoPagamentoEntity pagamento2 = new VeiculoPagamentoEntity
            {
                IdVeiculo = Guid.NewGuid(),
                ValorRecebido = valor,
                CpfCnpj = cpfCnpj,
                Banco = "Banco Itaú", // Fix for CS9035  
                Conta = "12345-6"     // Fix for CS9035  
            };

            // Act  
            Func<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Interfaces.IDomainEntity, bool> rule = pagamento1.AlterDuplicatedRule().Compile();
            bool isDuplicated = rule(pagamento2);

            // Assert  
            Assert.False(isDuplicated);
        }
        [Fact]
        public void Entity_PropertiesAreSetCorrectly()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid idVeiculo = Guid.NewGuid();
            DateTime data = DateTime.Now;
            VeiculoPagamentoEntity veiculo = new VeiculoPagamentoEntity
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
