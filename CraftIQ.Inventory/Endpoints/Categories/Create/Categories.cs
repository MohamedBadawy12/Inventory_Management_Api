using CraftIQ.Inventory.Core.Entities.Categories;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CraftIQ.Inventory.Endpoints.Categories.Create
{
    /// <summary>
    /// My Name Mohamed Badawy
    /// In this endpoint is about create category using repository
    /// </summary>
    public class Categories(IRepository<Category> repository) : EndpointsAsync
                                            .WithRequest<CreateCategoriesRequest>
                                            .WithActionResult<CreateCategoriesResponse>

    {
        private readonly IRepository<Category> _repository = repository;
        [HttpPost(Routes.CategoryRoutes.Create)]
        public async override Task<ActionResult<CreateCategoriesResponse>> HandleAsync
            (CreateCategoriesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("request can't be null", (int)HttpStatusCode.BadRequest);
            var oData = new Category(request.Name, request.Description);
            var oResult = await _repository.AddAsync(oData);
            return Ok(new CreateCategoriesResponse(oResult.Name, oResult.Description));
        }
    }
}
