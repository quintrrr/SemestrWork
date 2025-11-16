using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace BLL.Services;

public class RelationService : IRelationService
{
    private readonly ITRelationRepository _relationRepository;
    private readonly IMapper<TRelationDTO, TRelation> _mapper;

    public RelationService(
        ITRelationRepository relationRepository,
        IMapper<TRelationDTO, TRelation> mapper)
    {
        _relationRepository = relationRepository;
        _mapper = mapper;
    }
    
    public async Task<List<TRelationDTO>> GetParentRelationsAsync(long parentId)
    {
        var relations = await _relationRepository.ReadTRelationByParentIdAsync(parentId);

        return relations.Select(_mapper.ToBlo).ToList();
    }

    public async Task<List<TRelationDTO>> GetChildRelationsAsync(long childId)
    {
        var relations = await _relationRepository.ReadTRelationByChildIdAsync(childId);

        return relations.Select(_mapper.ToBlo).ToList();
    }

    public async Task DeleteRelationAsync(long parentId, long childId)
    {
        await _relationRepository.DeleteTRelationAsync(parentId, childId);
    }

    public async Task CreateRelationAsync(long parentId, long childId)
    {
        await _relationRepository.CreateTRelationAsync(parentId, childId);
    }
}