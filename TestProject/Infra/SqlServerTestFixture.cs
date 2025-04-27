using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class SqlServerTestFixture : IDisposable
    {
        const string pwd = "SqlServer2019!";
        const string network = "network-revendaDeVeiculos-test";

        //sqlserver
        private const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";
        private const string DataBaseName = "tech-challenge-revendaDeVeiculos";

        string _port; string _databaseContainerName;

        public SqlServerTestFixture(string imageNameMssqlTools,
                                    string containerNameMssqlTools,
                                    string databaseContainerName, string port)
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(databaseContainerName))
                {
                    _port = port;
                    _databaseContainerName = databaseContainerName;
                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(databaseContainerName);
                    DockerManager.KillVolume(databaseContainerName);

                    DockerManager.CreateNetWork(network);

                    DockerManager.RunContainerIfIsNotRunning(databaseContainerName,
                        $"run --name {databaseContainerName} " +
                        $"-e ACCEPT_EULA=Y " +
                        $"-e MSSQL_SA_PASSWORD={pwd} " +
                        $"-e MSSQL_PID=Developer " +
                        $"-p {port}:1433 " +
                        $"--network {network} " +
                        $"-d {ImageName}");

                    Thread.Sleep(1000);

                    DockerManager.PullImageIfDoesNotExists(imageNameMssqlTools);
                    DockerManager.KillContainer(containerNameMssqlTools);
                    DockerManager.KillVolume(containerNameMssqlTools);
                    DockerManager.RunContainerIfIsNotRunning(containerNameMssqlTools,
                        $"run --name {containerNameMssqlTools} " +
                        $"--network {network} " +
                        $"-d {imageNameMssqlTools}");

                    while (DockerManager.ContainerIsRunning(containerNameMssqlTools))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public FIAP.Pos.Tech.Challenge.RevendaDeVeiculos.Infra.Context GetDbContext()
        {
            string ConnectionString = $"Server=localhost,{_port}; Database={DataBaseName}; User ID=sa; Password={pwd}; MultipleActiveResultSets=true; TrustServerCertificate=True";

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
