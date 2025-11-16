using Core.Interfaces;
using DAL.DAO;
using DAL.Repository;

namespace BLL.Services;

public class PropertyService : IPropertyService
{
    private readonly TPropertyRepository _propertyRepository;

    public PropertyService(
        TPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<List<TProperty>> GetGroupPropertiesAsync(long groupId)
    {
        return await _propertyRepository.ReadTPropertyByGroupIdAsync(groupId);
    }

    public async Task<TProperty?> GetPropertyAsync(long propertyId)
    {
        return await _propertyRepository.ReadTPropertyByIdAsync(propertyId);
    }

    public async Task DeletePropertyAsync(long propertyId)
    {
        await _propertyRepository.DeleteTPropertyAsync(propertyId);
    }

    public async Task CreatePropertyAsync(string name, string value, long groupId)
    {
        await _propertyRepository.CreateTPropertyAsync(name, value, groupId);
    }

    public async Task UpdatePropertyAsync(long propertyId, string name, string value)
    {
        await _propertyRepository.UpdateTPropertyAsync(propertyId, name, value);
    }
}