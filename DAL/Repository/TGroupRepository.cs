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
        public List<TGroup> ReadTGroup()
        {
            return _context.Groups
                .AsNoTracking()
                .ToList();
        }

        public TGroup? ReadTGroupById(long id)
        {
            return _context.Groups
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public long GetNextGroupId()
        {
            return _context.Groups.Any() ? _context.Groups.Max(x => x.Id) + 1 : 1;
        }

        public void CreateTGroup(string name)
        {
            if (_context == null) return;

            var maxId = _context.Groups.Any() ? _context.Groups.Max(x => x.Id) + 1 : 1;

            var newGroup = new TGroup()
            {
                Id = maxId,
                Name = name
            };

            _context.Groups.Add(newGroup);
        }

        public void DeleteTGroup(long id)
        {
            if (_context == null) return;

            var groupForDelete = _context.Groups.SingleOrDefault(x => x.Id == id);

            if (groupForDelete == null)
            {
                MessageBox.Show($@"Группа с id = {id} не найдена!");
                return;
            }

            _context.Groups.Remove(groupForDelete);
        }

        public void UpdateTGroup(long id, string name)
        {
            if (_context == null) return;

            var groupForUpdate = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (groupForUpdate == null)
            {
                MessageBox.Show(@"Группа, предназначенная для обновления, не найдена");
                return;
            }

            groupForUpdate.Name = name;
        }
    }
}
