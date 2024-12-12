

using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.repository;
using webapi.domain.pizza;
using webapi.Features.Pizzas;

public class IngredientQuery : IFeatureModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/ingredients", (
                IQuery<Ingredient> repository,
                [FromQuery] string? name,
                [FromQuery] int page = 1,
                [FromQuery] int size = 3
            ) =>
            {
                var response = repository.Query.Where(i => (name == null) || (i.Name.ToLower().Contains(name.ToLower())))
                    .Skip((page - 1) * size)
                    .Take(size)
                    .Select(i => new IngredientsReponseWithoutCost(i.Id, i.Name));

                return Results.Ok(response);
            });
    }
}