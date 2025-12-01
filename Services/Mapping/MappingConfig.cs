using Core.DomainLayer.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;
using Services; // add this
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
                .Map(dest => dest.FullName, src => src.AppUser.FullName)
                .Map(dest => dest.Email, src => src.AppUser.Email)
                .Map(dest => dest.PhoneNumber, src => src.AppUser.PhoneNumber)
                .Map(dest => dest.ProfilePictureUrl, src => $"{BaseUrl}{src.AppUser.ProfilePictureUrl.Replace("\\","/")}")

                .Map(dest => dest.ClinicName, src => src.Clinic.Name);

            config.NewConfig<UpdateDoctorDto, Doctor>()
                .Map(dest => dest.AppUser.FullName, src => src.FullName)
                .Map(dest => dest.AppUser.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.AppUser.Email, src => src.Email)
                .Map(dest => dest.AppUser.PhoneNumber , src => src.PhoneNumber)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.AppUserId)
                .Ignore(dest => dest.AppUser);

        }
    }
}
