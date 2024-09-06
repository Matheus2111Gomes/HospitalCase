namespace HospitalCase.Models
{
    public class DoctorModel
    {
        public Guid DoctorId { get; set; }
        public string? DoctorCRM { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpecialty { get; set; }
        public ICollection<PatientModel>? Patients { get; set; }
        public ICollection<AppointmentModel>? Appointments { get; set; }

    }
}
