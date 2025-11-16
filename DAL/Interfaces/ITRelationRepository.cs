using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace Core.Interfaces
{
    public interface ITRelationRepository
    {
        Task<List<TRelation>> ReadTRelationAsync();
        Task<List<TRelation>> ReadTRelationByParentIdAsync(long id);
        Task<List<TRelation>> ReadTRelationByChildIdAsync(long id);
        Task CreateTRelationAsync(long parentId, long childId);
        Task DeleteTRelationAsync(long parentId, long childId);
        
    }
}
