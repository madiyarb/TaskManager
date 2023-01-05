using FluentValidation;
using ServiceContracts.Task.Queries;

namespace TaskManager.Application.Features.GetAll;

public class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
{
    public GetAllTasksQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .NotEmpty().WithMessage("PageNumber is Required.")
            .NotNull();
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be is number.");
        RuleFor(q => q.PageSize)
            .NotEmpty().WithMessage("PageSize is Required.")
            .NotNull();
        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize must be is number.");
    }
}