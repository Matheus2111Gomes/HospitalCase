namespace HospitalCase.Models
{
    public class DoctorModel
    {
        public Guid DoctorId { get; set; }
        public string? DoctorCRM { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpecialty { get; set; }
        public Guid[]? AppointmentList { get; set; }
    }
}
