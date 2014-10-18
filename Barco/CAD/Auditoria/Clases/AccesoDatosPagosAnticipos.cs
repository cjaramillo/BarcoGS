using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosPagosAnticipos:MostrarData
    {
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (dgv==null)
            {
                return -1;
            }
            /*
            sqlQuery = @"
                SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                        CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                        NovedadImportacion.Observacion
                FROM    Pago 
	                LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                WHERE   Pago.idCompra IN
                        (
			                SELECT Compra.idCompra
			                FROM   Compra 
				                INNER JOIN Cliente ON Compra.idCliente = Cliente.idCliente
			                WHERE Compra.idTipoFactura = 4 AND 
				                Compra.Pedido IN 
				                (
					                SELECT Numero
					                FROM   ControlOCLotes
					                UNION
					                SELECT Numero
					                FROM   Compra AS Compra_1
					                WHERE idTipoFactura = 2 AND Usuario <> 'OrdenLotes'
				                 )
		                )
                AND Pago.Borrar = 0
            ";
            */
            sqlQuery = @"
					SELECT  top(100) Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
						LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
					WHERE pago.Borrar=0
                    ORDER BY Pago.idPago desc
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
            /*
            sqlQuery = @"
                SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                        CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                        NovedadImportacion.Observacion
                FROM    Pago 
	                LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                WHERE   Pago.idCompra IN
                        (
			                SELECT Compra.idCompra
			                FROM   Compra 
				                INNER JOIN Cliente ON Compra.idCliente = Cliente.idCliente
			                WHERE Compra.idTipoFactura = 4 AND 
				                Compra.Pedido IN 
				                (
					                SELECT Numero
					                FROM   ControlOCLotes
					                UNION
					                SELECT Numero
					                FROM   Compra AS Compra_1
					                WHERE idTipoFactura = 2 AND Usuario <> 'OrdenLotes'
				                 )
		                )
                AND Pago.Borrar = 0 and pago.idcompra=" + idRegistro;
            */
            sqlQuery = @"
					SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
						LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
					WHERE pago.Borrar=0 and pago.idCompra=" + idRegistro + " ORDER BY Pago.idPago desc";
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
                /*
                sqlQuery = @"
                    SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
	                    LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
                    WHERE   Pago.idCompra IN
                            (
			                    SELECT Compra.idCompra
			                    FROM   Compra 
				                    INNER JOIN Cliente ON Compra.idCliente = Cliente.idCliente
			                    WHERE Compra.idTipoFactura = 4 AND 
				                    Compra.Pedido IN 
				                    (
					                    SELECT Numero
					                    FROM   ControlOCLotes
					                    UNION
					                    SELECT Numero
					                    FROM   Compra AS Compra_1
					                    WHERE idTipoFactura = 2 AND Usuario <> 'OrdenLotes'
				                        )
		                    )
                    AND Pago.Borrar = 0 and pago.idcompra in (" + listaIds + ") ";
                */
                sqlQuery = @"
					SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
						LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
					WHERE pago.Borrar=0 and pago.idCompra in (" + listaIds + ") ORDER BY Pago.idPago desc";
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
            /*
            sqlQuery = @"
                select pago.idPago, pago.idCompra, pago.FechaIngreso as 'Fecha Ingreso', pago.Numero, pago.Pago, pago.Fecha, pago.Concepto, 
                    pago.NumeroIngreso as 'Numero Ingreso', pago.Cheque, pago.Cuenta, 
                    case when pago.numero like '%N/C' then 'NOTA CREDITO' else pago.Nombre end as Proveedor, pago.Usuario  
                from Pago 
                where idCompra in 
                (
                    select compra.idCompra
                    FROM Compra 
                    inner join Cliente on compra.idCliente=cliente.idCliente
                    WHERE idTipoFactura=4 AND Pedido in 
                        (select numero from ControlOCLotes
                        union
                        select numero from Compra where idTipoFactura=2 and Usuario<>'OrdenLotes')
                ) and pago.Borrar=0 and  pago.fecha>='" + desde + "' and pago.fecha<='" + hasta + @"' ";
            */
            sqlQuery = @"
					SELECT  Pago.idPago, Pago.idCompra, Pago.FechaIngreso AS 'Fecha Ingreso', Pago.Numero, Pago.Pago, Pago.Usuario, 
		                    Pago.Fecha, Pago.Concepto, Pago.NumeroIngreso AS 'Numero Ingreso', Pago.Cheque, Pago.Cuenta, 
                            CASE WHEN pago.numero LIKE '%N/C' THEN 'NOTA CREDITO' ELSE pago.Nombre END AS Proveedor,
                            NovedadImportacion.Observacion
                    FROM    Pago 
						LEFT OUTER JOIN NovedadImportacion ON Pago.idPago = NovedadImportacion.idDocumento
					WHERE pago.Borrar=0 and  pago.fecha>='" + desde + "' and pago.fecha<='" + hasta + @"' ORDER BY Pago.idPago desc";
            miClase.LlenaGrid(dgv, "pago", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Pagos anticipos desde orden de compra simple.
            dgv.Columns[0].Name = "idPagosAnticiposOCS";
            dgv.Columns[0].Visible = false; // idPago va oculto
            dgv.Columns[1].Name = "idCompraFactOCS";
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
