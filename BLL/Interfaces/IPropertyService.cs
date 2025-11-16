using DAL.DAO;

namespace Core.Interfaces;

public interface IPropertyService
{
    Task<List<TProperty>> GetGroupPropertiesAsync(long groupId);
    Task<TProperty?> GetPropertyAsync(long propertyId);
    Task DeletePropertyAsync(long propertyId);
    Task CreatePropertyAsync(string name, string value, long groupId);
    Task UpdatePropertyAsync(long propertyId, string name, string value);
}