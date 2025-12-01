using DomainLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class ClinicController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpGet("clinics")]
        public async Task<ActionResult<ClinicResponseDto>> GetClinics()
        {
            var clinics = await _serviceManager.ClinicService.GetAllClinicsAsync();
            return Ok(clinics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicResponseDto>> GetClinic(int id)
        {
            var result = await _serviceManager.ClinicService.GetClinicByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClinicResponseDto>> CreateClinic([FromBody] CreateClinicDto dto)
        {
            var result = await _serviceManager.ClinicService.CreateClinicAsync(dto);
            return CreatedAtAction(nameof(GetClinic), new { id = result.Id }, result);
        }

        // update
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClinicResponseDto>> UpdateClinic(int id, [FromBody] CreateClinicDto dto)
        {
            var result = await _serviceManager.ClinicService.UpdateClinicAsync(id, dto);
            return Ok(result);
        }

        // delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            await _serviceManager.ClinicService.DeleteClinicAsync(id);
            return NoContent();
        }
    }
}