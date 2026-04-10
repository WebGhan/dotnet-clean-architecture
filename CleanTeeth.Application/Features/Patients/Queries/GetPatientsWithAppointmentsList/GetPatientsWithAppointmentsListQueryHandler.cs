using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Application.Utilities.Common;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsWithAppointmentsList;

public class GetPatientsWithAppointmentsListQueryHandler : IRequestHandler<GetPatientsWithAppointmentsListQuery, PagedResult<PatientWithAppointmentsListDto>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public GetPatientsWithAppointmentsListQueryHandler(
        IPatientRepository patientRepository,
        IAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<PagedResult<PatientWithAppointmentsListDto>> Handle(GetPatientsWithAppointmentsListQuery request)
    {
        // 获取分页患者列表
        var patients = await _patientRepository.GetFiltered(request);
        var totalCount = await _patientRepository.GetFilteredCount(request);

        // 如果没有患者，直接返回空结果
        if (!patients.Any())
        {
            return new PagedResult<PatientWithAppointmentsListDto>
            {
                Items = new List<PatientWithAppointmentsListDto>(),
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        // 批量获取所有患者的预约
        var patientIds = patients.Select(p => p.Id).ToList();
        var appointmentsByPatientId = await _appointmentRepository.GetByPatientIdsAsync(patientIds);

        // 组合患者和预约数据
        var patientsDto = patients.Select(patient =>
        {
            appointmentsByPatientId.TryGetValue(patient.Id, out var appointments);
            return patient.ToDtoWithAppointments(appointments ?? new List<Appointment>());
        }).ToList();

        return new PagedResult<PatientWithAppointmentsListDto>
        {
            Items = patientsDto,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}