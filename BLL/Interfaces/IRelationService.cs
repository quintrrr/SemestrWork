using DAL.DAO;

namespace Core.Interfaces;

public interface IRelationService
{
    Task<List<TRelation>> GetParentRelationsAsync(long parentId);
    Task<List<TRelation>> GetChildRelationsAsync(long childId);
    Task DeleteRelationAsync(long parentId, long childId);
    Task CreateRelationAsync(long parentId, long childId);
}