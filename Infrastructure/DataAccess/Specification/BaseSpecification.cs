using System.Linq.Expressions;
using Domain.Repository;

namespace Infrastructure.DataAccess.Specification;

public class BaseSpecification <T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> specification)
    {
        Criteria = specification;
    }

    public BaseSpecification()
    {
        
    }
    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDesc { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
    
    protected void AddOrderBy(Expression<Func<T, object>> orderExpression) => OrderBy = orderExpression;
    protected void AddOrderByDesc(Expression<Func<T, object>> orderDescExpression) => OrderByDesc = orderDescExpression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}