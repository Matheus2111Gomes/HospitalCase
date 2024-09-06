using Microsoft.Data.SqlClient;
using HospitalCase.Models;

public class DoctorRepository : IDoctorRepository
{
    private readonly string _connectionString;

    public DoctorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync()
    {
        var doctors = new List<DoctorModel>();
        var query = "SELECT Id, CRM, Name, SpecialtyId FROM Doctors";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        doctors.Add(new DoctorModel
                        {
                            DoctorId = reader.GetGuid(0),
                            DoctorCRM = reader.GetString(1),
                            DoctorName = reader.GetString(2),
                            DoctorSpecialty = reader.GetString(3)
                        });
                    }
                }
            }
        }

        return doctors;
    }

    public async Task<DoctorModel?> GetDoctorByIdAsync(Guid doctorId)
    {
        var query = "SELECT Id, CRM, Name, SpecialtyId FROM Doctors WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", doctorId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new DoctorModel
                        {
                            DoctorId = reader.GetGuid(0),
                            DoctorCRM = reader.GetString(1),
                            DoctorName = reader.GetString(2),
                            DoctorSpecialty = reader.GetString(3)
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task AddDoctorAsync(DoctorModel doctor)
    {
        var query = "INSERT INTO Doctors (Id, CRM, Name, SpecialtyId) VALUES (@Id, @CRM, @Name, @SpecialtyId)";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", doctor.DoctorId);
                command.Parameters.AddWithValue("@CRM", doctor.DoctorCRM);
                command.Parameters.AddWithValue("@Name", doctor.DoctorName);
                command.Parameters.AddWithValue("@SpecialtyId", doctor.DoctorSpecialty); // Ajuste conforme o tipo de SpecialtyId

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateDoctorAsync(DoctorModel doctor)
    {
        var query = "UPDATE Doctors SET CRM = @CRM, Name = @Name, SpecialtyId = @SpecialtyId WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", doctor.DoctorId);
                command.Parameters.AddWithValue("@CRM", doctor.DoctorCRM);
                command.Parameters.AddWithValue("@Name", doctor.DoctorName);
                command.Parameters.AddWithValue("@SpecialtyId", doctor.DoctorSpecialty); // Ajuste conforme o tipo de SpecialtyId

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteDoctorAsync(Guid doctorId)
    {
        var query = "DELETE FROM Doctors WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", doctorId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}