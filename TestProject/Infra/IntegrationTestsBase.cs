﻿namespace TestProject.Infra
{
    public class IntegrationTestsBase : IDisposable
    {
        internal readonly SqlServerTestFixture _sqlserverTest;
        private static int _tests = 0;

        public IntegrationTestsBase()
        {
            _tests += 1;
            _sqlserverTest = new SqlServerTestFixture("integration-test", port: "1430");
            Thread.Sleep(15000);
        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _sqlserverTest.Dispose();
            }
        }
    }
}
