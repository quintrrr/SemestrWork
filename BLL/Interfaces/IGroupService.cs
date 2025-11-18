using BLL.Enums;
using Core.DTO;

namespace Core.Interfaces;

public interface IGroupService
{
    Task CreateGroupAsync(string name);
    Task UpdateGroupAsync(long id, string name);
    Task DeleteGroupAsync(long groupId);
    Task<List<TGroupDTO>> GetChildGroupsAsync(long parentGroupId);
    Task<TGroupDTO?> GetGroupAsync(long groupId);
    Task<long> GetNextGroupIdAsync();
    Task<bool> IsGroupExistsAsync(long groupId);
    Task<GroupSaveResult> SaveGroupAsync(long groupId, long parentId, string name);
}