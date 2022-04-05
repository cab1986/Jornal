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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        static void Refreshh(Form4 form)
        {
            form.listBox1.DataSource = null;
            DataRow selectedDataRow = ((DataRowView)form.comboBox1.SelectedItem).Row;
            int userId = Convert.ToInt32(selectedDataRow["Id"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "SELECT predmet.predmet +', '+ users.fio_user as PredFio, t_st_pred.id_tsp FROM t_st_pred LEFT JOIN predmet ON t_st_pred.id_pred=predmet.id_predmet LEFT JOIN users ON t_st_pred.id_tch=users.id_user WHERE t_st_pred.id_st=@id_st ORDER BY predmet.predmet";
            cmd1.Parameters.Add("@id_st", SqlDbType.Int).Value = userId;
            DataTable dataTable2 = new DataTable("t_st_pred");
            dataTable2.Columns.Add("id_tsp");
            dataTable2.Columns.Add("PredFio");
            DbDataReader reader = cmd1.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int IdIndex = reader.GetOrdinal("id_tsp");
                    int predmet = reader.GetOrdinal("PredFio");
                    dataTable2.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(predmet));
                }
                form.listBox1.DataSource = dataTable2;
                form.listBox1.DisplayMember = "PredFio";
                form.listBox1.ValueMember = "id_tsp";
            }

            form.comboBox2.DataSource = null;
            SqlConnection conn1 = DBUtils.GetDBConnection();
            conn1.Open();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn1;
            cmd2.CommandText = "SELECT predmet,id_predmet FROM predmet WHERE id_predmet NOT IN(SELECT id_pred FROM t_st_pred WHERE id_st =@id_st)  ORDER BY predmet";
            cmd2.Parameters.Add("@id_st", SqlDbType.Int).Value = userId;
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
                form.comboBox2.DataSource = dataTable1;
                form.comboBox2.DisplayMember = "predmet";
                form.comboBox2.ValueMember = "id_predmet";
            }

           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)listBox1.SelectedItem).Row;
            int id_tsp = Convert.ToInt32(selectedDataRow["id_tsp"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "DELETE FROM t_st_pred WHERE id_tsp = @id_tsp";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_tsp", SqlDbType.Int).Value = id_tsp;
            cmd.ExecuteNonQuery();

            Refreshh(this);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            // Создать объект Command.
            SqlCommand cmd = new SqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM users WHERE prava=@prava";
            cmd.Parameters.Add("@prava", SqlDbType.Int).Value = 3;
            DataTable dataTable = new DataTable("fio");
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int IdIndex = reader.GetOrdinal("id_user");
                    int fioIndex = reader.GetOrdinal("fio_user");
                    dataTable.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(fioIndex));
                }
            }
            comboBox1.DataSource = dataTable;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            Refreshh(this);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            Form2 about = new Form2();
            about.ShowDialog();
            //Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refreshh(this);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.DataSource != null)
            {
                comboBox3.DataSource = null;
                DataRow selectedDataRow1 = ((DataRowView)comboBox2.SelectedItem).Row;
                int predId = Convert.ToInt32(selectedDataRow1["id_predmet"]);
                SqlConnection conn2 = DBUtils.GetDBConnection();
                conn2.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = conn2;
                cmd3.CommandText = "SELECT id_user,fio_user FROM users WHERE id_user IN(SELECT id_teach FROM teach_predmet WHERE id_predmet =@id_pred)  ORDER BY fio_user";
                cmd3.Parameters.Add("@id_pred", SqlDbType.Int).Value = predId;
                DataTable dataTable3 = new DataTable("teachpred");
                dataTable3.Columns.Add("id_user");
                dataTable3.Columns.Add("fio_user");
                DbDataReader reader2 = cmd3.ExecuteReader();
                //MessageBox.Show(this.comboBox1.SelectedText.ToString());
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        int IdIndex = reader2.GetOrdinal("id_user");
                        int fio = reader2.GetOrdinal("fio_user");
                        dataTable3.Rows.Add(reader2.GetInt32(IdIndex), reader2.GetString(fio));
                    }
                    comboBox3.DataSource = dataTable3;
                    comboBox3.DisplayMember = "fio_user";
                    comboBox3.ValueMember = "id_user";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)comboBox1.SelectedItem).Row;
            int stId = Convert.ToInt32(selectedDataRow["Id"]);
            DataRow selectedDataRow1 = ((DataRowView)comboBox2.SelectedItem).Row;
            int predId = Convert.ToInt32(selectedDataRow1["id_predmet"]);
            DataRow selectedDataRow2 = ((DataRowView)comboBox3.SelectedItem).Row;
            int teachId = Convert.ToInt32(selectedDataRow2["id_user"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "INSERT INTO t_st_pred(id_pred,id_tch,id_st) values (@id_predmet,@id_tch,@id_st)";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_predmet", SqlDbType.Int).Value = predId;
            cmd.Parameters.Add("@id_tch", SqlDbType.Int).Value = teachId;
            cmd.Parameters.Add("@id_st", SqlDbType.Int).Value = stId;

            // Выполнить Command (Используется для delete, insert, update).
            cmd.ExecuteNonQuery();

            Refreshh(this);
        }
    }
}
