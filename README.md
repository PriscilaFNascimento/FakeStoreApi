
# FakeStoreApi

Este projeto é uma simulação de uma API básica de um e-commerce genérico, e possui como intuito praticar e demonstrar habilidades de desenvolvimento de APIs .NET utilizando a linguagem de programação C#.

## Autores

- [@PriscilaFNascimento](https://www.github.com/PriscilaFNascimento)


## Stack utilizada

C# | .NET | xUnit | PostgreSQL


## Funcionalidades

- Cadastro/login simplificado de clientes, apenas para título de demonstração
- Listagem de produtos utilizando a API externa https://fakestoreapi.com/
- Adição de produtos ao carrinho de compras do cliente
- Alteração de produtos que estão no carrinho de compras do cliente
- Geração de pedidos com base no carrinho de compras do cliente
- Consulta do histórico de pedidos do cliente
- Visualização de detalhes de um pedido
- Testes unitários para cada uma dessas funcionalidades


## Rodando localmente com Docker Compose
Rodar o projeto com o Docker Compose permitirá que você execute os contêineres da API e do banco de dados Postgres de forma simples e rápida.

Para isso, primeiramente certifique-se de ter a ferramenta Docker instalada na sua máquina. Para isso, abra uma nova janela do prompt de comando e digite

```
docker -v
```
Caso o comando **não** retorne as informações da versão do Docker, será necessário fazer a instalação. Para mais informações de instalação do Docker, ver https://www.docker.com/.

Após se certificar de ter uma versão do Docker instalada, prossiga com os seguintes passos para rodar o projeto:

Clone o projeto

```bash
  git clone https://github.com/PriscilaFNascimento/FakeStoreApi.git
```

Entre no diretório do projeto

```bash
  cd FakeStoreApi
```

Execute o comando

```bash
  docker-compose up --build
```

Aguarde até ser exibida a mensagem de sucesso da execução do comando. Após isso, a API ficará disponível no endpoint

```
http://localhost:8080
```

Já a instância do banco de dados PostgreSQL ficará disponível para conexão no endpoint
```
http://localhost:5433
```
Para acessar o banco de dados, utilize como usuário e senha "postgres"


## Rodando os testes via cli

Para rodar os testes, abra o prompt de comando e vá para a seguinte pasta do projeto:

```bash
  FakeStoreApi\Tests
```

Em seguida, rode o comando

```
dotnet test
```
Aguarde pela finalização da execução do comando. Após isso, verifique o resultado da execução dos testes, e certifique-se de que todos eles foram bem-sucedidos.