Feature: ControlarVeiculos
	Para controlar os veiculos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um revendadeveiculos
	Alterar um revendadeveiculos
	Consultar um revendadeveiculos
	Listar os veiculos
	Deletar um revendadeveiculos

Scenario: Controlar veiculos
	Given Recebendo um revendadeveiculos na lanchonete vamos preparar o revendadeveiculos
	And Adicionar o revendadeveiculos
	And Encontrar o revendadeveiculos
	And Alterar o revendadeveiculos
	And Consultar o revendadeveiculos
	When Listar o revendadeveiculos
	Then posso deletar o revendadeveiculos