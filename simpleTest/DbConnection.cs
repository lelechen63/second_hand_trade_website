using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace simpleTest
{
    public class DbConnection
    {
        SqlConnection myConnection = new SqlConnection("server=tcp:gadpc8ehhl.database.windows.net,1433;" +
                                       "Trusted_Connection=yes;" +
                                       "database=portalDB; " +
                                        "User ID=portalTeam@gadpc8ehhl; " +
                                        "Password=hell123$; " +
                                        "Trusted_Connection=False; " +
                                        "Encrypt=True;" +
                                       "connection timeout=30");
        public SqlConnection getConnection()
        {
            return myConnection;
        }
    }
}