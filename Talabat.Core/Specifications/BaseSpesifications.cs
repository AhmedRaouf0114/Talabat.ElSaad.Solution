using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpesifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderByAsc { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get ; set; }
        public bool IsPaginationEnabled { get; set ; }

        public BaseSpesifications()
        {
            
        }

        public BaseSpesifications(Expression<Func<T, bool>> criteriaExpretion)
        {
            Criteria = criteriaExpretion;
        }

        public void AddOrderByAsc(Expression<Func<T, object>> orderbyAscExpression)
        {
            OrderByAsc = orderbyAscExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderbydescExpression)
        {
            OrderByDesc = orderbydescExpression;
        }

        public void ApplayPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;    
            Take = take;
        }

    }
}
