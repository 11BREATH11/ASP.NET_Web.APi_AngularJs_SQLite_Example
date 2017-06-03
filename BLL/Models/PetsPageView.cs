using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
	public class PetsPageView
	{
		public IEnumerable<Pets> Items;
		public int TotalItems;
		public string UserName;
	}
}
