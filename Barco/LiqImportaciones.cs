using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Barco
{
    public partial class LiqImportaciones : Form
    {
        public LiqImportaciones()
        {
            InitializeComponent();
        }

        private Datos miClase = new Datos();
        private string sqlQuery;

        private void cargarData()
        {
            // Cargar data al DS
            sqlQuery = @"SELECT * FROM CompraImportacion";
            miClase.llenaDS(ds1, sqlQuery, "CompraImportacion");
            sqlQuery = @"SELECT	CompraDetImp.idCompraDetImp, CompraDetImp.idImportacion, CompraDetImp.Fecha,CompraDetImp.Cliente, CompraDetImp.Codigo, CompraDetImp.CodAsiento, CompraDetImp.Total, 
		                    CompraDetImp.Iva, CompraDetImp.idTipoImportacion
                         FROM         CompraDetImp RIGHT OUTER JOIN
                                CompraImportacion ON CompraDetImp.idImportacion = CompraImportacion.idImportacion
                        ";
            miClase.llenaDS(ds1, sqlQuery, "CompraDetImp");
            if (ds1.Relations.Count < 1) 
            {
                ds1.Relations.Add("relacion1",
                               ds1.Tables["CompraImportacion"].Columns["idImportacion"],
                               ds1.Tables["CompraDetImp"].Columns["idImportacion"]);
                bs1 = new BindingSource(ds1, "CompraImportacion");
                bnControl.BindingSource = bs1;
                txtNumero.DataBindings.Add("Text", bs1, "numero");

                bs2 = new BindingSource(bs1, "relacion1");
                dgvImportaciones.DataSource = bs2;
            }
            estilizar(dgvImportaciones);
            errorProvider1.Clear();
            

            bs1.MoveLast();
        }

        private Double result;

        private void LiqImportaciones_Load(object sender, EventArgs e)
        {
            // Carga el Combo
            miClase.LlenaCombo(cmbArticulo, "articulo", "articulo", "idArticulo", "Articulo LIKE 'IG-%'", false);
            cargarData();
        }

        private void estilizar(DataGridView dgv) 
        {
            dgv.Columns[0].Visible = false; //idCompraDetImp
            dgv.Columns[1].Visible = false; //idImportacion
            dgv.Columns[2].Visible = true; //Fecha
            dgv.Columns[2].Width = 80;
            dgv.Columns[2].DefaultCellStyle.Format = "d";
            dgv.Columns[3].Visible = true; //Cliente
            dgv.Columns[3].Width = 280;
            dgv.Columns[4].Visible = true; // Codigo
            dgv.Columns[5].Visible = true; //CodAsiento
            dgv.Columns[6].Visible = true; //Total
            dgv.Columns[6].DefaultCellStyle.Format = "c";
            dgv.Columns[7].Visible = true; //Iva
            dgv.Columns[7].DefaultCellStyle.Format = "c";
            dgv.Columns[8].Visible = true; //idTipoImportacion
            dgv.Columns[8].HeaderText = "Importación";
            dgv.Columns[8].Width = 80;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        

        private void btnGenArticulo_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

        }

        private int nuevo, editar;
        private void btnNuevo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                nuevo++;
                DataRow dr = ds1.Tables["CompraImportacion"].NewRow();
                ds1.Tables["CompraImportacion"].Rows.Add(dr);
                dr = ds1.Tables["CompraDetImp"].NewRow();
                ds1.Tables["CompraDetImp"].Rows.Add(dr);
                bs1.MoveLast();
                cmbArticulo.Enabled = true;
                //btnGenArticulo.Enabled = true;
                bindingNavigatorMoveFirstItem.Enabled = false;
                bindingNavigatorMovePreviousItem.Enabled = false;
                bindingNavigatorMoveNextItem.Enabled = false;
                bindingNavigatorMoveLastItem.Enabled = false;
                bindingNavigatorPositionItem.Enabled = false;
                dgvImportaciones.Enabled = false;
                //errorProvider1.SetError(cmbArticulo, "Seleccione Artículo y Clic en Generar");
                btnNuevo.Visible = false;
                btnGuardar.Visible = true;
                btnEditar.Visible = false;
                btnDeshacer.Visible = true;
                btnBorrar.Enabled = false;
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("No Admitido");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guardar");
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            if (nuevo > 0)
            {
                cmbArticulo.Enabled = false;
                btnGenArticulo.Enabled = false;
                dgvImportaciones.Enabled = true;
                btnDeshacer.Visible = false;
                btnEditar.Visible = true;
                btnGuardar.Visible = false;
                btnNuevo.Visible = true;
                ds1.Tables["CompraDetImp"].Rows.Clear();
                ds1.Tables["CompraImportacion"].Rows.Clear();
                cargarData();
                nuevo = 0;
                return;
            }
            if (editar>0)
            {
                bindingNavigatorMoveFirstItem.Enabled = !false;
                bindingNavigatorMovePreviousItem.Enabled = !false;
                bindingNavigatorMoveNextItem.Enabled = !false;
                bindingNavigatorMoveLastItem.Enabled = !false;
                bindingNavigatorPositionItem.Enabled = !false;
                cmbArticulo.Enabled = false;
                btnGenArticulo.Enabled = false;
                btnNuevo.Visible = true;
                btnGuardar.Visible = !true;
                btnEditar.Visible = !false;
                btnDeshacer.Visible = !true;
                editar = 0;
                return;
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            cmbArticulo.Enabled = true;
            btnGenArticulo.Enabled = true;
            bindingNavigatorMoveFirstItem.Enabled = false;
            bindingNavigatorMovePreviousItem.Enabled = false;
            bindingNavigatorMoveNextItem.Enabled = false;
            bindingNavigatorMoveLastItem.Enabled = false;
            bindingNavigatorPositionItem.Enabled = false;
            cmbArticulo.Enabled = true;
            btnGenArticulo.Enabled=true;
            btnNuevo.Visible = false;
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnDeshacer.Visible = true;
            editar++;
        }

        private void dgvImportaciones_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /*
            if (dgvImportaciones.Rows.Count>0)
            {
                try
                {
                    result = dgvImportaciones.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDouble(x.Cells["Total"].Value));
                }
                catch (InvalidCastException ice)
                {
                    MessageBox.Show(ice.Message);
                }
                //txtSuma.Text = result.ToString();
            }
            */
        }
    }
}
