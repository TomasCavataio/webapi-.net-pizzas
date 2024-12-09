using webapi.core.IFeaturModule;
using webapi.domain.pizza;

namespace webapi.Features.Ingredients
{

    public readonly record struct IngredientRequest(string Name, decimal Cost)
    {

    }

    public readonly record struct IngredientResponse(Guid Id, string Name, decimal Cost)
    {

    }
    public class IngredientCreate : IFeatureModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {            
            app.MapPost("/ingredients", (IngredientRequest request)=>{
                var ingredient = Ingredient.Create(request.Name, request.Cost);
                var response = new IngredientResponse(ingredient.Id, ingredient.Name, ingredient.Cost);
                return Results.Created("",response);                
            });
        }
    }
}