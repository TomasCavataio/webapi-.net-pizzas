

using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;

public class IngredientGet : IFeatureModule
{
    [Injectable]
    public class Service:IService {

        private IGet<Ingredient, Guid> _repository;

        public Service(IGet<Ingredient, Guid> repository) {
            _repository = repository;
        }

        public webapi.Features.Ingredients.IngredientResponse Handle(Guid id) {
            var ingredient = _repository.Get(id);
            var response = new webapi.Features.Ingredients.IngredientResponse(ingredient.Id, ingredient.Name, ingredient.Cost);
            return response;
        }
    }


    public interface IService{
        webapi.Features.Ingredients.IngredientResponse Handle(Guid id);
    }

    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/ingredients/{id}", (IService service, [FromRoute] Guid id) =>
        {
            var response = service.Handle(id);
            return Results.Ok(response);
        });
        
    }
}