using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.DTOs;
using PatientManagement.Domain.Entities;
using PatientManagement.Infrastructure.Repositories;
using PatientManagement.Infrastructure.Services;

namespace PatientManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly PatientRepository _repo;
    private readonly AzureEventPublisher _eventPublisher;

    public PatientController(PatientRepository repo,AzureEventPublisher eventPublisher)
    {
        _repo = repo;
        _eventPublisher = eventPublisher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        await _repo.AddAsync(patient);

        await _eventPublisher.PublishAsync(new
        {
            patient.Id,
            patient.Name,
            patient.Email,
            patient.BloodGroup,
            patient.PhoneNumber,
            patient.Disease
        });

        return Ok(patient);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repo.GetAllAsync();
        return Ok(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePatientDto dto)
    {
        var patient = await _repo.GetByIdAsync(id);

        if (patient == null)
            return NotFound("Patient not found");

        // Update fields
        patient.Name = dto.Name;
        patient.Age = dto.Age;
        patient.Gender = dto.Gender;
        patient.Email = dto.Email;

        patient.Disease = dto.Disease;
        patient.BloodGroup = dto.BloodGroup;
        patient.PhoneNumber = dto.PhoneNumber;

        await _repo.UpdateAsync(patient);

        return Ok(patient);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _repo.GetByIdAsync(id);

        if (patient == null)
            return NotFound("Patient not found");

        await _repo.DeleteAsync(patient);

        return Ok("Patient deleted successfully");
    }
}
