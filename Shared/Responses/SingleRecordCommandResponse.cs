using Shared.DTO;

namespace Shared.Responses;

public class SingleRecordCommandResponse
{
    public CustomerDto? Customer { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
}

