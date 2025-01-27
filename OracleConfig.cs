namespace OracleApp;

internal static class OracleConfig
{
	internal const string ConnectionString = "User Id=EVGENY_OWNER;Password=evgeny;" +
	                                         "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.184.77)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)));" +
	                                         "Pooling=true;Max Pool Size=1;";
}
