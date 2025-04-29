# SIGV – Sistema Integrado de Gestão de Veículos

O **SIGV** é uma solução inicial para a **gestão de veículos**.  
O sistema é acessado via **plataforma web**, com suporte a funcionalidades específicas via **aplicativo mobile**, que realiza **laudos** com sincronização automática ao banco de dados.

---

## Plataforma Web

A plataforma web é o núcleo principal do sistema, onde é possível:

- Gerenciar o cadastro, logs, fotos, ocorrências dos veículos
- Gerenciar usuários e permissões ao sistema
- Visualizar registros de laudos feitas via app

---

## Aplicativo de Laudo

O aplicativo mobile é responsável por:

- Realizar laudos dos veículos (avarias, opcionais, fotos, observações)
- Trabalhar offline e sincronizar com o servidor quando conectado
- Garantir rastreabilidade das inspeções realizadas

---

## Tecnologias Utilizadas

### Backend
- C#
- .NET Framework
- ASP.NET
- JSON

### Frontend
- HTML, CSS, Bootstrap
- JavaScript, jQuery

### Mobile
- Xamarin
- .NET MAUI (migrado)

### Banco de Dados
- MySql

### Outros
- Hospedagem Web (IIS)
- Sincronização de dados entre app e servidor
