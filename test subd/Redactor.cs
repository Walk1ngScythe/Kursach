using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test_subd
{
    public partial class Redactor : Form
    {
        private SqlConnection connect;
        private string boxItem;
        string condition;
        string condition2;
        public int mashina = 0;
        public int osoba = 0;


        public Redactor(SqlConnection cnct, string item, string editID, string editID2)
        {
            connect = cnct;
            boxItem = item;
            condition = editID;
            condition2 = editID2;
            
            InitializeComponent();
        }

        private void Redactor_Load(object sender, EventArgs e)
        {
            
            label1.Text = $"Редактирование таблицы: {boxItem}";
            if (connect.State == ConnectionState.Open) 
            {
                connect.Close();
            }
            if (boxItem == "Адресса")
            {
                label2.Text = "Введите новый Адрес";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM points WHERE id_point = {condition}";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["address"].ToString();
                }

                reader.Close();
                conn.Close();
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            if (boxItem == "Роли")
            {
                label2.Text = "Введите новую Роль";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM Roles WHERE id_role = {condition}";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["name_role"].ToString();
                }
                reader.Close();
                conn.Close();
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            if (boxItem == "Категории")
            {
                label2.Text = "Введите новую Категорию";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM category_of_cars WHERE id_category = {condition}";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["name_category"].ToString();
                }
                reader.Close();
                conn.Close();
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            if (boxItem == "Особенности")
            {
                label2.Text = "Введите новую Особенность";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM feature WHERE id_feature = '{condition}'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["name_car_feature"].ToString();
                }
                reader.Close();
                conn.Close();
                this.Width = 250; 
                this.Height = 200; 
        
                button1.Left = 20; 
                button1.Top = 90;
                button2.Left = 20; 
                button2.Top = 115;
            }
            if (boxItem == "Пользователи")
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                int role = 0;
                comboBox1.Items.Add("Нет");
                comboBox1.Items.Add("Да");
                label2.Text = "Имя";
                label3.Text = "Почта";
                label4.Text = "Пароль";
                label5.Text = "Инвалидность";
                label6.Text = "Роль";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM Users WHERE id_user = {condition}";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["username"].ToString();
                    textBox2.Text = reader["email"].ToString();
                    textBox3.Text = reader["password"].ToString();
                    if (Convert.ToInt32(reader["disabled_person"]) == 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else 
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    role = Convert.ToInt32(reader["id_role"]);
                }
                reader.Close();
                conn.Close();
                using (SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn2.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_role, name_role FROM Roles", conn2);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_role"]), rdr["name_role"].ToString()));
                        }
                        comboBox2.DataSource = lstCategories;
                        comboBox2.DisplayMember = "name";
                        comboBox2.ValueMember = "id";
                        comboBox2.SelectedIndex = role-1;
                    }
                    conn2.Close();
                    
                }
                button1.Left = 20;
                button2.Left = 280;
                button1.Top = 150;
                button2.Top = 150;
                this.Width = 420;
                this.Height = 230;
            }
            if (boxItem == "Машины")
            {
                int category = 0;
                this.Width = 280;
                this.Height = 280;
                button1.Left = 20;
                button1.Top = 210;
                
                button2.Top = 210;
                button2.Left = 160;

                textBox2.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                label3.Visible = true;
                label6.Visible = true;
                label5.Visible = true;
                label7.Visible = true;
                comboBox1.Items.Add("Нет");
                comboBox1.Items.Add("Да");
                comboBox3.Items.Add("Свободна");
                comboBox3.Items.Add("Занята");
                label2.Text = "Модель";
                label3.Text = "Цена";
                label5.Text = "Для инвалидов";
                label6.Text = "Категория";
                label7.Text = "Статус";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString);
                conn.Open();
                string query = $"SELECT * FROM Cars WHERE id_car = {condition}";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["model"].ToString();
                    textBox2.Text = reader["price"].ToString();
                    if (Convert.ToInt32(reader["for_disabled_person"]) == 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Convert.ToInt32(reader["status"]) == 0)
                    {
                        comboBox3.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox3.SelectedIndex = 1;
                    }
                    category = Convert.ToInt32(reader["category"]);
                   
                }
                reader.Close();
                conn.Close();

                using (SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn2.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_category, name_category FROM category_of_cars", conn2);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_category"]), rdr["name_category"].ToString()));
                        }
                        comboBox2.DataSource = lstCategories;
                        comboBox2.DisplayMember = "name";
                        comboBox2.ValueMember = "id";
                        comboBox2.SelectedIndex = category - 1;
                    }
                    conn2.Close();

                }

            }
            if (boxItem == "ОсобенностиАвто")
            {
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                textBox2.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label2.Visible = false;
                label5.Visible = true;
                label6.Visible = true;
                label5.Text = "Модель Авто";
                label6.Text = "Особенность Авто";
                this.Width = 300;
                this.Height = 160;
                comboBox1.Left = 10;
                comboBox1.Top = 50;
                label5.Top = 35;
                comboBox2.Top = 50;
                label6.Top = 35;
                label3.Left = 8;
                button1.Left = 23;
                button1.Top = 80;
                button2.Left = 155;
                button2.Top = 80;


                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_feature, name_car_feature FROM feature", conn);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_feature"]), rdr["name_car_feature"].ToString()));
                        }
                        comboBox2.DataSource = lstCategories;
                        comboBox2.DisplayMember = "name";
                        comboBox2.ValueMember = "id";
                        comboBox2.Text = $"{condition2}";
                        osoba = comboBox2.SelectedIndex+1;
                    }
                    SqlCommand typeGoodCommand = new SqlCommand("SELECT id_car, model  FROM Cars", conn);
                    SqlDataReader reader1 = typeGoodCommand.ExecuteReader();
                    List<Categories> lstCategories1 = new List<Categories>();

                    while (reader1.Read())
                    {
                        lstCategories1.Add(new Categories(Convert.ToInt32(reader1["id_car"]), reader1["model"].ToString()));
                    }

                    comboBox1.DataSource = lstCategories1;
                    comboBox1.DisplayMember = "name";
                    comboBox1.ValueMember = "id";
                    comboBox1.Text = $"{condition}";
                    mashina = comboBox1.SelectedIndex+1;
                    reader1.Close();
                }
              

            }
            if (boxItem == "Заказы")
            {
                this.Width = 300;
                button2.Left = 175;
                textBox2.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label2.Text = "Дата начала";
                label3.Text = "Дата окончания";
                label5.Text = "Заказчик";
                label6.Text = "Автомобиль";
                label7.Text = "Точка начала";
                label8.Text = "Точка окончания";
                int user = 0;
                int car = 0;
                int point1 = 0;
                int point2 = 0;

                SqlConnection conn3 = new SqlConnection(Properties.Settings.Default.connectionString);
                conn3.Open();
                string query = $"SELECT * FROM orders WHERE id_order = {condition}";
                SqlCommand command = new SqlCommand(query, conn3);
                SqlDataReader reader4 = command.ExecuteReader();
                if (reader4.Read())
                {
                    textBox1.Text = reader4["start_date"].ToString();
                    textBox2.Text = reader4["expiration_date"].ToString();
                    user = Convert.ToInt32(reader4["id_customer"]);
                    car = Convert.ToInt32(reader4["id_rented_car"]);
                    point1 = Convert.ToInt32(reader4["start_point_id"]);
                    if (!reader4.IsDBNull(reader4.GetOrdinal("end_point_id")))
                    {
                        point2 = reader4.GetInt32(reader4.GetOrdinal("end_point_id"));
                    }
                    else
                    {
                        point2 = 0;
                    }

                }
                reader4.Close();
                conn3.Close();

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_user, username FROM Users", conn);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_user"]), rdr["username"].ToString()));
                        }
                        comboBox1.DataSource = lstCategories;
                        comboBox1.DisplayMember = "name";
                        comboBox1.ValueMember = "id";
                        comboBox1.SelectedValue = user;
                        
                    }
                    SqlCommand typeGoodCommand = new SqlCommand("SELECT id_car, model  FROM Cars ", conn);
                    SqlDataReader reader1 = typeGoodCommand.ExecuteReader();
                    List<Categories> lstCategories1 = new List<Categories>();

                    while (reader1.Read())
                    {
                        lstCategories1.Add(new Categories(Convert.ToInt32(reader1["id_car"]), reader1["model"].ToString()));
                    }

                    comboBox2.DataSource = lstCategories1;
                    comboBox2.DisplayMember = "name";
                    comboBox2.ValueMember = "id";
                    comboBox2.SelectedValue = car;
                    reader1.Close();
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
                /* SqlConnection conn3 = new SqlConnection(Properties.Settings.Default.connectionString);
                conn3.Open();
                string query = $"SELECT * FROM orders WHERE id_order = {condition}";
                SqlCommand command = new SqlCommand(query, conn3);
                SqlDataReader reader4 = command.ExecuteReader();
                if (reader4.Read())
                {
                    textBox1.Text = reader4["start_date"].ToString();
                    textBox2.Text = reader4["expiration_date"].ToString();
                    comboBox1.SelectedValue = reader4["id_customer"];
                    comboBox2.SelectedValue = reader4["id_rented_car"];
                    comboBox3.SelectedValue = reader4["start_point_id"];
                    comboBox4.SelectedValue = reader4["end_point_id"];
                }
                reader4.Close();
                conn3.Close()
                */
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            FormAdmin fm = Application.OpenForms["Form2"] as FormAdmin;
            if (fm != null)
            {
                // Показываем форму 2
                fm.Show();

                // Вызываем метод обновления данных на форме 2
                fm.RefreshData();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand();
                    logRequest.Connection = conn;
                    if (boxItem == "Адресса")
                    {
                        logRequest.CommandText = $"Update points set address = '{textBox1.Text}'  where id_point = {condition}";

                    }
                    if (boxItem == "Категории")
                    {
                        logRequest.CommandText = $"Update category_of_cars set name_category = '{textBox1.Text}'  where id_category = {condition}";

                    }
                    if (boxItem == "Роли")
                    {
                        logRequest.CommandText = $"Update Roles set name_role = '{textBox1.Text}'  where id_role = {condition}";

                    }
                    if (boxItem == "Особенности")
                    {
                        logRequest.CommandText = $"Update feature set name_car_feature = '{textBox1.Text}'  where id_feature = {condition}";

                    }
                    if (boxItem == "Пользователи")
                    {
                        int invalid = comboBox1.SelectedIndex;
                        logRequest.CommandText = $"Update users set username = '{textBox1.Text}', email = '{textBox2.Text}', password = '{textBox3.Text}'," +
                            $"disabled_person = {invalid}, id_role = {comboBox2.SelectedIndex + 1} where id_user  = {condition}";

                    }
                    if (boxItem == "Машины")
                    {

                        logRequest.CommandText = $"Update Cars set model = '{textBox1.Text}', category = {comboBox2.SelectedIndex + 1}, status = {comboBox3.SelectedIndex}," +
                            $"for_disabled_person = {comboBox1.SelectedIndex}, price = {Convert.ToInt32(textBox2.Text)} where id_car  = {condition}";

                    }
                    if (boxItem == "ОсобенностиАвто")
                    {



                        logRequest.CommandText = $"Update feature_to_cars set feature = {comboBox2.SelectedIndex + 1}, cars = {comboBox1.SelectedIndex + 1} where cars  = {mashina} and feature = {osoba}";

                    }
                    if (boxItem == "Заказы")
                    {
                        DateTime start_date = Convert.ToDateTime(textBox1.Text);
                        DateTime end_date = Convert.ToDateTime(textBox2.Text);

                        string startDateStr = start_date.ToString("yyyy-MM-dd HH:mm:ss");
                        string endDateStr = end_date.ToString("yyyy-MM-dd HH:mm:ss");

                        logRequest.CommandText = $"Update orders set id_customer = {comboBox1.SelectedIndex + 1}, id_rented_car = {comboBox2.SelectedIndex + 1}, start_date = '{startDateStr}', expiration_date = '{endDateStr}', start_point_id = {comboBox3.SelectedIndex + 1}, end_point_id = {comboBox4.SelectedIndex + 1}  where id_order = {condition}";
                    }


                    logRequest.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно изменена");
                }
            }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
    }
}
