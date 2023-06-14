using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
	private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		return Ok(await _userRepository.GetUsersAsync());
	}

	[HttpGet("{login}")]
	public async Task<ActionResult<User>> GetUser([FromRoute] string login)
	{
		return Ok(await _userRepository.GetUserByLoginAsync(login));
	}
	
	/*
	[HttpPost]
	public async Task<ActionResult<User>> AddUser([FromBody] User user)
	{
		var newUser = new User()
		{
			Login = user.Login,
			PasswordHash = user.PasswordHash,
			FirstName = user.FirstName,
			//SecondName = user.SecondName,
			Surname = user.Surname
		};

		await _context.Users.AddAsync(newUser);
		await _context.SaveChangesAsync();

		return Created($"/api/user/{newUser.Id}", null);
	}*/

}