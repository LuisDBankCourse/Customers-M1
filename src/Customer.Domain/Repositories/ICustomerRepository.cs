namespace Customer.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Entities.Customer?> GetByIdAsync(Guid Id, CancellationToken ct = default);
        Task<bool> ExistsByDocumentNumberAsync(string documentNumber, CancellationToken ct = default);
        Task AddAsync(Entities.Customer customer, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);

    }
}
