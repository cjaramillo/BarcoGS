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
using Barco.CAD;
using Barco.CAD.Clases;
using System.Diagnostics;
using System.IO;


using Excel = Microsoft.Office.Interop.Excel;




namespace Barco
{
    public partial class Auditoria : Form
    {
        #region "variables"
        Datos miClase = new Datos();
        public string sqlQuery = "";
        int flagRadioSeleccionado = 0;
        Size resolucion = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;

        // Capa de Datos.
        AccesoDatosAOCS adaocs = new AccesoDatosAOCS();
        AccesoDatosPC adpc = new AccesoDatosPC();
        AccesoDatosOCS adocs = new AccesoDatosOCS();
        AccesoDatosOCC adocc = new AccesoDatosOCC();
        AccesoDatosFactNorm adfn = new AccesoDatosFactNorm();
        AccesoDatosIBG adibg = new AccesoDatosIBG();
        AccesoDatosPagosAnticipos adpa = new AccesoDatosPagosAnticipos();
        AccesoDatosPagosFactNorm adpfn = new AccesoDatosPagosFactNorm();
        AccesoDatosPP adpp = new AccesoDatosPP();
        AccesoDatosSPAnticipos adspa = new AccesoDatosSPAnticipos();
        AccesoDatosSPFactNorm adspfn = new AccesoDatosSPFactNorm();

        // Estas creo que son basura.
        DateTime dt1, dt2;

        // Estas me sirven para la opción de ampliar - posiciones originales de los DGV
        private Point p1 = new Point(9, 32);
        private Point p2 = new Point(496, 32);
        private Point p3 = new Point(980, 32);
        private Point p4 = new Point(7, 234);
        private Point p5 = new Point(496, 234);
        private Point p6 = new Point(980, 234);
        private Point p7 = new Point(9, 437);
        private Point p8 = new Point(496, 437);
        private Point p9 = new Point(980, 437);
        private Point p10 = new Point(10, 649);
        private Point p11 = new Point(496, 649);
        private Point ampliado = new Point(120, 78);
        private int width_original = 452;
        private int height_original = 167;
        private int width_modif = 1159;
        private int height_modif = 547;

        #endregion

        public Auditoria()
        {
            InitializeComponent();
        }

        private void btnRefrescar_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Vuelve a cargar todos los datos.", btnRefrescar, 3000);
        }

        private void btnGuardaObservacion_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Guarda la observación ingresada en el cuadro de texto.", btnGuardaObservacion, 3000);
        }

        private void desactivaFiltros() 
        {
            flagRadioSeleccionado = 0;
            rbAnticiposOCS.Checked = false;
            rbFacturasNormal.Checked = false;
            rbIBG.Checked = false;
            rbOCC.Checked = false;
            rbOCS.Checked = false;
            rbPagosAnticiposOCS.Checked = false;
            rbPagosNormal.Checked = false;
            rbPC.Checked = false;
            rbPP.Checked = false;
            rbSPAnticiposOCS.Checked = false;
            rbSpPagosNormal.Checked = false;
            handlerLblNoData(0);
        }

        private void desactivaColoresLabel ()
        { 
            lbl1.ForeColor = Color.Blue;
            lbl2.ForeColor = Color.Blue;
            lbl3.ForeColor = Color.Blue;
            lbl4.ForeColor = Color.Blue;
            lbl5.ForeColor = Color.Blue;
            lbl6.ForeColor = Color.Blue;
            lbl7.ForeColor = Color.Blue;
            lbl8.ForeColor = Color.Blue;
            lbl9.ForeColor = Color.Blue;
            lbl10.ForeColor = Color.Blue;
            lbl11.ForeColor = Color.Blue;
        }

        private void desactivaFiltros(CheckBox chk) // Desactiva todos menos el que recibe
        {
            desactivaFiltros();
        }

        private void resaltaRb(RadioButton rb) // Resalta el control que recibe.
        {
            if (rb.Checked)
            {
                rb.ForeColor = Color.Red;
            }
            else
            {
                rb.ForeColor = Color.Black;
            }
        }

        private void btnDesactivaFiltros_Click(object sender, EventArgs e)
        {
            desactivaFiltros();
        }

        void handlerLblNoData(int modo)
        {
            if(modo==0)
            {
                // Quita los labels de "Sin Datos" 
                lblNoDataPC.Visible = false;
                lblNoDataOCS.Visible = false;
                lblNoDataOCC.Visible = false;
                lblNoDataAnticipos.Visible = false;
                lblNoDataSPAnticipos.Visible = false;
                lblNoDataPagoAnticipos.Visible = false;
                lblNoDataFacturasNormal.Visible = false;
                lblNoDataSPNormal.Visible = false;
                lblNoDataPagosNormal.Visible = false;
                lblNoDataPP.Visible = false;
                lblNoDataIBG.Visible = false;
            }
            if (modo == 1)
            {
                // Borra todos los dgv y muestra los label de no data.
                lblNoDataPC.Visible = true;
                lblNoDataOCS.Visible = true;
                lblNoDataOCC.Visible = true;
                lblNoDataAnticipos.Visible = true;
                lblNoDataSPAnticipos.Visible = true;
                lblNoDataPagoAnticipos.Visible = true;
                lblNoDataFacturasNormal.Visible = true;
                lblNoDataSPNormal.Visible = true;
                lblNoDataPagosNormal.Visible = true;
                lblNoDataPP.Visible = true;
                lblNoDataIBG.Visible = true;
                // Borrar DGV
                dgvPC.Columns.Clear();
                dgvOCS.Columns.Clear();
                dgvOCC.Columns.Clear();
                dgvAnticiposOCS.Columns.Clear();
                dgvSPAnticiposOCS.Columns.Clear();
                dgvPagosAnticiposOCS.Columns.Clear();
                dgvFacturasNormales.Columns.Clear();
                dgvSPFacturaNormal.Columns.Clear();
                dgvPagosFacturaNormal.Columns.Clear();
                dgvPP.Columns.Clear();
                dgvIBG.Columns.Clear();
            }
        }

        private void rbPC_CheckedChanged(object sender, EventArgs e)
        {
            int error = 0, PPcargado=0;
            //int idRegistro = 0;
            int idPC, idOCS, idOCC, idPP, idIBG;
            if (rbPC.Checked)
            {
                flagRadioSeleccionado = 1;
                // Hay que ver si tengo datos seleccionados en este dgv.
                idPC = 0;
                try
                {
                    idPC = Int32.Parse(dgvPC.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (Exception)
                {
                    error = 1;
                }

                if (error == 1)
                    handlerLblNoData(1); // no hay nada seleccionado ni que mostrar. - Muestro todos los dgv en blanco sin datos.
                else
                {
                    adpc.llenaGrid(dgvPC, idPC);
                    idOCS = adpc.up_OCS(idPC);
                    if (idOCS != -1)
                    {
                        adocs.llenaGrid(dgvOCS, idOCS, idPC); // Mostrar OCS
                        // Voy a buscar si tiene OCC.
                        idOCC=adocs.up_OCC(idOCS);
                        if (idOCC != -1)
                        {
                            adocc.llenaGrid(dgvOCC, idOCC); // Mostrar OCC
                            if (adocc.validaNombreIG(idOCC))
                            {
                                // Voy a buscar facturas normales, con sus SP y sus pagos desde una orden de compra consolidada, en caso de que la tuviese:
                                adfn.llenaGrid2(dgvFacturasNormales, adocc.up_idArticulo(idOCC));
                                if (dgvFacturasNormales.RowCount < 1)
                                {
                                    // No hay nada que mostrar
                                    dgvFacturasNormales.Columns.Clear();
                                    lblNoDataFacturasNormal.Visible = true;
                                    dgvSPFacturaNormal.Columns.Clear();
                                    lblNoDataSPNormal.Visible = true;
                                    dgvPagosFacturaNormal.Columns.Clear();
                                    lblNoDataPagosNormal.Visible = true;
                                }
                                else
                                {
                                    // Aqui hay que cargar los SP y los pagos asociados a esas facturas en caso de que existan.
                                    int[] idCompraSPFN = new int[100];
                                    idCompraSPFN = adocc.up_spFacturasNormales(adocc.up_idArticulo(idOCC));
                                    if (idCompraSPFN[0] != -1)
                                        adspfn.llenaGrid(dgvSPFacturaNormal, idCompraSPFN);
                                    else
                                    {
                                        // No hay ningún SP asociado a este idArticulo.
                                        dgvSPFacturaNormal.Columns.Clear();
                                        lblNoDataSPNormal.Visible = true;
                                    }
                                    // Aquí va la carga de los pagos de facturas normales proveedores.
                                    int[] idCompraFN = new int[100];
                                    for (int i = 0; i < 100; i++)
                                        idCompraFN[i] = 0;
                                    for (int i = 0; i < dgvFacturasNormales.RowCount; i++)
                                        idCompraFN[i] = Int32.Parse(dgvFacturasNormales.Rows[i].Cells["idCompra"].Value.ToString()); // Acá me tira error.. seguramente no estoy recuperando la data correctamente.
                                    adpfn.llenaGrid(dgvPagosFacturaNormal, idCompraFN);
                                    if (dgvPagosFacturaNormal.RowCount < 1)
                                    {
                                        // Ningún pago que mostrar.
                                        dgvPagosFacturaNormal.Columns.Clear();
                                        lblNoDataPagosNormal.Visible = true;
                                    }
                                }
                            }

                            // Buscar si tengo PP creado desde OCC.
                            idPP=adocc.up_PP(idOCC);
                            if (idPP != -1)
                            {
                                adpp.llenaGrid(dgvPP, idPP); // cargar el PP
                                PPcargado = 1;
                                // Buscar si tengo IBG
                                idIBG=adpp.up_IBG(idPP);
                                if (idIBG != -1)
                                    adibg.llenaGrid(dgvIBG, idIBG);
                                else
                                {
                                    // No hay IBG
                                    dgvIBG.Columns.Clear();
                                    lblNoDataIBG.Visible = true;
                                }
                            }
                            else
                            {
                                // No hay PP
                                dgvPP.Columns.Clear();
                                lblNoDataPP.Visible = true;
                                // No hay IBG
                                dgvIBG.Columns.Clear();
                                lblNoDataIBG.Visible = true;
                            }
                        }
                        else
                        {
                            // No tiene OCC
                            dgvOCC.Columns.Clear();
                            lblNoDataOCC.Visible = true;
                        }
                        // Independientemente que tenga o no enlace a OCC puede tener anticipos ligados a la OCS.
                        int[] idCompras = new int[100];
                        idCompras = adocs.up_FacturasAnticipos(idOCS);
                        if (idCompras[0] != -1)
                        {
                            // Si tengo facturas de anticipos asociadas a este OCS
                            lblNoDataAnticipos.Visible = false;
                            adaocs.llenaGrid(dgvAnticiposOCS, idCompras);
                            // Cargar las SP de anticipos desde OCS
                            if (adaocs.up_SP(idCompras[0]) != -1) 
                            {
                                // OJO: Le cargo solo la primera posición porque esta auditoria es desde PC y esta solo tiene asociado un OCS, 
                                // por ende esta OCS tiene asociadas dos facturas, y estas estan cargadas a un solo SP. No tengo mas
                                // casos de uso.
                                adspa.llenaGrid(dgvSPAnticiposOCS, adaocs.up_SP(idCompras[0]));
                            }
                            else
                            {
                                // no hay SP de anticipos generados desde OCS.
                                dgvSPAnticiposOCS.Columns.Clear();
                                lblNoDataSPAnticipos.Visible = true;
                            }

                            // Cargar Pagos de anticipos.
                            adpa.llenaGrid(dgvPagosAnticiposOCS, idCompras); // Cargo y verifico de golpe.

                            if (dgvPagosAnticiposOCS.RowCount == 0)
                            {
                                // No hay ni un solo pago de anticipo.
                                dgvPagosAnticiposOCS.Columns.Clear();
                                lblNoDataPagoAnticipos.Visible = true;
                            }
                        }
                        else
                        {
                            // No tengo ninguna factura de anticipos desde OCS, por tanto no hay sp de anticipos ni pagos de anticipos
                            dgvAnticiposOCS.Columns.Clear();
                            lblNoDataAnticipos.Visible = true;
                            dgvSPAnticiposOCS.Columns.Clear();
                            lblNoDataSPAnticipos.Visible = true;
                            dgvPagosAnticiposOCS.Columns.Clear();
                            lblNoDataPagoAnticipos.Visible = true;
                        }

                        /*
                        * Voy a buscar facturas normales cargadas a esa IG.. siempre y cuando la OCS ya sea una IG.
                        * Para esto hay que validar que el nombre de la OCS cumpla con el formato 'IG-XXX-20XX'
                        * Como estoy buscando desde PC hacia arriba solo voy a tener una sola OCS. OJO.
                        * */

                        if (dgvOCC.RowCount<1 && adocs.validaNombreIG(idOCS))
                        {
                           adfn.llenaGrid2(dgvFacturasNormales, adocs.up_idArticulo(idOCS));
                           if (dgvFacturasNormales.RowCount< 1)
                            {
                                // No hay nada que mostrar
                                dgvFacturasNormales.Columns.Clear();
                                lblNoDataFacturasNormal.Visible = true;
                                dgvSPFacturaNormal.Columns.Clear();
                                lblNoDataSPNormal.Visible = true;
                                dgvPagosFacturaNormal.Columns.Clear();
                                lblNoDataPagosNormal.Visible = true;
                            }
                           else
                           {
                               // Aqui hay que cargar los SP y los pagos asociados a esas facturas.
                               int[] idCompraSPFN = new int[100];
                               idCompraSPFN = adocs.up_spFacturasNormales(adocs.up_idArticulo(idOCS));
                               if (idCompraSPFN[0] != -1)
                                   adspfn.llenaGrid(dgvSPFacturaNormal, idCompraSPFN);
                               else
                               {
                                   // No hay ningún SP asociado a este idArticulo.
                                   dgvSPFacturaNormal.Columns.Clear();
                                   lblNoDataSPNormal.Visible = true;
                               }
                               // Aquí va la carga de los pagos de facturas normales proveedores.
                               int[] idCompraFN = new int[100];
                               for (int i = 0; i < 100; i++)
                                   idCompraFN[i] = 0;
                               for (int i = 0; i < dgvFacturasNormales.RowCount; i++)
                                   idCompraFN[i] = Int32.Parse(dgvFacturasNormales.Rows[i].Cells["idCompra"].Value.ToString());
                               adpfn.llenaGrid(dgvPagosFacturaNormal, idCompraFN);
                               if (dgvPagosFacturaNormal.RowCount<1)
                               {
                                   // Ningún pago que mostrar.
                                   dgvPagosFacturaNormal.Columns.Clear();
                                   lblNoDataPagosNormal.Visible = true;
                               }
                           }
                        }

                        if (dgvOCC.RowCount < 1 && !adocs.validaNombreIG(idOCS))
                        {
                            // Es un nombre de IG NO Válido. Por tanto:
                            // No hay Facturas Normales, ni SP, ni pagos.
                            dgvFacturasNormales.Columns.Clear();
                            lblNoDataFacturasNormal.Visible = true;
                            dgvSPFacturaNormal.Columns.Clear();
                            lblNoDataSPNormal.Visible = true;
                            dgvPagosFacturaNormal.Columns.Clear();
                            lblNoDataPagosNormal.Visible = true;
                        }

                        if (PPcargado == 0)
                        {
                            // No se cargó el PP aún, el último intento fue cargarlo después de mostrar el OCC
                            // Esto quiere decir que si el registro no tiene OCC se realizará la carga del PP desde aquí, partiendo desde la OCS
                            idPP=adocs.up_PP(idOCS);
                            if (idPP != -1)
                            {
                                adpp.llenaGrid(dgvPP, idPP);
                                // Verificar si tengo IBG.
                                idIBG = adpp.up_IBG(idPP);
                                if (idIBG != -1)
                                {
                                    adibg.llenaGrid(dgvIBG, idIBG);
                                }
                                else
                                {
                                    // No hay IBG
                                    dgvIBG.Columns.Clear();
                                    lblNoDataIBG.Visible = true;
                                }
                            }
                            else
                            {
                                // No hay PP
                                dgvPP.Columns.Clear();
                                lblNoDataPP.Visible = true;
                                // No hay IBG
                                dgvIBG.Columns.Clear();
                                lblNoDataIBG.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        // no tiene asociado OCS
                        dgvOCS.Columns.Clear();
                        lblNoDataOCS.Visible = true;
                        // y por ende tampoco tiene OCC
                        dgvOCC.Columns.Clear();
                        lblNoDataOCC.Visible = true;
                        // Y por ende no hay anticipos desde OCS ni sp de anticipos ni pagos de anticipos
                        dgvAnticiposOCS.Columns.Clear();
                        lblNoDataAnticipos.Visible = true;
                        dgvSPAnticiposOCS.Columns.Clear();
                        lblNoDataSPAnticipos.Visible = true;
                        dgvPagosAnticiposOCS.Columns.Clear();
                        lblNoDataPagoAnticipos.Visible = true;
                        // No hay Facturas Normales, ni SP, ni pagos.
                        dgvFacturasNormales.Columns.Clear();
                        lblNoDataFacturasNormal.Visible = true;
                        dgvSPFacturaNormal.Columns.Clear();
                        lblNoDataSPNormal.Visible = true;
                        dgvPagosFacturaNormal.Columns.Clear();
                        lblNoDataPagosNormal.Visible = true;
                        // No hay PP
                        dgvPP.Columns.Clear();
                        lblNoDataPP.Visible = true;
                        // No hay IBG
                        dgvIBG.Columns.Clear();
                        lblNoDataIBG.Visible = true;
                    }
                }
                resaltaRb(rbPC);
                flagRadioSeleccionado = 1;
            }
            else
            {
                resaltaRb(rbPC);
            }
            estilizar();
        }

        private void rbOCS_CheckedChanged(object sender, EventArgs e)
        {
            int idOCS = 0, error = 0, idPC = 0, idOCC = 0, idPP = 0, idIBG = 0, PPcargado = 0;
            //  int idRegistro = 0;
            if (rbOCS.Checked)
            {
                resaltaRb(rbOCS);
                flagRadioSeleccionado = 2;
                // Hay que ver si tengo datos seleccionados en este dgv.
                try
                {
                    idOCS = Int32.Parse(dgvOCS.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (Exception)
                {
                    error = 1;
                }
                if (error == 1)
                    handlerLblNoData(1); // no hay nada seleccionado ni que mostrar. - Muestro todos los dgv en blanco sin datos.
                else
                {
                    adocs.llenaGrid(dgvOCS, idOCS); // Muestro solo la OCS Seleccionada.
                    // Inicia enlace hacia abajo
                    //idPC=adocs.down_pc(idOCS);
                    try
                    {
                        idPC = Int32.Parse(dgvOCS.SelectedRows[0].Cells["pcOrigen"].Value.ToString());
                    }
                    catch
                    {
                        idPC = -1;
                    }

                    if (idPC < 1)
                    {
                        // No hay PC asociado a la OCS.
                        dgvPC.Columns.Clear();
                        lblNoDataPC.Visible = true;
                    }
                    else
                    {
                        adpc.llenaGrid(dgvPC, idPC);
                    }
                    // Inicia enlace hacia arriba:
                    // Verificar si tiene una OCC asociada a este OCS.
                    // Voy a buscar si tiene OCC.
                    idOCC = adocs.up_OCC(idOCS);
                    if (idOCC != -1)
                    {
                        adocc.llenaGrid(dgvOCC, idOCC); // Mostrar OCC
                        if (adocc.validaNombreIG(idOCC))
                        {
                            // Voy a buscar facturas normales, con sus SP y sus pagos desde una orden de compra consolidada, en caso de que la tuviese:
                            adfn.llenaGrid2(dgvFacturasNormales, adocc.up_idArticulo(idOCC));
                            if (dgvFacturasNormales.RowCount < 1)
                            {
                                // No hay nada que mostrar
                                dgvFacturasNormales.Columns.Clear();
                                lblNoDataFacturasNormal.Visible = true;
                                dgvSPFacturaNormal.Columns.Clear();
                                lblNoDataSPNormal.Visible = true;
                                dgvPagosFacturaNormal.Columns.Clear();
                                lblNoDataPagosNormal.Visible = true;
                            }
                            else
                            {
                                // Aqui hay que cargar los SP y los pagos asociados a esas facturas en caso de que existan.
                                int[] idCompraSPFN = new int[100];
                                idCompraSPFN = adocc.up_spFacturasNormales(adocc.up_idArticulo(idOCC));
                                if (idCompraSPFN[0] != -1)
                                    adspfn.llenaGrid(dgvSPFacturaNormal, idCompraSPFN);
                                else
                                {
                                    // No hay ningún SP asociado a este idArticulo.
                                    dgvSPFacturaNormal.Columns.Clear();
                                    lblNoDataSPNormal.Visible = true;
                                }
                                // Aquí va la carga de los pagos de facturas normales proveedores.
                                int[] idCompraFN = new int[100];
                                for (int i = 0; i < 100; i++)
                                    idCompraFN[i] = 0;
                                for (int i = 0; i < dgvFacturasNormales.RowCount; i++)
                                    idCompraFN[i] = Int32.Parse(dgvFacturasNormales.Rows[i].Cells["idCompra"].Value.ToString()); // Acá me tira error.. seguramente no estoy recuperando la data correctamente.
                                adpfn.llenaGrid(dgvPagosFacturaNormal, idCompraFN);
                                if (dgvPagosFacturaNormal.RowCount < 1)
                                {
                                    // Ningún pago que mostrar.
                                    dgvPagosFacturaNormal.Columns.Clear();
                                    lblNoDataPagosNormal.Visible = true;
                                }
                            }
                        }

                        // Buscar si tengo PP creado desde OCC.
                        idPP = adocc.up_PP(idOCC);
                        if (idPP != -1)
                        {
                            adpp.llenaGrid(dgvPP, idPP); // cargar el PP
                            PPcargado = 1;
                            // Buscar si tengo IBG
                            idIBG = adpp.up_IBG(idPP);
                            if (idIBG != -1)
                                adibg.llenaGrid(dgvIBG, idIBG);
                            else
                            {
                                // No hay IBG
                                dgvIBG.Columns.Clear();
                                lblNoDataIBG.Visible = true;
                            }
                        }
                        else
                        {
                            // No hay PP
                            dgvPP.Columns.Clear();
                            lblNoDataPP.Visible = true;
                            // No hay IBG
                            dgvIBG.Columns.Clear();
                            lblNoDataIBG.Visible = true;
                        }
                    }
                    else
                    {
                        // No tiene OCC
                        dgvOCC.Columns.Clear();
                        lblNoDataOCC.Visible = true;
                    }
                    // Independientemente que tenga o no enlace a OCC puede tener anticipos ligados a la OCS.
                    // Como estoy en el rbOCS tengo seleccionado un solo DataGridViewRow
                    // string nroOCS = dgvOCS.SelectedRows[0].Cells[5].Value.ToString(); // Extraigo el numero de la OCS. Ya no trabajo con el nroOCS.
                    int[] idCompras = new int[100];
                    idCompras = adocs.up_FacturasAnticipos(idOCS);
                    if (idCompras[0] != -1)
                    {
                        // Si tengo facturas de anticipos asociadas a este OCS
                        lblNoDataAnticipos.Visible = false;
                        //adaocs.llenaGrid(dgvAnticiposOCS, idCompras); // Esto no rescataba todos los anticipos.
                        adaocs.llenaGrid(dgvAnticiposOCS, idOCS);
                        // Cargar las SP de anticipos desde OCS
                        int maxPos = 0;
                        for (int i = 0; i < 100; i++)
                        {
                            if (idCompras[i + 1] == 0)
                            {
                                maxPos = i;
                                break;
                            }
                        }

                        if (adaocs.up_SP(idCompras[maxPos]) != -1)
                        {
                            // OJO: Le cargo solo la ultima posición del array que sea distinta de cero, porque esta auditoria es desde PC y esta solo tiene asociado un OCS, 
                            // por ende esta OCS tiene asociadas dos facturas, y estas estan cargadas a un solo SP. No tengo mas
                            // casos de uso.
                            adspa.llenaGrid(dgvSPAnticiposOCS, adaocs.up_SP(idCompras[maxPos]));
                        }
                        else
                        {
                            // no hay SP de anticipos generados desde OCS.
                            dgvSPAnticiposOCS.Columns.Clear();
                            lblNoDataSPAnticipos.Visible = true;
                        }

                        // Cargar Pagos de anticipos.
                        adpa.llenaGrid(dgvPagosAnticiposOCS, idCompras); // Cargo y verifico de golpe.

                        if (dgvPagosAnticiposOCS.RowCount == 0)
                        {
                            // No hay ni un solo pago de anticipo.
                            dgvPagosAnticiposOCS.Columns.Clear();
                            lblNoDataPagoAnticipos.Visible = true;
                        }
                    }
                    else
                    {
                        /*
                         * Si llego hasta acá significa que no encontró ningún anticipo asociado con el número de OCS, sin embargo tengo una segunda opción (y mas fiable)
                         * al buscar en base al idCompra de la OCS.
                        */
                        int idCompraOCS = Int32.Parse(dgvOCS.SelectedRows[0].Cells[0].Value.ToString());
                        idCompras = new int[100];
                        idCompras = adocs.up_FacturasAnticipos(idCompraOCS);
                        if (idCompras[0] != -1)
                        { 
                            // Encontró el anticipo.
                            // Si tengo facturas de anticipos asociadas a este OCS
                            lblNoDataAnticipos.Visible = false;
                            adaocs.llenaGrid(dgvAnticiposOCS, idCompras);
                            // Cargar las SP de anticipos desde OCS
                            if (adaocs.up_SP(idCompras[0]) != -1)
                            {
                                // OJO: Le cargo solo la primera posición porque esta auditoria es desde PC y esta solo tiene asociado un OCS, 
                                // por ende esta OCS tiene asociadas dos facturas, y estas estan cargadas a un solo SP. No tengo mas
                                // casos de uso.
                                adspa.llenaGrid(dgvSPAnticiposOCS, adaocs.up_SP(idCompras[0]));
                            }
                            else
                            {
                                // no hay SP de anticipos generados desde OCS.
                                dgvSPAnticiposOCS.Columns.Clear();
                                lblNoDataSPAnticipos.Visible = true;
                            }
                            // Cargar Pagos de anticipos.
                            // Verificar si el conjunto de OCS tiene asociados pagos.



                            adpa.llenaGrid(dgvPagosAnticiposOCS, idCompras); // Cargo y verifico de golpe.
                            if (dgvPagosAnticiposOCS.RowCount == 0)
                            {
                                // No hay ni un solo pago de anticipo.
                                dgvPagosAnticiposOCS.Columns.Clear();
                                lblNoDataPagoAnticipos.Visible = true;
                            }
                        }
                        else
                        {
                            // Descartados los 2 métodos - uno por el nombre de la OCS y el otro por el idCompra de la OCS.
                            // No tengo ninguna factura de anticipos desde OCS, por tanto no hay sp de anticipos ni pagos de anticipos
                            dgvAnticiposOCS.Columns.Clear();
                            lblNoDataAnticipos.Visible = true;
                            dgvSPAnticiposOCS.Columns.Clear();
                            lblNoDataSPAnticipos.Visible = true;
                            dgvPagosAnticiposOCS.Columns.Clear();
                            lblNoDataPagoAnticipos.Visible = true;
                        }
                    }

                    /*
                    * Voy a buscar facturas normales cargadas a esa IG.. siempre y cuando la OCS ya sea una IG.
                    * Para esto hay que validar que el nombre de la OCS cumpla con el formato 'IG-XXX-20XX'
                    * Como estoy buscando desde PC hacia arriba solo voy a tener una sola OCS. OJO.
                    * */

                    if (dgvOCC.RowCount < 1 && adocs.validaNombreIG(idOCS))
                    {
                        adfn.llenaGrid2(dgvFacturasNormales, adocs.up_idArticulo(idOCS));
                        if (dgvFacturasNormales.RowCount < 1)
                        {
                            // No hay nada que mostrar
                            dgvFacturasNormales.Columns.Clear();
                            lblNoDataFacturasNormal.Visible = true;
                            dgvSPFacturaNormal.Columns.Clear();
                            lblNoDataSPNormal.Visible = true;
                            dgvPagosFacturaNormal.Columns.Clear();
                            lblNoDataPagosNormal.Visible = true;
                        }
                        else
                        {
                            // Aqui hay que cargar los SP y los pagos asociados a esas facturas.
                            int[] idCompraSPFN = new int[100];
                            idCompraSPFN = adocs.up_spFacturasNormales(adocs.up_idArticulo(idOCS));
                            if (idCompraSPFN[0] != -1)
                                adspfn.llenaGrid(dgvSPFacturaNormal, idCompraSPFN);
                            else
                            {
                                // No hay ningún SP asociado a este idArticulo.
                                dgvSPFacturaNormal.Columns.Clear();
                                lblNoDataSPNormal.Visible = true;
                            }
                            // Aquí va la carga de los pagos de facturas normales proveedores.
                            int[] idCompraFN = new int[100];
                            for (int i = 0; i < 100; i++)
                                idCompraFN[i] = 0;
                            for (int i = 0; i < dgvFacturasNormales.RowCount; i++)
                                idCompraFN[i] = Int32.Parse(dgvFacturasNormales.Rows[i].Cells["idCompra"].Value.ToString());
                            adpfn.llenaGrid(dgvPagosFacturaNormal, idCompraFN);
                            if (dgvPagosFacturaNormal.RowCount < 1)
                            {
                                // Ningún pago que mostrar.
                                dgvPagosFacturaNormal.Columns.Clear();
                                lblNoDataPagosNormal.Visible = true;
                            }
                        }
                    }

                    if (dgvOCC.RowCount < 1 && !adocs.validaNombreIG(idOCS))
                    {
                        // Es un nombre de IG NO Válido. Por tanto:
                        // No hay Facturas Normales, ni SP, ni pagos.
                        dgvFacturasNormales.Columns.Clear();
                        lblNoDataFacturasNormal.Visible = true;
                        dgvSPFacturaNormal.Columns.Clear();
                        lblNoDataSPNormal.Visible = true;
                        dgvPagosFacturaNormal.Columns.Clear();
                        lblNoDataPagosNormal.Visible = true;
                    }

                    if (PPcargado == 0)
                    {
                        // No se cargó el PP aún, el último intento fue cargarlo después de mostrar el OCC
                        // Esto quiere decir que si el registro no tiene OCC se realizará la carga del PP desde aquí, partiendo desde la OCS
                        idPP=adocs.up_PP(idOCS);
                        if (idPP != -1)
                        {
                            adpp.llenaGrid(dgvPP, idPP);
                            // Verificar si tengo IBG.
                            idIBG = adpp.up_IBG(idPP);
                            if (idIBG != -1)
                            {
                                adibg.llenaGrid(dgvIBG, idIBG);
                            }
                            else
                            {
                                // No hay IBG
                                dgvIBG.Columns.Clear();
                                lblNoDataIBG.Visible = true;
                            }
                        }
                        else
                        {
                            // No hay PP
                            dgvPP.Columns.Clear();
                            lblNoDataPP.Visible = true;
                            // No hay IBG
                            dgvIBG.Columns.Clear();
                            lblNoDataIBG.Visible = true;
                        }
                    }
                }
            }
            else
            {
                resaltaRb(rbOCS);
            }
            estilizar();
        }

        private void rbOCC_CheckedChanged(object sender, EventArgs e)
        {
            int error = 0, idPC = 0, idOCC = 0, idPP=0, idIBG=0;
            // int PPcargado=0, idRegistro=0 ;
            if (rbOCC.Checked)
            {
                resaltaRb(rbOCC);
                flagRadioSeleccionado = 3;
                // Hay que ver si tengo datos seleccionados en este dgv.
                try
                {
                    idOCC = Int32.Parse(dgvOCC.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (Exception)
                {
                    error = 1;
                }
                if (error == 1)
                    handlerLblNoData(1); // no hay nada seleccionado ni que mostrar. - Muestro todos los dgv en blanco sin datos.
                else
                {
                    adocc.llenaGrid(dgvOCC, idOCC); // Muestro solo la OCC Seleccionada.
                    // Inicia enlace hacia abajo
                    // Busco de que OCS proviene esta OCC.
                    int[] idCompraOCS = new int[100];
                    idCompraOCS = adocc.down_OCS(idOCC);
                    if (idCompraOCS[0] == -1)
                    {
                        // NO hay ninguna OCS asociada a este OCC
                        dgvOCS.Columns.Clear();
                        lblNoDataOCS.Visible = true;
                        // Por ende tampoco voy a poder llegar a ningún PC
                        dgvPC.Columns.Clear();
                        lblNoDataPC.Visible = true;
                    }
                    else
                    {
                        // Mostrar las OCS desde OCC. Osea que no se tomarán en cuenta las OCS que se encuentran en estado: "Por Absorber"
                        adocs.llenaGridDesdeOCC(dgvOCS, idCompraOCS,idOCC);
                        // Buscar si tengo PC de esas OCS.
                        // mandar un for para rescatar las asociaciones desde el dgv columna "pcOrigen".. es mas facil.
                        int [] idPlanC = new int [100];
                        int posVector=0;
                        idPC=0;
                        for (int i = 0; i < 100; i++)
                            idPlanC[i] = 0;
                        idPlanC[0] = -1; // Si solo tengo un OCS y este no tiene PC esto va a hacer que me devuelva el array con el error.
                        for (int i = 0; i < dgvOCS.RowCount; i++)
                        {
                            try
                            {
                                idPC=Int32.Parse(dgvOCS.Rows[i].Cells["pcOrigen"].Value.ToString());
                            }
                            catch
                            {
                                idPC=-1;
                            }
                            if (idPC!=-1)
                            {
                                idPlanC[posVector++]=idPC;
                            }
                        }
                        if (idPlanC[0]!=-1)
                        {
                            adpc.llenaGrid(dgvPC, idPlanC);
                        }
                        else
                        {
                            // No hay planes de compra asociados.
                            dgvPC.Columns.Clear();
                            lblNoDataPC.Visible = true;
                        }
                    }
                    // Inicia enlace hacia arriba.
                    // Busqueda de anticipos desde OCS.
                    if (dgvOCS.RowCount > 0)
                    {
                        // Hay data de OCS
                        if (dgvOCS.RowCount == 1)
                        {
                            // Solo hay una OCS.
                            // para que una ocs pida un anticipo no es necesario que tenga un nombre válido.
                            int[] idComprasAnticipos = new int[100];
                            idComprasAnticipos = adocs.up_FacturasAnticipos(idCompraOCS[0]); // Aquí no me devuelve lo que debería.
                            if (idComprasAnticipos[0] != -1)
                            {
                                // Si tengo facturas de anticipos asociadas a este OCS
                                lblNoDataAnticipos.Visible = false;
                                adaocs.llenaGrid(dgvAnticiposOCS, idComprasAnticipos);
                                // Cargar las SP de anticipos desde OCS
                                if (adaocs.up_SP(idComprasAnticipos[0]) != -1)
                                {
                                    // OJO: Le cargo solo la primera posición porque esta auditoria es desde PC y esta solo tiene asociado un OCS, 
                                    // por ende esta OCS tiene asociadas dos facturas, y estas estan cargadas a un solo SP. No tengo mas
                                    // casos de uso.
                                    adspa.llenaGrid(dgvSPAnticiposOCS, adaocs.up_SP(idComprasAnticipos[0]));
                                }
                                else
                                {
                                    // no hay SP de anticipos generados desde OCS.
                                    dgvSPAnticiposOCS.Columns.Clear();
                                    lblNoDataSPAnticipos.Visible = true;
                                }

                                // Cargar Pagos de anticipos.
                                adpa.llenaGrid(dgvPagosAnticiposOCS, idComprasAnticipos); // Cargo y verifico de golpe.
                                if (dgvPagosAnticiposOCS.RowCount == 0)
                                {
                                    // No hay ni un solo pago de anticipo.
                                    dgvPagosAnticiposOCS.Columns.Clear();
                                    lblNoDataPagoAnticipos.Visible = true;
                                }
                            }
                            else
                            {
                                // No tengo ninguna factura de anticipos desde OCS, por tanto no hay sp de anticipos ni pagos de anticipos
                                dgvAnticiposOCS.Columns.Clear();
                                lblNoDataAnticipos.Visible = true;
                                dgvSPAnticiposOCS.Columns.Clear();
                                lblNoDataSPAnticipos.Visible = true;
                                dgvPagosAnticiposOCS.Columns.Clear();
                                lblNoDataPagoAnticipos.Visible = true;
                            }
                        }
                        if (dgvOCS.RowCount > 1)
                        {
                            // Tengo un conjunto de OCS.. Aquí me toca recorrer el dgvOCS para ver si alguna de estas tiene generado anticipos desde el carro.
                            // Recorrer el dgvOCS para obtener un array que tenga el nombre de las OCS cuyo campo pedido no se encuentre en blanco.
                            int[] idCompraOCSArray = new int[100]; // Aquí voy a almacenar todos los idcompraOCS que se estén mostrando en el dgvOCS
                            for (int i = 0; i < dgvOCS.RowCount; i++)
                            {
                                idCompraOCSArray[i] = Int32.Parse(dgvOCS.Rows[i].Cells[0].Value.ToString());
                            }
                            int[] idFactCompraAnticipos = new int[100];
                            idFactCompraAnticipos = adocs.up_FacturasAnticipos(idCompraOCSArray);
                            if (idFactCompraAnticipos[0] != -1)
                            {
                                adaocs.llenaGrid(dgvAnticiposOCS, idFactCompraAnticipos);
                                // de aquí tengo que cargar las SP de los anticipos.
                                int[] idCompraSPAnticipos = new int[100]; // En este vector se almacenarán todos los SP asociados a las OCS.
                                idCompraSPAnticipos = adocc.up_spFacturasAnticipos(idOCC);
                                if (idCompraSPAnticipos[0] != -1)
                                {
                                    adspa.llenaGrid(dgvSPAnticiposOCS, idCompraSPAnticipos);
                                }
                                else
                                {
                                    dgvSPAnticiposOCS.Columns.Clear();
                                    lblNoDataSPAnticipos.Visible = true;
                                }

                                // de aquí tengo que cargar los pagos de los anticipos.
                                adpa.llenaGrid(dgvPagosAnticiposOCS, idFactCompraAnticipos);

                            }
                            else
                            {
                                // No encontró ninguna factura de anticipo asociada al conjunto de OCS.
                                dgvAnticiposOCS.Columns.Clear();
                                lblNoDataAnticipos.Visible = true;
                                dgvSPAnticiposOCS.Columns.Clear();
                                lblNoDataSPAnticipos.Visible = true;
                                dgvPagosAnticiposOCS.Columns.Clear();
                                lblNoDataPagoAnticipos.Visible = true;
                            }
                        }
                    }
                    // Búsqueda de facturas normales.
                    if (adocc.validaNombreIG(idOCC))
                    {
                        // Voy a buscar facturas normales, con sus SP y sus pagos desde una orden de compra consolidada, en caso de que la tuviese:
                        adfn.llenaGrid2(dgvFacturasNormales, adocc.up_idArticulo(idOCC));
                        if (dgvFacturasNormales.RowCount < 1)
                        {
                            // No hay nada que mostrar
                            dgvFacturasNormales.Columns.Clear();
                            lblNoDataFacturasNormal.Visible = true;
                            dgvSPFacturaNormal.Columns.Clear();
                            lblNoDataSPNormal.Visible = true;
                            dgvPagosFacturaNormal.Columns.Clear();
                            lblNoDataPagosNormal.Visible = true;
                        }
                        else
                        {
                            // Aqui hay que cargar los SP y los pagos asociados a esas facturas en caso de que existan.
                            int[] idCompraSPFN = new int[100];
                            idCompraSPFN = adocc.up_spFacturasNormales(adocc.up_idArticulo(idOCC));
                            if (idCompraSPFN[0] != -1)
                                adspfn.llenaGrid(dgvSPFacturaNormal, idCompraSPFN);
                            else
                            {
                                // No hay ningún SP asociado a este idArticulo.
                                dgvSPFacturaNormal.Columns.Clear();
                                lblNoDataSPNormal.Visible = true;
                            }
                            // Aquí va la carga de los pagos de facturas normales proveedores.
                            int[] idCompraFN = new int[100];
                            for (int i = 0; i < 100; i++)
                                idCompraFN[i] = 0;
                            for (int i = 0; i < dgvFacturasNormales.RowCount; i++)
                                idCompraFN[i] = Int32.Parse(dgvFacturasNormales.Rows[i].Cells["idCompra"].Value.ToString()); // Acá me tira error.. seguramente no estoy recuperando la data correctamente.
                            adpfn.llenaGrid(dgvPagosFacturaNormal, idCompraFN);
                            if (dgvPagosFacturaNormal.RowCount < 1)
                            {
                                // Ningún pago que mostrar.
                                dgvPagosFacturaNormal.Columns.Clear();
                                lblNoDataPagosNormal.Visible = true;
                            }
                        }
                    }
                    // Búsqueda de Pedido proveedor.
                    idPP = 0;
                    idPP = adocc.up_PP(Int32.Parse(dgvOCC.Rows[0].Cells[0].Value.ToString()));
                    if (idPP > 0)
                        adpp.llenaGrid(dgvPP, idPP);
                    else
                    {
                        dgvPP.Columns.Clear();
                        lblNoDataPP.Visible = true;
                    }
                    // Búsqueda de IBG
                    idIBG = 0;
                    idIBG = adpp.up_IBG(idPP);
                    if (idIBG > 0)
                        adibg.llenaGrid(dgvIBG, idIBG);
                    else
                    {
                        dgvIBG.Columns.Clear();
                        lblNoDataIBG.Visible = true;
                    }
                }
            }
            else
            {
                resaltaRb(rbOCC);
            }
            estilizar();
        }

        private void rbAnticiposOCS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAnticiposOCS.Checked)
            {
                resaltaRb(rbAnticiposOCS);
                flagRadioSeleccionado = 4;
            }
        }
        
        private void rbSPAnticiposOCS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSPAnticiposOCS.Checked)
            {
                resaltaRb(rbSPAnticiposOCS);
                flagRadioSeleccionado = 5;
            }
        }

        private void rbPagosAnticiposOCS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPagosAnticiposOCS.Checked)
            {
                resaltaRb(rbPagosAnticiposOCS);
                flagRadioSeleccionado = 6;
            }
        }

        private void rbFacturasNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFacturasNormal.Checked)
            {
                resaltaRb(rbFacturasNormal);
                flagRadioSeleccionado = 7;
            }
        }

        private void rbSpPagosNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpPagosNormal.Checked)
            {
                resaltaRb(rbSpPagosNormal);
                flagRadioSeleccionado = 8;
            }
        }
        
        private void rbPagosNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPagosNormal.Checked)
            {
                resaltaRb(rbPagosNormal);
                flagRadioSeleccionado = 9;
            }
        }

        private void rbPP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPP.Checked)
            {
                resaltaRb(rbPP);
                flagRadioSeleccionado = 10;
            }
        }

        private void rbIBG_CheckedChanged(object sender, EventArgs e)
        {
            int idIBG=0, error=0;
            if (rbIBG.Checked)
            {
                resaltaRb(rbIBG);
                flagRadioSeleccionado = 11;
                // Hay que ver si tengo datos seleccionados en este dgv.
                try
                {
                    idIBG = Int32.Parse(dgvIBG.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (Exception)
                {
                    error = 1;
                }
                if (error == 1)
                    handlerLblNoData(1); // no hay nada seleccionado ni que mostrar. - Muestro todos los dgv en blanco sin datos.
                else
                {
                    adibg.llenaGrid(dgvIBG, idIBG); // Cargar solo el IBG seleccionado.
                }
            }
            else
                resaltaRb(rbIBG);
            estilizar();
        }

        private void cargarData() 
        {
            // Inicio la carga con la capa de Datos.
            //Planes de Compra:
            adpc.llenaGrid(dgvPC);
            adpc.estilo(dgvPC);
            
            //Ordenes de Compra Simples.
            adocs.llenaGrid(dgvOCS);
            adocs.estilo(dgvOCS);

            // Ordenes de compra consolidadas.
            adocc.llenaGrid(dgvOCC);
            adocc.estilo(dgvOCC);

            /*
             * Suprimo la carga de los componentes a continuación debido a que nunca los usan y retrasan la carga inicial del módulo.
             * Además el seguimiento desde cualquiera de estos no está habilitado.
             * 
             * 
            // Anticipos (facturas) desde orden de compra simple
            adaocs.llenaGrid(dgvAnticiposOCS);
            adaocs.estilo(dgvAnticiposOCS);
                    
            // SP de anticipos desde OCS
            adspa.llenaGrid(dgvSPAnticiposOCS);
            adspa.estilo(dgvSPAnticiposOCS);
            
            // Pagos de anticipos desde OCS
            adpa.llenaGrid(dgvPagosAnticiposOCS);
            adpa.estilo(dgvPagosAnticiposOCS);

            // Facturas normales proveedores.
            adfn.llenaGrid(dgvFacturasNormales);
            adfn.estilo(dgvFacturasNormales);
            
            // SP de facturas normales
            adspfn.llenaGrid(dgvSPFacturaNormal);
            adspfn.estilo(dgvSPFacturaNormal);

            // Pagos de Facturas Normales
            adpfn.llenaGrid(dgvPagosFacturaNormal);
            adpfn.estilo(dgvPagosFacturaNormal);
            */

            // Pedidos Proveedor
            adpp.llenaGrid(dgvPP);
            adpp.estilo(dgvPP);

            // Ingreso a bodega
            adibg.llenaGrid(dgvIBG);
            adibg.estilo(dgvIBG);
           
        }

        private void Auditoria_Load(object sender, EventArgs e)
        {
            cargarData();
        }
        
        private void cmbTipoVista_SelectedIndexChanged(object sender, EventArgs e)
        {
            // La opción de ampliar cuadrículas solo está disponible si la resolución es >= 1440x900
            if (resolucion.Width >= 1440 && resolucion.Height >= 900)
            {
                ampliarDGV(0);
                chkAbrirDGV.Enabled = true;
                chkAbrirDGV.Checked = false;
            }

            btnEditarObservación.Enabled = true;
            errorProvider1.Dispose();
            desactivaColoresLabel();

            int opcion=Int32.Parse(cmbTipoVista.Text);
            switch (opcion)
            {
                case 1:
                    {
                        lbl1.ForeColor = Color.Red;
                    }break;
                case 2:
                    {
                        lbl2.ForeColor = Color.Red;
                    } break;
                case 3:
                    {
                        lbl3.ForeColor = Color.Red;
                    } break;
                case 4:
                    {
                        lbl4.ForeColor = Color.Red;
                    } break;
                case 5:
                    {
                        lbl5.ForeColor = Color.Red;
                    } break;
                case 6:
                    {
                        lbl6.ForeColor = Color.Red;
                    } break;
                case 7:
                    {
                        lbl7.ForeColor = Color.Red;
                    } break;
                case 8:
                    {
                        lbl8.ForeColor = Color.Red;
                    } break;
                case 9:
                    {
                        lbl9.ForeColor = Color.Red;
                    } break;
                case 10:
                    {
                        lbl10.ForeColor = Color.Red;
                    } break;
                case 11:
                    {
                        lbl11.ForeColor = Color.Red;
                    } break;
            }
        }

        private void btnGuardaObservacion_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            try
            {
                Int32.Parse(cmbTipoVista.Text);
            }
            catch (Exception)
            {
                errorProvider1.SetError(cmbTipoVista, "Debe seleccionar en donde se almacenará el comentario");
                return;
            }
            // Si llega hasta acá la validación es correcta y hay que almacenar el comentario.
            guardaComentario(Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
        }

        private void guardaComentario(int nroDGV)
        {
            // Evalua con que dgv está trabajando y de acuerdo a esto llama a la clase de la capa datos correspondiente.
            int flagGuarda = 0;
            switch (nroDGV)
            {
                case 1:
                    {
                        if (adpc.ingresarComentario(dgvPC.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    }break;
                case 2:
                    {
                        if (adocs.ingresarComentario(dgvOCS.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    }break;

                case 3:
                    {
                        if (adocc.ingresarComentario(dgvOCC.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 4:
                    {
                        if (adaocs.ingresarComentario(dgvAnticiposOCS.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 5:
                    {
                        if (adspa.ingresarComentario(dgvSPAnticiposOCS.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 6:
                    {
                        if (adpa.ingresarComentario(dgvPagosAnticiposOCS.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 7:
                    {
                        if (adfn.ingresarComentario(dgvFacturasNormales.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 8:
                    {
                        if (adspfn.ingresarComentario(dgvSPFacturaNormal.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 9:
                    {
                        if (adpfn.ingresarComentario(dgvPagosFacturaNormal.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 10:
                    {
                        if (adpp.ingresarComentario(dgvPP.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;

                case 11:
                    {
                        if (adibg.ingresarComentario(dgvIBG.SelectedRows[0], nroDGV, txtNota.Text) != 0)
                            flagGuarda++; // Guardado del comentario tuvo error
                    } break;
            }
            if (flagGuarda == 0)
            {
                btnGuardaObservacion.Enabled = false;
                btnDeshacer.Enabled = false;
                btnEditarObservación.Enabled = true;
                txtNota.Enabled = false;
                /*
                 * No hubo error, el comentario fue guardado exitosamente. 
                 * Hay que encontrar una manera de que se muestre el campo actualizado en el dgv dependiendo del modo en el que se esté mostrando la data
                 * Lamentablemente mientras tanto hay que refrescar todo el dgv
                 * */
                btnRefrescar.PerformClick();
            }
        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void Auditoria_Shown(object sender, EventArgs e)
        {
            /*
             * Una vez que se termina de cargar toda la data en los dgv y que el formulario acaba de ser mostrado se procede
             * a formatear y pintar celdas
             * */
            estilizar();
        }

        private void estilizar()
        {
            // Pinta todos los dgv que tengan datos: Esto se usa al final de los eventos de auditoría individual de cada dgv.
            if (dgvPC.RowCount>0)
            {
                adpc.estilo(dgvPC);
                adpc.pintarCeldas(dgvPC);
            }

            if (dgvOCS.RowCount > 0)
            {
                adocs.estilo(dgvOCS);
                adocs.pintarCeldas(dgvOCS);
            }

            if (dgvOCC.RowCount > 0)
            {
                adocc.estilo(dgvOCC);
                adocc.pintarCeldas(dgvOCC);
            }

            if (dgvAnticiposOCS.RowCount > 0)
            {
                adaocs.estilo(dgvAnticiposOCS);
                adaocs.pintarCeldas(dgvAnticiposOCS);
            }

            if (dgvSPAnticiposOCS.RowCount > 0)
            {
                adspa.estilo(dgvSPAnticiposOCS);
                adspa.pintarCeldas(dgvSPAnticiposOCS);
            }

            if (dgvPagosAnticiposOCS.RowCount > 0)
            {
                adpa.estilo(dgvPagosAnticiposOCS);
                adpa.pintarCeldas(dgvPagosAnticiposOCS);
            }

            if (dgvFacturasNormales.RowCount > 0)
            {
                adfn.estilo(dgvFacturasNormales);
                adfn.pintarCeldas(dgvFacturasNormales);
            }

            if (dgvSPFacturaNormal.RowCount > 0)
            {
                adspfn.estilo(dgvSPFacturaNormal);
                adspfn.pintarCeldas(dgvSPFacturaNormal);
            }

            if (dgvPagosFacturaNormal.RowCount > 0)
            {
                adpfn.estilo(dgvPagosFacturaNormal);
                adpfn.pintarCeldas(dgvPagosFacturaNormal);
            }

            if (dgvPP.RowCount > 0)
            {
                adpp.estilo(dgvPP);
                adpp.pintarCeldas(dgvPP);
            }

            if (dgvIBG.RowCount > 0)
            {
                adibg.estilo(dgvIBG);
                adibg.pintarCeldas(dgvIBG);
            }
        }

        private void dgvPC_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // cada vez que se da clic en una columna del dgv hay que volver a recalcular los tiempos.
            adpc.estilo(dgvPC);
            adpc.pintarCeldas(dgvPC);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // Este método recarga toda la data.. equivale a salir y volver a entrar.
            handlerLblNoData(0);  // Borra todos los dgv
            
            btnRefrescar.Enabled = false;
            borrarData();
            cargarData();
            
            btnRefrescar.Enabled = true;
            // de aquí para abajo hay que llamar al método pintarCeldas de todas las clases.. OJO
            estilizar();
            // Desactivar todos los radiobutton
            rbPC.Checked = false;
            rbOCS.Checked = false;
            rbOCC.Checked = false;
            rbAnticiposOCS.Checked = false;
            rbSPAnticiposOCS.Checked = false;
            rbPagosAnticiposOCS.Checked = false;
            rbFacturasNormal.Checked = false;
            rbSpPagosNormal.Checked = false;
            rbPagosNormal.Checked = false;
            rbPP.Checked = false;
            rbIBG.Checked = false;
            flagRadioSeleccionado = 0;

            this.Cursor = Cursors.Default;
        }

        private void borrarData()
        {
            dgvAnticiposOCS.Columns.Clear();
            dgvFacturasNormales.Columns.Clear();
            dgvIBG.Columns.Clear();
            dgvOCC.Columns.Clear();
            dgvOCS.Columns.Clear();
            dgvPagosAnticiposOCS.Columns.Clear();
            dgvPagosFacturaNormal.Columns.Clear();
            dgvPC.Columns.Clear();
            dgvPP.Columns.Clear();
            dgvSPAnticiposOCS.Columns.Clear();
            dgvSPFacturaNormal.Columns.Clear();
        }

        private void dgvOCS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adocs.pintarCeldas(dgvOCS);
        }

        private void btnEditarObservación_Click(object sender, EventArgs e)
        {
            /*
             * Editar Observación
             * La función de este botón es la de recuperar el contenido del comentario para editarlo.
             * */
            btnEditarObservación.Enabled = false;
            btnDeshacer.Enabled = true;
            btnGuardaObservacion.Enabled = true;
            txtNota.Enabled = true;
            txtNota.Text = "";
            switch (Int32.Parse(cmbTipoVista.SelectedItem.ToString()))
            {
                case 1:
                    {
                        txtNota.Text = adpc.recuperarComentario(dgvPC.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString())); // Ningún DataGridView acepta selección múltiple de filas. Por eso trabajo solo con el índice cero del conjunto seleccionado.
                    }break;
                case 2:
                    {
                        txtNota.Text = adocs.recuperarComentario(dgvOCS.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    }break;
                case 3:
                    {
                        txtNota.Text = adocc.recuperarComentario(dgvOCC.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    }break;
                case 4:
                    {
                        txtNota.Text = adaocs.recuperarComentario(dgvAnticiposOCS.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 5:
                    {
                        txtNota.Text = adspa.recuperarComentario(dgvSPAnticiposOCS.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 6:
                    {
                        txtNota.Text = adpa.recuperarComentario(dgvPagosAnticiposOCS.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 7:
                    {
                        txtNota.Text = adfn.recuperarComentario(dgvFacturasNormales.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 8:
                    {
                        txtNota.Text = adspfn.recuperarComentario(dgvSPFacturaNormal.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 9:
                    {
                        txtNota.Text = adpfn.recuperarComentario(dgvPagosFacturaNormal.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 10:
                    {
                        txtNota.Text = adpp.recuperarComentario(dgvPP.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
                case 11:
                    {
                        txtNota.Text = adibg.recuperarComentario(dgvIBG.SelectedRows[0], Int32.Parse(cmbTipoVista.SelectedItem.ToString()));
                    } break;
            }
        }

        private void btnEditarObservación_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Edita la observación de la fila en la cuadrícula seleccionada", btnEditarObservación);
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            btnDeshacer.Enabled = false;
            btnGuardaObservacion.Enabled = false;
            btnEditarObservación.Enabled = true;
            txtNota.Text = "";
            txtNota.Enabled = false;
        }

        private void btnFiltrar_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Aplica el filtro de fechas", btnFiltrar);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            /*
             * Filtrar por fechas:
             * Antes de filtrar y enviar la data a la CAD hay que verificar que la fecha desde y hasta no se encuentre invertida.
             * */
            dt1 = DateTime.Parse(dtpDesde.Text);
            dt2 = DateTime.Parse(dtpHasta.Text);

            if (dt2.Subtract(dt1).Days > 1)
            {
                // Aquí van las llamadas a todos las clases de las capas de Acceso a Datos
                adpc.llenaGrid(dgvPC, dt1, dt2);
            }
            else
            {
                // Aquí van las llamadas a todos las clases de las capas de Acceso a Datos pero con las fechas invertidas.
                adpc.llenaGrid(dgvPC, dt2, dt1);
            }
        }

        private void dgvOCC_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adocc.pintarCeldas(dgvOCC);
        }

        private void dgvAnticiposOCS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adaocs.pintarCeldas(dgvAnticiposOCS);
        }

        private void dgvFacturasNormales_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adfn.pintarCeldas(dgvFacturasNormales);
        }

        private void dgvSPFacturaNormal_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adspfn.pintarCeldas(dgvSPFacturaNormal);
        }

        private void dgvPagosFacturaNormal_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adpfn.pintarCeldas(dgvPagosFacturaNormal);
        }

        private void dgvPP_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adpp.pintarCeldas(dgvPP);
        }

        private void dgvIBG_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adibg.pintarCeldas(dgvIBG);
        }
        
        private void chkAbrirDGV_CheckedChanged(object sender, EventArgs e)
        {
            // Este control no está habilitado hasta que muevan en cmbTipoVista y este tenga seleccionado un ítem
            // Sirve para ampliar la data.
            if (chkAbrirDGV.Checked)
                ampliarDGV(cmbTipoVista.SelectedIndex+1);
            else
                ampliarDGV(0);
        }

        private void ampliarDGV(int nroDGV)
        {
            switch (nroDGV)
            {
                case 0:
                    {
                        // Reestablecer todo a su estado original.
                        dgvPC.Location=p1;
                        dgvOCS.Location=p2;
                        dgvOCC.Location=p3;
                        dgvAnticiposOCS.Location=p4;
                        dgvSPAnticiposOCS.Location=p5;
                        dgvPagosAnticiposOCS.Location=p6;
                        dgvFacturasNormales.Location=p7;
                        dgvSPFacturaNormal.Location=p8;
                        dgvPagosFacturaNormal.Location=p9;
                        dgvPP.Location=p10;
                        dgvIBG.Location = p11;

                        dgvPC.Width = width_original;
                        dgvPC.Height = height_original;
                        dgvOCS.Width = width_original;
                        dgvOCS.Height = height_original;
                        dgvOCC.Width = width_original;
                        dgvOCC.Height = height_original;
                        dgvAnticiposOCS.Width = width_original;
                        dgvAnticiposOCS.Height = height_original;
                        dgvSPAnticiposOCS.Width = width_original;
                        dgvSPAnticiposOCS.Height = height_original;
                        dgvPagosAnticiposOCS.Width = width_original;
                        dgvPagosAnticiposOCS.Height = height_original;
                        dgvFacturasNormales.Width = width_original;
                        dgvFacturasNormales.Height = height_original;
                        dgvSPFacturaNormal.Width = width_original;
                        dgvSPFacturaNormal.Height = height_original;
                        dgvPagosFacturaNormal.Width = width_original;
                        dgvPagosFacturaNormal.Height = height_original;
                        dgvPP.Width = width_original;
                        dgvPP.Height = height_original;
                        dgvIBG.Width = width_original;
                        dgvIBG.Height = height_original;
                    }break;
                case 1:
                    {
                        dgvPC.Location = ampliado;
                        dgvPC.Width = width_modif;
                        dgvPC.Height = height_modif;
                        dgvPC.BringToFront();
                    }break;
                case 2:
                    {
                        dgvOCS.Location = ampliado;
                        dgvOCS.Width = width_modif;
                        dgvOCS.Height = height_modif;
                        dgvOCS.BringToFront();
                    }break;
                case 3:
                    {
                        dgvOCC.Location = ampliado;
                        dgvOCC.Width = width_modif;
                        dgvOCC.Height = height_modif;
                        dgvOCC.BringToFront();
                    }break;
                case 4:
                    {
                        dgvAnticiposOCS.Location = ampliado;
                        dgvAnticiposOCS.Width = width_modif;
                        dgvAnticiposOCS.Height = height_modif;
                        dgvAnticiposOCS.BringToFront();
                    } break;
                case 5:
                    {
                        dgvSPAnticiposOCS.Location = ampliado;
                        dgvSPAnticiposOCS.Width = width_modif;
                        dgvSPAnticiposOCS.Height = height_modif;
                        dgvSPAnticiposOCS.BringToFront();
                    } break;
                case 6:
                    {
                        dgvPagosAnticiposOCS.Location = ampliado;
                        dgvPagosAnticiposOCS.Width = width_modif;
                        dgvPagosAnticiposOCS.Height = height_modif;
                        dgvPagosAnticiposOCS.BringToFront();
                    } break;
                case 7:
                    {
                        dgvFacturasNormales.Location = ampliado;
                        dgvFacturasNormales.Width = width_modif;
                        dgvFacturasNormales.Height = height_modif;
                        dgvFacturasNormales.BringToFront();
                    } break;
                case 8:
                    {
                        dgvSPFacturaNormal.Location = ampliado;
                        dgvSPFacturaNormal.Width = width_modif;
                        dgvSPFacturaNormal.Height = height_modif;
                        dgvSPFacturaNormal.BringToFront();
                    } break;
                case 9:
                    {
                        dgvPagosFacturaNormal.Location = ampliado;
                        dgvPagosFacturaNormal.Width = width_modif;
                        dgvPagosFacturaNormal.Height = height_modif;
                        dgvPagosFacturaNormal.BringToFront();
                    } break;
                case 10:
                    {
                        dgvPP.Location = ampliado;
                        dgvPP.Width = width_modif;
                        dgvPP.Height = height_modif;
                        dgvPP.BringToFront();
                    } break;
                case 11:
                    {
                        dgvIBG.Location = ampliado;
                        dgvIBG.Width = width_modif;
                        dgvIBG.Height = height_modif;
                        dgvIBG.BringToFront();
                    } break;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (flagRadioSeleccionado==0)
            {
                MessageBox.Show("Debe habilitar el modo auditoría en una cuadrícula.");
            }
            else
            {
                //MessageBox.Show("Exportar Excel.. DGV: " + flagRadioSeleccionado);
                ExportToExcel(pBar);
            }
        }


        public void ExportToExcel(ProgressBar pBar)
        {

            if (pBar != null)
            {
                pBar.Maximum = dgvPC.RowCount +
                    dgvOCS.RowCount +
                    dgvOCC.RowCount +
                    dgvAnticiposOCS.RowCount +
                    dgvSPAnticiposOCS.RowCount +
                    dgvPagosAnticiposOCS.RowCount +
                    dgvFacturasNormales.RowCount +
                    dgvSPFacturaNormal.RowCount +
                    dgvPagosFacturaNormal.RowCount +
                    dgvPP.RowCount +
                    dgvIBG.RowCount+1;
                pBar.Value = 0;
                if (!pBar.Visible) pBar.Visible = true;
            }
            string sFont = "Verdana";
            int iSize = 8;
            //CREACIÓN DE LOS OBJETOS DE EXCEL
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Worksheet xlsSheet;
            Microsoft.Office.Interop.Excel.Workbook xlsBook;
            //AGREGAMOS EL LIBRO Y HOJA DE EXCEL
            xlsBook = xlsApp.Workbooks.Add(true);
            xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.ActiveSheet;
            //ESPECIFICAMOS EL TIPO DE LETRA Y TAMAÑO DE LA LETRA DEL LIBRO
            xlsSheet.Rows.Cells.Font.Size = iSize;
            xlsSheet.Rows.Cells.Font.Name = sFont;
            //Variables de posicionamiento, tanto para el recorrer el dgv como para escribir en el xls.
            int filDGV = 0, colDGV = 0;
            int filXLS = 3, colXLS = 1;

            Microsoft.Office.Interop.Excel.Range r;
            Color c;

            // Inicio con el dgvPC:
            DataGridView dgView = dgvPC;
            if (dgView.RowCount<1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PLANES DE COMPRA - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PLANES DE COMPRA";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS-1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    
                    colXLS = 1;
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // dgvOCS
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvOCS;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ORDENES DE COMPRA SIMPLES - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ORDENES DE COMPRA SIMPLES";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }


            // dgvOCC
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvOCC;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ORDENES DE COMPRA CONSOLIDADAS - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ORDENES DE COMPRA CONSOLIDADAS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // Anticipos ordenes de Compra Simples
            filXLS+= + 4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvAnticiposOCS;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }


            // SP de Anticipos ordenes de Compra Simples
            filXLS+= + 4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvSPAnticiposOCS;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "SP DE ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "SP DE ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // pagos de anticipos desde ordenes de compra simples.
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvPagosAnticiposOCS;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PAGOS DE ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PAGOS DE ANTICIPOS DESDE ORDENES DE COMPRA SIMPLES";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // facturas normales proveedor
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvFacturasNormales;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "FACTURAS NORMALES PROVEEDOR - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "FACTURAS NORMALES PROVEEDOR";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                
                                //xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;


                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // SP de facturas normales proveedor
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvSPFacturaNormal;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "SP DE FACTURAS NORMALES PROVEEDOR - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "SP DE FACTURAS NORMALES PROVEEDOR";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }

            // Pagos de facturas normales proveedor
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvPagosFacturaNormal;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PAGOS DE FACTURAS NORMALES PROVEEDOR - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PAGOS DE FACTURAS NORMALES PROVEEDOR";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }


            // Pedidos Proveedor
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvPP;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PEDIDOS PROVEEDOR - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "PEDIDOS PROVEEDOR";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }           


            // Ingresos de Bodega
            filXLS += +4;
            colXLS = 1;
            filDGV = 0;
            colDGV = 0;
            dgView = dgvIBG;
            if (dgView.RowCount < 1)
            {
                xlsSheet.Cells[filXLS - 2, 1] = "INGRESOS DE BODEGA - SIN DATOS";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
            }
                
            else
            {
                xlsSheet.Cells[filXLS - 2, 1] = "INGRESOS DE BODEGA";
                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 2, 1];
                xlsSheet.get_Range(r, r).Font.Bold = true;
                xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.CornflowerBlue);
                try
                {
                    // Copio los encabezados de columna
                    foreach (DataGridViewColumn column in dgView.Columns)
                    {
                        if (column.Visible)
                        {
                            xlsSheet.Cells[filXLS - 1, colXLS] = column.HeaderText;
                            r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS - 1, colXLS++];
                            xlsSheet.get_Range(r, r).Font.Bold = true;
                            xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        }
                    }
                    colXLS = 1;
                    // filXLS = 3;  Sino hago sobreescritura sobre el encabezado.
                    //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                    for (filDGV = 0; filDGV < dgView.RowCount; filDGV++)
                    {
                        for (colDGV = 0; colDGV < dgView.ColumnCount; colDGV++)
                        {
                            if (dgView.Rows[filDGV].Cells[colDGV].Visible)
                            {
                                // Las filas y columnas en excel se numeran desde 1.. NO DESDE CERO.. ojo!!
                                xlsSheet.Cells[filXLS, colXLS] = dgView.Rows[filDGV].Cells[colDGV].Value;
                                c = dgView.Rows[filDGV].Cells[colDGV].Style.BackColor;
                                r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[filXLS, colXLS];
                                if (!c.IsEmpty)
                                {
                                    // COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                                    xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[filDGV].Cells[colDGV].Style.BackColor);
                                }
                                // Alineación
                                xlsSheet.get_Range(r, r).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                ++colXLS;
                            }
                        }
                        if (dgView.ColumnCount < colDGV)
                        {
                            if (!dgView.Columns[colDGV].Visible)
                                ++filXLS;
                        }
                        pBar.Value += 1;
                        ++filXLS;
                        colXLS = 1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }


            if (pBar != null)
            {
                pBar.Value = 0;
                pBar.Visible = false;
            }
            xlsSheet.Columns.AutoFit();
            xlsSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            xlsSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            xlsSheet.PageSetup.Zoom = 80;
            xlsApp.Visible = true;
        }
    }
}
