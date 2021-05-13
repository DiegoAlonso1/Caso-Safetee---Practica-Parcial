using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Persistence.Repositories;
using CasoSafetee.Domain.Services;
using CasoSafetee.Domain.Services.Communications;
using CasoSafetee.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CasoSafetee.Test
{
    public class GuardianServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdWhenNoGuardianFoundReturnsGuardianNotFoundResponse()
        {
            //Arrange
            var mockGuardianRepository = GetDefaultIGuardianRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var guardianId = 1;
            mockGuardianRepository.Setup(r => r.FindByIdAsync(guardianId)).
                Returns(Task.FromResult<Guardian>(null));

            var service = new GuardianService(mockGuardianRepository.Object, mockUnitOfWork.Object);

            //Act
            GuardianResponse result = await service.GetByIdAsync(guardianId);
            var message = result.Message;

            //Assert
            message.Should().Be("Guardian not found");
        }

        [Test]
        public async Task GetByIdWhenValidGuardianIdReturnsGuardian()
        {
            //Arrange
            var mockGuardianRepository = GetDefaultIGuardianRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var guardianId = 1;
            mockGuardianRepository.Setup(r => r.FindByIdAsync(guardianId)).
                Returns(Task.FromResult<Guardian>(null));

            var service = new GuardianService(mockGuardianRepository.Object, mockUnitOfWork.Object);

            //Act
            GuardianResponse result = await service.GetByIdAsync(guardianId);
            var message = result.Message;

            //Assert
            message.Should().Be("Guardian not found");
        }

        private Mock<IGuardianRepository> GetDefaultIGuardianRepositoryInstance()
        {
            return new Mock<IGuardianRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}