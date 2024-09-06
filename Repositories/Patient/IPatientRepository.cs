using HospitalCase.Models;
using System;
using System.Collections.Generic;

namespace HospitalCase.Repositories.Patient
{
    public interface IPatientRepository
    {
        PatientModel GetById(Guid id);
        IEnumerable<PatientModel> GetAll();
        void Add(PatientModel patient);
        void Update(PatientModel patient);
        void Delete(Guid id);
    }
}