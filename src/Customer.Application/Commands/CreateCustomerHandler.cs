using Customer.Application.Common;
using Customer.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Customer.Application.Commands;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CreateCustomerHandler> _logger;

    public CreateCustomerHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Starting to create customer DocumentNumber={DocumentNumber} Email={DocumentEmail}", request.DocumentNumber, request.Email);
        
        var exists = await _customerRepository.ExistsByDocumentNumberAsync(request.DocumentNumber);

        if (exists)
        {
            _logger.LogWarning("Customer with DocumentNumber={DocumentNumber} already exists",
                request.DocumentNumber);
            return Result<CreateCustomerResponse>.Failure($"Customer with DocumentNumber={request.DocumentNumber} already exists");
        }

        var customer = Domain.Entities.Customer.Create(
            request.FullName,
            request.DocumentNumber,
            request.Email,
            request.MonthlyIncome,
            request.EmploymentType
            );
        
        await _customerRepository.AddAsync(customer, ct);
        await _customerRepository.SaveChangesAsync(ct);
        
        _logger.LogInformation("Customer with DocumentNumber={DocumentNumber} and Id={Id} Created", customer.DocumentNumber,  customer.Id);

        return Result<CreateCustomerResponse>.Success(new CreateCustomerResponse(customer.Id,"Customer Created successfully"));
    }

}