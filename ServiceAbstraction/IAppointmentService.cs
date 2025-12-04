using Shared.Dtos;
using Shared.Dtos.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> CreateAppointmentAsync(CreateAppointmentDto appointmentCreateDto, string patientId);
        Task<AppointmentResponseDto> GetAppointmentsByIdAsync(int id);
        Task<PaginationResult<AppointmentResponseDto>> GetAllApointmentAsync(AppointmentParams ap);
        Task<AppointmentResponseDto> UpdateAppointmentSatusAsync(int id, UpdateِAppointmentSatatusDto dto);
    
        Task<IReadOnlyList<AppointmentResponseDto>> GetMyAppointmentAsync(string doctorId, string role);

        Task<AppointmentResponseDto> UpdateAppointmentAsync(int id, UpdateAppointmentDto dto);
    }
}
