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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class GuardiansController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGuardianService _guardianService;

        public GuardiansController(IGuardianService guardianService, IMapper mapper)
        {
            _guardianService = guardianService;
            _mapper = mapper;
        }

        //GET ALL
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GuardianResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<GuardianResource>> GetAllAsync()
        {
            var guardians = await _guardianService.ListAsync();

            var resources = _mapper.Map<IEnumerable<Guardian>, IEnumerable<GuardianResource>>(guardians);
            
            return resources;
        }



        //POST
        [HttpPost]
        [ProducesResponseType(typeof(GuardianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveGuardianResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var guardian = _mapper.Map<SaveGuardianResource, Guardian>(resource);

            var result = await _guardianService.SaveAsync(guardian);

            if (!result.Success)
                return BadRequest(result.Message);
            var guardianResource = _mapper.Map<Guardian, GuardianResource>(result.Resource);
            return Ok(guardianResource);
        }



        //GET BY ID
        [HttpGet("{guardianId}")]
        [ProducesResponseType(typeof(GuardianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetById(int guardianId)
        {
            var result = await _guardianService.GetByIdAsync(guardianId);
            if (!result.Success)
                return BadRequest(result.Message);

            var guardianResource = _mapper.Map<Guardian, GuardianResource>(result.Resource);
            return Ok(guardianResource);
        }



        //PUT
        [HttpPut("{guardianId}")]
        [ProducesResponseType(typeof(GuardianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int guardianId, [FromBody] SaveGuardianResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var guardian = _mapper.Map<SaveGuardianResource, Guardian>(resource);
            var result = await _guardianService.UpdateAsync(guardianId, guardian);

            if (!result.Success)
                return BadRequest(result.Message);

            var guardianResource = _mapper.Map<Guardian, GuardianResource>(result.Resource);
            return Ok(guardianResource);
        }



        //DELETE
        [HttpDelete("{guardianId}")]
        [ProducesResponseType(typeof(GuardianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int guardianId)
        {
            var result = await _guardianService.DeleteAsync(guardianId);
            if (!result.Success)
                return BadRequest(result.Message);
            var guardianResource = _mapper.Map<Guardian, GuardianResource>(result.Resource);
            return Ok(guardianResource);

            //Las urgencias se eliminan automáticamente de la base de datos
        }
    }
}
