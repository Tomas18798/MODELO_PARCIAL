using RecetasSLN.Datos;
using RecetasSLN.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Servicios
{
    class Gestor
    {
        private IDao DAO;

        public Gestor(AbstractDaoFactory factory)
        {
			DAO = factory.CrearRecetaDao();
        }

		public List<Ingredientes> ObtenerIngredientes()
		{
			return DAO.MostrarIngredientes();
		}

		public int ProximoNumero()
		{
			return DAO.ObtenerProximoNumero();
		}

		internal bool GuardarReceta(Receta oReceta)
		{
			return DAO.GuardarReceta(oReceta);
		}
	}
}
