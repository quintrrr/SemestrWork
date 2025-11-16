using Core.Interfaces;
using DAL.DAO;
using DAL.Repository;

namespace BLL.Services;

public class GroupService : IGroupService
{
    private readonly TGroupRepository _groupRepository;
    private readonly TRelationRepository _relationRepository;

    public GroupService(
        TGroupRepository groupRepository,
        TRelationRepository relationRepository)
    {
        _groupRepository = groupRepository;
        _relationRepository = relationRepository;
    }
    
    public async Task CreateGroupAsync(string name)
    {
        await _groupRepository.CreateTGroupAsync(name);
    }

    public async Task UpdateGroupAsync(long id, string name)
    {
        await _groupRepository.UpdateTGroupAsync(id, name);
    }

    public async Task DeleteGroupAsync(long groupId)
    {
        await _groupRepository.DeleteTGroupAsync(groupId);
    }

    public async Task<List<TGroup>> GetChildGroupsAsync(long parentGroupId)
    {
        var relationsGroups = await _relationRepository.ReadTRelationByParentIdAsync(parentGroupId);
        
        var childGroups = new List<TGroup>();
        foreach (var relationsGroup in relationsGroups)
        {
            var childGroup = await _groupRepository.ReadTGroupByIdAsync(relationsGroup.ChildId);
            if (childGroup is not null) childGroups.Add(childGroup);
        }
        
        return childGroups;
    }

    public async Task<TGroup?> GetGroupAsync(long groupId)
    {
        return await _groupRepository.ReadTGroupByIdAsync(groupId);
    }

    public async Task<long> GetNextGroupIdAsync()
    {
        return await _groupRepository.GetNextGroupIdAsync();
    }

    public async Task<bool> IsGroupExistsAsync(long groupId)
    {
        return await _groupRepository.ReadTGroupByIdAsync(groupId) != null;
    }
}