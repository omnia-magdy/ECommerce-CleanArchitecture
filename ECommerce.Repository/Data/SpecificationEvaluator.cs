using ECommerce.Core.Specifications;
using ECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery; // بنبدأ بالـ DbContext.Set<TEntity>()

            // 1. ترجمة شرط الفلترة (Where)
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // p => p.Id == 5
            }

            // 2. ترجمة ربط الجداول (Includes)
            // بيمشي على كل Include اتضافت في القائمة ويطبقها على الـ query
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
