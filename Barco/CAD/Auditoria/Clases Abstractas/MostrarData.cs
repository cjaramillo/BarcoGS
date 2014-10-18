using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barco.CAD
{
    abstract class MostrarData
    {

        protected int[] tipoFacturas = new int[] { 0, -2, 2, 3, 1, 26, -26, 1, 26, -26, 14, 9 }; // No uso indice 0 tanto en filas como en columnas. 
        protected int tipoFact, idDoc;

        /*
         * ID TIPOS FACTURA - ESTO SE UTILIZA PARA ALMACENAR EN NovedadImportacion.idTipoFactura
         * -2: Plan de Compras
         * 2: Plan de Compras Simple
         * 3: Plan de Compras Consolidado
         * 1: Anticipos desde Orden de Compra Simple
         * 26: SP de Anticipos desde Orden de Compra Simple
         * -26: Pagos de Anticipos desde Orden de Compra Simple
         * 1: Facturas de proveedor simples.
         * 26: SP de Facturas de proveedor simples.
         * -26: Pagos de Facturas de proveedor simples.
         * 14: Pedido Proveedor
         * 9: Ingreso a bodega
         * */



        protected Datos miClase = new Datos();
        protected string sqlQuery = "";

        protected void escribirComentario(int tipoFact, int idDoc, int nroGrid, string observacion)
        {
            // Si no existe lo crea. Si existe hace update.
            sqlQuery = "select count(*) from NovedadImportacion where idTipoFactura=" + tipoFact + " and idDocumento=" + idDoc + " and idGrid=" + nroGrid;
            if (miClase.EjecutaEscalar(sqlQuery) > 0)
            {
                // Update
                sqlQuery = "update NovedadImportacion set observacion='" + observacion + "' where idTipoFactura=" + tipoFact + " and idDocumento=" + idDoc + " and idGrid=" + nroGrid;
                miClase.EjecutaSql(sqlQuery, true);
            }
            else
            {
                // Insert
                sqlQuery = "insert into NovedadImportacion (idTipoFactura, idDocumento, idGrid, observacion) values (" + tipoFact + "," + idDoc + "," + nroGrid + ",'" + observacion + "')";
                miClase.EjecutaSql(sqlQuery, true);
            }
        }


        public int ingresarComentario(DataGridViewRow dgvr, int nroGrid, string observacion)
        {
            if (nroGrid < 1 || nroGrid > 11 || dgvr == null)
                return -1; // Error
            else
            {
                tipoFact = tipoFacturas[nroGrid];
                idDoc = Int32.Parse(dgvr.Cells[0].Value.ToString());
                escribirComentario(tipoFact, idDoc, nroGrid, observacion);
                return 0;
            }
        }

        public int eliminarComentario(DataGridViewRow dgvr, int nroGrid)
        {
            if (nroGrid < 1 || nroGrid > 11 || dgvr == null)
                return -1; // Error
            else
            {
                tipoFact = tipoFacturas[nroGrid];
                idDoc = Int32.Parse(dgvr.Cells[0].Value.ToString());
                sqlQuery = "delete from NovedadImportacion where idTipoFactura=" + tipoFact + " and idDocumento=" + idDoc + " and idGrid=" + nroGrid;
                miClase.EjecutaSql(sqlQuery, true);
                return 0;
            }
        }

        public string recuperarComentario(DataGridViewRow dgvr, int nroGrid)
        {
            if (nroGrid < 1 || nroGrid > 11 || dgvr == null)
                return null; // Error
            else
            {
                tipoFact = tipoFacturas[nroGrid];
                idDoc = Int32.Parse(dgvr.Cells[0].Value.ToString());
                sqlQuery = "select observacion from NovedadImportacion where idTipoFactura=" + tipoFact + " and idDocumento=" + idDoc + " and idGrid=" + nroGrid;
                return miClase.EjecutaEscalarStr(sqlQuery);
            }
        }




        public abstract int llenaGrid(DataGridView dgv); // Llena todo.. ideal para inicialización.

        public abstract int llenaGrid(DataGridView dgv, int idRegistro);// Solo muestra un registro.

        public abstract int llenaGrid(DataGridView dgv, int[] idRegistro); // Muestra un conjunto de registros.

        public abstract int llenaGrid(DataGridView dgv, DateTime vDesde, DateTime vHasta);// Muestra registros en un rango comprendido de fechas basándose en compra.fecha.

        public abstract void estilo(DataGridView dgv);

        public abstract void calcularTiempos(DataGridView dgv);

        public abstract void pintarCeldas(DataGridView dgv);

       




       
    }
}
