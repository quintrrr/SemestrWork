using Core.DTO;
using Core.Interfaces;
using DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RelationsController : ControllerBase
{
    private readonly IRelationService _relationService;

    public RelationsController(
        IRelationService relationService)
    {
        _relationService = relationService;
    }

    [HttpGet("parent/{parentId:long}")]
    public async Task<ActionResult<List<TRelationDTO>>> GetParentRelations(long parentId)
    {
        return Ok(await _relationService.GetParentRelationsAsync(parentId));
    }
    
    [HttpGet("child/{childId:long}")]
    public async Task<ActionResult<List<TRelationDTO>>> GetChildRelations(long childId)
    {
        return Ok(await _relationService.GetChildRelationsAsync(childId));
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] TRelationDTO relation)
    {
        try
        {
            await _relationService.DeleteRelationAsync(relation.ParentId, relation.ChildId);
            return NoContent();
        }
        catch (Exception ex)
        {   
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TRelationDTO relation)
    {
        await _relationService.CreateRelationAsync(relation.ParentId, relation.ChildId);
        
        return NoContent();
    }
}