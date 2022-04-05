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
using System.Globalization;

namespace Jornal
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void Refreshh(Form2 form)
        {
            int mnth = form.comboBox1.SelectedIndex;
            int dayinmnt = DateTime.DaysInMonth(2022, mnth + 1);
            DateTime date = DateTime.Now;
            int predId=0;
            if (User.Prava != 3)
            {
                DataRow selectedDataRow = ((DataRowView)form.comboBox2.SelectedItem).Row;
                predId = Convert.ToInt32(selectedDataRow["Id_pr"]);
            }
            int idt;string txtch; int idst; string txst;
            if (User.Prava == 1)
            {
                DataRow selectedDataRow1 = ((DataRowView)form.comboBox3.SelectedItem).Row;
                idt = Convert.ToInt32(selectedDataRow1["Id"]);
                txtch = " AND id_tch=";
                idst = predId; txst = " AND id_pred=";
            }
            else if (User.Prava == 2) { idt = Convert.ToInt32(User.Value); txtch = " AND id_tch="; idst = predId; txst = " AND id_pred="; }
            else { idt = 1; txtch = " AND 1="; idst = User.Value; txst = " AND id_st="; }
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT date_les, id_st, id_pred FROM lesson WHERE date_les<@dateb AND date_les>=@dates " + txtch + idt + txst + idst;
            //cmd.Parameters.Add("@id_pred", SqlDbType.Int).Value = predId;
            //cmd.Parameters.Add("@id_tch", SqlDbType.Int).Value = idt;
            cmd.Parameters.Add("@dateb", SqlDbType.Date).Value = date.Year + "-" + (mnth + 2).ToString() + "-01";
            cmd.Parameters.Add("@dates", SqlDbType.Date).Value = date.Year + "-" + (mnth + 1).ToString() + "-01";
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable("predmet");
            dataTable.Columns.Add("id_st");
            dataTable.Columns.Add("date_les");
            dataTable.Columns.Add("id_pred");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int dat_l = reader.GetOrdinal("date_les");
                    int id_std = reader.GetOrdinal("id_st");
                    int id_pred = reader.GetOrdinal("id_pred");
                    dataTable.Rows.Add(reader.GetInt32(id_std), Convert.ToInt32(reader.GetDateTime(dat_l).Day), reader.GetInt32(id_pred));
                }
            }

            SqlConnection conn1 = DBUtils.GetDBConnection();
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn1;
            cmd1.CommandText = "SELECT date_m, id_st, mark, id_pred FROM mark WHERE date_m<@dateb AND date_m>=@dates" + txst + idst;
            //cmd1.Parameters.Add("@id_pred", SqlDbType.Int).Value = predId;
            cmd1.Parameters.Add("@dateb", SqlDbType.Date).Value = date.Year + "-" + (mnth + 2).ToString() + "-01";
            cmd1.Parameters.Add("@dates", SqlDbType.Date).Value = date.Year + "-" + (mnth + 1).ToString() + "-01";
            DbDataReader reader1 = cmd1.ExecuteReader();
            DataTable dataTable1 = new DataTable("predmet");
            dataTable1.Columns.Add("id_st");
            dataTable1.Columns.Add("date_m");
            dataTable1.Columns.Add("mark");
            dataTable1.Columns.Add("id_pred");
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    int dat_m = reader1.GetOrdinal("date_m");
                    int id_std1 = reader1.GetOrdinal("id_st");
                    int markind = reader1.GetOrdinal("mark");
                    int id_pred = reader.GetOrdinal("id_pred");
                    dataTable1.Rows.Add(reader1.GetInt32(id_std1), Convert.ToInt32(reader1.GetDateTime(dat_m).Day), reader1.GetInt32(markind), reader1.GetInt32(id_pred));
                }
            }

            form.dataGridView1.Rows.Clear();
            form.dataGridView1.Columns.Clear();
            form.dataGridView1.AllowUserToAddRows = false;
            form.dataGridView1.ReadOnly = true;
            int k = 1; int j = 0;
            while (dayinmnt > form.dataGridView1.ColumnCount)
            {
                form.dataGridView1.Columns.Add("", k.ToString());
                form.dataGridView1.Columns[k - 1].Width = 30;
                k++;
            }
            SqlConnection conn2 = DBUtils.GetDBConnection();
            conn2.Open();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn2;
            if(User.Prava != 3)
            {
                cmd2.CommandText = "SELECT t_st_pred.id_st, users.fio_user FROM t_st_pred JOIN users ON t_st_pred.id_st=users.id_user WHERE t_st_pred.id_pred=@id_pred AND t_st_pred.id_tch=@id_tch ORDER BY users.fio_user";
                cmd2.Parameters.Add("@id_pred", SqlDbType.Int).Value = predId;
                cmd2.Parameters.Add("@id_tch", SqlDbType.Int).Value = idt;
            }
            else
            {
                cmd2.CommandText = "SELECT t_st_pred.id_pred, predmet.predmet FROM t_st_pred JOIN predmet ON t_st_pred.id_pred=predmet.id_predmet WHERE t_st_pred.id_st=" + User.Value + " ORDER BY predmet.predmet";

            }
            DbDataReader reader2 = cmd2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    string fio_st; int id_stud;
                    if (User.Prava != 3)
                    {
                        int fio = reader2.GetOrdinal("fio_user");
                        int id_std2 = reader2.GetOrdinal("id_st");
                        fio_st = reader2.GetString(fio);
                        id_stud = reader2.GetInt32(id_std2);
                    }
                    else
                    {
                        int predmet = reader2.GetOrdinal("predmet");
                        int id_pred = reader2.GetOrdinal("id_pred");
                        fio_st = reader2.GetString(predmet);
                        id_stud = reader2.GetInt32(id_pred);
                    }
                    form.dataGridView1.Rows.Add();
                    form.dataGridView1.RowHeadersWidth = 200;
                    form.dataGridView1.Rows[j].HeaderCell.Value = fio_st;
                    for (int i = 0; i < dayinmnt; i++)
                    {
                        DataRow[] selectedls; DataRow[] selectedmr;
                        if (User.Prava != 3)
                        {
                            selectedls = dataTable.Select($"id_st = '{id_stud}' AND date_les = '{i + 1}'");
                            selectedmr = dataTable1.Select($"id_st = '{id_stud}' AND date_m = '{i + 1}'");
                        }
                        else
                        {
                            selectedls = dataTable.Select($"id_pred = '{id_stud}' AND date_les = '{i + 1}'");
                            selectedmr = dataTable1.Select($"id_pred = '{id_stud}' AND date_m = '{i + 1}'");
                        }
                        //form.dataGridView1.Columns[i].HeaderText = "название столбца";
                        //dataGridView1.Columns.Add();
                        //dataGridView1.Columns[1] [i,0].DefaultCellStyle.
                        int kk = 0; string mrk = "";
                        foreach (var b in selectedls) kk++;
                        foreach (var c in selectedmr)
                        {
                            if (mrk != "") mrk += ", ";
                            mrk += c["mark"].ToString();
                        }
                        form.dataGridView1[i, j].Value = mrk;
                        if (kk > 0) form.dataGridView1[i, j].Style.BackColor = Color.Green;
                    }
                    j++;
                }
            }

        }
        private void Form2_Load(object sender, EventArgs e)
        {

            //string month = dt.ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            this.comboBox1.Text = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            if (User.Prava != 1)
            {
                //MessageBox.Show(User.Value);
                //this.менюToolStripMenuItem.Visible = false;
                this.предметыToolStripMenuItem.Visible = false;
                this.пользавателиToolStripMenuItem.Visible = false;
                this.учебаToolStripMenuItem.Visible = false;
                this.comboBox3.Visible = false;
            }
            if (User.Prava != 3)
            {
                SqlConnection conn = DBUtils.GetDBConnection();
                conn.Open();

                // Создать объект Command.
                SqlCommand cmd = new SqlCommand();

                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM predmet ORDER BY predmet";
                DataTable dataTable = new DataTable("predmet");
                dataTable.Columns.Add("Id_pr");
                dataTable.Columns.Add("predmet");
                DbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int IdIndex = reader.GetOrdinal("id_predmet");
                        int predmetIndex = reader.GetOrdinal("predmet");
                        dataTable.Rows.Add(reader.GetInt32(IdIndex), reader.GetString(predmetIndex));
                    }
                }
                comboBox2.DataSource = dataTable;
                comboBox2.DisplayMember = "predmet";
                comboBox2.ValueMember = "Id_pr";
                //conn.Close();


                SqlConnection conn1 = DBUtils.GetDBConnection();
                conn1.Open();

                // Создать объект Command.
                SqlCommand cmd1 = new SqlCommand();

                // Сочетать Command с Connection.
                cmd1.Connection = conn1;
                cmd1.CommandText = "SELECT users.fio_user, users.id_user FROM users LEFT JOIN teach_predmet ON users.id_user=teach_predmet.id_teach WHERE teach_predmet.id_predmet=@predm ORDER BY users.fio_user";
                DataRow selectedDataRow = ((DataRowView)comboBox2.SelectedItem).Row;
                int predId = Convert.ToInt32(selectedDataRow["Id_pr"]);
                cmd1.Parameters.Add("@predm", SqlDbType.Int).Value = predId;
                DataTable dataTable1 = new DataTable("fio");
                dataTable1.Columns.Add("Id");
                dataTable1.Columns.Add("Name");
                DbDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        int IdIndex = reader1.GetOrdinal("id_user");
                        int fioIndex = reader1.GetOrdinal("fio_user");
                        dataTable1.Rows.Add(reader1.GetInt32(IdIndex), reader1.GetString(fioIndex));
                    }
                }
                comboBox3.DataSource = dataTable1;
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "Id";
                //conn1.Close();
            }
            if (User.Prava == 3)
            {
                comboBox2.Visible = false;
                button1.Visible = false;
            }
            Refreshh(this);
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.DataSource != null || User.Prava==3)
            {
                Refreshh(this);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Hide();
            Form3 about = new Form3();
            about.ShowDialog();
        }

        private void предметыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form3 about = new Form3();
            about.ShowDialog();
        }

        private void учебаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form4 about = new Form4();
            about.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (User.Value == 9)
            {
                SqlConnection conn1 = DBUtils.GetDBConnection();
                conn1.Open();

                // Создать объект Command.
                SqlCommand cmd1 = new SqlCommand();

                // Сочетать Command с Connection.
                cmd1.Connection = conn1;
                cmd1.CommandText = "SELECT users.fio_user, users.id_user FROM users LEFT JOIN teach_predmet ON users.id_user=teach_predmet.id_teach WHERE teach_predmet.id_predmet=@predm ORDER BY users.fio_user";
                DataRow selectedDataRow = ((DataRowView)comboBox2.SelectedItem).Row;
                int predId = Convert.ToInt32(selectedDataRow["Id_pr"]);
                cmd1.Parameters.Add("@predm", SqlDbType.Int).Value = predId;
                DataTable dataTable1 = new DataTable("fio");
                dataTable1.Columns.Add("Id");
                dataTable1.Columns.Add("Name");
                DbDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        int IdIndex = reader1.GetOrdinal("id_user");
                        int fioIndex = reader1.GetOrdinal("fio_user");
                        dataTable1.Rows.Add(reader1.GetInt32(IdIndex), reader1.GetString(fioIndex));
                    }
                }
                comboBox3.DataSource = dataTable1;
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "Id";
            }
            Refreshh(this);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refreshh(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow selectedDataRow = ((DataRowView)comboBox2.SelectedItem).Row;
            int predId = Convert.ToInt32(selectedDataRow["Id_pr"]);
            string predmt = selectedDataRow["predmet"].ToString();
            Form5 about = new Form5();
            about.pr2 = predId;
            about.npr2 = predmt;
            about.ShowDialog();
            //about.Show();
            //Form2 f2 = new Form2();
            //Enabled = false;
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            Refreshh(this);
            //Enabled = true;
        }

        private void пользавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
