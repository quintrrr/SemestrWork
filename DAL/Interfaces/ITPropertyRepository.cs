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
        List<TProperty> ReadTProperty();
        TProperty? ReadTPropertyById(long id);
        List<TProperty> ReadTPropertyByGroupId(long groupId);
        void CreateTProperty(string name, string value, long groupId);
        void DeleteTProperty(long id);
        void UpdateTProperty(long id, string name, string value);
    }
}
