using System.Text.Json.Serialization;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    P = 'P',
    C = 'C'
}
