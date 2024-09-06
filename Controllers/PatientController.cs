using Microsoft.AspNetCore.Mvc;
using HospitalCase.Models; 
public class PatientController : Controller
{
    private readonly PatientService _patientService;

    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }

    public IActionResult Index()
    {
        var patients = _patientService.GetAllPatients();
        return View(patients);
    }

    public IActionResult Details(int id)
    {
        var patient = _patientService.GetPatient;
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
    public IActionResult Create(PatientModel patient)
    {
        if (ModelState.IsValid)
        {
            _patientService.AddPatient(patient);
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }

    public IActionResult Edit(int id)
    {
        var patient = _patientService.GetPatient;
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Guid id, PatientModel patient)
    {
        if (id != patient.PatientId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _patientService.UpdatePatient(patient);
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }

    public IActionResult Delete(int id)
    {
        var patient = _patientService.GetPatient;
        if (patient == null)
        {
            return NotFound();
        }
        return View(patient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _patientService.DeletePatient(id);
        return RedirectToAction(nameof(Index));
    }
}