using Core.DomainLayer.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;
using Services; // add this
using Shared.Dtos.AppointmentDto;
using Shared.Dtos.DoctorDto;
using Shared.Dtos.DoctorsDto;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    internal class MappingConfig : IRegister
    {
        public static string BaseUrl;
        public void Register(TypeAdapterConfig config)
        {
            // Create Mapping
            config.NewConfig<RegisterDoctorDto, Doctor>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.AppUserId)
                .Ignore(dest => dest.Clinic)
                .Ignore(dest => dest.AppUser);

            // Read Mapping
            config.NewConfig<Doctor, DoctorResponseDto>()
                .Map(dest => dest.FullName,
                    src => src.AppUser != null ? src.AppUser.FullName : null)

                .Map(dest => dest.Email,
                    src => src.AppUser != null ? src.AppUser.Email : null)

                .Map(dest => dest.PhoneNumber,
                    src => src.AppUser != null ? src.AppUser.PhoneNumber : null)

                .Map(dest => dest.ProfilePictureUrl,
                    src => src.AppUser != null && src.AppUser.ProfilePictureUrl != null
                         ? $"{BaseUrl}{src.AppUser.ProfilePictureUrl.Replace("\\", "/")}"
                         : null)

                .Map(dest => dest.ClinicName,
                    src => src.Clinic != null ? src.Clinic.Name : null);

            config.NewConfig<UpdateDoctorDto, Doctor>()
                .Map(dest => dest.AppUser.FullName, src => src.FullName)
                .Map(dest => dest.AppUser.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.AppUser.Email, src => src.Email)
                .Map(dest => dest.AppUser.PhoneNumber, src => src.PhoneNumber)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.AppUserId)
                .Ignore(dest => dest.AppUser);


            config.NewConfig<CreateAppointmentDto, Appointment>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.PatientId)
                .Ignore(dest => dest.Patient)
                .Ignore(dest => dest.Status)
                .Ignore(dest => dest.Doctor)
                .Ignore(dest => dest.Status);

            config.NewConfig<Appointment, AppointmentResponseDto>()
                .Map(dest => dest.DoctorName, src => src.Doctor.AppUser.FullName)
                .Map(dest => dest.ClinicName, src => src.Doctor.Clinic.Name)
                .Map(dest => dest.PatientName, src => src.Patient.FullName);
        }
    }
}
