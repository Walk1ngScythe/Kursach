using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test_subd
{
    public partial class CustomerForm : Form
    {
        public SqlConnection connect;
        int roleForm;
        int invalidnost;
        int UserId;
        public CustomerForm(SqlConnection cnct, int RoleFM, int disabled_person, int numberUser)
        {
            connect = cnct;
            roleForm = RoleFM;
            invalidnost = disabled_person;
            UserId = numberUser;
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            SqlConnection qsqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
            qsqlConnect.Open();

            // Используйте параметры запроса для передачи значения invalidnost
            string sqlQuery = "select * from Машины where Статус = 0 and [Для инвалидов] = @InvalidnostValue";

            SqlCommand qlogRequst = new SqlCommand(sqlQuery, qsqlConnect);
            qlogRequst.Parameters.AddWithValue("@InvalidnostValue", invalidnost);

            SqlDataAdapter qadapter = new SqlDataAdapter(qlogRequst);
            DataSet qdataSet = new DataSet();
            qadapter.Fill(qdataSet);

            dataGridView1.DataSource = qdataSet.Tables[0];

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.CellClick += DataGridViewCellClick;

            if (dataGridView1.Rows.Count == 0)
            {
                // DataGridView пуст
                // Можно выполнить нужные действия, например, вывести сообщение о том, что таблица пуста
                MessageBox.Show("Свободных авто для вас к сожалению нету");
            }
            else
            {


                qsqlConnect.Close();
                SqlConnection ssqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand llogRequst = new SqlCommand();
                llogRequst.CommandText = $"SELECT name_car_feature as 'Особенности авто' FROM feature f JOIN feature_to_cars cf ON(f.id_feature = cf.feature) JOIN Cars c ON(cf.cars= c.id_car) WHERE id_car = {1}";

                llogRequst.Connection = ssqlConnect;



                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter aadapter = new SqlDataAdapter(llogRequst);
                DataSet ddataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                aadapter.Fill(ddataSet);

                dataGridView2.DataSource = ddataSet.Tables[0];
                ssqlConnect.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var columnNames = dataGridView1.Columns.Cast<DataGridViewColumn>()
                       .Select(x => "[" + x.HeaderText + "]")
                       .ToList();

            // Создаем строку фильтрации
            string filterExpression = string.Empty;
            foreach (var columnName in columnNames)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " OR ";

                filterExpression += $"CONVERT({columnName}, 'System.String') LIKE '%{textBox1.Text}%'";
            }

            // Применяем фильтр к строкам DGV1
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = filterExpression;

        }
        private void DataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                SqlConnection ssqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand llogRequst = new SqlCommand();
                llogRequst.CommandText = $"SELECT name_car_feature as 'Особенности авто' FROM feature f JOIN feature_to_cars cf ON(f.id_feature = cf.feature) JOIN Cars c ON(cf.cars= c.id_car) WHERE id_car = {Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)}";

                llogRequst.Connection = ssqlConnect;



                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter aadapter = new SqlDataAdapter(llogRequst);
                DataSet ddataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                aadapter.Fill(ddataSet);

                dataGridView2.DataSource = ddataSet.Tables[0];
                ssqlConnect.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm2 = new frmAuthorization();
            fm2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm3 = new MyOrders(UserId);
            fm3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
            string condition = selectedRow.Cells[0].Value.ToString();
            this.Hide();
            Form fm3 = new NewOrder(UserId, condition, invalidnost);
            fm3.ShowDialog();
        }
    }
}
