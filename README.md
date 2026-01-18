🍽️ Restaurant System

Sistema de gerenciamento de reservas de restaurante desenvolvido em .NET 10, seguindo Clean Architecture + DDD + CQRS, com Entity Framework Core e PostgreSQL.

🚀 Tecnologias

.NET 10

Clean Architecture

Domain-Driven Design (DDD)

CQRS

Entity Framework Core

PostgreSQL

📋 Status das Reservas

As reservas podem ter os seguintes status:

Status	Descrição
PENDING	Reserva criada, aguardando confirmação
CONFIRMED	Reserva confirmada pelo restaurante
CHECKED_IN	Cliente fez check-in no restaurante
REVIEW	Reserva finalizada, aguardando avaliação
COMPLETED	Reserva finalizada e avaliada
CANCELLED	Reserva cancelada
NO_SHOW	Cliente não compareceu
🔐 Autenticação

O sistema possui usuário único, não há cadastro de novos usuários:

Email: admin@restaurant.com

Senha: Admin123!

Regras de Login

Ao logar, todos os refresh tokens antigos são invalidados.

Refresh Token

Implementado para renovação de sessão.

Cada login invalida os tokens anteriores para garantir segurança.

🛠️ Funcionalidades
Criar Reserva

Status inicial: PENDING

Se o cliente não existir, é criado automaticamente.

Ao criar a reserva, um evento é disparado.

Confirmar Reserva

Só é possível confirmar reservas com status PENDING.

Ao confirmar, um evento é disparado.

Check-In

Só é possível fazer check-in em reservas CONFIRMED.

Ao fazer check-in, um evento é disparado.

Completar Reserva

O restaurante marca a reserva como REVIEW após o check-in.

Avaliar / Review

Adiciona pontuação ao restaurante.

Só pode ser usado após o restaurante completar a reserva.

Muda o status de REVIEW para COMPLETED.

Cancelar Reserva

Só é possível cancelar reservas que não estejam nos status: COMPLETED, CANCELLED, NO_SHOW.

Política de reembolso:

Cancelado mais de 24h antes: 100% do valor

Cancelado entre 2h e 24h antes: 50% do valor

Cancelado menos de 2h antes: sem reembolso

⚡ Observações

Eventos são disparados em todas as ações importantes de reservas para integração futura (ex.: notificações, analytics).

Arquitetura segue Clean Architecture e CQRS para facilitar manutenção e escalabilidade.

O sistema está parcialmente implementado; priorize a finalização apenas quando possível.
