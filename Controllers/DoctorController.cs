using Microsoft.AspNetCore.Mvc;
using HospitalCase.Models;
using System;
using System.Threading.Tasks;

public class DoctorController : Controller
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    // GET: Doctor
    public async Task<IActionResult> Index()
    {
        var doctors = await _doctorRepository.GetAllDoctorsAsync();
        return View(doctors);
    }

    // GET: Doctor/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    // GET: Doctor/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Doctor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DoctorId,DoctorCRM,DoctorName,DoctorSpecialty")] DoctorModel doctor)
    {
        if (ModelState.IsValid)
        {
            doctor.DoctorId = Guid.NewGuid(); // Gerar um novo GUID para o novo médico
            await _doctorRepository.AddDoctorAsync(doctor);
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }

    // GET: Doctor/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    // POST: Doctor/Edit/5
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
                await _doctorRepository.UpdateDoctorAsync(doctor);
            }
            catch (Exception ex) // Ajuste para capturar exceções específicas de ADO.NET
            {
                if (await _doctorRepository.GetDoctorByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    // Você pode adicionar um log aqui para capturar o erro específico
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }

    // GET: Doctor/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    // POST: Doctor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _doctorRepository.DeleteDoctorAsync(id);
        return RedirectToAction(nameof(Index));
    }
}