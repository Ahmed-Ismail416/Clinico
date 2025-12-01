using Shared.Dtos.DoctorDto;
using Shared.Dtos.DoctorsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IDoctorService
    {
        Task<IReadOnlyList<DoctorResponseDto>> GetAllDoctorsAsync();
        Task<DoctorResponseDto?> GetDoctorByIdAsync(int id);
        Task<DoctorResponseDto> CreateDoctorAsync(RegisterDoctorDto doctorCreateDto);
        Task<DoctorResponseDto> UpdateDoctorAsync(int id, UpdateDoctorDto doctorUpdateDto);
        Task DeleteDoctorAsync(int id);

    }
}
