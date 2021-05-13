using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Services
{
    public interface IUrgencyService
    {
        Task<IEnumerable<Urgency>> ListByGuardianIdAsync(int guardianId);
        Task<UrgencyResponse> GetByIdAsync(int urgencyId);
        Task<UrgencyResponse> SaveAsync(int guardianId, Urgency urgency);
        Task<UrgencyResponse> UpdateAsync(int guardianId, int urgencyId, Urgency urgency);
        Task<UrgencyResponse> DeleteAsync(int urgencyId);
    }
}
