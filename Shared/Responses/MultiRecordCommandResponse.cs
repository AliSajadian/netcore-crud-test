using Shared.DTO;

namespace Shared.Responses;

public class MultiRecordCommandResponse
{
    public int Id { get; set; }
    public List<CustomerDto>? Customers { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}