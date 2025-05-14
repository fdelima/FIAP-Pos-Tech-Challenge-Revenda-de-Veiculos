Feature: ControlarVeiculos
	Para controlar os veiculos da revenda
	Eu preciso das seguindes funcionalidades
	Adicionar um veiculo
	Alterar um veiculo
	Consultar um veiculo
	Listar os veiculos
	Deletar um veiculo

Scenario: Controlar veiculos
	Given Recebendo um veiculo na revenda vamos preparar o veiculo
	And Adicionar o veiculo
	And Encontrar o veiculo
	And Alterar o veiculo
	And Consultar o veiculo
	And Listar os veiculos a venda
	When Listar os veiculos vendidos
	Then posso deletar o veiculo