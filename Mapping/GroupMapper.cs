using Core.DTO;
using Core.Interfaces;
using DAL.DAO;

namespace Mapping
{
    public class GroupMapper : IMapper<TGroupDTO, TGroup>
    {

        public TGroup ToDao(TGroupDTO blo)
        {
            return new TGroup
            {
                Id = blo.Id,
                Name = blo.Name
            };
        }

        public TGroupDTO ToBlo(TGroup dao)
        {
            return new TGroupDTO
            {
                Id = dao.Id,
                Name = dao.Name
            };
        }
    }
}
