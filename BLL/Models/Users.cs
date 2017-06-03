using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
	public class Users
	{
		[SQLite.AutoIncrement, SQLite.PrimaryKey, SQLite.Indexed]
		public int? Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
	}
}
