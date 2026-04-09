using Microsoft.EntityFrameworkCore;
using PatientManagement.Domain.Entities;
using PatientManagement.Infrastructure.Persistence;

namespace PatientManagement.Infrastructure.Repositories;

public class ConsultationRepository
{
    private readonly ApplicationDbContext _context;

    public ConsultationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Consultation consultation)
    {
        _context.Consultations.Add(consultation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Consultation>> GetAllAsync()
    {
        return await _context.Consultations.ToListAsync();
    }

    public async Task<List<Consultation>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Consultations
            .Where(c => c.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<Consultation?> GetByIdAsync(int id)
    {
        return await _context.Consultations.FindAsync(id);
    }

    public async Task UpdateAsync(Consultation consultation)
    {
        _context.Consultations.Update(consultation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Consultation consultation)
    {
        _context.Consultations.Remove(consultation);
        await _context.SaveChangesAsync();
    }
}