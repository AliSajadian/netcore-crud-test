namespace Contracts ;
public interface IUnitOfWork 
{ 
    ICustomerRepository Customer { get; }
    void Save();
    Task SaveAsync(); 
}