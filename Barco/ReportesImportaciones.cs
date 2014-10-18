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
    public partial class ReportesImportaciones : Form
    {
        private int numReporte = 0;

       


        public ReportesImportaciones()
        {
            InitializeComponent();
        }

        private void ReportesImportaciones_Load(object sender, EventArgs e)
        {
            cmbComprasPagos.SelectedIndex = 0;

        }

        void desactivaTodo()
        {
            errorProvider1.Dispose();
            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;
            txtIBG.Enabled = false;
            cmbComprasPagos.Enabled = false;
            chkAnticipos.Enabled = false;
            txtIG.Enabled = false;
        }



        private void btnGenerar_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            numReporte = 0;
            if (rbAuditaIBG.Checked)
                numReporte = 1;
            if (rbAlertasProduccion.Checked)
                numReporte = 2;
            if (rbAlertasEntrega.Checked)
                numReporte = 3;
            if (rbPlanCompras.Checked)
                numReporte = 4;
            if (rbReporteAnticipos.Checked)
                numReporte = 5;
            if (rbAnticiposOCIG.Checked)
                numReporte = 6;
            if (rbCruceAnticiposOCS.Checked)
                numReporte = 7;
            if (numReporte == 0)
                errorProvider1.SetError(rbAuditaIBG, "Debe seleccionar un tipo de Reporte");
            else
            {
                // Dependiendo del reporte selecciionado se inicia la validación
                int validador = 0;
                if (numReporte == 6)
                {
                    //Validar formato de IG.
                    if (txtIG.Text.Trim().Length == 0)
                    {
                        errorProvider1.SetError(txtIG, "El valor debe cumplir con el formato: IG-XXX-20XX");
                        validador = 1;
                    }
                    else
                    {
                        if ((txtIG.Text.Substring(0, 3).CompareTo("IG-") != 0) || (txtIG.Text.Length != 11) || (txtIG.Text.Substring(2, 1).CompareTo("-") != 0)
                                    || (txtIG.Text.Substring(6, 1).CompareTo("-") != 0) || (txtIG.Text.Substring(7, 2).CompareTo("20") != 0))
                        {
                            errorProvider1.SetError(txtIG, "El valor debe cumplir con el formato: IG-XXX-20XX");
                            validador = 1;
                        }
                        else
                            validador = 0;
                    }
                }
                if (numReporte == 1 || numReporte == 5)
                {
                    // Validar IBG
                    if (txtIBG.Text.Trim().Length == 0)
                    {
                        errorProvider1.SetError(txtIBG, "Debe ingresar un número de IBG");
                        validador = 1;
                    }
                    else
                    {
                        // Verificar si existe esa IBG
                        int nroRegistros = 0;
                        nroRegistros = new Datos().EjecutaEscalar("select isnull(count(*),0) from compra where idtipofactura=9 and numero='" + txtIBG.Text.Trim() + "'");
                        if (nroRegistros == 0)
                        {
                            errorProvider1.SetError(txtIBG, "Número de IBG no existe");
                            validador = 1;
                        }
                        else
                        {
                            if (nroRegistros > 1)
                            {
                                errorProvider1.SetError(txtIBG, "Existe mas de un IBG con ese número");
                                validador = 1;
                            }
                            else
                                validador = 0;
                        }
                    }
                }
                if (numReporte != 1 && numReporte != 5 && numReporte != 6)
                    validador = 0; // No hay nada que validar.
                PreviaReportes objprevia = new PreviaReportes();
                if (validador == 0)
                {
                    switch (numReporte)
                    {
                        case 1:
                            {
                                this.Cursor = Cursors.WaitCursor;
                                new Datos().EjecutaSql("exec sp_AuditaIBG '" + txtIBG.Text + "',0 ", false); // 20140719: Daniela me dijo que quiere solo los PC, OCS, OCC, PP, IBG
                                // En caso de que quieran llamar a todos los documentos habría que poner en modo extendido =1
                                
                                // Acá tengo que llamar al reporte.
                                objprevia.VerReporte("AuditaIBG.rpt", "");
                                objprevia.Titulo("Seguimiento IBG");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                        case 2:
                            {
                                new Datos().EjecutaSql("exec sp_generaNotificaciones 1", false);
                                this.Cursor = Cursors.WaitCursor;
                                objprevia.VerReporte("Alertas.rpt", "");
                                objprevia.Titulo("Alertas Producción");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                        case 3:
                            {
                                new Datos().EjecutaSql("exec sp_generaNotificaciones 2", false);
                                this.Cursor = Cursors.WaitCursor;
                                objprevia.VerReporte("Alertas.rpt", "");
                                objprevia.Titulo("Alertas Producción");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                        case 4:
                            {
                                this.Cursor = Cursors.WaitCursor;
                                DateTime dtFechaIni = (DateTime)this.dtpDesde.Value;
                                DateTime dtFechaFin = (DateTime)this.dtpHasta.Value;

                                string sql = "Exec sp_ReportePlanCompras '" + dtFechaIni.ToString("yyyyMMdd") + "','" + dtFechaFin.ToString("yyyyMMdd") + "'";
                                new Datos().EjecutaSql(sql, false);
                                objprevia.VerReporte("ReportePlanCompras.rpt", "");
                                objprevia.Titulo("Reporte Plan Compras");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                        case 5:
                            {
                                int anticipos = 0, modo = 0;
                                if (chkAnticipos.Checked)
                                    anticipos = 1;
                                else
                                    anticipos = 0;

                                if (cmbComprasPagos.SelectedIndex == 0)
                                    modo = 1;
                                if (cmbComprasPagos.SelectedIndex == 1)
                                    modo = 2;
                                if (cmbComprasPagos.SelectedIndex == 2)
                                    modo = 3;

                                if (btnGenerar.Text.CompareTo("Todo") == 0)
                                    anticipos = 0;
                                if (btnGenerar.Text.CompareTo("Con Saldos") == 0)
                                    anticipos = 0;

                                // Anticipos Pagados
                                this.Cursor = Cursors.WaitCursor;
                                new Datos().EjecutaSql("exec sp_reporteAnticipos '" + txtIBG.Text + "', " + modo.ToString() + "," + anticipos.ToString() + " "
                                    , false);
                                // Acá tengo que llamar al reporte.
                                objprevia.VerReporte("ReporteTodosPagos.rpt", "");
                                objprevia.Titulo("Seguimiento IBG");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;

                            } break;
                        case 6:
                            {
                                this.Cursor = Cursors.WaitCursor;
                                new Datos().EjecutaSql("exec sp_ReporteAnticiposPP '" + txtIG.Text + "'", false);
                                objprevia.VerReporte("AnticiposPP.rpt", "");
                                objprevia.Titulo("Anticipos PP");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                        case 7:
                            {
                                this.Cursor = Cursors.WaitCursor;
                                objprevia.VerReporte("ReporteCruceAnticipos.rpt", "");
                                objprevia.Titulo("Reporte Cruce Anticipos");
                                objprevia.MdiParent = this.MdiParent;
                                objprevia.Show();
                                this.Cursor = Cursors.Default;
                            } break;
                    }
                }
            }
        }

        private void rbAuditaIBG_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAuditaIBG.Checked)
            {
                desactivaTodo();
                txtIBG.Enabled = true;
            }
        }

        private void rbAlertasProduccion_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlertasProduccion.Checked)
            {
                desactivaTodo();
            }
        }

        private void rbAlertasEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlertasEntrega.Checked)
                desactivaTodo();
        }

        private void rbPlanCompras_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPlanCompras.Checked)
            {
                desactivaTodo();
                dtpDesde.Enabled = true;
                dtpHasta.Enabled = true;
            }
        }

        private void rbReporteAnticipos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReporteAnticipos.Checked)
            {
                desactivaTodo();
                txtIBG.Enabled = true;
                cmbComprasPagos.Enabled = true;
                chkAnticipos.Enabled = true;
            }
        }

        private void rbAnticiposOCIG_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAnticiposOCIG.Checked)
            {
                desactivaTodo();
                txtIG.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbCruceAnticiposOCS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCruceAnticiposOCS.Checked)
            {
                desactivaTodo();
            }
        }



    }
}
