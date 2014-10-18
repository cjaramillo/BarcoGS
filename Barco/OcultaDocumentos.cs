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
    public partial class OcultaDocumentos : Form
    {
        Datos miClase = new Datos();
        string sqlQuery;
        int idTipoFactura = 0;

       

        public OcultaDocumentos()
        {
            InitializeComponent();
        }

        private void OcultaDocumentos_Load(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbTipoFactura, "Por favor seleccione el tipo de documento con el que desea trabajar.");
        }

        private void activarBotones()
        {
            btnActualiza.Enabled = true;
            btnOcultaEspecifica.Enabled = true;
            btnOcultaTodas.Enabled = true;
            btnRestaura.Enabled = true;
            txtOC.Enabled = true;
        }
        
        private void cmbTipoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            activarBotones();
            errorProvider1.Dispose();
            dgvDocumentos.Columns.Clear();
            if (cmbTipoFactura.SelectedIndex == 0) 
            {
                lblTipoDoc.Text = "ORDEN DE COMPRA";
            }
            if (cmbTipoFactura.SelectedIndex == 1)
            {
                lblTipoDoc.Text = "PEDIDO PROVEEDOR";
            }
            cargarData(cmbTipoFactura.SelectedIndex);
        }

        private void cargarData(int tipo) 
        {
            if (tipo == 0)
            {
                // Trabajo con Ordenes de Compra.
                idTipoFactura = 2;
            }
            else
            {
                // Trabajo con PP
                idTipoFactura = 14;
            }
            btnActualiza.PerformClick();
        }

        private void formatoDGV()
        {
            dgvDocumentos.Columns[0].Width = 110; // Numero
            dgvDocumentos.Columns[1].Width = 250; // Nombre
            dgvDocumentos.Columns[2].Width = 100; // Fecha
            dgvDocumentos.Columns[2].DefaultCellStyle.Format = "d";

            dgvDocumentos.Columns[3].Width = 100; // Total
            dgvDocumentos.Columns[3].DefaultCellStyle.Format = "F";

            dgvDocumentos.Columns[4].Width = 100; // Nro Items
            dgvDocumentos.Columns[5].Width = 400; // Notas
        }

        private void btnRestaura_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = dgvDocumentos[0, dgvDocumentos.CurrentCell.RowIndex].Value.ToString();
                miClase.EjecutaSql("exec sp_ocultaDoc 2,"+idTipoFactura+",'" + nombre + "'", true);
                btnActualiza.PerformClick();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("No existen documentos para restaurar");
            }
        }

        private void btnOcultaTodas_Click(object sender, EventArgs e)
        {
            sqlQuery = "select * from compra where idtipofactura=" + idTipoFactura + " and Compra.Revisado=1 and Compra.Entregado=1 and Compra.Comprob1='PROCESADO' and Compra.Otro='LIQUIDADO'";
            if ((miClase.EjecutaEscalar(sqlQuery) == 0))
            {
                MessageBox.Show("No existe ningun documento para ocultar.");
            }
            else
            {
                miClase.EjecutaSql("exec sp_ocultaDoc 0,"+idTipoFactura+"", true);
                btnActualiza.PerformClick();
            }
        }

        private void btnOcultaEspecifica_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if (txtOC.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtOC, "Debe ingresar el número del documento que desea ocultar.");
            }
            else
            {
                if ((miClase.EjecutaEscalar("select * from compra where idtipofactura="+idTipoFactura+" and numero='" + txtOC.Text + "'") == 0))
                {
                    errorProvider1.SetError(txtOC, "No existe ningún documento con el número ingresado");
                }
                else
                {
                    miClase.EjecutaSql("exec sp_ocultaDoc 1,"+idTipoFactura+",'" + txtOC.Text + "'", true);
                    btnActualiza.PerformClick();
                }
            }
        }

        private void btnActualiza_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            txtOC.Text = "";
            sqlQuery = @"
				    select bk_Compra.Numero, cliente.Nombre, bk_Compra.Fecha, bk_Compra.Total, COUNT (bk_DetCompra.idCompra) as 'Nro Items',cast (isnull(bk_Compra.notas,'')as varchar(100)) as Notas
				    from bk_Compra 
				    inner join bk_DetCompra on bk_Compra.idcompra=bk_DetCompra.idcompra 
				    inner join Cliente on bk_Compra.idcliente=cliente.idCliente
				    where bk_Compra.idTipoFactura=" + idTipoFactura + @"
				    group by bk_Compra.numero, cliente.nombre, bk_Compra.fecha, bk_Compra.total, cast (isnull(bk_Compra.notas,'')as varchar(100))
			";
            miClase.LlenaGrid(dgvDocumentos, "bk_Compra", sqlQuery);
            formatoDGV();
            pbTutorial.Focus();
        }
    }
}
