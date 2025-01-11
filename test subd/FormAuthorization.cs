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

namespace test_subd
{
    public partial class frmAuthorization : Form
    {

        static SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString);


        public frmAuthorization()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConnect_Click(object sender, EventArgs e) // Войти
        {
            
            
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                    connect.Close();
                    connect.Open();
               
            


            // если процедуры нет 
            SqlCommand command = new SqlCommand($"Select * from [Users] where email = '{tbLogin.Text}' and password = '{tbPassword.Text}' ", connect);

            // подготавливаем команду для чтения
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
                
                if (dataReader.HasRows) // если есть хоть одна строка 
                {
                    int UserId = Convert.ToInt32(dataReader["id_user"]);
                    int roleForm = 0;
                    int invalidnost = Convert.ToInt32(dataReader["disabled_person"]);
                    
                    switch (dataReader.GetInt32(5))
                    {
                       
                        // первая роль типа юзерок-фраерок
                        case 1:
                            this.Hide();
                            roleForm = 1;
                            FormAdmin fm = new FormAdmin(connect, roleForm);
                            fm.ShowDialog();
                            break;
                        case 2:
                            this.Hide();
                            roleForm = 2;
                            CustomerForm fm2 = new CustomerForm(connect, roleForm,invalidnost, UserId);
                            MessageBox.Show($"{invalidnost}");
                            fm2.ShowDialog();
                            break;
                        case 3:
                            this.Hide();
                            roleForm = 3;
                            FormAdmin fm3 = new FormAdmin(connect, roleForm);
                            fm3.ShowDialog();
                            break;
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }

           connect.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось создать подключение: {ex.Message}");
                return;
            }




            /* если создана процедура CheckLogin

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CheckLogin";
            command.Parameters.AddWithValue("@user_login", tbLogin.Text);
            command.Parameters.AddWithValue("@user_password", tbPassword.Text); */





        }
    }
}
