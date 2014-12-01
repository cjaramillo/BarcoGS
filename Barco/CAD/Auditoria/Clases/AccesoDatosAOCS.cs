using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosAOCS: MostrarData
    {
        
        
        // Capa de acceso a datos para facturas de anticipos desde ordenes de compra simples.
        /*
         * Advertencia: 20140520:
         * El día de hoy se realizó una corrección en la senetencia SQL de esta capa, en esta corrección se pudo detectar la necesidad de emplear
         * una tabla que registre todos los anticipos solicitados desde OCS, misma que se llama ControlAnticiposOCS. Desde la presente se encuentra 
         * operando y registrando datos de todos los anticipos, pero aún no se emplea este mecanismo en la presente capa.
         * Se recomienda una vez que las IG que se han creado del 20140520 hacia atrás hayan sido liquidadas y procesadas se realice la migración
         * de sentencia SQL.
         * */


        public int up_SP(int idRegistro)
        {
            // Devuelve el idCompra del SP asociado a esta compra
            sqlQuery=@"select idcompra from compra where idtipofactura=26 and borrar=0 and numero in (select top(1)otro from compra where idtipofactura=4 and borrar=0 and idcompra="+idRegistro+")";
            int resultado=0;
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

            sqlQuery = @"
            select tabla.* from (
				-- Esta consulta funciona perfectamente con los anticipos de las OCS que aun existen (osea las que aún no han sido consolidadas en su totalidad).
				Select compra.idcompra, OCS.idCompra as idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre as Proveedor, compra.Total, cast (compra.Notas as varchar(4000)) as Notas, 
	                compra.pedido, compra.otro as 'Comprobante SP', OCS.FechaRevision as 'Aprueba Cotización',
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso) as 'Dias para solicitar anticipo',
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
	                MIN(pago.fecha) as 'Fecha Primer Pago',
	                DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
	                NI.observacion as 'Observación'
                from compra 
	                inner join cliente on compra.idcliente=cliente.idcliente 
	                inner join (
		                select idcompra, Departamento, FechaRevision 
		                from Compra 
		                where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
	                )OCS on Compra.Otro=OCS.Departamento
	                inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	                left outer join Pago on compra.idCompra=pago.idCompra
	                left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4
                group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, OCS.FechaRevision,
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
                -- Acá iría un UNION con los anticipos de las OCS que ya no existen porque fueron absorbidas pero están registradas en 
                union 
                SELECT  compra.idcompra, caocs.idCompraOCS, compra.FechaIngreso, compra.Estacion, compra.Numero, compra.Fecha, Cliente.Nombre as Proveedor,
						Compra.Total,cast (compra.Notas as varchar(4000)) as Notas, compra.Pedido, compra.otro as 'Comprobante SP', caocs.fechaRevisionOCS as 'Aprueba Cotización',
						DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso) as 'Dias para solicitar anticipo',
						DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
						MIN(pago.fecha) as 'Fecha Primer Pago',
						DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
						NI.observacion as 'Observación'
                from Compra
                right outer join 
					(select idCompraAnticipo, idCompraOCS, idCompraSP, nroOCS, fechaRevisionOCS from ControlAnticiposOCS) caocs on compra.Mensaje2=caocs.nroOCS
				inner join Cliente on compra.idCliente=Cliente.idCliente
				inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	            left outer join Pago on compra.idCompra=pago.idCompra
	            left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4 
					and compra.idCompra not in (-- Omitir las facturas de anticipos que ya se agregaron en la primera parte del UNION.
						Select compra.idcompra
						from compra 
							inner join cliente on compra.idcliente=cliente.idcliente 
							inner join (
								select idcompra, Departamento, FechaRevision 
								from Compra 
								where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
							)OCS on Compra.Otro=OCS.Departamento
							inner join 
							(
								select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
							)SP on compra.Otro=SP.Numero
							left outer join Pago on compra.idCompra=pago.idCompra
							left outer join (
								select idDocumento, observacion 
								from NovedadImportacion 
								where idTipoFactura=1 and idGrid=4 and borrar=0
							)NI on Compra.idCompra=NI.idDocumento
						where compra.idTipoFactura=4
						group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
							cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
							compra.pedido, compra.otro, OCS.FechaRevision,
							DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
							DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
							NI.observacion
					)
                group by compra.idcompra, caocs.idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, caocs.fechaRevisionOCS,
	                DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
			) tabla ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "tabla", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            // Solo muestra un registro. Recibe como parámetro el número de la OCS, como salida muestra todas las facturas de anticipo asociadas a esa OCS.
            if (dgv == null || idRegistro == 0 )
            {
                return -1;
            }
            sqlQuery = @"
            select count(*) from (
				-- Esta consulta funciona perfectamente con los anticipos de las OCS que aun existen (osea las que aún no han sido consolidadas en su totalidad).
				Select compra.idcompra, OCS.idCompra as idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre as Proveedor, compra.Total, cast (compra.Notas as varchar(4000)) as Notas, 
	                compra.pedido, compra.otro as 'Comprobante SP', OCS.FechaRevision as 'Aprueba Cotización',
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso) as 'Dias para solicitar anticipo',
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
	                MIN(pago.fecha) as 'Fecha Primer Pago',
	                DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
	                NI.observacion as 'Observación'
                from compra 
	                inner join cliente on compra.idcliente=cliente.idcliente 
	                inner join (
		                select idcompra, Departamento, FechaRevision 
		                from Compra 
		                where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
	                )OCS on Compra.Otro=OCS.Departamento
	                inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	                left outer join Pago on compra.idCompra=pago.idCompra
	                left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4
                group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, OCS.FechaRevision,
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
                -- Acá iría un UNION con los anticipos de las OCS que ya no existen porque fueron absorbidas pero están registradas en 
                union 
                SELECT  compra.idcompra, caocs.idCompraOCS, compra.FechaIngreso, compra.Estacion, compra.Numero, compra.Fecha, Cliente.Nombre as Proveedor,
						Compra.Total,cast (compra.Notas as varchar(4000)) as Notas, compra.Pedido, compra.otro as 'Comprobante SP', caocs.fechaRevisionOCS as 'Aprueba Cotización',
						DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso) as 'Dias para solicitar anticipo',
						DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
						MIN(pago.fecha) as 'Fecha Primer Pago',
						DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
						NI.observacion as 'Observación'
                from Compra
                right outer join 
					(select idCompraAnticipo, idCompraOCS, idCompraSP, nroOCS, fechaRevisionOCS from ControlAnticiposOCS) caocs on compra.Mensaje2=caocs.nroOCS
				inner join Cliente on compra.idCliente=Cliente.idCliente
				inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	            left outer join Pago on compra.idCompra=pago.idCompra
	            left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4 
					and compra.idCompra not in (-- Omitir las facturas de anticipos que ya se agregaron en la primera parte del UNION.
						Select compra.idcompra
						from compra 
							inner join cliente on compra.idcliente=cliente.idcliente 
							inner join (
								select idcompra, Departamento, FechaRevision 
								from Compra 
								where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
							)OCS on Compra.Otro=OCS.Departamento
							inner join 
							(
								select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
							)SP on compra.Otro=SP.Numero
							left outer join Pago on compra.idCompra=pago.idCompra
							left outer join (
								select idDocumento, observacion 
								from NovedadImportacion 
								where idTipoFactura=1 and idGrid=4 and borrar=0
							)NI on Compra.idCompra=NI.idDocumento
						where compra.idTipoFactura=4
						group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
							cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
							compra.pedido, compra.otro, OCS.FechaRevision,
							DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
							DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
							NI.observacion
					)
                group by compra.idcompra, caocs.idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, caocs.fechaRevisionOCS,
	                DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
			) tabla 
            where tabla.Proveedor not like 'PE %' and tabla.idCompraOCS=" + idRegistro + "  ";

            if (miClase.EjecutaEscalar(sqlQuery) > 0)
            {
                sqlQuery = @"
                    select tabla.* from (
				        -- Esta consulta funciona perfectamente con los anticipos de las OCS que aun existen (osea las que aún no han sido consolidadas en su totalidad).
				        Select compra.idcompra, OCS.idCompra as idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre as Proveedor, compra.Total, cast (compra.Notas as varchar(4000)) as Notas, 
	                        compra.pedido, compra.otro as 'Comprobante SP', OCS.FechaRevision as 'Aprueba Cotización',
	                        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso) as 'Dias para solicitar anticipo',
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
	                        MIN(pago.fecha) as 'Fecha Primer Pago',
	                        DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
	                        NI.observacion as 'Observación'
                        from compra 
	                        inner join cliente on compra.idcliente=cliente.idcliente 
	                        inner join (
		                        select idcompra, Departamento, FechaRevision 
		                        from Compra 
		                        where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
	                        )OCS on Compra.Otro=OCS.Departamento
	                        inner join 
	                        (
		                        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                        )SP on compra.Otro=SP.Numero
	                        left outer join Pago on compra.idCompra=pago.idCompra
	                        left outer join (
		                        select idDocumento, observacion 
		                        from NovedadImportacion 
		                        where idTipoFactura=1 and idGrid=4 and borrar=0
	                        )NI on Compra.idCompra=NI.idDocumento
                        where compra.idTipoFactura=4
                        group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                        compra.pedido, compra.otro, OCS.FechaRevision,
	                        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                        NI.observacion
                        -- Acá iría un UNION con los anticipos de las OCS que ya no existen porque fueron absorbidas pero están registradas en 
                        union 
                        SELECT  compra.idcompra, caocs.idCompraOCS, compra.FechaIngreso, compra.Estacion, compra.Numero, compra.Fecha, Cliente.Nombre as Proveedor,
						        Compra.Total,cast (compra.Notas as varchar(4000)) as Notas, compra.Pedido, compra.otro as 'Comprobante SP', caocs.fechaRevisionOCS as 'Aprueba Cotización',
						        DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso) as 'Dias para solicitar anticipo',
						        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
						        MIN(pago.fecha) as 'Fecha Primer Pago',
						        DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
						        NI.observacion as 'Observación'
                        from Compra
                        right outer join 
					        (select idCompraAnticipo, idCompraOCS, idCompraSP, nroOCS, fechaRevisionOCS from ControlAnticiposOCS) caocs on compra.Mensaje2=caocs.nroOCS
				        inner join Cliente on compra.idCliente=Cliente.idCliente
				        inner join 
	                        (
		                        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                        )SP on compra.Otro=SP.Numero
	                    left outer join Pago on compra.idCompra=pago.idCompra
	                    left outer join (
		                        select idDocumento, observacion 
		                        from NovedadImportacion 
		                        where idTipoFactura=1 and idGrid=4 and borrar=0
	                        )NI on Compra.idCompra=NI.idDocumento
                        where compra.idTipoFactura=4 
					        and compra.idCompra not in (-- Omitir las facturas de anticipos que ya se agregaron en la primera parte del UNION.
						        Select compra.idcompra
						        from compra 
							        inner join cliente on compra.idcliente=cliente.idcliente 
							        inner join (
								        select idcompra, Departamento, FechaRevision 
								        from Compra 
								        where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
							        )OCS on Compra.Otro=OCS.Departamento
							        inner join 
							        (
								        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
							        )SP on compra.Otro=SP.Numero
							        left outer join Pago on compra.idCompra=pago.idCompra
							        left outer join (
								        select idDocumento, observacion 
								        from NovedadImportacion 
								        where idTipoFactura=1 and idGrid=4 and borrar=0
							        )NI on Compra.idCompra=NI.idDocumento
						        where compra.idTipoFactura=4
						        group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
							        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
							        compra.pedido, compra.otro, OCS.FechaRevision,
							        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
							        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
							        NI.observacion
					        )
                        group by compra.idcompra, caocs.idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                        compra.pedido, compra.otro, caocs.fechaRevisionOCS,
	                        DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso),
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                        NI.observacion
			        ) tabla 
                    where tabla.Proveedor not like 'PE %' and tabla.idCompraOCS=" + idRegistro + "  order by tabla.idCompra desc";
                /*
                Antes el where estaba así:
                where tabla.idCompra="+idRegistro+ "  order by tabla.idCompra desc";
                */
                dgv.Columns.Clear();
                miClase.LlenaGrid(dgv, "compra", sqlQuery);
                estilo(dgv);
                return 0;
            }
            return -1; // No hay nada que imprimir.
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
                    // Aqui iría el select con el in
                    sqlQuery = @"
                    select tabla.* from (
				        -- Esta consulta funciona perfectamente con los anticipos de las OCS que aun existen (osea las que aún no han sido consolidadas en su totalidad).
				        Select compra.idcompra, OCS.idCompra as idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre as Proveedor, compra.Total, cast (compra.Notas as varchar(4000)) as Notas, 
	                        compra.pedido, compra.otro as 'Comprobante SP', OCS.FechaRevision as 'Aprueba Cotización',
	                        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso) as 'Dias para solicitar anticipo',
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
	                        MIN(pago.fecha) as 'Fecha Primer Pago',
	                        DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
	                        NI.observacion as 'Observación'
                        from compra 
	                        inner join cliente on compra.idcliente=cliente.idcliente 
	                        inner join (
		                        select idcompra, Departamento, FechaRevision 
		                        from Compra 
		                        where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
	                        )OCS on Compra.Otro=OCS.Departamento
	                        inner join 
	                        (
		                        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                        )SP on compra.Otro=SP.Numero
	                        left outer join Pago on compra.idCompra=pago.idCompra
	                        left outer join (
		                        select idDocumento, observacion 
		                        from NovedadImportacion 
		                        where idTipoFactura=1 and idGrid=4 and borrar=0
	                        )NI on Compra.idCompra=NI.idDocumento
                        where compra.idTipoFactura=4
                        group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                        compra.pedido, compra.otro, OCS.FechaRevision,
	                        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                        NI.observacion
                        -- Acá iría un UNION con los anticipos de las OCS que ya no existen porque fueron absorbidas pero están registradas en 
                        union 
                        SELECT  compra.idcompra, caocs.idCompraOCS, compra.FechaIngreso, compra.Estacion, compra.Numero, compra.Fecha, Cliente.Nombre as Proveedor,
						        Compra.Total,cast (compra.Notas as varchar(4000)) as Notas, compra.Pedido, compra.otro as 'Comprobante SP', caocs.fechaRevisionOCS as 'Aprueba Cotización',
						        DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso) as 'Dias para solicitar anticipo',
						        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
						        MIN(pago.fecha) as 'Fecha Primer Pago',
						        DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
						        NI.observacion as 'Observación'
                        from Compra
                        right outer join 
					        (select idCompraAnticipo, idCompraOCS, idCompraSP, nroOCS, fechaRevisionOCS from ControlAnticiposOCS) caocs on compra.Mensaje2=caocs.nroOCS
				        inner join Cliente on compra.idCliente=Cliente.idCliente
				        inner join 
	                        (
		                        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                        )SP on compra.Otro=SP.Numero
	                    left outer join Pago on compra.idCompra=pago.idCompra
	                    left outer join (
		                        select idDocumento, observacion 
		                        from NovedadImportacion 
		                        where idTipoFactura=1 and idGrid=4 and borrar=0
	                        )NI on Compra.idCompra=NI.idDocumento
                        where compra.idTipoFactura=4 
					        and compra.idCompra not in (-- Omitir las facturas de anticipos que ya se agregaron en la primera parte del UNION.
						        Select compra.idcompra
						        from compra 
							        inner join cliente on compra.idcliente=cliente.idcliente 
							        inner join (
								        select idcompra, Departamento, FechaRevision 
								        from Compra 
								        where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
							        )OCS on Compra.Otro=OCS.Departamento
							        inner join 
							        (
								        select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
							        )SP on compra.Otro=SP.Numero
							        left outer join Pago on compra.idCompra=pago.idCompra
							        left outer join (
								        select idDocumento, observacion 
								        from NovedadImportacion 
								        where idTipoFactura=1 and idGrid=4 and borrar=0
							        )NI on Compra.idCompra=NI.idDocumento
						        where compra.idTipoFactura=4
						        group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
							        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
							        compra.pedido, compra.otro, OCS.FechaRevision,
							        DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
							        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
							        NI.observacion
					        )
                        group by compra.idcompra, caocs.idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                        cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                        compra.pedido, compra.otro, caocs.fechaRevisionOCS,
	                        DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso),
	                        DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                        NI.observacion
			        ) tabla 
                    where tabla.idcompra in (" + listaIds + @") 
                    order by tabla.idCompra desc";
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
            // Muestra registros en un rango comprendido de fechas basándose en compra.fecha.
            if (vDesde == null || vHasta == null || dgv == null)
            {
                return -1;
            }
            string desde = vDesde.AddDays(-1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string hasta = vHasta.AddDays(1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            sqlQuery = @"
                select tabla.* from (
				-- Esta consulta funciona perfectamente con los anticipos de las OCS que aun existen (osea las que aún no han sido consolidadas en su totalidad).
				Select compra.idcompra, OCS.idCompra as idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre as Proveedor, compra.Total, cast (compra.Notas as varchar(4000)) as Notas, 
	                compra.pedido, compra.otro as 'Comprobante SP', OCS.FechaRevision as 'Aprueba Cotización',
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso) as 'Dias para solicitar anticipo',
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
	                MIN(pago.fecha) as 'Fecha Primer Pago',
	                DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
	                NI.observacion as 'Observación'
                from compra 
	                inner join cliente on compra.idcliente=cliente.idcliente 
	                inner join (
		                select idcompra, Departamento, FechaRevision 
		                from Compra 
		                where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
	                )OCS on Compra.Otro=OCS.Departamento
	                inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	                left outer join Pago on compra.idCompra=pago.idCompra
	                left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4
                group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, OCS.FechaRevision,
	                DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
                -- Acá iría un UNION con los anticipos de las OCS que ya no existen porque fueron absorbidas pero están registradas en 
                union 
                SELECT  compra.idcompra, caocs.idCompraOCS, compra.FechaIngreso, compra.Estacion, compra.Numero, compra.Fecha, Cliente.Nombre as Proveedor,
						Compra.Total,cast (compra.Notas as varchar(4000)) as Notas, compra.Pedido, compra.otro as 'Comprobante SP', caocs.fechaRevisionOCS as 'Aprueba Cotización',
						DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso) as 'Dias para solicitar anticipo',
						DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso) as 'Días para Generar SP',
						MIN(pago.fecha) as 'Fecha Primer Pago',
						DATEDIFF(day, compra.fechaIngreso, min(pago.fecha)) as 'Dias para Pago',
						NI.observacion as 'Observación'
                from Compra
                right outer join 
					(select idCompraAnticipo, idCompraOCS, idCompraSP, nroOCS, fechaRevisionOCS from ControlAnticiposOCS) caocs on compra.Mensaje2=caocs.nroOCS
				inner join Cliente on compra.idCliente=Cliente.idCliente
				inner join 
	                (
		                select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
	                )SP on compra.Otro=SP.Numero
	            left outer join Pago on compra.idCompra=pago.idCompra
	            left outer join (
		                select idDocumento, observacion 
		                from NovedadImportacion 
		                where idTipoFactura=1 and idGrid=4 and borrar=0
	                )NI on Compra.idCompra=NI.idDocumento
                where compra.idTipoFactura=4 
					and compra.idCompra not in (-- Omitir las facturas de anticipos que ya se agregaron en la primera parte del UNION.
						Select compra.idcompra
						from compra 
							inner join cliente on compra.idcliente=cliente.idcliente 
							inner join (
								select idcompra, Departamento, FechaRevision 
								from Compra 
								where idTipoFactura=2 and Usuario<>'OrdenLotes' and Departamento is not null 
							)OCS on Compra.Otro=OCS.Departamento
							inner join 
							(
								select numero, FechaIngreso from Compra where idTipoFactura=26 and Borrar=0
							)SP on compra.Otro=SP.Numero
							left outer join Pago on compra.idCompra=pago.idCompra
							left outer join (
								select idDocumento, observacion 
								from NovedadImportacion 
								where idTipoFactura=1 and idGrid=4 and borrar=0
							)NI on Compra.idCompra=NI.idDocumento
						where compra.idTipoFactura=4
						group by compra.idcompra, OCS.idCompra, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
							cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
							compra.pedido, compra.otro, OCS.FechaRevision,
							DATEDIFF(day, ocs.fechaRevision, compra.fechaIngreso),
							DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
							NI.observacion
					)
                group by compra.idcompra, caocs.idCompraOCS, compra.fechaIngreso, compra.estacion, compra.numero, compra.fecha, 
	                cliente.nombre, compra.Total, cast (compra.Notas as varchar(4000)), 
	                compra.pedido, compra.otro, caocs.fechaRevisionOCS,
	                DATEDIFF(day, caocs.fechaRevisionOCS, compra.fechaIngreso),
	                DATEDIFF(day, compra.fechaIngreso, sp.FechaIngreso),
	                NI.observacion
			) tabla 
            where tabla.fecha>='" + desde + "' and tabla.fecha<='" + hasta + @"
            order by tabla.idCompra desc";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "tabla", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns[0].Name = "idCompra";
            dgv.Columns[0].Visible = false; // idCompra va oculto.
            dgv.Columns[1].Name = "idCompraOCS";
            dgv.Columns[1].Visible = false; // idCompra  de la orden de compra simple que dio origen a esta factura de anticipo va oculto
            dgv.Columns[2].Name = "fechaIngreso";
            dgv.Columns[2].Width = 100; // FechaIngreso
            dgv.Columns[3].Width = 80; // Estación
            dgv.Columns[4].Name = "nroFactAOCS";
            dgv.Columns[4].Width = 80; // Número
            dgv.Columns[5].Width = 100; // Fecha
            dgv.Columns[6].Width = 120; // Proveedor
            dgv.Columns[7].DefaultCellStyle.Format = "F";
            dgv.Columns[7].Width = 80; // Total
            dgv.Columns[8].Width = 200; // Notas
            dgv.Columns[9].Name = "pedidoFactAOCS";
            dgv.Columns[9].Width = 80; // Pedido
            dgv.Columns[10].Name = "comprobanteFactAOCS";
            dgv.Columns[10].Width = 80; // Comprobante
            dgv.Columns[11].Width = 100; // Fecha Aprueba Cotización
            dgv.Columns[12].Name = "diasSolicitudAnticipo";
            dgv.Columns[12].Width = 120;
            dgv.Columns[13].Name = "diasGenerarSP";// Dias para generar SP
            dgv.Columns[13].Width = 100;
            dgv.Columns[14].Width = 100; // Fecha Primer Pago
            dgv.Columns[15].Name = "diasPago";
            dgv.Columns[15].Width = 80; // Dias para pago
            dgv.Columns[16].Width = 200; // Observación
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // No tiene implementación porque en la consulta SQL ya se encuenrtan calculados implícitamente.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // Verificar la columna:  dgv.Columns[12].Name = "diasSolicitudAnticipo"; Si el valor es mayor a uno se pinta.
            string dato = "";
            int nroDias = 0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasSolicitudAnticipo", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias >= 3)
                        {
                            dgv["diasSolicitudAnticipo", i].Style.BackColor = Color.Red;
                            dgv["diasSolicitudAnticipo", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias <= 2)
                            {
                                dgv["diasSolicitudAnticipo", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasSolicitudAnticipo", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    /*
                     * no se implementa el caso contrario porque si tengo nulos significa que en la orden de compra no se puso la fecha de revision, 
                     * que es la fecha en la cual se debió haber aprobado la cotización
                     * la diferencia que se muestra aquí en días es (fecha de revisión)-(fecha de ingreso de factura de anticipo.)
                     * */
                }
            }

            // dgv.Columns[13].Name = "diasGenerarSP";
            dato = "";
            nroDias = 0;
            error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasGenerarSP", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias > 1)
                        {
                            dgv["diasGenerarSP", i].Style.BackColor = Color.Red;
                            dgv["diasGenerarSP", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias == 0 || nroDias == 1)
                            {
                                dgv["diasGenerarSP", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasGenerarSP", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    /*
                     * no se implementa el caso contrario porque técnicamente no se puede crear el sp antes de la factura. por tanto no existirían nulos.
                     * */
                }
            }

            // dgv.Columns[15].Name = "diasPago";
            dato = "";
            nroDias = 0;
            error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasPago", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias > 1)
                        {
                            dgv["diasPago", i].Style.BackColor = Color.Red;
                            dgv["diasPago", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias == 0 || nroDias == 1)
                            {
                                dgv["diasPago", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasPago", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    else
                    {
                        // Si noy hay fecha del primer pago entonces hay que ver si a la fecha actual ya es hora de emitir uno.
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Parse(dgv["fechaIngreso", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        if ((tiempo.Days) > 1)
                        {
                            dgv["diasPago", i].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }
        }
    }
}
