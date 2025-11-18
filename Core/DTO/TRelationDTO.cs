using System.Text.Json.Serialization;

namespace Core.DTO
{
    public class TRelationDTO
    {
        public long ParentId { get; set; }
        public long ChildId { get; set; }

        public TRelationDTO()
        {
            
        }

        [JsonConstructor]
        public TRelationDTO(long parentId, long childId)
        {
            ParentId = parentId;
            ChildId = childId;
        }
    }
}
