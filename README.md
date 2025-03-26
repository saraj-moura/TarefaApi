# TarefaApi

## Propósito

Desenvolver uma API Rest para gerenciar tarefas, com as funcionalidades de **Consultar**, **Incluir**, **Alterar** e **Excluir** tarefas. A API deve ter integração com o **Swagger** e a base de dados fica a critério do desenvolvedor, podendo ser até um container de classe.

## Propriedades da Tarefa

- **Código da Tarefa**
- **Descrição da Tarefa**
- **Status da Tarefa**:
  - `'P'` = Pendente
  - `'C'` = Concluído

## Regras de Negócio

### Inclusão

- Não deve haver repetição de códigos de tarefas.
- Toda nova tarefa cadastrada deve ter o status definido como `'P'` (Pendente).
- A descrição da tarefa deve ser obrigatoriamente definida.

### Alteração

- Não é permitido alterar o código da tarefa após a criação.
- A descrição da tarefa deve estar sempre definida.
- O status da tarefa só pode ser alterado para `'C'` (Concluído).

### Consulta

- **Consulta Total**: Retorna um JSON com informações de todas as tarefas cadastradas.
- **Consulta Específica**: Retorna um JSON com as informações de uma tarefa específica, identificada pelo código passado como parâmetro.
