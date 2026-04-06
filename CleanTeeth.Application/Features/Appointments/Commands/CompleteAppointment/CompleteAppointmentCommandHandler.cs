using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Commands.CompleteAppointment;

public class CompleteAppointmentCommandHandler : IRequestHandler<CompleteAppointmentCommand>
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CompleteAppointmentCommand request)
    {
        var appointment = await _repository.GetById(request.Id);

        if (appointment is null)
        {
            throw new NotFoundException();
        }

        appointment.Complete();

        try
        {
            await _repository.Update(appointment);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}