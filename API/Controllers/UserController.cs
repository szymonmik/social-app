using API.Data;
using API.Entities;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
	private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
	{
		var users = await _userRepository.GetMembersAsync();

		return Ok(users);
	}

	[HttpGet("{login}")]
	public async Task<ActionResult<MemberDto>> GetUser([FromRoute] string login)
	{
		var user = await _userRepository.GetMemberAsync(login);

		return Ok(user);
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