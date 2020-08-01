using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Expenditure: Repository
    {
        private const string getExpenditureItemsSql = @"SELECT * FROM [ExpenditureItems] WHERE ExpenditureId = @ExpenditureId";

        public static async Task<List<ExpenditureModel>> GetAllExpenditure(DateTime from, DateTime to)
        {
            try
            {
                var getInvoicesSql = @"SELECT * FROM [Expenditure] i WHERE i.[BillDate] >= @from AND i.[BillDate] <= @to";
                var expenditures = new List<ExpenditureModel>();

                using (var connection = OpenConnection())
                {
                    var allExpenditure = connection.Query<ExpenditureModel>(getInvoicesSql, new { @from = from, @to = to }).ToList();
                    foreach (var exp in allExpenditure)
                    {
                        var i = await GetExpenditureElements(connection, exp);
                        expenditures.Add(i);
                    }
                    return expenditures;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ExpenditureModel> GetExpenditure(int expenditureId)
        {
            try
            {
                var getExpenditureSql = @"SELECT e.Id, e.Amount, e.BillDate, e.[Name], e.Notes, e.[Type], eitems.[Name], eitems.Quantity, eitems.TotalPrice, eitems.UnitPrice
                                        FROM Expenditure e INNER JOIN ExpenditureItems eitems on e.Id = eitems.ExpenditureId where e.Id = @Id";
                using (var connection = OpenConnection())
                {
                    var exp = connection.Query<ExpenditureModel>(getExpenditureSql, new { @Id = expenditureId }).Single();
                    return await GetExpenditureElements(connection, exp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<ExpenditureModel>> GetExpendituresByType(int expenditureType)
        {
            try
            {
                var getExpenditureSql = @"SELECT e.Id, e.Amount, e.BillDate, e.[Name], e.Notes, e.[Type], eitems.[Name], eitems.Quantity, eitems.TotalPrice, eitems.UnitPrice
                                        FROM Expenditure e INNER JOIN ExpenditureItems eitems on e.Id = eitems.ExpenditureId where e.Type = @expenditureType";
                var expenditures = new List<ExpenditureModel>();

                using (var connection = OpenConnection())
                {
                    var exp = connection.Query<ExpenditureModel>(getExpenditureSql, new { @expenditureType = expenditureType }).ToList();
                    foreach (var e in exp)
                    {
                        var expenditure = await GetExpenditureElements(connection, e);
                        expenditures.Add(expenditure);
                    }
                }
                return expenditures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<int> AddExpenditure(ExpenditureModel expenditure)
        {
            try
            {
                var addExpenditureSql =
                    @"INSERT INTO Expenditure (Name, Type, Amount, BillDate, Notes) VALUES (@Name, @Type, @Amount, @BillDate, @Notes);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                var addExpenditureItemsSql = @"INSERT INTO [ExpenditureItems] (ExpenditureId, Name, Quantity, UnitPrice, TotalPrice)
                                        VALUES (@ExpenditureId, @Name, @Quantity, @UnitPrice, @TotalPrice)";

                using (var connection = OpenConnection())
                {
                    var expenditureId = connection.Query<int>(addExpenditureSql, expenditure).Single();
                    if(expenditure.ExpenditureItems != null)
                    {
                        foreach (var expenditureItem in expenditure.ExpenditureItems)
                        {
                            expenditureItem.ExpenditureId = expenditureId;
                            connection.Execute(addExpenditureItemsSql, expenditureItem);
                        }
                    }
                    return expenditureId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<ExpenditureModel> GetExpenditureElements(IDbConnection connection, ExpenditureModel expenditure)
        {
            var expenditureItems =
                           connection.Query<ExpenditureItemsModel>(getExpenditureItemsSql, new { @ExpenditureId = expenditure.Id }).ToList();
            expenditure.ExpenditureItems = expenditureItems;
            return expenditure;
        }
    }
}
