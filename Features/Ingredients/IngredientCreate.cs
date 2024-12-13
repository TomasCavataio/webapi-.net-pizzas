using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using webapi.core.IFeaturModule;
using webapi.core.ioc;
using webapi.core.repository;
using webapi.domain.pizza;

namespace webapi.Features.Ingredients;


public readonly record struct IngredientRequest(string Name, decimal Cost)
{

}

public readonly record struct IngredientResponse(Guid Id, string Name, decimal Cost)
{

}
public class IngredientCreate : IFeatureModule
{
    [Injectable]
    public class Service : IService
    {
        private readonly IAdd<Ingredient> _repository;
        private IValidator<IngredientRequest> _validator;

        public Service(IAdd<Ingredient> repository, IValidator<IngredientRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IngredientResponse Handle(IngredientRequest request)
        {
            _validator.ValidateAndThrow(request);
            var ingredient = Ingredient.Create(request.Name, request.Cost);
            _repository.Add(ingredient);
            _repository.Commit();
            return new IngredientResponse(ingredient.Id, ingredient.Name, ingredient.Cost);
        }
    }

    public interface IService
    {
        IngredientResponse Handle(IngredientRequest request);
    }


    [Injectable]
    private class Validator : AbstractValidator<IngredientRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Cost).NotNull().NotEmpty().WithMessage("Debe haber un precio para el ingrediente");
        }
    }


    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/ingredients", (
            IService service,
            [FromBody] IngredientRequest request
        ) =>
        {
            var response = service.Handle(request);
            return Results.Created("", response);
        }).RequireAuthorization();
    }
}




