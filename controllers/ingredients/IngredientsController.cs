/*using Microsoft.AspNetCore.Mvc;
using webapi.domain.pizza;

namespace webapi.controllers.ingredients;

[ApiController]
[Route("[controller]")]
public class IngredientsController : ControllerBase
{
   
    

    /// <summary>
    /// [HttpPost]
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public IActionResult Post([FromBody] IngredientRequest request)
    {
        var ingredient = Ingredient.Create(request.Name, request.Cost);
        return StatusCode(201,
           new IngredientResponse(
               ingredient.Id,
               ingredient.Name,
               ingredient.Cost)
           );

    }
}*/
