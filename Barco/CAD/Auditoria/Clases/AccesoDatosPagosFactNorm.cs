using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosPagosFactNorm : MostrarData
    {
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (dgv==null)
            {
                return -1;
            }
            sqlQuery = @"
                SELECT  TOP(200) Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                        CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                        NovedadImportacion.Observacion
                FROM    Pago 
	                LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                WHERE   Pago.idCompra IN
                        (
			                SELECT  distinct Compra.idCompra
							FROM    DetCompra 
									INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
									LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
							WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
									AND Compra.idTipoFactura=4 
									and compra.Usuario<>'SPLotes' and compra.Borrar=0
		                )
                AND Pago.Borrar = 0
            ";
            miClase.LlenaGrid(dgv, "pago", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
             dgv.Columns.Clear();
            if (dgv == null|| idRegistro < 1)
            {
                return -1;
            }
            sqlQuery = @"
                SELECT  TOP(200) Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                        CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                        NovedadImportacion.Observacion
                FROM    Pago 
	                LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                WHERE   Pago.idCompra IN
                        (
			                SELECT  distinct Compra.idCompra
							FROM    DetCompra 
									INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
									LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
							WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
									AND Compra.idTipoFactura=4 
									and compra.Usuario<>'SPLotes' and compra.Borrar=0
		                )
                AND Pago.Borrar = 0 and pago.idcompra="+idRegistro+" ";
            miClase.LlenaGrid(dgv, "pago", sqlQuery);
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
                    SELECT  TOP(200) Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
	                    LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                    WHERE   Pago.idCompra IN
                            (
			                    SELECT  distinct Compra.idCompra
							    FROM    DetCompra 
									    INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
									    LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
							    WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
									    AND Compra.idTipoFactura=4 
									    and compra.Usuario<>'SPLotes' and compra.Borrar=0
		                    )
                    AND Pago.Borrar = 0 and pago.idcompra in (" + listaIds + ") ";
                miClase.LlenaGrid(dgv, "pago", sqlQuery);
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
                SELECT  TOP(200) Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                        CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                        NovedadImportacion.Observacion
                FROM    Pago 
	                LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                WHERE   Pago.idCompra IN
                        (
			                SELECT  distinct Compra.idCompra
							FROM    DetCompra 
									INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
									LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
							WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
									AND Compra.idTipoFactura=4 
									and compra.Usuario<>'SPLotes' and compra.Borrar=0
		                )
                AND Pago.Borrar = 0 and pago.fecha>='" + desde + "' and pago.fecha<='" + hasta + @"' ";
            miClase.LlenaGrid(dgv, "pago", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Pagos de facturas normales desde orden de compra simple.
            dgv.Columns[0].Name = "idPagoFactNormal";
            dgv.Columns[0].Visible = false; // idPago va oculto
            dgv.Columns[1].Name = "idCompraFactNormal";
            dgv.Columns[1].Visible = false; // idCompra va oculto
            dgv.Columns[2].Width = 100; // FechaIngreso
            dgv.Columns[3].Width = 80; // Numero
            dgv.Columns[4].Width = 80; // Pago
            dgv.Columns[4].DefaultCellStyle.Format = "F";
            dgv.Columns[5].Width = 80; // Usuario
            dgv.Columns[6].Width = 100; // Fecha
            dgv.Columns[7].Width = 150; // Concepto
            dgv.Columns[8].Width = 80; // NumeroIngreso
            dgv.Columns[9].Width = 80; // Cheque
            dgv.Columns[10].Width = 80; // Cuenta
            dgv.Columns[11].Width = 200; // Nombre Proveedor
            dgv.Columns[12].Width = 150; // Observación
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // No hay porque está incluido en la consulta sql.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // No existe implementación porque ya está pintado el dgv de las facturas.
        }
    }
}
