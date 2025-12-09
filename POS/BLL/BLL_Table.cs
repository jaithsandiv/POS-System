using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_Table
    {
        private readonly DAL_Table _dalTable = new DAL_Table();

        public DataTable GetTables()
        {
            return _dalTable.GetTables();
        }

        public int InsertTable(string tableNumber, int capacity, int createdBy)
        {
            return _dalTable.InsertTable(tableNumber, capacity, createdBy);
        }

        public bool UpdateTable(int tableId, string tableNumber, int capacity, int updatedBy)
        {
            return _dalTable.UpdateTable(tableId, tableNumber, capacity, updatedBy);
        }

        public bool DeleteTable(int tableId, int updatedBy)
        {
            return _dalTable.DeleteTable(tableId, updatedBy);
        }

        public DataTable GetTableById(int tableId)
        {
            return _dalTable.GetTableById(tableId);
        }

        public DataTable SearchTables(string keyword)
        {
            return _dalTable.SearchTables(keyword);
        }
    }
}
