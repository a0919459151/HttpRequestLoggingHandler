using HttpRequestLoggingHandler.Endpoints.Post;
using HttpRequestLoggingHandler.Services.Interface;

namespace HttpRequestLoggingHandler.Services;

public class PostService : IPostService
{
    private readonly IJsonServerClient _jsonServerClient;

    public PostService(IJsonServerClient jsonServerClient)
    {
        _jsonServerClient = jsonServerClient;
    }

    // GetPosts
    public async Task<GetPostsResponseDto> GetPosts()
    {
        var apiResponse = await _jsonServerClient.GetPostsAsync();

        GetPostsResponseDto response = new()
        {
            Posts = apiResponse.Adapt<IEnumerable<GetPostsDto>>(),
        };

        return response;
    }

    // GetPost
    public async Task<GetPostResponseDto> GetPost(string id)
    {
        var apiResponse = await _jsonServerClient.GetPostAsync(id);

        GetPostResponseDto response = apiResponse.Adapt<GetPostResponseDto>();

        return response;
    }

    // CreatePost
    public async Task<CreatePostResponseDto> CreatePost(CreatePostRequestDto request)
    {
        var apiRequest = request.Adapt<HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos.CreatePostRequestDto>();

        var apiResponse = await _jsonServerClient.CreatePostAsync(apiRequest);

        CreatePostResponseDto response = new()
        {
            Id = apiResponse.Id
        };

        return response;
    }

    // DeletePost
    public async Task DeletePost(string id)
    {
        await _jsonServerClient.DeletePostAsync(id);
    }
}
