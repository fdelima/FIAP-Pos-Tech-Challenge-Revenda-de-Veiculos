using System.Collections;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados para consulta
    /// </summary>
    public class PedidoConsultaMock : IEnumerable<object[]>
    {
        /// <summary>
        /// Retorna o mock de dados para consulta
        /// </summary>
        public IEnumerator<object[]> GetEnumerator()
        {
            var filtro = new PedidoParam
            {
                PageSize = 10,
                CurrentPage = 1,
                Direction = "ASC",
                OrderBy = "DESCRICAO"
            };

            yield return new object[] { filtro };
        }

        /// <summary>
        /// Retorna o mock de dados para consulta
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
