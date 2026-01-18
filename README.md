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

## 🔐 Autenticação e Segurança

O sistema utiliza um modelo de usuário administrativo único para gestão do restaurante.

* **Email:** `admin@restaurant.com`
* **Senha:** `Admin123!`

### Regras de Login
* **Refresh Token:** Implementado para renovação de sessão sem necessidade de novo login manual.
* **Invalidating Strategy:** Ao realizar um login, todos os refresh tokens antigos do usuário são invalidados, garantindo que apenas a sessão mais recente permaneça ativa.

---

## 🛠️ Regras de Negócio

### Fluxo de Reserva
* **Criação Automática:** Caso o cliente não exista na base de dados, ele é criado automaticamente ao realizar uma reserva.
* **Validação de Transições:**
    * Confirmação permitida apenas para status `PENDING`.
    * Check-in permitido apenas para status `CONFIRMED`.
    * Avaliação permitida apenas após o restaurante marcar como `REVIEW`.

### Política de Cancelamento e Reembolso
O estorno é calculado automaticamente com base no tempo de antecedência:

* **> 24h de antecedência:** 100% de reembolso.
* **Entre 2h e 24h de antecedência:** 50% de reembolso.
* **< 2h de antecedência:** Sem reembolso (0%).

---

## ⚡ Observações Técnicas

* **Eventos:** Ações importantes (Criação, Confirmação, Check-in) disparam eventos de domínio para permitir integrações futuras com serviços de notificação ou
