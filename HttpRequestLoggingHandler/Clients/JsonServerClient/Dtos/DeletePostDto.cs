namespace HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

public class DeletePostResponseDto
{
    public string Id { get; set; } = null!;
    public DeletePostDto Post { get; set; } = null!;
}

public class DeletePostDto
{
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
}
