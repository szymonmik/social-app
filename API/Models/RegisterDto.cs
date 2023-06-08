using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class RegisterDto
{
	[Required]
	public string Login { get; set; }
	[StringLength(8, MinimumLength = 4)]
	[Required]
	public string Password { get; set; }
	public string FirstName { get; set; }
	public string Surname { get; set; }

}