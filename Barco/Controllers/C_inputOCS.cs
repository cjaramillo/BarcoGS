using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Barco.CAD;

namespace Barco.Controllers
{
    public class C_inputOCS
    {
        /*
         * Esta clase actúa como controlador de la vista para el ingreso de datos de las OCS.
         * 
         * */

        private Datos miClase = new Datos();
        private String sqlQuery; 

        // Elementos WindowsForms para mapeado de paneles.
        Control.ControlCollection icc;
        DateTimePicker dtp;
        Label lbl;
        TextBox txt;
        ComboBox cmb;

        public void fillPanel(Panel p1, byte nroGrid, int idOCS)
        {
            DataSet ds = new DataSet();
            icc = p1.Controls;
            switch (nroGrid)
            {
                case 1:
                    {
                        String[,] controls = new String[,]{{
                            "dtpSolCot",
                            "dtpRecibCot",
                            "dtpApruebaCot",
                            "dtpFinManufactT",
                            "dtpFinManufactR",
                            "dtpDespachoFab",
                            "dtpLlegaPuertoIntl"
                        },{
                            "lblSolCot",
                            "lblRecibCot",
                            "lblApruebaCot1",
                            "lblFinManufactT",
                            "lblFinManufactR",
                            "lblDespachoFab",
                            "lblLlegaPuertoIntl"
                        }};
                        sqlQuery = @"
                            select	Compra.Fecha, TiemposOCS.recibeCotizacion, Compra.FechaRevision, Compra.FechaVencimiento, 
		                            Compra.FechaEntrega, TiemposOCS.despachoFabrica, TiemposOCS.llegaPuertoIntl
                            from	Compra left outer join TiemposOCS on Compra.idCompra=TiemposOCS.idCompra
                            where	Compra.idCompra=" + idOCS;
                        miClase.llenaDS(ds, sqlQuery, "Tiempos");

                        for (int i = 0; i < (controls.Length / controls.Rank); i++)
                        {
                            dtp = (DateTimePicker)icc.Find(controls[0, i], false)[0];
                            lbl = (Label)icc.Find(controls[1, i], false)[0];
                            if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Length == 0)
                            {
                                dtp.Value = new DateTime(2000, 01, 01);
                                lbl.ForeColor = System.Drawing.Color.Red;
                                dtp.Checked = false;
                            }
                            else
                            {
                                dtp.Value = DateTime.Parse(ds.Tables[0].Rows[0].ItemArray[i].ToString());
                                lbl.ForeColor = System.Drawing.Color.Black;
                                dtp.Checked = true;
                            }
                        }
                    }break;
                case 2:
                    {
                        String[] controls = new String[]{
                            "txtSPAnticipo",
                            "txtVenceAnt",
                            "txtPagoAnticipo",
                            "txtFactPrepago",
                            "txtVencePrepago",
                            "txtPagoPrepago"
                        };
                        TextBox txt = (TextBox)icc.Find("txtSPAnticipo", false)[0];
                        Button btn = (Button)icc.Find("btnSolAnticipo", false)[0];
                        int sp = new AccesoDatosOCS().up_SP(idOCS);
                        if (sp > 0) //Anticipos:
                        {
                            sqlQuery = @"select numero from compra where idcompra="+sp;
                            txt.Text = miClase.EjecutaEscalarStr(sqlQuery);
                            btn.Visible = false;
                            txt.Visible = true;
                            txt.Enabled = false;

                            sqlQuery = @"
                                select cast(Convert(varchar(10),CONVERT(date,Compra.FechaVencimiento,106),103) as nvarchar(10))
                                from controlAnticiposOCS 
	                                left outer join Compra on  controlAnticiposOCS.idCompraAnticipo=Compra.idCompra
                                where controlAnticiposOCS.idCompraOCS=" + idOCS;
                            txt = (TextBox)icc.Find("txtVenceAnt", false)[0];
                            txt.Text = miClase.EjecutaEscalarStr(sqlQuery);
                            txt.Enabled = false;
                            txt.Visible = true;

                            // Verificar si exiten pagos:
                            sqlQuery = @"
                            SELECT  COUNT(pago.Pago)
                            FROM         Pago RIGHT OUTER JOIN
                                                  Compra ON Pago.idCompra = Compra.idCompra RIGHT OUTER JOIN
                                                  controlAnticiposOCS ON Compra.idCompra = controlAnticiposOCS.idCompraAnticipo
                            where controlAnticiposOCS.idCompraOCS=" + idOCS;
                            txt = (TextBox)icc.Find("txtPagoAnticipo", false)[0];
                            if (miClase.EjecutaEscalar(sqlQuery) > 0)
                            {
                                sqlQuery = @"
                                SELECT  top(1)  cast(Convert(varchar(10),CONVERT(date,Pago.Fecha,106),103) as nvarchar(10))
                                FROM         Pago RIGHT OUTER JOIN
                                                      Compra ON Pago.idCompra = Compra.idCompra RIGHT OUTER JOIN
                                                      controlAnticiposOCS ON Compra.idCompra = controlAnticiposOCS.idCompraAnticipo
                                where controlAnticiposOCS.idCompraOCS=" + idOCS+@"
                                order by pago.Fecha desc";
                                txt.Text = miClase.EjecutaEscalarStr(sqlQuery);
                            }
                            else
                            {
                                txt.Text = "SIN PAGOS";
                            }
                            txt.Visible = true;
                            txt.Enabled = false;
                            btn = (Button)icc.Find("btnBorraAnticipo", false)[0];
                            btn.Visible = true;
                        }
                        else
                        {
                            btn.Visible = true; // Habilitar el botón para solicitar anticipo.
                            btn.Enabled = true;
                            btn = (Button)icc.Find("btnBorraAnticipo", false)[0];
                            btn.Visible = false; // No hay anticipo x tanto no hay como borrarlo
                            // Como no hay anticipo tampoco habrá fecha de vencimiento ni fecha de pago.
                            txt.Visible = false;
                            txt = (TextBox)icc.Find("txtVenceAnt", false)[0];
                            txt.Visible = false;
                            txt = (TextBox)icc.Find("txtPagoAnticipo", false)[0];
                            txt.Visible = false;
                        } // Fin anticipos.

                        // Factura Prepago:
                        txt = (TextBox)icc.Find("txtFactPrepago", false)[0];
                        btn = (Button)icc.Find("btnFactPrepago", false)[0];
                        sqlQuery = @"select count(*) from TiemposOCS where idCompraPrep is not null and idCompra=" + idOCS;
                        if (miClase.EjecutaEscalar(sqlQuery) > 0)
                        {
                            sqlQuery = @"select idCompraPrep from TiemposOCS where idCompraPrep is not null and idCompra=" + idOCS;
                            int idCompraFactPrepago = miClase.EjecutaEscalar(sqlQuery);

                            sqlQuery = @"select count(numero) from compra where idCompra=" + idCompraFactPrepago;
                            if (miClase.EjecutaEscalar(sqlQuery) > 0)
                            {
                                // Existe la factura de compra a la que se hace referencia en TiemposOCS.idCompraPrep
                                sqlQuery = @"select numero from compra where idCompra=" + idCompraFactPrepago;
                                btn.Visible = false;
                                txt.Visible = true;
                                txt.Enabled = false;
                                txt.Text = miClase.EjecutaEscalarStr(sqlQuery); // Hasta aquí está cargado el número de la factura prepago en caso de que haya sido registrado
                                // Habilito el botón para eliminar el ligue entre la factura prepago y la OCS
                                btn = (Button)icc.Find("btnEliminar", false)[0];
                                btn.Visible = true;
                                // Oculto el botón de buscar la factura:
                                btn = (Button)icc.Find("btnBuscaFact", false)[0];
                                btn.Visible = false;

                                // Inicio carga de vencimiento de factura prepago
                                txt = (TextBox)icc.Find("txtVencePrepago", false)[0];
                                sqlQuery = @"select cast(Convert(varchar(10),CONVERT(date,Compra.FechaVencimiento,106),103) as nvarchar(10)) from compra where idCompra=" + idCompraFactPrepago;
                                txt.Text = miClase.EjecutaEscalarStr(sqlQuery); // Hasta aquí está cargado el número de la factura prepago en caso de que haya sido registrado
                                txt.Visible = true;
                                txt.Enabled = false;
                                
                                
                                // Inicio carga de pagos de factura final
                                // Verificar si exiten pagos:
                                sqlQuery = @"
                                    SELECT  COUNT(pago.Pago)
                                    FROM         Pago RIGHT OUTER JOIN
                                                          Compra ON Pago.idCompra = Compra.idCompra 
                                    where compra.idCompra=" + idCompraFactPrepago;
                                txt = (TextBox)icc.Find("txtPagoPrepago", false)[0];
                                if (miClase.EjecutaEscalar(sqlQuery) > 0)
                                {
                                    sqlQuery = @"
                                        SELECT  top(1)  cast(Convert(varchar(10),CONVERT(date,Pago.Fecha,106),103) as nvarchar(10))
                                        FROM         Pago RIGHT OUTER JOIN
                                                              Compra ON Pago.idCompra = Compra.idCompra
                                        where compra.idCompra=" + idCompraFactPrepago+@"
                                        order by pago.Fecha desc";
                                    txt.Text = miClase.EjecutaEscalarStr(sqlQuery);
                                }
                                else
                                {
                                    txt.Text = "SIN PAGOS";
                                }
                                txt.Visible = true;
                                txt.Enabled = false;
                            }
                            else
                            {
                                txt.Text = "#ErrorND"; // A pesar de que en tiemposOCS.idCompraPrep existe un idCompra este no existe en la tabla compra.
                                // Por ende no tengo vencimientos y peor aún pagos.
                                txt = (TextBox)icc.Find("txtVencePrepago", false)[0];
                                txt.Text = "#ErrorND";
                                txt.Visible = true;
                                txt.Enabled = false;
                                txt = (TextBox)icc.Find("txtPagoPrepago", false)[0];
                                txt.Text = "#ErrorND";
                                txt.Visible = true;
                                txt.Enabled = false;
                                // Aquí debería hacer un update para setear tiemposOCS.idCompraPrep = null. A pesar de que existe restricción en la bdd xq ese campo está como
                                // references compra (idCompra)
                                sqlQuery = "update TiemposOCS set idCompraPrep=null where idCompra="+idOCS;
                                miClase.EjecutaSql(sqlQuery, false);
                            }
                        } 
                        else
                        {
                            // Si no hay factura prepago habilito el botón para que puedan ligar.
                            btn.Visible = true;
                            txt.Visible = false;
                            txt = (TextBox)icc.Find("txtVencePrepago", false)[0];
                            txt.Visible = false;
                            txt = (TextBox)icc.Find("txtPagoPrepago", false)[0];
                            txt.Visible = false;
                            // Oculto el botón para eliminar el ligue entre la factura prepago y la OCS
                            btn = (Button)icc.Find("btnEliminar", false)[0];
                            btn.Visible = false;
                        } // Fin fact. prepago
                    } break;
                case 3:
                    {

                    } break;
            }
            
        }

        public bool saveData(Panel p1, int idOCS)
        {
            icc = p1.Controls;
            String sqlUpdate;
            short nroRegistros = 0; // Flag q indica el número de campos que se incluyen en el update;
            /*
             * Códigos
             * */
            String[,] controls = new String[,]{{
                "dtpSolCot",            
                "dtpRecibCot",          
                "dtpApruebaCot",       
                "dtpFinManufactT",      
                "dtpFinManufactR",      
                "dtpDespachoFab",       
                "dtpLlegaPuertoIntl"    
            },{
              "Compra.Fecha",
              "TiemposOCS.recibeCotizacion",
              "Compra.FechaRevision",
              "Compra.FechaVencimiento",
              "compra.FechaEntrega",
              "TiemposOCS.despachoFabrica",
              "TiemposOCS.llegaPuertoIntl"
              }};

            // El panel ya viene validado así que solo voy a guardarlo:
            // Verifico que exista en la tabla TiemposOCS
            sqlQuery = @"select count(*) from TiemposOCS where idCompra=" + idOCS;
            if (miClase.EjecutaEscalar(sqlQuery) < 1)
            {
                sqlQuery = @"insert into TiemposOCS (idCompra) values ("+idOCS+")";
                miClase.EjecutaSql(sqlQuery, false);
            }

            
            // Trabajo solo con los Checked, 2 update, 1 x tabla.
            // Primero tabla Compra.
            nroRegistros = 0;
            sqlUpdate = "update compra set ";
            for (int i = 0; i < controls.Length/2; i++)
            {
                if (controls[1, i].StartsWith("Compra."))
                {
                    dtp = (DateTimePicker)icc.Find(controls[0, i], false)[0];
                    if (dtp.Checked)
                    {
                        sqlUpdate = String.Concat(sqlUpdate, controls[1, i], "='", dtp.Value,"', ");
                        nroRegistros++;
                    }
                }
            }
            sqlUpdate=sqlUpdate.Substring(0, sqlUpdate.Length - 2);
            sqlUpdate = String.Concat(sqlUpdate, " where idCompra=", idOCS);
            try {
                if (nroRegistros > 0)
                    miClase.EjecutaSql(sqlUpdate, false);
            }
            catch (Exception)
            {
                return false;
            }

            // Tabla TiemposOCS
            nroRegistros = 0;
            sqlUpdate = "update TiemposOCS set ";
            for (int i = 0; i < controls.Length / 2; i++)
            {
                if (controls[1, i].StartsWith("TiemposOCS."))
                {
                    dtp = (DateTimePicker)icc.Find(controls[0, i], false)[0];
                    if (dtp.Checked)
                    {
                        sqlUpdate = String.Concat(sqlUpdate, controls[1, i], "='", dtp.Value, "', ");
                        nroRegistros++;
                    }
                }
            }
            sqlUpdate = sqlUpdate.Substring(0, sqlUpdate.Length - 2);
            sqlUpdate = String.Concat(sqlUpdate, " where idCompra=", idOCS);
            try
            {
                if (nroRegistros>0)
                    miClase.EjecutaSql(sqlUpdate, false);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public void anticipos(Panel p1, int idOCS)
        {
            icc = p1.Controls;
            /*
            Debo guardar en la tabla compra los siguientes campos:
             * Compra.FechaCaducidad  // Fecha máxima de pago del anticipo
             * Compra.Pedido // Nombre de IG en formato IG-###-20##
             * Compra.Depart // Número referencial de la factura final 
             * Compra.Contado // Valor del anticipo
            */

            DateTime fechaCaducidad;
            float valorAnticipo;
            int idArticulo;
            String nombreIG, factFin;
            
            // La vista se encarga de validar la coherencia de los datos.
            // Fecha máxima de pago de anticipo
            dtp = (DateTimePicker)icc.Find("dtpVenceAnticipo", false)[0];
            fechaCaducidad = dtp.Value;

            // Nombre de IG en formato IG-###-20##
            cmb = (ComboBox)icc.Find("cmbCargoIG", false)[0];
            idArticulo = Int32.Parse(cmb.SelectedValue.ToString());
            sqlQuery = @"
                select (substring(Articulo,1,6)+'-2'+substring(Articulo,len(Articulo)-2,5)) as Articulo
                from Articulo where idArticulo="+idArticulo;
            nombreIG = miClase.EjecutaEscalarStr(sqlQuery);

            // Número referencial de la factura final 
            txt = (TextBox)icc.Find("txtValAnticipo", false)[0];
            factFin = txt.Text;
            
            // Valor del anticipo
            txt = (TextBox)icc.Find("txtValAnticipo", false)[0];
            float.TryParse(txt.Text, out valorAnticipo);

            sqlQuery = @"
            update compra set
                Compra.FechaCaducidad='" + fechaCaducidad.ToString() + @"',
                Compra.Pedido='" + nombreIG + @"',
                Compra.Depart='" + factFin + @"',
                Compra.Contado=" + valorAnticipo + @"
            where idCompra=" + idOCS;

            miClase.EjecutaSql(sqlQuery, false);

            // Llamar al SP:
            miClase.EjecutaSql("exec sp_GeneraSP", false);
            
            // Recuperar el mensaje de error o la SP:
            sqlQuery = @"
            select isnull(compra.Departamento,'')
            from compra
            where idCompra="+idOCS;
            txt = (TextBox)icc.Find("txtMsgAnticipo", false)[0];
            txt.Text=miClase.EjecutaEscalarStr(sqlQuery);
        }

        public void borrarError(int idOCS)
        {
            sqlQuery = @"
                update compra 
                set compra.departamento=null 
                where Compra.Departamento not like 'SP-%' and len(isnull(compra.Departamento,''))>0
                    and compra.idCompra=" + idOCS;
            miClase.EjecutaSql(sqlQuery, false);
        }

        public short borrarAnticipo(int idOCS)
        {
            // Se supone que ya debe venir validado desde la vista.
            short code=0;
            String[,] parametros = {{"vIdCompraOCS", idOCS.ToString()}};
            if (!short.TryParse(miClase.EjecutaSP("sp_eliminaAnticipoOCS", parametros, "@vOutput", SqlDbType.Int), out code)){
                return -100;
            }
            return code;
        }

    }
}




