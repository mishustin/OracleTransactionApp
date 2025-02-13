using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OracleTransactionApp.Entity;
[Table("USER")]
internal class UserDao
{
	[Key]
	[Column("USER_ID")]
	public long Id { get; set; }

	[Required]
	[Column("LAST_CHANGE_DATE")]
	public DateTime LastChangeDate { get; set; }
}
