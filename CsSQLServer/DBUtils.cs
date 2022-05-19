using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tutorial.SqlConn
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-KJ58DRQ\SQLEXPRESS";
            string database = "Jornal_DB";
            string username = "sa";
            string password = "1234";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}