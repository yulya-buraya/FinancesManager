using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
	 public class PlaningCost
	
	{
		public int ID { get; set; }
		public DateTime Pdate { get; set; }
		public int Type { get; set; }
		public decimal Amount { get; set; }
		public int Account { get; set; }
		public string Description { get; set; }

		//public virtual Accounts_ Accounts_ { get; set; }
		//public virtual ViewCost ViewCost { get; set; }


	}

	public class PlaningCost1
	{
		public string Pdate { get; set; }
		public string Account { get; set; }
		public string Type { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public int ID { get; set; }

		public PlaningCost1(string a, string b, string c, decimal d, string e, int k)
		{
			Pdate = a;
			Account = b;
			Type = c;
			Amount = d;
			Description = e;
			ID = k;
		}

	}
}
