namespace PatientManagement.Application.DTOs;

public class CreateConsultationDto
{
    public int PatientId { get; set; }
    public string Diagnosis { get; set; }
    public string Notes { get; set; }
    public string Prescription { get; set; }
}
