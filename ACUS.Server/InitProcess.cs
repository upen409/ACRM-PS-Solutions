using ACUS.Server.DBFactory;
using ACUS.Server.Helpers;
using ACUS.Shared;
using LiteX.DbHelper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ACUS.Server
{
    public class InitProcess
    {
        private IDatabaseFactory databaseFactory;
        private IDbHelper dbHelper;
        private IDbHelper desDbHelper;
        private DatabaseHelper databaseHelper;
        private DatabaseHelper desDatabaseHelper;

        private void InitDatabase()
        {
            databaseFactory = new DatabaseFactory();
            dbHelper = databaseFactory.GetDBContext(ACUSConstants.SelectedDatabaseType);
            desDbHelper = databaseFactory.GetDesignerDBContext(ACUSConstants.SelectedDatabaseType);
            databaseHelper = new DatabaseHelper(dbHelper);
            desDatabaseHelper = new DatabaseHelper(desDbHelper);
        }

        public IEnumerable<int> ProcessData(string xmlFilePath)
        {
            this.InitDatabase();
            this.InitOutputList();
            var elements = XElement.Load(xmlFilePath).Elements("Measurement");
            var totalRecords = elements.Count();
            var processedRecords = 0;
            foreach (XElement ele in elements)
            {
                var measurement = ele.Attribute("name").Value;
                //var measurement = ele.Attribute("exposedName").Value;
                var returnType = ele.Attribute("returnType").Value;
                XElement queriesEle = ACUSConstants.SelectedDatabaseType == DatabaseType.Oracle 
                                                        ? ele.Element("OracleQueries")
                                                        : ele.Element("SqlQueries");

                var measurementRes = new StringBuilder(measurement);
                measurementRes.Append(";");

                foreach (var query in queriesEle.Elements("Query"))
                {
                    var q = query.Value;
                    var isDesigner = Convert.ToBoolean(query.Attribute("isDesigner").Value);
                    var res = ExecuteQuery(q, returnType, isDesigner);
                    measurementRes.Append(res);
                    measurementRes.Append(";");
                }
               
                ACUSConstants.LstOutputData.Add(Convert.ToString(measurementRes).TrimEnd(';'));
                processedRecords++;
                var percentage = (processedRecords / totalRecords) * 100;
                yield return percentage;
            }
        }

        private object ExecuteQuery(string query, string returnType, bool isDesigner)
        {
            object returnValue = null;
            if (!string.IsNullOrEmpty(query))
            {
                query = query.Replace("{tablePrefix}", ACUSConstants.TablePrefix);
                //query = query.Replace("&lt;", "<");
                var dt = isDesigner ? desDatabaseHelper.GetDataTable(query) : databaseHelper.GetDataTable(query);
                if(dt != null && dt.Rows.Count > 0)
                {
                    if(returnType.Equals("int"))
                    {
                        returnValue = dt.Rows[0][0];
                    }
                    else if(returnType.Equals("bool"))
                    {
                        returnValue = Convert.ToBoolean(dt.Rows[0][0]) ? "Yes" : "No";
                    }
                }
            }
            return returnValue;
        }

        private void InitOutputList()
        {
            if (ACUSConstants.LstOutputData == null)
                ACUSConstants.LstOutputData = new List<string>();
            ACUSConstants.LstOutputData.Clear();
        }
    }
}
