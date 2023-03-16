using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
	private readonly DataContext _context;

	public UserController(DataContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		var users = await _context.Users.ToListAsync();

		return Ok(users);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser([FromRoute] int id)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

		return Ok(user);
	}
	
	[HttpPost]
	public async Task<ActionResult<User>> AddUser([FromBody] User user)
	{
		var newUser = new User()
		{
			Login = user.Login,
			PasswordHash = user.PasswordHash,
			FirstName = user.FirstName,
			SecondName = user.SecondName,
			Surname = user.Surname
		};

		await _context.Users.AddAsync(newUser);
		await _context.SaveChangesAsync();

		return Created($"/api/user/{newUser.Id}", null);
	}

}