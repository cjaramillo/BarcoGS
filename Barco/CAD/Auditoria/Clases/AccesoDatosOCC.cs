using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosOCC: MostrarData
    {
        /*
         * Capa de acceso a datos para ordenes de compra consolidadas.
         * 
         * */

        public Boolean validaNombreIG(int idCompraOCC)
        {
            sqlQuery = @"select count(*) from compra where idtipofactura=2 and usuario='OrdenLotes' and idcompra=" + idCompraOCC;
            if (miClase.EjecutaEscalar(sqlQuery) < 1)
                return false;
            sqlQuery = "select numero from compra where idtipofactura=2 and usuario='OrdenLotes' and idcompra=" + idCompraOCC;
            string numero = miClase.EjecutaEscalarStr(sqlQuery);
            if (numero.Length != 11)
                return false;
            if ((numero.Substring(0, 3).CompareTo("IG-") != 0) || (numero.Substring(6, 1).CompareTo("-")) != 0)
                return false;
            try
            {
                Int32.Parse(numero.Substring(3, 3));
                Int32.Parse(numero.Substring(7, 4));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public int up_idArticulo(int idOCC)
        {
            if (idOCC < 1 || !validaNombreIG(idOCC) || miClase.EjecutaEscalar("select count(*) from compra where idCompra=" + idOCC) == 0)
                return -1;
            string numero = miClase.EjecutaEscalarStr("Select numero from compra where idcompra=" + idOCC);
            /*
             * IG-008-2014
             * IG-008/014
             * */
            numero = String.Concat(numero.Substring(0, 6) + "/" + numero.Substring(8, 3));
            sqlQuery = "select count(*) from articulo where articulo='" + numero + "' ";
            if (miClase.EjecutaEscalar(sqlQuery) == 0 || miClase.EjecutaEscalar(sqlQuery) >= 2)
                return -1;
            return miClase.EjecutaEscalar("select top(1)idarticulo from articulo where articulo='" + numero + "' ");
        }

        public int[] up_spFacturasNormales(int idArticulo)
        {
            // Devuelve un array del tipo int que contiene los idCompra de las facturas que tienen cargo a un determinado artículo.
            // Este método se llama desde el módulo de auditoría para las facturas normales.
            int[] idCompraSP = new int[100];
            for (int i = 0; i < 100; i++)
                idCompraSP[i] = 0;
            if (idArticulo < 1)
            {
                idCompraSP[0] = -1;
                return idCompraSP;
            }

            /*
            sqlQuery = @"
            select count(*) from Compra where idTipoFactura=26 and Numero in (
					SELECT  Compra.Otro as 'Comprobante SP'
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 and compra.Usuario='SPLotes' and Compra.Otro is not null 
							and detcompra.idArticulo=" + idArticulo + @"
                )
            ";
             * */

            sqlQuery = @"
            select count(distinct compra.idCompra)
            from Compra inner join DetCompra on compra.idCompra=detcompra.idCompra
            where idTipoFactura=26 and detcompra.idArticulo="+idArticulo;

            if (miClase.EjecutaEscalar(sqlQuery) == 0)
            {
                idCompraSP[0] = -1;
                return idCompraSP;
            }
            DataSet ds = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlQuery = @"
                select distinct compra.idcompra
                from Compra inner join DetCompra on compra.idCompra=detcompra.idCompra
                where idTipoFactura=26 and detcompra.idArticulo=" + idArticulo;


            sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
            sqlda.Fill(ds, "compra");
            ds.Tables["compra"].Rows[0].ToString();
            DataRow dr;
            for (int i = 0; i < ds.Tables["compra"].Rows.Count; i++)
            {
                dr = ds.Tables["compra"].Rows[i];
                idCompraSP[i] = Int32.Parse(dr["idcompra"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
            }
            return idCompraSP;
        }

        public int[] up_spFacturasAnticipos(int idCompraOCC)
        {
            int[] idCompraSPAnticipos = new int[100];
            sqlQuery = @"
                SELECT COUNT(distinct idCompraSP)FROM controlAnticiposOCS where idCompraOCS in 
                (
	                select idcompra2 from ControlOCLotes where idCompra=30062865 
                ) 
            ";
            if (miClase.EjecutaEscalar(sqlQuery) > 0)
            {
                sqlQuery = @"
                SELECT distinct idcompraSP FROM controlAnticiposOCS where idCompraOCS in 
                (
	                select idcompra2 from ControlOCLotes where idCompra=30062865 
                ) 
            ";
                DataSet ds = new DataSet();
                SqlDataAdapter sqlda = new SqlDataAdapter();
                sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
                sqlda.Fill(ds, "controlAnticiposOCS");
                ds.Tables["controlAnticiposOCS"].Rows[0].ToString();
                DataRow dr;
                for (int i = 0; i < ds.Tables["controlAnticiposOCS"].Rows.Count; i++)
                {
                    dr = ds.Tables["controlAnticiposOCS"].Rows[i];
                    idCompraSPAnticipos[i] = Int32.Parse(dr["idcompraSP"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
                }
            }
            else
            {
                idCompraSPAnticipos[0] = -1;
            }
            return idCompraSPAnticipos;
        }


        public int up_PP (int idCompraOCC){
            // Devuelve el idCompra del PP asociado a esta OCC. En caso de no existir devuelve -1
            sqlQuery = @"
                Select count(idcompra)
                from compra 
                where idtipofactura=14 and borrar=0 
                    and numero in 
                        (
                            select top(1) numero from compra where idtipofactura=2 and borrar=0 and Compra.Usuario = 'OrdenLotes' and compra.idcompra=" + idCompraOCC + @"
                        )";
            if (miClase.EjecutaEscalar(sqlQuery) < 1)
            {
                return -1;
            }
            sqlQuery = @"
                Select top(1) idcompra 
                from compra 
                where idtipofactura=14 and borrar=0 
                    and numero in 
                        (
                            select top(1) numero from compra where idtipofactura=2 and borrar=0 and Compra.Usuario = 'OrdenLotes' and compra.idcompra="+idCompraOCC+@"
                        )";
            return miClase.EjecutaEscalar(sqlQuery);
        }

        public int[] down_OCS(int idCompraOCC)
        {
            // Devuelve el conjunto de idCompraOCS a partir de las cuales se originó el OCC.
            int[] idCompraOCS = new int[100];
            if (idCompraOCC < 1)
            {
                idCompraOCS[0] = -1;
                return idCompraOCS;
            }
            /*
             * Teóricamente esto debería soportar 6 niveles de anidación.. sin embargo está probado hasta en 4, ha pesar que en análisis de casos de uso me dijeron que a lo mucho
             * tendríamos solo 3 niveles de anidación.
             * */

            sqlQuery = @"
                select count(*) from (
	                select idCompra2, numero from ControlOCLotes where idCompra in (" + idCompraOCC + @")
	                union
	                select idcompra2, numero from ControlOCLotes where idCompra in 
		                (
			                select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
			                union
							select idcompra2 from ControlOCLotes where idCompra in 
								(
									select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
									union
									select idcompra2 from ControlOCLotes where idCompra in 
										(
											select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
											union
											select idcompra2 from ControlOCLotes where idCompra in 
												(
													select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
													union
													select idcompra2 from ControlOCLotes where idCompra in 
														(
															select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
														)
												)
										)
								)
		                )
                )p
            ";
            if (miClase.EjecutaEscalar(sqlQuery) == 0)
            {
                idCompraOCS[0] = -1;
                return idCompraOCS;
            } 
            DataSet ds = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlQuery = @"
                select idCompra2 as idcompra from (
	                select idCompra2, numero from ControlOCLotes where idCompra in (" + idCompraOCC + @")
	                union
	                select idcompra2, numero from ControlOCLotes where idCompra in 
		                (
			                select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
			                union
							select idcompra2 from ControlOCLotes where idCompra in 
								(
									select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
									union
									select idcompra2 from ControlOCLotes where idCompra in 
										(
											select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
											union
											select idcompra2 from ControlOCLotes where idCompra in 
												(
													select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
													union
													select idcompra2 from ControlOCLotes where idCompra in 
														(
															select idCompra2 from ControlOCLotes where idCompra in (" + idCompraOCC + @")
														)
												)
										)
								)
		                )
                )p
                order by idCompra2 asc
            ";
            sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
            sqlda.Fill(ds, "compra");
            ds.Tables["compra"].Rows[0].ToString();
            DataRow dr;
            for (int i = 0; i < ds.Tables["compra"].Rows.Count; i++)
            {
                dr = ds.Tables["compra"].Rows[i];
                idCompraOCS[i] = Int32.Parse(dr["idcompra"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
            }
            return idCompraOCS;
        }


        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            if (dgv == null)
            {
                return -1;
            }
            sqlQuery = @"
                SELECT     Compra.idCompra, PP.idCompra as idCompraPP, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, Compra.Total, Compra.Notas, 
                           Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Pedido,DATEDIFF(day,compra.fechaIngreso, PP.FechaIngreso) as 'Dias desde OCC a PP',
                           observaciones.Observacion
                FROM       Compra INNER JOIN
		                   Cliente ON Compra.idCliente = Cliente.idCliente
		                   left outer join 
				                  (Select idDocumento, idGrid, observacion, estacion, fechaIngreso, borrar FROM NovedadImportacion where idTipoFactura=3) observaciones
				                on compra.idCompra=observaciones.idDocumento
		                   left outer join
				                  (select idCompra, FechaIngreso, Estacion, Numero from Compra where idTipoFactura=14) PP
				                on compra.Numero=PP.Numero
                WHERE     (Compra.idTipoFactura = 2) AND (Compra.Usuario = 'OrdenLotes') and compra.borrar=0 
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            if (dgv == null || idRegistro == 0)
            {
                return -1;
            }
            sqlQuery = @"
             SELECT     Compra.idCompra, PP.idCompra as idCompraPP, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, Compra.Total, Compra.Notas, 
                           Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Pedido,DATEDIFF(day,compra.fechaIngreso, PP.FechaIngreso) as 'Dias desde OCC a PP',
                           observaciones.Observacion
                FROM       Compra INNER JOIN
		                   Cliente ON Compra.idCliente = Cliente.idCliente
		                   left outer join 
				                  (Select idDocumento, idGrid, observacion, estacion, fechaIngreso, borrar FROM NovedadImportacion where idTipoFactura=3) observaciones
				                on compra.idCompra=observaciones.idDocumento
		                   left outer join
				                  (select idCompra, FechaIngreso, Estacion, Numero from Compra where idTipoFactura=14) PP
				                on compra.Numero=PP.Numero
                WHERE     (Compra.idTipoFactura = 2) AND (Compra.Usuario = 'OrdenLotes') and compra.borrar=0 and compra.idcompra="+idRegistro+" ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int[] idRegistro)
        {
            // Muestra un conjunto de registros.
            if (idRegistro[0] == -1 || idRegistro[0] == 0 || dgv == null)
            {
                dgv.Columns.Clear();
                return -1; // El conjunto de datos está vacío
            }
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
                    SELECT     Compra.idCompra, PP.idCompra as idCompraPP, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, Compra.Total, Compra.Notas, 
                                Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Pedido,DATEDIFF(day,compra.fechaIngreso, PP.FechaIngreso) as 'Dias desde OCC a PP',
                                observaciones.Observacion
                    FROM       Compra INNER JOIN
		                        Cliente ON Compra.idCliente = Cliente.idCliente
		                        left outer join 
				                        (Select idDocumento, idGrid, observacion, estacion, fechaIngreso, borrar FROM NovedadImportacion where idTipoFactura=3) observaciones
				                    on compra.idCompra=observaciones.idDocumento
		                        left outer join
				                        (select idCompra, FechaIngreso, Estacion, Numero from Compra where idTipoFactura=14) PP
				                    on compra.Numero=PP.Numero
                    WHERE     (Compra.idTipoFactura = 2) AND (Compra.Usuario = 'OrdenLotes') and compra.borrar=0 and compra.idcompra in (" + listaIds + @") ";
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
                SELECT     Compra.idCompra, PP.idCompra as idCompraPP, Compra.FechaIngreso AS 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, Compra.Total, Compra.Notas, 
                           Compra.Fecha, Compra.FechaVencimiento AS 'Fecha Vencimiento', Compra.Pedido,DATEDIFF(day,compra.fechaIngreso, PP.FechaIngreso) as 'Dias desde OCC a PP',
                           observaciones.Observacion
                FROM       Compra INNER JOIN
		                   Cliente ON Compra.idCliente = Cliente.idCliente
		                   left outer join 
				                  (Select idDocumento, idGrid, observacion, estacion, fechaIngreso, borrar FROM NovedadImportacion where idTipoFactura=3) observaciones
				                on compra.idCompra=observaciones.idDocumento
		                   left outer join
				                  (select idCompra, FechaIngreso, Estacion, Numero from Compra where idTipoFactura=14) PP
				                on compra.Numero=PP.Numero
                WHERE     (Compra.idTipoFactura = 2) AND (Compra.Usuario = 'OrdenLotes') and compra.borrar=0 and compra.fecha>='" + desde + "' and p.fecha<='" + hasta + @"' ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Ordenes de compra consolidadas.
            dgv.Columns[0].Name = "idCompraOCC";
            dgv.Columns[0].Visible = false; // idCompra va oculto.
            dgv.Columns[1].Name = "idCompraPP";
            dgv.Columns[1].Visible = false; // idCompraPP va oculto.
            dgv.Columns[2].Name = "fechaIngresoOCC";
            dgv.Columns[2].Width = 100; // Fecha Ingreso
            dgv.Columns[3].Width = 80; // Estación
            dgv.Columns[4].Name = "numeroCompraOCC";
            dgv.Columns[4].Width = 80; // Numero
            dgv.Columns[5].Width = 240; // Proveedor
            dgv.Columns[6].Width = 80; // Total
            dgv.Columns[6].DefaultCellStyle.Format = "F";
            dgv.Columns[7].Width = 200; // Notas
            dgv.Columns[8].Width = 100; // Fecha OCC
            dgv.Columns[9].Width = 100; // Fecha Vencimiento  OCC
            dgv.Columns[10].Name = "pedidoCompraOCC";
            dgv.Columns[10].Width = 80; // Pedido
            dgv.Columns[11].Name = "diasDesdeOCCaPP";
            dgv.Columns[11].Width = 80; // Pedido
            dgv.Columns[12].Width = 200; // Observación
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // no tiene implementación porque ya se crea implícitamente en la consulta SQL.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            // Solo tengo una medición de tiempo: dgv.Columns[11].Name = "diasDesdeOCCaPP";
            // Si es mayor a 14 se pinta de rojo.
            string dato = "";
            int nroDias = 0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasDesdeOCCaPP", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias > 14)
                        {
                            dgv["diasDesdeOCCaPP", i].Style.BackColor = Color.Red;
                            dgv["diasDesdeOCCaPP", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias < 15 )
                            {
                                dgv["diasDesdeOCCaPP", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasDesdeOCCaPP", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    else
                    {
                        /*
                         * Si no tengo fecha de creación del PP hay que realizar la comparación contra la fecha actual y pintar de naranja en el caso de que 
                         * ya sea hora de convertir
                         * */
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Parse(dgv["fechaIngresoOCC", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        if ((tiempo.Days) > 14)
                        {
                            dgv["diasDesdeOCCaPP", i].Style.BackColor = Color.Orange;
                        }
                        else
                        {
                            dgv["diasDesdeOCCaPP", i].Style.BackColor = Color.Green;
                        }
                    }
                }
                
            }
        }
    }
}
