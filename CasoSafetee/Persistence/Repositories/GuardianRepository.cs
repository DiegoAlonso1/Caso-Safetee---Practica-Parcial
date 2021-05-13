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
    public class GuardianRepository : BaseRepository, IGuardianRepository
    {
        public GuardianRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Guardian guardian)
        {
            await _context.Guardians.AddAsync(guardian);
        }

        public void Remove(Guardian guardian)
        {
            _context.Guardians.Remove(guardian);
        }

        public async Task<Guardian> FindByIdAsync(int guardianId)
        {
            var guardians = await _context.Guardians.Include(g => g.Urgencies).ToListAsync();
            return guardians.Where(g => g.Id == guardianId).FirstOrDefault();
        }

        public async Task<IEnumerable<Guardian>> ListAsync()
        {
            return await _context.Guardians.Include(g => g.Urgencies).ToListAsync();
        }

        public void Update(Guardian guardian)
        {
            _context.Guardians.Update(guardian);
        }

        public async Task AddUrgencyAsync(Urgency urgency)
        {
            await _context.Guardians.ToListAsync();
        }
    }
}
