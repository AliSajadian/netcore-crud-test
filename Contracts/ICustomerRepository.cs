using System.Xml.Schema;
using Entities.Models;

namespace Contracts;

public interface ICustomerRepository
{ 
    Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges);
    Task<Customer?> GetCustomerAsync(int customerId, bool trackChanges);
    Task<Customer?> GetCustomerAsync(int customerId);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Customer customer);
} 

