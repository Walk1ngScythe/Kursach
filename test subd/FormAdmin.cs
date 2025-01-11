using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using Microsoft.SqlServer.Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.DataFormats;
using System.Reflection;

namespace test_subd
{

    public partial class FormAdmin : Form
    {

        public SqlConnection connect;
        int roleForm;


        public FormAdmin(SqlConnection cnct,int RoleFM)
        {
            connect = cnct;
            roleForm = RoleFM;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (roleForm == 3)
            {
                button3.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
            }
            dataGridView1.CellDoubleClick += DataGridViewCellDoubleClick;
            dataGridView1.CellClick += DataGridViewCellClick;
            SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString); // тут надо вставить переменную  

            sqlConnect.Open();
            SqlCommand logRequst = new SqlCommand();
            logRequst.CommandText = $"select * from {comboBox1.SelectedItem.ToString()}";

            logRequst.Connection = sqlConnect;


            // SqlAdapter - прослойка между источником данных и базой данных
            SqlDataAdapter adapter = new SqlDataAdapter(logRequst);
            DataSet dataSet = new DataSet();
            // заполняем источник данных полученными из адаптера записями
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            label1.Text = Convert.ToString(dataSet.Tables[0].Rows.Count); //считаем строчки

            sqlConnect.Close();
            dataGridView1.Columns[0].Visible = false;


        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm2 = new frmAuthorization();
            fm2.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string boxItem = comboBox1.SelectedItem.ToString();
            this.Hide();
            //Form fm = Application.OpenForms["Form3"];
            FormAddition fm3 = new FormAddition(connect,boxItem);
            fm3.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView2.Visible = true;
               


            }
            else
            {
                dataGridView2.Visible = false;
                
            }

            if (comboBox1.SelectedIndex == 3)
            {
                SqlConnection qsqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand qlogRequst = new SqlCommand();

                qlogRequst.CommandText = $"select * from ОсобенностиАвто Order By Автомобиль ASC";
                qlogRequst.Connection = qsqlConnect;
                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter qadapter = new SqlDataAdapter(qlogRequst);
                DataSet qdataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                qadapter.Fill(qdataSet);

                dataGridView1.DataSource = qdataSet.Tables[0];

                label1.Text = Convert.ToString(qdataSet.Tables[0].Rows.Count); //считаем строчки
            }
            else
            {
                SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand logRequst = new SqlCommand();

                logRequst.CommandText = $"SELECT * FROM {comboBox1.SelectedItem.ToString()}";
                logRequst.Connection = sqlConnect;

                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter adapter = new SqlDataAdapter(logRequst);
                DataSet dataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                adapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];

                label1.Text = Convert.ToString(dataSet.Tables[0].Rows.Count); //считаем строчки


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

                dataGridView1.Columns[0].Visible = false;

                sqlConnect.Close();
            }





        

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            /*
            bool t = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value) > dateTimePicker1.Value;
            if (t)
                MessageBox.Show("da");
            else
                MessageBox.Show("net");
            */

        }

        

        private void DataGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (roleForm == 1)
            {


                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;

                // Формируем текст для подтверждения удаления
                string confirmationText = $"Вы действительно хотите редактировать запись: ";

                // Получаем значения всех столбцов выбранной строки и добавляем их к тексту подтверждения
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    confirmationText += $"{selectedRow.Cells[i].Value.ToString()}";
                    if (i < selectedRow.Cells.Count - 1)
                        confirmationText += " - ";
                }

                string boxItem = comboBox1.SelectedItem.ToString();
                // Получение значения из 1 столбца (индексация начинается с 0)
                string condition = selectedRow.Cells[0].Value.ToString();

                // Получение значения из 2 столбца
                string condition2 = selectedRow.Cells[1].Value.ToString();






                this.Hide();

                Redactor fm = new Redactor(connect, boxItem, condition, condition2);
                fm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Получаем выбранную строку
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
             
            // Формируем текст для подтверждения удаления
            string confirmationText = $"Вы действительно хотите удалить из таблицы ";

            // Получаем значения всех столбцов выбранной строки и добавляем их к тексту подтверждения
            for (int i = 0; i < selectedRow.Cells.Count; i++)
            {
                confirmationText += $"{selectedRow.Cells[i].Value.ToString()}";
                if (i < selectedRow.Cells.Count - 1)
                    confirmationText += " - ";
            }

            // Показываем окно подтверждения
            DialogResult result = MessageBox.Show(confirmationText, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Если пользователь подтвердил удаление, выполняем операцию
            if (result == DialogResult.Yes)
            {

                try
                {

                    using (SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {

                        string condition = selectedRow.Cells[0].Value.ToString();
                        string condition2 = selectedRow.Cells[1].Value.ToString();
                        


                        sqlConnect.Open();
                        SqlCommand logRequest = new SqlCommand();
                        logRequest.Connection = sqlConnect;

                        string columnName = dataGridView1.Columns[0].HeaderText;
                        
                        string tableName = comboBox1.SelectedItem.ToString();

                        if (comboBox1.SelectedItem.ToString() == "Адресса")
                        {
                            logRequest.CommandText = $"Delete FROM points where id_point = {selectedRow.Cells[0].Value.ToString()}";

                        }
                        else if (comboBox1.SelectedItem.ToString() == "Особенности")
                        {
                            logRequest.CommandText = $"Delete FROM feature where id_feature = {selectedRow.Cells[0].Value.ToString()}";
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Категории")
                        {
                            logRequest.CommandText = $"Delete FROM category_of_cars where id_category = {selectedRow.Cells[0].Value.ToString()}";
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Роли")
                        {
                            logRequest.CommandText = $"Delete FROM Roles where id_role = {selectedRow.Cells[0].Value.ToString()}";
                        }

                        else if (comboBox1.SelectedItem.ToString() == "Машины")
                        {
                            logRequest.CommandText = $"Delete FROM Cars where id_car = {selectedRow.Cells[0].Value.ToString()}";
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Пользователи")
                        {
                            logRequest.CommandText = $"Delete FROM Users where id_user = {selectedRow.Cells[0].Value.ToString()}";
                        }
                        else if (comboBox1.SelectedItem.ToString() == "ОсобенностиАвто")
                        {
                            int osoba = 0;
                            int masina = 0;
                            SqlConnection conn3 = new SqlConnection(Properties.Settings.Default.connectionString);
                            conn3.Open();
                            string query = $"SELECT id_feature FROM feature WHERE name_car_feature = '{condition2}'";
                            SqlCommand command = new SqlCommand(query, conn3);
                            SqlDataReader reader4 = command.ExecuteReader();
                            if (reader4.Read())
                            {
                                osoba = Convert.ToInt32(reader4["id_feature"]);

                            }
                            reader4.Close();
                            conn3.Close();

                            SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.connectionString);
                            conn2.Open();
                            string query2 = $"SELECT id_car FROM Cars WHERE model = '{condition}'";
                            SqlCommand command2 = new SqlCommand(query2, conn2);
                            SqlDataReader reader2 = command2.ExecuteReader();
                            if (reader2.Read())
                            {
                                masina = Convert.ToInt32(reader2["id_car"]);

                            }
                            reader2.Close();
                            conn2.Close();

                          

                            logRequest.CommandText = $"Delete FROM feature_to_cars where cars = {masina} and feature = {osoba}"; 
                        }
                        else if (comboBox1.SelectedItem.ToString() == "Заказы")
                        {
                            logRequest.CommandText = $"Delete FROM orders where id_order = {selectedRow.Cells[0].Value.ToString()}";
                        }
                        logRequest.ExecuteNonQuery();


                    }
                    if (comboBox1.SelectedIndex == 0)
                    {
                        dataGridView2.Visible = true;
                       


                    }
                    else
                    {
                        dataGridView2.Visible = false;
                        

                    }

                    if (comboBox1.SelectedIndex == 3)
                    {
                        SqlConnection qsqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                        SqlCommand qlogRequst = new SqlCommand();

                        qlogRequst.CommandText = $"select * from ОсобенностиАвто Order By Автомобиль ASC";
                        qlogRequst.Connection = qsqlConnect;
                        // SqlAdapter - прослойка между источником данных и базой данных
                        SqlDataAdapter qadapter = new SqlDataAdapter(qlogRequst);
                        DataSet qdataSet = new DataSet();

                        // заполняем источник данных полученными из адаптера записями
                        qadapter.Fill(qdataSet);

                        dataGridView1.DataSource = qdataSet.Tables[0];

                        label1.Text = Convert.ToString(qdataSet.Tables[0].Rows.Count); //считаем строчки
                    }
                    else
                    {
                        SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                        SqlCommand logRequst = new SqlCommand();

                        logRequst.CommandText = $"SELECT * FROM {comboBox1.SelectedItem.ToString()}";
                        logRequst.Connection = sqlConnect;

                        // SqlAdapter - прослойка между источником данных и базой данных
                        SqlDataAdapter adapter = new SqlDataAdapter(logRequst);
                        DataSet dataSet = new DataSet();

                        // заполняем источник данных полученными из адаптера записями
                        adapter.Fill(dataSet);

                        dataGridView1.DataSource = dataSet.Tables[0];

                        label1.Text = Convert.ToString(dataSet.Tables[0].Rows.Count); //считаем строчки


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

                        dataGridView1.Columns[0].Visible = false;

                        sqlConnect.Close();
                    }
                }


                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void RefreshData()
        {
            if (comboBox1.SelectedIndex == 3)
            {
                SqlConnection qsqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand qlogRequst = new SqlCommand();

                qlogRequst.CommandText = $"select * from ОсобенностиАвто Order By Автомобиль ASC";
                qlogRequst.Connection = qsqlConnect;
                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter qadapter = new SqlDataAdapter(qlogRequst);
                DataSet qdataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                qadapter.Fill(qdataSet);

                dataGridView1.DataSource = qdataSet.Tables[0];

                label1.Text = Convert.ToString(qdataSet.Tables[0].Rows.Count); //считаем строчки
            }
            else
            {
                SqlConnection sqlConnect = new SqlConnection(Properties.Settings.Default.connectionString);
                SqlCommand logRequst = new SqlCommand();

                logRequst.CommandText = $"SELECT * FROM {comboBox1.SelectedItem.ToString()}";
                logRequst.Connection = sqlConnect;

                // SqlAdapter - прослойка между источником данных и базой данных
                SqlDataAdapter adapter = new SqlDataAdapter(logRequst);
                DataSet dataSet = new DataSet();

                // заполняем источник данных полученными из адаптера записями
                adapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];

                label1.Text = Convert.ToString(dataSet.Tables[0].Rows.Count); //считаем строчки


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

                dataGridView1.Columns[0].Visible = false;

                sqlConnect.Close();
            }







        }
    

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;

                // Формируем текст для подтверждения удаления
                string confirmationText = $"Вы действительно хотите редактировать запись: ";

                // Получаем значения всех столбцов выбранной строки и добавляем их к тексту подтверждения
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    confirmationText += $"{selectedRow.Cells[i].Value.ToString()}";
                    if (i < selectedRow.Cells.Count - 1)
                        confirmationText += " - ";
                }

                string boxItem = comboBox1.SelectedItem.ToString();
                // Получение значения из 1 столбца (индексация начинается с 0)
                string condition = selectedRow.Cells[0].Value.ToString();

                // Получение значения из 2 столбца
                string condition2 = selectedRow.Cells[1].Value.ToString();






                this.Hide();

                Redactor fm = new Redactor(connect, boxItem, condition, condition2);
                fm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void DataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Машины")
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
        }

        /*
private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
{
(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Date > {dateTimePicker1.Value} AND Date < {dateTimePicker2}";
}

private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
{
(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"Date > {dateTimePicker1.Value} AND Date < {dateTimePicker2}";
}
*/
    }
    }
