using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Dominio
{
    class Ingredientes
    {
        public int _id { get; set; }
        public string _nombre { get; set; }
        public string _unidad { get; set; }

		public Ingredientes(int id, string nombre, string unidad)
		{
			_id = id;
			_nombre = nombre;
			_unidad = unidad;
		}
		public Ingredientes()
		{
			_id = 0;
			_nombre = "";
			_unidad = "";
		}


		public override string ToString()
		{
			return _nombre;
		}
	}
}
