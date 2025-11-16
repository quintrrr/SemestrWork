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
        Task<List<TGroup>> ReadTGroupAsync();
        Task<TGroup?> ReadTGroupByIdAsync(long id);
        Task<long> GetNextGroupIdAsync();
        Task CreateTGroupAsync(string name);
        Task DeleteTGroupAsync(long id);
        Task UpdateTGroupAsync(long id, string name);
    }
}
