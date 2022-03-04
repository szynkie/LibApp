using System.Collections.Generic;
using LibApp.Models;
using System.Threading.Tasks;

namespace LibApp.Interfaces
{
    //Methods
    public interface IMembershipTypeActions
    {
        //Methods
        IEnumerable<MembershipType> Get();
        MembershipType GetById(int id);
        void Delete(MembershipType MsT);
        void Add(MembershipType MsT);
        void Update(MembershipType MsT);
        void Save();

        //AsyncedMethods
        Task<IEnumerable<MembershipType>> GetAsync();
        Task<MembershipType> GetByIdAsync(int id);
        Task DeleteAsync(MembershipType MsT);
        Task AddAsync(MembershipType MsT);
        Task UpdateAsync(MembershipType MsT);
        Task SaveAsync();
    }
}