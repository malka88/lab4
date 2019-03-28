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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class CTest
        {
            public int number { get; set; }
            public string fio { get; set; }
            public int rateM { get; set; }
            public int rateP { get; set; }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

            
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string db_name = "C:\\Users\\Admin\\Documents\\ри-89\\progLab4\\db.db";
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();

            Window1 dialog = new Window1();
                 if (dialog.ShowDialog() == true)
                 {
                    string sql = "INSERT INTO fio (number, fio) VALUES (" + dialog.num.Text + ",'" + dialog.fio.Text + "')";
                    string sql1 = "INSERT INTO rate (number, rateM, rateP) VALUES (" + dialog.num.Text + "," + dialog.rateM.Text + "," + dialog.rateP.Text + ")";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();
                 }
        }
    }
}


