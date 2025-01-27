using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OracleApp.Entity;
[Table("BOOK")]
internal class BookDao
{
	[Key]
	[Column("BOOK_ID")]
	public long Id { get; set; }

	[Required]
	[Column("TITLE")]
	public string Title { get; set; }

	[Required]
	[Column("UPDATE_COUNT")]
	public long UpdateCount { get; set; }
}
