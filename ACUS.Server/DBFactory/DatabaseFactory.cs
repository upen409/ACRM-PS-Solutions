using ACUS.Shared;
using LiteX.DbHelper.Core;
using LiteX.DbHelper.Oracle;
using LiteX.DbHelper.SqlServer;

namespace ACUS.Server.DBFactory
{
    class DatabaseFactory : IDatabaseFactory
    {
        public IDbHelper GetDBContext(DatabaseType databaseType)
        {
            IDbHelper dbHelper = null;
            switch (databaseType)
            {
                case DatabaseType.SQLServer:
                    dbHelper = new SqlHelper(ACUSConstants.DBConnectionString);
                    break;
                case DatabaseType.Oracle:
                    dbHelper = new OracleHelper(ACUSConstants.DBConnectionString);
                    break;                
                default:
                    dbHelper = new SqlHelper(ACUSConstants.DBConnectionString);
                    break;
            }
            
            return dbHelper;
        }

        public IDbHelper GetDesignerDBContext(DatabaseType databaseType)
        {
            IDbHelper dbHelper = null;
            switch (databaseType)
            {
                case DatabaseType.SQLServer:
                    dbHelper = new SqlHelper(ACUSConstants.DesDBConnectionString);
                    break;
                case DatabaseType.Oracle:
                    dbHelper = new OracleHelper(ACUSConstants.DesDBConnectionString);
                    break;
                default:
                    dbHelper = new SqlHelper(ACUSConstants.DesDBConnectionString);
                    break;
            }

            return dbHelper;
        }
    }
}
