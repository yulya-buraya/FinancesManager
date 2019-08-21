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
	/// Логика взаимодействия для addIncome.xaml
	/// </summary>
	public partial class addIncome : Window
	{
		MainWindow obj = new MainWindow();
		public addIncome()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection("server= 1-ПК\\SQLSERVER; Trusted_Connection=Yes; DataBase=Finances");
			SqlCommand sqlcommand = new SqlCommand("SELECT * FROM ViewIncomes order by ViewIncomes ", sqlConnection);
			sqlConnection.Open();
			SqlDataReader reader;
			SqlCommand sqlcommand1 = new SqlCommand("SELECT   * FROM Accounts left join Users On Users.ID=Accounts.NameUser Where Users.NameUser='" + obj.GetText() + " '", sqlConnection);

			reader = sqlcommand1.ExecuteReader();
			while (reader.Read())
			{
				try
				{
					string result = reader.GetString(1);
					account.Items.Add(result);
					account.Text = result;
				}
				catch { }

			}
			reader.Close();
			reader = sqlcommand.ExecuteReader();

			while (reader.Read())
			{
				try
				{
					string result = reader.GetString(1);
					categorie.Items.Add(result);
					categorie.Text = result;
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
			var viewincomesID = Select("SELECT ID FROM [dbo].[ViewIncomes] WHERE [ViewIncomes]='" + (categorie.SelectedItem) + " '");
			var accountID = Select("SELECT ID FROM [dbo].[Accounts] WHERE [Name]='" + (account.SelectedItem) + " '");

			try
		{
				Income income1 = new Income()
				{
					Pdate = (DateTime)date.SelectedDate,
					Account = Int32.Parse(accountID.Rows[0].ItemArray[0].ToString()),
					Type = Int32.Parse(viewincomesID.Rows[0].ItemArray[0].ToString()),
					Amount = decimal.Parse(balance.Text),
					Description = description.Text
				};
				using (FContext db = new FContext())
				{
					db.Incomes.Add(new Income()
					{
						Pdate = income1.Pdate,
						Account = income1.Account,
						Type = income1.Type,
						Amount = income1.Amount,
						Description = income1.Description
					});
					db.SaveChanges();
				//	FinanceManager financeManager = new FinanceManager();
				//	financeManager.Refresh();
					MessageBox.Show("Доход добавлен!");
					this.Close();
				}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
		 }


		}
	}
	}
	
