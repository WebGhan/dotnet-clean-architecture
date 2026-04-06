using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Notifications;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entites;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotifications _notifications;

    public CreateAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork,
        INotifications notifications)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _notifications = notifications;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request)
    {
        var existsOverlap = await _repository.OverlapExists(request.DentistId, request.StartDate, request.EndDate);

        if (existsOverlap)
        {
            throw new CustomValidationException("The dentist have an appointment that overlap");
        }

        var timeInterval = new TimeInterval(request.StartDate, request.EndDate);
        var appointment = new Appointment(request.PatientId, request.DentistId, request.DentalOfficeId, timeInterval);

        Guid? id = null;

        try
        {
            var result = await _repository.Add(appointment);
            await _unitOfWork.Commit();
            id = result.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }

        var appointmentDb = await _repository.GetById(id.Value);
        var notificationDb = appointmentDb!.ToDto();
        await _notifications.SendAppointmentConfirmation(notificationDb);
        return id.Value;
    }
}