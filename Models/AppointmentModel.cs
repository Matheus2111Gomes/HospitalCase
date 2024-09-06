namespace HospitalCase.Models
{
    public class AppointmentModel
    {
        public Guid AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string? AppointmentInfos { get; set; }

        public DoctorModel? Doctor { get; set; }
        public PatientModel? Patient { get; set; }

    }
}
