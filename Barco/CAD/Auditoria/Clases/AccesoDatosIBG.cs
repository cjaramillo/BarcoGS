﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosIBG : MostrarData
    {
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (dgv==null)
            {
                return -1;
            }
            sqlQuery = @"
                select top(200)compra.idCompra as idCompraIBG, PP.idCompra as idCompraPP, compra.Numero, Cliente.Nombre as Proveedor,compra.Estacion, 
	                compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, compra.fechavencimiento as 'Fecha Vencimiento', Total, 
	                compra.Notas, Comprobante, NovedadImportacion.Observacion
                from Compra inner join Cliente on compra.idCliente=cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
	                (
		                select compra.idCompra, compra.Numero 
                        from Compra 
                        where compra.idTipoFactura=14 and compra.borrar=0
	                ) PP on compra.Comprobante=PP.Numero
                where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
                order by compra.idCompra desc
            ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            dgv.Columns.Clear();
            if (dgv == null || idRegistro < 1)
            {
                return -1;
            }
            sqlQuery = @"
                select top(200)compra.idCompra as idCompraIBG, PP.idCompra as idCompraPP, compra.Numero, Cliente.Nombre as Proveedor,compra.Estacion, 
	                compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, compra.fechavencimiento as 'Fecha Vencimiento', Total, 
	                compra.Notas, Comprobante, NovedadImportacion.Observacion
                from Compra inner join Cliente on compra.idCliente=cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
	                (
		                select compra.idCompra, compra.Numero 
                        from Compra 
                        where compra.idTipoFactura=14 and compra.borrar=0
	                ) PP on compra.Comprobante=PP.Numero
                where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0 and compra.idcompra=" + idRegistro+@" 
                order by compra.idCompra desc";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int[] idRegistro)
        {
            if (idRegistro[0] < 1 || dgv == null)
            {
                return -1; // El conjunto de datos está vacío
            }
            dgv.Columns.Clear();
            String listaIds = "";
            int i = 0;
            while (idRegistro[i] != 0)
            {
                if (idRegistro[i + 1] != 0)
                {
                    listaIds = String.Concat(listaIds, idRegistro[i].ToString(), ", ");
                }
                if (idRegistro[i + 1] == 0)
                {
                    listaIds = String.Concat(listaIds, idRegistro[i].ToString());
                    break;
                }
                i++;
            }
            if (listaIds.Length > 1)
            {
                sqlQuery = @"
                    select top(200)compra.idCompra as idCompraIBG, PP.idCompra as idCompraPP, compra.Numero, Cliente.Nombre as Proveedor,compra.Estacion, 
	                    compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, compra.fechavencimiento as 'Fecha Vencimiento', Total, 
	                    compra.Notas, Comprobante, NovedadImportacion.Observacion
                    from Compra inner join Cliente on compra.idCliente=cliente.idCliente
	                    left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                    left outer join 
	                    (
		                    select compra.idCompra, compra.Numero 
                            from Compra 
                            where compra.idTipoFactura=14 and compra.borrar=0
	                    ) PP on compra.Comprobante=PP.Numero
                    where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0 and compra.idcompra in (" + listaIds + @") 
                    order by compra.idCompra desc";
                miClase.LlenaGrid(dgv, "compra", sqlQuery);
                estilo(dgv);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, DateTime vDesde, DateTime vHasta)
        {
            if (vDesde == null || vHasta == null || dgv == null)
            {
                return -1;
            }
            dgv.Columns.Clear();
            string desde = vDesde.AddDays(-1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string hasta = vHasta.AddDays(1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            sqlQuery = @"
                select top(200)compra.idCompra as idCompraIBG, PP.idCompra as idCompraPP, compra.Numero, Cliente.Nombre as Proveedor,compra.Estacion, 
	                compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, compra.fechavencimiento as 'Fecha Vencimiento', Total, 
	                compra.Notas, Comprobante, NovedadImportacion.Observacion
                from Compra inner join Cliente on compra.idCliente=cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
	                (
		                select compra.idCompra, compra.Numero 
                        from Compra 
                        where compra.idTipoFactura=14 and compra.borrar=0
	                ) PP on compra.Comprobante=PP.Numero
                where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
                    and compra.fecha>='" + desde + "' and compra.fecha<='" + hasta + @"' order by compra.idCompra desc";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Ingresos de Bodega
            dgv.Columns[0].Name = "idCompraIBG";
            dgv.Columns[0].Visible = false; // idCompraPP va oculto
            dgv.Columns[1].Name = "idCompraPP";
            dgv.Columns[1].Visible = false; // idCompraOC va oculto
            dgv.Columns[2].Width = 80; // Numero
            dgv.Columns[3].Width = 150; // Nombre
            dgv.Columns[4].Width = 80; // Estación
            dgv.Columns[5].Width = 100; // Fecha Ingreso
            dgv.Columns[6].Width = 100; // Fecha
            dgv.Columns[7].Width = 100; // Fecha Vencimiento
            dgv.Columns[8].Width = 80; // Total
            dgv.Columns[8].DefaultCellStyle.Format = "F";
            dgv.Columns[9].Width = 150; // Notas
            dgv.Columns[10].Width = 100; // Comprobante
            dgv.Columns[11].Width = 200; // Observación.
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // IBG No tiene calculo de tiempos
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // IBG no tiene calculo de tiempos ni celdas de alerta
        }
    }
}
