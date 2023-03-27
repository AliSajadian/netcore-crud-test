using Microsoft.EntityFrameworkCore;

using Entities.Models;
using Contracts;

namespace Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository 
{ 
    public CustomerRepository(RepositoryContext repositoryContext) : 
        base(repositoryContext) 
    { 
    } 

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges) => 
        await FindAll(trackChanges)
              .OrderBy(c => c.LastName)
              .ThenBy(c => c.FirstName) 
              .ToListAsync(); 

    public async Task<Customer?> GetCustomerAsync(int CustomerId, bool trackChanges) => 
        await FindByCondition(c => c.Id.Equals(CustomerId), trackChanges)
              .SingleOrDefaultAsync(); 

    public async Task<Customer?> GetCustomerAsync(int CustomerId) => 
        await FindByCondition(c => c.Id.Equals(CustomerId), false)
              .SingleOrDefaultAsync(); 
        
    public async Task<Customer> CreateCustomerAsync(Customer Customer) => await Create(Customer);

    public async Task UpdateCustomerAsync(Customer Customer) => await Update(Customer);

    public async Task DeleteCustomerAsync(Customer Customer) => await Delete(Customer);
}