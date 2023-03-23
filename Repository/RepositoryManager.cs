using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager 
{ 
    private readonly RepositoryContext _repositoryContext; 
    private readonly Lazy<ICustomerRepository> _CustomerRepository; 

    public RepositoryManager(RepositoryContext repositoryContext)
    {
         _repositoryContext = repositoryContext;
         _CustomerRepository = new Lazy<ICustomerRepository>(() => 
            new CustomerRepository(repositoryContext));
    } 

    public ICustomerRepository Customer => _CustomerRepository.Value; 
    public void Save() => _repositoryContext.SaveChanges(); 
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}