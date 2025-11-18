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
    public class TGroupRepository : ITGroupRepository
    {
        private readonly AppDbContext _dbContext;

        public TGroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TGroup>> ReadTGroupAsync()
        {
            return await _dbContext.Groups
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TGroup?> ReadTGroupByIdAsync(long id)
        {
            return await  _dbContext.Groups
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<long> GetNextGroupIdAsync()
        {
            return await _dbContext.Groups.AnyAsync() ? await _dbContext.Groups.MaxAsync(x => x.Id) + 1 : 1;
        }

        public async Task CreateTGroupAsync(string name)
        {
            var maxId = await GetNextGroupIdAsync();

            var newGroup = new TGroup()
            {
                Id = maxId,
                Name = name
            };

            await _dbContext.Groups.AddAsync(newGroup);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTGroupAsync(long id)
        {
            var groupForDelete = await _dbContext.Groups.SingleOrDefaultAsync(x => x.Id == id);

            if (groupForDelete == null)
            {
                throw new Exception( "Group not found");
            }

            _dbContext.Groups.Remove(groupForDelete);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTGroupAsync(long id, string name)
        {
            var groupForUpdate = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (groupForUpdate == null)
            {
                throw new Exception( "Group not found");
            }

            groupForUpdate.Name = name;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
