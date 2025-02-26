using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatformAPI.Dtos.Account;
using SocialMediaPlatformAPI.Interfaces;
using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Controllers;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<AccountController> _logger;
    public AccountController(IAuthRepository authRepository, ILogger<AccountController> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authRepository.RegisterUserAsync(registerDto);
        if (!result.IsAuthenticated)
            return BadRequest(new {Message = result.Message});

        return Ok(new  { Token = result.Token});
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authRepository.LoginUserAsync(loginDto);
        if (!result.IsAuthenticated)
            return BadRequest(new {Message = result.Message});
        return Ok(new {Token = result.Token});
    }

    [HttpPost("addrole")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRole addRole)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authRepository.AddRoleAsync(addRole);
        if (!string.IsNullOrEmpty(result))
            return BadRequest(result);
        return  Ok(addRole);
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        
        return Ok(new {msg="up"});
    }
}