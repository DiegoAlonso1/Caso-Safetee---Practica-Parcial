using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Persistence.Repositories;
using CasoSafetee.Domain.Services;
using CasoSafetee.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Services
{
    public class GuardianService : IGuardianService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGuardianRepository _guardianRepository;

        public GuardianService(IGuardianRepository guardianRepository, IUnitOfWork unitOfWork)
        {
            _guardianRepository = guardianRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GuardianResponse> DeleteAsync(int guardianId)
        {
            var existingGuardian = await _guardianRepository.FindByIdAsync(guardianId);

            if (existingGuardian == null)
                return new GuardianResponse("Guardian not found");

            try
            {
                _guardianRepository.Remove(existingGuardian);
                await _unitOfWork.CompleteAsync();
                return new GuardianResponse(existingGuardian);
            }
            catch (Exception ex)
            {
                return new GuardianResponse($"An error ocurred while deleting the guardian: {ex.Message}");
            }
        }

        public async Task<GuardianResponse> GetByIdAsync(int guardianId)
        {
            var existingGuardian = await _guardianRepository.FindByIdAsync(guardianId);

            if (existingGuardian == null)
                return new GuardianResponse("Guardian not found");

            return new GuardianResponse(existingGuardian);
        }

        public async Task<IEnumerable<Guardian>> ListAsync()
        {
            return await _guardianRepository.ListAsync();
        }

        public async Task<GuardianResponse> SaveAsync(Guardian guardian)
        {
            if (string.IsNullOrWhiteSpace(guardian.FirstName) || string.IsNullOrWhiteSpace(guardian.LastName))
                return new GuardianResponse("Guardian's FirstName or LastName is incorrect");

            try
            {
                await _guardianRepository.AddAsync(guardian);
                await _unitOfWork.CompleteAsync();
                return new GuardianResponse(guardian);
            }
            catch (Exception ex)
            {
                return new GuardianResponse($"An error ocurred while saving the guardian: {ex.Message}");
            }
        }

        public async Task<GuardianResponse> UpdateAsync(int guardianId, Guardian guardian)
        {
            if (string.IsNullOrWhiteSpace(guardian.FirstName) || string.IsNullOrWhiteSpace(guardian.LastName))
                return new GuardianResponse("Guardian's FirstName or LastName is incorrect");

            var existingGuardian = await _guardianRepository.FindByIdAsync(guardianId);

            if (existingGuardian == null)
                return new GuardianResponse("Guardian not found");

            existingGuardian.Username = guardian.Username;
            existingGuardian.Email = guardian.Email;
            existingGuardian.FirstName = guardian.FirstName;
            existingGuardian.LastName = guardian.LastName;
            existingGuardian.Gender = guardian.Gender;
            existingGuardian.Adress = guardian.Adress;

            try
            {
                _guardianRepository.Update(existingGuardian);
                await _unitOfWork.CompleteAsync();
                return new GuardianResponse(existingGuardian);
            }
            catch (Exception ex)
            {
                return new GuardianResponse($"An error ocurred while updating the guardian: {ex.Message}");
            }
        }
    }
}
