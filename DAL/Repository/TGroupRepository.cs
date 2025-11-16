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
        private AppContext _context;

        public TGroupRepository(AppContext context)
        {
            _context = context;
        }
        public async Task<List<TGroup>> ReadTGroupAsync()
        {
            return await _context.Groups
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TGroup?> ReadTGroupByIdAsync(long id)
        {
            return await  _context.Groups
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<long> GetNextGroupIdAsync()
        {
            return await _context.Groups.AnyAsync() ? await _context.Groups.MaxAsync(x => x.Id) + 1 : 1;
        }

        public async Task CreateTGroupAsync(string name)
        {
            var maxId = await _context.Groups.AnyAsync() ? await _context.Groups.MaxAsync(x => x.Id) + 1 : 1;

            var newGroup = new TGroup()
            {
                Id = maxId,
                Name = name
            };

            await _context.Groups.AddAsync(newGroup);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTGroupAsync(long id)
        {
            var groupForDelete = await _context.Groups.SingleOrDefaultAsync(x => x.Id == id);

            if (groupForDelete == null)
            {
                return;
            }

            _context.Groups.Remove(groupForDelete);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTGroupAsync(long id, string name)
        {
            var groupForUpdate = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (groupForUpdate == null)
            {
                return;
            }

            groupForUpdate.Name = name;
            
            await _context.SaveChangesAsync();
        }
    }
}
