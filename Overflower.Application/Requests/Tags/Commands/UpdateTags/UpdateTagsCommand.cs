using MediatR;
using Overflower.Application.Requests.Tags.Queries;

namespace Overflower.Application.Requests.Tags.Commands.UpdateTags;

public class UpdateTagsCommand : IRequest<TagDto[]>;
