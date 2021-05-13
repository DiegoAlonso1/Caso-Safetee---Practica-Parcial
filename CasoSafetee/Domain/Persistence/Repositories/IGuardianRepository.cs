using CasoSafetee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Persistence.Repositories
{
    public interface IGuardianRepository
    {
        Task<IEnumerable<Guardian>> ListAsync();
        Task AddAsync(Guardian guardian);
        Task AddUrgencyAsync(Urgency urgency);
        Task<Guardian> FindByIdAsync(int guardianId);
        void Update(Guardian guardian);
        void Remove(Guardian guardian);
    }
}
