using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace OracleTransactionApp;

internal class Program
{
	static async Task Main(string[] args)
	{
		Console.WriteLine($".net version: {Environment.OSVersion}");
		Console.WriteLine($".net version: {Environment.Version}");
		Console.WriteLine("________________________________________________________________");
		Console.WriteLine();

		var dbContextFactory = new OracleContextFactory();
		var userId = 1;
		while (true)
		{
			try
			{
				using (var transaction1 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(2), TransactionScopeAsyncFlowOption.Enabled))
				{
					for (var i = 0; i < 1000; i++)
					{
						using (var contextNew = dbContextFactory.Create())
						{
							var user = await contextNew.Users.FirstAsync(s => s.Id == userId).ConfigureAwait(false);
							user.LastChangeDate = DateTime.Now;
							contextNew.Update(user);
							await contextNew.SaveChangesAsync().ConfigureAwait(false);
						}
					}

					transaction1.Complete();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(DateTime.Now);
				Console.WriteLine(e.Message);
				if (e.InnerException is not null)
				{
					Console.WriteLine($"{nameof(e.InnerException)}: {e.InnerException.Message}");
				}

				Console.WriteLine("________________________________________________________________");
				Console.WriteLine();
				if (e.Message == "ORA-50012: Pooled connection request timed out")
				{
					break;
				}
			}
		}

		Console.WriteLine("Catch problem!");
		for (var i = 0; i < 20; i++)
		{
			try
			{
				using (var contextNew = dbContextFactory.Create())
				{
					var user = await contextNew.Users.FirstAsync(s => s.Id == userId).ConfigureAwait(false);
					user.LastChangeDate = DateTime.Now;
					contextNew.Update(user);
					await contextNew.SaveChangesAsync().ConfigureAwait(false);
					Console.WriteLine(DateTime.Now);
					Console.WriteLine("OK");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				if (e.InnerException is not null)
				{
					Console.WriteLine($"{nameof(e.InnerException)}: {e.InnerException.Message}");
				}
			}
			Console.WriteLine("________________________________________________________________");
			Console.WriteLine();
		}
	}
}