using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace NikeSpliteBox
{
    public static class SqlHelper
    {
        //protected static readonly string SMSSqlConnection = ConfigurationManager.ConnectionStrings["SMS"].ConnectionString.ToString();

        private static readonly string Constring = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString();
        //ConfigurationManager.AppSettings["ConStr"].ToString();
        public static DataTable GetDateTable(string sqlstring)
        {
            DataSet ds = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(Constring))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(ds);
                sqlConnection.Close();
            }
            if(ds!=null && ds.Tables.Count>0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public static bool ExecSql(string sqlstring)
        {
            int rowcount = 0;
            using (SqlConnection sqlConnection = new SqlConnection(Constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
                rowcount = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return rowcount > 0 ? true : false;
        }

        public static void InsertList<T>(string TableName,List<T> list,string sqlupdate="")
        {
            DataTable dataTable = new DataTable();
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (var item in props)
            {
                dataTable.Columns.Add(item.Name,item.PropertyType);
            }
            for (int i = 0; i < list.Count; i++)
            { 
                DataRow dataRow = dataTable.NewRow();
                foreach (var item in props)
                {
                    var val = item.GetValue(list[i], null);
                    //Type t = item.GetType();
                    dataRow[item.Name] = val;

                }
                dataTable.Rows.Add(dataRow);
            }

            SqlConnection sqlConnection = new SqlConnection(Constring);
            sqlConnection.Open();
            using (SqlTransaction tran = sqlConnection.BeginTransaction())
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, tran);
                sqlBulkCopy.DestinationTableName = TableName;
                sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                try
                {
                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    if (!string.IsNullOrEmpty(sqlupdate))
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlupdate, sqlConnection);
                        sqlCommand.Transaction = tran;
                        sqlCommand.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }
                sqlBulkCopy.Close();
            }
            sqlConnection.Close();
        }

        public static List<T> DataTableToList<T>(this DataTable dt) where T:new()
        {
            
            if (dt == null || dt.Rows.Count <= 0)
                return null;
            Type t = typeof(T);
            List<T> tList = new List<T>();
            List<PropertyInfo> props = t.GetProperties().ToList();
            var cols = dt.Columns;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T tmodel = new T();
                foreach (DataColumn col in cols)
                {
                    if (props.Select(m => m.Name).Contains(col.ColumnName))
                    {
                        var pi = props.Find(m => m.Name == col.ColumnName);
                        if (!pi.CanWrite) continue;
                        object value = dt.Rows[i][col.ColumnName];
                        pi.SetValue(tmodel, value, null);
                    }
                }
                tList.Add(tmodel);
            }
            return tList;
        }
    }
}
