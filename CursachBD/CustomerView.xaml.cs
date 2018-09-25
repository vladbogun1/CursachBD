using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CursachBD
{
    /// <summary>
    /// Логика взаимодействия для CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        SqlConnection sqlConnection;
        public CustomerView()
        {
            InitializeComponent();
        }
        private void RefreshMethod(object sender, RoutedEventArgs e)
        {
            CustomerView view = new CustomerView();
            view.ShowDialog();
        }
        private void CloseIntent(object sender, RoutedEventArgs e)
        {
            Close();
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikto\Desktop\BD\DotNet\CursachBD\CursachBD\Database.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionStr);
            await sqlConnection.OpenAsync();

            SqlDataReader sqlDataReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Drug Table]", sqlConnection);

            try
            {
                sqlDataReader = await command.ExecuteReaderAsync();
                while (await sqlDataReader.ReadAsync())
                {
                    itemsGrid.Items.Add(sqlDataReader["ID"] + " " + sqlDataReader["DrugName"]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error );
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();


            }
        }
    }
    
}
