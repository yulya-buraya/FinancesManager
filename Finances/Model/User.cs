using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{

	public class User
	{
		public int ID { get; set; }
		public string NameUser { get; set; }
		public string PasswordUser { get; set; }
		public string Salt { get; set; }
		public string Hash { get; set; }


	}

}
