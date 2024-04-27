using HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

namespace HttpRequestLoggingHandler.Clients.JsonServerClient;

public interface IJsonServerClient
{
    Task<IEnumerable<GetPostsResponseDto>> GetPostsAsync();
    Task<GetPostResponseDto> GetPostAsync(string id);
    Task<CreatePostResponseDto> CreatePostAsync(CreatePostRequestDto post);
    Task DeletePostAsync(string id);
}
