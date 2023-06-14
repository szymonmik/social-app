using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
        
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users
        .FindAsync(id);
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        return await _context.Users
        .Include(p => p.Photos)
        .FirstOrDefaultAsync(x => x.Login == login);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users
        .Include(p => p.Photos)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
}
