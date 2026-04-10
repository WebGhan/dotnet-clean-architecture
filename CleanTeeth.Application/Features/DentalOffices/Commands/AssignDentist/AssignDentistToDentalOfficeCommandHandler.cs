using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.AssignDentist;

public class AssignDentistToDentalOfficeCommandHandler : IRequestHandler<AssignDentistToDentalOfficeCommand>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IDentistRepository _dentistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignDentistToDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository,
        IDentistRepository dentistRepository,
        IUnitOfWork unitOfWork)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _dentistRepository = dentistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AssignDentistToDentalOfficeCommand request)
    {
        var dentalOffice = await _dentalOfficeRepository.GetByIdWithAssignments(request.DentalOfficeId);
        if (dentalOffice is null)
        {
            throw new NotFoundException();
        }

        var dentist = await _dentistRepository.GetById(request.DentistId);
        if (dentist is null)
        {
            throw new NotFoundException();
        }

        try
        {
            dentalOffice.AssignDentist(request.DentistId);
            await _dentalOfficeRepository.Update(dentalOffice);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            throw;
        }
    }
}