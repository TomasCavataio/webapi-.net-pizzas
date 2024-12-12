using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.repository;
using webapi.domain.pizza;

namespace webapi.Features.Pizzas
{

    public readonly record struct IngredientsReponseWithoutCost(Guid Id, string Name) { }

    public readonly record struct PizzaRequest(string Name, string Description, string Url, List<Guid> Ingredients)
    {

    }

    public readonly record struct PizzaResponse(Guid Id, string Name, string Description, string Url, decimal Price, IEnumerable<IngredientsReponseWithoutCost> Ingredients)
    {

    }
    public class PizzaCreate : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Pizzas", (
                IAdd<Pizza> repository,
                IGet<Ingredient, Guid> repositoryIngredient,
                [FromBody] PizzaRequest request
            ) =>
            {
                var ingredientsList = request.Ingredients.Select((ingredientId) => repositoryIngredient.Get(ingredientId));
                var pizza = Pizza.Create(request.Name, request.Description, request.Url, ingredientsList);
                repository.Add(pizza);
                var response = new PizzaResponse(pizza.Id,
                pizza.Name,
                pizza.Description,
                pizza.Url,
                pizza.Price,
                pizza.Ingredients.Select(i => new IngredientsReponseWithoutCost(i.Id, i.Name)));

                return Results.Created("", response);
            });
        }
    }
}