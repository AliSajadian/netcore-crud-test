using System.Xml.Schema;
using Entities.Models;

namespace Contracts;

public interface ICustomerRepository
{ 
    Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges);
    IEnumerable<Customer> GetAllCustomers(bool trackChanges);
    Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    IEnumerable<Customer> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    Task<Customer?> GetCustomerAsync(Guid customerId, bool trackChanges);
    Customer? GetCustomer(Guid customerId, bool trackChanges);
    void CreateCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
} 

