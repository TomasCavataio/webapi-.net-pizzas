

using Microsoft.AspNetCore.Mvc;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;

public class IngredientDelete : IFeatureModule
{

    [Injectable]
    public class Service:IService{
        private readonly IRemove<Ingredient, Guid> _repository;

        public Service(IRemove<Ingredient, Guid> repository){
            _repository = repository;
        }


        public void Handle(Guid id) {
            var ingredient = _repository.Get(id);
            _repository.Remove(ingredient);
        }

    }

    public interface IService{
        void Handle(Guid id);
    }


    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/ingredients/{id}", (IService service, [FromRoute] Guid id) =>
        {
            service.Handle(id);
            return Results.NoContent();
        });

    }
}