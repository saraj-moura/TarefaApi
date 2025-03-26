using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TarefaApi.Models
{
    public class Tarefa
    {
        [Key]
        public Guid Codigo { get; init; }
        public string Descricao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        public Tarefa(Guid codigo, string descricao, Status status)
        {
            Codigo = codigo;
            Descricao = descricao;
            Status = status;
        }
    }
}
