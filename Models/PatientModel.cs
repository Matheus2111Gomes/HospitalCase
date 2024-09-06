namespace HospitalCase.Models
{
    public class PatientModel
    {
        public Guid PatientId { get; set; }
        public string? PatientName { get; set;  }
        public string? PatientAddress { get; set; }
        public string? PatientTelephone{ get; set; }
        public string? PatientDocument { get; set; }
        public DateTime? PatientDateBirth{ get; set; }
        public ICollection<DoctorModel>? Doctors { get; set; }
        public ICollection<AppointmentModel>? Appointments { get; set; }


    }
}
