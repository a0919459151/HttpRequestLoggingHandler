using HttpRequestLoggingHandler.Clients.Common;
using HttpRequestLoggingHandler.Clients.JsonServerClient.Dtos;

namespace HttpRequestLoggingHandler.Clients.JsonServerClient;

public class JsonServerClient : HttpClientBase, IJsonServerClient
{
    public static readonly string HttpClientName = "JsonServerClient";
    public static readonly string BaseUrl_Dev = "http://localhost:3000";
    public static readonly string BaseUrl_Prod = "http://httprequestlogging";

    public JsonServerClient(
        IHttpClientFactory httpClientFactory) 
        : base(HttpClientName, httpClientFactory)
    {
    }

    public async Task<IEnumerable<GetPostsResponseDto>> GetPostsAsync()
    {
        var url = "posts";

        return await GetAsync<IEnumerable<GetPostsResponseDto>>(url);
    }

    public async Task<GetPostResponseDto> GetPostAsync(string id)
    {
        var url = $"posts/{id}";

        return await GetAsync<GetPostResponseDto>(url);
    }

    public async Task<CreatePostResponseDto> CreatePostAsync(CreatePostRequestDto post)
    {
        var url = "posts";

        return await PostAsync<CreatePostResponseDto>(url, post);
    }

    public async Task DeletePostAsync(string id)
    {
        var url = $"posts/{id}";

        await DeleteAsync(url);
    }
}
