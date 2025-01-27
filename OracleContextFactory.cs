using System.Collections.Concurrent;
using System.Data.Common;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;

namespace OracleApp;
internal class OracleContextFactory
{
	private readonly AsyncLocal<Lazy<DbConnection>> _connection = new()
	{
		Value = new Lazy<DbConnection>(() => new OracleConnection(OracleConfig.ConnectionString), LazyThreadSafetyMode.ExecutionAndPublication)
	};

	private readonly bool _supportAmbientTransaction;

	public OracleContextFactory(bool supportAmbientTransaction)
	{
		_supportAmbientTransaction = supportAmbientTransaction;
	}

	public OracleContext Create()
	{
		OracleContext? oracleContext;
		if (!_supportAmbientTransaction)
		{
			oracleContext = new OracleContext();
		}
		else if (Transaction.Current == null)
		{
			oracleContext = new OracleContext();
		}
		else
		{
			var connection = _connection.Value!.Value;
			oracleContext = new OracleContext(connection);
		}

		return oracleContext;
	}
}
