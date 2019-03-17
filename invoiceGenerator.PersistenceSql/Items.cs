using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Item: Repository
    {
        public static List<ItemsModel> GetAllItems()
        {
            try
            {
                var getAllItemsSql = @"SELECT Id, Name, Category, Price, IsAvailable FROM [Items]";
                using (var connection = OpenConnection())
                {
                    var allCustomers = connection.Query<ItemsModel>(getAllItemsSql).ToList();
                    return allCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ItemsModel GetItem(int itemId)
        {
            try
            {
                var getItemSql = @"SELECT Id, Name, Category, Price, IsAvailable FROM [Items] WHERE Id = @Id";
                using (var connection = OpenConnection())
                {
                    var item = connection.QueryFirstOrDefault<ItemsModel>(getItemSql, new {@Id = itemId});
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ItemsModel GetItem(string name, string category)
        {
            var getItemsSql = @"SELECT Id, Name, Category, Price, IsAvailable FROM [Items] WHERE ";
            if (!string.IsNullOrEmpty(category))
            {
                getItemsSql += string.IsNullOrEmpty(name) ? "" : " Name = @name and ";
                getItemsSql += @" Category = @category";
            }
            else if(!string.IsNullOrEmpty(name))
            {
                getItemsSql += @" Name = @name";
            }

            using (var connection = OpenConnection())
            {
                var item = connection.QueryFirstOrDefault<ItemsModel>(getItemsSql, new
                {
                    @name = name,
                    @category = category
                });
                return item;
            }

        }

        public static async Task<int> AddItem(ItemsModel item)
        {
            try
            {
                var addItemSql =
                    @"INSERT INTO [Items] (Name, Category, Price, IsAvailable )
                                        VALUES (@Name, @Category, @Price, @IsAvailable )";
                                    
                using (var connection = OpenConnection())
                {
                    var alreadyExists = GetItem(item.Name, item.Category);
                    if (alreadyExists != null)
                    {
                        return alreadyExists.Id;
                    }
                    var id = connection.Execute(addItemSql, item);
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateItem(ItemsModel item)
        {
            var updateItemSql = @"Update [Items] SET Name = @Name, Category = @Category, Price = @Price, IsAvailable = @IsAvailable
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateItemSql, item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteItem(int id)
        {
            var updateItemSql = @"Update [Items] SET IsAvailable = 'false'
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateItemSql, new{@Id = id});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
