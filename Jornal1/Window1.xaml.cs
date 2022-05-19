using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jornal
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 
    public partial class Window1 : Window
    {
        public string Predmt;
        public int PredId;
        public string usr;
        public Window1(int prid)
        {
            InitializeComponent();
            this.DataContext = this;
            using (Jornal_DBEntities db = new Jornal_DBEntities())
            {
                var students = from TSP in db.t_st_pred
                               join u in db.Users on TSP.id_st equals u.Id
                               where TSP.id_pred == prid && TSP.id_tch == User1.Value
                               orderby u.fio_user
                               select new { Name = u.fio_user, IdSt = TSP.id_st };
                ObservableCollection<L_M> data = new ObservableCollection<L_M>();
                foreach (var st in students)
                    data.Add(new L_M(st.IdSt, st.Name));
                ComboBox1.ItemsSource = data;
                ComboBox1.DisplayMemberPath = "Name";
                ComboBox1.SelectedValuePath = "IdSt";
                ComboBox1.SelectedIndex = 0;
            }
            ComboBox2.Items.Add("5");
            ComboBox2.Items.Add("4");
            ComboBox2.Items.Add("3");
            ComboBox2.Items.Add("2");
            ComboBox2.Items.Add("1");
            ComboBox2.SelectedIndex = 0;
            dateTimePicker1.SelectedDate = DateTime.Today;
        }
        public string Titttle => "Выбран предмет: " + Predmt;

        private void LessonButton_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(ComboBox1.SelectedValue) != 0) {
                using (Jornal_DBEntities db = new Jornal_DBEntities())
                {
                    lesson less = new lesson { id_tch = User1.Value, id_st = Convert.ToInt32(ComboBox1.SelectedValue), id_pred = PredId, date_les = (DateTime)dateTimePicker1.SelectedDate };
                    db.lessons.Add(less);
                    db.SaveChanges();
                }
            }

        }
        private void MarkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(ComboBox1.SelectedValue) != 0)
            {
                using (Jornal_DBEntities db = new Jornal_DBEntities())
                {
                    mark mrk = new mark { mark1 = 5 - Convert.ToInt32(ComboBox2.SelectedIndex), id_st = Convert.ToInt32(ComboBox1.SelectedValue), id_pred = PredId, date_m = (DateTime)dateTimePicker1.SelectedDate };
                    db.marks.Add(mrk);
                    db.SaveChanges();
                }
            }

        }
    }

}
