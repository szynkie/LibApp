using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces
{
    public interface IMembershipTypeActions
    {
        IEnumerable<MembershipType> GetMsTs();
        MembershipType GetMsTById(int id);
        void Delete(int id);
        void Add(MembershipType MsT);
        void Update(MembershipType MsT);
        void Save();
    }
}