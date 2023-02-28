using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccessLayer.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly CIDbContext _dbContext;
    internal DbSet<T> dbSet;
    public Repository(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet= _dbContext.Set<T>();
    }
    public void Add(T entity)
    {
        Console.WriteLine("Adding Entity DB");
        dbSet.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet.Where(filter);
        return query.FirstOrDefault();
    }
}
