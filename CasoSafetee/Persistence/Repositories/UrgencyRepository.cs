using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Persistence.Contexts;
using CasoSafetee.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Persistence.Repositories
{
    public class UrgencyRepository : BaseRepository, IUrgencyRepository
    {
        public UrgencyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Urgency urgency)
        {
            await _context.Urgencies.AddAsync(urgency);
        }

        public void Remove(Urgency urgency)
        {
            _context.Urgencies.Remove(urgency);
        }

        public async Task<Urgency> FindByIdAsync(int urgencyId)
        {
            return await _context.Urgencies.FindAsync(urgencyId);
        }

        public async Task<IEnumerable<Urgency>> ListByGuardianIdAsync(int guardianId)
        {
            return await _context.Urgencies.Where(u => u.GuardianId == guardianId).ToListAsync();
        }

        public void Update(Urgency urgency)
        {
            _context.Urgencies.Update(urgency);
        }
    }
}
