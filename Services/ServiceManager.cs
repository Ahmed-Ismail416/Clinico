using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(UserManager<AppUser> _userManager,
                             IConfiguration _configuration,
                             IUnitOfWork _unitOfWork) : IServiceManager
    {
        // with lazy implementation
        public Lazy<IAuthService> _lazyAuthService = new Lazy<IAuthService>(() => new AuthService(_userManager, _configuration));

        public IAuthService AuthService =>_lazyAuthService.Value;

        public Lazy<IClinicService> _lazyClinicService = new Lazy<IClinicService>(() => new ClinicService(_unitOfWork));
        public IClinicService ClinicService => _lazyClinicService.Value;

        public Lazy<IDoctorService> _lazyDoctorService = new Lazy<IDoctorService>(() => new DoctorService(_unitOfWork, _userManager));
    
        public IDoctorService DoctorService => _lazyDoctorService.Value;

    }
}
