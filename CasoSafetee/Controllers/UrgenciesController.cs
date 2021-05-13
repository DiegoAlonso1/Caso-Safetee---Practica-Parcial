using AutoMapper;
using CasoSafetee.Domain.Models;
using CasoSafetee.Domain.Services;
using CasoSafetee.Extensions;
using CasoSafetee.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasoSafetee.Controllers
{
    [Route("api/guardians/{guardianId}/urgencies")]
    [Produces("application/json")]
    [ApiController]
    public class UrgenciesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUrgencyService _urgencyService;

        public UrgenciesController(IUrgencyService urgencyService, IMapper mapper)
        {
            _urgencyService = urgencyService;
            _mapper = mapper;
        }



        //GET ALL BY GUARDIAN ID
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UrgencyResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UrgencyResource>> GetAllByGuardianIdAsync(int guardianId)
        {
            var urgencies = await _urgencyService.ListByGuardianIdAsync(guardianId);
            var resources = _mapper.Map<IEnumerable<Urgency>, IEnumerable<UrgencyResource>>(urgencies);
            return resources;
        }



        //GET BY ID
        [HttpGet("{urgencyId}")]
        [ProducesResponseType(typeof(UrgencyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetById(int urgencyId)
        {
            var result = await _urgencyService.GetByIdAsync(urgencyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var urgencyResource = _mapper.Map<Urgency, UrgencyResource>(result.Resource);
            return Ok(urgencyResource);
        }



        //POST
        [HttpPost]
        [ProducesResponseType(typeof(UrgencyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int guardianId, [FromBody] SaveUrgencyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var urgency = _mapper.Map<SaveUrgencyResource, Urgency>(resource);

            var result = await _urgencyService.SaveAsync(guardianId, urgency);

            if (!result.Success)
                return BadRequest(result.Message);
            var urgencyResource = _mapper.Map<Urgency, UrgencyResource>(result.Resource);
            return Ok(urgencyResource);
        }



        //PUT
        [HttpPut("{urgencyId}")]
        [ProducesResponseType(typeof(GuardianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int guardianId, int urgencyId, [FromBody] SaveUrgencyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var urgency = _mapper.Map<SaveUrgencyResource, Urgency>(resource);
            var result = await _urgencyService.UpdateAsync(guardianId, urgencyId, urgency);

            if (!result.Success)
                return BadRequest(result.Message);

            var urgencyResource = _mapper.Map<Urgency, UrgencyResource>(result.Resource);
            return Ok(urgencyResource);
        }



        //DELETE
        [HttpDelete("{urgencyId}")]
        [ProducesResponseType(typeof(UrgencyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int urgencyId)
        {
            var result = await _urgencyService.DeleteAsync(urgencyId);
            if (!result.Success)
                return BadRequest(result.Message);
            var urgencyResource = _mapper.Map<Urgency, UrgencyResource>(result.Resource);
            return Ok(urgencyResource);
        }
    }
}
