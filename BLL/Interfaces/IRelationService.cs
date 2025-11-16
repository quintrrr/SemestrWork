using Core.DTO;

namespace Core.Interfaces;

public interface IRelationService
{
    Task<List<TRelationDTO>> GetParentRelationsAsync(long parentId);
    Task<List<TRelationDTO>> GetChildRelationsAsync(long childId);
    Task DeleteRelationAsync(long parentId, long childId);
    Task CreateRelationAsync(long parentId, long childId);
}