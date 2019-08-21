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
using System.Windows.Shapes;
using System.Configuration;
using Finances.Model;
using System.Text.RegularExpressions;

namespace Finances
{
	/// <summary>
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{

		public Registration()
		{
			InitializeComponent();

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			User user = new User
			{
				NameUser = login.Text
			};
			if (password1.Password.CompareTo(password2.Password) == 0 && password1.Password.CompareTo("") != 0
			 && password2.Password.CompareTo("") != 0 && login.Text.CompareTo("") != 0 )
			{
				
			Repo.Helpers.Hashing.SaltedHash obj = new Repo.Helpers.Hashing.SaltedHash(password1.Password);
			using (FContext db = new FContext())
			{
			var table = db.Users;
			bool flag_login = false;
			foreach (var item in table)
			{
			if (Equals(login.Text, item.NameUser))
			{
			flag_login = true;
			}
			}
			if (flag_login == true)
			MessageBox.Show("Пользователь с таким именем уже существует!");
			else
			{
			db.Users.Add(new User
			{
			NameUser = user.NameUser,
			Hash = obj.Hash,
			Salt = obj.Salt,
				
			});
			db.SaveChanges();
			MessageBox.Show("Пользователь зарегистрирован!");
					
			}
			}
								
			}
			else
				MessageBox.Show("Проверьте правильность введённых данных!");
	

		this.Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

