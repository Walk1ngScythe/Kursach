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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test_subd
{
    public partial class NewOrder : Form
    {
        int UserId;
        string condition;
        int invalidnost;
        public int car;
       
        public NewOrder(int identy, string condition, int invalid)

        {

            UserId = identy;
            InitializeComponent();
            this.condition = condition;
            this.invalidnost = invalid;

        }

        private void NewOrder_Load(object sender, EventArgs e)
        {
            
         

            label1.Text = "Дата начала";
            label2.Text = "Дата окончания";
            
            label4.Text = "Автомобиль";
            label5.Text = "Точка начала";
            label6.Text = "Точка окончания";
            
            
            int point1 = 0;
            int point2 = 0;
           
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                conn.Open();

                SqlCommand qlogRequst = new SqlCommand($"SELECT *  FROM Cars where status = 0 and for_disabled_person = @InvalidnostValue and id_car = {condition}", conn);
                qlogRequst.Parameters.AddWithValue("@InvalidnostValue", invalidnost);
                SqlDataReader reader = qlogRequst.ExecuteReader();
                if (reader.Read())
                {
                    textBox3.Text = reader["model"].ToString();
                    car = Convert.ToInt32(reader["id_car"]);
                }
                reader.Close();



                
                SqlCommand ttypeGoodCommand = new SqlCommand("SELECT id_point, address  FROM points", conn);
                SqlDataReader reader2 = ttypeGoodCommand.ExecuteReader();
                List<Categories> lstCategories2 = new List<Categories>();

                while (reader2.Read())
                {
                    lstCategories2.Add(new Categories(Convert.ToInt32(reader2["id_point"]), reader2["address"].ToString()));
                }

                comboBox3.DataSource = lstCategories2;
                comboBox3.DisplayMember = "name";
                comboBox3.ValueMember = "id";
                comboBox3.SelectedValue = point1;
                reader2.Close();
                SqlCommand typeGoodCommand2 = new SqlCommand("SELECT id_point, address  FROM points", conn);
                SqlDataReader reader3 = ttypeGoodCommand.ExecuteReader();
                List<Categories> lstCategories3 = new List<Categories>();

                while (reader3.Read())
                {
                    lstCategories3.Add(new Categories(Convert.ToInt32(reader3["id_point"]), reader3["address"].ToString()));
                }

                comboBox4.DataSource = lstCategories3;
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "id";
                comboBox4.SelectedValue = point2;
                reader3.Close();
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form fm = Application.OpenForms["CustomerForm"];
            fm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand();
                    logRequest.Connection = conn;



                    DateTime start_date = Convert.ToDateTime(textBox1.Text);
                    DateTime end_date = Convert.ToDateTime(textBox2.Text);
                    string startDateStr = start_date.ToString("yyyy-MM-dd HH:mm:ss");
                    string endDateStr = end_date.ToString("yyyy-MM-dd HH:mm:ss");
                    logRequest.CommandText = "INSERT INTO orders (id_customer, id_rented_car, start_date, expiration_date, start_point_id, end_point_id) " +
                          "VALUES (@id_customer, @id_rented_car, @start_date, @end_date, @start_point_id, @end_point_id)";

                    logRequest.Parameters.AddWithValue("@id_customer", UserId);
                    logRequest.Parameters.AddWithValue("@id_rented_car", car);
                    logRequest.Parameters.AddWithValue("@start_date", startDateStr);
                    logRequest.Parameters.AddWithValue("@end_date", endDateStr);
                    logRequest.Parameters.AddWithValue("@start_point_id", comboBox3.SelectedIndex + 1);
                    logRequest.Parameters.AddWithValue("@end_point_id", comboBox4.SelectedIndex + 1);




                    logRequest.ExecuteNonQuery();
                    MessageBox.Show("Заказ успешно оформлен");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
