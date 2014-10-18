using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barco.CAD.Clases
{
    class AccesoDatosSPFactNorm : MostrarData
    {
        public override int llenaGrid(System.Windows.Forms.DataGridView dgv)
        {
            if (dgv == null)
            {
                return -1;
            }
            dgv.Columns.Clear();
            sqlQuery = @"
                Select top(200)Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where compra.Numero in (
					SELECT  distinct Compra.Comprobante
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
							LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 
							and compra.Usuario<>'SPLotes' 
							and compra.Comprobante like 'SP%' 
							and compra.Comprobante is not null
					GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, 
							CAST(Compra.Notas AS varchar(2000)), 
							Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Comprobante, Compra.Pedido,
							NovedadImportacion.Observacion, detcompra.idarticulo
                )
                and compra.idTipoFactura=26 and compra.Usuario<>'SPLotes'
                order by idCompra desc
            ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override int llenaGrid(System.Windows.Forms.DataGridView dgv, int idRegistro)
        {
             if (dgv == null || idRegistro < 1)
            {
                return -1;
            }
            dgv.Columns.Clear();
            sqlQuery = @"
                Select top(200)Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where compra.Numero in (
					SELECT  distinct Compra.Comprobante
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
							LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 
							and compra.Usuario<>'SPLotes' 
							and compra.Comprobante like 'SP%' 
							and compra.Comprobante is not null
					GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, 
							CAST(Compra.Notas AS varchar(2000)), 
							Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Comprobante, Compra.Pedido,
							NovedadImportacion.Observacion, detcompra.idarticulo
                )
                and compra.idTipoFactura=26 and compra.Usuario<>'SPLotes' and compra.idcompra="+idRegistro+ @" 
                order by idCompra desc
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
            sqlQuery = @"
                Select top(200)Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where compra.Numero in (
					SELECT  distinct Compra.Comprobante
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
							LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 
							and compra.Usuario<>'SPLotes' 
							and compra.Comprobante like 'SP%' 
							and compra.Comprobante is not null
					GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, 
							CAST(Compra.Notas AS varchar(2000)), 
							Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Comprobante, Compra.Pedido,
							NovedadImportacion.Observacion, detcompra.idarticulo
                )
                and compra.idTipoFactura=26 and compra.Usuario<>'SPLotes' and compra.idcompra in ("+listaIds+@") 
                order by idCompra desc
            ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
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
            sqlQuery = @"
                Select top(200)Compra.idcompra, Compra.fechaIngreso as 'Fecha Ingreso', Compra.Estacion, Compra.Numero, Compra.fecha, Compra.Total, 
	                Compra.Notas, Compra.Pedido, Compra.Comprobante, NovedadImportacion.Observacion
                from Compra 
	                left outer join NovedadImportacion on compra.idCompra=NovedadImportacion.idDocumento
                where compra.Numero in (
					SELECT  distinct Compra.Comprobante
					FROM    DetCompra 
							INNER JOIN Compra ON DetCompra.idCompra = Compra.idCompra
							LEFT OUTER JOIN NovedadImportacion ON Compra.idCompra = NovedadImportacion.idDocumento
					WHERE   DetCompra.idArticulo IN (SELECT idArticulo FROM Articulo WHERE  Articulo LIKE 'IG-%') 
							AND Compra.idTipoFactura=4 
							and compra.Usuario<>'SPLotes' 
							and compra.Comprobante like 'SP%' 
							and compra.Comprobante is not null
					GROUP BY Compra.idCompra, detcompra.iddetcompra,Compra.FechaIngreso, Compra.Numero, 
							CAST(Compra.Notas AS varchar(2000)), 
							Compra.Fecha, Compra.FechaVencimiento, Compra.Usuario, Compra.estacion, Compra.Total, Compra.Comprobante, Compra.Pedido,
							NovedadImportacion.Observacion, detcompra.idarticulo
                )
                and compra.idTipoFactura=26 and compra.Usuario<>'SPLotes' and and compra.fecha>='" + desde + "' and compra.fecha<='" + hasta + @"' 
                order by idCompra desc
            ";
            miClase.LlenaGrid(dgv, "compra", sqlQuery);
            estilo(dgv);
            return 0;
        }

        public override void estilo(System.Windows.Forms.DataGridView dgv)
        {
            if (dgv != null)
            {
                // Sp de facturas normales.
                dgv.Columns[0].Name = "idCompra";
                dgv.Columns[0].Visible = false; // idCompra
                dgv.Columns[1].Width = 100; // Fecha Ingreso
                dgv.Columns[2].Width = 100; // Estacion
                dgv.Columns[3].Width = 80; // Numero SP
                dgv.Columns[4].Width = 100; // Fecha
                dgv.Columns[5].Width = 100; // Total
                dgv.Columns[5].DefaultCellStyle.Format = "F";
                dgv.Columns[6].Width = 200; // Notas
                dgv.Columns[7].Width = 80; // Pedido
                dgv.Columns[8].Width = 80; // Comprobante
                dgv.Columns[9].Width = 150; // Observación
            }
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
