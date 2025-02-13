using Microsoft.EntityFrameworkCore;
using OracleTransactionApp.Entity;

namespace OracleTransactionApp;
internal class OracleContext : DbContext
{
	public DbSet<UserDao> Users { get; set; } = null!;

	public OracleContext()
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder
			.UseOracle(OracleConfig.GetConnectionString());
		base.OnConfiguring(optionsBuilder);
	}
}
