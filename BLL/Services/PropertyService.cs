using BLL.Enums;
using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace BLL.Services;

public class PropertyService : IPropertyService
{
    private readonly ITPropertyRepository _propertyRepository;
    private readonly IMapper<TPropertyDTO, TProperty> _mapper;

    public PropertyService(
        ITPropertyRepository propertyRepository,
        IMapper<TPropertyDTO, TProperty> mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<List<TPropertyDTO>> GetGroupPropertiesAsync(long groupId)
    {
        var propertiesDAO = await _propertyRepository.ReadTPropertyByGroupIdAsync(groupId);
        return propertiesDAO.Select(_mapper.ToBlo).ToList();
    }

    public async Task<TPropertyDTO?> GetPropertyAsync(long propertyId)
    {
        var property = await _propertyRepository.ReadTPropertyByIdAsync(propertyId);

        if (property == null) throw new ArgumentNullException(nameof(property));

        return _mapper.ToBlo(property);
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

    public async Task<PropertySaveResult> SavePropertyAsync(SavePropertyDTO savePropertyDto)
    {
        if (savePropertyDto.PropertyId is null)
        {
            await CreatePropertyAsync(savePropertyDto.Name, savePropertyDto.Value, savePropertyDto.GroupId);

            return PropertySaveResult.Created;
        }
        
        await UpdatePropertyAsync((long)savePropertyDto.PropertyId, savePropertyDto.Name, savePropertyDto.Value);
        
        return PropertySaveResult.Updated;
    }
}