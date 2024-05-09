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
            var a = await request.Content.ReadAsStringAsync();

            var apiLog = new T
            {
                Method = request.Method.ToString(),
                RequestUri = request.RequestUri?.ToString(),
                RequestHeaders = request.Content?.Headers.ToString(),
                RequestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : null,
                RequestAt = DateTime.UtcNow
            };

            var response = await base.SendAsync(request, cancellationToken);

            apiLog.StatusCode = (int)response.StatusCode;
            apiLog.ResponseHeaders = response.Content.Headers.ToString();
            apiLog.ResponseBody = response.Content != null ? await response.Content.ReadAsStringAsync() : null;
            apiLog.ResponseAt = DateTime.Now;

            await _dbContext.Set<T>().AddAsync(apiLog);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
