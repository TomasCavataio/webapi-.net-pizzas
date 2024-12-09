using System.Collections.ObjectModel;
using webapi.core.entitybase;

namespace webapi.domain.pizza;
class Pizza : EntityBase
{

    private const decimal PROFIT = 1.2M;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public decimal Price => ingredients.Sum(x => x.Cost) * PROFIT;
    public ReadOnlyCollection<Ingredient> Ingredients => new([.. ingredients]);
    private readonly ISet<Ingredient> ingredients = new HashSet<Ingredient>();
    protected Pizza(Guid id, string name, string description, string url, ISet<Ingredient> ingredients) : base(id)
    {
        Name = name;
        Description = description;
        Url = url;
        this.ingredients = ingredients;

    }
    public void Update(string name, string description, string url)
    {
        Name = name;
        Description = description;
        Url = url;
    }
    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }
    public void RemoveIngredient(Ingredient ingredient)
    {
        ingredients.Remove(ingredient);
    }

    public static Pizza Create(string name, string description, string url, IEnumerable<Ingredient> ingredients)
    {
        return new Pizza(
                Guid.NewGuid(),
                name,
                description,
                url,
                ingredients.ToHashSet()
                );
    }
}