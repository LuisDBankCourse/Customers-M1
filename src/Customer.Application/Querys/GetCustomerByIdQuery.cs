using Customer.Application.Common;
using Customer.Domain.Enums;
using MediatR;

namespace Customer.Application.Querys;

public record GetCustomerByIdQuery(Guid CustomerId) : IRequest<Result<GetCustomerByIdResponse>>;

public record GetCustomerByIdResponse(
    Guid Id,
    string FullName,
    string DocumentNumber,
    string Email, 
    decimal MonthlyIncome,
    EmploymentTypes EmploymentType);