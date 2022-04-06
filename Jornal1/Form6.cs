using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tutorial.SqlConn;

namespace Jornal
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err = "";;
            if (textBox1.Text.Length < 3) err += "Слишком короткое ФИО";
            if (textBox2.Text.Length < 3) err += "Слишком короткий Логин";
            if (textBox3.Text.Length < 3) err += "Слишком короткий Пароль";
            if (textBox3.Text != textBox4.Text) err += "Пароли не совпадают";
            if (err == "")
            {
                SqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();
                string sql = "INSERT INTO users(fio_user,log_in,pass,prava) values (@fio_user,@log_in,@pass,@prava)";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@fio_user", SqlDbType.VarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@log_in", SqlDbType.VarChar).Value = textBox2.Text;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = Form1.GetMd5Hash(textBox3.Text);
                cmd.Parameters.Add("@prava", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedIndex) + 1;

                // Выполнить Command (Используется для delete, insert, update).
                cmd.ExecuteNonQuery();
                label6.Text = "Пользователь добавлен";
                label6.ForeColor = Color.Green;
            }
            else { label6.Text = err; label6.ForeColor = Color.Red; }
        }
    }
}
