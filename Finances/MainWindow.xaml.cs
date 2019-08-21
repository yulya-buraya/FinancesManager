using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using Finances.Model;

namespace Finances
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>

	

	public partial class MainWindow : Window
	{
		private static string name_user;
		public static User user = null;
	

		public MainWindow()
		{
			InitializeComponent();
		}

	
		public static string userName;

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			try
			{ 
			if (password.Password.Length == 0 && login.Text.Length == 0)
			MessageBox.Show("Введите имя пользователя и пароль");

			else if (login.Text.Length > 0)
			{
			if (password.Password.Length > 0)
			{
									   
			MainWindow mainWindow = new MainWindow();
			using (FContext db = new FContext())
			{
			var table = db.Users;
			bool flag_login = false;
			bool flag_password = false;
			foreach (var item in table)
			{
			if (Equals(login.Text, item.NameUser))
			{
			flag_login = true;
			if (Repo.Helpers.Hashing.SaltedHash.Verify(item.Hash, password.Password, item.Salt))
			flag_password = true;
			}
			}
			if (flag_login == false)
			MessageBox.Show("Пользователя с таким именем не существует!");
			else
			{
			if (flag_password == false)
			MessageBox.Show("Неверно введён пароль");
			else
			{
			foreach (var item in table)
			{
			if (item.NameUser == login.Text)
			{
			MainWindow.user = item;
			}
			}
			name_user = login.Text;

	this.Close();
	FinanceManager financeManager = new FinanceManager(user);
		financeManager.Show();
					}

				}
						}
		}
		else
		MessageBox.Show("Введите пароль");

	}
	else
	MessageBox.Show("Введите логин");


	}
	catch {	}

		}

		public string GetText()
		{
			return name_user;
		}


		private void Registr_Click(object sender, RoutedEventArgs e)
		{
			Registration registration = new Registration();
			registration.Show();
		}


	}

}






