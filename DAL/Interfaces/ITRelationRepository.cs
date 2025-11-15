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
        List<TRelation> ReadTRelation();
        List<TRelation> ReadTRelationByParentId(long id);
        List<TRelation> ReadTRelationByChildId(long id);
        void CreateTRelation(long parentId, long childId);
        void DeleteTRelation(long parentId, long childId);
        
    }
}
