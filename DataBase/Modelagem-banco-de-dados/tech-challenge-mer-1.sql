IF NOT EXISTS ( SELECT 1 FROM [sys].[databases] WHERE [NAME] = 'tech-challenge-revenda-de-veiculos')
BEGIN
	CREATE DATABASE [tech-challenge-revenda-de-veiculos]
END
GO