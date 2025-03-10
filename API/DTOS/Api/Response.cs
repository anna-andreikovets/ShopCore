using System.Text.Json.Serialization;

namespace ShopCore.API.DTOS.Api;

public class Response<T>
{
    /// <summary>
    /// Статус (успешно/нет)
    /// </summary>
    [JsonPropertyName("status")]
    public bool Success { get; set; } = true;

    /// <summary>
    /// Описание результата
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; } = "OK";
    
    [JsonPropertyName("result")]
    public T? Result { get; set; }
}