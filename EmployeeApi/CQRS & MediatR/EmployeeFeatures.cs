using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi;

// --- GET ALL ---
public record GetEmployeesQuery() : IRequest<List<EmployeeDto>>;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _context.Employees.ToListAsync(cancellationToken);
        return _mapper.Map<List<EmployeeDto>>(employees);
    }
}

// --- GET BY ID ---
public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
        return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
    }
}

// --- CREATE ---
public record CreateEmployeeCommand(CreateEmployeeDto Employee) : IRequest<EmployeeDto>;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request.Employee);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<EmployeeDto>(employee);
    }
}

// --- UPDATE ---
public record UpdateEmployeeCommand(int Id, UpdateEmployeeDto Employee) : IRequest<bool>;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
        if (employee == null) return false;

        _mapper.Map(request.Employee, employee);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

// --- DELETE ---
public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}