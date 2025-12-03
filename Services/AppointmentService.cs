using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using ServiceAbstraction;
using Services.Specification;
using Shared.Dtos.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppointmentService(IUnitOfWork _unitOfWork) : IAppointmentService
    {
        public async Task<AppointmentResponseDto> CreateAppointmentAsync(CreateAppointmentDto Dto, string patientId)
        {
            // Check doctor's availability for the requested time slot
            var doctor =await _unitOfWork.GetRepo<Doctor, int>().GetByIdAsync(Dto.DoctorId)
                ?? throw new DoctorNotFoundException(Dto.DoctorId);
            // Validate Time
            if(Dto.EndTime <= Dto.StartTime)
                throw new BadRequestException(new List<string> { "End time must be after start time." }, "Invalid appointment time");
            // Conflict Checking
            var spec = new AppointmentConflictSpecification(Dto.DoctorId, Dto.StartTime, Dto.EndTime);
            var conflictingAppointments = await _unitOfWork.GetRepo<Appointment, int>().CountAsync(spec);
            if(conflictingAppointments > 0)
                throw new AppointmentConflictException("The doctor is not available during the requested time slot.");

            // Create Mapping

            var appointment = Dto.Adapt<Appointment>();
            appointment.PatientId = patientId;
            appointment.Status = AppointmentStatus.Pending;
            // Save
            await _unitOfWork.GetRepo<Appointment, int>().AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            // return 
            var displaySpec = new AppointmentWithDetailsSpecification(appointment.Id);
            var createdAppointment = await _unitOfWork.GetRepo<Appointment, int>().GetEntityWithSpec(displaySpec)
                ?? throw new AppointmentNotFoundException(appointment.Id);
            return createdAppointment.Adapt<AppointmentResponseDto>();
            
        }

        public async Task<IReadOnlyList<AppointmentResponseDto>> GetAllApointmentAsync()
        {
            var spec = new AppointmentWithDetailsSpecification();
            var appointments = await _unitOfWork.GetRepo<Appointment, int>().ListAsyncWithSpec(spec);
            return appointments.Adapt<IReadOnlyList<AppointmentResponseDto>>();
        }

        public async Task<AppointmentResponseDto> GetAppointmentsByIdAsync(int id)
        {
            var spec = new AppointmentWithDetailsSpecification(id);
            var appointment = await _unitOfWork.GetRepo<Appointment, int>().GetEntityWithSpec(spec);

            if (appointment == null)
                throw new BadRequestException(new List<string> { "Appointment not found" }, "Not Found");

            return appointment.Adapt<AppointmentResponseDto>();
        }

        public async Task<AppointmentResponseDto> UpdateAppointmentSatusAsync(int id, UpdateِAppointmentSatatusDto dto)
        {
            var appointment = await   _unitOfWork.GetRepo<Appointment, int>().GetByIdAsync(id)
                ?? throw new AppointmentNotFoundException(id);

            appointment.Status = dto.Status;

            _unitOfWork.GetRepo<Appointment, int>().Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            var spec = new AppointmentWithDetailsSpecification(id);
            var updatedAppointment = await _unitOfWork.GetRepo<Appointment, int>().GetEntityWithSpec(spec)
                ?? throw new AppointmentNotFoundException(id);
            return updatedAppointment.Adapt<AppointmentResponseDto>();

        }
    }
}
