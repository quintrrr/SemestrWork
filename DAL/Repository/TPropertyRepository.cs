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
        public async Task<List<TProperty>> ReadTPropertyAsync()
        {
            return await _context.Properties
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TProperty?> ReadTPropertyByIdAsync(long id)
        {
            return await _context.Properties
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<TProperty>> ReadTPropertyByGroupIdAsync(long groupId)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.GroupId == groupId)
                .ToListAsync();
        }

        public async Task CreateTPropertyAsync(string name, string value, long groupId)
        {
            var maxId = await _context.Properties.AnyAsync() ?
                await _context.Properties.MaxAsync(x => x.Id) + 1 : 1;

            var newProperty = new TProperty()
            {
                Id = maxId,
                Name = name,
                Value = value,
                GroupId = groupId
            };

            await _context.Properties.AddAsync(newProperty);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTPropertyAsync(long id)
        {
            var propertyForDelete = await _context.Properties.SingleOrDefaultAsync(x => x.Id == id);

            if (propertyForDelete == null)
            {
                return;
            }

            _context.Properties.Remove(propertyForDelete);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTPropertyAsync(long id, string name, string value)
        {
            var propertyForUpdate = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (propertyForUpdate == null)
            {
                return;
            }

            propertyForUpdate.Name = name;
            propertyForUpdate.Value = value;
            
            await _context.SaveChangesAsync();
        }
    }
}
