using Shared.Dtos.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IClinicService
    {
        Task<IReadOnlyList<ClinicResponseDto>> GetAllClinicsAsync();
        Task<ClinicResponseDto?> GetClinicByIdAsync(int id);
        Task<ClinicResponseDto> CreateClinicAsync(CreateClinicDto clinicCreateDto);
        Task<ClinicResponseDto> UpdateClinicAsync(int id, CreateClinicDto clinicUpdateDto);
        Task DeleteClinicAsync(int id);
    }
}
