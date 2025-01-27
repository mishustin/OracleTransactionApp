using System.Collections.Concurrent;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace OracleApp;

internal class Program
{
	static async Task Main(string[] args)
	{
		Console.WriteLine(Environment.Version);

		var dbContextFactory = new OracleContextFactory(supportAmbientTransaction: false);

		using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
		{
			var tasks = new List<Task>();

			var dbContext1 = dbContextFactory.Create();
			var book1 = await dbContext1.Books.Where(x => x.Id == 1).FirstAsync();
			book1.UpdateCount++;
			dbContext1.Update(book1);
			tasks.Add(dbContext1.SaveChangesAsync());
			//await dbContext1.SaveChangesAsync();

			var dbContext2 = dbContextFactory.Create();
			var book2 = await dbContext2.Books.Where(x => x.Id == 2).FirstAsync();
			book2.UpdateCount++;
			dbContext2.Update(book2);
			tasks.Add(dbContext2.SaveChangesAsync());
			//await dbContext2.SaveChangesAsync();

			await Task.WhenAll(tasks);

			transaction.Complete();
		}

		Console.WriteLine("Done!");
	}
}