using RecetasSLN.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Datos
{
    interface IDao
    {
        List<Ingredientes> MostrarIngredientes();
        bool GuardarReceta(Receta oReceta);
        int ObtenerProximoNumero();
    }
}
