# FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos
TRABALHO SUB TECH CHALLENGE CURSO SOAT – PÓSTECH   FASE 2

Uma empresa de revenda de veículos automotores nos contratou pois quer implantar uma 
plataforma que funcione na internet, sendo assim, temos que criar a plataforma. O time de UX já 
está criando os designs, e ficou sob sua responsabilidade criar a API. O desenho da solução 
envolve as seguintes necessidades do negócio:  

- Cadastrar um veículo para venda (Marca, modelo, ano, cor, preço);  
- Editar os dados do veículo;  
- Efetuar a venda de um veículo (CPF da pessoa que comprou, data da venda);  
- Listagem de veículos à venda, ordenada por preço, do mais barato para o mais caro;  
- Listagem de veículos vendidos, ordenada por preço, do mais barato para o mais caro;  
- Disponibilizar um endpoint (webhook) para que a entidade que processa o pagamento consiga, a partir do código do pagamento, informar se o pagamento foi efetuado ou cancelado.  

Importante: nem todos os campos e funcionalidades necessárias para atender os 
requisitos estão descritos acima, por isso a modelagem é fundamental para entender como 
resolver o problema e entender o que precisa ser feito para que a solução funcione. 

Outro ponto importante é fazer a documentação dos endpoints de forma adequada para que o time de frontend possa fazer a integração de forma correta. O padrão a ser usado é o OpenAPI/Swagger. 
Além disso, como estamos com um novo time responsável pela infraestrutura, será necessário a implantação de Kubernetes. Foi solicitado que toda a descrição dos serviços a serem publicados sejam feitas neste padrão, usando deployment, configmap, secrets e services.

## Como executar no visual studio 2022
* Certifique-se que o docker desktop esteja em execução
* Abra a solução (FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos.sln) com o visual studio 2022
* Start visual studio na opção docker-compose conforme imagem abaixo:
![image](Documentacao/VS-2022-play-docker-compose.png)

## Como executar manualmente no windows
* Certifique-se que o docker desktop esteja em execução
* Após o clone do projeto abra a pasta "Docker" no prompt de comando conforme imagem abaixo:
![image](Documentacao/Abrir-Terminal.png)
* Excute o commando abaixo:
```
docker compose up
```
# Navegação
* Documentação 
    * [https://localhost:8081/api-docs](https://localhost:8081/api-docs/index.html)
    * [http://localhost:8080/api-docs](http://localhost:8080/api-docs/index.html) 
* Swagger
    * [https://localhost:8081/swagger](https://localhost:8081/swagger/index.html)
    * [http://localhost:8080/swagger](http://localhost:8080/swagger/index.html) 


# Banco de dados
## Diagrama entidade relacionamento (DER)
![image](Documentacao/Revenda-de-automoveis-DER.png)

 # Qualidade de software;
 ## **Teste realizados**  
 > Realizado teste de componente em BDD.  
 > Realizado teste de integração.  
 > Realizado teste unitários.  
 > * ![Teste realizados](Documentacao/tests.png)     
 >    
 > * **Code coverage**
 ![Code coverage 80%](Documentacao/code-coverage.png)  
 [Xunit Code Coverage :: Veja aqui mais detalhes](https://html-preview.github.io/?url=https://github.com/fdelima/FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos/blob/develop/TestProject/CodeCoverage/Report/index.html)
        - 
# Entregáveis 
## 1. PDF contendo os links de acesso aos itens abaixo:  
### 1.1 Repositório com o código-fonte do software (ver próximo item);  
- [Repositório :: FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos](https://github.com/fdelima/FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos)  

### 1.2 Vídeo demonstrando a solução funcionando, tanto na implementação da aplicação quanto na infraestrutura Kubernetes.  
- xxx

## 2. Conteúdo do Repositório:  
### 2.1 Arquivo Readme.md que explique o que é o projeto, como foi implementado, como usar localmente e como testar;  
- [Como executar no visual studio 2022](https://github.com/fdelima/FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos/tree/develop#como-executar-no-visual-studio-2022)
- [Como executar manualmente no windows](https://github.com/fdelima/FIAP-Pos-Tech-Challenge-Revenda-de-Veiculos/tree/develop#como-executar-manualmente-no-windows)

### 2.2 Código-fonte de software que funcione corretamente, implemente todas as necessidades acima descritas e implemente os conceitos SOLID e Clean Architecture de forma prescritiva;  
- ![image](Documentacao/estrutura-projeto-arquitetura-limpa.png)

### 2.3 Todos os arquivos “manifesto” Kubernetes para a implementação da solução em um cluster, o Dockerfile para o build da aplicação e o arquivo de definição dockercompose que descreva todos os componentes necessários para que a aplicação funcione corretamente e seja possível subir a aplicação localmente usando apenas o comando “docker compose up”.
- xxx
