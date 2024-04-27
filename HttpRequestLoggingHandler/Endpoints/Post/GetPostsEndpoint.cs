using HttpRequestLoggingHandler.Services.Interface;

namespace HttpRequestLoggingHandler.Endpoints.Post;

public class GetPostsResponseDto
{
    public IEnumerable<GetPostsDto> Posts { get; set; } = null!;
}
public class GetPostsDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Views { get; set; }
}

public class GetPostsEndpoint : Endpoint<EmptyRequest, GetPostsResponseDto>
{
    public IPostService PostService { get; set; } = null!;

    public override void Configure()
    {
        Get("/api/posts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest empty, CancellationToken ct)
    {
        var response = await PostService.GetPosts();

        await SendAsync(response);
    }
}
