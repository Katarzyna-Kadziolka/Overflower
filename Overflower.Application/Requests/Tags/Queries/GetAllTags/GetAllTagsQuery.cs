using MediatR;

namespace Overflower.Application.Requests.Tags.Queries.GetAllTags;

public class GetAllTagsQuery : IRequest<TagDto[]>;
