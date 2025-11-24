
namespace RebuildProject.Models;

public partial class ApiLog
{
    public Guid LogId { get; set; }

    public Guid RequestId { get; set; }

    public string?  RequestUrl { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestHeaders { get; set; }

    public string? QueryString { get; set; }

    public string? ResponseStatus { get; set; }

    public string? ResponseHeaders { get; set; }

    public string? ContentType { get; set; }

    public string? ErrorMessage { get; set; }

    public string? StackTrace { get; set; }

    public DateTime? RequestTime { get; set; }

    public DateTime? ResponseTime { get; set; }

    public static implicit operator ApiLog(Task<ApiLog> v)
    {
        throw new NotImplementedException();
    }
}
