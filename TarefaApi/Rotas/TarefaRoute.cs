using Microsoft.EntityFrameworkCore;
using TarefaApi.Dados;
using TarefaApi.Models;
using TarefaApi.Requisicoes;

namespace TarefaApi.Rotas
{
    public static class TarefaRoute
    {
        public static async void TarefaRoutes(this WebApplication app)
        {
            var route = app.MapGroup("tarefa");
            route.MapGet("/listar", async (TarefaContexto contexto) =>
            {
                var tarefas = await contexto.Tarefa.ToListAsync();
                return Results.Ok(tarefas);
            });


            route.MapGet("/buscar/{codigo}", async (Guid codigo, TarefaContexto contexto) =>
            {
                var tarefa = await contexto.Tarefa.FindAsync(codigo);

                if (tarefa == null)
                {
                    return Results.NotFound("Não foi encontrada tarefa com o nº " + codigo);
                }

                return Results.Ok(tarefa);
            });


            route.MapPost("/criar", async (TarefaRequest req, TarefaContexto contexto) =>
            {
                if (string.IsNullOrWhiteSpace(req.Descricao))
                {
                    return Results.BadRequest("Preencher a descrição da tarefa.");
                }
                if (!Enum.IsDefined(typeof(Status), req.Status))
                {
                    return Results.BadRequest("Status inválido. Use apenas 'Pendente' ou 'Concluido'.");
                }

                var tarefaExistente = await contexto.Tarefa.AnyAsync(t => t.Codigo == req.Codigo);
                if (tarefaExistente)
                {
                    return Results.BadRequest("Não será possível adicionar a tarefa, já existe uma tarefa com o mesmo código.");
                }

                var tarefa = new Tarefa(req.Codigo, req.Descricao, Status.P);

                await contexto.Tarefa.AddAsync(tarefa);
                await contexto.SaveChangesAsync();

                return Results.Created($"/tarefa/{tarefa.Codigo}", tarefa);
            });

            route.MapPut("/atualizar/{codigo}", async (Guid codigo, TarefaRequest req, TarefaContexto contexto) =>
            {
                var tarefa = await contexto.Tarefa.FindAsync(codigo);

                if (tarefa == null)
                {
                    return Results.NotFound("Tarefa com o nº " + codigo + " não foi encontrada");
                }

                if (codigo != req.Codigo)
                {
                    return Results.BadRequest("Não é possível alterar o código da tarefa depois que já foi criada.");
                }

                if (string.IsNullOrWhiteSpace(req.Descricao))
                {
                    return Results.BadRequest("Preencher a descrição.");
                }

                if (!Enum.IsDefined(typeof(Status), req.Status))
                {
                    return Results.BadRequest("Status inválido. Use apenas 'P' - Pendente ou 'C' - Concluído.");
                }

                if (req.Status != Status.C)
                {
                    return Results.BadRequest("Uma vez criadas, as tarefas só poderão ser alteradas para o status: 'C' - Concluído.");
                }

                tarefa.Descricao = req.Descricao;
                tarefa.Status = req.Status;

                await contexto.SaveChangesAsync();

                return Results.Ok(tarefa);
            });

            route.MapDelete("/excluir/{codigo}", async (Guid codigo, TarefaContexto contexto) =>
            {
                var tarefa = await contexto.Tarefa.FindAsync(codigo);

                if (tarefa == null)
                {
                    return Results.NotFound("Não foi possível encontrar a tarefa nº " + codigo);
                }

                contexto.Tarefa.Remove(tarefa);
                await contexto.SaveChangesAsync();

                return Results.Ok("A tarefa: " + codigo + " foi excluída com sucesso.");
            });

            route.MapDelete("/deletar-todas", async (TarefaContexto contexto) =>
            {
                var tarefas = await contexto.Tarefa.ToListAsync();
                if (tarefas.Count == 0)
                {
                    return Results.NotFound("Nenhuma tarefa encontrada para excluir.");
                }

                contexto.Tarefa.RemoveRange(tarefas);
                await contexto.SaveChangesAsync();

                return Results.Ok("Todas as tarefas foram excluídas com sucesso.");
            });

        }
    }
}