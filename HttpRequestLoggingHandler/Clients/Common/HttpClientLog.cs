namespace HttpRequestLoggingHandler.Clients.Common;

public abstract class HttpClientLog
{
    // Id
    public int Id { get; set; }

    // Http method
    public string? Method { get; set; }

    // Request uri
    public string? RequestUri { get; set; }

    // Request headers
    public string? RequestHeaders { get; set; }

    // Request body
    public string? RequestBody { get; set; }

    // Response status code
    public int? StatusCode { get; set; }

    // Response headers
    public string? ResponseHeaders { get; set; }

    // Response body
    public string? ResponseBody { get; set; }

    // Request time
    public DateTime? RequestAt { get; set; }

    // Response time
    public DateTime? ResponseAt { get; set; }
}
