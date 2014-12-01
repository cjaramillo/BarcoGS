using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosFactNorm: MostrarData
    {
        // Capa de acceso a datos de facturas normales de pago a proveedores 

        public int up_SP(int idRegistro)
        {
            // Devuelve el idCompra del SP asociado a esta compra normal
            sqlQuery = @"select idcompra from compra where idtipofactura=26 and borrar=0 and numero in (select top(1)otro from compra where idtipofactura=4 and borrar=0 and idcompra=" + idRegistro + ")";
            int resultado = 0;
            resultado = miClase.EjecutaEscalar(sqlQuery);
            if (resultado > 0)
                return resultado;
            else
                return -1;
        }


        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            if (dgv == null)
            {
                return -1;
            }
            dgv.Columns.Clear();
            sqlQuery = @"
               SELECT     Compra.idCompra, DetCompra.idDetCompra, DetCompra.idArticulo, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Numero AS 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', 
                      Artic.Articulo, SUBSTRING(Artic.Articulo, 1, 6) + '-2' + SUBSTRING(Artic.Articulo, 8, 10) AS 'IG', CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, 
                      Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Estacion, Compra.Total AS 'Valor Factura', SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) 
                      AS 'Valor cargado a la IG', DATEDIFF(DAY, Compra.Fecha, Compra.FechaIngreso) AS 'Dias Ingreso de Factura', Pagos.SumaPagos AS 'Suma Pagos', pagos.FechaMin AS 'Fecha Primer Pago', 
                      pagos.FechaMax AS 'Fecha Último Pago', tiemposAuditoria.PlazoPagoFacturaDias AS 'Plazo Días Pago Facturas', CASE WHEN pagos.SumaPagos IS NULL THEN NULL ELSE DATEDIFF(day, 
                      compra.fecha, Pagos.FechaMax) END AS 'Dias Pago Factura', CASE WHEN Pagos.SumaPagos IS NULL OR
                      tiemposAuditoria.PlazoPagoFacturaDias IS NULL THEN NULL ELSE DATEDIFF(day, compra.fecha, pagos.FechaMax) - tiemposAuditoria.PlazoPagoFacturaDias END AS 'Atraso dias Pago', 
                      Compra.Otro AS 'Comprobante SP', Compra.Pedido, NovedadImportacion.observacion
				FROM  DetCompra RIGHT OUTER JOIN
                      (
						SELECT idArticulo, Articulo
                        FROM   Articulo
                        WHERE  Articulo LIKE 'IG-%'
                      ) AS Artic ON DetCompra.idArticulo = Artic.idArticulo LEFT OUTER JOIN
                      Compra ON DetCompra.idCompra = Compra.idCompra LEFT OUTER JOIN
                      Cliente ON Compra.idCliente = Cliente.idCliente LEFT OUTER JOIN
                      NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento LEFT OUTER JOIN
                      (
	                    select idCompra,SUM(Pago) as SumaPagos,MIN(pago.Fecha) as FechaMin, MAX(pago.Fecha) as FechaMax, COUNT(Pago.Pago) as CuentaPagos
						from pago where pago.Borrar=0
						group by pago.idCompra
                      ) Pagos on detcompra.idCompra=Pagos.idCompra LEFT OUTER JOIN
                      tiemposAuditoria ON Compra.idCliente = tiemposAuditoria.idClientePE
				WHERE     (Compra.idTipoFactura = 4) AND (Compra.Usuario <> 'SPLotes')
				GROUP BY Compra.idCompra,DetCompra.idDetCompra,DetCompra.idArticulo,Compra.FechaIngreso,Compra.Numero, Cliente.Nombre, Artic.Articulo,CAST(Compra.Notas AS varchar(2000)),
					Compra.Fecha,Compra.FechaVencimiento, Compra.Estacion, Compra.Total, Pagos.SumaPagos, Pagos.FechaMin, Pagos.FechaMax, tiemposAuditoria.PlazoPagoFacturaDias,
					Compra.Otro, Compra.Pedido, NovedadImportacion.observacion
				ORDER BY Compra.idCompra DESC
            ";
            miClase.LlenaGrid(dgv, "detcompra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            if (dgv == null || idRegistro < 1)
                return -1;
            dgv.Columns.Clear();
            /*
            sqlQuery = @"
                SELECT  Top(150) Compra.idCompra, detcompra.idDetCompra, detcompra.idarticulo, Compra.FechaIngreso AS 'Fecha Ingreso', 
		                Compra.Numero as 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', Articulo.Articulo, 
		                SUBSTRING(Articulo.Articulo, 1,6)+'-2'+SUBSTRING(Articulo.Articulo, 8,10) as 'IG',
		                CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', 
		                Compra.Estacion, Compra.Total AS 'Valor Factura', 
		                SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) AS 'Valor cargado a la IG',
		                datediff(DAY, compra.Fecha, compra.FechaIngreso) as'Dias Ingreso de Factura', 
		                sum(pago.pago) as 'Suma Pagos', MIN(pago.fecha) as 'Fecha Primer Pago', MAX(pago.fecha) as 'Fecha Último Pago',
		                tiemposAuditoria.PlazoPagoFacturaDias as 'Plazo Días Pago Facturas',
		                case when sum(pago.Pago) is null then null else DATEDIFF(day, compra.fecha, max(pago.fecha)) end as 'Dias Pago Factura',
		                case when sum(pago.Pago) is null OR tiemposAuditoria.PlazoPagoFacturaDias IS null then 
							null 
						else 
							DATEDIFF(day, compra.fecha, max(pago.fecha))-tiemposAuditoria.PlazoPagoFacturaDias
						end as 'Atraso dias Pago',
		                Compra.Otro as 'Comprobante SP', Compra.Pedido, NovedadImportacion.Observacion
                FROM    DetCompra 
		                INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
		                INNER JOIN Cliente on Compra.idCliente=Cliente.idCliente
		                INNER JOIN Articulo on detcompra.idArticulo=Articulo.idArticulo
		                LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
		                LEFT OUTER JOIN Pago on detcompra.idCompra=pago.idCompra
		                LEFT OUTER JOIN tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
		                AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and compra.idcompra=" + idRegistro + @" 
                GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, Cliente.Nombre, 
		                CAST(Compra.Notas AS varchar(2000)), 
		                Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Otro, Compra.Pedido,
		                NovedadImportacion.Observacion, Articulo.Articulo,detcompra.idarticulo,tiemposAuditoria.PlazoPagoFacturaDias
                ORDER BY Compra.idCompra desc
            ";*/
            sqlQuery = @"
                SELECT     Compra.idCompra, DetCompra.idDetCompra, DetCompra.idArticulo, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Numero AS 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', 
                      Artic.Articulo, SUBSTRING(Artic.Articulo, 1, 6) + '-2' + SUBSTRING(Artic.Articulo, 8, 10) AS 'IG', CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, 
                      Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Estacion, Compra.Total AS 'Valor Factura', SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) 
                      AS 'Valor cargado a la IG', DATEDIFF(DAY, Compra.Fecha, Compra.FechaIngreso) AS 'Dias Ingreso de Factura', Pagos.SumaPagos AS 'Suma Pagos', pagos.FechaMin AS 'Fecha Primer Pago', 
                      pagos.FechaMax AS 'Fecha Último Pago', tiemposAuditoria.PlazoPagoFacturaDias AS 'Plazo Días Pago Facturas', CASE WHEN pagos.SumaPagos IS NULL THEN NULL ELSE DATEDIFF(day, 
                      compra.fecha, Pagos.FechaMax) END AS 'Dias Pago Factura', CASE WHEN Pagos.SumaPagos IS NULL OR
                      tiemposAuditoria.PlazoPagoFacturaDias IS NULL THEN NULL ELSE DATEDIFF(day, compra.fecha, pagos.FechaMax) - tiemposAuditoria.PlazoPagoFacturaDias END AS 'Atraso dias Pago', 
                      Compra.Otro AS 'Comprobante SP', Compra.Pedido, NovedadImportacion.observacion
				FROM  DetCompra RIGHT OUTER JOIN
                      (
						SELECT idArticulo, Articulo
                        FROM   Articulo
                        WHERE  Articulo LIKE 'IG-%'
                      ) AS Artic ON DetCompra.idArticulo = Artic.idArticulo LEFT OUTER JOIN
                      Compra ON DetCompra.idCompra = Compra.idCompra LEFT OUTER JOIN
                      Cliente ON Compra.idCliente = Cliente.idCliente LEFT OUTER JOIN
                      NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento LEFT OUTER JOIN
                      (
	                    select idCompra,SUM(Pago) as SumaPagos,MIN(pago.Fecha) as FechaMin, MAX(pago.Fecha) as FechaMax, COUNT(Pago.Pago) as CuentaPagos
						from pago where pago.Borrar=0
						group by pago.idCompra
                      ) Pagos on detcompra.idCompra=Pagos.idCompra LEFT OUTER JOIN
                      tiemposAuditoria ON Compra.idCliente = tiemposAuditoria.idClientePE
				WHERE     (Compra.idTipoFactura = 4) AND (Compra.Usuario <> 'SPLotes') AND compra.idcompra=" + idRegistro + @" 
				GROUP BY Compra.idCompra,DetCompra.idDetCompra,DetCompra.idArticulo,Compra.FechaIngreso,Compra.Numero, Cliente.Nombre, Artic.Articulo,CAST(Compra.Notas AS varchar(2000)),
					Compra.Fecha,Compra.FechaVencimiento, Compra.Estacion, Compra.Total, Pagos.SumaPagos, Pagos.FechaMin, Pagos.FechaMax, tiemposAuditoria.PlazoPagoFacturaDias,
					Compra.Otro, Compra.Pedido, NovedadImportacion.observacion
				ORDER BY Compra.idCompra DESC
            ";
            miClase.LlenaGrid(dgv, "detcompra", sqlQuery);
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
            if (listaIds.Length < 2)
            {
                return -1;
            }
            /*
            sqlQuery = @"
                SELECT  Top(150) Compra.idCompra, detcompra.idDetCompra, detcompra.idarticulo, Compra.FechaIngreso AS 'Fecha Ingreso', 
		                Compra.Numero as 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', Articulo.Articulo, 
		                SUBSTRING(Articulo.Articulo, 1,6)+'-2'+SUBSTRING(Articulo.Articulo, 8,10) as 'IG',
		                CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', 
		                Compra.Estacion, Compra.Total AS 'Valor Factura', 
		                SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) AS 'Valor cargado a la IG',
		                datediff(DAY, compra.Fecha, compra.FechaIngreso) as'Dias Ingreso de Factura', 
		                sum(pago.pago) as 'Suma Pagos', MIN(pago.fecha) as 'Fecha Primer Pago', MAX(pago.fecha) as 'Fecha Último Pago',
		                tiemposAuditoria.PlazoPagoFacturaDias as 'Plazo Días Pago Facturas',
		                case when sum(pago.Pago) is null then null else DATEDIFF(day, compra.fecha, max(pago.fecha)) end as 'Dias Pago Factura',
		                case when sum(pago.Pago) is null OR tiemposAuditoria.PlazoPagoFacturaDias IS null then 
							null 
						else 
							DATEDIFF(day, compra.fecha, max(pago.fecha))-tiemposAuditoria.PlazoPagoFacturaDias
						end as 'Atraso dias Pago',
		                Compra.Otro as 'Comprobante SP', Compra.Pedido, NovedadImportacion.Observacion
                FROM    DetCompra 
		                INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
		                INNER JOIN Cliente on Compra.idCliente=Cliente.idCliente
		                INNER JOIN Articulo on detcompra.idArticulo=Articulo.idArticulo
		                LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
		                LEFT OUTER JOIN Pago on detcompra.idCompra=pago.idCompra
		                LEFT OUTER JOIN tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
		                AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and compra.idcompra in (" + listaIds + @")
                GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, Cliente.Nombre, 
		                CAST(Compra.Notas AS varchar(2000)), 
		                Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Otro, Compra.Pedido,
		                NovedadImportacion.Observacion, Articulo.Articulo,detcompra.idarticulo,tiemposAuditoria.PlazoPagoFacturaDias
                ORDER BY Compra.idCompra desc            
            ";
             * */
            sqlQuery = @"
                SELECT     Compra.idCompra, DetCompra.idDetCompra, DetCompra.idArticulo, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Numero AS 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', 
                      Artic.Articulo, SUBSTRING(Artic.Articulo, 1, 6) + '-2' + SUBSTRING(Artic.Articulo, 8, 10) AS 'IG', CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, 
                      Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Estacion, Compra.Total AS 'Valor Factura', SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) 
                      AS 'Valor cargado a la IG', DATEDIFF(DAY, Compra.Fecha, Compra.FechaIngreso) AS 'Dias Ingreso de Factura', Pagos.SumaPagos AS 'Suma Pagos', pagos.FechaMin AS 'Fecha Primer Pago', 
                      pagos.FechaMax AS 'Fecha Último Pago', tiemposAuditoria.PlazoPagoFacturaDias AS 'Plazo Días Pago Facturas', CASE WHEN pagos.SumaPagos IS NULL THEN NULL ELSE DATEDIFF(day, 
                      compra.fecha, Pagos.FechaMax) END AS 'Dias Pago Factura', CASE WHEN Pagos.SumaPagos IS NULL OR
                      tiemposAuditoria.PlazoPagoFacturaDias IS NULL THEN NULL ELSE DATEDIFF(day, compra.fecha, pagos.FechaMax) - tiemposAuditoria.PlazoPagoFacturaDias END AS 'Atraso dias Pago', 
                      Compra.Otro AS 'Comprobante SP', Compra.Pedido, NovedadImportacion.observacion
				FROM  DetCompra RIGHT OUTER JOIN
                      (
						SELECT idArticulo, Articulo
                        FROM   Articulo
                        WHERE  Articulo LIKE 'IG-%'
                      ) AS Artic ON DetCompra.idArticulo = Artic.idArticulo LEFT OUTER JOIN
                      Compra ON DetCompra.idCompra = Compra.idCompra LEFT OUTER JOIN
                      Cliente ON Compra.idCliente = Cliente.idCliente LEFT OUTER JOIN
                      NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento LEFT OUTER JOIN
                      (
	                    select idCompra,SUM(Pago) as SumaPagos,MIN(pago.Fecha) as FechaMin, MAX(pago.Fecha) as FechaMax, COUNT(Pago.Pago) as CuentaPagos
						from pago where pago.Borrar=0
						group by pago.idCompra
                      ) Pagos on detcompra.idCompra=Pagos.idCompra LEFT OUTER JOIN
                      tiemposAuditoria ON Compra.idCliente = tiemposAuditoria.idClientePE
				WHERE     (Compra.idTipoFactura = 4) AND (Compra.Usuario <> 'SPLotes') AND Compra.idCompra in (" + listaIds + @")
				GROUP BY Compra.idCompra,DetCompra.idDetCompra,DetCompra.idArticulo,Compra.FechaIngreso,Compra.Numero, Cliente.Nombre, Artic.Articulo,CAST(Compra.Notas AS varchar(2000)),
					Compra.Fecha,Compra.FechaVencimiento, Compra.Estacion, Compra.Total, Pagos.SumaPagos, Pagos.FechaMin, Pagos.FechaMax, tiemposAuditoria.PlazoPagoFacturaDias,
					Compra.Otro, Compra.Pedido, NovedadImportacion.observacion
				ORDER BY Compra.idCompra DESC
            ";

            miClase.LlenaGrid(dgv, "detcompra", sqlQuery);
            estilo(dgv);
            return 0;
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
                SELECT  Top(150) Compra.idCompra, detcompra.idDetCompra, detcompra.idarticulo, Compra.FechaIngreso AS 'Fecha Ingreso', 
		                Compra.Numero as 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', Articulo.Articulo, 
		                SUBSTRING(Articulo.Articulo, 1,6)+'-2'+SUBSTRING(Articulo.Articulo, 8,10) as 'IG',
		                CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', 
		                Compra.Estacion, Compra.Total AS 'Valor Factura', 
		                SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) AS 'Valor cargado a la IG',
		                datediff(DAY, compra.Fecha, compra.FechaIngreso) as'Dias Ingreso de Factura', 
		                sum(pago.pago) as 'Suma Pagos', MIN(pago.fecha) as 'Fecha Primer Pago', MAX(pago.fecha) as 'Fecha Último Pago',
		                tiemposAuditoria.PlazoPagoFacturaDias as 'Plazo Días Pago Facturas',
		                case when sum(pago.Pago) is null then null else DATEDIFF(day, compra.fecha, max(pago.fecha)) end as 'Dias Pago Factura',
		                case when sum(pago.Pago) is null OR tiemposAuditoria.PlazoPagoFacturaDias IS null then 
							null 
						else 
							DATEDIFF(day, compra.fecha, max(pago.fecha))-tiemposAuditoria.PlazoPagoFacturaDias
						end as 'Atraso dias Pago',
		                Compra.Otro as 'Comprobante SP', Compra.Pedido, NovedadImportacion.Observacion
                FROM    DetCompra 
		                INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
		                INNER JOIN Cliente on Compra.idCliente=Cliente.idCliente
		                INNER JOIN Articulo on detcompra.idArticulo=Articulo.idArticulo
		                LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
		                LEFT OUTER JOIN Pago on detcompra.idCompra=pago.idCompra
		                LEFT OUTER JOIN tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
		                AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and compra.fecha>='" + desde + "' and compra.fecha<='" + hasta + @"' 
                GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, Cliente.Nombre, 
		                CAST(Compra.Notas AS varchar(2000)), 
		                Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Otro, Compra.Pedido,
		                NovedadImportacion.Observacion, Articulo.Articulo,detcompra.idarticulo,tiemposAuditoria.PlazoPagoFacturaDias
                ORDER BY Compra.idCompra desc
            ";
             * */
            sqlQuery = @"
                SELECT     Compra.idCompra, DetCompra.idDetCompra, DetCompra.idArticulo, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Numero AS 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', 
                      Artic.Articulo, SUBSTRING(Artic.Articulo, 1, 6) + '-2' + SUBSTRING(Artic.Articulo, 8, 10) AS 'IG', CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, 
                      Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Estacion, Compra.Total AS 'Valor Factura', SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) 
                      AS 'Valor cargado a la IG', DATEDIFF(DAY, Compra.Fecha, Compra.FechaIngreso) AS 'Dias Ingreso de Factura', Pagos.SumaPagos AS 'Suma Pagos', pagos.FechaMin AS 'Fecha Primer Pago', 
                      pagos.FechaMax AS 'Fecha Último Pago', tiemposAuditoria.PlazoPagoFacturaDias AS 'Plazo Días Pago Facturas', CASE WHEN pagos.SumaPagos IS NULL THEN NULL ELSE DATEDIFF(day, 
                      compra.fecha, Pagos.FechaMax) END AS 'Dias Pago Factura', CASE WHEN Pagos.SumaPagos IS NULL OR
                      tiemposAuditoria.PlazoPagoFacturaDias IS NULL THEN NULL ELSE DATEDIFF(day, compra.fecha, pagos.FechaMax) - tiemposAuditoria.PlazoPagoFacturaDias END AS 'Atraso dias Pago', 
                      Compra.Otro AS 'Comprobante SP', Compra.Pedido, NovedadImportacion.observacion
				FROM  DetCompra RIGHT OUTER JOIN
                      (
						SELECT idArticulo, Articulo
                        FROM   Articulo
                        WHERE  Articulo LIKE 'IG-%'
                      ) AS Artic ON DetCompra.idArticulo = Artic.idArticulo LEFT OUTER JOIN
                      Compra ON DetCompra.idCompra = Compra.idCompra LEFT OUTER JOIN
                      Cliente ON Compra.idCliente = Cliente.idCliente LEFT OUTER JOIN
                      NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento LEFT OUTER JOIN
                      (
	                    select idCompra,SUM(Pago) as SumaPagos,MIN(pago.Fecha) as FechaMin, MAX(pago.Fecha) as FechaMax, COUNT(Pago.Pago) as CuentaPagos
						from pago where pago.Borrar=0
						group by pago.idCompra
                      ) Pagos on detcompra.idCompra=Pagos.idCompra LEFT OUTER JOIN
                      tiemposAuditoria ON Compra.idCliente = tiemposAuditoria.idClientePE
				WHERE     (Compra.idTipoFactura = 4) AND (Compra.Usuario <> 'SPLotes') AND compra.fecha>='" + desde + "' and compra.fecha<='" + hasta + @"' 
				GROUP BY Compra.idCompra,DetCompra.idDetCompra,DetCompra.idArticulo,Compra.FechaIngreso,Compra.Numero, Cliente.Nombre, Artic.Articulo,CAST(Compra.Notas AS varchar(2000)),
					Compra.Fecha,Compra.FechaVencimiento, Compra.Estacion, Compra.Total, Pagos.SumaPagos, Pagos.FechaMin, Pagos.FechaMax, tiemposAuditoria.PlazoPagoFacturaDias,
					Compra.Otro, Compra.Pedido, NovedadImportacion.observacion
				ORDER BY Compra.idCompra DESC
            ";

            miClase.LlenaGrid(dgv, "detcompra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public int llenaGrid2(System.Windows.Forms.DataGridView dgv, int idArticulo)
        {
            // Muestra todos los registros asociados a una IG en el detcompra relacionándolos con el idArtículo
            if (dgv == null || idArticulo < 1)
                return -1;
            dgv.Columns.Clear();

            /*
            sqlQuery = @"
                SELECT  Top(150) Compra.idCompra, detcompra.idDetCompra, detcompra.idarticulo, Compra.FechaIngreso AS 'Fecha Ingreso', 
		                Compra.Numero as 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', Articulo.Articulo, 
		                SUBSTRING(Articulo.Articulo, 1,6)+'-2'+SUBSTRING(Articulo.Articulo, 8,10) as 'IG',
		                CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', 
		                Compra.Estacion, Compra.Total AS 'Valor Factura', 
		                SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) AS 'Valor cargado a la IG',
		                datediff(DAY, compra.Fecha, compra.FechaIngreso) as'Dias Ingreso de Factura', 
		                sum(pago.pago) as 'Suma Pagos', MIN(pago.fecha) as 'Fecha Primer Pago', MAX(pago.fecha) as 'Fecha Último Pago',
		                tiemposAuditoria.PlazoPagoFacturaDias as 'Plazo Días Pago Facturas',
		                case when sum(pago.Pago) is null then null else DATEDIFF(day, compra.fecha, max(pago.fecha)) end as 'Dias Pago Factura',
		                case when sum(pago.Pago) is null OR tiemposAuditoria.PlazoPagoFacturaDias IS null then 
							null 
						else 
							DATEDIFF(day, compra.fecha, max(pago.fecha))-tiemposAuditoria.PlazoPagoFacturaDias
						end as 'Atraso dias Pago',
		                Compra.Otro as 'Comprobante SP', Compra.Pedido, NovedadImportacion.Observacion
                FROM    DetCompra 
		                INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
		                INNER JOIN Cliente on Compra.idCliente=Cliente.idCliente
		                INNER JOIN Articulo on detcompra.idArticulo=Articulo.idArticulo
		                LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
		                LEFT OUTER JOIN Pago on detcompra.idCompra=pago.idCompra
		                LEFT OUTER JOIN tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
		                AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and detcompra.idArticulo=" + idArticulo + @" 
                GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, Cliente.Nombre, 
		                CAST(Compra.Notas AS varchar(2000)), 
		                Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Otro, Compra.Pedido,
		                NovedadImportacion.Observacion, Articulo.Articulo,detcompra.idarticulo,tiemposAuditoria.PlazoPagoFacturaDias
                ORDER BY Compra.idCompra desc
            ";
             * */

            sqlQuery = @"
                SELECT     Compra.idCompra, DetCompra.idDetCompra, DetCompra.idArticulo, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Numero AS 'Nro. Factura', Cliente.Nombre AS 'Proveedor en Factura', 
                      Artic.Articulo, SUBSTRING(Artic.Articulo, 1, 6) + '-2' + SUBSTRING(Artic.Articulo, 8, 10) AS 'IG', CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, 
                      Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Estacion, Compra.Total AS 'Valor Factura', SUM(DetCompra.Precio) + SUM(DetCompra.Precio * DetCompra.Impuesto / 100) 
                      AS 'Valor cargado a la IG', DATEDIFF(DAY, Compra.Fecha, Compra.FechaIngreso) AS 'Dias Ingreso de Factura', Pagos.SumaPagos AS 'Suma Pagos', pagos.FechaMin AS 'Fecha Primer Pago', 
                      pagos.FechaMax AS 'Fecha Último Pago', tiemposAuditoria.PlazoPagoFacturaDias AS 'Plazo Días Pago Facturas', CASE WHEN pagos.SumaPagos IS NULL THEN NULL ELSE DATEDIFF(day, 
                      compra.fecha, Pagos.FechaMax) END AS 'Dias Pago Factura', CASE WHEN Pagos.SumaPagos IS NULL OR
                      tiemposAuditoria.PlazoPagoFacturaDias IS NULL THEN NULL ELSE DATEDIFF(day, compra.fecha, pagos.FechaMax) - tiemposAuditoria.PlazoPagoFacturaDias END AS 'Atraso dias Pago', 
                      Compra.Otro AS 'Comprobante SP', Compra.Pedido, NovedadImportacion.observacion
				FROM  DetCompra RIGHT OUTER JOIN
                      (
						SELECT idArticulo, Articulo
                        FROM   Articulo
                        WHERE  Articulo LIKE 'IG-%'
                      ) AS Artic ON DetCompra.idArticulo = Artic.idArticulo LEFT OUTER JOIN
                      Compra ON DetCompra.idCompra = Compra.idCompra LEFT OUTER JOIN
                      Cliente ON Compra.idCliente = Cliente.idCliente LEFT OUTER JOIN
                      NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento LEFT OUTER JOIN
                      (
	                    select idCompra,SUM(Pago) as SumaPagos,MIN(pago.Fecha) as FechaMin, MAX(pago.Fecha) as FechaMax, COUNT(Pago.Pago) as CuentaPagos
						from pago where pago.Borrar=0
						group by pago.idCompra
                      ) Pagos on detcompra.idCompra=Pagos.idCompra LEFT OUTER JOIN
                      tiemposAuditoria ON Compra.idCliente = tiemposAuditoria.idClientePE
				WHERE     (Compra.idTipoFactura = 4) AND (Compra.Usuario <> 'SPLotes') AND detcompra.idArticulo=" + idArticulo + @" 
				GROUP BY Compra.idCompra,DetCompra.idDetCompra,DetCompra.idArticulo,Compra.FechaIngreso,Compra.Numero, Cliente.Nombre, Artic.Articulo,CAST(Compra.Notas AS varchar(2000)),
					Compra.Fecha,Compra.FechaVencimiento, Compra.Estacion, Compra.Total, Pagos.SumaPagos, Pagos.FechaMin, Pagos.FechaMax, tiemposAuditoria.PlazoPagoFacturaDias,
					Compra.Otro, Compra.Pedido, NovedadImportacion.observacion
				ORDER BY Compra.idCompra DESC
            ";
            miClase.LlenaGrid(dgv, "detcompra", sqlQuery);
            estilo(dgv);
            return 0;
        }





        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            if (dgv != null)
            {
                // Facturas Normales Proveedor.
                dgv.Columns[0].Name = "idCompra";
                dgv.Columns[0].Visible = false; // idCompra
                dgv.Columns[1].Name = "idDetCompra";
                dgv.Columns[1].Visible = false; // idDetCompra
                dgv.Columns[2].Name = "idArticulo";
                dgv.Columns[2].Visible = false; // idArticulo
                dgv.Columns[3].Width = 100; // Fecha Ingreso
                //dgv.Columns[3].DefaultCellStyle.Format = "d";
                dgv.Columns[4].Width = 80; // Numero Factura
                dgv.Columns[5].Width = 150; // Proveedor
                dgv.Columns[6].Width = 80; // Artículo
                dgv.Columns[7].Width = 80; // IG
                dgv.Columns[8].Width = 200; // Notas
                dgv.Columns[9].Name = "compraFecha";
                dgv.Columns[9].Width = 80; // Fecha
                //dgv.Columns[9].DefaultCellStyle.Format = "d";
                dgv.Columns[10].Width = 100; // Fecha Vencimiento
                //dgv.Columns[10].DefaultCellStyle.Format = "d";
                dgv.Columns[11].Width = 100; // Estacion
                dgv.Columns[12].Width = 100; // Valor Factura
                dgv.Columns[12].DefaultCellStyle.Format = "F";
                dgv.Columns[13].Width = 100; // Valor Cargado a la IG
                dgv.Columns[13].DefaultCellStyle.Format = "F";
                dgv.Columns[14].Name = "diasIngresoFactura";
                dgv.Columns[14].Width = 100; // Dias Ingreso Factura
                dgv.Columns[15].Width = 80; // Suma Pagos
                dgv.Columns[15].DefaultCellStyle.Format = "F";
                dgv.Columns[16].Width = 100; // Fecha Primer Pago
                //dgv.Columns[16].DefaultCellStyle.Format = "d";
                dgv.Columns[17].Width = 100; // Fecha Ultimo Pago Pago
                //dgv.Columns[17].DefaultCellStyle.Format = "d";
                dgv.Columns[18].Name = "PlazoPagoFact";
                dgv.Columns[18].Width = 120;// Plazo pago en días de factura
                dgv.Columns[19].Name = "diasPagoFactura";
                dgv.Columns[19].Width = 100; // Dias Pago Factura
                dgv.Columns[20].Name = "atrasoDiasPago";
                dgv.Columns[20].Width = 80;
                dgv.Columns[21].Width = 80; // ComprobanteSP
                dgv.Columns[22].Width = 80; // Pedido
                dgv.Columns[23].Width = 150; // Observación
            }
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // No tiene implementación porque ya se incluye implícitamente en la bdd.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // dgv.Columns[14].Name = "diasIngresoFactura";
            string dato = "";
            int nroDias = 0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasIngresoFactura", i].Value.ToString(); // Aquí se produce la excepción.
                }
                catch (System.NullReferenceException)
                {
                    error++;
                }
                if (error == 0)
                {
                    if (dato.Length > 0)
                    {
                        // Hay data.
                        nroDias = Int32.Parse(dato);
                        if (nroDias > 4)
                        {
                            dgv["diasIngresoFactura", i].Style.BackColor = Color.Red;
                            dgv["diasIngresoFactura", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias <5 )
                            {
                                dgv["diasIngresoFactura", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasIngresoFactura", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    /*
                     * no se implementa el caso contrario porque si la factura existe es porque tengo dos fechas, la de ingreso y la de la factura.
                     * */
                }
            }

            // dgv.Columns[20].Name = "atrasoDiasPago";
            dato = "";
            error = nroDias = 0;
            int plazo=0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["atrasoDiasPago", i].Value.ToString(); // Aquí se produce la excepción.
                }
                catch (System.NullReferenceException)
                {
                    error++;
                }
                if (error == 0)
                {
                    if (dato.Length > 0)
                    {
                        // Hay data.
                        nroDias = Int32.Parse(dato);
                        if (nroDias > 2)
                        {
                            dgv["atrasoDiasPago", i].Style.BackColor = Color.Red;
                            dgv["atrasoDiasPago", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias < 3)
                            {
                                dgv["atrasoDiasPago", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["atrasoDiasPago", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    else
                    {
                        // Si noy hay fecha del primer pago entonces hay que ver si a la fecha actual ya es hora de emitir uno.
                        // Teniendo como referencia los plazos para pagos del proveedor en la tabla tiemposAuditoria dgv.Columns[18].Name = "PlazoPagoFact";
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Parse(dgv["compraFecha", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        error = 0;
                        try
                        {
                            plazo = Int32.Parse(dgv["PlazoPagoFact", i].Value.ToString());
                        }
                        catch(Exception)
                        {
                            error++;
                        }
                        if (error == 0)
                        {
                            if (tiempo.Days > plazo)
                            {
                                // Ya es hora de emitir un anticipo.
                                dgv["atrasoDiasPago", i].Style.BackColor = Color.Orange;
                            }
                        }
                    }
                }
                
            }
        }
    }
}
