using System;
using System.Collections.Generic;
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
using System.Data.Entity;

using System.ComponentModel;
using Finances.Model;

namespace Finances
{
	/// <summary>
	/// Логика взаимодействия для Owner.xaml
	/// </summary>
	public partial class Owner : Window
	{
		FContext db;
		public Owner()
		{
			InitializeComponent();
			db = new FContext();
			db.Users.Load();
			UsersGrid.ItemsSource = db.Users.Local.ToBindingList();
			this.Closing += Owner_Closing;
			Status.Text = "Количество пользователей: " + (UsersGrid.Items.Count - 1);
		}

		private void Owner_Closing(object sender, CancelEventArgs e)
		{
			db.Dispose();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (UsersGrid.SelectedItems.Count > 0)
			{
				for (int i = 0; i < UsersGrid.SelectedItems.Count; i++)
				{
					User user = UsersGrid.SelectedItems[i] as User;
					if (user != null)
					{
						db.Users.Remove(user);
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
			Registration addUser = new Registration();
			addUser.Show();
			db.SaveChanges();

		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			Owner owner = new Owner();
			owner.Show();
		}


	}
}
