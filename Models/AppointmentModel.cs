namespace HospitalCase.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? AppointmentInfos { get; set; }
       
    }
}
