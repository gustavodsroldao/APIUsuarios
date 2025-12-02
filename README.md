# API de Gerenciamento de Usuários

## Descrição
API REST desenvolvida como parte da Avaliação Semestral da disciplina de Desenvolvimento Backend. O projeto implementa um sistema de gerenciamento de usuários com operações CRUD completas, seguindo os princípios de Clean Architecture e utilizando as melhores práticas do ecossistema .NET.

## Tecnologias Utilizadas
- .NET 8.0
- ASP.NET Core Minimal APIs
- Entity Framework Core 8.0
- SQLite
- FluentValidation
- Swagger

## Padrões de Projeto Implementados
- **Clean Architecture**: Separação clara de responsabilidades em camadas (Domain, Application, Infrastructure, API).
- **Repository Pattern**: Abstração da camada de acesso a dados.
- **Service Pattern**: Encapsulamento da lógica de negócios.
- **DTO Pattern**: Transferência de dados segura e desacoplada das entidades de domínio.
- **Dependency Injection**: Inversão de controle para melhor testabilidade e manutenção.

## Como Executar o Projeto

### Pré-requisitos
- .NET SDK 8.0 ou superior

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/api-usuarios-as-seu-nome.git
   ```
2. Navegue até a pasta do projeto:
   ```bash
   cd APIUsuarios
   ```
3. Execute as migrations para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```
4. Execute a aplicação:
   ```bash
   dotnet run
   ```
   A API estará disponível em `http://localhost:5000`.
   A documentação Swagger pode ser acessada em `http://localhost:5000/swagger` (em ambiente de desenvolvimento).

### Exemplos de Requisições

**Criar Usuário (POST /usuarios)**
```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "senha123",
  "dataNascimento": "1990-01-01",
  "telefone": "(11) 99999-9999"
}
```

**Listar Usuários (GET /usuarios)**
```bash
curl -X GET http://localhost:5000/usuarios
```

## Estrutura do Projeto
- **APIUsuarios**: Projeto principal contendo a configuração da API e endpoints.
  - **Domain**: Entidades e regras de negócio centrais.
  - **Application**: Interfaces, DTOs, Services e Validadores.
  - **Infrastructure**: Implementação de acesso a dados (EF Core, Repositories).

## Autor
Nome: Gustavo da Silveira Roldão
Curso: Desenvolvimento Backend
