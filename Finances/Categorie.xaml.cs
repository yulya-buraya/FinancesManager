using Finances.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Finances
{
	/// <summary>
	/// Логика взаимодействия для Categorie.xaml
	/// </summary>
	public partial class Categorie : Window
	{
		FContext db;

		public Categorie()
		{
			InitializeComponent();
			db = new FContext();
			db.ViewCosts.Load();
			CostsGrid.ItemsSource = db.ViewCosts.Local.ToBindingList();
			CostsStatus.Text = "Категорий расходов: " + (CostsGrid.Items.Count - 1);


			{

				db.ViewIncomes.Load();
				IncomesGrid.ItemsSource = db.ViewIncomes.Local.ToBindingList();
				IncomeStatus.Text = "Категорий доходов: " + (IncomesGrid.Items.Count - 1);

				this.Closing += IncomeAndCost_Closing;
			}
		}

		private void IncomeAndCost_Closing(object sender, CancelEventArgs e)
		{
			db.Dispose();
		}


		//private void DeleteButton_Click(object sender, RoutedEventArgs e)
		//{
		//	if (IncomesGrid.SelectedItems.Count > 0)
		//	{
		//		for (int i = 0; i < IncomesGrid.SelectedItems.Count; i++)
		//		{
		//			ViewIncome t_Income = IncomesGrid.SelectedItems[i] as ViewIncome;
		//			if (t_Income != null)
		//			{
		//				db.ViewIncomes.Remove(t_Income);
		//			}
		//		}
		//	}

		//	if (CostsGrid.SelectedItems.Count > 0)
		//	{
		//		for (int i = 0; i < CostsGrid.SelectedItems.Count; i++)
		//		{
		//			ViewCost t_Cost = CostsGrid.SelectedItems[i] as ViewCost;
		//			if (t_Cost != null)
		//			{
		//				db.ViewCosts.Remove(t_Cost);
		//			}
		//		}
		//	}

		//	db.SaveChanges();
		//}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{

			db.SaveChanges();
		
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			Categorie categorie = new Categorie();
			categorie.Show();
		}

	}
}
