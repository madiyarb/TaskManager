using FluentValidation;
using ServiceContracts.Task.Commands;

namespace TaskManager.Application.Features.Create;

public class CreateCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty().WithMessage("Name is Required.")
            .NotNull();
        RuleFor(q => q.Description)
            .NotEmpty().WithMessage("Description is Required.")
            .NotNull();
        RuleFor(q => q.ProjectId)
            .NotEmpty().WithMessage("ProjectId is Required.")
            .NotNull();
        RuleFor(q => q.Priority)
            .NotEmpty().WithMessage("Priority is Required.")
            .NotNull();
    }
}