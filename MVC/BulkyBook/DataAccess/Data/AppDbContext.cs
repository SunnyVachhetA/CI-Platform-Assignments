using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Data;
public class AppDbContext : DbContext
{
	public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Category> Categories { get; set; }
}