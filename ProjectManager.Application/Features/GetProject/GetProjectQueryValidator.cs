using FluentValidation;
using ServiceContracts.Project.Queries;

namespace ProjectManager.Application.Features.GetProject;

public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id is Required.")
            .NotNull();
        RuleFor(q => q.Id)
            .GreaterThanOrEqualTo(1).WithMessage("Id must be is number.");
    }
}