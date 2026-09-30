using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách toàn bộ nhân viên
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetEmployeesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin chi tiết 1 nhân viên theo ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (result == null) return NotFound($"Không tìm thấy nhân viên có Mã ID = {id}");
        return Ok(result);
    }

    /// <summary>
    /// Thêm mới một nhân viên
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var result = await _mediator.Send(new CreateEmployeeCommand(dto));
        return Ok(result);
    }

    /// <summary>
    /// Cập nhật thông tin nhân viên theo ID
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var success = await _mediator.Send(new UpdateEmployeeCommand(id, dto));
        if (!success) return NotFound($"Không tìm thấy nhân viên có Mã ID = {id}");
        return NoContent();
    }

    /// <summary>
    /// Xóa nhân viên theo ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteEmployeeCommand(id));
        if (!success) return NotFound($"Không tìm thấy nhân viên có Mã ID = {id}");
        return NoContent();
    }
}