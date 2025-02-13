namespace OracleTransactionApp;

internal class OracleContextFactory
{
	public OracleContextFactory()
	{
	}

	public OracleContext Create()
	{
		return new OracleContext();
	}
}
