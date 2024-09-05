using HospitalCase.Models;

namespace HospitalCase.Repositories.Patient
{
    public interface IPatientRepository
    {
        PatientModel GetById(int id);
        IEnumerable<PatientModel> GetAll();
        void Add(PatientModel patient);
        void Update(PatientModel patient);
        void Delete(int id);
    }
}
