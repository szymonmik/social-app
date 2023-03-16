using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
	private readonly DataContext _context;
	private readonly ITokenService _tokenService;

	public AccountController(DataContext content, ITokenService tokenService)
	{
		_context = content;
		_tokenService = tokenService;
	}
	
	[HttpPost("register")]
	public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto dto)
	{
		if (await UserExists(dto.Login))
		{
			return BadRequest("Login is taken");
		}
		
		using var hmac = new HMACSHA512();
		
		var newUser = new User()
		{
			Login = dto.Login.ToLower(),
			PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
			PasswordSalt = hmac.Key,
			FirstName = dto.FirstName,
			Surname = dto.Surname
		};

		await _context.Users.AddAsync(newUser);
		await _context.SaveChangesAsync();

		return Created($"/api/user/{newUser.Id}", new UserDto()
		{
			Login = newUser.Login,
			FirstName = newUser.FirstName,
			Surname = newUser.Surname,
			Token = _tokenService.CreateToken(newUser)
		});
	}

	private async Task<bool> UserExists(string login)
	{
		return await _context.Users.AnyAsync(x => x.Login == login.ToLower());
	}

	[HttpPost("login")]
	public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto dto)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
		if (user is null)
		{
			return NotFound("User with this login doesn't exist");
		}

		using var hmac = new HMACSHA512(user.PasswordSalt);

		var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

		for (int i = 0; i < computedHash.Length; i++)
		{
			if (computedHash[i] != user.PasswordHash[i])
			{
				return Unauthorized("Invalid password");
			}
		}
		
		return Ok(new UserDto()
		{
			Login = user.Login,
			FirstName = user.FirstName,
			Surname = user.Surname,
			Token = _tokenService.CreateToken(user)
		});
	}
	
}