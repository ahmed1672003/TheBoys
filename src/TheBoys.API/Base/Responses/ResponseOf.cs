using System.Text.Json.Serialization;

namespace TheBoys.API.Base.Responses;

public record ResponseOf<TResponse> : Response
{
    [JsonPropertyOrder(11)]
    public TResponse Result { get; set; }
}
