# Data-Lawyer-Crawler
Developing a crawler with ASP.NET Core REST API C#

## Acesso ao projeto

`https://github.com/edsoncesarDev/Data-Lawyer-Crawler`

## Abrir e rodar o projeto

`**OBS** Verifique se o seu Visual Studio executou o docker-compose automaticamente, se sim, apague as imagens e os containers, em seguida siga as orientações abaixo.`

- Após obter o repositório, faça o `build` da solução para compilar o código-fonte.
- Para executar o projeto é necessário ter o `Docker` instalado em seu dispositivo.
- Utilizando o visual studio, basta clicar em `Tools > Command Line > Developer PowerShell` para abrir uma janela do mesmo.
- Com isso basta digitar o comando: `docker-compose -f docker-compose.yml up -d`
- Logo após, abra seu navegador e digite o seguinte endereço: `http://localhost:5000/swagger/index.html`

## Funcionalidades

- `Registrar usuário` é necessário criar seu usuário para utilizar as demais funcionalidades do projeto.
- `Login` ao efetuar o login será gerado um token de autenticação, clique no campo com o nome `Authorize` e o preencha com `bearer + token` logo após você terá acesso aos demais endpoints.
- `Consultando e persistindo dados do processo via Crawler` para realizar e obter os dados da consulta via crawler, basta passar um número de um processo válido, com isso será persistido todas as informações no SGBD MySQL, importante ressaltar que não será possível cadastrar o mesmo processo já obtido.
- `Atualizar dados do processo` é possível atualizar os dados persistido, passando o `Id` do processo e as informações que deseja alterar.
- `Busca todos os processos` obtén todos os processos criados.
- `Busca apenas um processo` obtén apenas um processo definido pelo `Id`.
- `Deleta um processo` busca um processo pelo `Id` em seguida o remove.

## Verbos HTTP

### Users:
- `METHOD POST:` Registra usuário.
- `METHOD POST:` Efetua login.

### Processes:
- `METHOD POST:` Consulta e persiste dados do processo via Crawler.
- `METHOD GET:` Busca todos os processos cadastrados.
- `METHOD GET` Busca um processo pelo Id.
- `METHOD PUT` Atualiza um processo.
- `METHOD DELETE` Exclui um processo.
