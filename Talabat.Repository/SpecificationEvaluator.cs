using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> Spec)
        {
            var query = InputQuery;

            if (Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }

            if (Spec.OrderByAsc != null)
            {
                query = query.OrderBy(Spec.OrderByAsc);
            }
            else if (Spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(Spec.OrderByDesc);
            }

            if (Spec.IsPaginationEnabled)
            {
                query = query.Skip(Spec.Skip).Take(Spec.Take);
            }

                query = Spec.Includes.Aggregate(query, (currentQuery, IncludeExperation) => currentQuery.Include(IncludeExperation));

                return query;
            }
        }
    }

