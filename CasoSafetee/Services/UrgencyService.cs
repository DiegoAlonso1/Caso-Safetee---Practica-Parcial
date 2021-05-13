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
    public class UrgencyService : IUrgencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUrgencyRepository _urgencyRepository;
        private readonly IGuardianRepository _guardianRepository;

        public UrgencyService(IUrgencyRepository urgencyRepository, IUnitOfWork unitOfWork, IGuardianRepository guardianRepository)
        {
            _urgencyRepository = urgencyRepository;
            _unitOfWork = unitOfWork;
            _guardianRepository = guardianRepository;
        }

        public async Task<UrgencyResponse> DeleteAsync(int urgencyId)
        {
            var existingUrgency = await _urgencyRepository.FindByIdAsync(urgencyId);

            if (existingUrgency == null)
                return new UrgencyResponse("Urgency not found");

            try
            {
                _urgencyRepository.Remove(existingUrgency);
                await _unitOfWork.CompleteAsync();
                return new UrgencyResponse(existingUrgency);
            }
            catch (Exception ex)
            {
                return new UrgencyResponse($"An error ocurred while deleting the urgency: {ex.Message}");
            }
        }

        public async Task<UrgencyResponse> GetByIdAsync(int urgencyId)
        {
            var existingUrgency = await _urgencyRepository.FindByIdAsync(urgencyId);
            
            if (existingUrgency == null)
                return new UrgencyResponse("Urgency not found");

            return new UrgencyResponse(existingUrgency);
        }

        public async Task<IEnumerable<Urgency>> ListByGuardianIdAsync(int guardianId)
        {
            return await _urgencyRepository.ListByGuardianIdAsync(guardianId);
        }

        public async Task<UrgencyResponse> SaveAsync(int guardianId, Urgency urgency)
        {
            var existingGuardian = await _guardianRepository.FindByIdAsync(guardianId);

            if (existingGuardian == null)
                return new UrgencyResponse("Guardian not found");

            var othersUrgencies = await _urgencyRepository.ListByGuardianIdAsync(guardianId);


            var otherUrgency = othersUrgencies.Where(other => urgency.Title == other.Title && urgency.Latitude == other.Latitude 
                && urgency.Longitude == other.Longitude && DateTime.Now.Date == other.ReportedAt.Date).ToList().FirstOrDefault();

            if (otherUrgency != null)
                return new UrgencyResponse("There is another urgency with same title, location and report day");

            urgency.Guardian = existingGuardian;

            try
            {
                await _urgencyRepository.AddAsync(urgency);
                await _unitOfWork.CompleteAsync();
                return new UrgencyResponse(urgency);
            }
            catch (Exception ex)
            {
                return new UrgencyResponse($"An error ocurred while saving the urgency: {ex.Message}");
            }
        }

        public async Task<UrgencyResponse> UpdateAsync(int guardianId, int urgencyId, Urgency urgency)
        {
            var existingGuardian = await _guardianRepository.FindByIdAsync(guardianId);

            if (existingGuardian == null)
                return new UrgencyResponse("Guardian not found");

            var existingUrgency = await _urgencyRepository.FindByIdAsync(urgencyId);

            if (existingUrgency == null)
                return new UrgencyResponse("Urgency not found");

            existingUrgency.Title = urgency.Title;
            existingUrgency.Summary = urgency.Summary;
            existingUrgency.Latitude = urgency.Latitude;
            existingUrgency.Longitude = urgency.Longitude;

            var othersUrgencies = await _urgencyRepository.ListByGuardianIdAsync(guardianId);


            var otherUrgency = othersUrgencies.Where(other => other != existingUrgency).Where(other => existingUrgency.Title == other.Title && existingUrgency.Latitude == other.Latitude
                && existingUrgency.Longitude == other.Longitude && existingUrgency.ReportedAt.Date == other.ReportedAt.Date).ToList().FirstOrDefault();

            if (otherUrgency != null)
                return new UrgencyResponse("There is another urgency with same title, location and report day");

            try
            {
                _urgencyRepository.Update(existingUrgency);
                await _unitOfWork.CompleteAsync();
                return new UrgencyResponse(urgency);
            }
            catch (Exception ex)
            {
                return new UrgencyResponse($"An error ocurred while updating the urgency: {ex.Message}");
            }
        }
    }
}
