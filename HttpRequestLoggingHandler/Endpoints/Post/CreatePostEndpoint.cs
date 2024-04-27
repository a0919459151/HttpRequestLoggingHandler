using HttpRequestLoggingHandler.Services.Interface;

namespace HttpRequestLoggingHandler.Endpoints.Post;

public class CreatePostRequestDto
{
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
}

public class CreatePostResponseDto
{
    public string Id { get; set; } = null!;
}

public class CreatePostEndpoint : Endpoint<CreatePostRequestDto, CreatePostResponseDto>
{
    public IPostService PostService { get; set; } = null!;

    public override void Configure()
    {
        Post("/api/posts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreatePostRequestDto request, CancellationToken ct)
    {
        CreatePostResponseDto response = await PostService.CreatePost(request);

        await SendAsync(response);
    }
}
