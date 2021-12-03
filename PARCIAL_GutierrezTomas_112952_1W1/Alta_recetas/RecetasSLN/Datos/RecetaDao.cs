using RecetasSLN.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RecetasSLN.Frm_Alta;

namespace RecetasSLN.Datos
{
    class RecetaDao : IDao
    {


        public bool GuardarReceta(Receta oReceta)
        {
            bool resultado = true;
            int filasAfectadas = 0;

            filasAfectadas = HelperDao.ObtenerInstancia().EjecutarSQLMaestroDetalle("SP_INSERTAR_RECETA", "SP_INSERTAR_DETALLES", oReceta, Accion.Create);

            if (filasAfectadas == 0) resultado = false;

            return resultado;
        }

        public List<Ingredientes> MostrarIngredientes()
        {
            List<Ingredientes> lst = new List<Ingredientes>();
            DataTable tabla = HelperDao.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_INGREDIENTES");
            foreach (DataRow row in tabla.Rows)
            {
                Ingredientes oIngredientes = new Ingredientes();
                oIngredientes._id = Convert.ToInt32(row["id_ingrediente"].ToString());
                oIngredientes._nombre = row["n_ingrediente"].ToString();
                oIngredientes._unidad = row["unidad_medida"].ToString();
                
                lst.Add(oIngredientes);
            }
            return lst;
        }

        public int ObtenerProximoNumero()
        {
            return HelperDao.ObtenerInstancia().ProximoID("pa_PROXIMO_ID", "@next");
        }
    }
}
