namespace Jornal
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользавателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предметыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.учебаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.report1 = new FastReport.Report();
            this.jornal_DBDataSet1 = new Jornal.Jornal_DBDataSet();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jornal_DBDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.comboBox1.Location = new System.Drawing.Point(244, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(189, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(488, 72);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(205, 21);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 147);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1135, 267);
            this.dataGridView1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1220, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.пользавателиToolStripMenuItem,
            this.предметыToolStripMenuItem,
            this.учебаToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // пользавателиToolStripMenuItem
            // 
            this.пользавателиToolStripMenuItem.Name = "пользавателиToolStripMenuItem";
            this.пользавателиToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.пользавателиToolStripMenuItem.Text = "Пользаватели";
            this.пользавателиToolStripMenuItem.Click += new System.EventHandler(this.пользавателиToolStripMenuItem_Click);
            // 
            // предметыToolStripMenuItem
            // 
            this.предметыToolStripMenuItem.Name = "предметыToolStripMenuItem";
            this.предметыToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.предметыToolStripMenuItem.Text = "Предметы";
            this.предметыToolStripMenuItem.Click += new System.EventHandler(this.предметыToolStripMenuItem_Click);
            // 
            // учебаToolStripMenuItem
            // 
            this.учебаToolStripMenuItem.Name = "учебаToolStripMenuItem";
            this.учебаToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.учебаToolStripMenuItem.Text = "Учеба";
            this.учебаToolStripMenuItem.Click += new System.EventHandler(this.учебаToolStripMenuItem_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(732, 72);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(202, 21);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(519, 444);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Отметить урок/оценку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // report1
            // 
            this.report1.NeedRefresh = false;
            this.report1.ReportResourceString = resources.GetString("report1.ReportResourceString");
            this.report1.Tag = null;
            this.report1.RegisterData(this.jornal_DBDataSet1, "jornal_DBDataSet1");
            // 
            // jornal_DBDataSet1
            // 
            this.jornal_DBDataSet1.DataSetName = "Jornal_DBDataSet";
            this.jornal_DBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 497);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Журнал";
            this.Activated += new System.EventHandler(this.Form2_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jornal_DBDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользавателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem предметыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem учебаToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox comboBox2;
        private FastReport.Report report1;
        private Jornal_DBDataSet jornal_DBDataSet1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}