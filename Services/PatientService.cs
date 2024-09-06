using HospitalCase.Models;
using HospitalCase.Repositories.Patient;

public class PatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public PatientModel GetPatient(Guid id)
    {
        return _patientRepository.GetById(id);
    }

    public IEnumerable<PatientModel> GetAllPatients()
    {
        return _patientRepository.GetAll();
    }

    public void AddPatient(PatientModel patient)
    {
        _patientRepository.Add(patient);
    }

    public void UpdatePatient(PatientModel patient)
    {
        _patientRepository.Update(patient);
    }

    public void DeletePatient(Guid id)
    {
        _patientRepository.Delete(id);
    }
}