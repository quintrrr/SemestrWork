using System.Text.Json.Serialization;

namespace Core.DTO
{
    public class TPropertyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public long GroupId { get; set; }

        public TPropertyDTO()
        {
            
        }

        [JsonConstructor]
        public TPropertyDTO(string name, string value, long groupId)
        {
            Name = name;
            Value = value;
            GroupId = groupId;
        }
    }
}
