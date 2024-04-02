using FluentValidation;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQueryValidator : AbstractValidator<GetAllTagsQuery> {
    public GetAllTagsQueryValidator() {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(1_000);
    }
}
