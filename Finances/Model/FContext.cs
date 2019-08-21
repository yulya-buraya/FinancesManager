using Finances.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Model
{
	class FContext : DbContext
	{
	
		public FContext() : base("DefaultConnection")
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<ViewCost> ViewCosts { get; set; }
		public DbSet<ViewIncome> ViewIncomes { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Cost> Costs { get; set; }
		public DbSet<Income> Incomes { get; set; }
		public DbSet<Transfer> Transfers { get; set; }
		public DbSet<PlaningCost> PlaningCosts { get; set; }
		public DbSet<PlaningIncome> PlaningIncomes { get; set; }
	}
}


