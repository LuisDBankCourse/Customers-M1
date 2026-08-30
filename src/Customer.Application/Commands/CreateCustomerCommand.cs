using Customer.Application.Common;
using Customer.Domain.Enums;
using MediatR;

namespace Customer.Application.Commands
{
    public record CreateCustomerCommand(
     string FullName,
     string DocumentNumber,
     string Email,
     decimal MonthlyIncome,
     EmploymentTypes EmploymentType
        ): IRequest<Result<CreateCustomerResponse>>;


    public record CreateCustomerResponse(
    Guid CustomerId,
    string Message

       );
}
