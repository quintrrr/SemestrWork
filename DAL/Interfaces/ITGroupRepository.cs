using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace Core.Interfaces
{
    public interface ITGroupRepository
    {
        List<TGroup> ReadTGroup();
        TGroup? ReadTGroupById(long id);
        long GetNextGroupId();
        void CreateTGroup(string name);
        void DeleteTGroup(long id);
        void UpdateTGroup(long id, string name);
    }
}
