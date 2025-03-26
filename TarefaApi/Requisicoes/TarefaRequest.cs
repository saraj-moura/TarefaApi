using System.Text.Json.Serialization;

namespace TarefaApi.Requisicoes
{
    public record TarefaRequest
    {
        public Guid Codigo { get; set; }
        public string Descricao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}