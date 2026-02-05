using MALZEME_TAKIP_SISTEMI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALZEMETAKIPSISTEMI
{
    class RegisterDependency
    {
        public DataTable GetData()
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clSqlTanim.connectionString))
                {
                    conn.Open();

                    using (SqlCommand sCommand = new SqlCommand("Select COUNT(*) adet from TBL_LST_MALZEMEDEPOISTEM where MALZEMEDEPOISTEM_DURUM not in ('3','2')", conn))
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

    }
}
