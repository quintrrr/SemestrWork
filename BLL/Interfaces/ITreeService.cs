using DAL.DAO;

namespace Core.Interfaces;

public interface ITreeService
{
    List<TGroup> GetChildGroups(long parentGroupId);
    List<TProperty> GetGroupProperties(long groupId);
    TGroup? GetTGroup(long groupId);
    long GetNextGroupId();
    TProperty? GetProperty(long propertyId);
    List<TRelation> GetParentRelations(long parentId);
    List<TRelation> GetChildRelations(long childId);
    void DeleteRelation(long parentId, long childId);
    void DeleteProperty(long propertyId);
    void DeleteGroup(long groupId);
}