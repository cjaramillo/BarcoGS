using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class PreviaReportes : Form
    {
        public ReportDocument oRpt;//Objeto para manipular el reporte
        Datos miclase = new Datos();
        string strReporte;

       

        public void Titulo(string strTitulo)
        {
            Section section;
            TextObject textObject = null;

            try
            {
                section = oRpt.ReportDefinition.Sections["ReportHeaderSection1"];
                textObject = section.ReportObjects["pcTitulo"] as TextObject;
            }
            catch
            {
                try
                {
                    section = oRpt.ReportDefinition.Sections["PageHeaderSection1"];
                    textObject = section.ReportObjects["pcTitulo"] as TextObject;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message + ". Funcion: Titulo");
                }
            }

            if (textObject != null)
            {
                textObject.Text = strTitulo;//--------------> ASIGNA TITULO
                //textObject.Color = Color.Blue;
            }
        }

        public void VerReporte(string Reporte, string filtro)
        {
            oRpt = new ReportDocument();



            strReporte = Datos.strReporte + Reporte;
            //strReporte = @"\\Servidor\Latinium\Reportes\" + Reporte;
            //strReporte = @"\\ServerGye\Latinium\Reportes\" + Reporte;
            //strReporte = @"\\Pc1\Latinium\Reportes\" + Reporte;
            //strReporte = @"C:\Latinium\Reportes\" + Reporte;

            this.crvReportes.SelectionFormula = filtro;

            if (!File.Exists(@strReporte))
            {
                MessageBox.Show("Archivo no existe: " + strReporte);
                return;
            }
            //---------------Bloque try para cargar el Reporte //

            try
            {
                oRpt.Load(strReporte);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Carga de Reporte: " + strReporte);
                return;
            }

            ConnectionInfo crConnectionInfo = new ConnectionInfo();

            //Seteo la Informacion para la cadena de conexion de los Reportes

            crConnectionInfo.ServerName = Datos.strServidor;
            crConnectionInfo.DatabaseName = Datos.strBase;
            crConnectionInfo.UserID = "fer";
            crConnectionInfo.Password = "05043001";

            //Declaro los objetos que voy a utilizar

            TableLogOnInfo crTableLogOnInfo;
            Database crDatabase = oRpt.Database;//-->Para la BDD
            Tables crTables = crDatabase.Tables;//-->Para las tablas
            Table crTable;

            //------Me barro las tablas-----

            for (int i = 0; i < crTables.Count; i++)
            {
                crTable = crTables[i];
                crTableLogOnInfo = crTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo; //-->Asigno La Informacion de la Conexion
                crTable.ApplyLogOnInfo(crTableLogOnInfo);

            }

            this.crvReportes.ReportSource = oRpt;

        }


        public PreviaReportes()
        {
            InitializeComponent();
            
        }

        private void PreviaReportes_Load(object sender, EventArgs e)
        {
            btnImprime.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            this.oRpt.PrintToPrinter(1, false, 1, 1);
        }
    }
}
