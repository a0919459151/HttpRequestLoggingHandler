namespace HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

public class CreatePostRequestDto
{
    public CreatePostResponseDto Post { get; set; } = null!;
}


public class CreatePostResponseDto
{
    public string Id { get; set; } = null!;
    public CreatePostDto Post { get; set; } = null!;
}

public class CreatePostDto
{
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
}