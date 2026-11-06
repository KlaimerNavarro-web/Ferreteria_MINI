using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "ADMINISTRADOR")]
[ApiController]
[Route("api/logs")]
public class LogsController : ControllerBase
{
    private readonly LogService _logService;

    public LogsController(LogService logService) => _logService = logService;

   
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? tipo, [FromQuery] int limite = 50)
    {
        if (limite < 1 || limite > 200)
            return BadRequest("El parámetro 'limite' debe estar entre 1 y 200.");

        return Ok(await _logService.ObtenerAsync(tipo, limite));
    }
}