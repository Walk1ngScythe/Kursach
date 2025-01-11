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
    public partial class MyOrders : Form
    {
        int numberUser;
        public MyOrders(int UserID)
        {
            numberUser = UserID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form fm = Application.OpenForms["CustomerForm"];
            fm.Show();


        }

        private void MyOrders_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString); // тут надо вставить переменную  

            sqlConnect.Open();
            SqlCommand logRequst = new SqlCommand();
            logRequst.CommandText = $"select * from MyOrders where id_customer = {numberUser}";

            logRequst.Connection = sqlConnect;


            // SqlAdapter - прослойка между источником данных и базой данных
            SqlDataAdapter adapter = new SqlDataAdapter(logRequst);
            DataSet dataSet = new DataSet();
            // заполняем источник данных полученными из адаптера записями
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            

            sqlConnect.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }
    }
}
