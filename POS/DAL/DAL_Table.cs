using POS.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL
{
    internal class DAL_Table
    {
        public DataTable GetTables()
        {
            string query = @"
                SELECT 
                    table_id, 
                    table_number, 
                    capacity, 
                    status
                FROM [Table]
                WHERE status = 'A'";

            return Connection.ExecuteQuery(query);
        }

        public int InsertTable(string tableNumber, int capacity, int createdBy)
        {
            string query = @"
                INSERT INTO [Table] (table_number, capacity, created_by, status)
                VALUES (@table_number, @capacity, @created_by, 'A');
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@table_number", tableNumber),
                new SqlParameter("@capacity", capacity),
                new SqlParameter("@created_by", createdBy)
            };

            object result = Connection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public bool UpdateTable(int tableId, string tableNumber, int capacity, int updatedBy)
        {
            string query = @"
                UPDATE [Table]
                SET table_number = @table_number,
                    capacity = @capacity,
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE table_id = @table_id";

            SqlParameter[] parameters = {
                new SqlParameter("@table_id", tableId),
                new SqlParameter("@table_number", tableNumber),
                new SqlParameter("@capacity", capacity),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteTable(int tableId, int updatedBy)
        {
            string query = @"
                UPDATE [Table]
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE table_id = @table_id";

            SqlParameter[] parameters = {
                new SqlParameter("@table_id", tableId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable GetTableById(int tableId)
        {
            string query = @"
                SELECT 
                    table_id, 
                    table_number, 
                    capacity, 
                    status
                FROM [Table]
                WHERE table_id = @table_id";

            SqlParameter[] parameters = {
                new SqlParameter("@table_id", tableId)
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        public DataTable SearchTables(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetTables();
            }

            string query = @"
                SELECT 
                    table_id, 
                    table_number, 
                    capacity, 
                    status
                FROM [Table]
                WHERE status = 'A' AND (table_number LIKE @keyword)";

            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return Connection.ExecuteQuery(query, parameters);
        }
    }
}
