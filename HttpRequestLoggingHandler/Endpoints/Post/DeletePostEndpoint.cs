using HttpRequestLoggingHandler.Services.Interface;

namespace HttpRequestLoggingHandler.Endpoints.Post;


public class DeletePostEndpoint : Endpoint<EmptyRequest, EmptyResponse>
{
    public const string RouteId = "Id";

    public IPostService PostService { get; set; } = null!;

    public override void Configure()
    {
        Delete($"/api/posts/{{{RouteId}}}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest empty, CancellationToken ct)
    {
        var id = Route<string>($"{RouteId}") ?? throw new ArgumentNullException(nameof(RouteId));

        await PostService.DeletePost(id);

        await SendAsync(new EmptyResponse());
    }
}
