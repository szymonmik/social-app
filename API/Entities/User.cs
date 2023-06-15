﻿using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
	public string FirstName { get; set; }
	//public string SecondName { get; set; }
	public string Surname { get; set; }
	public DateOnly DateOfBirth { get; set; }
	public DateTime Created { get; set; } = DateTime.UtcNow;
	public DateTime LastActive { get; set; } = DateTime.UtcNow;
	public string Gender { get; set; }
	public string Introduction { get; set; }
	public string Interests { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
	public List<Photo> Photos { get; set; } = new();

	/*public int GetAge()
	{
		return DateOfBirth.CalculateAge();
	}*/
}