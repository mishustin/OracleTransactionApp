namespace OracleTransactionApp;

internal static class OracleConfig
{
	private const string PasswordPlaceHolder = "{password}";
	private const string ConnectionStringTemplate = "User Id=USER_OWNER;Password={password};data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=yourhost)(PORT=yourport))(CONNECT_DATA=(SERVICE_NAME=DB)));" +
	                                         "Pooling=true;Max Pool Size=1;" +
	                                         "Connection Timeout=4";

	internal static string GetConnectionString()
	{
		var pass = Environment.GetEnvironmentVariable("PASS");
		ArgumentNullException.ThrowIfNull(pass, nameof(pass));
		var connectionString = ConnectionStringTemplate.Replace(PasswordPlaceHolder, pass);

		return connectionString;
	}
}
