using ACUS.Shared;
using LiteX.DbHelper.Core;

namespace ACUS.Server.DBFactory
{
    interface IDatabaseFactory
    {
        IDbHelper GetDBContext(DatabaseType databaseType);

        IDbHelper GetDesignerDBContext(DatabaseType databaseType);
    }
}
