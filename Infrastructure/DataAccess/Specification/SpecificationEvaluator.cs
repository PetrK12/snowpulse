using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Specification;

public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        var query = inputQuery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        query = specification.Includes.Aggregate(query, (current, include)
            => current.Include(include));
        return query;
    }
}