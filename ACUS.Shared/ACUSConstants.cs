using System.Collections.Generic;

namespace ACUS.Shared
{
    public static class ACUSConstants
    {
        public static string DBConnectionString 
        { 
            get 
            { 
                return "Data Source=" + CRMDataDBInstance + ";Initial Catalog=" + CRMDataDBName + ";User ID=" + CRMDataDBUser + ";Password=" + CRMDataDBPassword;
            }
        }
        public static string DesDBConnectionString 
        {
            get
            {
                return "Data Source=" + CRMDesignerDBInstance + ";Initial Catalog=" + CRMDesignerDBName + ";User ID=" + CRMDesignerDBUser + ";Password=" + CRMDesignerDBPassword;
            }
        }
                
        public static DatabaseType SelectedDatabaseType { get; set; }
        public static string TablePrefix { get; set; }
        public static string CRMDataDBUser { get; set; }
        public static string CRMDataDBPassword { get; set; }
        public static string CRMDataDBInstance { get; set; }
        public static string CRMDataDBName { get; set; }
        public static string CRMDesignerDBUser { get; set; }
        public static string CRMDesignerDBPassword { get; set; }
        public static string CRMDesignerDBInstance { get; set; }
        public static string CRMDesignerDBName { get; set; }
        public static List<string> LstOutputData { get; set; }

        public static string ResultFilePath { get; set; }
    }

    public enum DatabaseType
    {
        SQLServer,
        Oracle
    }
}
