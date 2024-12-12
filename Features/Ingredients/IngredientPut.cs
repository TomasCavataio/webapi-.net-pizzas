using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.repository;
using webapi.domain.pizza;

namespace webapi.Features.Ingredients
{


    public class IngredientPut : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/ingredients/{id}", (
                IUpdate<Ingredient, Guid> repository,
                [FromBody] IngredientRequest request,
                [FromRoute] Guid id
            ) =>
            {
                var ingredient = repository.Get(id);
                ingredient.Update(request.Name, request.Cost);
                repository.Update(ingredient);
                var response = new IngredientResponse(ingredient.Id, ingredient.Name, ingredient.Cost);

                return Results.Ok(response);
            });
        }
    }
}