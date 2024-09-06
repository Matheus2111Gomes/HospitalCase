using HospitalCase.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HospitalCase.Repositories.Patient
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;

        public PatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PatientModel GetById(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM Patients WHERE Id = @PatientId", connection))
                {
                    command.Parameters.AddWithValue("@PatientId", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PatientModel
                            {
                                PatientId = (Guid)reader["Id"],
                                PatientName = reader["Name"] as string,
                                PatientAddress = reader["Address"] as string,
                                PatientTelephone = reader["Telephone"] as string,
                                PatientDocument = reader["Document"] as string,
                                PatientDateBirth = reader["DateBirth"] as DateTime?
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
                using (var command = new SqlCommand("SELECT * FROM Patients", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new PatientModel
                            {
                                PatientId = (Guid)reader["Id"],
                                PatientName = reader["Name"] as string,
                                PatientAddress = reader["Address"] as string,
                                PatientTelephone = reader["Telephone"] as string,
                                PatientDocument = reader["Document"] as string,
                                PatientDateBirth = reader["DateBirth"] as DateTime?
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
                using (var command = new SqlCommand("INSERT INTO Patients (Id, Name, Address, Telephone, Document, DateBirth) VALUES (@PatientId, @PatientName, @PatientAddress, @PatientTelephone, @PatientDocument, @PatientDateBirth)", connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patient.PatientId);
                    command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                    command.Parameters.AddWithValue("@PatientAddress", patient.PatientAddress);
                    command.Parameters.AddWithValue("@PatientTelephone", patient.PatientTelephone);
                    command.Parameters.AddWithValue("@PatientDocument", patient.PatientDocument);
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
                using (var command = new SqlCommand("UPDATE Patients SET Name = @PatientName, Address = @PatientAddress, Telephone = @PatientTelephone, Document = @PatientDocument, DateBirth = @PatientDateBirth WHERE Id = @PatientId", connection))
                {
                    command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                    command.Parameters.AddWithValue("@PatientAddress", patient.PatientAddress);
                    command.Parameters.AddWithValue("@PatientTelephone", patient.PatientTelephone);
                    command.Parameters.AddWithValue("@PatientDocument", patient.PatientDocument);
                    command.Parameters.AddWithValue("@PatientDateBirth", patient.PatientDateBirth);
                    command.Parameters.AddWithValue("@PatientId", patient.PatientId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("DELETE FROM Patients WHERE Id = @PatientId", connection))
                {
                    command.Parameters.AddWithValue("@PatientId", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}