using Customer.Domain.Repositories;
using Customer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustomerRepository(CustomerDbContext customerDbContext)
        {
                _customerDbContext = customerDbContext;
        }

        public async Task AddAsync(Domain.Entities.Customer customer, CancellationToken ct = default)
        => await _customerDbContext.Customers.AddAsync(customer, ct);

        public Task<bool> ExistsByDocumentNumberAsync(string documentNumber, CancellationToken ct = default)
        => _customerDbContext.Customers.AnyAsync(x => x.DocumentNumber == documentNumber);

        public Task<Domain.Entities.Customer?> GetByIdAsync(Guid Id, CancellationToken ct = default)
        => _customerDbContext.Customers.SingleOrDefaultAsync(x => x.Id == Id);

        public Task SaveChangesAsync(CancellationToken ct = default)
        => _customerDbContext.SaveChangesAsync(ct);
    }
}
