using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Tutorial.SqlConn;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity;

namespace Jornal
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        
    }
    static class User1
    {
        public static int Value { get; set; }
        public static int Prava { get; set; }
    }

    

}
