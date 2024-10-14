using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>>? _criteria;

        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria => _criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExperssion)
        {
            OrderBy = orderByExperssion;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }


    }
}