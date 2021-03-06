using Citrouille.Infrastructure.Commands;
using Citrouille.Infrastructure.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Citrouille.Api.Controllers;

[Route("/api/Collections")]
public class CollectionsCommandController : BaseController
{
    private readonly ICollectionCommandService _handler;

    public CollectionsCommandController(ICollectionCommandService handler)
    {
        _handler = handler;
    }

    [HttpPut("items")]
    public async Task<IActionResult> Put([FromBody] AddCollectionItem command)
    {
        await _handler.AddCollectionItemAsync(command);
        return NoContent();
    }

    [HttpDelete("items")]
    public async Task<IActionResult> Delete([FromBody] RemoveCollectionItem command)
    {
        await _handler.RemoveCollectionItemAsync(command);
        return NoContent();
    }

    [HttpDelete("")]
    public async Task<IActionResult> Delete([FromBody] RemoveCollection command)
    {
        await _handler.RemoveCollectionAsync(command);
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut("{collectionId:guid}/like")]
    public async Task<IActionResult> Like([FromRoute] Guid collectionId, [FromBody]LikeCollectionItem command)
    {
        await _handler.LikeItemAsync(command with {CollectionId = collectionId});
        return NoContent();
    }
    
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut("{collectionId:guid}/comment")]
    public async Task<IActionResult> Comment([FromRoute] Guid collectionId, [FromBody]CommentCollectionItem command)
    {
        await _handler.CommentItemAsync(command with {CollectionId = collectionId});
        return NoContent();
    }
}