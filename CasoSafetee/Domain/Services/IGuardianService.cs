using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Domain.Services
{
    public interface IGuardianService
    {
        Task<IEnumerable<Guardian>> ListAsync();
        Task<GuardianResponse> SaveAsync(Guardian guardian);
        Task<GuardianResponse> GetByIdAsync(int guardianId);
        Task<GuardianResponse> UpdateAsync(int guardianId, Guardian guardian);
        Task<GuardianResponse> DeleteAsync(int guardianId);
    }
}
