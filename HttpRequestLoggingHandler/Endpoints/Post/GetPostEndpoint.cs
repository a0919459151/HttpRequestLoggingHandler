using HttpRequestLoggingHandler.Services.Interface;

namespace HttpRequestLoggingHandler.Endpoints.Post;

public class GetPostResponseDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Views { get; set; }
}

public class GetPostEndpoint : Endpoint<EmptyRequest, GetPostResponseDto>
{
    public const string RouteId = "Id";

    public IPostService PostService { get; set; } = null!;

    public override void Configure()
    {
        Get($"/api/posts/{{{RouteId}}}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest empty, CancellationToken ct)
    {
        var id = Route<string>($"{RouteId}") ?? throw new ArgumentNullException(nameof(RouteId));

        var response = await PostService.GetPost(id);

        await SendAsync(response);
    }
}
