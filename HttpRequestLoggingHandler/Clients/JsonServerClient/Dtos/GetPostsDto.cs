using System.Text.Json.Serialization;

namespace HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

public class GetPostsResponseDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Views { get; set; }
}
