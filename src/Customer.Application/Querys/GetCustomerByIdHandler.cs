using Customer.Application.Common;
using Customer.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Application.Querys;

public class GetCustomerByIdHandler :  IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger _logger;

    public GetCustomerByIdHandler(ICustomerRepository customerRepository, ILogger<GetCustomerByIdHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken ct)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
        {
            _logger.LogWarning("Customer with CustomerId={CustomerId} not found",
                request.CustomerId);
            return Result<GetCustomerByIdResponse>.Failure($"Customer with CustomerId={request.CustomerId} doesn't exists");
        }

        return Result<GetCustomerByIdResponse>.Success(new GetCustomerByIdResponse(
            customer.Id,
            customer.FullName,
            customer.DocumentNumber,
            customer.Email,
            customer.MonthlyIncome,
            customer.EmploymentType
            ));
    }
}