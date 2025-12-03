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
        Task<IReadOnlyList<AppointmentResponseDto>> GetAllApointmentAsync();
        Task<AppointmentResponseDto> UpdateAppointmentSatusAsync(int id, UpdateِAppointmentSatatusDto dto);
    }
}
