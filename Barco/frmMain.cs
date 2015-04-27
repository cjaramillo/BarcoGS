using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barco
{
    public partial class frmMain : Form
    {
        
        
        public frmMain()
        {
            InitializeComponent();
        }

        Datos miClase = new Datos();

        private void frmMain_Load(object sender, EventArgs e)
        {
            string stArch = @"c:\Servidor.txt";
            string stArchBase = @"c:\Catalogo.txt";
            string stArchReportes = @"c:\Reporte.txt";

            if (File.Exists(stArch))
            {
                StreamReader sr = File.OpenText(stArch);
                Datos.strServidor = sr.ReadLine();
            }
            else
            {
                Datos.strServidor = @"192.168.1.15";
            }

            if (File.Exists(stArchBase))
            {
                StreamReader sr = File.OpenText(stArchBase);
                Datos.strBase = sr.ReadLine();
            }
            else
            {
                Datos.strBase = "GraphicSource2007";
            }

            if (File.Exists(stArchReportes))
            {
                StreamReader sr = File.OpenText(stArchReportes);
                Datos.strReporte = sr.ReadLine();
            }
            else
            {
                Datos.strReporte = @"\\Servidor\Latinium\Reportes\";
            }
            Datos.strMaquina = miClase.EjecutaEscalarStr("select host_name()");
            Datos.idSucursal = miClase.EjecutaEscalar("Select Top 1 IdSucursal from SucursalGs Where Principal=1");
            this.Text += " Empresa: " + Datos.strBase.ToString();
            if (Datos.strServidor.Trim().ToUpper().CompareTo("CESAR") == 0)
            {
                //lblServer.Text += Datos.strServidor;
                lblServer.Text = "BDD DESARROLLO";
                lblServer.BackColor = Color.LightGreen;
            }
            else
            {
                lblServer.Visible = false;
            }
        }



        private void mAuditoríaImportaciones_Click(object sender, EventArgs e)
        {
            Auditoria auditoria = new Auditoria();
            auditoria.MdiParent = this;
            auditoria.Show();
            
        }

        private void mOrdenDeCompraLotes_Click(object sender, EventArgs e)
        {
            Datos.idTipoFactura = 2;
            TransformaOrdenes objLotes = new TransformaOrdenes();
            objLotes.MdiParent = this;
            objLotes.Show();
        }

        private void mSolicitarAnticipos_Click(object sender, EventArgs e)
        {
            // Procesar solicitudes de anticipos.
            miClase.EjecutaSql("exec sp_GeneraSP", true);
        }

        private void mReglas_Click(object sender, EventArgs e)
        {
            Reglas r = new Reglas();
            r.MdiParent = this;
            r.Show();
        }

        private void mOcultarMostrarOC_Click(object sender, EventArgs e)
        {
            OcultaDocumentos oc = new OcultaDocumentos();
            oc.MdiParent = this;
            oc.Show();
        }

        private void mAsignarOCAPlanCompras_Click(object sender, EventArgs e)
        {
            // Asignar OC a plan de compras.
            AsignarOCaPC aocpp = new AsignarOCaPC();
            aocpp.MdiParent = this;
            aocpp.Show();
        }

        private void mGenerarReportes_Click(object sender, EventArgs e)
        {
            ReportesImportaciones ri = new ReportesImportaciones();
            ri.MdiParent = this;
            ri.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test t = new Test();
            t.MdiParent = this;
            t.Show();
        }

        private void respaldaYRestauraIBGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestauraIBG ribg = new RestauraIBG();
            ribg.MdiParent = this;
            ribg.Show();
        }

        private void liquidarImportacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiqImportaciones li = new LiqImportaciones();
            li.MdiParent = this;
            li.Show();
        }

        private void requisicionesAOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transformar t = new Transformar();
            t.modo = 0;
            t.MdiParent = this;
            t.Show();
        }

        private void desbloqueaReqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transformar t = new Transformar();
            t.modo = 1;
            t.MdiParent = this;
            t.Show();
        }

        private void ingresoDatosOCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputDataOCS idocs = new InputDataOCS();
            idocs.MdiParent = this;
            idocs.Show();
        }

        private void oCSCantidadesAPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transformar t = new Transformar();
            t.modo = 2;
            t.MdiParent = this;
            t.Show();
        }
    }
}
