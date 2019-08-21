using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
	public class Account
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int NameUser { get; set; }
		public decimal Amount { get; set; }
		//public decimal Rest { get; set; }
		public int Currency { get; set; }
		public string Description { get; set; }

	}

	class Account1
	{
		
		public string Name { get; set; }
		//public string; NameUser { get; set; }
		public decimal Amount { get; set; }
		public decimal Rest { get; set; }
		public string Currency { get; set; }
		public string Description { get; set; }
		public int ID { get; set; }

		public Account1(string a, string b, decimal c, decimal d, string e, int k)
		{
			Name = a;
			Currency= b;
			Amount = c;
		    Rest= d;
			Description = e;
			ID = k;
		}


	}
}
