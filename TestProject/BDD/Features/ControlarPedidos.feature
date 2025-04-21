Feature: ControlarPedidos
	Para controlar os pedidos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um revendadeveiculos
	Alterar um revendadeveiculos
	Consultar um revendadeveiculos
	Listar os pedidos
	Deletar um revendadeveiculos

Scenario: Controlar pedidos
	Given Recebendo um revendadeveiculos na lanchonete vamos preparar o revendadeveiculos
	And Adicionar o revendadeveiculos
	And Encontrar o revendadeveiculos
	And Alterar o revendadeveiculos
	And Consultar o revendadeveiculos
	When Listar o revendadeveiculos
	Then posso deletar o revendadeveiculos