using Microsoft.AspNetCore.Mvc;
using HospitalCase.Models;
using HospitalCase.Services;
using System;
using System.Threading.Tasks;

public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return View(doctors);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DoctorCRM,DoctorName,DoctorSpecialty")] DoctorModel doctor)
    {
        if (ModelState.IsValid)
        {
            await _doctorService.AddDoctorAsync(doctor);
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("DoctorId,DoctorCRM,DoctorName,DoctorSpecialty")] DoctorModel doctor)
    {
        if (id != doctor.DoctorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _doctorService.UpdateDoctorAsync(doctor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}