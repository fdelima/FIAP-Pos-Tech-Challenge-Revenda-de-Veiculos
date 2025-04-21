using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.ValuesObject;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class PedidoMock
    {

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid(),
                    new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                    }
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty,
                    new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 0
                        }
                    }
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var pedidos = new List<Pedido>();
                for (var index2 = 1; index <= quantidade; index++)
                {
                    var idPedido = Guid.NewGuid();
                    pedidos.Add(new Pedido
                    {
                        IdPedido = idPedido,
                        IdDispositivo = Guid.NewGuid(),
                        PedidoItems = new PedidoItem[]
                        {
                        new PedidoItem {
                            IdPedidoItem = Guid.NewGuid(),
                            IdPedido = idPedido,
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                        },
                        Status = ((enmPedidoStatus)new Random().Next(0, 2)).ToString(),
                    });
                }
                var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }

        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
        {
            var pedidos = new List<Pedido>();
            for (var index = 1; index <= quantidade; index++)
                pedidos.Add(new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    IdDispositivo = Guid.NewGuid(),
                    PedidoItems = new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                    },
                    Status = ((enmPedidoStatus)new Random().Next(0, 2)).ToString()
                });

            pedidos.Add(
                new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    PedidoItems = new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                    },
                    Status = enmPedidoStatus.FINALIZADO.ToString()
                });

            for (var index = 1; index <= quantidade; index++)
            {
                var param = new PagingQueryParam<Pedido>() { CurrentPage = index, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }
    }
}
