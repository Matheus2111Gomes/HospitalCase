using Microsoft.AspNetCore.Mvc;
using HospitalCase.Models;
using System;
using System.Threading.Tasks;
using HospitalCase.Repositories.Patient;

public class PatientController : Controller
{
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public IActionResult Index()
    {
        var patients = _patientRepository.GetAll();
        return View(patients);
    }

    public IActionResult Details(Guid id)
    {
        var patient = _patientRepository.GetById(id);
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("PatientId,PatientName,PatientAddress,PatientTelephone,PatientDocument,PatientDateBirth")] PatientModel patient)
    {
        if (ModelState.IsValid)
        {
            patient.PatientId = Guid.NewGuid();
            _patientRepository.Add(patient);
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }

    public IActionResult Edit(Guid id)
    {
        var patient = _patientRepository.GetById(id);
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Guid id, [Bind("PatientId,PatientName,PatientAddress,PatientTelephone,PatientDocument,PatientDateBirth")] PatientModel patient)
    {
        if (id != patient.PatientId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _patientRepository.Update(patient);
            }
            catch (Exception ex)
            {
                if (_patientRepository.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }
    public IActionResult Delete(Guid id)
    {
        var patient = _patientRepository.GetById(id);
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
        var patient = _patientRepository.GetById(id);
        if (patient == null)
        {
            return NotFound();
        }

        _patientRepository.Delete(id);
        return RedirectToAction(nameof(Index)); 
    }


}