using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Settings: Repository
    {
        public static SettingModel GetSetting(string key)
        {
            try
            {
                var getSettingSql = @"SELECT Id, Key, Value, Group, CreatedOn, UpdatedOn FROM [Settings] WHERE [Key] = @Key";
                using (var connection = OpenConnection())
                {
                    var setting = connection.QueryFirstOrDefault<SettingModel>(getSettingSql, new { @Key = key});
                    return setting;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SettingModel> GetSettingByGroup(string group)
        {
            try
            {
                var getSettingSql = @"SELECT Id, Key, Value, Group, CreatedOn, UpdatedOn FROM [Settings] WHERE [Group] = @Group";
                using (var connection = OpenConnection())
                {
                    var settings = connection.Query<SettingModel>(getSettingSql, new { @Group = group }).ToList();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static async Task<int> AddSetting(SettingModel setting)
        {
            try
            {
                var addSettingSql =
                    @"INSERT INTO [Settings] (Key, Value, Group, CreatedOn, UpdatedOn)
                                        VALUES (@Key, @Value, @Group, @CreatedOn, @UpdatedOn);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                                    
                using (var connection = OpenConnection())
                {
                    var alreadyExists = GetSetting(setting.Key);
                    if (alreadyExists != null)
                    {
                        return alreadyExists.Id;
                    }
                    var id = connection.Query<int>(addSettingSql, setting).Single();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateEmployee(SettingModel setting)
        {
            var updateSettingSql = @"Update Settings SET Value = @Value, UpdatedOn = @UpdatedOn, Group = @Group
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateSettingSql, setting);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteEmployee(int settingId)
        {
            var deleteSettingSql = @"Delete Settings WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(deleteSettingSql, new { @Id = settingId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
