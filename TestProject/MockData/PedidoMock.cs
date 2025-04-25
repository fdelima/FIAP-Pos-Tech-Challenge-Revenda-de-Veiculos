//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
//using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;

//namespace TestProject.MockData
//{
//    /// <summary>
//    /// Mock de dados das ações
//    /// </summary>
//    public class VeiculoMock
//    {

//        /// <summary>
//        /// Mock de dados válidos
//        /// </summary>
//        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
//        {
//            for (var index = 1; index <= quantidade; index++)
//                yield return new object[]
//                {
//                    Guid.NewGuid(),
//                    new VeiculoFoto[]
//                    {
//                        new VeiculoFoto {
//                            IdProduto =  Guid.NewGuid(),
//                            Quantidade = 1
//                        }
//                    }
//                };
//        }

//        /// <summary>
//        /// Mock de dados inválidos
//        /// </summary>
//        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
//        {
//            for (var index = 1; index <= quantidade; index++)
//                yield return new object[]
//                {
//                    Guid.Empty,
//                    new VeiculoFoto[]
//                    {
//                        new VeiculoFoto {
//                            IdProduto =  Guid.NewGuid(),
//                            Quantidade = 0
//                        }
//                    }
//                };
//        }

//        /// <summary>
//        /// Mock de dados válidos
//        /// </summary>
//        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
//        {
//            for (var index = 1; index <= quantidade; index++)
//            {
//                var veiculos = new List<Veiculo>();
//                for (var index2 = 1; index <= quantidade; index++)
//                {
//                    var idVeiculo = Guid.NewGuid();
//                    veiculos.Add(new Veiculo
//                    {
//                        IdVeiculo = idVeiculo,
//                        IdDispositivo = Guid.NewGuid(),
//                        VeiculoFotos = new VeiculoFoto[]
//                        {
//                        new VeiculoFoto {
//                            IdVeiculoItem = Guid.NewGuid(),
//                            IdVeiculo = idVeiculo,
//                            IdProduto =  Guid.NewGuid(),
//                            Quantidade = 1
//                        }
//                        },
//                        Status = ((enmVeiculoStatus)new Random().Next(0, 2)).ToString(),
//                    });
//                }
//                var param = new PagingQueryParam<Veiculo>() { CurrentPage = 1, Take = 10 };
//                yield return new object[]
//                {
//                    param,
//                    param.SortProp(),
//                    veiculos
//                };
//            }

//        }

//        /// <summary>
//        /// Mock de dados inválidos
//        /// </summary>
//        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
//        {
//            var veiculos = new List<Veiculo>();
//            for (var index = 1; index <= quantidade; index++)
//                veiculos.Add(new Veiculo
//                {
//                    IdVeiculo = Guid.NewGuid(),
//                    IdDispositivo = Guid.NewGuid(),
//                    VeiculoFotos = new VeiculoFoto[]
//                    {
//                        new VeiculoFoto {
//                            IdProduto =  Guid.NewGuid(),
//                            Quantidade = 1
//                        }
//                    },
//                    Status = ((enmVeiculoStatus)new Random().Next(0, 2)).ToString()
//                });

//            veiculos.Add(
//                new Veiculo
//                {
//                    IdVeiculo = Guid.NewGuid(),
//                    VeiculoFotos = new VeiculoFoto[]
//                    {
//                        new VeiculoFoto {
//                            IdProduto =  Guid.NewGuid(),
//                            Quantidade = 1
//                        }
//                    },
//                    Status = enmVeiculoStatus.FINALIZADO.ToString()
//                });

//            for (var index = 1; index <= quantidade; index++)
//            {
//                var param = new PagingQueryParam<Veiculo>() { CurrentPage = index, Take = 10 };
//                yield return new object[]
//                {
//                    param,
//                    param.SortProp(),
//                    veiculos
//                };
//            }
//        }

//        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
//        {
//            for (var index = 1; index <= quantidade; index++)
//                yield return new object[]
//                {
//                    Guid.NewGuid()
//                };
//        }

//        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
//        {
//            for (var index = 1; index <= quantidade; index++)
//                yield return new object[]
//                {
//                    Guid.Empty
//                };
//        }
//    }
//}
