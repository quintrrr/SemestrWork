using Core.DTO;

namespace Core.Interfaces;

public interface IPropertyService
{
    Task<List<TPropertyDTO>> GetGroupPropertiesAsync(long groupId);
    Task<TPropertyDTO?> GetPropertyAsync(long propertyId);
    Task DeletePropertyAsync(long propertyId);
    Task CreatePropertyAsync(string name, string value, long groupId);
    Task UpdatePropertyAsync(long propertyId, string name, string value);
}