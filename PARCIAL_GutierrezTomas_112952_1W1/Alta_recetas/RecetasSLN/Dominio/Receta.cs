using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Dominio
{
    class Receta
    {
        public int _nro { get; set; }
        public string _nombre { get; set; }
        public int _tipo { get; set; }
        public string _cheff { get; set; }

        public List<DetalleReceta> detReceta { get; set; }

        public Receta()
        {
            detReceta = new List<DetalleReceta>();
        }

		public void AgregarDetalle(DetalleReceta detalle)
		{
			detReceta.Add(detalle);
		}
		public void QuitarDetalle(int indice)
		{
			detReceta.RemoveAt(indice);
		}


	}
}
