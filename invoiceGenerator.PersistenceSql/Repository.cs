using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoiceGenerator.PersistenceSql
{
    public class Repository
    {
        protected readonly IDbConnectionFactory connectionFactory;

        public static IDbConnection OpenConnection()
        {
            //var dbSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            var dbSettings = "Data Source=localhost;Initial Catalog=invoice; User Id=sa; password=pass@word1;";
            var connection = new SqlConnection(dbSettings);
            connection.Open();

            return connection;
        }
    }
}
