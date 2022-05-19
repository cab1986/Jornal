using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tutorial.SqlConn;

namespace Jornal
{
    public partial class Form5 : Form
    {
        public int pr2;
        public string npr2;

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)comboBox1.SelectedItem).Row;
            int stId = Convert.ToInt32(selectedDataRow["id_st"]);
            //Form2 f2 = new Form2();
            //DataRow selectedDataRow1 = ((DataRowView)f2.comboBox2.SelectedItem).Row;
            //int id_predmet = Convert.ToInt32(selectedDataRow1["Id_pr"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "INSERT INTO lesson(id_tch,id_st,id_pred,date_les) values (@id_tech,@userId,@id_predmet,@date_les)";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_predmet", SqlDbType.Int).Value = pr2;
            cmd.Parameters.Add("@id_tech", SqlDbType.Int).Value = User1.Value;
            cmd.Parameters.Add("@date_les", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = stId;

            // Выполнить Command (Используется для delete, insert, update).
            cmd.ExecuteNonQuery();
            Form2 frm = new Form2();
            //frm.Enabled = true;
            //frm.Focus();
            frm.Activate();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //DataRow selectedDataRow = ((DataRowView)comboBox1.SelectedItem).Row;
            //int predId = Convert.ToInt32(selectedDataRow["Id_pr"]);
            label1.Text = "Выбран предмет: " + npr2;
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT t_st_pred.id_st, users.fio_user FROM t_st_pred JOIN users ON t_st_pred.id_st=users.Id WHERE t_st_pred.id_pred=@id_pred AND t_st_pred.id_tch=@id_tch ORDER BY users.fio_user";
            cmd.Parameters.Add("@id_pred", SqlDbType.Int).Value = pr2;
            cmd.Parameters.Add("@id_tch", SqlDbType.Int).Value = User1.Value;
            DataTable dataTable = new DataTable("fio");
            dataTable.Columns.Add("id_st");
            dataTable.Columns.Add("Name");
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int IdIndex = reader.GetOrdinal("id_st");
                    int fioIndex = reader.GetOrdinal("fio_user");
                    dataTable.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(fioIndex));
                }
            }
            comboBox1.DataSource = dataTable;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id_st";
            comboBox2.SelectedIndex = 0;
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            //Form2 f2 = new Form2();
            //2.Enabled = true;
        }

        private void Form5_Activated(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)comboBox1.SelectedItem).Row;
            int stId = Convert.ToInt32(selectedDataRow["id_st"]);
            int mark = 5 - Convert.ToInt32(comboBox2.SelectedIndex);
            //Form2 f2 = new Form2();
            //DataRow selectedDataRow1 = ((DataRowView)f2.comboBox2.SelectedItem).Row;
            //int id_predmet = Convert.ToInt32(selectedDataRow1["Id_pr"]);
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            string sql = "INSERT INTO mark(mark,id_st,id_pred,date_m) values (@mark,@userId,@id_predmet,@date_m)";

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id_predmet", SqlDbType.Int).Value = pr2;
            cmd.Parameters.Add("@mark", SqlDbType.Int).Value = mark;
            cmd.Parameters.Add("@date_m", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = stId;

            // Выполнить Command (Используется для delete, insert, update).
            cmd.ExecuteNonQuery();
            Form2 frm = new Form2();
            frm.Enabled = true;
            frm.Focus();
            frm.Enabled = false;
        }
    }
}
