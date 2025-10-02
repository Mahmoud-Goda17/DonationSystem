using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Presistance.Repository
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly DonationDbContext _context;
        private Dictionary<string, object> _repositories;
        public UnitOfWork(DonationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;//convert to string
            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<T>)_repositories[type];
            }
            var repository = new GenericRepository<T>(_context);
            _repositories.Add(type, repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
