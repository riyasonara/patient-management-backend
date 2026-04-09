using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.DTOs;
using PatientManagement.Domain.Entities;
using PatientManagement.Infrastructure.Repositories;

namespace PatientManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationsController : ControllerBase
{
    private readonly ConsultationRepository _repo;

    public ConsultationsController(ConsultationRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateConsultationDto dto)
    {
        var consultation = new Consultation
        {
            PatientId = dto.PatientId,
            Diagnosis = dto.Diagnosis,
            Notes = dto.Notes,
            Prescription = dto.Prescription
        };

        await _repo.AddAsync(consultation);

        return Ok(consultation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repo.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var data = await _repo.GetByPatientIdAsync(patientId);
        return Ok(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateConsultationDto dto)
    {
        var consultation = await _repo.GetByIdAsync(id);

        if (consultation == null)
            return NotFound("Consultation not found");

        consultation.Diagnosis = dto.Diagnosis;
        consultation.Notes = dto.Notes;
        consultation.Prescription = dto.Prescription;

        await _repo.UpdateAsync(consultation);

        return Ok(consultation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var consultation = await _repo.GetByIdAsync(id);

        if (consultation == null)
            return NotFound("Consultation not found");

        await _repo.DeleteAsync(consultation);

        return Ok("Consultation deleted successfully");
    }
}