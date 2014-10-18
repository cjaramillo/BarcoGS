using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosPP : MostrarData
    {
        public int up_IBG (int idCompraPP)
        {
            //Devuelve el idcompra del IBG asociado al PP que recibe.
            if (idCompraPP < 1)
                return -1;
            sqlQuery = @"
                select count(idcompra) from compra where idtipofactura=9 and comprobante in 
                (
                    select numero from compra where idtipofactura=14 and borrar=0 and idcompra="+idCompraPP+@"
                )
            ";
            if (miClase.EjecutaEscalar(sqlQuery) == 0)
            {
                return -1;
            }
            sqlQuery = @"
                select top(1)idcompra from compra where idtipofactura=9 and comprobante in 
                (
                    select top(1)numero from compra where idtipofactura=14 and borrar=0 and idcompra=" + idCompraPP + @"
                )
            ";
            int retorna=miClase.EjecutaEscalar(sqlQuery);
            if (retorna > 0)
                return retorna;
            return -1;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns.Clear();
            if (dgv==null)
            {
                return -1;
            }
            sqlQuery = @"
                select compra.idCompra as idCompraPP, OC.idCompra as idCompraOC,IBG.idCompra as idCompraIBG, compra.Numero, 
					Cliente.Nombre as 'Proveedor',compra.Estacion, compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, 
					compra.fechavencimiento as 'Fecha Vencimiento', Total, compra.Notas, compra.Comprobante, 
					isnull(tiemposAuditoria.plazoTransporteDias,-1) as 'Plazo para IBG',
					case when ibg.idCompra is not null then ibg.fechaIngreso else null end as 'Fecha IBG', 
					case when ibg.idCompra is not null then datediff(day, compra.fechaIngreso, IBG.FechaIngreso) end as 'Dias que se demoró para ser IBG',
					case 
						when ibg.idCompra is not null then 
							datediff(day, compra.fechaIngreso, IBG.FechaIngreso)-tiemposAuditoria.plazoTransporteDias 
						else 
							DATEDIFF(day, compra.fechaIngreso, GETDATE())
						end 
						as 'Retraso Transformación IBG',
					NovedadImportacion.Observacion
                from Compra 
	                inner join Cliente on compra.idCliente=Cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
		                (
			                select idcompra, numero from Compra where idTipoFactura=2 and Borrar=0
		                ) OC on compra.Numero=OC.Numero
		            left outer join 
						(
							select compra.idCompra as idCompra, compra.Comprobante, compra.numero, compra.FechaIngreso
							from Compra 
							where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
						) IBG on Compra.Numero=IBG.Comprobante
					left outer join tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                where compra.idTipoFactura=14 and compra.borrar=0
                order by Compra.idCompra desc
            ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
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
                select compra.idCompra as idCompraPP, OC.idCompra as idCompraOC,IBG.idCompra as idCompraIBG, compra.Numero, 
					Cliente.Nombre as 'Proveedor',compra.Estacion, compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, 
					compra.fechavencimiento as 'Fecha Vencimiento', Total, compra.Notas, compra.Comprobante, 
					isnull(tiemposAuditoria.plazoTransporteDias,-1) as 'Plazo para IBG',
					case when ibg.idCompra is not null then ibg.fechaIngreso else null end as 'Fecha IBG', 
					case when ibg.idCompra is not null then datediff(day, compra.fechaIngreso, IBG.FechaIngreso) end as 'Dias que se demoró para ser IBG',
					case 
						when ibg.idCompra is not null then 
							datediff(day, compra.fechaIngreso, IBG.FechaIngreso)-tiemposAuditoria.plazoTransporteDias 
						else 
							DATEDIFF(day, compra.fechaIngreso, GETDATE())
						end 
						as 'Retraso Transformación IBG',
					NovedadImportacion.Observacion
                from Compra 
	                inner join Cliente on compra.idCliente=Cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
		                (
			                select idcompra, numero from Compra where idTipoFactura=2 and Borrar=0
		                ) OC on compra.Numero=OC.Numero
		            left outer join 
						(
							select compra.idCompra as idCompra, compra.Comprobante, compra.numero, compra.FechaIngreso
							from Compra 
							where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
						) IBG on Compra.Numero=IBG.Comprobante
					left outer join tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                where compra.idTipoFactura=14 and compra.borrar=0 and compra.idcompra=" + idRegistro + @"
                order by Compra.idCompra desc";
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
                    select compra.idCompra as idCompraPP, OC.idCompra as idCompraOC,IBG.idCompra as idCompraIBG, compra.Numero, 
					Cliente.Nombre as 'Proveedor',compra.Estacion, compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, 
					compra.fechavencimiento as 'Fecha Vencimiento', Total, compra.Notas, compra.Comprobante, 
					isnull(tiemposAuditoria.plazoTransporteDias,-1) as 'Plazo para IBG',
					case when ibg.idCompra is not null then ibg.fechaIngreso else null end as 'Fecha IBG', 
					case when ibg.idCompra is not null then datediff(day, compra.fechaIngreso, IBG.FechaIngreso) end as 'Dias que se demoró para ser IBG',
					case 
						when ibg.idCompra is not null then 
							datediff(day, compra.fechaIngreso, IBG.FechaIngreso)-tiemposAuditoria.plazoTransporteDias 
						else 
							DATEDIFF(day, compra.fechaIngreso, GETDATE())
						end 
						as 'Retraso Transformación IBG',
					NovedadImportacion.Observacion
                from Compra 
	                inner join Cliente on compra.idCliente=Cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
		                (
			                select idcompra, numero from Compra where idTipoFactura=2 and Borrar=0
		                ) OC on compra.Numero=OC.Numero
		            left outer join 
						(
							select compra.idCompra as idCompra, compra.Comprobante, compra.numero, compra.FechaIngreso
							from Compra 
							where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
						) IBG on Compra.Numero=IBG.Comprobante
					left outer join tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                where compra.idTipoFactura=14 and compra.borrar=0 and compra.idcompra in (" + listaIds+@") 
                order by Compra.idCompra desc";
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
                select compra.idCompra as idCompraPP, OC.idCompra as idCompraOC,IBG.idCompra as idCompraIBG, compra.Numero, 
					Cliente.Nombre as 'Proveedor',compra.Estacion, compra.FechaIngreso as 'Fecha Ingreso', compra.Fecha, 
					compra.fechavencimiento as 'Fecha Vencimiento', Total, compra.Notas, compra.Comprobante, 
					isnull(tiemposAuditoria.plazoTransporteDias,-1) as 'Plazo para IBG',
					case when ibg.idCompra is not null then ibg.fechaIngreso else null end as 'Fecha IBG', 
					case when ibg.idCompra is not null then datediff(day, compra.fechaIngreso, IBG.FechaIngreso) end as 'Dias que se demoró para ser IBG',
					case 
						when ibg.idCompra is not null then 
							datediff(day, compra.fechaIngreso, IBG.FechaIngreso)-tiemposAuditoria.plazoTransporteDias 
						else 
							DATEDIFF(day, compra.fechaIngreso, GETDATE())
						end 
						as 'Retraso Transformación IBG',
					NovedadImportacion.Observacion
                from Compra 
	                inner join Cliente on compra.idCliente=Cliente.idCliente
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
	                left outer join 
		                (
			                select idcompra, numero from Compra where idTipoFactura=2 and Borrar=0
		                ) OC on compra.Numero=OC.Numero
		            left outer join 
						(
							select compra.idCompra as idCompra, compra.Comprobante, compra.numero, compra.FechaIngreso
							from Compra 
							where compra.idTipoFactura=9 and compra.idSubProyecto=1 and compra.Borrar=0
						) IBG on Compra.Numero=IBG.Comprobante
					left outer join tiemposAuditoria on compra.idCliente=tiemposAuditoria.idClientePE
                where compra.idTipoFactura=14 and compra.borrar=0 and compra.fecha>='" + desde + "' and compra.fecha<='" + hasta + @"' 
                order by Compra.idCompra desc ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Pedidos Proveedor.
            dgv.Columns[0].Name = "idCompraPP";
            dgv.Columns[0].Visible = false; // idCompraPP va oculto
            dgv.Columns[1].Name = "idCompraOC";
            dgv.Columns[1].Visible = false; // idCompraOC va oculto
            dgv.Columns[2].Name = "idCompraIBG";
            dgv.Columns[2].Visible = false; // idCompraIBG va oculto
            dgv.Columns[3].Width = 80; // Numero
            dgv.Columns[4].Width = 150; // Nombre
            dgv.Columns[5].Width = 80; // Estación
            dgv.Columns[6].Width = 100; // Fecha Ingreso
            dgv.Columns[7].Width = 100; // Fecha
            dgv.Columns[8].Width = 100; // Fecha Vencimiento
            dgv.Columns[9].Width = 80; // Total
            dgv.Columns[9].DefaultCellStyle.Format = "F";
            dgv.Columns[10].Width = 150; // Notas
            dgv.Columns[11].Width = 100; // Comprobante
            dgv.Columns[12].Width = 80;  // Plazo en dias para IBG
            dgv.Columns[13].Width = 100; // Fecha IBG
            dgv.Columns[14].Width = 80; // Dias que se demoró para ser IBG
            dgv.Columns[15].Name = "diasRetrasoTransforIBG";
            dgv.Columns[15].Width = 80; // Retraso transformación IBG
            dgv.Columns[16].Width = 200; // Observación.
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // contenido en la sentencia sql

        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // dgv.Columns[15].Name = "diasRetrasoTransforIBG"; Si es mayor a cero se pinta.
            string dato = "";
            int nroDias = 0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasRetrasoTransforIBG", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias > 0)
                        {
                            dgv["diasRetrasoTransforIBG", i].Style.BackColor = Color.Red;
                            dgv["diasRetrasoTransforIBG", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias <= 0)
                            {
                                dgv["diasRetrasoTransforIBG", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasRetrasoTransforIBG", i].Style.ForeColor = Color.White;
                            }
                            // No hay else.. los valores negativos se evaluan en el if
                        }
                    }
                    // Aquí el else evalua las celdas cuyo valor es null. Voy a pintarles de naranja como precaución
                    else
                    {
                        dgv["diasRetrasoTransforIBG", i].Style.BackColor = Color.Orange; // Precaución. Tiempo no definido.
                    }
                }
            }
        }
    }
}
