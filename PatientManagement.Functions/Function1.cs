using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatientManagement.Functions
{
    public class PatientCreatedFunction
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public PatientCreatedFunction(ILoggerFactory loggerFactory, IConfiguration config)
        {
            _logger = loggerFactory.CreateLogger<PatientCreatedFunction>();
            _config = config;
        }

        [Function("PatientCreatedFunction")]
        public async Task Run(
            [ServiceBusTrigger("patient-created-queue", Connection = "ServiceBusConnection")]
            string message)
        {
            _logger.LogInformation("🔥 Message received!");

            var patient = JsonSerializer.Deserialize<PatientDto>(message);

            await SendEmail(patient);
        }

        private async Task SendEmail(PatientDto patient)
        {
            var apiKey = _config["SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("riyasonara0908@gmail.com", "Patient System"));
            msg.AddTo(new EmailAddress(patient.Email, patient.Name));

            msg.SetTemplateId("d-935bd76c133d4503b56cabbc5e897b08");

            msg.SetTemplateData(new
            {
                name = patient.Name,
                email = patient.Email,
                disease = patient.Disease,
                bloodGroup = patient.BloodGroup,
                phoneNumber = patient.PhoneNumber
            });

            var response = await client.SendEmailAsync(msg);

            _logger.LogInformation($"📧 Email sent to {patient.Email} with status {response.StatusCode}");
        }
    }
}

public class PatientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string Disease { get; set; }
    public string BloodGroup { get; set; }
    public string PhoneNumber { get; set; }
}