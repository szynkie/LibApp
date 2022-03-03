using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibApp.Respositories
{
    public class MembershipTypeRepository : IMembershipTypeActions
    {
        private readonly ApplicationDbContext _context;

        public MembershipTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<MembershipType> Get() => _context.MembershipTypes;
        public MembershipType GetById(int id) => _context.MembershipTypes.Find(id);
        public void Add(MembershipType MsT) => _context.MembershipTypes.Add(MsT);
        public void Delete(int id) => _context.MembershipTypes.Remove(GetById(id));
        public void Update(MembershipType MsT) => _context.MembershipTypes.Update(MsT);
        public void Save() => _context.SaveChanges();

        public async Task<IEnumerable<MembershipType>> GetAsync() => await _context.MembershipTypes.ToListAsync();
        public async Task<MembershipType> GetByIdAsync(int id) => await _context.MembershipTypes.SingleOrDefaultAsync(m => m.Id == id);
        public async Task DeleteAsync(int id)
        {
            _context.MembershipTypes.Remove(GetById(id));
            await SaveAsync();
        }

        public async Task AddAsync(MembershipType MsT) => await _context.MembershipTypes.AddAsync(MsT);
        public async Task UpdateAsync(MembershipType MsT)
        {
            _context.MembershipTypes.Update(MsT);
            await SaveAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}