using System.Text.Json.Serialization;

namespace Core.DTO
{
    public class TGroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public TGroupDTO()
        {
            
        }
        
        [JsonConstructor]
        public TGroupDTO(string name)
        {
            Name = name;
        }
    }
}
