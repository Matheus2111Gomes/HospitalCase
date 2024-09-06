using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalCase.Models;

public interface IAppointmentRepository
{
    Task<IEnumerable<AppointmentModel>> GetAllAppointmentsAsync();
    Task<AppointmentModel> GetAppointmentByIdAsync(Guid id);
    Task AddAppointmentAsync(AppointmentModel appointment);
    Task UpdateAppointmentAsync(AppointmentModel appointment);
    Task DeleteAppointmentAsync(Guid id);
}