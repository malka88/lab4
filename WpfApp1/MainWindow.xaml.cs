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
using Microsoft.Win32;

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

            string db_name = "C:\\Users\\Admin\\Documents\\ри-89\\progLab4\\db.db";
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();

           
                string sql = "SELECT * FROM fio ORDER BY number";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                string sql1 = "SELECT * FROM rate ORDER BY number";
                SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                SQLiteDataReader reader1 = command1.ExecuteReader();

                while (reader.Read() && reader1.Read())
                {
                    var datag = new CTest { number = int.Parse(reader["number"].ToString()), fio = reader["fio"].ToString(), rateM = int.Parse(reader1["rateM"].ToString()), rateP = int.Parse(reader1["rateP"].ToString()) };
                    data.Items.Add(datag);
                }

            reader.Close();
            reader1.Close();
            data.Items.Refresh();
            
            m_dbConnection.Close();
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
                string sql = "INSERT INTO fio (fio) VALUES ('" + dialog.fio.Text + "'" + ")";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                string sql4 = "INSERT INTO rate (rateM, rateP) VALUES (" + dialog.rateM.Text + "," + dialog.rateP.Text+")";
                SQLiteCommand command4 = new SQLiteCommand(sql4, m_dbConnection);
                command4.ExecuteNonQuery();


                string sql1 = "SELECT fio.number,fio.fio,rate.rateM,rate.rateP FROM fio,rate ORDER BY fio.number DESC";
                SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                SQLiteDataReader reader1 = command1.ExecuteReader();


                //// while (reader.Read())
                //{
                reader1.Read();
                //reader2.Read();
                var datag = new CTest { number = int.Parse(reader1["number"].ToString()), fio = reader1["fio"].ToString(), rateM = int.Parse(reader1["rateM"].ToString()), rateP = int.Parse(reader1["rateP"].ToString()) };
                data.Items.Add(datag);
                //    }

                reader1.Close();
                //reader2.Close();
                data.Items.Refresh();

            }
            
            m_dbConnection.Close();

            
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            string db_name = "C:\\Users\\Admin\\Documents\\ри-89\\progLab4\\db.db";
            
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();

            CTest redData = (CTest)data.SelectedItem;
            
            
            string sql = "DELETE FROM fio WHERE number = " + redData.number.ToString();
            string sql1 = "DELETE FROM rate WHERE number = " + redData.number.ToString();

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();

            if (data.SelectedIndex > -1)
            {
                data.Items.RemoveAt(data.SelectedIndex);
            }
            data.Items.Refresh();
            
            m_dbConnection.Close();


        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            string db_name = "C:\\Users\\Admin\\Documents\\ри-89\\progLab4\\db.db";
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
            m_dbConnection.Open();
            Window1 dialog = new Window1();

            CTest redData = (CTest)data.SelectedItem;
            dialog.fio.AppendText(redData.fio);
            dialog.rateM.AppendText(redData.rateM.ToString());
            dialog.rateP.AppendText(redData.rateP.ToString());

            if (dialog.ShowDialog() == true)
            {
                
                string sql = "UPDATE fio SET fio = '" + dialog.fio.Text + "' WHERE number = " + redData.number.ToString();
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                string sql1 = "UPDATE rate SET rateM = " + dialog.rateM.Text + ", rateP = " + dialog.rateP.Text + " WHERE number = " + redData.number.ToString();
                SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                command1.ExecuteNonQuery();

                redData.fio = dialog.fio.Text;
                redData.rateM = int.Parse(dialog.rateM.Text);
                redData.rateP = int.Parse(dialog.rateP.Text);
            }

            if (data.SelectedIndex > -1)
            {
                data.Items.Insert(data.SelectedIndex, redData);
                data.Items.RemoveAt(data.SelectedIndex);
            }
            data.Items.Refresh();
            m_dbConnection.Close();

           
        }

        
    }
}


