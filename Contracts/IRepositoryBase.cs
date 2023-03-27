using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Contracts;

public interface IRepositoryBase<T> 
{ 
    IQueryable<T> FindAll(bool trackChanges); 
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); 
    Task<T> Create(T entity); 
    Task Update(T entity); 
    Task Delete(T entity);
}