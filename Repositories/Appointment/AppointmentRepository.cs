using Microsoft.Data.SqlClient;
using HospitalCase.Models;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;

    public AppointmentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<AppointmentModel>> GetAllAppointmentsAsync()
    {
        var appointments = new List<AppointmentModel>();
        var query = "SELECT Id, AppointmentDate, PatientId, DoctorId, AppointmentInfos FROM Appointments";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appointments.Add(new AppointmentModel
                        {
                            AppointmentId = reader.GetGuid(0),
                            AppointmentDate = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                            PatientId = reader.GetGuid(2),
                            DoctorId = reader.GetGuid(3),
                            AppointmentInfos = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                }
            }
        }

        return appointments;
    }

    public async Task<AppointmentModel> GetAppointmentByIdAsync(Guid id)
    {
        AppointmentModel appointment = null;
        var query = "SELECT Id, AppointmentDate, PatientId, DoctorId, AppointmentInfos FROM Appointments WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        appointment = new AppointmentModel
                        {
                            AppointmentId = reader.GetGuid(0),
                            AppointmentDate = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                            PatientId = reader.GetGuid(2),
                            DoctorId = reader.GetGuid(3),
                            AppointmentInfos = reader.IsDBNull(4) ? null : reader.GetString(4)
                        };
                    }
                }
            }
        }

        return appointment;
    }

    public async Task AddAppointmentAsync(AppointmentModel appointment)
    {
        var query = "INSERT INTO Appointments (Id, AppointmentDate, PatientId, DoctorId, AppointmentInfos) VALUES (@Id, @AppointmentDate, @PatientId, @DoctorId, @AppointmentInfos)";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", appointment.AppointmentId);
                command.Parameters.AddWithValue("@AppointmentDate", (object)appointment.AppointmentDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@AppointmentInfos", (object)appointment.AppointmentInfos ?? DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAppointmentAsync(AppointmentModel appointment)
    {
        var query = "UPDATE Appointments SET AppointmentDate = @AppointmentDate, PatientId = @PatientId, DoctorId = @DoctorId, AppointmentInfos = @AppointmentInfos WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", appointment.AppointmentId);
                command.Parameters.AddWithValue("@AppointmentDate", (object)appointment.AppointmentDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@AppointmentInfos", (object)appointment.AppointmentInfos ?? DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAppointmentAsync(Guid id)
    {
        var query = "DELETE FROM Appointments WHERE Id = @Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}