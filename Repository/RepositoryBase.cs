using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Contracts;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> 
    where T : class 
{ 
    protected RepositoryContext RepositoryContext; 
    public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext; 
    public IQueryable<T> FindAll(bool trackChanges) => 
        !trackChanges ? 
            RepositoryContext.Set<T>() 
                .AsNoTracking() : 
            RepositoryContext.Set<T>(); 

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, 
        bool trackChanges) => 
            !trackChanges ? 
                RepositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() : 
                RepositoryContext.Set<T>() 
                    .Where(expression); 
    public async Task<T> Create(T entity) 
    {
        await RepositoryContext.Set<T>().AddAsync(entity); 
        return entity;
    } 
        
    public async Task Update(T entity) => RepositoryContext.Entry(entity).State = EntityState.Modified;
    
    public async Task Delete(T entity) => RepositoryContext.Set<T>().Remove(entity); 
}