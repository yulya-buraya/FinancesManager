using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
	class Transfer
	{
		public int ID { get; set; }
		public DateTime Pdate { get; set; }
		public int Account1 { get; set; }
		public int Account2 { get; set; }
		public decimal Amount { get; set; }


		
	}

	public class Transfer1
	{
		public string Pdate { get; set; }
		public string Account1 { get; set; }
		public string Account2 { get; set; }
		public decimal Amount { get; set; }
		public int ID { get; set; }

		public Transfer1(string a, string b, string c, decimal d, int k)
		{
			Pdate = a;
			Account1 = b;
			Account2 = c;
			Amount = d;
			ID = k;
			

		}
	}
}
