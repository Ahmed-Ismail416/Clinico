using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using ServiceAbstraction;
using Shared.Dtos.Clinics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClinicService(IUnitOfWork _unitOfWork) : IClinicService
    {
        public async Task<ClinicResponseDto> CreateClinicAsync(CreateClinicDto clinicCreateDto)
        {

            var clinic = new Clinic
            {
                Name = clinicCreateDto.Name,
                Address = clinicCreateDto.Address,
                Phone = clinicCreateDto.Phone
            };
            await _unitOfWork.GetRepo<Clinic,int>().AddAsync(clinic);
            await _unitOfWork.SaveChangesAsync();

            return new ClinicResponseDto
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                Phone = clinic.Phone
            };

        }

        public async Task<IReadOnlyList<ClinicResponseDto>> GetAllClinicsAsync()
        {
            var clinics = await _unitOfWork.GetRepo<Clinic,int>().ListAllAsync();
            return clinics.Select(c => new ClinicResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone
            }).ToList();
        }

        public async Task<ClinicResponseDto?> GetClinicByIdAsync(int  id)
        {
            var clinic = await _unitOfWork.GetRepo<Clinic,int>().GetByIdAsync(id);
            if (clinic == null)
                throw new ClinicNotFoundException(id);

            return new ClinicResponseDto()
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                Phone = clinic.Phone
            };
        }

        public async Task<ClinicResponseDto> UpdateClinicAsync(int id, CreateClinicDto clinicUpdateDto)
        {
            var repo = _unitOfWork.GetRepo<Clinic, int>();
            var clinic = await repo.GetByIdAsync(id);
            if (clinic == null)
                throw new ClinicNotFoundException(id);

            clinic.Name = clinicUpdateDto.Name;
            clinic.Address = clinicUpdateDto.Address;
            clinic.Phone = clinicUpdateDto.Phone;

            try
            {
                repo.Update(clinic);
                await _unitOfWork.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new BadRequestException(new List<string> { ex.Message }, "Can't update clinic");
            }
            return new ClinicResponseDto
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                Phone = clinic.Phone
            };
        }

        public async Task DeleteClinicAsync(int id)
        {
            var repo = _unitOfWork.GetRepo<Clinic, int>();
            var clinic = await repo.GetByIdAsync(id);
            if (clinic == null)
                throw new ClinicNotFoundException(id);
            try
            {

                repo.Delete(clinic);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(new List<string> { ex.Message }, "Can't delete clinic");

            }
        }
    }
}
