using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain;
using FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    [FeatureFile("./BDD/Features/ControlarVeiculos.feature")]
    public class VeiculoControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        Veiculo _veiculo;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public VeiculoControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Veiculo Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um revendaDeVeiculos na lanchonete vamos preparar o revendaDeVeiculos")]
        public void PrepararVeiculo()
        {
            _veiculo = new Veiculo();
            _veiculo.IdDispositivo = Guid.NewGuid();
            _veiculo.VeiculoFotos.Add(new VeiculoFoto()
            {
                IdProduto = Guid.Parse("f724910b-ed6d-41a2-ab52-da4cd26ba413"),
                Quantidade = 1
            });
            _veiculo.VeiculoFotos.Add(new VeiculoFoto
            {
                IdProduto = Guid.Parse("802be132-64ef-4737-9de7-c83298c70a73"),
                Quantidade = 1
            });
            _veiculo.VeiculoFotos.Add(new VeiculoFoto
            {
                IdProduto = Guid.Parse("f44b20ab-a453-4579-accf-d94d7075f508"),
                Quantidade = 1
            });
        }

        [And(@"Adicionar o revendaDeVeiculos")]
        public async Task AdicionarVeiculo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/revendaDeVeiculos/checkout", _veiculo);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _veiculo = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Encontrar o revendaDeVeiculos")]
        public async Task EncontrarVeiculo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/revendaDeVeiculos/{_veiculo.IdVeiculo}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _veiculo = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Alterar o revendaDeVeiculos")]
        public async Task AlterarVeiculo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/revendaDeVeiculos/{_veiculo.IdVeiculo}", _veiculo);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _veiculo = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Consultar o revendaDeVeiculos")]
        public async Task ConsultarVeiculo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/revendaDeVeiculos/consult", new PagingQueryParam<Veiculo> { ObjFilter = _veiculo });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [When(@"Listar o revendaDeVeiculos")]
        public async Task ListarPardido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/revendaDeVeiculos/Lista");

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o revendaDeVeiculos")]
        public async Task DeletarVeiculo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/revendaDeVeiculos/{_veiculo.IdVeiculo}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _veiculo = null;

            Assert.True(actualResult.IsValid);
        }
    }
}
