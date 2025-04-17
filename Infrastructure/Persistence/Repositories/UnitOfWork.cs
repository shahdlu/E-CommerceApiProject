using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var typeName = typeof(TEntity).Name;
            //Dic<string, object> => string key [NAme of Type] -- object object from generic repository
            //if (_repositories.ContainsKey(typeName) )
            //{
            //    return (IGenericRepository<TEntity, TKey>) _repositories[typeName];
            //}
            if (_repositories.TryGetValue(typeName, out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                //create object
                var repo = new GenericRepository<TEntity,TKey>(_dbContext);
                //store object in dictionary
                //_repositories.Add(typeName, repo);
                _repositories["typeName"] = repo;
                //return object
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
