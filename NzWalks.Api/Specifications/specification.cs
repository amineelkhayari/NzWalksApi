using System.Linq.Expressions;

namespace NzWalks.Api.Specifications
{
    public abstract class specification<TEntity>
        
    {
        public Expression<Func<TEntity, bool>> Criteria { get;}
        public List<Expression<Func<TEntity, object>>> includesExpressions { get; } = new();
        public Expression<Func<TEntity, bool>> orderBy { get; }

    }
}
