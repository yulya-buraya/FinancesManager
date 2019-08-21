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
	/// Логика взаимодействия для Currency.xaml
	/// </summary>
	public partial class Currencs : Window
	{
		FContext db;
		public Currencs()
		{
			InitializeComponent();
			db = new FContext();
			db.Currencies.Load();
			CyrrenciesGrid.ItemsSource = db.Currencies.Local.ToBindingList();
			Status.Text = "Количество валют: " + (CyrrenciesGrid.Items.Count - 1);
		}

		private void Currency_Closing(object sender, CancelEventArgs e)
		{
			db.Dispose();
		}



		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (CyrrenciesGrid.SelectedItems.Count > 0)
			{
				for (int i = 0; i < CyrrenciesGrid.SelectedItems.Count; i++)
				{
					Finances.Model.Currency currency = CyrrenciesGrid.SelectedItems[i] as Finances.Model.Currency;
					if (currency != null)
					{
						db.Currencies.Remove(currency);
					}
				}
			}
			db.SaveChanges();
		}

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
			Currencs currencies = new Currencs();
			currencies.Show();
		}


	}
}
