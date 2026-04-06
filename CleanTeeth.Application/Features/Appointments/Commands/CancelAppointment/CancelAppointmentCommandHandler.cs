using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CancelAppointmentCommand request)
    {
        var appointment = await _repository.GetById(request.Id);

        if (appointment == null)
        {
            throw new NotFoundException();
        }

        appointment.Cancel();

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