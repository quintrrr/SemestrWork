using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace Core.Interfaces
{
    public interface ITPropertyRepository
    {
        Task<List<TProperty>> ReadTPropertyAsync();
        Task<TProperty?> ReadTPropertyByIdAsync(long id);
        Task<List<TProperty>> ReadTPropertyByGroupIdAsync(long groupId);
        Task CreateTPropertyAsync(string name, string value, long groupId);
        Task DeleteTPropertyAsync(long id);
        Task UpdateTPropertyAsync(long id, string name, string value);
    }
}
