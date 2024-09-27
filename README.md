# FinanceiroWeb

O FinanceiroWeb é um sistema de gestão financeira desenvolvido em ASP.NET Core com Entity Framework Core para controle de dados financeiros. Este sistema utiliza conteiner Docker para facilitar a implantação e oferece suporte para SQL Server como banco de dados.


## Versões das Ferramentas Utilizadas:

* ASP.NET Core SDK: 8.0
* Entity Framework Core: 8.0
* Banco de Dados: SQL Server (usando localdb para desenvolvimento)
* Docker: Imagem base: mcr.microsoft.com/dotnet/aspnet:8.0
* Docker: Imagem SDK: mcr.microsoft.com/dotnet/sdk:8.0
* IDE: Visual Studio 2022

## Instruções Básicas para Setup e Uso (Pré-requisitos):
* .NET SDK 8.0
* Docker
* SQL Server ou o SQL Server Express LocalDB que já vem com o Visual Studio.

### Setup do Banco de Dados:

* String de conexão no arquivo appsettings.json:<br>
`"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FinanceiroDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}`

* Executar migrations para criar as tabelas: No terminal do Visual Studio
`dotnet ef database update`

### Configuração do Docker

* Construir e executar o contêiner:<br>
Para rodar o sistema usando Docker, execute<br>
`docker build -t financeiro-web .
docker run -d -p 8080:8080 -p 8081:8081 financeiro-web
`

### Executando Localmente com Visual Studio
* Copie a url deste projeto: https://github.com/ThaysaRafaele/FinanceiroWeb.git
* No prompt de comando faça o clone do repositório com o comando: `git clone https://github.com/ThaysaRafaele/FinanceiroWeb.git`
* Abra o projeto no Visual Studio
* Pressione F5 para iniciar a depuração ou use o comando `dotnet run` via terminal para executar o projeto.


# Visão Geral:

## Dashboard:

![image](https://github.com/user-attachments/assets/029bbe7d-eb14-4ea1-8e53-bd1059ba4743)
![image](https://github.com/user-attachments/assets/2427c02e-3b96-47b9-8345-a6067b61a9f6)


## Notas Fiscais:

### Listagem:

![image](https://github.com/user-attachments/assets/ec2099aa-ba15-4c4d-b0f7-9426353cfd21)

### Listagem com filtro:

![image](https://github.com/user-attachments/assets/e171acb3-5543-4727-9d70-74f2e06e6fcd)

### Adição de nota:

![image](https://github.com/user-attachments/assets/c3028b4e-f8c0-4fb7-ac7c-bcafddb850ed)

### Edição de nota:

![image](https://github.com/user-attachments/assets/de5a1344-786c-47b8-ac03-323e7fb8402d)

### Exclusão de nota:

![image](https://github.com/user-attachments/assets/0a30aeb1-3596-4d09-b815-a42a4456defd)



