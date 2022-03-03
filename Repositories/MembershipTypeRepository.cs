using System.Collections.Generic;
using LibApp.Models;
using LibApp.Data;
using LibApp.Interfaces;

namespace LibApp.Respositories
{
    public class MembershipTypeRepository : IMembershipTypeActions
    {
        private readonly ApplicationDbContext _context;

        public MembershipTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<MembershipType> GetMsTs() => _context.MembershipTypes;
        public MembershipType GetMsTById(int id) => _context.MembershipTypes.Find(id);
        public void Add(MembershipType MsT) => _context.MembershipTypes.Add(MsT);
        public void Delete(int id) => _context.MembershipTypes.Remove(GetMsTById(id));
        public void Update(MembershipType MsT) => _context.MembershipTypes.Update(MsT);
        public void Save() => _context.SaveChanges();
    }
}