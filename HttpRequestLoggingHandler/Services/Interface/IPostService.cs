using HttpRequestLoggingHandler.Endpoints.Post;

namespace HttpRequestLoggingHandler.Services.Interface;

public interface IPostService
{
    Task<GetPostsResponseDto> GetPosts();
    Task<GetPostResponseDto> GetPost(string id);
    Task<CreatePostResponseDto> CreatePost(CreatePostRequestDto request);
    Task DeletePost(string id);
}
