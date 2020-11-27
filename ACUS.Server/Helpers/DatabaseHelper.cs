using LiteX.DbHelper.Core;
using System.Data;

namespace ACUS.Server.Helpers
{
    class DatabaseHelper
    {
        private IDbHelper dbHelper;

        public DatabaseHelper(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;            
        }

        public DataTable GetDataTable(string query)
        {
            return dbHelper.GetDataTable(query, CommandType.Text);
        }
    }
}
