using DomainLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AppointmentController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<AppointmentResponseDto>> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? throw new UnauthorizedAccessException("User ID claim not found.");
            var appointment = await _serviceManager.AppointmentService.CreateAppointmentAsync(dto, patientId);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }
        [HttpGet]
        public async Task<ActionResult<PaginationResult<AppointmentResponseDto>>> GetAllAppointments([FromQuery] AppointmentParams ap)
        {
            // هنا ممكن مستقبلاً نعمل فلترة (المريض يشوف مواعيده بس، والدكتور يشوف مواعيده بس)
            var result = await _serviceManager.AppointmentService.GetAllApointmentAsync(ap);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> GetAppointment(int id)
        {
            var result = await _serviceManager.AppointmentService.GetAppointmentsByIdAsync(id);
            return Ok(result);
        }


        [HttpPut("{id}/status")]
        [Authorize(Roles = "Doctor,Admin")] // الدكتور أو الأدمن بس
        public async Task<ActionResult<AppointmentResponseDto>> UpdateStatus(int id, [FromBody] UpdateِAppointmentSatatusDto dto)
        {
            var result = await _serviceManager.AppointmentService.UpdateAppointmentSatusAsync(id, dto);
            return Ok(result);
        }

        [HttpGet("myappointments")]
        [Authorize(Roles = "Patient,Doctor,Admin")]
        public async Task<ActionResult<IReadOnlyList<AppointmentResponseDto>>> GetMyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                return Unauthorized();

            var result = await _serviceManager.AppointmentService.GetMyAppointmentAsync(userId, role);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Patient,Doctor,Admin")]
        public async Task<ActionResult<AppointmentResponseDto>> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto dto)
        {
            var result = await _serviceManager.AppointmentService.UpdateAppointmentAsync(id, dto);
            return Ok(result);
        }
    }
}
