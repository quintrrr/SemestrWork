using BLL.Enums;
using Core.DTO;
using Core.Interfaces;
using DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(
        IGroupService groupService)
    {
        _groupService = groupService;
    }
    
    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        try
        {
            await _groupService.DeleteGroupAsync(id); 
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{parentId:long}/children")]
    public async Task<ActionResult<List<TGroupDTO>>> GetChildGroups(long parentId)
    {
        return Ok(await _groupService.GetChildGroupsAsync(parentId));
    }

    [HttpPost("save")]
    public async Task<ActionResult> Save([FromBody] SaveGroupDTO saveGroupDto)
    {
        var result = await _groupService.SaveGroupAsync(
            saveGroupDto.GroupId, saveGroupDto.ParentId, saveGroupDto.Name);

        return result switch
        {
            GroupSaveResult.Created => Created(),
            GroupSaveResult.Updated => NoContent(),
            _ => NoContent()
        };
    }

    [HttpGet("nextGroupId")]
    public async Task<ActionResult<long>> GetNextGroupId()
    {
        return Ok(await _groupService.GetNextGroupIdAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TGroupDTO>> GetGroup(long id)
    {
        try
        {
            return Ok(await _groupService.GetGroupAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}