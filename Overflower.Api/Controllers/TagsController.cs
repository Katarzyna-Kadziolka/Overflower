using MediatR;
using Microsoft.AspNetCore.Mvc;
using Overflower.Application.Requests.Tags.Queries;
using Overflower.Application.Requests.Tags.Queries.GetAllTags;

namespace Overflower.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase {
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(TagDto[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<TagDto[]>> GetAll([FromQuery] GetAllTagsQuery request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(request, cancellationToken);
        return result;
    }

    [HttpPut]
    [ProducesResponseType(typeof(TagDto[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<TagDto[]>> Update([FromQuery] GetAllTagsQuery request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(request, cancellationToken);
        return result;
    }
}
