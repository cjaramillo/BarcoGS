using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barco.CAD
{
    class AccesoDatosPC:MostrarData
    {
        /*
         * Capa de acceso a datos para plan de compras
         * */
        #region "variables"

        #endregion


        public int up_OCS(int idPC)
        {
            // Recibe el idPC y devuelve el idOCS. -1 en el caso de que no exista.
            int retornar=0;
            if (idPC > 0)
                retornar = miClase.EjecutaEscalar("select isnull(idcompraOC,0) from registroPC where idRegistro=" + idPC + " ");
            if (retornar == 0)
                return -1;
            return retornar;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            // Llena todo.. ideal para inicialización.
            if (dgv == null)
                return -1;
            sqlQuery = @"
				SELECT registroPC.idRegistro AS 'Nro PC', ArticuloMarca.Marca,CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', registroPC.fecha AS 'Fecha Emisión', 
                       registroPC.usuario AS 'Estacion', registroPC.idCompraOc, CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                       CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                       CASE WHEN registroPC.usuarioAsignacion IS NULL THEN NULL ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                       CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.', 
                       CASE WHEN registroPC.idCompraOc IS NOT NULL THEN DATEDIFF(day, registroPC.fecha, registroPC.fechaEntregaDr) ELSE NULL END AS 'Dias Revision Dr', 
                       NovedadImportacion.observacion AS Observación
				FROM         registroPC LEFT OUTER JOIN
										NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
									  inner join 
										ArticuloMarca on registroPC.idMarca=ArticuloMarca.idMarca
				WHERE     (registroPC.pcInformativo IS NULL)
				ORDER BY 'Nro PC'
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
            if (idRegistro > 0)
            {
                // Solo muestra un registro.
                sqlQuery = @"
                SELECT registroPC.idRegistro AS 'Nro PC', ArticuloMarca.Marca,CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', registroPC.fecha AS 'Fecha Emisión', 
                       registroPC.usuario AS 'Estacion', registroPC.idCompraOc, CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                       CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                       CASE WHEN registroPC.usuarioAsignacion IS NULL THEN NULL ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                       CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.', 
                       CASE WHEN registroPC.idCompraOc IS NOT NULL THEN DATEDIFF(day, registroPC.fecha, registroPC.fechaEntregaDr) ELSE NULL END AS 'Dias Revision Dr', 
                       NovedadImportacion.observacion AS Observación
				FROM         registroPC LEFT OUTER JOIN
										NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
									  inner join 
										ArticuloMarca on registroPC.idMarca=ArticuloMarca.idMarca
				where registroPC.pcInformativo is null and registroPC.idRegistro=" + idRegistro + @" 
				order by registroPC.idRegistro asc
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
                    SELECT registroPC.idRegistro AS 'Nro PC', ArticuloMarca.Marca,CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', registroPC.fecha AS 'Fecha Emisión', 
                       registroPC.usuario AS 'Estacion', registroPC.idCompraOc, CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                       CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                       CASE WHEN registroPC.usuarioAsignacion IS NULL THEN NULL ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                       CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.', 
                       CASE WHEN registroPC.idCompraOc IS NOT NULL THEN DATEDIFF(day, registroPC.fecha, registroPC.fechaEntregaDr) ELSE NULL END AS 'Dias Revision Dr', 
                       NovedadImportacion.observacion AS Observación
				    FROM registroPC 
                        LEFT OUTER JOIN
						    NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
						inner join 
							ArticuloMarca on registroPC.idMarca=ArticuloMarca.idMarca
				    where registroPC.pcInformativo is null and registroPC.idRegistro in (" + listaIds + @")
				    order by registroPC.idRegistro asc
                    ";
                    dgv.Columns.Clear();
                    miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
                    estilo(dgv);
                    return 0;
                }
                return -1;
            }
        }


        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, DateTime vDesde, DateTime vHasta)
        {
            // Muestra registros en un rango comprendido de fechas basándose en compra.fecha.
            if(vDesde==null || vHasta==null || dgv == null)
            {
                return -1;
            }
            string desde = vDesde.AddDays(-1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string hasta = vHasta.AddDays(1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            sqlQuery = @"
                SELECT registroPC.idRegistro AS 'Nro PC', ArticuloMarca.Marca,CASE WHEN registroPC.modo = 1 THEN 'Consolidado' ELSE 'Normal' END AS 'Modo', registroPC.fecha AS 'Fecha Emisión', 
                       registroPC.usuario AS 'Estacion', registroPC.idCompraOc, CASE WHEN registroPC.numeroOc IS NULL THEN 'LIBRE' ELSE registroPC.numeroOc END AS 'Numero OC', 
                       CASE WHEN registroPC.fechaAsignacion IS NULL THEN NULL ELSE registroPC.fechaAsignacion END AS 'Fecha Asignación', 
                       CASE WHEN registroPC.usuarioAsignacion IS NULL THEN NULL ELSE registroPC.usuarioAsignacion END AS 'Usuario Asignación', 
                       CASE WHEN registroPC.fechaEntregaDr IS NULL THEN NULL ELSE registroPC.fechaEntregaDr END AS 'Fecha Entrega Dr.', 
                       CASE WHEN registroPC.idCompraOc IS NOT NULL THEN DATEDIFF(day, registroPC.fecha, registroPC.fechaEntregaDr) ELSE NULL END AS 'Dias Revision Dr', 
                       NovedadImportacion.observacion AS Observación
				FROM   registroPC 
					LEFT OUTER JOIN
						NovedadImportacion ON registroPC.idRegistro = NovedadImportacion.idDocumento
					INNER JOIN
						ArticuloMarca on registroPC.idMarca=ArticuloMarca.idMarca
				WHERE     (registroPC.pcInformativo IS NULL) and registroPC.fecha>='" + desde + "' and registroPC.fecha<='" + hasta + @"' 
				ORDER BY 'Nro PC'
            ";
            dgv.Columns.Clear();
            miClase.LlenaGrid(dgv, "registroPC", sqlQuery);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            // Planes de Compra
            dgv.Columns[0].Width = 80; // Nro PC
            dgv.Columns[1].Width = 120; // Marca
            dgv.Columns[2].Width = 80; // Modo
            dgv.Columns[3].Width = 100; // Fecha Emisión
            dgv.Columns[4].Width = 80; // Usuario
            dgv.Columns[5].Name = "idCompraOC"; // idCompraOc
            dgv.Columns[5].Visible = false;
            dgv.Columns[6].Width = 80; // Numero OC
            dgv.Columns[6].Name = "numeroOC";
            dgv.Columns[7].Width = 100; // Fecha Asignación
            dgv.Columns[8].Width = 80; // Usuario Asignación
            dgv.Columns[9].Width = 100; // Fecha Entrega Dr.
            dgv.Columns[10].Name = "diasRevision";
            dgv.Columns[10].Width = 100;
            dgv.Columns[11].Width = 120; // Observación.
        }

        public override void calcularTiempos(System.Windows.Forms.DataGridView dgv)
        {
            // En esta clase no necesita implementación del cuerpo porque a través de SQL ya obtiene los tiempos.
        }

        public override void pintarCeldas(System.Windows.Forms.DataGridView dgv)
        {
            string dato="";
            int nroDias=0;
            int error = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                error = 0;
                try
                {
                    dato = dgv["diasRevision", i].Value.ToString(); // Aquí se produce la excepción.
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
                        if (nroDias >= 5)
                        {
                            dgv["diasRevision", i].Style.BackColor = Color.Red;
                            dgv["diasRevision", i].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            if (nroDias >= 0 && nroDias <= 4)
                            {
                                dgv["diasRevision", i].Style.BackColor = Color.Green; // Dentro de Lo permitido
                                dgv["diasRevision", i].Style.ForeColor = Color.White;
                            }
                            // Acá por el else iría el color de las celdas cuyas OCs fueron creadas antes de los PC.. cosa que teóricamente sería imposible.
                        }
                    }
                    else
                    {
                        /*
                         * Si el doctor aún no ha entregado y si:
                         * (la fecha de creación del plan de compras)-(la fecha actual) es >=5 
                         * entonces hay que pintar porque en teoría eso ya es un atraso
                         * */
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Parse(dgv["Fecha Emisión", i].Value.ToString());
                        TimeSpan tiempo = dt1.Subtract(dt2);
                        if ((tiempo.Days) >= 5)
                        {
                            dgv["diasRevision", i].Style.BackColor = Color.Orange;
                        }
                    }
                }
            }
        }
    }
}
