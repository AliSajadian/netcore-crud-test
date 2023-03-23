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
    public async Task<Customer?> GetCustomerAsync(Guid CustomerId, bool trackChanges) => 
        await FindByCondition(c => c.Id.Equals(CustomerId), trackChanges)
              .SingleOrDefaultAsync(); 
        
    public async Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => 
        await FindByCondition(x => ids.Contains(x.Id), trackChanges) 
              .ToListAsync();
    
    public IEnumerable<Customer> GetAllCustomers(bool trackChanges) => 
        FindAll(trackChanges).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();

    public IEnumerable<Customer> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => 
        FindByCondition(x => ids.Contains(x.Id), trackChanges) .ToList();

    public Customer? GetCustomer(Guid CustomerId, bool trackChanges) => 
        FindByCondition(c => c.Id.Equals(CustomerId), trackChanges)
        .SingleOrDefault();

    public void CreateCustomer(Customer Customer) => Create(Customer);

    public void DeleteCustomer(Customer Customer) => Delete(Customer);
}