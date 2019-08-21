using Finances.Model;
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

namespace Finances
{
	/// <summary>
	/// Логика взаимодействия для AddAccount.xaml
	/// </summary>
	public partial class addAccount : Window
	{
		MainWindow obj = new MainWindow();
		public addAccount()
		{
			InitializeComponent();

			SqlConnection sqlConnection = new SqlConnection("server= 1-ПК\\SQLSERVER; Trusted_Connection=Yes; DataBase=Finances");
			SqlCommand sqlcommand = new SqlCommand("SELECT * FROM Currencies order by Fullname ", sqlConnection);
			sqlConnection.Open();
			SqlDataReader reader;
			reader = sqlcommand.ExecuteReader();
			while (reader.Read())
			{
				try
				{
					string result = reader.GetString(1);
					currency.Items.Add(result);
					currency.Text = result;
				}
				catch { }

			}
			sqlConnection.Close();



		}

		public DataTable Select(string selectSQL)
		{
			DataTable dataTable = new DataTable("dataBase");

			SqlConnection sqlConnection = new SqlConnection("server= 1-ПК\\SQLSERVER; Trusted_Connection=Yes; DataBase=Finances");


			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = selectSQL;

			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataTable);

			return dataTable;

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			var userID = Select("SELECT ID FROM [dbo].[Users] WHERE [NameUser]='" + obj.GetText() + "'");
			var currencyID = Select("SELECT ID FROM [dbo].[Currencies] WHERE [Fullname]='" + (currency.SelectedItem) + " '");

			try
		{
				Account account1 = new Account()
				{
					Name = account.Text,
					NameUser = Int32.Parse(userID.Rows[0].ItemArray[0].ToString()),
					Amount = decimal.Parse(balance.Text),
					Currency = Int32.Parse(currencyID.Rows[0].ItemArray[0].ToString()),
					Description = description.Text
				};
				using (FContext db = new FContext())
				{
					db.Accounts.Add(new Account()
					{
						Name = account1.Name,
						NameUser = account1.NameUser,
						Amount = account1.Amount,
						Currency = account1.Currency,
						Description = account1.Description
					});
					db.SaveChanges();
					//FinanceManager financeManager = new FinanceManager();
					//financeManager.Refresh();
					MessageBox.Show("Счёт добавлен!");
					this.Close();
				}
			}
		  catch (Exception ex)
				{
				MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
				}
		

			}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
