using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace invoiceGenerator.PersistenceSql
{
    class DatabaseSetting
    {
        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string DatabaseName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool UseIntegratedSecurity { get; set; }

        public DatabaseSetting() { }

        public DatabaseSetting(string connectionString)
        {
            var c = new SqlConnectionStringBuilder(connectionString);
            ServerName = c.DataSource;
            DatabaseName = c.InitialCatalog;
            UserName = c.UserID;
            Password = c.Password;
            UseIntegratedSecurity = c.IntegratedSecurity;
        }

        public DatabaseSetting(
            string serverName,
            string databaseName,
            string userName,
            string password,
            bool useIntegratedSecurity = false
        )
        {
            this.ServerName = serverName;
            this.DatabaseName = databaseName;
            this.UserName = userName;
            this.Password = password;
            this.UseIntegratedSecurity = useIntegratedSecurity;
        }

        public string CreateConnectionString()
        {
            return CreateConnectionString(this);
        }
        public static string CreateConnectionString(DatabaseSetting dbSetting)
        {
            if (dbSetting == null)
            {
                return null;
            }
            var cBuilder = new SqlConnectionStringBuilder();
            cBuilder.ConnectTimeout = 30;
            cBuilder.DataSource = dbSetting.ServerName;
            cBuilder.InitialCatalog = dbSetting.DatabaseName;
            cBuilder.IntegratedSecurity = dbSetting.UseIntegratedSecurity;
            if (!dbSetting.UseIntegratedSecurity)
            {
                cBuilder.UserID = dbSetting.UserName;
                cBuilder.Password = dbSetting.Password;
            }
            cBuilder.MultipleActiveResultSets = true;

            return cBuilder.ConnectionString;
        }
    }
}
