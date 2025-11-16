using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace Mapping
{
    public class RelationMapper : IMapper<TRelationDTO, TRelation>
    {
        public TRelationDTO ToBlo(TRelation dao)
        {
            return new TRelationDTO
            {
                ChildId = dao.ChildId,
                ParentId = dao.ParentId
            };
        }

        public TRelation ToDao(TRelationDTO blo)
        {
            return new TRelation
            {
                ChildId = blo.ChildId,
                ParentId = blo.ParentId
            };
        }
    }
}
