using Core.Interfaces;
using DAL.DAO;

namespace BLL.Services;

public class TreeService : ITreeService
{
    private readonly ITGroupRepository _groupRepository;
    private readonly ITPropertyRepository _propertyRepository;
    private readonly ITRelationRepository _relationRepository;

    public TreeService(
        ITGroupRepository groupRepository,
        ITPropertyRepository propertyRepository,
        ITRelationRepository relationRepository)
    {
        _groupRepository = groupRepository;
        _propertyRepository = propertyRepository;
        _relationRepository = relationRepository;
    }
    
    public List<TGroup> GetChildGroups(long parentGroupId)
    {
        var relationsGroups = _relationRepository.ReadTRelationByParentId(parentGroupId);
        
        var childGroups = new List<TGroup>();
        foreach (var relationsGroup in relationsGroups)
        {
            var childGroup = _groupRepository.ReadTGroupById(relationsGroup.ChildId);
            if (childGroup is not null) childGroups.Add(childGroup);
        }
        
        return childGroups;
    }
    
    public List<TProperty> GetGroupProperties(long groupId)
    {
        return _propertyRepository.ReadTPropertyByGroupId(groupId);
    }

    public TGroup? GetTGroup(long groupId)
    {
        return _groupRepository.ReadTGroupById(groupId);
    }

    public long GetNextGroupId()
    {
        return _groupRepository.GetNextGroupId();
    }

    public TProperty? GetProperty(long propertyId)
    {
        return _propertyRepository.ReadTPropertyById(propertyId);
    }

    public List<TRelation> GetParentRelations(long parentId)
    {
        return _relationRepository.ReadTRelationByParentId(parentId);
    }

    public List<TRelation> GetChildRelations(long childId)
    {
        return _relationRepository.ReadTRelationByChildId(childId);
    }

    public void DeleteRelation(long parentId, long childId)
    {
        _relationRepository.DeleteTRelation(parentId, childId);
    }

    public void DeleteProperty(long propertyId)
    {
        _propertyRepository.DeleteTProperty(propertyId);
    }

    public void DeleteGroup(long groupId)
    {
        _groupRepository.DeleteTGroup(groupId);
    }
}