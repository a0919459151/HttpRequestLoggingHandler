namespace HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

public class GetPostResponseDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Views { get; set; }
}

