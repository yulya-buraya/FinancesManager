using Finances.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
	/// Логика взаимодействия для FinanceManagr.xaml
	/// </summary>

	public partial class FinanceManager : Window
	{
		public static int IDP;
		FContext db;
		static string user_name;
		public FinanceManager()
		{
			InitializeComponent();


		}
		public FinanceManager(User users)
		{

			InitializeComponent();

			MainWindow.user = users;

			db = new FContext();
			user_name = users.NameUser;
			db.Users.Load();
			db.ViewCosts.Load();
			db.ViewIncomes.Load();

			db.PlaningCosts.Load();
			db.Incomes.Load();
			db.Currencies.Load();
			db.Costs.Load();
			db.Accounts.Load();
			db.Transfers.Load();
			string sqlExpression = "SELECT Costs.Pdate,ViewCosts.ViewCosts, Costs.Amount, Accounts.Name ,Costs.Description, Users.NameUser, Costs.ID FROM Costs left join Accounts On Accounts.ID=Costs.Account left join Users On Accounts.NameUser=Users.ID left join ViewCosts On Costs.Type = ViewCosts.ID Where Users.NameUser='" + users.NameUser + "'";
			string sqlAccount = " Select Accounts.Name, Accounts.Amount, Currencies.Shortname , ISNULL(d.[Costs],0), ISNULL(b.[Incomes],0), Accounts.Description , Users.NameUser, Accounts.ID  From Accounts full join Users On Accounts.NameUser=Users.ID full join ( Select  sum(Costs.Amount)[Costs], Account  From Costs  group by Account) d  on d.Account=Accounts.ID  left join Currencies on Accounts.Currency=Currencies.ID full join (Select  sum(Incomes.Amount)[Incomes], Account From Incomes  group by Account) b  on b.Account=Accounts.ID Where Users.NameUser='" + users.NameUser + "'";
			string sqlIncome = "SELECT Incomes.Pdate,ViewIncomes.ViewIncomes, Incomes.Amount, Accounts.Name ,Incomes.Description, Users.NameUser,Incomes.ID FROM Incomes left join Accounts On Accounts.ID=Incomes.Account left join Users On Accounts.NameUser=Users.ID left join ViewIncomes On Incomes.Type = ViewIncomes.ID Where Users.NameUser='" + users.NameUser + "'";
			string sqlTransfer = "SELECT Transfers.Pdate,ac1.Name,ac2.Name, Transfers.Amount, Users.NameUser, Transfers.ID FROM Transfers left join [Accounts] ac1 ON Transfers.Account1=ac1.ID left join  [Accounts] ac2  on Transfers.Account2=ac2.ID  left join Users On Users.ID=ac2.NameUser AND Users.ID=ac1.NameUser Where Users.NameUser='" + users.NameUser + "'";
			string sqlplaning = "SELECT PlaningCosts.Pdate,ViewCosts.ViewCosts, PlaningCosts.Amount, Accounts.Name ,PlaningCosts.Description, Users.NameUser, PlaningCosts.ID FROM PlaningCosts left join Accounts On Accounts.ID=PlaningCosts.Account left join Users On Accounts.NameUser=Users.ID left join ViewCosts On PlaningCosts.Type = ViewCosts.ID where Users.NameUser='" + users.NameUser + "'";
			string sqlPlaneIncome = "SELECT PlaningIncomes.Pdate,ViewIncomes.ViewIncomes, PlaningIncomes.Amount, Accounts.Name ,PlaningIncomes.Description, Users.NameUser, PlaningIncomes.ID FROM PlaningIncomes left join Accounts On Accounts.ID = PlaningIncomes.Account left join Users On Accounts.NameUser = Users.ID left join ViewIncomes On PlaningIncomes.Type = ViewIncomes.ID where Users.NameUser = '" + users.NameUser + "'";

			using (SqlConnection sqlconnection1 = new SqlConnection("Data Source = 1-ПК\\SQLSERVER; MultipleActiveResultSets=True; Trusted_Connection = Yes; DataBase = Finances"))
			{
				sqlconnection1.Open();

				SqlCommand command = new SqlCommand(sqlExpression, sqlconnection1);
				SqlDataReader reader = command.ExecuteReader();
				List<Cost1> costs = new List<Cost1>();


				if (reader.HasRows)
				{
					try
					{
						while (reader.Read())
						{
							string pdata = reader.GetDateTime(0).ToShortDateString();
							Cost1 cost = new Cost1(pdata, reader.GetString(3), reader.GetString(1), reader.GetDecimal(2), reader.GetString(4), reader.GetInt32(6));
							costs.Add(cost);
						}
						CostsGrid.ItemsSource = costs;
						CostsStatus.Text = "Количество записей: " + CostsGrid.Items.Count;
					}

					catch { }

					reader.Close();
				}
				else
				{
					reader.Close();
				}



				SqlCommand commandAccount = new SqlCommand(sqlAccount, sqlconnection1);
				SqlDataReader reader1 = commandAccount.ExecuteReader();

				List<Account1> accounts = new List<Account1>();


				if (reader1.HasRows)
				{

					if (reader1.HasRows)
					{
						try
						{
							while (reader1.Read())
							{
								decimal rest = reader1.GetDecimal(1) - reader1.GetDecimal(3) + reader1.GetDecimal(4);


								Account1 account = new Account1(reader1.GetString(0), reader1.GetString(2), reader1.GetDecimal(1), rest, reader1.GetString(5), reader1.GetInt32(7));

								accounts.Add(account);
							}
							AccountGrid.ItemsSource = accounts;
							AccountStatus.Text = "Количество счетов: " + AccountGrid.Items.Count;
						}
						catch { }
					}

					reader1.Close();


				}

				SqlCommand commandIncome = new SqlCommand(sqlIncome, sqlconnection1);
				SqlDataReader reader2 = commandIncome.ExecuteReader();
				List<Income1> incomes = new List<Income1>();

				if (reader2.HasRows)
				{
					try
					{
						while (reader2.Read())
						{

							string pdata = reader2.GetDateTime(0).ToShortDateString();
							Income1 income = new Income1(pdata, reader2.GetString(3), reader2.GetString(1), reader2.GetDecimal(2), reader2.GetString(4), reader2.GetInt32(6));
							incomes.Add(income);
						}
						IncomeGrid.ItemsSource = incomes;
						IncomeStatus.Text = "Количество записей: " + IncomeGrid.Items.Count;
					}
					catch { }
					reader2.Close();
				}

				SqlCommand commandTransfer = new SqlCommand(sqlTransfer, sqlconnection1);
				SqlDataReader reader3 = commandTransfer.ExecuteReader();
				List<Transfer1> transfers = new List<Transfer1>();

				if (reader3.HasRows)
				{
					try
					{
						while (reader3.Read())
						{

							string pdata = reader3.GetDateTime(0).ToShortDateString();
							Transfer1 transfer = new Transfer1(pdata, reader3.GetString(1), reader3.GetString(2), reader3.GetDecimal(3), reader3.GetInt32(5));
							transfers.Add(transfer);
						}
						TransfersGrid.ItemsSource = transfers;
						Status.Text = "Количество записей: " + TransfersGrid.Items.Count;
					}
					catch { }
					reader3.Close();

					SqlCommand command3 = new SqlCommand(sqlplaning, sqlconnection1);
					SqlDataReader reader6 = command3.ExecuteReader();
					List<PlaningCost1> planingcosts = new List<PlaningCost1>();

					if (reader6.HasRows)
					{
						try
						{
							while (reader6.Read())
							{
								string pdata = reader6.GetDateTime(0).ToShortDateString();
								PlaningCost1 cost = new PlaningCost1(pdata, reader6.GetString(3), reader6.GetString(1), reader6.GetDecimal(2), reader6.GetString(4), reader6.GetInt32(6));
								planingcosts.Add(cost);
							}
							CostGrid.ItemsSource = planingcosts;
							CostStatus.Text = "Количество записей: " + CostGrid.Items.Count;
						}
						catch { }
						reader6.Close();
					}


					SqlCommand planeIncome = new SqlCommand(sqlPlaneIncome, sqlconnection1);
					SqlDataReader reader4 = planeIncome.ExecuteReader();
					List<PlaningIncome1> incom = new List<PlaningIncome1>();

					if (reader4.HasRows)
					{
						try
						{
							while (reader4.Read())
							{

								string pdata = reader4.GetDateTime(0).ToShortDateString();
								PlaningIncome1 income = new PlaningIncome1(pdata, reader4.GetString(3), reader4.GetString(1), reader4.GetDecimal(2), reader4.GetString(4), reader4.GetInt32(6));
								incom.Add(income);
							}
							IncomsGrid.ItemsSource = incom;
							IncomesStatus.Text = "Количество записей: " + IncomsGrid.Items.Count;
						}
						catch { }
						reader4.Close();

					}


				}

			}

		}





		private void Button6_Checked_1(object sender, RoutedEventArgs e)
		{
			Calculator calculator = new Calculator();
			calculator.Show();

		}

		private void Button7_Checked(object sender, RoutedEventArgs e)
		{
			this.Close();
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
		}

		//private void Users_Checked(object sender, RoutedEventArgs e)
		//{
		//	Owner user = new Owner();
		//	user.Show();

		//}

		private void Button2_Checked(object sender, RoutedEventArgs e)
		{
			Categorie categorie = new Categorie();
			categorie.Show();
		}

		private void Button3_Checked(object sender, RoutedEventArgs e)
		{
			Currencs currencies = new Currencs();
			currencies.Show();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			addAccount addAccount = new addAccount();
			addAccount.Show();
		}

		private void AddButton1_Click(object sender, RoutedEventArgs e)
		{
			addCosts addCost = new addCosts();
			addCost.Show();
		}

		private void AddButton2_Click(object sender, RoutedEventArgs e)
		{
			addIncome addIncome = new addIncome();
			addIncome.Show();
		}

		private void AddButton3_Click(object sender, RoutedEventArgs e)
		{
			addTrans addTrans = new addTrans();
			addTrans.Show();
		}

		private void AddButton4_Click(object sender, RoutedEventArgs e)
		{
			addPlaneCosts addCost = new addPlaneCosts();
			addCost.Show();
		}
		private void AddButton5_Click(object sender, RoutedEventArgs e)
		{
			addPlaneIncome addIncome = new addPlaneIncome();
			addIncome.Show();
		}
		public void Refresh()
		{
			db.Users.Load();
			db.ViewCosts.Load();
			db.ViewIncomes.Load();

			db.PlaningCosts.Load();
			db.Incomes.Load();
			db.Currencies.Load();
			db.Costs.Load();
			db.Accounts.Load();
			db.Transfers.Load();
			string sqlExpression = "SELECT Costs.Pdate,ViewCosts.ViewCosts, Costs.Amount, Accounts.Name ,Costs.Description, Users.NameUser, Costs.ID FROM Costs left join Accounts On Accounts.ID=Costs.Account left join Users On Accounts.NameUser=Users.ID left join ViewCosts On Costs.Type = ViewCosts.ID Where Users.NameUser='" + user_name + "'";
			string sqlAccount = " Select Accounts.Name, Accounts.Amount, Currencies.Shortname , ISNULL(d.[Costs],0), ISNULL(b.[Incomes],0), Accounts.Description , Users.NameUser, Accounts.ID  From Accounts full join Users On Accounts.NameUser=Users.ID full join ( Select  sum(Costs.Amount)[Costs], Account  From Costs  group by Account) d  on d.Account=Accounts.ID  left join Currencies on Accounts.Currency=Currencies.ID full join (Select  sum(Incomes.Amount)[Incomes], Account From Incomes  group by Account) b  on b.Account=Accounts.ID Where Users.NameUser='" + user_name + "'";
			string sqlIncome = "SELECT Incomes.Pdate,ViewIncomes.ViewIncomes, Incomes.Amount, Accounts.Name ,Incomes.Description, Users.NameUser, Incomes.ID FROM Incomes left join Accounts On Accounts.ID=Incomes.Account left join Users On Accounts.NameUser=Users.ID left join ViewIncomes On Incomes.Type = ViewIncomes.ID Where Users.NameUser='" + user_name + "'";
			string sqlTransfer = "SELECT Transfers.Pdate,ac1.Name,ac2.Name, Transfers.Amount, Users.NameUser, Transfers.ID FROM Transfers left join [Accounts] ac1 ON Transfers.Account1=ac1.ID left join  [Accounts] ac2  on Transfers.Account2=ac2.ID  left join Users On Users.ID=ac2.NameUser AND Users.ID=ac1.NameUser Where Users.NameUser='" + user_name + "'";
			string sqlplaning = "SELECT PlaningCosts.Pdate,ViewCosts.ViewCosts, PlaningCosts.Amount, Accounts.Name ,PlaningCosts.Description, Users.NameUser, PlaningCosts.ID FROM PlaningCosts left join Accounts On Accounts.ID=PlaningCosts.Account left join Users On Accounts.NameUser=Users.ID left join ViewCosts On PlaningCosts.Type = ViewCosts.ID where Users.NameUser='" + user_name + "'";
			string sqlPlaneIncome = "SELECT PlaningIncomes.Pdate,ViewIncomes.ViewIncomes, PlaningIncomes.Amount, Accounts.Name ,PlaningIncomes.Description, Users.NameUser, PlaningIncomes.ID FROM PlaningIncomes left join Accounts On Accounts.ID = PlaningIncomes.Account left join Users On Accounts.NameUser = Users.ID left join ViewIncomes On PlaningIncomes.Type = ViewIncomes.ID where Users.NameUser = '" + user_name + "'";

			using (SqlConnection sqlconnection1 = new SqlConnection("Data Source = 1-ПК\\SQLSERVER; MultipleActiveResultSets=True; Trusted_Connection = Yes; DataBase = Finances"))
			{
				sqlconnection1.Open();

				SqlCommand command = new SqlCommand(sqlExpression, sqlconnection1);
				SqlDataReader reader = command.ExecuteReader();
				List<Cost1> costs = new List<Cost1>();


				if (reader.HasRows)
				{
					try
					{
						while (reader.Read())
						{
							string pdata = reader.GetDateTime(0).ToShortDateString();
							Cost1 cost = new Cost1(pdata, reader.GetString(3), reader.GetString(1), reader.GetDecimal(2), reader.GetString(4), reader.GetInt32(6));
							costs.Add(cost);
						}
						CostsGrid.ItemsSource = costs;
						CostsStatus.Text = "Количество записей: " + CostsGrid.Items.Count;
					}

					catch { }

					reader.Close();
				}
				else
				{
					reader.Close();
				}



				SqlCommand commandAccount = new SqlCommand(sqlAccount, sqlconnection1);
				SqlDataReader reader1 = commandAccount.ExecuteReader();

				List<Account1> accounts = new List<Account1>();


				if (reader1.HasRows)
				{

					if (reader1.HasRows)
					{
						try
						{
							while (reader1.Read())
							{
								decimal rest = reader1.GetDecimal(1) - reader1.GetDecimal(3) + reader1.GetDecimal(4);


								Account1 account = new Account1(reader1.GetString(0), reader1.GetString(2), reader1.GetDecimal(1), rest, reader1.GetString(5), reader1.GetInt32(7));

								accounts.Add(account);
							}
							AccountGrid.ItemsSource = accounts;
							AccountStatus.Text = "Количество счетов: " + AccountGrid.Items.Count;
						}
						catch { }
					}

					reader1.Close();


				}

				SqlCommand commandIncome = new SqlCommand(sqlIncome, sqlconnection1);
				SqlDataReader reader2 = commandIncome.ExecuteReader();
				List<Income1> incomes = new List<Income1>();

				if (reader2.HasRows)
				{
					try
					{
						while (reader2.Read())
						{

							string pdata = reader2.GetDateTime(0).ToShortDateString();
							Income1 income = new Income1(pdata, reader2.GetString(3), reader2.GetString(1), reader2.GetDecimal(2), reader2.GetString(4), reader2.GetInt32(6));
							incomes.Add(income);
						}
						IncomeGrid.ItemsSource = incomes;
						IncomeStatus.Text = "Количество записей: " + IncomeGrid.Items.Count;
					}
					catch { }
					reader2.Close();
				}

				SqlCommand commandTransfer = new SqlCommand(sqlTransfer, sqlconnection1);
				SqlDataReader reader3 = commandTransfer.ExecuteReader();
				List<Transfer1> transfers = new List<Transfer1>();

				if (reader3.HasRows)
				{
					try
					{
						while (reader3.Read())
						{

							string pdata = reader3.GetDateTime(0).ToShortDateString();
							Transfer1 transfer = new Transfer1(pdata, reader3.GetString(1), reader3.GetString(2), reader3.GetDecimal(3), reader3.GetInt32(5));
							transfers.Add(transfer);
						}
						TransfersGrid.ItemsSource = transfers;
						Status.Text = "Количество записей: " + TransfersGrid.Items.Count;
					}
					catch { }
					reader3.Close();

					SqlCommand command3 = new SqlCommand(sqlplaning, sqlconnection1);
					SqlDataReader reader6 = command3.ExecuteReader();
					List<PlaningCost1> planingcosts = new List<PlaningCost1>();

					if (reader6.HasRows)
					{
						try
						{
							while (reader6.Read())
							{
								string pdata = reader6.GetDateTime(0).ToShortDateString();
								PlaningCost1 cost = new PlaningCost1(pdata, reader6.GetString(3), reader6.GetString(1), reader6.GetDecimal(2), reader6.GetString(4), reader6.GetInt32(6));
								planingcosts.Add(cost);
							}
							CostGrid.ItemsSource = planingcosts;
							CostStatus.Text = "Количество записей: " + CostGrid.Items.Count;
						}
						catch { }
						reader6.Close();
					}


					SqlCommand planeIncome = new SqlCommand(sqlPlaneIncome, sqlconnection1);
					SqlDataReader reader4 = planeIncome.ExecuteReader();
					List<PlaningIncome1> incom = new List<PlaningIncome1>();

					if (reader4.HasRows)
					{
						try
						{
							while (reader4.Read())
							{

								string pdata = reader4.GetDateTime(0).ToShortDateString();
								PlaningIncome1 income = new PlaningIncome1(pdata, reader4.GetString(3), reader4.GetString(1), reader4.GetDecimal(2), reader4.GetString(4), reader4.GetInt32(6));
								incom.Add(income);
							}
							IncomsGrid.ItemsSource = incom;
							IncomesStatus.Text = "Количество записей: " + IncomsGrid.Items.Count;
						}
						catch { }
						reader4.Close();

					}


				}

			}


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







		private void DeleteButton3_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить этот перревод? При удалении этой записи расход и доход, связанные с этим переводом также удалятся.  Удалить перевод?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{

					if (TransfersGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < TransfersGrid.SelectedItems.Count; i++)
						{
							Transfer1 transfer = TransfersGrid.SelectedItems[i] as Transfer1;
							if (transfer != null)
							{
								var item = db.Transfers.Find(transfer.ID);

								db.Transfers.Remove(item);
								db.SaveChanges();

								var table = db.Costs;
								foreach (var cost in table)
								{
									if ((cost.Amount == transfer.Amount) && (cost.Account == item.Account1) && (item.Pdate == cost.Pdate))
									{
										db.Costs.Remove(cost);
									}
								}
								db.SaveChanges();
								var table1 = db.Incomes;
								foreach (var income in table1)
								{
									if (income.Amount == transfer.Amount && (income.Account == item.Account2) && (item.Pdate == income.Pdate))
									{
										db.Incomes.Remove(income);
									}
								}
								db.SaveChanges();
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
			this.Close();

		}

		private void RefreshButton1_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
		}

		private void RefreshButton2_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
		}

		private void RefreshButton3_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
		}

		private void RefreshButton4_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
		}

		private void RefreshButton5_Click(object sender, RoutedEventArgs e)
		{
			db.SaveChanges();
			Refresh();
		}

		private void DeleteButton5_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{


					if (IncomsGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < IncomsGrid.SelectedItems.Count; i++)
						{
							PlaningIncome1 planingIncome = IncomsGrid.SelectedItems[i] as PlaningIncome1;
							if (planingIncome != null)
							{
								var item = db.PlaningIncomes.Find(planingIncome.ID);
								db.PlaningIncomes.Remove(item);
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();

		}

		private void DeleteButton4_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{


					if (CostGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < CostGrid.SelectedItems.Count; i++)
						{
							PlaningCost1 cost = CostGrid.SelectedItems[i] as PlaningCost1;
							if (cost != null)
							{
								var item = db.PlaningCosts.Find(cost.ID);
								db.PlaningCosts.Remove(item);
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить этот счёт? При удалении этого счёта все расходы, доходы, переводы и запланированные денежные операции, связанные с этим счётом  также будут удалены .  Удалить счёт?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{

					if (AccountGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < AccountGrid.SelectedItems.Count; i++)
						{
							Account1 account = AccountGrid.SelectedItems[i] as Account1;


							if (account != null)
							{
								var item = db.Accounts.Find(account.ID);

								var table = db.Costs;
								foreach (var cost in table)
								{
									if (cost.Account == account.ID)
									{
										db.Costs.Remove(cost);
									}
								}
								db.SaveChanges();
								var table1 = db.Incomes;
								foreach (var income in table1)
								{
									if (income.Account == account.ID)
									{
										db.Incomes.Remove(income);
									}
								}
								db.SaveChanges();
								foreach (var transfer in db.Transfers)
								{
									if ((transfer.Account1 == account.ID) || (transfer.Account2 == account.ID))
									{
										db.Transfers.Remove(transfer);
									}
								}
								db.SaveChanges();
								foreach (var plinc in db.PlaningIncomes)
								{
									if (plinc.Account == account.ID)
									{
										db.PlaningIncomes.Remove(plinc);
									}
								}
								db.SaveChanges();
								foreach (var plc in db.PlaningCosts)
								{
									if (plc.Account == account.ID)
									{
										db.PlaningCosts.Remove(plc);
									}
								}
								db.SaveChanges();
								db.Accounts.Remove(item);
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();
		}

		private void DeleteButton1_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{


					if (CostsGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < CostsGrid.SelectedItems.Count; i++)
						{
							Cost1 cost = CostsGrid.SelectedItems[i] as Cost1;
							if (cost != null)
							{
								var item = db.Costs.Find(cost.ID);
								db.Costs.Remove(item);
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();
		}


		private void DeleteButton2_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				using (FContext db = new FContext())
				{


					if (IncomeGrid.SelectedItems.Count > 0)
					{
						for (int i = 0; i < IncomeGrid.SelectedItems.Count; i++)
						{
							Income1 income = IncomeGrid.SelectedItems[i] as Income1;
							if (income != null)
							{
								var item = db.Incomes.Find(income.ID);
								db.Incomes.Remove(item);
							}
						}
					}
					db.SaveChanges();

				}

			}
			Refresh();

		}

		private void ActivateButton4_Click(object sender, RoutedEventArgs e)
		{
			using (FContext db = new FContext())
			{


				if (CostGrid.SelectedItems.Count > 0)
				{
					for (int i = 0; i < CostGrid.SelectedItems.Count; i++)
					{
						PlaningCost1 cost = CostGrid.SelectedItems[i] as PlaningCost1;
						if (cost != null)
						{
							var item = db.PlaningCosts.Find(cost.ID);
							db.Costs.Add(new Cost()
							{
								Pdate=item.Pdate,
								Account=item.Account,
								Amount=item.Amount,
								Type=item.Type,
								Description=item.Description
							});
							MessageBox.Show("Расход активирован!");
							db.PlaningCosts.Remove(item);
						}
					}
				}
				db.SaveChanges();

			}
			Refresh();

		}

		private void ActivateButton5_Click(object sender, RoutedEventArgs e)
		{
			using (FContext db = new FContext())
			{


				if (IncomsGrid.SelectedItems.Count > 0)
				{
					for (int i = 0; i < IncomsGrid.SelectedItems.Count; i++)
					{
						PlaningIncome1 income = IncomsGrid.SelectedItems[i] as PlaningIncome1;
						if (income != null)
						{
							var item = db.PlaningIncomes.Find(income.ID);
							db.Incomes.Add(new Income()
							{
								Pdate = item.Pdate,
								Account = item.Account,
								Amount = item.Amount,
								Type = item.Type,
								Description = item.Description
							});
							MessageBox.Show("Доход активирован!");
							db.PlaningIncomes.Remove(item);
						}
					}
				}
				db.SaveChanges();

			}
			Refresh();
		}
	}
}



	






