using DomainLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Shared.Dtos;
using Shared.Dtos.DoctorDto;
using Shared.Dtos.DoctorsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
 
    public class DoctorController(IServiceManager _serviceManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<PaginationResult<DoctorResponseDto>> GetDoctors([FromQuery] DoctorParams dp )
        {
            var doctors = await _serviceManger.DoctorService.GetAllDoctorsAsync(dp);
            return doctors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> GetDoctor(int id)
        {
            var result = await _serviceManger.DoctorService.GetDoctorByIdAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<DoctorResponseDto>> CreateDoctor([FromForm] RegisterDoctorDto dto)
        {
            var result = await _serviceManger.DoctorService.CreateDoctorAsync(dto);
            return CreatedAtAction(nameof(GetDoctors), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> UpdateDoctor(int id, [FromForm] UpdateDoctorDto dto)
        {
            var result = await _serviceManger.DoctorService.UpdateDoctorAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            await _serviceManger.DoctorService.DeleteDoctorAsync(id);
            return NoContent();
        }


    }
}
