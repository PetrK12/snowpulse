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

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}