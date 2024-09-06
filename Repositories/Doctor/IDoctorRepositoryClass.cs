using HospitalCase.Models;

public interface IDoctorRepository
{
    Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync();
    Task<DoctorModel?> GetDoctorByIdAsync(Guid doctorId);
    Task AddDoctorAsync(DoctorModel doctor);
    Task UpdateDoctorAsync(DoctorModel doctor);
    Task DeleteDoctorAsync(Guid doctorId);
}