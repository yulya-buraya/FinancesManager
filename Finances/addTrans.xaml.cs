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
	/// Логика взаимодействия для addTrans.xaml
	/// </summary>
	public partial class addTrans : Window
	{
		
		MainWindow obj = new MainWindow();
		public addTrans()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection("server= 1-ПК\\SQLSERVER; Trusted_Connection=Yes; DataBase=Finances");
			sqlConnection.Open();
			SqlDataReader reader;
			SqlCommand sqlcommand1 = new SqlCommand("SELECT   * FROM Accounts left join Users On Users.ID=Accounts.NameUser Where Users.NameUser='" + obj.GetText() + "'", sqlConnection);
			reader = sqlcommand1.ExecuteReader();
			while (reader.Read())
			{
				try
				{
					string result = reader.GetString(1);
					account1.Items.Add(result);
					account1.Text = result;
					account2.Items.Add(result);
					account2.Text = result;

				}
				catch { }

			}
			reader.Close();
			
			

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

			var account1ID = Select("SELECT ID FROM [dbo].[Accounts] WHERE [Name]='" + (account1.SelectedItem) + " '");
			var account2ID = Select("SELECT ID FROM [dbo].[Accounts] WHERE [Name]='" + (account2.SelectedItem) + " '");
			var viewincomesID = Select("SELECT ID FROM [dbo].[ViewIncomes] WHERE [ViewIncomes]='перевод'");
			var viewcostsID = Select("SELECT ID FROM [dbo].[ViewCosts] WHERE [ViewCosts]='перевод'");

			try
			{
			Transfer transfer1 = new Transfer()
			{
				Pdate = (DateTime)date.SelectedDate,
				Account1 = Int32.Parse(account1ID.Rows[0].ItemArray[0].ToString()),
				Account2 = Int32.Parse(account2ID.Rows[0].ItemArray[0].ToString()),
				Amount = decimal.Parse(balance.Text)
	
			};

			Income income1 = new Income()
			{
				Pdate = (DateTime)date.SelectedDate,
				Account = Int32.Parse(account2ID.Rows[0].ItemArray[0].ToString()),
				Type = Int32.Parse(viewincomesID.Rows[0].ItemArray[0].ToString()),
				Amount = decimal.Parse(balance.Text),
				Description = " с "+ account1.SelectedItem.ToString()
			};

			Cost cost1 = new Cost()
			{
				Pdate = (DateTime)date.SelectedDate,
				Account = Int32.Parse(account1ID.Rows[0].ItemArray[0].ToString()),
				Type = Int32.Parse(viewcostsID.Rows[0].ItemArray[0].ToString()),
				Amount = decimal.Parse(balance.Text),
				Description = " на " + account2.SelectedItem.ToString()
			};
			using (FContext db = new FContext())
			{
				db.Transfers.Add(new Transfer()
				{
					Pdate = transfer1.Pdate,
					Account1 = transfer1.Account1,
					Account2 = transfer1.Account2,
					Amount = transfer1.Amount
	
				});
				
					db.SaveChanges();
				db.Incomes.Add(new Income()
				{
					Pdate = income1.Pdate,
					Account = income1.Account,
					Type = income1.Type,
					Amount = income1.Amount,
					Description = income1.Description
				});
				db.SaveChanges();
				db.Costs.Add(new Cost()
				{
					Pdate = cost1.Pdate,
					Account = cost1.Account,
					Type = cost1.Type,
					Amount = cost1.Amount,
					Description = cost1.Description
				});
				db.SaveChanges();
			//FinanceManager financeManager = new FinanceManager();
				//	financeManager.Refresh();
					MessageBox.Show("Перевод добавлен!");
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

