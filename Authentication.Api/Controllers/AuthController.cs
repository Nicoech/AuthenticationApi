using AuthenticationApi.Domain.Entities;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    public readonly DataContext _dataContext;

    public AuthController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    [HttpPost("login")]
    [Authorize]
    public async Task<IActionResult> Login()
    { 
        return await Task.FromResult(new OkObjectResult("Autenticación exitosa con Keycloak."));
    }

    [HttpGet("list")]
    public async Task<IEnumerable<Role>> Get()
    {
        return await Task.FromResult(_dataContext.Roles.ToList());
    }
}