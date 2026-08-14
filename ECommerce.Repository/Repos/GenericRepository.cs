using ECommerce.Core.Interfaces;
using ECommerce.Core.Models;
using ECommerce.Core.Specifications;
using ECommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

      
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
          
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
          
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
    }
}
