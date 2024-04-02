using MediatR;
using Microsoft.AspNetCore.Mvc;
using Overflower.Application.Paging;
using Overflower.Application.Requests.Tags.Commands.UpdateTags;
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
    [ProducesResponseType(typeof(PageResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<PageResult>> GetAll([FromQuery] GetAllTagsQuery request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(request, cancellationToken);
        return result;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update(UpdateTagsCommand request,
        CancellationToken cancellationToken) {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}
