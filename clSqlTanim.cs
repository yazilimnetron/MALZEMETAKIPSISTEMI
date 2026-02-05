using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

/// <summary>
/// Summary description for SqlProccess
/// </summary>
/// 
namespace MALZEME_TAKIP_SISTEMI
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
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        sCommand.CommandTimeout = 600;
                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            sAdapter.Fill(ds, strName);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ds = null;
                throw ex;
            }

            return ds;
        }

        public static DataTable RunStoredProc(String strQuery)
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

                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            dt = new DataTable();
                            sAdapter.Fill(dt);
                       }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;
                
                throw ex;
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
            catch (Exception ex)
            {
                dt = null;

                throw ex;
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

                //// Re-register the SqlDependency to continue receiving notifications.
                //RegisterDependency();
            }
        }


        public static SqlDataReader RunStoredProcDR(String strQuery)
        {
            SqlDataReader dr = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sCommand = new SqlCommand(strQuery, conn))
                    {
                        conn.Open();
                        sCommand.CommandTimeout = 600;
                        using (SqlDataAdapter sAdapter = new SqlDataAdapter(sCommand))
                        {
                            dr = sCommand.ExecuteReader(CommandBehavior.CloseConnection); return dr;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                dr = null;
                throw ex;
            }
        }


        public static int ExecuteNonQuery(string sqlString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                return cmd.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public static object ExecuteScalar(string sqlString, CommandType type, SqlParameter[] paramArray)
        {

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);

            cmd.CommandType = type;

            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                return cmd.ExecuteScalar();
            }

            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public static SqlDataReader ExecuteReader(string sqlString, CommandType type, SqlParameter[] paramArray)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);

            cmd.CommandType = type;

            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                return cmd.ExecuteReader((CommandBehavior.CloseConnection));
            }
            catch (SqlException exp)
            {
                throw exp;
            }
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
