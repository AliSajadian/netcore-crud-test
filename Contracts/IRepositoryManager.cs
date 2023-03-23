namespace Contracts ;
public interface IRepositoryManager 
{ 
    ICustomerRepository Customer { get; }
    void Save();
    Task SaveAsync(); 
}