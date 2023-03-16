namespace API.Entities;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
	public string FirstName { get; set; }
	public string SecondName { get; set; }
	public string Surname { get; set; }

}