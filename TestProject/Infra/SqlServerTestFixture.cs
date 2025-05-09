using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class SqlServerTestFixture : IDisposable
    {
        const string _pwd = "SqlServer2019!";
        const string _network = "network-revendadeveiculos-test";

        //sqlserver
        private const string _imageName = "mcr.microsoft.com/mssql/server:2019-latest";
        private const string _dataBaseName = "tech-challenge-revenda-de-veiculos";
        private string _databaseContainerName = "sqlserver-db-revendadeveiculos";

        //Mssql Tools
        private const string _imageNameMssqlTools = "fdelima/fiap-pos-techchallenge-revendadeveiculos-scripts-database:fase2";
        private string _containerNameMssqlTools = "mssql-tools-revendadeveiculos";

        private string _port = string.Empty;

        public SqlServerTestFixture(string testName, string port)
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(_databaseContainerName))
                {
                    _port = port;
                    _databaseContainerName = string.Concat(_databaseContainerName, "-", testName);
                    _containerNameMssqlTools = string.Concat(_containerNameMssqlTools, "-", testName);
                    DockerManager.PullImageIfDoesNotExists(_imageName);
                    DockerManager.KillContainer(_databaseContainerName);
                    DockerManager.KillVolume(_databaseContainerName);

                    DockerManager.CreateNetWork(_network);

                    DockerManager.RunContainerIfIsNotRunning(_databaseContainerName,
                        $"run --name {_databaseContainerName} " +
                        $"-e ACCEPT_EULA=Y " +
                        $"-e MSSQL_SA_PASSWORD={_pwd} " +
                        $"-e MSSQL_PID=Developer " +
                        $"-p {port}:1433 " +
                        $"--network {_network} " +
                        $"-d {_imageName}");

                    Thread.Sleep(1000);

                    DockerManager.PullImageIfDoesNotExists(_imageNameMssqlTools);
                    DockerManager.KillContainer(_containerNameMssqlTools);
                    DockerManager.KillVolume(_containerNameMssqlTools);
                    DockerManager.RunContainerIfIsNotRunning(_containerNameMssqlTools,
                        $"run --name {_containerNameMssqlTools} " +
                        $"-e INSTANCE={_databaseContainerName} " +
                        $"-e USER=sa " +
                        $"-e PASSWORD={_pwd} " +
                        $"--network {_network} " +
                        $"-d {_imageNameMssqlTools}");

                    while (DockerManager.ContainerIsRunning(_containerNameMssqlTools))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Context GetDbContext()
        {
            string ConnectionString = $"Server=localhost,{_port}; Database={_dataBaseName}; User ID=sa; Password={_pwd}; MultipleActiveResultSets=true; TrustServerCertificate=True";

            DbContextOptions<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Context> options = new DbContextOptionsBuilder<FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Context>()
                                .UseSqlServer(ConnectionString).Options;

            return new FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Context(options);
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(_databaseContainerName);
                DockerManager.KillVolume(_databaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
