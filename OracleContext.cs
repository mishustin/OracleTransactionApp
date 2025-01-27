using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using OracleApp.Entity;

namespace OracleApp;
internal class OracleContext : DbContext
{
	private readonly DbConnection? _connection;

	public DbSet<BookDao> Books { get; set; } = null!;

	public OracleContext()
	{
	}

	public OracleContext(DbConnection? connection)
	{
		_connection = connection;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (_connection == null)
		{
			optionsBuilder
				.UseOracle(OracleConfig.ConnectionString);
		}
		else
		{
			optionsBuilder
				.UseOracle(_connection);
		}

		base.OnConfiguring(optionsBuilder);
	}
}
