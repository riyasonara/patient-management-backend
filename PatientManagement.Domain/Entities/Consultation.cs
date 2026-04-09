namespace PatientManagement.Domain.Entities;

public class Consultation
{
    public int Id { get; set; }
    public int PatientId { get; set; }

    public string Diagnosis { get; set; }
    public string Notes { get; set; }
    public string Prescription { get; set; }

    public DateTime ConsultationDate { get; set; } = DateTime.UtcNow;
}