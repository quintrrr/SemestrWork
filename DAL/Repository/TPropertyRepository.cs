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
        private readonly AppDbContext _dbContext;

        public TPropertyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TProperty>> ReadTPropertyAsync()
        {
            return await _dbContext.Properties
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TProperty?> ReadTPropertyByIdAsync(long id)
        {
            return await _dbContext.Properties
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<TProperty>> ReadTPropertyByGroupIdAsync(long groupId)
        {
            return await _dbContext.Properties
                .AsNoTracking()
                .Where(p => p.GroupId == groupId)
                .ToListAsync();
        }

        public async Task CreateTPropertyAsync(string name, string value, long groupId)
        {
            var maxId = await _dbContext.Properties.AnyAsync() ?
                await _dbContext.Properties.MaxAsync(x => x.Id) + 1 : 1;

            var newProperty = new TProperty()
            {
                Id = maxId,
                Name = name,
                Value = value,
                GroupId = groupId
            };

            await _dbContext.Properties.AddAsync(newProperty);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTPropertyAsync(long id)
        {
            var propertyForDelete = await _dbContext.Properties.SingleOrDefaultAsync(x => x.Id == id);

            if (propertyForDelete == null)
            {
                throw new Exception("Property not found");
            }

            _dbContext.Properties.Remove(propertyForDelete);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTPropertyAsync(long id, string name, string value)
        {
            var propertyForUpdate = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (propertyForUpdate == null)
            {
                return;
            }

            propertyForUpdate.Name = name;
            propertyForUpdate.Value = value;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
