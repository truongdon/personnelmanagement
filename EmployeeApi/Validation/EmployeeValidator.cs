using FluentValidation;

namespace EmployeeApi;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee.Name)
            .NotEmpty().WithMessage("Tên nhân viên không được để trống.");

        RuleFor(x => x.Employee.Age)
            .InclusiveBetween(18, 100).WithMessage("Tuổi phải nằm trong khoảng từ 18 đến 100.");

        RuleFor(x => x.Employee.Email)
            .NotEmpty().WithMessage("Địa chỉ email không được để trống.")
            .EmailAddress().WithMessage("Địa chỉ email không đúng định dạng.");

        RuleFor(x => x.Employee.Salary)
            .GreaterThan(0).WithMessage("Mức lương phải lớn hơn 0.");
    }
}

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee.Name)
            .NotEmpty().WithMessage("Tên nhân viên không được để trống.");

        RuleFor(x => x.Employee.Age)
            .InclusiveBetween(18, 100).WithMessage("Tuổi phải nằm trong khoảng từ 18 đến 100.");

        RuleFor(x => x.Employee.Email)
            .NotEmpty().WithMessage("Địa chỉ email không được để trống.")
            .EmailAddress().WithMessage("Địa chỉ email không đúng định dạng.");

        RuleFor(x => x.Employee.Salary)
            .GreaterThan(0).WithMessage("Mức lương phải lớn hơn 0.");
    }
}