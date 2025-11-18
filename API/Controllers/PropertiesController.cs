using BLL.Enums;
using Core.DTO;
using Core.Interfaces;
using DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(
        IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet("group/{id:long}")]
    public async Task<ActionResult<List<TPropertyDTO>>> GetGroupProperties(long id)
    {
        return Ok(await _propertyService.GetGroupPropertiesAsync(id));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TPropertyDTO>> GetProperty(long id)
    {
        try
        {
            return Ok(await _propertyService.GetPropertyAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        try
        {
            await _propertyService.DeletePropertyAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("save")]
    public async Task<ActionResult> Save([FromBody] SavePropertyDTO savePropertyDTO)
    {
        var result = await _propertyService.SavePropertyAsync(savePropertyDTO);

        return result switch
        {
            PropertySaveResult.Created => Created(),
            PropertySaveResult.Updated => NoContent(),
            _ => NoContent()
        };
    }
}