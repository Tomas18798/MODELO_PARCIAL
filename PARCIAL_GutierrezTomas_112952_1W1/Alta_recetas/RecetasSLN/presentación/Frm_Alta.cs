
using RecetasSLN.Datos;
using RecetasSLN.Dominio;
using RecetasSLN.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN
{
    public partial class Frm_Alta : Form
    {
        private Accion _Modo;
        private Receta _Oreceta;
        private Gestor _Ogestor;


        public Frm_Alta()
        {
            InitializeComponent();
            _Ogestor = new Gestor(new DaoFactory());
            _Oreceta = new Receta();
        }

        public enum Accion
        {
            Create,
            Read,
            Update,
            Delete
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCheff.Text == "")
            {
                MessageBox.Show("Debe especificar un Cheff.", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCheff.Focus();
                return;
            }
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un detalle.", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProducto.Focus();
                return;
            }

            GuardarReceta();
        }

        private void GuardarReceta()
        {
            _Oreceta._nro = _Ogestor.ProximoNumero();
            _Oreceta._cheff = txtCheff.Text;
            _Oreceta._tipo = cboTipo.SelectedIndex;
            _Oreceta._nombre = textBox1.Text;

            if (_Modo.Equals(Accion.Create))
            {
                if (_Ogestor.GuardarReceta(_Oreceta))
                {
                    MessageBox.Show("Receta registrada con exito.", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Close();
                    //NuevaReceta();
                }
                else
                {
                    MessageBox.Show("ERROR. No se pudo registrar la receta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void NuevaReceta()
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo();

        }

        private void CargarCombo()
        {
            List<Ingredientes> lst = _Ogestor.ObtenerIngredientes();
            cboProducto.DataSource = lst;
            cboProducto.ValueMember = "_id";
            cboProducto.DisplayMember = "_nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un producto", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ExisteProductoEnGrilla(cboProducto.SelectedItem.ToString()))
            {
                MessageBox.Show("Este producto ya se encuentra registrado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ExisteProductoEnGrilla(cboProducto.SelectedItem.ToString()))
            {
                MessageBox.Show("Este producto ya se encuentra registrado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Ingredientes oIngrediente = (Ingredientes)cboProducto.SelectedItem;
            DetalleReceta detalle = new DetalleReceta();
            detalle._ingrediente = oIngrediente;
            detalle._cantidad = Convert.ToInt32(nudCantidad.Value);

            _Oreceta.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] { oIngrediente._id, oIngrediente._nombre, detalle._cantidad });
            CalcularTotalIngredientes();
        }

        private void CalcularTotalIngredientes()
        {

        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["ingrediente"].Value.Equals(text))
                    return true;
            }
            return false;
        }

       



        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
                _Oreceta.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
            }
        }
    }
}
