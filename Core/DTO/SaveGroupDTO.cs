namespace Core.DTO;

public class SaveGroupDTO
{
    public long GroupId { get; set; } 
    public long ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
}