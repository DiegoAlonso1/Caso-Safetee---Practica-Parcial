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
            var urgencies = await _context.Urgencies.Include(u => u.Guardian).ToListAsync();
            return urgencies.Where(u => u.Id == urgencyId).FirstOrDefault();
        }

        public async Task<IEnumerable<Urgency>> ListByGuardianIdAsync(int guardianId)
        {
            var urgencies = await _context.Urgencies.Include(u => u.Guardian).ToListAsync();
            return urgencies.Where(u => u.GuardianId == guardianId).ToList();
        }

        public void Update(Urgency urgency)
        {
            _context.Urgencies.Update(urgency);
        }
    }
}
