namespace Core.DTO;

public class SavePropertyDTO
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public long GroupId { get; set; }
    public long? PropertyId { get; set; }
}