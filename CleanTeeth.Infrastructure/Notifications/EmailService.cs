using System.Globalization;
using System.Net;
using System.Net.Mail;
using CleanTeeth.Application.Notifications;
using Microsoft.Extensions.Configuration;

namespace CleanTeeth.Infrastructure.Notifications;

public class EmailService : INotifications
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAppointmentConfirmation(AppointmentConfirmationDto appointmentConfirmationDto)
    {
        var subject = "Appointment Confirmation - Clean Teeth";

        var body = $"""
                    Dear, {appointmentConfirmationDto.Patient},

                    Your appointment with Dr. {appointmentConfirmationDto.Dentist} has been scheduled for {appointmentConfirmationDto.Date.ToString("f", new CultureInfo("es-DO"))} in the office {appointmentConfirmationDto.DentalOffice}. 

                    We will be waiting for you.

                    Clean Teeth team
                    """;

        await SendEmail(appointmentConfirmationDto.PatientEmail, subject, body);
    }

    public async Task SendAppointmentReminder(AppointmentReminderDto appointmentReminderDto)
    {
        var subject = "Appointment Confirmation - Clean Teeth";

        var body = $"""
                    Dear, {appointmentReminderDto.Patient},

                    Your appointment with Dr. {appointmentReminderDto.Dentist} has been scheduled for {appointmentReminderDto.Date.ToString("f", new CultureInfo("es-DO"))} in the office {appointmentReminderDto.DentalOffice}. 

                    We will be waiting for you.

                    Clean Teeth team
                    """;
        
        await SendEmail(appointmentReminderDto.PatientEmail, subject, body);
    }

    private async Task SendEmail(string to, string subject, string body)
    {
        var from = _configuration.GetValue<string>("EMAIL_CONFIGURATIONS:EMAIL");
        var password = _configuration.GetValue<string>("EMAIL_CONFIGURATIONS:PASSWORD");
        var host = _configuration.GetValue<string>("EMAIL_CONFIGURATIONS:HOST");
        var port = _configuration.GetValue<int>("EMAIL_CONFIGURATIONS:PORT");

        var smtpClient = new SmtpClient(host, port);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(from, password);

        var message = new MailMessage(from!, to, subject, body);
        await smtpClient.SendMailAsync(message);
    }
}