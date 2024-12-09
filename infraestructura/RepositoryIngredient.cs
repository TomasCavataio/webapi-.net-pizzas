using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;


namespace webapi.infraestructura
{
    
    [Injectable]
    public class RepositoryIngredient : IRepository<Ingredient, Guid>
    {
        private static readonly ISet<Ingredient> data = new HashSet<Ingredient>();
        ICollection<Ingredient> IDatabase<Ingredient>.Data => data;
    }
}