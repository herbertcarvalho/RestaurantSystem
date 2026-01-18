# 🍽️ Restaurant Reservation System

Sistema robusto de gerenciamento de reservas de restaurante desenvolvido em **.NET 10**. O projeto utiliza padrões arquiteturais modernos para garantir escalabilidade, manutenibilidade e separação de responsabilidades.

## 🚀 Tecnologias e Padrões

* **Runtime:** .NET 10
* **Arquitetura:** Clean Architecture + Domain-Driven Design (DDD)
* **Padrões:** CQRS (Command Query Responsibility Segregation)
* **Persistência:** Entity Framework Core & PostgreSQL
* **Segurança:** JWT com Refresh Token
* **Comunicação:** Domain Events para desacoplamento de processos

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, dividindo-se em:

1.  **Domain:** Entidades, Objetos de Valor, Agregados e Interfaces de Domínio.
2.  **Application:** Casos de uso (Commands/Queries), DTOs e Validadores.
3.  **Infrastructure:** Implementação de repositórios, contexto do banco de dados (EF Core) e serviços externos.
4.  **API:** Controllers e configurações de Middleware.

---

## 📋 Status das Reservas

O sistema gerencia o ciclo de vida da reserva através dos seguintes estados:

| Status | Descrição |
| :--- | :--- |
| `PENDING` | Reserva criada, aguardando confirmação. |
| `CONFIRMED` | Reserva confirmada pelo restaurante. |
| `CHECKED_IN` | Cliente presente no estabelecimento. |
| `REVIEW` | Atendimento finalizado, aguardando avaliação. |
| `COMPLETED` | Reserva finalizada e avaliada. |
| `CANCELLED` | Reserva cancelada pelo cliente ou sistema. |
| `NO_SHOW` | Cliente não compareceu no horário agendado. |

---

## 🔐 Autenticação

O sistema opera com um modelo de **usuário único**. Não há fluxo de cadastro de novos usuários.

* **Email:** `admin@restaurant.com`
* **Senha:** `Admin123!`

### Regras de Segurança e Tokens
* **Refresh Token:** Implementado para renovação automática de sessão.
* **Invalidating Strategy:** Ao realizar um novo login, **todos os refresh tokens antigos são invalidados**, garantindo que apenas a sessão mais recente seja válida.

---

## 🛠️ Funcionalidades e Regras de Negócio

### Fluxo da Reserva
* **Criar Reserva:** Status inicial `PENDING`. Se o cliente não existir, o sistema o cria automaticamente. Dispara um evento de criação.
* **Confirmar Reserva:** Permitido apenas para status `PENDING`. Dispara um evento de confirmação.
* **Check-In:** Permitido apenas para reservas `CONFIRMED`. Dispara um evento de check-in.
* **Completar Reserva:** Após o check-in, o restaurante marca a reserva como `REVIEW`.
* **Avaliar (Review):** Adiciona pontuação ao restaurante. Só pode ser realizado após o status `REVIEW`. Altera o status final para `COMPLETED`.

### Política de Cancelamento e Reembolso
O cancelamento é permitido desde que a reserva não esteja como `COMPLETED`, `CANCELLED` ou `NO_SHOW`. O reembolso segue a regra:

| Tempo de Antecedência | Reembolso |
| :--- | :--- |
| Mais de 24h | **100% do valor** |
| Entre 2h e 24h | **50% do valor** |
| Menos de 2h | **Sem reembolso** |

---

## ⚡ Observações Técnicas

* **Eventos de Domínio:** São disparados em todas as ações críticas (Criação, Confirmação, Check-in) para permitir integrações futuras com notificações (E-mail/SMS) ou Analytics.
* **Escalabilidade:** O uso de CQRS separa as operações de leitura e escrita, otimizando a performance do banco de dados.
* **Status do Projeto:** O sistema está parcialmente implementado. A prioridade atual é a finalização dos Handlers de domínio.

---

## 🐳 Como Rodar o Projeto

Este projeto está containerizado com **Docker**, o que torna a configuração do ambiente muito mais simples e rápida.

### 📋 Pré-requisitos

Antes de começar, você precisa ter instalado:
* [Docker](https://docs.docker.com/get-docker/)

---

### 🚀 Passo a Passo

Siga as etapas abaixo para subir a aplicação:

**1. Clonar o repositório**
git clone [https://github.com/seu-usuario/nome-do-projeto.git](https://github.com/seu-usuario/nome-do-projeto.git)

**2. Abrir cmd na pasta do projeto**
cd nome-do-projeto

**3. Subir docker compose**
docker compose up -d
