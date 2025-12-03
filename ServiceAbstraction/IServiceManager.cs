using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        IClinicService ClinicService { get; }
        IDoctorService DoctorService { get; }
        IAppointmentService AppointmentService { get; }
    }
}
