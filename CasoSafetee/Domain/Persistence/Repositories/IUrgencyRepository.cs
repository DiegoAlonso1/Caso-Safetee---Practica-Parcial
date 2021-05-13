using CasoSafetee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Persistence.Repositories
{
    public interface IUrgencyRepository
    {
        Task<IEnumerable<Urgency>> ListByGuardianIdAsync(int guardianId);
        Task<Urgency> FindByIdAsync(int urgencyId);
        Task AddAsync(Urgency urgency);
        void Update(Urgency urgency);
        void Remove(Urgency urgency);
    }
}
