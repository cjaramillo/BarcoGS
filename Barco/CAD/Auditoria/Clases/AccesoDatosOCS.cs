using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Barco.CAD
{
    class AccesoDatosOCS:MostrarData
    {
        /*
         * Capa de acceso a datos para ordenes de compra simples.
         * */
        #region "Variables"

        #endregion

        public Boolean validaNombreIG(int idCompraOCS)
        {
            sqlQuery = @"select count(*) from compra where idtipofactura=2 and usuario<>'OrdenLotes' and idcompra="+idCompraOCS;
            if (miClase.EjecutaEscalar(sqlQuery) < 1)
                return false;
            sqlQuery = "select numero from compra where idtipofactura=2 and usuario<>'OrdenLotes' and idcompra=" + idCompraOCS;
            string numero = miClase.EjecutaEscalarStr(sqlQuery);
            if (numero.Length != 11)
                return false;
            if ((numero.Substring(0, 3).CompareTo("IG-") != 0) || (numero.Substring(6, 1).CompareTo("-"))!=0)
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

        public int down_pc (int idOCS)
        {
            // Devuelve el PC asociado a esta OCS. -1 en caso de no existir asociación.
            if (miClase.EjecutaEscalar("select count(*) from registroPC where idCompraOC="+idOCS) > 0)
            {
                // Existe asociación
                return miClase.EjecutaEscalar("select idRegistro from registroPC where idCompraOC="+idOCS);
            }
            else
            {
                return -1;
            }
        }

        public int up_OCC(int idOCS)
        {
            // Devuelve la OCC a la cual se encuentra asociada esta OCS. -1 en caso de no existir asociación.
            if (idOCS < 1)
                return -1;
            sqlQuery = "select count(idcompra) from controlOCLotes where idCompra2=" + idOCS;
            if (miClase.EjecutaEscalar(sqlQuery)>0)
            {
                sqlQuery = "select isnull(idcompra,0) from controlOCLotes where idCompra2=" + idOCS + " order by idControl desc";
                return miClase.EjecutaEscalar(sqlQuery);

            }
            return -1;
        }

        public int up_SP(int idOCS)
        {
            // Devuelve el idCompra del SP que se generó en caso de tener anticipo. -1 en caso de no tener anticipos.
            
            // Validación de parámetros
            if (idOCS < 1)
                return -1;

            // Primero determinar si tiene anticipo verificando en la tabla controlAnticiposOCS
            sqlQuery = @"select COUNT(*) from controlAnticiposOCS where idCompraOCS="+idOCS;
            if (miClase.EjecutaEscalar(sqlQuery) > 0)
            {
                sqlQuery = @"select top(1) idCompraSP from controlAnticiposOCS where idCompraOCS=" + idOCS;
                return miClase.EjecutaEscalar(sqlQuery);
            }
            else
            {
                // Métodos alternativos: En caso de que sea antigua.
                if (miClase.EjecutaEscalar("select count(*) from compra where idtipofactura=2 and idcompra=" + idOCS + " and departamento is not null and departamento like 'SP-%'") == 0)
                    return -1;
                sqlQuery = "select top(1) departamento from compra where idtipofactura=2 and idcompra=" + idOCS + " and departamento is not null and departamento like 'SP-%' order by idcompra desc";
                string nroSP = miClase.EjecutaEscalarStr(sqlQuery);
                sqlQuery = "select top(1) idcompra from compra where idtipofactura=26 and numero='" + nroSP + "'";
                return miClase.EjecutaEscalar(sqlQuery); 
            }
        }

        public int up_PP(int idOCS)
        {
            if (idOCS < 1)
                return -1;
            sqlQuery = @"
                Select count(idcompra)
                from compra 
                where idtipofactura=14 and borrar=0 
                    and numero in 
                        (
                            select top(1) numero from compra where idtipofactura=2 and borrar=0 and Compra.Usuario <> 'OrdenLotes' and compra.idcompra=" + idOCS + @"
            )";
            if (miClase.EjecutaEscalar(sqlQuery) > 0)
            {
                sqlQuery = @"
                Select top(1) idcompra 
                from compra 
                where idtipofactura=14 and borrar=0 
                    and numero in 
                        (
                            select top(1) numero from compra where idtipofactura=2 and borrar=0 and Compra.Usuario <> 'OrdenLotes' and compra.idcompra=" + idOCS + @"
                        )";
                return miClase.EjecutaEscalar(sqlQuery);
            }
            return -1; // Puede ser que la orden de compra simple ya no exista en la tabla compra.. Si ya no existe pierdo el vínculo con el PP.. OJO.
            // Si quisiera mas adelante buscarla en algun otro lado aquí se debe implementar el else
        }

        public int[] up_FacturasAnticipos(int idOCS)
        {
            // devuelve un array int que contiene los idcompra de las facturas que se generaron como anticipo.
            DataSet ds = new DataSet();
            DataRow dr;
            SqlDataAdapter sqlda = new SqlDataAdapter();
            int [] idComprasFacturasAnticipos = new int[100];
            int idCompra = 0;

            // Primeramente con el idOCS que recibo tengo que buscar en la tabla controlAnticiposOCS:
            //sqlQuery = @"select count(*) from controlAnticiposOCS where idCompraOCS=" + idOCS;
            sqlQuery = @"
            select COUNT (*) from  (
                select idCompraAnticipo from controlAnticiposOCS where idCompraOCS=" + idOCS + @" and idCompraAnticipo is not null 
                union 
                select idCompraISD from controlAnticiposOCS where idCompraOCS=" + idOCS + @" and idCompraISD is not null
            ) p
            ";


            if (miClase.EjecutaEscalar(sqlQuery) > 0) 
            {
                sqlQuery = @"
                    select idCompraAnticipo from controlAnticiposOCS where idCompraOCS=" + idOCS + @" and idCompraAnticipo is not null 
                    union 
                    select idCompraISD from controlAnticiposOCS where idCompraOCS=" + idOCS + @" and idCompraISD is not null
                ";

                sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
                sqlda.Fill(ds, "compra");
                ds.Tables["compra"].Rows[0].ToString();
                
                if (ds.Tables["compra"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["compra"].Rows.Count; i++)
                    {
                        dr = ds.Tables["compra"].Rows[i];
                        idCompra = Int32.Parse(dr["idCompraAnticipo"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
                        idComprasFacturasAnticipos[i] = idCompra;
                    }
                    return idComprasFacturasAnticipos;
                }
                
            }
            else
            {
                /*
                 * Esto quiere decir que no encontró ningún anticipo buscando con el idCompraOCS en la tabla controlAnticiposOCS
                 * Esto puede deberse a que es un anticipo antiguo, y fue creado antes de que se implemente la tabla controlAnticiposOCS
                 * Vamos a intentar buscar de otras maneras (en bloque de código antiguo)
                 * */
                int idSP = this.up_SP(idOCS);
                if (idSP == -1)
                {
                    // No hay anticipos.
                    idComprasFacturasAnticipos[0] = -1; // Ojo con esto. Marco la primera posición con -1
                    return idComprasFacturasAnticipos;
                }
                sqlQuery = @"select top(1) numero from compra where idtipofactura=26 and idcompra=" + idSP;
                string nroSP = miClase.EjecutaEscalarStr(sqlQuery);
                sqlQuery = @"select idcompra from compra where idtipofactura=4 and borrar=0 and otro='" + nroSP + "'";
                sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
                sqlda.Fill(ds, "compra");
                ds.Tables["compra"].Rows[0].ToString();
                if (ds.Tables["compra"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["compra"].Rows.Count; i++)
                    {
                        dr = ds.Tables["compra"].Rows[i];
                        idCompra = Int32.Parse(dr["idcompra"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
                        idComprasFacturasAnticipos[i] = idCompra;
                    }
                    return idComprasFacturasAnticipos;
                }
                else
                {
                    // Esto quiere decir que no encontró ningún anticipo buscando con el nombre de la OCS
                    // Para esto voy a emplear búsqueda sobre la tabla: controlAnticiposOCS
                    sqlQuery = @"select count(*) from controlAnticiposOCS where idCompraOCS=" + idOCS;
                    if (miClase.EjecutaEscalar(sqlQuery) > 0)
                    {
                        // hay data.
                        ds = new DataSet();
                        sqlda = new SqlDataAdapter();
                        sqlQuery = @"select idCompraAnticipo from controlAnticiposOCS where idCompraOCS=" + idOCS;
                        sqlda.SelectCommand = new SqlCommand(sqlQuery, Datos.sqlConn);
                        sqlda.Fill(ds, "controlAnticiposOCS");
                        ds.Tables["controlAnticiposOCS"].Rows[0].ToString();
                        DataRow dr1;
                        if (ds.Tables["controlAnticiposOCS"].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables["controlAnticiposOCS"].Rows.Count; i++)
                            {
                                dr1 = ds.Tables["controlAnticiposOCS"].Rows[i];
                                idCompra = Int32.Parse(dr1["idCompraAnticipo"].ToString()); // no manejo excepción. de la integridad de los datos se encarga el sgbd
                                idComprasFacturasAnticipos[i] = idCompra;
                            }
                            return idComprasFacturasAnticipos;
                        }
                    }
                    /*
                    else
                    {
                        idComprasFacturasAnticipos[0] = -1; // No mismo.. no hay nada
                    }
                    return idComprasFacturasAnticipos;
                    */
                }
            }
            idComprasFacturasAnticipos[0] = -1; // No mismo.. no hay nada
            return idComprasFacturasAnticipos;
        }
        
        public int[] up_FacturasAnticipos(int[] idOCS) 
        {
            // Devuelve los idCompra de facturas de anticipos asociados al conjunto de idOCS que recibe.
            // Debe usar recurrentemente el metodo public int[] up_FacturasAnticipos(int idOCS) para cada iteración. y acumular los resultados en un solo vector.
            int[] idCompraFacturasAnticipos = new int[100]; // Este será el vector que el método devuelva.
            int[] arrayTemporal= new int[100]; // Este es un vector de uso temporal.
            if (idOCS[0] == -1) // Validación
            {
                idCompraFacturasAnticipos[0] = -1;
                return idCompraFacturasAnticipos;
            }
            int posVectorGlobal = 0;
            // Primero voy a buscar asociaciones en la tabla controlAnticiposOCS para cada elemento del idOCS
            for (int i = 0; i < 100; i++)
            {
                if (idOCS[i] > 0)
                {
                    arrayTemporal = new int[100];
                    arrayTemporal = this.up_FacturasAnticipos(idOCS[i]);
                    for (int j = 0; j < 100; j++)
                    {
                        if (arrayTemporal[j] > 0)
                            idCompraFacturasAnticipos[posVectorGlobal++] = arrayTemporal[j];
                        if (arrayTemporal[j + 1] == 0 && arrayTemporal[j + 2] == 0)
                            break; // No tiene sentido seguir ejecutando el for. DE j
                    }
                }
                if (idOCS[i + 1] == 0 && idOCS[i + 2] == 0)
                    break; // No tiene sentido seguir ejecutando el for. DE i
            }
            if (idCompraFacturasAnticipos[0]==0 && idCompraFacturasAnticipos[1]==0)
            {
                idCompraFacturasAnticipos[0] = -1;
            }
            return idCompraFacturasAnticipos;
        }

        public int[] up_spFacturasNormales(int idArticulo)
        {
            // Devuelve un array del tipo int que contiene los idCompra de las facturas que tienen cargo a un determinado artículo.
            // Este método se llama desde el módulo de auditoriía para las facturas normales.
            int[] idCompraSP=new int[100];
            for(int i=0;i<100;i++)
                idCompraSP[i]=0;
            if (idArticulo<1)
            {   
                idCompraSP[0]=-1;
                return idCompraSP;
            }
            sqlQuery = @"
            select count(*) from Compra where idTipoFactura=26 and Numero in (
					SELECT  Compra.Otro as 'Comprobante SP'
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and Compra.Otro is not null 
							and detcompra.idArticulo="+idArticulo+@"
                )
            ";
            if (miClase.EjecutaEscalar(sqlQuery) == 0)
            {
                idCompraSP[0] = -1;
                return idCompraSP;
            }
            DataSet ds = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlQuery = @"
                select idcompra from Compra where idTipoFactura=26 and Numero in (
					SELECT  Compra.Otro as 'Comprobante SP'
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 and compra.Usuario<>'SPLotes' and Compra.Otro is not null 
							and detcompra.idArticulo=" + idArticulo + @"
                )
            ";
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

        public int up_idArticulo(int idOCS)
        {
            if (idOCS < 1 || !validaNombreIG(idOCS) || miClase.EjecutaEscalar("select count(*) from compra where idCompra="+idOCS)==0)
                return -1;
            string numero = miClase.EjecutaEscalarStr("Select numero from compra where idcompra=" + idOCS);
            /*
             * IG-008-2014
             * IG-008/014
             * */
            numero = String.Concat(numero.Substring(0, 6) + "/" + numero.Substring(8, 3));
            sqlQuery = "select count(*) from articulo where articulo='" + numero + "' ";
            if (miClase.EjecutaEscalar(sqlQuery) == 0 || miClase.EjecutaEscalar(sqlQuery)>=2)
                return -1;
            return miClase.EjecutaEscalar("select top(1)idarticulo from articulo where articulo='"+numero+"' ");
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            // Llena todo.. ideal para inicialización.
            if (dgv == null)
                return -1;
            sqlQuery = @"
                SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
                   p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
                   p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
                   CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
                   CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
                   Y.plazoProduccionDias as 'Plazo Dias Produccion', 
                   case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
                   case when p.Estado='Absorbida' then 
			            (
				            select top(1) case when exists(select * from ControlOCLotes where idCompra2=p.idCompraDestino) then 
					            (select top(1)fechaIngreso from ControlOCLotes where idCompra2=p.idCompraDestino)
				            else 
					            case when exists(select * from Compra where idCompra=p.idCompraDestino) then 
						            (select top(1)fechaIngreso from Compra where idCompra=p.idCompraDestino)
					            end
				            end as mensaje 
				            from ControlOCLotes 
			            ) 
	               else
			            -- Si aún no ha sido consolidada entonces el campo es null
			            null
	               end as 'Fecha Consolidación a OCC',
                   pp.fechaIngreso as 'Fecha Transforma a PP',
                   case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
                   case when COMPRA.FechaIngreso IS NOT NULL then 
		                DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
                   else 
	                   case when pp.fechaIngreso IS NOT NULL then 
			                DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
	                   else 
			                DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
	                   end
                   end
                   as 'Dias Atraso Produccion',
                   NovedadImportacion.Observacion
            FROM 
            (
	                SELECT	Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, cliente.idCliente,Cliente.Nombre AS Proveedor, 
                            CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                            Compra.Pedido, registroPC.idRegistro
	                FROM    Compra LEFT OUTER JOIN
                            registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                            Cliente ON Compra.idCliente = Cliente.idCliente
	                WHERE   Compra.idTipoFactura = 2 AND Compra.Usuario <> 'OrdenLotes' -- Estas son simples sin consolidar.
	                UNION
		            SELECT DISTINCT 
                                  ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra AS idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', 
                                  ControlOCLotes.estacion AS Estacion, DetCompra.RefNumero AS Numero, Cliente.idCliente, 
                                  Cliente.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, ControlOCLotes.fecha AS Fecha, 
                                  --DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 
                                  case when ControlOCLotes.idCompra IS not null then 
						            (select top(1) detcompra.Vencimiento from DetCompra where idCompra=ControlOCLotes.idCompra)
                                  else
						            null
                                  end as 'Fecha Inicio Manufactura', 
                                  'Absorbida' AS Estado, 
                      
                                  --Compra.Pedido, 
                                  case when (ControlOCLotes.idCompra IS not null) then
					                (select top(1) compra.Pedido from Compra where idCompra=ControlOCLotes.idCompra)
                                  else
						            null
                                  end as 'Pedido',
                                  registroPC.idRegistro AS 'Nro Plan'
		            FROM         tiemposAuditoria RIGHT OUTER JOIN
							              ControlOCLotes ON tiemposAuditoria.idClientePE = ControlOCLotes.idCliente LEFT OUTER JOIN
							              Cliente ON ControlOCLotes.idCliente = Cliente.idCliente LEFT OUTER JOIN
							              registroPC ON ControlOCLotes.idCompra2 = registroPC.idCompraOc LEFT OUTER JOIN
							              DetCompra LEFT OUTER JOIN
							              Compra ON DetCompra.idCompra = Compra.idCompra ON ControlOCLotes.numero = DetCompra.RefNumero
		            WHERE     (Compra.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
            ) AS p
            LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
            LEFT OUTER JOIN (
                SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
	                  CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
                FROM         registroPC LEFT OUTER JOIN
					                  NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
                WHERE     (registroPC.idCompraOc IS NOT NULL)
            ) X on p.idCompra=x.idCompraOc
            LEFT OUTER JOIN (
                SELECT     tiemposAuditoria.idTiempo, tiemposAuditoria.idClientePE,Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
	                  tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
                FROM         tiemposAuditoria INNER JOIN
	                  Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
            ) Y on p.idCliente=Y.idClientePE
            LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
            LEFT OUTER JOIN (
                SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
            ) PP on P.Numero=pp.Numero
            WHERE P.Numero NOT LIKE '%SEGURIDAD%' 
	            and p.idCompra<>0
            ORDER BY p.idCompra
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv,"p", sqlQuery);
            estilo(dgv);
            return 0;
        }

               
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            // Solo muestra un registro.
            if (dgv == null || idRegistro == 0 )
                return -1;
            sqlQuery = @"
				SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
					   p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
					   p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
					   CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
					   CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
					   Y.plazoProduccionDias as 'Plazo Dias Produccion', 
					   case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
					   COMPRA.FechaIngreso as 'Fecha Consolidación a OCC',
					   pp.fechaIngreso as 'Fecha Transforma a PP',
                       case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
					   case when COMPRA.FechaIngreso IS NOT NULL then 
							DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
					   else 
						   case when pp.fechaIngreso IS NOT NULL then 
								DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
						   else 
								DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
						   end
					   end
					   as 'Dias Atraso Produccion',
                       NovedadImportacion.Observacion
				FROM 
				(
						SELECT     Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, 
                                              CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                              Compra.Pedido, registroPC.idRegistro
						FROM          Compra LEFT OUTER JOIN
                                              registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                              Cliente ON Compra.idCliente = Cliente.idCliente
						WHERE      (Compra.idTipoFactura = 2) AND (Compra.Usuario <> 'OrdenLotes') -- Estas son simples sin consolidar.
						UNION
						SELECT DISTINCT 
                                             ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra as idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', ControlOCLotes.estacion AS Estacion, 
                                             DetCompra.RefNumero AS Numero, Cliente_1.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, 
                                             ControlOCLotes.fecha AS Fecha, DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 'Absorbida' AS Estado, Compra_1.Pedido, 
                                             registroPC_1.idRegistro AS 'Nro Plan'
						FROM         DetCompra INNER JOIN
                                             Compra AS Compra_1 ON DetCompra.idCompra = Compra_1.idCompra INNER JOIN
                                             ControlOCLotes ON DetCompra.RefNumero = ControlOCLotes.numero INNER JOIN
                                             tiemposAuditoria ON ControlOCLotes.idCliente = tiemposAuditoria.idClientePE INNER JOIN
                                             Cliente AS Cliente_1 ON ControlOCLotes.idCliente = Cliente_1.idCliente LEFT OUTER JOIN
                                             registroPC AS registroPC_1 ON ControlOCLotes.idCompra2 = registroPC_1.idCompraOc
						WHERE     (Compra_1.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) 
							AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
				) AS p
				LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
				LEFT OUTER JOIN (
					SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
						  CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
					FROM         registroPC LEFT OUTER JOIN
										  NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
					WHERE     (registroPC.idCompraOc IS NOT NULL)
				) X on p.idCompra=x.idCompraOc
				LEFT OUTER JOIN (
					SELECT     tiemposAuditoria.idTiempo, Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
						  tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
					FROM         tiemposAuditoria LEFT OUTER JOIN
						  Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
				) Y on p.Proveedor=Y.ClientePE
				LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
				LEFT OUTER JOIN (
					SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
				) PP on P.Numero=pp.Numero
                WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.idCompra=" + idRegistro+@"
				ORDER BY p.idCompra
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "p", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro, int idPC)
        {
            // Solo muestra un registro SOLO UNO teniendo en cuenta el idCompra del OCS y el idPC al cual se encuentra ligado.
            // Implementado debido a que el método sobrecargado anterior mostraba varios registros con todos los PC que hacian referencia a el mismo
            if (dgv == null || idRegistro == 0 || idPC == 0)
                return -1;
            sqlQuery = @"
				                SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
                       p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
                       p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
                       CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
                       CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
                       Y.plazoProduccionDias as 'Plazo Dias Produccion', 
                       case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
                       case when p.Estado='Absorbida' then 
			                (
				                select top(1) case when exists(select * from ControlOCLotes where idCompra2=p.idCompraDestino) then 
					                (select top(1)fechaIngreso from ControlOCLotes where idCompra2=p.idCompraDestino)
				                else 
					                case when exists(select * from Compra where idCompra=p.idCompraDestino) then 
						                (select top(1)fechaIngreso from Compra where idCompra=p.idCompraDestino)
					                end
				                end as mensaje 
				                from ControlOCLotes 
			                ) 
	                   else
			                -- Si aún no ha sido consolidada entonces el campo es null
			                null
	                   end as 'Fecha Consolidación a OCC',
                       pp.fechaIngreso as 'Fecha Transforma a PP',
                       case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
                       case when COMPRA.FechaIngreso IS NOT NULL then 
		                    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
                       else 
	                       case when pp.fechaIngreso IS NOT NULL then 
			                    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
	                       else 
			                    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
	                       end
                       end
                       as 'Dias Atraso Produccion',
                       NovedadImportacion.Observacion
                FROM 
                (
	                    SELECT	Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, cliente.idCliente,Cliente.Nombre AS Proveedor, 
                                CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                Compra.Pedido, registroPC.idRegistro
	                    FROM    Compra LEFT OUTER JOIN
                                registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                Cliente ON Compra.idCliente = Cliente.idCliente
	                    WHERE   Compra.idTipoFactura = 2 AND Compra.Usuario <> 'OrdenLotes' -- Estas son simples sin consolidar.
	                    UNION
		                SELECT DISTINCT 
                                      ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra AS idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', 
                                      ControlOCLotes.estacion AS Estacion, DetCompra.RefNumero AS Numero, Cliente.idCliente, 
                                      Cliente.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, ControlOCLotes.fecha AS Fecha, 
                                      --DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 
                                      case when ControlOCLotes.idCompra IS not null then 
						                (select top(1) detcompra.Vencimiento from DetCompra where idCompra=ControlOCLotes.idCompra)
                                      else
						                null
                                      end as 'Fecha Inicio Manufactura', 
                                      'Absorbida' AS Estado, 
                      
                                      --Compra.Pedido, 
                                      case when (ControlOCLotes.idCompra IS not null) then
					                    (select top(1) compra.Pedido from Compra where idCompra=ControlOCLotes.idCompra)
                                      else
						                null
                                      end as 'Pedido',
                                      registroPC.idRegistro AS 'Nro Plan'
		                FROM         tiemposAuditoria RIGHT OUTER JOIN
							                  ControlOCLotes ON tiemposAuditoria.idClientePE = ControlOCLotes.idCliente LEFT OUTER JOIN
							                  Cliente ON ControlOCLotes.idCliente = Cliente.idCliente LEFT OUTER JOIN
							                  registroPC ON ControlOCLotes.idCompra2 = registroPC.idCompraOc LEFT OUTER JOIN
							                  DetCompra LEFT OUTER JOIN
							                  Compra ON DetCompra.idCompra = Compra.idCompra ON ControlOCLotes.numero = DetCompra.RefNumero
		                WHERE     (Compra.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
                ) AS p
                LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
                LEFT OUTER JOIN (
                    SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
	                      CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
                    FROM         registroPC LEFT OUTER JOIN
					                      NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
                    WHERE     (registroPC.idCompraOc IS NOT NULL)
                ) X on p.idCompra=x.idCompraOc
                LEFT OUTER JOIN (
                    SELECT     tiemposAuditoria.idTiempo, tiemposAuditoria.idClientePE,Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
	                      tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
                    FROM         tiemposAuditoria INNER JOIN
	                      Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
                ) Y on p.idCliente=Y.idClientePE
                LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
                LEFT OUTER JOIN (
                    SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
                ) PP on P.Numero=pp.Numero
                WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.idCompra=" + idRegistro + @" and p.idRegistro="+idPC+@" 
				ORDER BY p.idCompra
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "p", sqlQuery);
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
				     SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
					       p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
					       p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
					       CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
					       CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
					       Y.plazoProduccionDias as 'Plazo Dias Produccion', 
					       case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
					       COMPRA.FechaIngreso as 'Fecha Consolidación a OCC',
					       pp.fechaIngreso as 'Fecha Transforma a PP',
                           case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
					       case when COMPRA.FechaIngreso IS NOT NULL then 
							    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
					       else 
						       case when pp.fechaIngreso IS NOT NULL then 
								    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
						       else 
								    DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
						       end
					       end
					       as 'Dias Atraso Produccion',
                           NovedadImportacion.Observacion
				    FROM 
				    (
						    SELECT     Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, 
                                                  CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                                  Compra.Pedido, registroPC.idRegistro
						    FROM          Compra LEFT OUTER JOIN
                                                  registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                                  Cliente ON Compra.idCliente = Cliente.idCliente
						    WHERE      (Compra.idTipoFactura = 2) AND (Compra.Usuario <> 'OrdenLotes') -- Estas son simples sin consolidar.
						    UNION
						    SELECT DISTINCT 
                                                 ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra as idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', ControlOCLotes.estacion AS Estacion, 
                                                 DetCompra.RefNumero AS Numero, Cliente_1.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, 
                                                 ControlOCLotes.fecha AS Fecha, DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 'Absorbida' AS Estado, Compra_1.Pedido, 
                                                 registroPC_1.idRegistro AS 'Nro Plan'
						    FROM         DetCompra INNER JOIN
                                                 Compra AS Compra_1 ON DetCompra.idCompra = Compra_1.idCompra INNER JOIN
                                                 ControlOCLotes ON DetCompra.RefNumero = ControlOCLotes.numero INNER JOIN
                                                 tiemposAuditoria ON ControlOCLotes.idCliente = tiemposAuditoria.idClientePE INNER JOIN
                                                 Cliente AS Cliente_1 ON ControlOCLotes.idCliente = Cliente_1.idCliente LEFT OUTER JOIN
                                                 registroPC AS registroPC_1 ON ControlOCLotes.idCompra2 = registroPC_1.idCompraOc
						    WHERE     (Compra_1.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) 
							    AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
				    ) AS p
				    LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
				    LEFT OUTER JOIN (
					    SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
						      CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
					    FROM         registroPC LEFT OUTER JOIN
										      NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
					    WHERE     (registroPC.idCompraOc IS NOT NULL)
				    ) X on p.idCompra=x.idCompraOc
				    LEFT OUTER JOIN (
					    SELECT     tiemposAuditoria.idTiempo, Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
						      tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
					    FROM         tiemposAuditoria LEFT OUTER JOIN
						      Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
				    ) Y on p.Proveedor=Y.ClientePE
				    LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
				    LEFT OUTER JOIN (
					    SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
				    ) PP on P.Numero=pp.Numero
                    WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.idCompra in (" + listaIds + @") 
				    ORDER BY p.idCompra
                    ";
                    dgv.Columns.Clear();
                    miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
                    estilo(dgv);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int llenaGridDesdeOCC(System.Windows.Forms.DataGridView dgv, int[] idRegistro, int idCompraOCC)
        {
            // No toma en cuenta las OCS que tienen como estado: "Por Absorber"
            // Muestra un conjunto de registros.
            if (idRegistro[0] == -1 || idRegistro[0] == 0 || dgv == null)
            {
                dgv.Columns.Clear();
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
                        SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
                               p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
                               p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
                               CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
                               CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
                               Y.plazoProduccionDias as 'Plazo Dias Produccion', 
                               case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
                               (select top(1)fechaIngreso from Compra where idCompra="+idCompraOCC+@") as 'Fecha Consolidación a OCC',
                               pp.fechaIngreso as 'Fecha Transforma a PP',
                               case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
                               case when COMPRA.FechaIngreso IS NOT NULL then 
		                            DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
                               else 
	                               case when pp.fechaIngreso IS NOT NULL then 
			                            DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
	                               else 
			                            DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
	                               end
                               end
                               as 'Dias Atraso Produccion',
                               NovedadImportacion.Observacion
                        FROM 
                        (
	                            SELECT	Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, 
                                        CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                        Compra.Pedido, registroPC.idRegistro
	                            FROM    Compra LEFT OUTER JOIN
                                        registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                        Cliente ON Compra.idCliente = Cliente.idCliente
	                            WHERE   Compra.idTipoFactura = 2 AND Compra.Usuario <> 'OrdenLotes' -- Estas son simples sin consolidar.
	                            UNION
		                        SELECT DISTINCT 
				                        ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra AS idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', ControlOCLotes.estacion AS Estacion, 
				                        DetCompra.RefNumero AS Numero, Cliente_1.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, ControlOCLotes.fecha AS Fecha, 
				                        DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 'Absorbida' AS Estado, Compra_1.Pedido, registroPC_1.idRegistro AS 'Nro Plan'
		                        FROM    tiemposAuditoria RIGHT OUTER JOIN
				                        ControlOCLotes ON tiemposAuditoria.idClientePE = ControlOCLotes.idCliente LEFT OUTER JOIN
				                        Cliente AS Cliente_1 ON ControlOCLotes.idCliente = Cliente_1.idCliente RIGHT OUTER JOIN
				                        DetCompra LEFT OUTER JOIN
				                        Compra AS Compra_1 ON DetCompra.idCompra = Compra_1.idCompra ON ControlOCLotes.numero = DetCompra.RefNumero LEFT OUTER JOIN
				                        registroPC AS registroPC_1 ON ControlOCLotes.idCompra2 = registroPC_1.idCompraOc
		                        WHERE   (Compra_1.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0) AND (DetCompra.idCompra = "+idCompraOCC+@")
                        ) AS p
                        LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
                        LEFT OUTER JOIN (
                            SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
	                              CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
                            FROM         registroPC LEFT OUTER JOIN
					                              NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
                            WHERE     (registroPC.idCompraOc IS NOT NULL)
                        ) X on p.idCompra=x.idCompraOc
                        LEFT OUTER JOIN (
                            SELECT     tiemposAuditoria.idTiempo, Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
	                              tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
                            FROM         tiemposAuditoria LEFT OUTER JOIN
	                              Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
                        ) Y on p.Proveedor=Y.ClientePE
                        LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
                        LEFT OUTER JOIN (
                            SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
                        ) PP on P.Numero=pp.Numero
                        WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.idCompra in (" + listaIds + @")
                        and p.idCompraDestino<>0
                        ORDER BY p.idCompra
                    ";


                    /*
                    sqlQuery = @"
                    SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
                           p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
                           p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
                           CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
                           CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
                           Y.plazoProduccionDias as 'Plazo Dias Produccion', 
                           case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
                           COMPRA.FechaIngreso as 'Fecha Consolidación a OCC',
                           pp.fechaIngreso as 'Fecha Transforma a PP',
                           case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
                           case when COMPRA.FechaIngreso IS NOT NULL then 
		                        DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
                           else 
	                           case when pp.fechaIngreso IS NOT NULL then 
			                        DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
	                           else 
			                        DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
	                           end
                           end
                           as 'Dias Atraso Produccion',
                           NovedadImportacion.Observacion
                    FROM 
                    (
	                        SELECT     Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, 
                                                  CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                                  Compra.Pedido, registroPC.idRegistro
	                        FROM          Compra LEFT OUTER JOIN
                                                  registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                                  Cliente ON Compra.idCliente = Cliente.idCliente
	                        WHERE      Compra.idTipoFactura = 2 AND Compra.Usuario <> 'OrdenLotes' -- Estas son simples sin consolidar.
	                        UNION
	                        SELECT DISTINCT 
                                                 ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra as idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', 
                                                 ControlOCLotes.estacion AS Estacion, DetCompra.RefNumero AS Numero, Cliente_1.Nombre AS Proveedor, 
                                                 CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, ControlOCLotes.fecha AS Fecha, 
                                                 DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 'Absorbida' AS Estado, Compra_1.Pedido, registroPC_1.idRegistro AS 'Nro Plan'
	                        FROM         DetCompra INNER JOIN
                                                 Compra AS Compra_1 ON DetCompra.idCompra = Compra_1.idCompra INNER JOIN
                                                 ControlOCLotes ON DetCompra.RefNumero = ControlOCLotes.numero INNER JOIN
                                                 tiemposAuditoria ON ControlOCLotes.idCliente = tiemposAuditoria.idClientePE INNER JOIN
                                                 Cliente AS Cliente_1 ON ControlOCLotes.idCliente = Cliente_1.idCliente LEFT OUTER JOIN
                                                 registroPC AS registroPC_1 ON ControlOCLotes.idCompra2 = registroPC_1.idCompraOc
	                        WHERE     (Compra_1.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) 
		                        AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
		                        -- Añadido: OJOJOJOJOJOJOOOOOOOOOOO********************************************** ACÁ IRÍA EL IDCOMPRA DE LA OCC
		                        and detcompra.idCompra="+idCompraOCC+@"
                    ) AS p
                    LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
                    LEFT OUTER JOIN (
                        SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
	                          CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
                        FROM         registroPC LEFT OUTER JOIN
					                          NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
                        WHERE     (registroPC.idCompraOc IS NOT NULL)
                    ) X on p.idCompra=x.idCompraOc
                    LEFT OUTER JOIN (
                        SELECT     tiemposAuditoria.idTiempo, Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
	                          tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
                        FROM         tiemposAuditoria LEFT OUTER JOIN
	                          Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
                    ) Y on p.Proveedor=Y.ClientePE
                    LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
                    LEFT OUTER JOIN (
                        SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
                    ) PP on P.Numero=pp.Numero
                    WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.idCompra in (" + listaIds + @")
                    and p.idCompraDestino<>0  
                    ORDER BY p.idCompra
                    ";
                    */

                    dgv.Columns.Clear();
                    miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
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
                SELECT distinct p.idCompra, case when p.idCompraDestino=0 then null else p.idCompraDestino end as idCompraDestino, pp.idCompra as idCompraPP,
					   p.FechaIngreso as 'Fecha Ingreso', p.Estacion, p.Numero, p.Proveedor, p.Notas, p.Fecha, 
					   p.FechaRevision as 'Fecha Inicio Manufactura', p.Estado, p.Pedido, p.idRegistro as 'Nro Plan', 
					   CASE WHEN p.idRegistro IS NOT NULL THEN DATEDIFF(day, x.[Fecha Entrega Dr.], p.FechaIngreso) ELSE NULL END AS 'Dias Proceso crear OC desde PC',
					   CASE WHEN p.FechaRevision IS NOT NULL THEN DATEDIFF(day, p.FechaIngreso, p.FechaRevision) ELSE null END AS 'Dias Proceso Aprobación Proformas',
					   Y.plazoProduccionDias as 'Plazo Dias Produccion', 
					   case when p.FechaRevision is not null then DATEADD(day,y.plazoProduccionDias, p.FechaRevision) else null end as 'Vence Producción',
					   COMPRA.FechaIngreso as 'Fecha Consolidación a OCC',
					   pp.fechaIngreso as 'Fecha Transforma a PP',
                       case when pp.fechaIngreso IS not null then DATEDIFF(day,p.FechaIngreso,pp.fechaIngreso) else null end as 'Dias desde OCS a PP',
					   case when COMPRA.FechaIngreso IS NOT NULL then 
							DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),COMPRA.FechaIngreso)
					   else 
						   case when pp.fechaIngreso IS NOT NULL then 
								DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),PP.FechaIngreso)
						   else 
								DATEDIFF(DAY, DATEADD(day,y.plazoProduccionDias, p.FechaRevision),GETDATE())
						   end
					   end
					   as 'Dias Atraso Produccion',
                       NovedadImportacion.Observacion
				FROM 
				(
						SELECT     Compra.idCompra,0 as idCompraDestino, Compra.FechaIngreso, Compra.Estacion, Compra.Numero, Cliente.Nombre AS Proveedor, 
                                              CAST(Compra.Notas AS varchar(2000)) AS Notas, Compra.Fecha, Compra.FechaRevision, 'Por Consolidar' AS Estado, 
                                              Compra.Pedido, registroPC.idRegistro
						FROM          Compra LEFT OUTER JOIN
                                              registroPC ON Compra.idCompra = registroPC.idCompraOc LEFT OUTER JOIN
                                              Cliente ON Compra.idCliente = Cliente.idCliente
						WHERE      (Compra.idTipoFactura = 2) AND (Compra.Usuario <> 'OrdenLotes') -- Estas son simples sin consolidar.
						UNION
						SELECT DISTINCT 
                                             ControlOCLotes.idCompra2 AS idCompra, ControlOCLotes.idCompra as idCompraDestino, ControlOCLotes.fechaIngreso AS 'Fecha Ingreso', ControlOCLotes.estacion AS Estacion, 
                                             DetCompra.RefNumero AS Numero, Cliente_1.Nombre AS Proveedor, CAST(ControlOCLotes.notas AS varchar(2000)) AS Notas, 
                                             ControlOCLotes.fecha AS Fecha, DetCompra.Vencimiento AS 'Fecha Inicio Manufactura', 'Absorbida' AS Estado, Compra_1.Pedido, 
                                             registroPC_1.idRegistro AS 'Nro Plan'
						FROM         DetCompra INNER JOIN
                                             Compra AS Compra_1 ON DetCompra.idCompra = Compra_1.idCompra INNER JOIN
                                             ControlOCLotes ON DetCompra.RefNumero = ControlOCLotes.numero INNER JOIN
                                             tiemposAuditoria ON ControlOCLotes.idCliente = tiemposAuditoria.idClientePE INNER JOIN
                                             Cliente AS Cliente_1 ON ControlOCLotes.idCliente = Cliente_1.idCliente LEFT OUTER JOIN
                                             registroPC AS registroPC_1 ON ControlOCLotes.idCompra2 = registroPC_1.idCompraOc
						WHERE     (Compra_1.idTipoFactura = 2) AND (DetCompra.RefNumero IS NOT NULL) 
							AND (LEN(LTRIM(DetCompra.RefNumero)) <> 0)
				) AS p
				LEFT OUTER JOIN	NovedadImportacion ON p.idCompra = NovedadImportacion.idDocumento
				LEFT OUTER JOIN (
					SELECT     registroPC.idRegistro AS 'Nro PC', registroPC.idCompraOc, 
						  CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.' 
					FROM         registroPC LEFT OUTER JOIN
										  NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
					WHERE     (registroPC.idCompraOc IS NOT NULL)
				) X on p.idCompra=x.idCompraOc
				LEFT OUTER JOIN (
					SELECT     tiemposAuditoria.idTiempo, Cliente.Nombre as ClientePE, tiemposAuditoria.plazoProduccionDias, tiemposAuditoria.plazoTransporteDias, 
						  tiemposAuditoria.PlazoPagoAnticipoDias, tiemposAuditoria.PlazoPagoFacturaDias
					FROM         tiemposAuditoria LEFT OUTER JOIN
						  Cliente ON tiemposAuditoria.idClientePE = Cliente.idCliente 
				) Y on p.Proveedor=Y.ClientePE
				LEFT OUTER JOIN Compra ON p.idCompraDestino=COMPRA.idCompra
				LEFT OUTER JOIN (
					SELECT IDCOMPRA, NUMERO, FECHAINGRESO FROM Compra WHERE idTipoFactura=14 AND Borrar=0
				) PP on P.Numero=pp.Numero
                WHERE P.Numero NOT LIKE '%SEGURIDAD%' AND p.fecha>='" + desde + "' and p.fecha<='" + hasta + @"' 
				ORDER BY p.idCompra
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Aplica el estilo correspondiente al DGV
            dgv.Columns[0].Name = "idCompraOCS";
            dgv.Columns[0].Visible = false; // idCompra va oculto.
            dgv.Columns[1].Name = "idCompraOCC";
            dgv.Columns[1].Visible = false; // idCompra va oculto.
            dgv.Columns[2].Name = "idCompraPP";
            dgv.Columns[2].Visible = false; // idCompra va oculto.
            dgv.Columns[3].Name = "fechaIngresoOCS";
            dgv.Columns[3].Width = 100; // Fecha Ingreso
            dgv.Columns[4].Width = 80; // Estación
            dgv.Columns[5].Width = 80; // Numero
            dgv.Columns[6].Width = 240; // Proveedor
            dgv.Columns[7].Width = 200; // Notas
            dgv.Columns[8].Width = 100; // Fecha OCS
            dgv.Columns[9].Width = 100; // Fecha Inicio manufactura
            dgv.Columns[10].Width = 100; // Estado
            dgv.Columns[11].Width = 80; // Pedido
            dgv.Columns[12].Name = "pcOrigen";
            dgv.Columns[12].Width = 80; // PC Origen
            dgv.Columns[13].Name = "diasCreacion";
            dgv.Columns[13].Width = 120; // Dias Proceso crear OC desde PC
            dgv.Columns[14].Name = "diasAprobacionProforma";
            dgv.Columns[14].Width = 140; // Dias Proceso Aprobación Proformas
            dgv.Columns[15].Width = 150; // Plazo Dias Produccion
            dgv.Columns[16].Width = 150; // Vence Producción
            dgv.Columns[17].Width = 100; // Fecha Consolidación a OCC
            dgv.Columns[18].Width = 100; // Fecha Transforma a PP
            dgv.Columns[19].Name = "diasDesdeOCSaPP";
            dgv.Columns[19].Width = 120; //Dias desde OCS a PP
            dgv.Columns[20].Name = "diasAtrasoProduccion";
            dgv.Columns[20].Width = 80; // Dias Atraso Produccion
            dgv.Columns[21].Width = 200; // Observacion
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // no tiene implementación porque ya se crea implícitamente en la consulta SQL.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            /*
             *  Recorrer Columna a Columna porque tengo varias mediciones de tiempo.
             *  dgv.Columns[13].Name = "diasCreacion";
             *  dgv.Columns[14].Name = "diasAprobacionProforma";
             *  dgv.Columns[19].Name = "diasDesdeOCSaPP";
             *  dgv.Columns[20].Name = "diasAtrasoProduccion";
             */

            // Empiezo con dgv.Columns[13].Name = "diasCreacion"; Debo pintar las celdas si una vez que ha revisado el doctor +1 diua se han procedido a crear las ordenes de compra simples.
            string dato = "";
            int nroDias = 0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasCreacion", i].Value.ToString(); // Aquí se produce la excepción.
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
                            dgv["diasCreacion", i].Style.BackColor = Color.Red;
                            dgv["diasCreacion", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias <= 2)
                            {
                                dgv["diasCreacion", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasCreacion", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    // no se implementa el caso contrario porque si no tengo fecha de revisión del doctor no tengo punto de referencia para tomar tiempo.
                }
            }



            // Ahora voy a pintar dgv.Columns[14].Name = "diasAprobacionProforma";
            dato = "";
            nroDias = 0;
            error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasAprobacionProforma", i].Value.ToString(); // Aquí se produce la excepción.
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
                            dgv["diasAprobacionProforma", i].Style.BackColor = Color.Red;
                            dgv["diasAprobacionProforma", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias <= 2)
                            {
                                dgv["diasAprobacionProforma", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasAprobacionProforma", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo.
                        }
                    }
                    else
                    {
                        // Si aún no hay aprobación de la proforma hay que verificar si ya es hora de que apruebe.
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Parse(dgv["fechaIngresoOCS", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        if ((tiempo.Days) >= 3)
                        {
                            dgv["diasAprobacionProforma", i].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }

            // dgv.Columns[19].Name = "diasDesdeOCSaPP";
            // Rojo: >14
            // Verde: de 0 a 14
            // Naranja: En caso de que a la fecha actual la OCS ya debió haberse convertido a PP.

            dato = "";
            nroDias = error = 0;
            
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasDesdeOCSaPP", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias >= 15)
                        {
                            dgv["diasDesdeOCSaPP", i].Style.BackColor = Color.Red;
                            dgv["diasDesdeOCSaPP", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias <= 14)
                            {
                                dgv["diasDesdeOCSaPP", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasDesdeOCSaPP", i].Style.ForeColor = Color.White;
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
                        DateTime dt2 = DateTime.Parse(dgv["fechaIngresoOCS", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        if ((tiempo.Days) >= 15)
                        {
                            dgv["diasDesdeOCSaPP", i].Style.BackColor = Color.Orange;
                        }
                        else
                        {
                            dgv["diasDesdeOCSaPP", i].Style.BackColor = Color.Green;
                        }
                    }
                }

            }








            // Ahora me toca dgv.Columns[20].Name = "diasAtrasoProduccion"; Aquí si es mayor a cero ya debe encenderse la alarma a rojo.
            dato = "";
            nroDias = 0;
            error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasAtrasoProduccion", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias > 8) // 8 Dias de tiempo muerto es lo máximo permitido.
                        {
                            dgv["diasAtrasoProduccion", i].Style.BackColor = Color.Red;
                            dgv["diasAtrasoProduccion", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias < 9)
                            {
                                dgv["diasAtrasoProduccion", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasAtrasoProduccion", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyo tiempo es negativo. pero ya está evaluado ese caso en la condición.
                        }
                    }
                    // No evalúo el else.. Los valores null son los que aún no tienen definida fecha de inicio de manufactura.
                }
            }
        }
    }
}
