using BLL.Enums;
using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace BLL.Services;

public class GroupService : IGroupService
{
    private readonly ITGroupRepository _groupRepository;
    private readonly ITRelationRepository _relationRepository;
    private readonly ITPropertyRepository _propertyRepository;
    private readonly IMapper<TGroupDTO, TGroup> _mapper;

    public GroupService(
        ITGroupRepository groupRepository,
        ITRelationRepository relationRepository, 
        ITPropertyRepository propertyRepository,
        IMapper<TGroupDTO, TGroup> mapper)
    {
        _groupRepository = groupRepository;
        _relationRepository = relationRepository;
        _propertyRepository = propertyRepository;
        _mapper = mapper;
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
        var parentRelations = await _relationRepository.ReadTRelationByParentIdAsync(groupId);
        var childRelations = await _relationRepository.ReadTRelationByChildIdAsync(groupId);
        var properties = await _propertyRepository.ReadTPropertyByGroupIdAsync(groupId);

        foreach (var parentRelation in parentRelations)
        {
            await _relationRepository.DeleteTRelationAsync(groupId, parentRelation.ChildId);
        }

        foreach (var childRelation in childRelations)
        {
            await _relationRepository.DeleteTRelationAsync(childRelation.ParentId, groupId);
        }

        foreach (var property in properties)
        {
            await _propertyRepository.DeleteTPropertyAsync(property.Id);
        }

        await _groupRepository.DeleteTGroupAsync(groupId);
    }

    public async Task<List<TGroupDTO>> GetChildGroupsAsync(long parentGroupId)
    {
        var relationsGroups = await _relationRepository.ReadTRelationByParentIdAsync(parentGroupId);
        
        var childGroups = new List<TGroupDTO>();
        foreach (var relationsGroup in relationsGroups)
        {
            var childGroup = await _groupRepository.ReadTGroupByIdAsync(relationsGroup.ChildId);
            if (childGroup is not null) childGroups.Add(_mapper.ToBlo(childGroup));
        }
        
        return childGroups;
    }

    public async Task<TGroupDTO?> GetGroupAsync(long groupId)
    {
        var group = await _groupRepository.ReadTGroupByIdAsync(groupId);

        if (group is null) throw new ArgumentNullException(nameof(group));

        return _mapper.ToBlo(group);
    }

    public async Task<long> GetNextGroupIdAsync()
    {
        return await _groupRepository.GetNextGroupIdAsync();
    }

    public async Task<bool> IsGroupExistsAsync(long groupId)
    {
        return await _groupRepository.ReadTGroupByIdAsync(groupId) != null;
    }

    public async Task<GroupSaveResult> SaveGroupAsync(long groupId, long parentId, string name)
    {
        if (await IsGroupExistsAsync(groupId))
        {
            await UpdateGroupAsync(groupId, name);

            return GroupSaveResult.Updated;
        }
        
        await CreateGroupAsync(name);
        
        await _relationRepository.CreateTRelationAsync(parentId, groupId);
        
        return GroupSaveResult.Created;
    }
}