using Microsoft.AspNetCore.Mvc;

namespace NewsAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class BaseController() : ControllerBase;

