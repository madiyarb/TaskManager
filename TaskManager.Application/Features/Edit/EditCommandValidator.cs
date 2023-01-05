using FluentValidation;
using ServiceContracts.Task.Commands;

namespace TaskManager.Application.Features.Edit;

public class EditCommandValidator : AbstractValidator<EditTaskCommand>
{
    public EditCommandValidator()
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