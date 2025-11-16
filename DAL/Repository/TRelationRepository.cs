using Core.Interfaces;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class TRelationRepository : ITRelationRepository
    {
        private AppContext _context;

        public TRelationRepository(AppContext context)
        {
            _context = context;
        }
        public async Task<List<TRelation>> ReadTRelationAsync()
        {
            return await _context.Relations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TRelation>> ReadTRelationByParentIdAsync(long id)
        {
            return await _context.Relations
                .Where(x => x.ParentId == id)
                .ToListAsync();
        }

        public async Task<List<TRelation>> ReadTRelationByChildIdAsync(long id)
        {
            return await _context.Relations
                .AsNoTracking()
                .Where(x => x.ChildId == id)
                .ToListAsync();
        }

        public async Task CreateTRelationAsync(long parentId, long childId)
        {
            if (await _context.Relations.AnyAsync(x => x.ParentId == parentId && x.ChildId == childId))
            {
                throw new Exception("Такая связь уже существует");
            }

            var newRelation = new TRelation()
            {
                ParentId = parentId,
                ChildId = childId
            };

            await _context.Relations.AddAsync(newRelation);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTRelationAsync(long parentId, long childId)
        {
            var relationForDelete = await _context.Relations
                .FirstOrDefaultAsync(x => x.ParentId == parentId && x.ChildId == childId);

            if (relationForDelete == null)
            {
                return;
            }

            _context.Relations.Remove(relationForDelete);
            
            await _context.SaveChangesAsync();
        }
    }
}
