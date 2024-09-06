using HospitalCase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync();
        Task<DoctorModel?> GetDoctorByIdAsync(Guid doctorId);
        Task AddDoctorAsync(DoctorModel doctor);
        Task UpdateDoctorAsync(DoctorModel doctor);
        Task DeleteDoctorAsync(Guid doctorId);
    }
}