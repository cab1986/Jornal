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

namespace Jornal
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        static void Refreshh(Form3 form)
        {
            form.listBox1.DataSource = null;
            DataRow selectedDataRow = ((DataRowView)form.comboBox1.SelectedItem).Row;
            int userId = Convert.ToInt32(selectedDataRow["Id"]);
            string userName = selectedDataRow["Name"].ToString();
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "SELECT predmet.predmet, teach_predmet.id_t_pr FROM teach_predmet LEFT JOIN predmet ON teach_predmet.id_predmet=predmet.id_predmet WHERE teach_predmet.id_teach=@id_teach ORDER BY predmet.predmet";
            cmd1.Parameters.Add("@id_teach", SqlDbType.Int).Value = userId;
            DataTable dataTable2 = new DataTable("pred_t");
            dataTable2.Columns.Add("id_t_pr");
            dataTable2.Columns.Add("predmet");
            DbDataReader reader = cmd1.ExecuteReader();
            //MessageBox.Show(this.comboBox1.SelectedText.ToString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int IdIndex = reader.GetOrdinal("id_t_pr");
                    int predmet = reader.GetOrdinal("predmet");
                    dataTable2.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(predmet));
                }
                form.listBox1.DataSource = dataTable2;
                form.listBox1.DisplayMember = "predmet";
                form.listBox1.ValueMember = "id_t_pr";
            }

            form.listBox2.DataSource = null;
            SqlConnection conn1 = DBUtils.GetDBConnection();
            conn1.Open();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn1;
            cmd2.CommandText = "SELECT predmet,id_predmet FROM predmet WHERE id_predmet NOT IN(SELECT predmet.id_predmet FROM teach_predmet LEFT JOIN predmet ON teach_predmet.id_predmet=predmet.id_predmet WHERE teach_predmet.id_teach=@id_teach)  ORDER BY predmet";
            cmd2.Parameters.Add("@id_teach", SqlDbType.Int).Value = userId;
            DataTable dataTable1 = new DataTable("predm");
            dataTable1.Columns.Add("id_predmet");
            dataTable1.Columns.Add("predmet");
            DbDataReader reader1 = cmd2.ExecuteReader();
            //MessageBox.Show(this.comboBox1.SelectedText.ToString());
            if (reader1.HasRows)
            {
               while (reader1.Read())
                {
                    int IdIndex = reader1.GetOrdinal("id_predmet");
                    int predmet = reader1.GetOrdinal("predmet");
                    dataTable1.Rows.Add(reader1.GetInt32(IdIndex), reader1.GetString(predmet));
                }
                form.listBox2.DataSource = dataTable1;
                form.listBox2.DisplayMember = "predmet";
                form.listBox2.ValueMember = "id_predmet";
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            Form2 about = new Form2();
            about.ShowDialog();
            //Application.Exit();
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            // Создать объект Command.
            SqlCommand cmd = new SqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM users WHERE prava=@prava";
            cmd.Parameters.Add("@prava", SqlDbType.Int).Value = 2;
            DataTable dataTable = new DataTable("fio");
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int IdIndex = reader.GetOrdinal("Id");
                    int fioIndex = reader.GetOrdinal("fio_user");
                    dataTable.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(fioIndex));
                }
            }
            comboBox1.DataSource = dataTable;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refreshh(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)listBox2.SelectedItem).Row;
            int id_predmet = Convert.ToInt32(selectedDataRow["id_predmet"]);
            DataRow selectedDataRow1 = ((DataRowView)comboBox1.SelectedItem).Row;
            int userId = Convert.ToInt32(selectedDataRow1["Id"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "INSERT INTO teach_predmet(id_predmet,id_teach) values (@id_predmet,@userId)";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_predmet", SqlDbType.Int).Value = id_predmet;
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

            // Выполнить Command (Используется для delete, insert, update).
            cmd.ExecuteNonQuery();

            Refreshh(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)listBox1.SelectedItem).Row;
            int id_t_pr = Convert.ToInt32(selectedDataRow["id_t_pr"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "DELETE FROM teach_predmet WHERE id_t_pr = @id_t_pr";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_t_pr", SqlDbType.Int).Value = id_t_pr;
            cmd.ExecuteNonQuery();

            Refreshh(this);
        }
    }
}
