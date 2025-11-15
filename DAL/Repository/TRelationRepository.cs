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
        public List<TRelation> ReadTRelation()
        {
            return _context.Relations
                .AsNoTracking()
                .ToList();
        }

        public List<TRelation> ReadTRelationByParentId(long id)
        {
            return _context.Relations.Where(x => x.ParentId == id).ToList();
        }

        public List<TRelation> ReadTRelationByChildId(long id)
        {
            return _context.Relations
                .AsNoTracking()
                .Where(x => x.ChildId == id)
                .ToList();
        }

        public void CreateTRelation(long parentId, long childId)
        {
            if (_context == null) return;

            if (_context.Relations.Any(x => x.ParentId == parentId && x.ChildId == childId))
            {
                throw new Exception("Такая связь уже существует");
            }

            var newRelation = new TRelation()
            {
                ParentId = parentId,
                ChildId = childId
            };

            _context.Relations.Add(newRelation);
        }

        public void DeleteTRelation(long parentId, long childId)
        {
            if (_context == null) return;

            var relationForDelete = _context.Relations
                .FirstOrDefault(x => x.ParentId == parentId && x.ChildId == childId);

            if (relationForDelete == null)
            {
                MessageBox.Show("Такой связи не существует");
                return;
            }

            _context.Relations.Remove(relationForDelete);
        }
    }
}
