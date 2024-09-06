using HospitalCase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalCase.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<DoctorModel?> GetDoctorByIdAsync(Guid doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            return doctor;
        }

        public async Task AddDoctorAsync(DoctorModel doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.DoctorCRM))
            {
                throw new ArgumentException("CRM is required.");
            }

            doctor.DoctorId = Guid.NewGuid();
            await _doctorRepository.AddDoctorAsync(doctor);
        }

        public async Task UpdateDoctorAsync(DoctorModel doctor)
        {
            var existingDoctor = await _doctorRepository.GetDoctorByIdAsync(doctor.DoctorId);
            if (existingDoctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            await _doctorRepository.UpdateDoctorAsync(doctor);
        }

        public async Task DeleteDoctorAsync(Guid doctorId)
        {
            var existingDoctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (existingDoctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            await _doctorRepository.DeleteDoctorAsync(doctorId);
        }
    }
}