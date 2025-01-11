using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test_subd
{
    public partial class FormAddition : Form
    {
        private SqlConnection connect;
        private string boxItem;
        public FormAddition(SqlConnection cnct, string item)
        {
            connect = cnct;
            boxItem = item;
            InitializeComponent();
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
                    logRequest.CommandText = $"INSERT INTO points (address) " +
                                             $"VALUES ('{textBox3.Text}')";
                }
                else if (boxItem == "Особенности")
                {
                    logRequest.CommandText = $"INSERT INTO feature (name_car_feature) " +
                                             $"VALUES ('{textBox3.Text}')";
                }
                else if (boxItem == "Категории")
                {
                    logRequest.CommandText = $"INSERT INTO category_of_cars (name_category) " +
                                             $"VALUES ('{textBox3.Text}')";
                }
                else if (boxItem == "Роли")
                {
                    logRequest.CommandText = $"INSERT INTO Roles (name_role) " +
                                             $"VALUES ('{textBox3.Text}')";
                }

                else if (boxItem == "Машины")
                {
                    int price = Convert.ToInt32(textBox4.Text);
                    string model = textBox3.Text;
                    int category = comboBox3.SelectedIndex + 1;
                    int status = comboBox1.SelectedIndex;
                    int forDisabledPerson = comboBox2.SelectedIndex;

                    logRequest.CommandText = $"INSERT INTO Cars (model, category, status, for_disabled_person, price) " +
                                             $"VALUES ('{model}', {category}, {status}, {forDisabledPerson}, {price})";

                }
                else if (boxItem == "Пользователи")
                {
                    string username = textBox3.Text;
                    string email = textBox4.Text;
                    string password = textBox5.Text;
                    int disabled_person = comboBox2.SelectedIndex;
                    int role = comboBox1.SelectedIndex + 1;
                    logRequest.CommandText = $"INSERT INTO Users (username,email,password,disabled_person,id_role) " +
                                             $"VALUES ('{username}','{email}','{password}',{disabled_person},{role})";
                }
                else if (boxItem == "ОсобенностиАвто")
                {
                    logRequest.CommandText = $"INSERT INTO feature_to_cars (feature,cars) " +
                                            $"VALUES ({comboBox2.SelectedIndex + 1},{comboBox1.SelectedIndex + 1})";
                }
                else if (boxItem == "Заказы")
                {
                    DateTime start_date = Convert.ToDateTime(textBox4.Text);
                    DateTime end_date = Convert.ToDateTime(textBox5.Text);
                    string startDateStr = start_date.ToString("yyyy-MM-dd HH:mm:ss");
                    string endDateStr = end_date.ToString("yyyy-MM-dd HH:mm:ss");
                    logRequest.CommandText = "INSERT INTO orders (id_customer, id_rented_car, start_date, expiration_date, start_point_id, end_point_id) " +
                          "VALUES (@id_customer, @id_rented_car, @start_date, @end_date, @start_point_id, @end_point_id)";

                    logRequest.Parameters.AddWithValue("@id_customer", comboBox1.SelectedIndex + 1);
                    logRequest.Parameters.AddWithValue("@id_rented_car", comboBox2.SelectedIndex + 1);
                    logRequest.Parameters.AddWithValue("@start_date", startDateStr);
                    logRequest.Parameters.AddWithValue("@end_date", endDateStr);
                    logRequest.Parameters.AddWithValue("@start_point_id", comboBox5.SelectedIndex + 1);
                    logRequest.Parameters.AddWithValue("@end_point_id", comboBox4.SelectedIndex + 1);


                }

                logRequest.ExecuteNonQuery();
                MessageBox.Show("Запись успешно добавлена");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (connect.State == ConnectionState.Open)
            {
                connect.Close();
            }
            if (boxItem == "Заказы")
            {
                button1.Left = 20;
                button1.Top = 180;
                button2.Left = 235;
                button2.Top = 180;
                this.Width = 350;
                this.Height = 250;
                comboBox2.Left = 12;
                label3.Left = 12;
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
                        comboBox1.Text = "";
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
                        comboBox2.Text = "";
                    reader1.Close();
                    SqlCommand ttypeGoodCommand = new SqlCommand("SELECT id_point, address  FROM points", conn);
                    SqlDataReader reader2 = ttypeGoodCommand.ExecuteReader();
                    List<Categories> lstCategories2 = new List<Categories>();

                    while (reader2.Read())
                    {
                        lstCategories2.Add(new Categories(Convert.ToInt32(reader2["id_point"]), reader2["address"].ToString()));
                    }

                    comboBox4.DataSource = lstCategories2;
                    comboBox4.DisplayMember = "name";
                    comboBox4.ValueMember = "id";
                    comboBox4.Text = "";
                    reader2.Close();
                    SqlCommand typeGoodCommand2 = new SqlCommand("SELECT id_point, address  FROM points", conn);
                    SqlDataReader reader3 = ttypeGoodCommand.ExecuteReader();
                    List<Categories> lstCategories3 = new List<Categories>();

                    while (reader3.Read())
                    {
                        lstCategories3.Add(new Categories(Convert.ToInt32(reader3["id_point"]), reader3["address"].ToString()));
                    }

                    comboBox5.DataSource = lstCategories3;
                    comboBox5.DisplayMember = "name";
                    comboBox5.ValueMember = "id";
                    comboBox5.Text = "";
                    reader3.Close();


                }
                label7.Visible = true;
                label8.Visible = true; 
                label9.Visible = true;
                comboBox4.Visible = true; 
                comboBox5.Visible = true;
            } 
            else if (boxItem == "Машины")
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_category, name_category  FROM category_of_cars", conn);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_category"]), rdr["name_category"].ToString()));
                        }
                        comboBox3.DataSource = lstCategories;
                        comboBox3.DisplayMember = "name";
                        comboBox3.ValueMember = "id";
                        comboBox3.Text = "";
                    }
                }
                    label1.Text = "Название Aвто";
                textBox3.Visible = true;
                label1.Visible = true;
                label4.Text = "Цена";
                label2.Text = "Статус";
                label3.Text = "Для инвалидов";
                label5.Visible = false;
                comboBox1.Items.Add("Свободна");
                comboBox1.Items.Add("Занята");
                comboBox1.Text = "Свободна";
                comboBox2.Items.Add("Нет");
                comboBox2.Items.Add("Да");
                comboBox2.Text = "Нет";
                textBox5.Visible = false;
                label6.Text = "Категория";
                label6.Visible = true;
                comboBox3.Visible = true;
                this.Width = 550;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 115;
                label6.Left = 185;
                button2.Top = 115;
                comboBox3.Left = 185;

            }
            else if (boxItem == "Категории")
            {
                label1.Text = "Введите название новой категории";
                label1.Visible = true;
                textBox3.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;

                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;

            }
            else if (boxItem == "Роли")
            {
                label1.Text = "Введите название новой Роли";
                label1.Visible = true;
                textBox3.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            else if (boxItem == "Адресса")
            {
                label1.Text = "Введите название нового Адреса";
                label1.Visible = true;
                label2.Visible = false;
                textBox3.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            else if (boxItem == "Пользователи")
            {
                button1.Left = 20;
                button1.Top = 115;
                button2.Top = 115;
                this.Width = 550;
                this.Height = 200;
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    conn.Open();
                    SqlCommand logRequest = new SqlCommand("SELECT id_role, name_role FROM Roles", conn);

                    using (SqlDataReader rdr = logRequest.ExecuteReader())
                    {
                        List<Categories> lstCategories = new List<Categories>();

                        while (rdr.Read())
                        {
                            lstCategories.Add(new Categories(Convert.ToInt32(rdr["id_role"]), rdr["name_role"].ToString()));
                        }
                        comboBox1.DataSource = lstCategories;
                        comboBox1.DisplayMember = "name";
                        comboBox1.ValueMember = "id";
                        comboBox1.Text = "";
                    }
                }
                label1.Text = "Введите имя пользователя";
                textBox3.Visible = true;
                label1.Visible = true;
                label2.Text = "Выберите роль пользователя";
                label4.Text = "Введите почту пользователя";
                label5.Text = "Введите пароль пользователя";
                label3.Text = "Инвалидность";
                comboBox2.Items.Add("Нет");
                comboBox2.Items.Add("Да");
                comboBox2.Text = "Нет";

            }
            else if (boxItem == "Категории")
            {
                label1.Text = "Введите название новой категории";
                textBox3.Visible = true;
                label1.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            else if (boxItem == "Особенности")
            {
                label1.Text = "Введите название новой особенности";
                label1.Visible = true;
                textBox3.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                this.Width = 250;
                this.Height = 200;
                button1.Left = 20;
                button1.Top = 90;
                button2.Left = 20;
                button2.Top = 115;
            }
            else if (boxItem == "ОсобенностиАвто")
            {
                this.Width = 350;
                this.Height = 170;
                comboBox2.Left = 10;
                label3.Left = 8;
                button1.Left = 23;
                button1.Top = 80;
                button2.Left = 213;
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
                        comboBox2.Text = "";
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
                    comboBox1.Text = "";

                    reader1.Close();
                }

                label2.Text = "Выберите название авто";
                label3.Text = "Выберите название особенности";
                label1.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox3.Visible = false;
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }

        public Categories(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}

