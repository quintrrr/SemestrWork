using Core.Interfaces;
using DAL.DAO;
using DAL.Repository;

namespace BLL.Services;

public class RelationService : IRelationService
{
    private readonly TRelationRepository _relationRepository;

    public RelationService(
        TRelationRepository relationRepository)
    {
        _relationRepository = relationRepository;
    }
    
    public async Task<List<TRelation>> GetParentRelationsAsync(long parentId)
    {
        return await _relationRepository.ReadTRelationByParentIdAsync(parentId);
    }

    public async Task<List<TRelation>> GetChildRelationsAsync(long childId)
    {
        return await _relationRepository.ReadTRelationByChildIdAsync(childId);
    }

    public async Task DeleteRelationAsync(long parentId, long childId)
    {
        await _relationRepository.DeleteTRelationAsync(parentId, childId);
    }

    public async Task CreateRelationAsync(long parentId, long childId)
    {
        await _relationRepository.CreateTRelationAsync(parentId, childId);
    }
}