# Data-Lawyer-Crawler
Developing a crawler with ASP.NET Core REST API C#

## Acesso ao projeto

https://github.com/edsoncesarDev/Data-Lawyer-Crawler

## Abrir e rodar o projeto

**OBS** Verifique se o seu Visual Studio executou o docker-compose automaticamente, se sim, apague as imagens e os containers, em seguida siga as orientações abaixo.

- Após obter o repositório, faça o **build** da solução para compilar o código-fonte.
- Para executar o projeto é necessário ter o **Docker** instalado em seu dispositivo.
- Utilizando o visual studio, basta clicar em **Tools > Command Line > Developer PowerShell** para abrir uma janela do mesmo.
- Com isso basta digitar o comando: **docker-compose -f docker-compose.yml up -d**
- Logo após, abra seu navegador e digite o seguinte endereço: **http://localhost:5000/swagger/index.html**

## Funcionalidades

- **Registrar usuário** METHOD POST, é necessário criar seu usuário para utilizar as demais funcionalidades do projeto.
- **Login** METHOD POST, ao efetuar o login será gerado um token de autenticação clique no campo com o nome `Authorize` e o preencha com `bearer + token` logo após você terá acesso aos demais endpoints.
