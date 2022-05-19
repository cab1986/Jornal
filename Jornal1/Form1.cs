using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tutorial.SqlConn;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography;
namespace Jornal
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            // ����������� ������� ������ � ������ ���� � ��������� ���
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // ������� ����� Stringbuilder (���������� ������) ��� ������ ����
            StringBuilder sBuilder = new StringBuilder();

            // ����������� ������ ���� ���� � ����������������� ������
            for (int i = 0; i < data.Length; i++)
            {
                //���������, ��� ����� ������������� ������� � ����������������� ������ ������ � ��� �������
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            // ������� ������ Command.
            SqlCommand cmd = new SqlCommand();

            // �������� Command � Connection.
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Id,prava FROM users WHERE log_in=@login AND pass=@pass";

            cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = this.textBox1.Text;
            cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = GetMd5Hash(this.textBox2.Text);
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int Id = reader.GetOrdinal("Id");
                    User1.Value = Convert.ToInt32(reader.GetValue(Id));
                    int prava = reader.GetOrdinal("prava");
                    User1.Prava = Convert.ToInt32(reader.GetValue(prava));
                }
                Hide();
                Form2 about = new Form2();
                about.Show();

                //conn.Close();
                //conn.Dispose();
            }
            else
            {
                this.label4.Visible = true;
            }
        }
    }
}