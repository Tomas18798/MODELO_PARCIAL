using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Dominio
{
    class DetalleReceta
    {
        public Ingredientes _ingrediente { get; set; }
        public int _cantidad { get; set; }

		public DetalleReceta(int cantidad, Ingredientes ingrediente)
		{
			_cantidad = cantidad;
			_ingrediente = ingrediente;
		}
		public DetalleReceta()
		{
			_ingrediente = null;
			_cantidad = 0;
		}


	}
}
