namespace PatientManagement.Application.DTOs;

public class UpdatePatientDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }

    public string Disease { get; set; }
    public string BloodGroup { get; set; }
    public string PhoneNumber { get; set; }
}