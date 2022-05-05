using Citrouille.Infrastructure.Queries;
using Citrouille.Infrastructure.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citrouille.Api.Controllers;

[Route("/api/Collections")]
public class CollectionsQueryController : BaseController
{
    private readonly CollectionQueryService _handler;

    public CollectionsQueryController(CollectionQueryService handler)
    {
        _handler = handler;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CollectionDto>> Get([FromRoute] Guid id)
    {
        var result = await _handler.GetById(id);
        return OkOrNotFound(result);
    }
}