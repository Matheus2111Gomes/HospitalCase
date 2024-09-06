using Microsoft.AspNetCore.Mvc;
using HospitalCase.Models;
using System;
using System.Threading.Tasks;

public class AppointmentController : Controller
{
    private readonly IAppointmentRepository _repository;

    public AppointmentController(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var Appointment = await _repository.GetAllAppointmentsAsync();
        return View(Appointment);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var appointment = await _repository.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return View(appointment);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,PatientId,DoctorId,AppointmentInfos")] AppointmentModel appointment)
    {
        if (ModelState.IsValid)
        {
            appointment.AppointmentId = Guid.NewGuid(); // Generate a new GUID for the new appointment
            await _repository.AddAppointmentAsync(appointment);
            return RedirectToAction(nameof(Index));
        }
        return View(appointment);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var appointment = await _repository.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("AppointmentId,AppointmentDate,PatientId,DoctorId,AppointmentInfos")] AppointmentModel appointment)
    {
        if (id != appointment.AppointmentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _repository.UpdateAppointmentAsync(appointment);
            return RedirectToAction(nameof(Index));
        }
        return View(appointment);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var appointment = await _repository.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return View(appointment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _repository.DeleteAppointmentAsync(id);
        return RedirectToAction(nameof(Index));
    }
}