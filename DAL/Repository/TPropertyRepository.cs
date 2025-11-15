using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class TPropertyRepository : ITPropertyRepository
    {
        private AppContext _context;

        public TPropertyRepository(AppContext context)
        {
            _context = context;
        }
        public List<TProperty> ReadTProperty()
        {
            return _context.Properties.AsNoTracking().ToList();
        }

        public TProperty? ReadTPropertyById(long id)
        {
            return _context.Properties.AsNoTracking().SingleOrDefault(g => g.Id == id);
        }

        public List<TProperty> ReadTPropertyByGroupId(long groupId)
        {
            return _context.Properties.AsNoTracking().Where(p => p.GroupId == groupId).ToList();
        }

        public void CreateTProperty(string name, string value, long groupId)
        {
            if (_context == null) return;

            var maxId = _context.Properties.Any() ? _context.Properties.Max(x => x.Id) + 1 : 1;

            var newProperty = new TProperty()
            {
                Id = maxId,
                Name = name,
                Value = value,
                GroupId = groupId
            };

            _context.Properties.Add(newProperty);
        }

        public void DeleteTProperty(long id)
        {
            if (_context == null) return;

            var propertyForDelete = _context.Properties.SingleOrDefault(x => x.Id == id);

            if (propertyForDelete == null)
            {
                MessageBox.Show($@"Свойство с id = {id} не найдено!");
                return;
            }

            _context.Properties.Remove(propertyForDelete);
        }

        public void UpdateTProperty(long id, string name, string value)
        {
            if (_context == null) return;

            var propertyForUpdate = _context.Properties.FirstOrDefault(x => x.Id == id);

            if (propertyForUpdate == null)
            {
                MessageBox.Show(@"Группа, предназначенная для обновления, не найдена");
                return;
            }

            propertyForUpdate.Name = name;
            propertyForUpdate.Value = value;
        }
    }
}
