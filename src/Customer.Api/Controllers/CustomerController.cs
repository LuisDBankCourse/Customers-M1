using Customer.Application.Commands;
using Customer.Application.Query;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateCustomerCommand> _validator;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(IMediator mediator, IValidator<CreateCustomerCommand> validator,
        ILogger<CustomerController> logger)
    {
        _mediator = mediator;
        _validator = validator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(command, ct);

        if (!validation.IsValid)
        {
            var errors = validation.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g
                    .Select(x => x.ErrorMessage)
                    .ToArray());

            return ValidationProblem(new ValidationProblemDetails(errors)
            {
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return Problem(
                title: "Conflict",
                detail: result.Error,
                statusCode: StatusCodes.Status409Conflict

            );
        }

        return CreatedAtAction(nameof(GetByIdCustomer),
           new{id = result.Value}, result.Value );

    }
    
    [HttpGet("{Id}")]
    
    public async Task<ActionResult> GetByIdCustomer(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id), ct);

        if (!result.IsSuccess)
        {
            return Problem(
                title: "NotFound",
                detail: result.Error,
                statusCode: StatusCodes.Status404NotFound);
        }
        return Ok(result.Value);
    }
    
}
