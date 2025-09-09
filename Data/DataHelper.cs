using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppFacturacion1._5.Data
{
    public sealed class DataHelper
    {
        private const string CONNECTION_STRING =
            @"Data Source=DESKTOP-PVTAFR9\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True;Trust Server Certificate=True";

        private static DataHelper _instance;
        private DataHelper() { }
        public static DataHelper GetInstance() => _instance ??= new DataHelper();

        private SqlConnection GetConnection() => new SqlConnection(CONNECTION_STRING);

        private static void AddParameters(SqlCommand cmd, List<SpParameter> parameters)
        {
            if (parameters == null) return;

            foreach (var p in parameters)
            {
                var sqlParam = new SqlParameter
                {
                    ParameterName = p.Name,
                    Direction = p.Direction
                };

                if (p.Type.HasValue)
                {
                    sqlParam.SqlDbType = p.Type.Value;
                    if (p.Size.HasValue) sqlParam.Size = p.Size.Value;
                    sqlParam.Value = p.Value ?? DBNull.Value;
                }
                else
                {
                    // Inferencia simple
                    sqlParam.Value = p.Value ?? DBNull.Value;
                }

                cmd.Parameters.Add(sqlParam);
            }
        }

        public DataTable ExecuteSPQuery(string spName, List<SpParameter> parameters = null)
        {
            using var cn = GetConnection();
            using var cmd = new SqlCommand(spName, cn) { CommandType = CommandType.StoredProcedure };
            AddParameters(cmd, parameters);
            var table = new DataTable();
            cn.Open();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(table);
            return table;
        }

        public int ExecuteSPDML(string spName, List<SpParameter> parameters = null)
        {
            using var cn = GetConnection();
            using var cmd = new SqlCommand(spName, cn) { CommandType = CommandType.StoredProcedure };
            AddParameters(cmd, parameters);
            cn.Open();
            return cmd.ExecuteNonQuery();
        }

        // Devuelve INT de un parámetro de salida (por ejemplo @id_factura OUTPUT)
        public int ExecuteSPReturnOutputInt(string spName, List<SpParameter> parameters, string outputParamName)
        {
            using var cn = GetConnection();
            using var cmd = new SqlCommand(spName, cn) { CommandType = CommandType.StoredProcedure };
            AddParameters(cmd, parameters);
            cn.Open();
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(cmd.Parameters[outputParamName].Value);
        }
    }
}
