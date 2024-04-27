using HttpRequestLoggingHandler.EFCore;

namespace HttpRequestLoggingHandler.Clients.Common;

public class HttpClientLogHandler<T>
    : DelegatingHandler where T : HttpClientLog, new()
{
    private readonly AppDbContext _dbContext;

    public HttpClientLogHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            var apiLog = new T()
            {
                Id = 0,
                Method = request.Method.Method,
                RequestUri = request.RequestUri?.ToString(),
                RequestHeaders = request.Headers.ToString(),
                RequestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : null,
                StatusCode = (int)response.StatusCode,
                ResponseHeaders = response.Headers.ToString(),
                ResponseBody = response.Content != null ? await response.Content.ReadAsStringAsync() : null,
                RequestAt = DateTime.UtcNow,
                ResponseAt = DateTime.UtcNow
            };

            _dbContext.Set<T>().Add(apiLog);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
