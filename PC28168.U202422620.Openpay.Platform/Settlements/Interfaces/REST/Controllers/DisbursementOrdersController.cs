using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PC28168.U202422620.Openpay.Platform.Settlements.Application.Handlers;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Transform;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/disbursement-orders")]
public sealed class DisbursementOrdersController(
    CreateDisbursementOrderCommandHandler handler,
    DisbursementOrderResourceAssembler assembler) : ControllerBase
{
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [EndpointSummary("Create a disbursement order")]
    [EndpointDescription("Registers a disbursement order and returns the stored resource with its generated identifier and last update time.")]
    [ProducesResponseType(typeof(DisbursementOrderResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DisbursementOrderResource>> Create(
        [FromBody] CreateDisbursementOrderResource resource,
        CancellationToken cancellationToken)
    {
        var command = assembler.ToCommand(resource);
        var order = await handler.HandleAsync(command, cancellationToken);
        var response = assembler.ToResource(order);
        return StatusCode(StatusCodes.Status201Created, response);
    }
}
