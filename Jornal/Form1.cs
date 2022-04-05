namespace Jornal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label4.Visible = true;
            Hide();
            Form2 about = new Form2();
            about.ShowDialog();

        }
    }
}