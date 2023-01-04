using FluentValidation;
using ServiceContracts.Project.Commands;

namespace ProjectManager.Application.Features.Create;

public class CreateCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty().WithMessage("Name is Required.")
            .NotNull();
        RuleFor(q => q.Description)
            .NotEmpty().WithMessage("Description is Required.")
            .NotNull();
    }
}