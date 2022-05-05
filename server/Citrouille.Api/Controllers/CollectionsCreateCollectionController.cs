using Citrouille.Infrastructure.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citrouille.Api.Controllers;

[Route("/api/Collections")]
public class CollectionsCreateCollectionController : BaseController
{
    private readonly CreateCollectionService _handler;

    public CollectionsCreateCollectionController(CreateCollectionService handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCollection command)
    {
        await _handler.CreateCollectionAsync(command);
        return CreatedAtAction(nameof(Post), new { id = command.Id }, null);
    }
}