# 📚 F&MD Lab 2025 - API RESTful (.NET)

Desenvolvimento de uma API RESTful em .NET Core para gerenciamento do evento **F&MD Lab 2025**. Esta API permite o cadastro de palestras e participantes, além de uma integração com uma API externa de perguntas e respostas do tipo *trivia* para interação entre os participantes.

---

## 📌 Funcionalidades

- ✅ Cadastro de palestras
- ✅ Cadastro e gerenciamento de participantes
- ✅ Integração com API de Trivia (`https://opentdb.com/api.php?amount=1`)

---

## 🔗 Endpoints

### 🎤 Palestras

- `GET /api/palestras`  
  Retorna a lista de todas as palestras cadastradas.

- `POST /api/palestras`  
  Cadastra uma nova palestra.

### 👥 Participantes

- `GET /api/participantes`  
  Retorna a lista de todos os participantes cadastrados.

- `POST /api/participantes`  
  Cadastra um novo participante e o associa a uma palestra.

- `PUT /api/participantes/{participanteId}`  
  Atualiza os dados de um participante existente.

- `DELETE /api/participantes/{participanteId}`  
  Remove um participante do sistema.

> ⚠️ Regra de negócio: Um participante pode estar inscrito em **somente uma palestra**.  
> Uma palestra pode conter **múltiplos participantes**.

### ❓ Trivia

- `GET /api/trivia`  
  Retorna uma pergunta aleatória e sua resposta correta usando a API pública [Open Trivia Database](https://opentdb.com).

---

## 🛠 Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Swagger
- Moq + xUnit (para testes)
- API externa: [Open Trivia Database](https://opentdb.com)

---

## ⚙️ Como executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/fmdlab-api.git
   cd fmdlab-api
