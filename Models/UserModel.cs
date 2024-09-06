public class UserModel
{
    public Guid Id { get; set; }
    public string? Username { get; set; } // CRM to doctors / Document to Patients
    public string? PasswordHash { get; set; }
    public string? Role { get; set; } 
    public Guid? ForeignId { get; set; } // PatientId / DoctorId
}