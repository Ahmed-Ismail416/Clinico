using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Services.Specification;
using Shared.Dtos.DoctorDto;
using Shared.Dtos.DoctorsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctorService(IUnitOfWork _unitOfWork,
                                UserManager<AppUser> _userManager) : IDoctorService
    {
        #region Create
        public async Task<DoctorResponseDto> CreateDoctorAsync(RegisterDoctorDto doctorCreateDto)
        {
            //get Clinic by id
            var clinic = await _unitOfWork.GetRepo<Clinic, int>().GetByIdAsync(doctorCreateDto.ClinicId);
            if (clinic == null)
                throw new ClinicNotFoundException(doctorCreateDto.ClinicId);
            // get User by email
            var existingUser = await _userManager.FindByEmailAsync(doctorCreateDto.Email);
            if (existingUser != null)
                throw new BadRequestException(new List<string> { "Email taken" }, "Failed");

            //Upload Image 
            var imageUrl = "";
            if (doctorCreateDto.Image != null)
            {
                imageUrl = await FileService.UploadFileAsync(doctorCreateDto.Image, "Doctors");

            }

            //create User
            var appUser = new AppUser
            {
                UserName = doctorCreateDto.Email.Split('@')[0],
                Email = doctorCreateDto.Email,
                FullName = doctorCreateDto.FullName,
                PhoneNumber = doctorCreateDto.PhoneNumber,
                ProfilePictureUrl = imageUrl
            };

            // Add User and Role to it
            var result = await _userManager.CreateAsync(appUser, doctorCreateDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(errors, "Can't Create User");
            }
            await _userManager.AddToRoleAsync(appUser, "Doctor");

            //create Doctor
            var doctor = doctorCreateDto.Adapt<Doctor>();
            doctor.AppUserId = appUser.Id;

            await _unitOfWork.GetRepo<Doctor, int>().AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();



            //retun 
            var doctorDto = doctor.Adapt<DoctorResponseDto>();
            doctorDto.FullName = appUser.FullName;
            doctorDto.Email = appUser.Email;
            doctorDto.PhoneNumber = appUser.PhoneNumber;
            doctorDto.ClinicName = clinic.Name;
            doctorDto.ProfilePictureUrl = appUser.ProfilePictureUrl;
            return doctorDto;

        }
        #endregion

        #region Read
        public async Task<IReadOnlyList<DoctorResponseDto>> GetAllDoctorsAsync()
        {
            var Spec = new DoctorWithClinicAndUserSpecification();
            var doctors = await _unitOfWork.GetRepo<Doctor, int>().ListAsyncWithSpec(Spec);
            var doctorDtos = doctors.Adapt<IReadOnlyList<DoctorResponseDto>>();

            return doctorDtos;
        }

        public async Task<DoctorResponseDto?> GetDoctorByIdAsync(int id)
        {
            var spec = new DoctorWithClinicAndUserSpecification(id);
            var doctor = await _unitOfWork.GetRepo<Doctor, int>().GetEntityWithSpec(spec) ?? throw new DoctorNotFoundException(id);
            return doctor.Adapt<DoctorResponseDto>();
        }
        #endregion

        #region Update
        public async Task<DoctorResponseDto> UpdateDoctorAsync(int id, UpdateDoctorDto doctorUpdateDto)
        {

            // 1. نجيب الدكتور مع بيانات اليوزر عشان نعدل الاتنين
            var spec = new DoctorWithClinicAndUserSpecification(id);
            var doctor = await _unitOfWork.GetRepo<Doctor, int>().GetEntityWithSpec(spec);

            if (doctor == null)
                throw new DoctorNotFoundException(id);
            var clinic = await _unitOfWork.GetRepo<Clinic, int>().GetByIdAsync(doctorUpdateDto.ClinicId);
            if (clinic == null)
                throw new ClinicNotFoundException(doctorUpdateDto.ClinicId);
            doctorUpdateDto.Email = doctorUpdateDto.Email.Split('@')[0];
            // 2. نعدل بيانات الدكتور واليوزر
            doctorUpdateDto.Adapt(doctor);

            if (doctorUpdateDto.Image != null)
            {
                // Upload new image
                if (!string.IsNullOrEmpty(doctor.AppUser.ProfilePictureUrl))
                {
                    FileService.DeleteFile(doctor.AppUser.ProfilePictureUrl);
                }
                var imageUrl = await FileService.UploadFileAsync(doctorUpdateDto.Image, "Doctors");
                doctor.AppUser.ProfilePictureUrl = imageUrl;
            }
            _unitOfWork.GetRepo<Doctor, int>().Update(doctor);
            await _unitOfWork.SaveChangesAsync();
            return doctor.Adapt<DoctorResponseDto>();
        }
        #endregion

        #region Delete
        public async Task DeleteDoctorAsync(int id)
        {
            var spec = new DoctorWithClinicAndUserSpecification(id);
            var doctor = await _unitOfWork.GetRepo<Doctor, int>().GetEntityWithSpec(spec);

            if (doctor == null)
                throw new DoctorNotFoundException(id); 

            // 1. مسح الصورة من السيرفر
            if (!string.IsNullOrEmpty(doctor.AppUser.ProfilePictureUrl))
            {
                FileService.DeleteFile(doctor.AppUser.ProfilePictureUrl);
            }

            // 2. مسح الدكتور (هيمسح الـ Doctor Entity)
            _unitOfWork.GetRepo<Doctor, int>().Delete(doctor);

            // 3. مسح اليوزر (Identity User) - اختياري حسب البيزنس
            // لو مسحنا الدكتور، هل نمسح حساب دخوله؟ غالباً نعم في هذا النوع من الأنظمة
            await _userManager.DeleteAsync(doctor.AppUser);

            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}