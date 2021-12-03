using RecetasSLN.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RecetasSLN.Frm_Alta;

namespace RecetasSLN.Datos
{
    class HelperDao
    {
		private static HelperDao instancia;
		private string cadenaConexion;
		SqlConnection conexion;
		SqlCommand comando;
		private HelperDao()
		{
			cadenaConexion = Properties.Resources.strConexion;
			conexion = new SqlConnection(cadenaConexion);
			comando = new SqlCommand();
		}
		public static HelperDao ObtenerInstancia()
		{
			if (instancia == null)
			{
				instancia = new HelperDao();
			}
			return instancia;
		}
		//		---------
		public void SetearComando(CommandType type, string CommandText)
		{
			comando.Connection = conexion;
			comando.CommandType = type;
			comando.CommandText = CommandText;
		}
		public DataTable ConsultaSQL(string nombreSP)
		{
			DataTable tabla = new DataTable();
			try
			{
				comando.Parameters.Clear();
				conexion.Open();

				SetearComando(CommandType.StoredProcedure, nombreSP);
				//comando.Connection = cnn;
				//comando.CommandType = CommandType.StoredProcedure;
				//comando.CommandText = nombreSP;


				tabla.Load(comando.ExecuteReader());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conexion.State == ConnectionState.Open)
					conexion.Close();
			}
			return tabla;
		}
		public int ProximoID(string nombreSP, string nombreParam)
		{
			SqlParameter param = new SqlParameter(nombreParam, SqlDbType.Int);
			try
			{
				comando.Parameters.Clear();
				conexion.Open();

				SetearComando(CommandType.StoredProcedure, nombreSP);
				//comando.Connection = cnn;
				//comando.CommandType = CommandType.StoredProcedure;
				//comando.CommandText = nombreSP;

				param.Direction = ParameterDirection.Output;
				comando.Parameters.Add(param);
				comando.ExecuteNonQuery();
				return (int)param.Value;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conexion.State == ConnectionState.Open)
					conexion.Close();
			}
		}
		public int EjecutarSQLMaestroDetalle(string spMaestro, string spDetalle, Receta oReceta, Accion modo)
		{
			int filasAfectadas = 0;
			SqlTransaction transaccion = null;

			try
			{
				comando.Parameters.Clear();
				conexion.Open();
				transaccion = conexion.BeginTransaction();
				comando.Transaction = transaccion;

				SetearComando(CommandType.StoredProcedure, spMaestro);
				//cmd.Connection = cnn;
				//cmd.CommandText = spMaestro;
				//cmd.CommandType = CommandType.StoredProcedure;

				if (modo == Accion.Create)
				{
					comando.Parameters.AddWithValue("@id_receta", oReceta._nro);
					comando.Parameters.AddWithValue("@tipo_receta", oReceta._tipo);
					comando.Parameters.AddWithValue("@nombre", oReceta._nombre);
					comando.Parameters.AddWithValue("@cheff", oReceta._cheff);
					comando.ExecuteNonQuery();
				}
				//if (modo == Accion.Update)
				//{
				//	comando.Parameters.AddWithValue("@nro_presupuesto", oFactura._nro);
				//	comando.Parameters.AddWithValue("@fecha", oFactura._fecha);
				//	comando.Parameters.AddWithValue("@cliente", oFactura._cliente);
				//	comando.Parameters.AddWithValue("@total", oFactura._total);
				//	comando.ExecuteNonQuery();
				//}

				int detNro = 1;

				foreach (DetalleReceta item in oReceta.detReceta)
				{
					SqlCommand cmdDet = new SqlCommand();
					cmdDet.Connection = conexion;
					cmdDet.Transaction = transaccion;
					cmdDet.CommandText = spDetalle;
					cmdDet.CommandType = CommandType.StoredProcedure;

					cmdDet.Parameters.AddWithValue("@id_receta", oReceta._nro);
					cmdDet.Parameters.AddWithValue("@id_ingrediente", item._ingrediente._id);
					cmdDet.Parameters.AddWithValue("@cantidad", item._cantidad);
					filasAfectadas = cmdDet.ExecuteNonQuery();
					detNro++;
				}

				transaccion.Commit();
			}
			catch (Exception e)
			{
				throw e;
				string mensaje = e.Message;
				transaccion.Rollback();
				filasAfectadas = 0;
			}
			finally
			{
				if (conexion != null && conexion.State == ConnectionState.Open) conexion.Close();
			}

			return filasAfectadas;
		}
	
}
}
