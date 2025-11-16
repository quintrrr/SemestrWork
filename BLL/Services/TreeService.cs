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
}