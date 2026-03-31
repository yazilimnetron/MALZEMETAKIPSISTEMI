using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

/// <summary>
/// Summary description for SqlProccess
/// </summary>
/// 
namespace MALZEMETAKIPSISTEMI
{
    static class clSqlTanim
    {
        public static string connectionString;

        static clSqlTanim()
        {
            connectionString = ConfigurationManager.ConnectionStrings["veritabaniBaglantisi"].ConnectionString;
        }

        public static DataSet RunStoredProcDS(String strQuery, String strName)
        {
            return RunStoredProcDS(strQuery, strName, null);
        }

        public static DataSet RunStoredProcDS(String strQuery, String strName, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        sCommand.CommandTimeout = 600;
                        if (parameters != null)
                            sCommand.Parameters.AddRange(parameters);
                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            sAdapter.Fill(ds, strName);
                        }
                    }
                }
            }
            catch
            {
                ds = null;
                throw;
            }

            return ds;
        }

        public static DataTable RunStoredProc(String strQuery)
        {
            return RunStoredProc(strQuery, null);
        }

        public static DataTable RunStoredProc(String strQuery, SqlParameter[] parameters)
        {
            DataTable dt = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        sCommand.CommandTimeout = 600;
                        if (parameters != null)
                            sCommand.Parameters.AddRange(parameters);

                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            dt = new DataTable();
                            sAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                dt = null;
                throw;
            }

            return dt;
        }

        public static DataTable RunStoredProcDependency(String strQuery)
        {
            DataTable dt = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        sCommand.CommandTimeout = 600;

                        SqlDependency dependency = new SqlDependency(sCommand);
                        SqlDependency.Start(connectionString);
                        dependency.OnChange += OnDataChange;

                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            dt = new DataTable();
                            sAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                dt = null;
                throw;
            }

            return dt;
        }

        private static void OnDataChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert || e.Info == SqlNotificationInfo.Update || e.Info == SqlNotificationInfo.Delete)
            {
                MessageBox.Show("Data has changed!");

                SqlDependency dependency = (SqlDependency)sender;
                dependency.OnChange -= OnDataChange;
            }
        }


        public static SqlDataReader RunStoredProcDR(String strQuery)
        {
            try
            {
                // Connection kapatılmaz: reader kapatıldığında CommandBehavior.CloseConnection devreye girer
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand sCommand = new SqlCommand(strQuery, conn);
                sCommand.CommandTimeout = 600;
                conn.Open();
                return sCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }


        public static int ExecuteNonQuery(string sqlString)
        {
            return ExecuteNonQuery(sqlString, null);
        }

        public static int ExecuteNonQuery(string sqlString, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlString, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sqlString, CommandType type, SqlParameter[] paramArray)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlString, con))
            {
                cmd.CommandType = type;
                if (paramArray != null)
                    cmd.Parameters.AddRange(paramArray);
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static SqlDataReader ExecuteReader(string sqlString, CommandType type, SqlParameter[] paramArray)
        {
            // Connection kapatılmaz: reader kapatıldığında CommandBehavior.CloseConnection devreye girer
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.CommandType = type;
            if (paramArray != null)
                cmd.Parameters.AddRange(paramArray);
            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static void FillTree(TreeView tr, DataTable dt)
        {
            tr.Nodes.Clear();
            mucalsstree[] trs = new mucalsstree[dt.Rows.Count];
            int i = 0, sifir = 0, bir = 0;
            foreach (DataRow dr in dt.Rows)
            {
                trs[i] = new mucalsstree();
                trs[i].SetGetId = clGenelTanim.DBToInt32(dr["Id"]);
                trs[i].Text = clGenelTanim.DBToString(dr["Baslik"]);
                if (clGenelTanim.DBToInt32(dr["Lv"]) == 0)
                {
                    sifir = i;
                    tr.Nodes.Add(trs[i]);
                }
                if (clGenelTanim.DBToInt32(dr["Lv"]) == 1)
                {
                    trs[sifir].Nodes.Add(trs[i]);
                    bir = i;
                }
                if (clGenelTanim.DBToInt32(dr["Lv"]) == 2)
                {
                    trs[bir].Nodes.Add(trs[i]);
                }
                i++;
            }
        }

        public class mucalsstree : TreeNode
        {
            private int Id = 0;
            private int SiraNo = 0;
            public int SetGetId
            {
                get { return Id; }
                set { Id = value; }
            }
            public int SetGetSiraNo
            {
                get { return SiraNo; }
                set { SiraNo = value; }
            }
        }
    }
}
