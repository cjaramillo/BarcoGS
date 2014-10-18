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
    public partial class AsignarOCaPC : Form
    {
        Datos miClase = new Datos();
        string sqlQuery = "";
        int idCompraOC = 0, idPC = 0;
        string nroOC = "", fechaIngreso = "";

        public AsignarOCaPC()
        {
            InitializeComponent();
        }

        private void AsignarOCaPP_Load(object sender, EventArgs e)
        {
            cmbMuestraPC.SelectedIndex = 0;
            cmbMuestraOC.SelectedIndex = 0;
            cargaDatosOC(cmbMuestraOC.SelectedIndex);
        }

        void cargaDatosOC(int modo)
        {
            if (modo == 0) // OC Libres
                sqlQuery = @"
				    select compra.idcompra as 'idCompra', compra.Numero as 'Nro OC', compra.fechaIngreso as 'Fecha Emisión', compra.Fecha as 'Fecha' ,compra.FechaVencimiento as 'Fecha Vence', compra.Usuario as 'Usuario', 
					    count(detcompra.idDetCompra) as 'Nro Items', compra.Total as 'Total'
				    from Compra
					    inner join DetCompra on compra.idCompra=detcompra.idCompra
				    where compra.idTipoFactura=2 and compra.Borrar=0 and compra.Numero not in (select numeroOc from registroPC where numeroOc is not null) and compra.Usuario<>'OrdenLotes'
				    group by compra.Numero, compra.Fecha, compra.FechaVencimiento,compra.Usuario, compra.Total, compra.idCompra, compra.FechaIngreso
				    order by compra.idCompra asc
			    ";
            if (modo == 1) // Asignadas
                sqlQuery = @"
				    select compra.idcompra as 'idCompra', compra.Numero as 'Nro OC', compra.fechaIngreso as 'Fecha Emisión', compra.Fecha as 'Fecha' ,compra.FechaVencimiento as 'Fecha Vence', compra.Usuario as 'Usuario', 
					    count(detcompra.idDetCompra) as 'Nro Items', compra.Total as 'Total'
				    from Compra
					    inner join DetCompra on compra.idCompra=detcompra.idCompra
				    where compra.idTipoFactura=2 and compra.Borrar=0 and compra.Numero in (select numeroOc from registroPC where numeroOc is not null) and compra.Usuario<>'OrdenLotes'
				    group by compra.Numero, compra.Fecha, compra.FechaVencimiento,compra.Usuario, compra.Total, compra.idCompra, compra.FechaIngreso
				    order by compra.idCompra asc
			    ";
            if (modo == 2) // Todas
                sqlQuery = @"
				    select compra.idcompra as 'idCompra', compra.Numero as 'Nro OC', compra.fechaIngreso as 'Fecha Emisión', compra.Fecha as 'Fecha' ,compra.FechaVencimiento as 'Fecha Vence', compra.Usuario as 'Usuario', 
					    count(detcompra.idDetCompra) as 'Nro Items', compra.Total as 'Total'
				    from Compra
					    inner join DetCompra on compra.idCompra=detcompra.idCompra
				    where compra.idTipoFactura=2 and compra.Borrar=0  and compra.Usuario<>'OrdenLotes'
				    group by compra.Numero, compra.Fecha, compra.FechaVencimiento,compra.Usuario, compra.Total, compra.idCompra, compra.FechaIngreso
				    order by compra.idCompra asc
			    ";
            miClase.LlenaGrid(dgvOrdenesCompra, "compra", sqlQuery);
            formatoDGV(2);
        }

        private void formatoDGV(int dgv)
        {
            switch (dgv)
            {
                case 1:
                {
                    // Planes de Compra
                    dgvPlanesCompra.Columns[0].Width = 50;// Nro PC
                    dgvPlanesCompra.Columns[1].Width = 80;// Modo
                    dgvPlanesCompra.Columns[2].Width = 150;// Marca
                    dgvPlanesCompra.Columns[3].Width = 120;// Fecha
                    dgvPlanesCompra.Columns[4].Width = 100;// usuario
                    dgvPlanesCompra.Columns[5].Width = 100;// Numero OC
                    dgvPlanesCompra.Columns[6].Width = 100;// Fecha Asignación
                    dgvPlanesCompra.Columns[7].Width = 100;// Usuario Asignación
                    dgvPlanesCompra.Columns[8].Width = 80;// Fecha entrega DR.
                    dgvPlanesCompra.Columns[9].Width = 80;// Tipo PC
                    dgvPlanesCompra.Columns[10].Width = 400;// Observación
                } break;

                case 2:
                {
                    // Ordenes de Compra.
                    dgvOrdenesCompra.Columns[0].Visible = false; // idcompra va oculto
                    dgvOrdenesCompra.Columns[1].Width = 120; // Nro OC
                    dgvOrdenesCompra.Columns[2].Width = 160; // Fecha Emisión
                    dgvOrdenesCompra.Columns[3].Width = 160; // Fecha
                    dgvOrdenesCompra.Columns[4].Width = 160; // Fecha Vence
                    dgvOrdenesCompra.Columns[5].Width = 120; // Usuario
                    dgvOrdenesCompra.Columns[6].Width = 100; // Nro Items
                    dgvOrdenesCompra.Columns[7].Width = 120; // Total
                    dgvOrdenesCompra.Columns[7].DefaultCellStyle.Format = "F";

                } break;
            }
        }

        private void cmbMuestraPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbMuestraPC.SelectedIndex)
            {
                case 0: 
                    { 
                        // Todos
                        sqlQuery = @"
                            SELECT  RegistroPC.idRegistro AS 'Nro PC', CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', ArticuloMarca.Marca,
		                            registroPC.fecha AS 'Fecha Emisión', 
		                            RegistroPC.usuario AS 'Usuario', CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                                    CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                                    CASE WHEN registroPC.usuarioAsignacion IS NULL THEN 'LIBRE' ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                                    RegistroPC.fechaEntregaDr AS 'Fecha Entrega DR.', CASE WHEN registroPC.pcInformativo IS NULL THEN 'Aplica Control' ELSE 'PC Informativo' END AS 'Tipo PC',
                                    RegistroPC.Observacion
                            FROM    RegistroPC LEFT OUTER JOIN
			                            ArticuloMarca ON registroPC.idMarca = ArticuloMarca.idMarca
				            order by registroPC.idRegistro asc
			            ";
                    } break;
                case 1:
                    {
                        // Solo Asignados
                        sqlQuery = @"
				            SELECT  RegistroPC.idRegistro AS 'Nro PC', CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', ArticuloMarca.Marca,
		                            registroPC.fecha AS 'Fecha Emisión', 
		                            RegistroPC.usuario AS 'Usuario', CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                                    CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                                    CASE WHEN registroPC.usuarioAsignacion IS NULL THEN 'LIBRE' ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                                    RegistroPC.fechaEntregaDr AS 'Fecha Entrega DR.', CASE WHEN registroPC.pcInformativo IS NULL THEN 'Aplica Control' ELSE 'PC Informativo' END AS 'Tipo PC',
                                    RegistroPC.Observacion
                            FROM    RegistroPC LEFT OUTER JOIN
			                            ArticuloMarca ON registroPC.idMarca = ArticuloMarca.idMarca
                            where registroPC.idCompraOC is not null
				            order by registroPC.idRegistro asc
			            ";
                    } break;

                case 2:
                    {
                        // Solo Libres
                        sqlQuery = @"
				            SELECT  RegistroPC.idRegistro AS 'Nro PC', CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', ArticuloMarca.Marca,
		                        registroPC.fecha AS 'Fecha Emisión', 
		                        RegistroPC.usuario AS 'Usuario', CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                                CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                                CASE WHEN registroPC.usuarioAsignacion IS NULL THEN 'LIBRE' ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                                RegistroPC.fechaEntregaDr AS 'Fecha Entrega DR.', CASE WHEN registroPC.pcInformativo IS NULL THEN 'Aplica Control' ELSE 'PC Informativo' END AS 'Tipo PC',
                                RegistroPC.Observacion
                        FROM    RegistroPC LEFT OUTER JOIN
			                        ArticuloMarca ON registroPC.idMarca = ArticuloMarca.idMarca
                            where registroPC.idCompraOC is null and registroPC.pcInformativo is null
				            order by registroPC.idRegistro asc
			            ";
                    } break;

                case 3:
                    {
                        // Solo Informativos
                        sqlQuery = @"
                            SELECT  RegistroPC.idRegistro AS 'Nro PC', CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', ArticuloMarca.Marca,
		                            registroPC.fecha AS 'Fecha Emisión', 
		                            RegistroPC.usuario AS 'Usuario', CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                                    CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                                    CASE WHEN registroPC.usuarioAsignacion IS NULL THEN 'LIBRE' ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                                    RegistroPC.fechaEntregaDr AS 'Fecha Entrega DR.', CASE WHEN registroPC.pcInformativo IS NULL THEN 'Aplica Control' ELSE 'PC Informativo' END AS 'Tipo PC',
                                    RegistroPC.Observacion
                            FROM    RegistroPC LEFT OUTER JOIN
			                            ArticuloMarca ON registroPC.idMarca = ArticuloMarca.idMarca
                            where registroPC.pcInformativo=1
				            order by registroPC.idRegistro asc
			            ";
                    } break;
            }
            miClase.LlenaGrid(dgvPlanesCompra, "registroPC", sqlQuery);
            formatoDGV(1);
            dgvPlanesCompra.Focus();
        }


        private void btnAsignar_Click(object sender, EventArgs e)
        {
            // Asigna una OC a un PC
            try
            {
                idCompraOC = Int32.Parse(dgvOrdenesCompra[0,dgvOrdenesCompra.CurrentCell.RowIndex].Value.ToString());
                idPC = Int32.Parse(dgvPlanesCompra[0, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString());
                nroOC = dgvOrdenesCompra[1, dgvOrdenesCompra.CurrentCell.RowIndex].Value.ToString();
                fechaIngreso = dgvPlanesCompra[3, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString();
                sqlQuery = @"
                update registroPC set idCompraOC=" + idCompraOC + @", numeroOC='" + nroOC + @"', fechaAsignacion=getdate(), usuarioAsignacion=host_name(), fechaIngresoOc='" + fechaIngreso + @"',
                pcInformativo=null, fechaEntregaDr='" + dtpFecha.Text + @"' where idRegistro=" + idPC + @" ";
                miClase.EjecutaSql(sqlQuery, true);
                cargaDatosOC(cmbMuestraOC.SelectedIndex);
                cmbMuestraPC_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un plan de compras y una orden de compra para realizar la asignación.");
            }
           
        }

        private void btnLiberarPC_Click(object sender, EventArgs e)
        {
            // Liberar plan de compras.
            idPC = Int32.Parse(dgvPlanesCompra[0, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString());
            sqlQuery = @"
                update registroPC set idCompraOC=null, numeroOC=null, fechaAsignacion=null, usuarioAsignacion=null, fechaIngresoOc=null,
                fechaEntregaDr=null where idRegistro=" + idPC;
            miClase.EjecutaSql(sqlQuery, true);
            cargaDatosOC(cmbMuestraOC.SelectedIndex);
            cmbMuestraPC_SelectedIndexChanged(null, null);
        }

        private void btnInformativo_Click(object sender, EventArgs e)
        {
            // Setear como informativo el plan de compras.
            idPC = Int32.Parse(dgvPlanesCompra[0, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString());
            sqlQuery = @"
                update registroPC set idCompraOC=null, numeroOC=null, fechaAsignacion=null, usuarioAsignacion=null, fechaIngresoOc=null,
                fechaEntregaDr=null, pcInformativo=1 where idRegistro=" + idPC;
            miClase.EjecutaSql(sqlQuery, true);
            cargaDatosOC(cmbMuestraOC.SelectedIndex);
            cmbMuestraPC_SelectedIndexChanged(null, null);
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            // Setear como control el plan de compras.
            idPC = Int32.Parse(dgvPlanesCompra[0, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString());
            sqlQuery = @"
                update registroPC set pcInformativo=null where idRegistro=" + idPC;
            miClase.EjecutaSql(sqlQuery, true);
            cargaDatosOC(cmbMuestraOC.SelectedIndex);
            cmbMuestraPC_SelectedIndexChanged(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar.
            int idPC = Int32.Parse(dgvPlanesCompra[0, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString());
            sqlQuery = "update registroPC set observacion='"+txtObservacion.Text+"' where idRegistro="+idPC;
            miClase.EjecutaSql(sqlQuery, true);
            txtObservacion.Text = "";
            txtObservacion.Enabled = false;
            // refrescar.
            cmbMuestraPC_SelectedIndexChanged(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Carga el comentario que está en el dgv al textbox.
            txtObservacion.Text = dgvPlanesCompra[10, dgvPlanesCompra.CurrentCell.RowIndex].Value.ToString();
            txtObservacion.Enabled = true;
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            txtObservacion.Text = "";
            txtObservacion.Enabled = false;
        }

        private void cmbMuestraOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaDatosOC(cmbMuestraOC.SelectedIndex);
        }
    }
}
