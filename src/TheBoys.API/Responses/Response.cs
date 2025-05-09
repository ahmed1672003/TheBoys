using System.Net;
using System.Text.Json.Serialization;

namespace TheBoys.API.Responses;

public record Response
{
    public Response() => StatusCode = (int)HttpStatusCode.OK;

    [JsonPropertyOrder(1)]
    public bool Success { get; set; }

    [JsonPropertyOrder(2)]
    public int StatusCode { get; set; }

    [JsonPropertyOrder(3)]
    public string Message { get; set; }
}
