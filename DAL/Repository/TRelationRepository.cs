using Core.Interfaces;
using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class TRelationRepository : ITRelationRepository
    {
        private readonly AppDbContext _dbContext;

        public TRelationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TRelation>> ReadTRelationAsync()
        {
            return await _dbContext.Relations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TRelation>> ReadTRelationByParentIdAsync(long id)
        {
            return await _dbContext.Relations
                .Where(x => x.ParentId == id)
                .ToListAsync();
        }

        public async Task<List<TRelation>> ReadTRelationByChildIdAsync(long id)
        {
            return await _dbContext.Relations
                .AsNoTracking()
                .Where(x => x.ChildId == id)
                .ToListAsync();
        }

        public async Task CreateTRelationAsync(long parentId, long childId)
        {
            if (await _dbContext.Relations.AnyAsync(x => x.ParentId == parentId && x.ChildId == childId))
            {
                throw new Exception("Такая связь уже существует");
            }

            var newRelation = new TRelation()
            {
                ParentId = parentId,
                ChildId = childId
            };

            await _dbContext.Relations.AddAsync(newRelation);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTRelationAsync(long parentId, long childId)
        {
            var relationForDelete = await _dbContext.Relations
                .FirstOrDefaultAsync(x => x.ParentId == parentId && x.ChildId == childId);

            if (relationForDelete == null)
            {
                throw new Exception("Relation not found");
            }

            _dbContext.Relations.Remove(relationForDelete);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
