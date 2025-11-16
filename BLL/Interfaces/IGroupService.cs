using DAL.DAO;

namespace Core.Interfaces;

public interface IGroupService
{
    Task CreateGroupAsync(string name);
    Task UpdateGroupAsync(long id, string name);
    Task DeleteGroupAsync(long groupId);
    Task<List<TGroup>> GetChildGroupsAsync(long parentGroupId);
    Task<TGroup?> GetGroupAsync(long groupId);
    Task<long> GetNextGroupIdAsync();
    Task<bool> IsGroupExistsAsync(long groupId);
}