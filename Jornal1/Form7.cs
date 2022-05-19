using FastReport;
using FastReport.Utils;
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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = DBUtils.GetDBConnection();
            conn1.Open();

            // Создать объект Command.
            SqlCommand cmd1 = new SqlCommand();

            // Сочетать Command с Connection.
            cmd1.Connection = conn1;
            cmd1.CommandText = "SELECT fio_user,ISNULL((SELECT ROUND(avg(CAST(mark.mark AS float)),2) FROM mark WHERE users.Id=mark.id_st AND date_m>=@dateb AND date_m<=@dateen),'0') Over_Mark FROM users WHERE prava = 3; ";
            cmd1.Parameters.Add("@dateb", SqlDbType.Date).Value = dateTimePicker1.Value.ToShortDateString();
            cmd1.Parameters.Add("@dateen", SqlDbType.Date).Value = dateTimePicker2.Value.ToShortDateString();
            DataTable dataTable1 = new DataTable("Over_M");
            dataTable1.Columns.Add("fio");
            dataTable1.Columns.Add("mark");
            DbDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    int IdIndex = reader1.GetOrdinal("fio_user");
                    int fioIndex = reader1.GetOrdinal("Over_Mark");
                    dataTable1.Rows.Add(reader1.GetString(IdIndex), reader1.GetDouble(fioIndex));
                }
            }
            Report report = new Report();
            // register the "Products" table
            report.RegisterData(dataTable1, "Over_M");
            // enable it to use in a report
            report.GetDataSource("Over_M").Enabled = true;
            // create A4 page with all margins set to 1cm
            ReportPage page1 = new ReportPage();
            page1.Name = "Page1";
            report.Pages.Add(page1);
            // create ReportTitle band
            page1.ReportTitle = new ReportTitleBand();
            page1.ReportTitle.Name = "Средний бал";
            // set its height to 1.5cm
            page1.ReportTitle.Height = Units.Centimeters * 1.5f;
            // create group header
            GroupHeaderBand group1 = new GroupHeaderBand();
            group1.Name = "GroupHeader1";
            group1.Height = Units.Centimeters * 1;
            // set group condition
            group1.Condition = "[Over_M.fio]";
            // add group to the page.Bands collection
            page1.Bands.Add(group1);
            // create group footer
            group1.GroupFooter = new GroupFooterBand();
            group1.GroupFooter.Name = "GroupFooter1";
            group1.GroupFooter.Height = Units.Centimeters * 1;
            // create DataBand
            DataBand data1 = new DataBand();
            data1.Name = "Data1";
            data1.Height = Units.Centimeters * 0.5f;
            // set data source
            data1.DataSource = report.GetDataSource("Over_M");
            // connect databand to a group
            group1.Data = data1;

            // create "Text" objects
            // report title
            TextObject text1 = new TextObject();
            text1.Name = "Text1";
            // set bounds
            text1.Bounds = new RectangleF(0, 0,
            Units.Centimeters * 19, Units.Centimeters * 1);
            // set text
            text1.Text = "Средний бал";
            // set appearance
            text1.HorzAlign = HorzAlign.Center;
            text1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            // add it to ReportTitle
            page1.ReportTitle.Objects.Add(text1);

            // group
            TextObject text2 = new TextObject();
            text2.Name = "Text2";
            text2.Bounds = new RectangleF(0, 0, Units.Centimeters * 10, Units.Centimeters * 1);
            text2.Text = "[Over_M.fio]";
            text2.Font = new Font("Tahoma", 10, FontStyle.Bold);
            // add it to GroupHeader
            group1.Objects.Add(text2);
            // data band
            TextObject text3 = new TextObject();
            text3.Name = "Text3";
            text3.Bounds = new RectangleF(0, 0, Units.Centimeters * 10, Units.Centimeters * 0.5f);
            text3.Text = "Средний балл: " + "[Over_M.mark]";
            text3.Font = new Font("Tahoma", 8);
            // add it to DataBand
            data1.Objects.Add(text3);
            // group footer
            TextObject text4 = new TextObject();
            text4.Name = "Text4";
            text4.Bounds = new RectangleF(0, 0,
            Units.Centimeters * 10, Units.Centimeters * 0.5f);
            text4.Text = "";
            text4.Font = new Font("Tahoma", 8, FontStyle.Bold);
            // add it to GroupFooter
            group1.GroupFooter.Objects.Add(text4);
            // add a total
            //Total groupTotal = new Total();
            // groupTotal.Name = "CountOfProducts";
            // groupTotal.TotalType = TotalType.Count;
            // groupTotal.Evaluator = data1;
            //groupTotal.PrintOn = group1.Footer;
            // add it to report totals
            //report.Dictionary.Totals.Add(groupTotal);


            report.Show();
        }
    }
}
