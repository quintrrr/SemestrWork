using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace Mapping
{
    public class PropertyMapper : IMapper<TPropertyDTO, TProperty>
    {
        public TPropertyDTO ToBlo(TProperty dao)
        {
            return new TPropertyDTO
            {
                Id = dao.Id,
                Name = dao.Name,
                GroupId = dao.GroupId,
                Value = dao.Value
            };
        }

        public TProperty ToDao(TPropertyDTO blo)
        {
            return new TProperty
            {
                Id = blo.Id,
                Name = blo.Name,
                GroupId = blo.GroupId,
                Value = blo.Value
            };
        }
    }
}
