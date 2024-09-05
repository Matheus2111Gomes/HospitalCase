using HospitalCase.Repositories.Patient;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using HospitalCase.Models;
public class PatientRepository : IPatientRepository
{
    private readonly string _connectionString;

    public PatientRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public PatientModel GetById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SELECT * FROM Patient WHERE Id = @PatientId", connection))
            {
                command.Parameters.AddWithValue("@PatientId", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PatientModel
                        {
                            PatientId = (int)reader["PatientId"],
                            PatientName = (string)reader["PatientName"],
                            PatientDateBirth = (DateTime)reader["PatientDateBirth"]
                        };
                    }
                }
            }
        }
        return null;
    }

    public IEnumerable<PatientModel> GetAll()
    {
        var patients = new List<PatientModel>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SELECT * FROM Patient", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new PatientModel
                        {
                            PatientId = (int)reader["Id"],
                            PatientName = (string)reader["Name"],
                            PatientDateBirth = (DateTime)reader["DateBirth"]
                        });
                    }
                }
            }
        }
        return patients;
    }

    public void Add(PatientModel patient)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("INSERT INTO Patient (Name, DatefBirth) VALUES (@PatientName, @PatientDateBirth)", connection))
            {
                command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                command.Parameters.AddWithValue("@PatientDateBirth", patient.PatientDateBirth);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(PatientModel patient)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("UPDATE Patient SET Name = @PatientName, DateBirth = @PatientDateBirth WHERE Id = @PatientId", connection))
            {
                command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                command.Parameters.AddWithValue("@PatientDateBirth", patient.PatientDateBirth);
                command.Parameters.AddWithValue("@PatientId", patient.PatientId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("DELETE FROM Patient WHERE Id = @PatientId", connection))
            {
                command.Parameters.AddWithValue("@PatientId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}