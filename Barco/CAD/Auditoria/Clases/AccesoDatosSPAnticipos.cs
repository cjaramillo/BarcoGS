using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosSPAnticipos: MostrarData
    {
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (dgv == null)
            {
                return -1;
            }
            sqlQuery = @"
                select Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where Usuario='SPLotes' and compra.idTipoFactura=26
                order by idCompra asc";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            dgv.Columns.Clear();
            if (dgv == null || idRegistro == 0)
            {
                return -1;
            }
            sqlQuery = @"
                select Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where Usuario='SPLotes'  and compra.idTipoFactura=26 and compra.idcompra=" + idRegistro + " order by idCompra asc";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int[] idRegistro)
        {
            dgv.Columns.Clear();
            // Muestra un conjunto de registros.
            if (idRegistro[0] == -1 || idRegistro[0] == 0 || dgv == null)
            {
                return -1; // El conjunto de datos está vacío
            }
            else
            {
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
                        select Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                        Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                        from Compra 
	                        left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                        where Usuario='SPLotes' and compra.idTipoFactura=26 and compra.idcompra in (" + listaIds + ") order by idCompra asc";
                    dgv.Columns.Clear();
                    miClase.LlenaGrid(dgv, "compra", sqlQuery);
                    estilo(dgv);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, DateTime vDesde, DateTime vHasta)
        {
            dgv.Columns.Clear();
            if (dgv == null || vDesde == null || vHasta == null)
            {
                return -1;
            }
            string desde = vDesde.AddDays(-1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string hasta = vHasta.AddDays(1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            sqlQuery = @"
                select Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where Usuario='SPLotes'  and compra.idTipoFactura=26 and compra.fecha >=" + desde + " and compra.fecha<=" + hasta + " order by idCompra asc ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns[0].Name = "idCompraSpOCS";
            dgv.Columns[0].Visible = false; // idCompra va oculto.
            dgv.Columns[1].Width = 100; // FechaIngreso
            dgv.Columns[2].Width = 80; // Estación
            dgv.Columns[3].Name = "nroSpOCS";
            dgv.Columns[3].Width = 80; // Número
            dgv.Columns[4].Width = 100; // Fecha
            dgv.Columns[5].Width = 80; // Total
            dgv.Columns[5].DefaultCellStyle.Format = "F";
            dgv.Columns[6].Width = 200; // Notas
            dgv.Columns[7].Name = "pedidoCompraSpOCS";
            dgv.Columns[7].Width = 80; // Pedido
            dgv.Columns[8].Name = "ComprobanteSpOCS"; // Comprobante
            dgv.Columns[8].Width= 120;
            dgv.Columns[9].Width = 150; // Observación
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // No se implementa porque esta capa no tiene conteo de tiempos.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // No se implementa porque esta capa no tiene conteo de tiempos.
        }
    }
}
