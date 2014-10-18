using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barco
{
    public partial class TransformaOrdenes : Form
    {
              
        public TransformaOrdenes()
        {
            InitializeComponent();
        }

        Datos miClase = new Datos();
        DataTable dt;
        DataRow myDataRow;
		string Filtro;
        CultureInfo us = new CultureInfo("en-US");		
		DateTime Desde,Hasta;
        int idCompra = 0, idCliente = 0;



        private void TransformaOrdenes_Load(object sender, EventArgs e)
        {
            this.cmbPago.SelectedIndex = 0;

            miClase.LlenaCombo(this.cmbProv, "Cliente", "Nombre", "idCliente", "Proveedor=1 or Ambos=1", false);
            creaDataSet();
            formatoDGV(2);
            if (Datos.idTipoFactura == 4)
            {
                this.Text = "Crea Solicitudes de Pago";
                this.btnTranformacion.Text = "Crea Solicitud";
                this.lblNota.Visible = true;
                this.txtNota.Visible = true;
                this.lblSol.Visible = true;
                this.cmbPago.Visible = true;
            }
            if (Datos.idTipoFactura == 2)
            {
                this.txtNumero.Visible = true;
                this.lblSol.Text = "Número IG";
                this.lblSol.Visible = true;
            }
        }

        void creaDataSet ()
        { 
            dt = new DataTable("Ordenes");
			dt.Columns.Add("idArticulo");
			dt.Columns.Add("Numero");
			dt.Columns.Add("Codigo");
			dt.Columns.Add("Articulo");
			dt.Columns.Add("Cantidad");
			dt.Columns.Add("RefCodigo");
			dt.Columns.Add("RefNumero");
			dt.Columns.Add("idDetCompra");
			dt.Columns.Add("Notas");
			dt.Columns.Add("IdCompra");
            dgvDatos.DataSource = dt;
        }




        void formatoDGV(int dgv) 
        { 
            switch (dgv)
            {
                case 1: { 
                    // Para el dgvOrdenes
                    dgvOrdenes.Columns[0].Width = 200; // Nombre
                    dgvOrdenes.Columns[1].Width = 100; // Numero
                    dgvOrdenes.Columns[2].Width = 80; // Fecha
                    dgvOrdenes.Columns[3].Width = 400; // Notas
                    dgvOrdenes.Columns[4].Visible = false; // idCompra
                } break;

                case 2: {
                    // Para el dgvDatos.
                    dgvDatos.Columns[0].Visible = false; // idArticulo
                    dgvDatos.Columns[1].Width = 80; //Numero
                    dgvDatos.Columns[2].Width = 110; //Codigo
                    dgvDatos.Columns[3].Width = 250; //Articulo
                    dgvDatos.Columns[4].Width = 75; //Cantidad
                    dgvDatos.Columns[4].ReadOnly = false;
                    dgvDatos.Columns[5].Width = 80; //RefCodigo
                    dgvDatos.Columns[6].Width = 80; //RefNumero
                    dgvDatos.Columns[7].Visible=false; //idDetCompra
                    dgvDatos.Columns[8].Visible = false; // Notas
                    dgvDatos.Columns[9].Visible = false; // idCompra
                } break;
            }
        }


        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string Sql = "SELECT dbo.Cliente.Nombre, dbo.Compra.Numero, dbo.Compra.Fecha, dbo.Compra.Notas,dbo.Compra.idCompra"
                + " FROM dbo.Cliente INNER JOIN dbo.Compra ON dbo.Cliente.idCliente = dbo.Compra.idCliente"
                + " Where idTipoFactura=" + Datos.idTipoFactura + " And Compra.Borrar=0 ";

            Filtro = "";

            if (chkProveedor.Checked == false) this.Filtro += " And dbo.Compra.IdCliente=" + cmbProv.SelectedValue + "";

            if (chkFechas.Checked == false)
            {
                if (chkFechaIngreso.Checked == true)
                {
                    string Estacion = miClase.EjecutaEscalarStr("Select host_name()");
                    Desde = (DateTime)dtpDesde.Value;
                    Hasta = (DateTime)dtpHasta.Value;
                    this.Filtro += " And dbo.Compra.FechaIngreso>='" + Desde.ToString("yyyyMMdd") + "'"
                        + " And dbo.Compra.FechaIngreso <'" + Hasta.AddDays(1).ToString("yyyyMMdd") + "'"
                        + " And dbo.Compra.Estacion='" + Estacion + "'";
                }
                else
                {
                    Desde = (DateTime)dtpDesde.Value;
                    Hasta = (DateTime)dtpHasta.Value;
                    this.Filtro += " And dbo.Compra.Fecha>='" + Desde.ToString("yyyyMMdd") + "' And dbo.Compra.Fecha <'" + Hasta.AddDays(1).ToString("yyyyMMdd") + "'";
                }
            }
            if (Datos.idTipoFactura == 4) this.Filtro += " And IsNull(dbo.Compra.Otro,'') not like 'SP%'";

            this.miClase.LlenaGrid(dgvOrdenes, "Compra", Sql + Filtro);
            formatoDGV(1);
            //this.Formato();
        }

        private void dgvOrdenes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string numero = dgvOrdenes[1, dgvOrdenes.CurrentCell.RowIndex].Value.ToString();
            string nombre = dgvOrdenes[0, dgvOrdenes.CurrentCell.RowIndex].Value.ToString();
            this.idCliente = miClase.EjecutaEscalar("Select idCliente From Cliente Where Nombre='" + nombre + "'");
            this.idCompra = Int32.Parse(dgvOrdenes[4,dgvOrdenes.CurrentCell.RowIndex].Value.ToString());
            if (existeAnticipo(idCompra))
            {
                DialogResult dr1=
                    MessageBox.Show("Orden de Compra " + nombre + @" tiene solicitado un anticipo. TENGA EN CUENTA que no se permite consolidar esta orden de manera parcial."+ 
                                " En caso de que deje ítems pendientes de consolidación en esta Orden de Compra estos serán eliminados así como la orden de compra. Desea Continuar?", "Anticipo en OCS",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    this.LeeRegistro(idCompra);
                }
            }
            else
            {
                this.LeeRegistro(idCompra);
            }
        }


        bool existeAnticipo(int idCompra)
        {
            string sqlQuery = @"select count(*) from controlAnticiposOCS where idCompraOCS="+idCompra;
            if (miClase.EjecutaEscalar(sqlQuery) > 0)
                return true;
            return false;
        }


        void LeeRegistro(int idCompra)
        {
            string strSql = " SELECT Articulo.idArticulo,Compra.Numero, Articulo.Codigo, Articulo.Articulo, DetCompra.Cantidad, DetCompra.RefCodigo,"
                         + " DetCompra.RefNumero,DetCompra.idDetCompra,DetCompra.Notas,DetCompra.IdCompra"
                         + " FROM Compra INNER JOIN DetCompra ON Compra.idCompra = DetCompra.idCompra INNER JOIN"
                         + " Articulo ON DetCompra.idArticulo = Articulo.idArticulo Where Compra.idCompra=" + idCompra + "";
            try
            {
                Datos.ocomando = new OleDbCommand(strSql, miClase.cadcon);
                if (miClase.cadcon.State == ConnectionState.Closed) miClase.cadcon.Open();
                Datos.odatareader = Datos.ocomando.ExecuteReader();
                while (Datos.odatareader.Read())
                {
                    this.CopiaFila(Datos.odatareader[0].ToString(),
                        Datos.odatareader[1].ToString(), Datos.odatareader[2].ToString(),
                        Datos.odatareader[3].ToString(), Datos.odatareader[4].ToString(),
                        Datos.odatareader[5].ToString(), Datos.odatareader[6].ToString(),
                        Datos.odatareader[7].ToString(), Datos.odatareader[8].ToString(),
                        Datos.odatareader[9].ToString());

                }
                Datos.odatareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                miClase.cadcon.Close();
            }
        }


        void CopiaFila(string idArticulo, string Numero, string Codigo, string Articulo, string Cantidad, string RefCodigo, string RefNumero, string idDetCompra, string Notas, string idCompra)
        {
            myDataRow = dt.NewRow();
            myDataRow["idArticulo"] = idArticulo;
            myDataRow["Numero"] = Numero;
            myDataRow["Codigo"] = Codigo;
            myDataRow["Articulo"] = Articulo;
            myDataRow["Cantidad"] = Cantidad;
            myDataRow["RefCodigo"] = RefCodigo;
            myDataRow["RefNumero"] = RefNumero;
            myDataRow["idDetCompra"] = idDetCompra;
            myDataRow["Notas"] = Notas;
            myDataRow["idCompra"] = idCompra;
            dt.Rows.Add(myDataRow);
        }

        private void btnLimpia_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Esta seguro de borrar el contenido de las ordenes seleccionadas",
                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                dt.Rows.Clear();
                errorProvider1.Dispose();
            }
        }

        private void btnTranformacion_Click(object sender, EventArgs e)
        {
            if (Datos.idTipoFactura == 2)
            {
                errorProvider1.Dispose();
                if (txtNumero.Text.Length == 0)
                    errorProvider1.SetError(txtNumero, "Debe indicar el número de la IG (Destino)");
                else
                {
                    if ((txtNumero.Text.Substring(0, 3).CompareTo("IG-") != 0) || (txtNumero.Text.Length != 11) || (txtNumero.Text.Substring(2, 1).CompareTo("-") != 0)
                        || (txtNumero.Text.Substring(6, 1).CompareTo("-") != 0) || (txtNumero.Text.Substring(7, 2).CompareTo("20") != 0))
                    {
                        errorProvider1.SetError(txtNumero, "El valor debe cumplir con el formato: IG-XXX-20XX");
                    }
                    else
                    {
                        // Validar que ese número de IG aún no exista creada como idtipofactura=2
                        if (miClase.EjecutaEscalar("Select count(idcompra) from compra where idtipofactura=2 and borrar=0 and numero='" + txtNumero.Text + "'") > 0)
                        {
                            errorProvider1.Dispose();
                            errorProvider1.SetError(txtNumero, "El número de IG asignado ya existe como orden de compra");
                        }
                        else
                        {
                            if (dt.Rows.Count < 2)
                            {
                                errorProvider1.Dispose();
                                MessageBox.Show("Se deben seleccionar al menos dos órdenes de compra para consolidar", "Datos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                errorProvider1.Dispose();
                                if (DialogResult.Yes == MessageBox.Show("Esta seguro de ejecutar la Transaccion", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                    this.Transforma();
                            }
                        }
                    }
                }
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Esta seguro de ejecutar la Transaccion", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.TransformaSolicitud();
                }
            }
        }

        void Transforma()
        {
            string strSql = ""; double Cantidad = 0;
            double Precio = 0;
            string fecha, fecha1, numero, comprobante, refNum, estacionOc, vencimientoOc, fechaNormal, notas, departamento;  //CJ
            int temporal = 0, idProveedor = 0; // CJ


            // Para determinar que cliente debe ir asignado a la OC (El que mas tiene en detcompra.)		
            string[] compraNumero = new string[99];
            int[] idCliente = new int[99];
            int[] nroVeces = new int[99];
            // Número de las órdenes de compra. Esto es para determinar que proveedor va como principal en la orden de compra.

            // Esta es para actualizar los anticipos en caso de que hayan pedido anticipo y que el mismo no esté cargado a la IG actual.
            string[] detNumero = new string[99];
            for (int i = 0; i < 99; i++)
            {
                compraNumero[i] = detNumero[i] = "";
                idCliente[i] = nroVeces[i] = 0;
            }

            int idcliente = 0;
            int flag;
            string detTemp;

            // Llenar el detNumero[]
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                detTemp = ""; flag = 0;
                if (dgvDatos[6,i].Value.ToString().CompareTo("") != 0)
                {
                    detTemp = dgvDatos[6,i].Value.ToString();
                    // Busco el valor dentro del vector
                    flag = 0;
                    for (int j = 0; j < 99; j++)
                    {
                        if (detNumero[j] == detTemp)
                        {
                            flag++; // Encontrado
                            break;
                        }
                        if (j <= 96)
                        {
                            if (detNumero[j].CompareTo("") == 0 && detNumero[j + 1].CompareTo("") == 0 && detNumero[j + 2].CompareTo("") == 0)
                                break;
                        }
                    }
                    if (flag == 0) // no encontró
                    {
                        for (int j = 0; j < 99; j++) // Buscar posición vacía
                        {
                            if ((detNumero[j].CompareTo("")) == 0)
                            {
                                detNumero[j] = detTemp;// Insertar aquí
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flag = 0;
                idcliente = miClase.EjecutaEscalar("select compra.idcliente from compra where idcompra=" + dgvDatos[9,i].Value.ToString());
                for (int j = 0; j < 99; j++)
                {
                    if (idCliente[j] == idcliente)
                    {
                        flag++; // Encontrado
                        nroVeces[j]++;
                        break;
                    }
                    if (j <= 96)
                    {
                        if (idCliente[j] == 0 && idCliente[j + 1] == 0 && idCliente[j + 2] == 0)
                            break;
                    }
                }
                if (flag == 0) // Si no lo encontró.
                {
                    for (int j = 0; j < 99; j++)
                    {
                        // Busco posición vacía
                        if (idCliente[j] == 0)
                        {
                            idCliente[j] = idcliente;// Almacenar aquí
                            compraNumero[j] = miClase.EjecutaEscalarStr("select compra.numero from compra where idcompra=" + dgvDatos[9,i].Value.ToString());
                            nroVeces[j] = 1;
                            break;
                        }
                    }
                }
            }
            int clienteMax = idCliente[0], cuentaMax = nroVeces[0];
            for (int i = 1; i < 99; i++)
            {
                if (nroVeces[i] > cuentaMax)
                {
                    clienteMax = idCliente[i];
                    cuentaMax = nroVeces[i];
                }
                if (i <= 96)
                {
                    if (idCliente[i] == 0 && idCliente[i + 1] == 0 && idCliente[i + 2] == 0)
                        break; // Para que seguir buscando...
                }
            }

            this.miClase.EjecutaSql("Insert Into Compra (idTipoFactura,Usuario,IdComprobante,numero, idCliente,notas) values(" + Datos.idTipoFactura + ",'OrdenLotes',25,'" + txtNumero.Text + "'," + clienteMax + ",'"+txtNota.Text.ToString()+"')", false);
            this.idCompra = miClase.EjecutaEscalar("Select Top 1 idCompra From Compra Where idTipoFactura=" + Datos.idTipoFactura + " And Usuario='OrdenLotes' Order by IdCompra Desc");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cantidad = this.miClase.EjecutaEscalarF("Select cantidad From DetCompra Where IdDetCompra=" + dgvDatos[7,i].Value.ToString() + "");
                Precio = this.miClase.EjecutaEscalarF("Select precio  From DetCompra Where IdDetCompra=" + dgvDatos[7, i].Value.ToString() + "");
                fecha = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),FechaRevision,12) as varchar(30)),CAST('20'+convert(varchar(15),GETDATE(),12) as varchar(30))) as fechaRevision from compra where idcompra=" + dgvDatos[9, i].Value.ToString() + ""); //CJ
                if ((Convert.ToDouble(dgvDatos[4, i].Value.ToString()) <= Cantidad) && (Cantidad != 0))
                {
                    strSql = "Update DetCompra Set Cantidad=Cantidad-" + dgvDatos[4,i].Value.ToString() + " Where IdDetCompra=" + dgvDatos[7,i].Value.ToString() + "";
                    miClase.EjecutaSql(strSql, false);
                    refNum = dgvDatos[6, i].Value.ToString();
                    if (refNum.CompareTo("") == 0) // Tengo que insertar la compra.fechaRevision
                    {
                        fecha = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),fechaRevision,12) as varchar(30)),CAST('20'+convert(varchar(15),GETDATE(),12) as varchar(30))) as fechaRevision from Compra where idCompra=" + dgvDatos[9,i].Value.ToString() + "");
                        strSql = "Insert Into DetCompra (idCompra,IdArticulo,Cantidad,RefNumero,Precio,Vencimiento) values(" + idCompra + "," + dgvDatos[0, i].Value.ToString() + "," + dgvDatos[4, i].Value.ToString() + ",'" + dgvDatos[1, i].Value.ToString() + "'," + Precio.ToString("###.##0.00", us) + ", '" + fecha + "')";
                    }
                    else // Respeto detcompra.vencimiento y detcompra.refNumero.
                    {
                        fecha = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),Vencimiento,12) as varchar(30)),CAST('20'+convert(varchar(15),GETDATE(),12) as varchar(30))) as vencimiento from detCompra where iddetcompra=" + dgvDatos[7, i].Value.ToString() + ""); //CJ
                        strSql = "Insert Into DetCompra (idCompra,IdArticulo,Cantidad,RefNumero,Precio,Vencimiento) values(" + idCompra + "," + dgvDatos[0, i].Value.ToString() + "," + dgvDatos[4, i].Value.ToString() + ",'" + refNum + "'," + Precio.ToString("###.##0.00", us) + ", '" + fecha + "')";
                    }
                    miClase.EjecutaSql(strSql, false);

                    if (temporal != Int32.Parse(dgvDatos[9, i].Value.ToString())) //CJ Esta línea detecta si hubo un cambio de idCompra en el dgvDatos al momento de migrar (mientras se recorre el dgv)
                    {
                        // Esto me permite crear un registro en la tabla controlOCLotes por cada OC y no por cada detCompra
                        temporal = Int32.Parse(dgvDatos[9, i].Value.ToString());
                        fecha1 = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),FechaIngreso,12) as varchar(30)),CAST('20'+convert(varchar(15),fechaIngreso,12) as varchar(30))) as fechaIngreso from compra where idcompra=" + temporal + "");
                        numero = this.miClase.EjecutaEscalarStr("select isnull(numero,'') as numero from compra where idCompra=" + temporal + "");
                        comprobante = this.miClase.EjecutaEscalarStr("select isnull(comprobante,'') from compra where idcompra=" + temporal + " and idtipofactura=2");
                        // aquí recuperar la data.
                        vencimientoOc = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),FechaVencimiento,12) as varchar(30)),CAST('20'+convert(varchar(15),fechaVencimiento,12) as varchar(30))) as fechaVencimiento from compra where idcompra=" + temporal + "");
                        fechaNormal = this.miClase.EjecutaEscalarStr("select isnull(CAST('20'+convert(varchar(15),Fecha,12) as varchar(30)),CAST('20'+convert(varchar(15),fecha,12) as varchar(30))) as fecha from compra where idcompra=" + temporal + "");
                        estacionOc = this.miClase.EjecutaEscalarStr("select isnull(estacion,'') as estacion from compra where idCompra=" + temporal + "");
                        idProveedor = this.miClase.EjecutaEscalar("select top(1)idCliente as idCliente from compra where idCompra=" + temporal + "");
                        notas = this.miClase.EjecutaEscalarStr("select compra.Notas from Compra where idTipoFactura=2 and idcompra=" + temporal + "");
                        departamento = this.miClase.EjecutaEscalarStr("select isnull(compra.departamento,'') from compra where idtipofactura=2 and idcompra=" + temporal + "");
                        strSql = "insert into controlOCLotes (idcompra, idcompra2,fechaIngreso, numero, comprobante, idCliente, estacionOC, fechaVencimiento, fecha, notas, departamento)" +
                                "values (" + this.idCompra + "," + temporal + ",'" + fecha1 + "','" + numero + "','" + comprobante + "'," + idProveedor + ",'" + estacionOc + "','" + vencimientoOc + "','" + fechaNormal + "','" + notas + "','" + departamento + "')";
                        miClase.EjecutaSql(strSql, false);
                        if (i > 0)
                        {
                            // Borrar el registro de la OCS en caso de que tenga anticipos solicitados. Esto elimina la posibilidad de que puedan partir una OCS con anticipo en 2
                            if (i == (dt.Rows.Count-1))
                            {
                                if (existeAnticipo(Int32.Parse(dgvDatos[9, i].Value.ToString())))
                                {
                                    strSql = @"delete from compra where idCompra = " + dgvDatos[9, i].Value.ToString();
                                    miClase.EjecutaSql(strSql, false);
                                }
                            }
                            else
                            {
                                if (existeAnticipo(Int32.Parse(dgvDatos[9, i - 1].Value.ToString())))
                                {
                                    strSql = @"delete from compra where idCompra = " + dgvDatos[9, i - 1].Value.ToString();
                                    miClase.EjecutaSql(strSql, false);
                                }
                            }
                        }
                    }
                }
            }
            strSql = "exec facturaTotal " + this.idCompra;
            miClase.EjecutaSql(strSql, false);

            strSql = "Delete DetCompra FROM  DetCompra INNER JOIN  Compra ON DetCompra.idCompra = Compra.idCompra"
                  + " WHERE (DetCompra.Cantidad < 0.01) AND (Compra.idTipoFactura = " + Datos.idTipoFactura + ")";
            miClase.EjecutaSql(strSql, false);

            //Borrar las OC que ya no tienen ninguna línea en el detalle.
            strSql = " delete from Compra where idTipoFactura=2 and idCompra not in "
                    + " (select compra.idCompra from Compra inner join DetCompra on compra.idCompra=DetCompra.idCompra "
                    + " where compra.idTipoFactura=2 "
                    + " group by compra.idCompra "
                    + " having count(detcompra.idDetCompra)>0) ";
            miClase.EjecutaSql(strSql, false);

            // Borrar las ordenes de compra que tienen solicitados anticipos y que fueron seleccionadas para consolidación, independientemente si quedaron con registros detcompra activos.
            // PENDIENTE

            //Acá uso los vectores para actualizar el campo compra.pedido FASE 1
            for (int i = 0; i < 99; i++)
            {
                if (compraNumero[i].CompareTo("") != 0)
                {
                    // Actualizar las compras y sp que hagan relación a otra IG distinta a la que este momento se está consolidando.
                    if (miClase.EjecutaEscalar("select count(idCompra) from compra where idTipoFactura in (4,26) and mensaje2='" + compraNumero[i] + "' and pedido like 'IG-%' and pedido<>'" + txtNumero.Text + "'") > 0)
                    {
                        miClase.EjecutaSql("update compra set pedido='" + txtNumero.Text + "' where idTipoFactura in (4,26) and mensaje2='" + compraNumero[i] + "'", false);
                    }
                }
                if (i <= 96)
                {
                    if (idCliente[i] == 0 && idCliente[i + 1] == 0 && idCliente[i + 2] == 0)
                        break; // Para que seguir buscando...
                }
            }

            //Acá uso los vectores para actualizar el campo compra.pedido FASE 2
            for (int i = 0; i < 99; i++)
            {
                if (detNumero[i].CompareTo("") != 0)
                {
                    // Actualizar las compras y sp que hagan relación a otra IG distinta a la que este momento se está consolidando.
                    if (miClase.EjecutaEscalar("select count(idCompra) from compra where idTipoFactura in (4,26) and mensaje2='" + detNumero[i] + "' and pedido like 'IG-%' and pedido<>'" + txtNumero.Text + "'") > 0)
                    {
                        miClase.EjecutaSql("update compra set pedido='" + txtNumero.Text + "' where idTipoFactura in (4,26) and mensaje2='" + detNumero[i] + "'", false);
                    }
                }
                if (i <= 96)
                {
                    if ((detNumero[i].CompareTo("")) == 0 && (detNumero[i + 1].CompareTo("")) == 0 && (detNumero[i + 2].CompareTo("")) == 0)
                        break; // Para que seguir buscando...
                }
            }
            // Actualiza el cargo a la IG en el detcompra de las facturas de ISD del ancitipo.
            strSql = @"
				declare @vNumero varchar(25), @vPedido varchar(25), @vIdDetCompra int, @vIdArticulo int, @vArticulo varchar(50), @vIdArticulo2 int
				declare actualiza scroll cursor for
					select compra.numero, compra.pedido, idDetCompra, Articulo.idarticulo, Articulo.Articulo
					from Compra 
						inner join DetCompra on compra.idCompra=DetCompra.idCompra 
						inner join Articulo on detcompra.idArticulo=Articulo.idArticulo
					where compra.Pedido like 'IG-%' and compra.Numero like '%ISD' and compra.idTipoFactura =4 
						and compra.Usuario='SPLotes' and DetCompra.idArticulo<>10317 and (SUBSTRING(compra.Pedido, 1,6)+'/'+SUBSTRING(compra.Pedido, 9,3))<>Articulo.Articulo	
				open actualiza
					fetch next from actualiza into @vNumero, @vPedido, @vIdDetCompra, @vIdArticulo, @vArticulo
					while (@@FETCH_STATUS<>-1)
					begin
						if(@@FETCH_STATUS<>-2)
						begin
							set @vIdArticulo2=0
							select @vIdArticulo2=isnull(idarticulo,0) from Articulo where Articulo=(select SUBSTRING(@vPedido, 1,6)+'/'+SUBSTRING(@vPedido, 9,3))
							if (@vIdArticulo2<>0)
							begin
								update DetCompra set idArticulo=@vIdArticulo2 where idDetCompra=@vIdDetCompra
							end
							else
							begin
								raiserror ('No se encontró un artículo.idArticulo asociado a la IG.',10,1)
							end
						end
						fetch next from actualiza into @vNumero, @vPedido, @vIdDetCompra, @vIdArticulo, @vArticulo
					end
				close actualiza
				deallocate actualiza
			";
            miClase.EjecutaSql(strSql, false);

            // Actualiza el cargo a la IG en el detcompra de las facturas finales generadas por el sistema
            strSql = @"
				declare @vNumero varchar(25), @vPedido varchar(25), @vIdDetCompra int, @vIdArticulo int, @vArticulo varchar(50), @vIdArticulo2 int
				declare actualiza scroll cursor for
					select compra.numero, compra.pedido, idDetCompra, Articulo.idarticulo, Articulo.Articulo
					from Compra 
						inner join DetCompra on compra.idCompra=DetCompra.idCompra 
	                    inner join Articulo on detcompra.idArticulo=Articulo.idArticulo
	                    inner join cliente on compra.idcliente=cliente.idcliente
                    where compra.Pedido like 'IG-%' and compra.Numero not like '%ISD' and compra.idTipoFactura =4 
	                    and compra.Usuario<>'SPLotes' and DetCompra.idArticulo<>10317 and (SUBSTRING(compra.Pedido, 1,6)+'/'+SUBSTRING(compra.Pedido, 9,3))<>Articulo.Articulo	
	                    and cliente.nombre like 'PE%'
				open actualiza
					fetch next from actualiza into @vNumero, @vPedido, @vIdDetCompra, @vIdArticulo, @vArticulo
					while (@@FETCH_STATUS<>-1)
					begin
						if(@@FETCH_STATUS<>-2)
						begin
							set @vIdArticulo2=0
							select @vIdArticulo2=isnull(idarticulo,0) from Articulo where Articulo=(select SUBSTRING(@vPedido, 1,6)+'/'+SUBSTRING(@vPedido, 9,3))
							if (@vIdArticulo2<>0)
							begin
								update DetCompra set idArticulo=@vIdArticulo2 where idDetCompra=@vIdDetCompra
							end
							else
							begin
								raiserror ('No se encontró un artículo.idArticulo asociado a la IG.',10,1)
							end
						end
						fetch next from actualiza into @vNumero, @vPedido, @vIdDetCompra, @vIdArticulo, @vArticulo
					end
				close actualiza
				deallocate actualiza
			";
            miClase.EjecutaSql(strSql, false);


            // Corregir los detcompra.idArticulo 'Anticipo ISD ' de los SP.
            strSql = @"DECLARE @nIG varchar(25)='" + txtNumero.Text + "', @vIdArticulo int=0  ";
            strSql = strSql +
                @"
					select top(1) @vIdArticulo=isnull(Articulo.idArticulo,0) 
						from Articulo where Articulo.Articulo=(SUBSTRING(@nIG, 1,6)+'/'+SUBSTRING(@nIG, 9,3))
					update DetCompra set idArticulo=@vIdArticulo 
					from DetCompra 
						inner join Compra on DetCompra.idCompra=compra.idCompra
						inner join Articulo on detcompra.idArticulo=Articulo.idArticulo
					where compra.idTipoFactura=26 and compra.Pedido=@nIG and detcompra.Notas='ISD Anticipo'
				";

            miClase.EjecutaSql(strSql, false);

            MessageBox.Show("Transformacion Terminada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }




        void TransformaSolicitud()
        {
            string strSql = "", Numero = ""; double Precio = 0, Impuesto = 0;
            int idProy = 0, idSubProy = 0, idCliente = 0;
            DateTime Fecha;
            string Pago = cmbPago.Text;
            if (cmbProv.Enabled == true)
                idCliente = (int)this.cmbProv.SelectedValue;

            this.miClase.EjecutaSql("sp_CreaSolicitudPago " + idCliente + " ,'" + txtNota.Text + "','" + cmbPago.Text + "'", false);
            this.idCompra = miClase.EjecutaEscalar("Select Top 1 idCompra From Compra Where idTipoFactura=26 And Usuario='SolicitudLotes' Order by IdCompra Desc");
            Numero = miClase.EjecutaEscalarStr("Select Top 1 Numero From Compra Where idTipoFactura=26 And Usuario='SolicitudLotes' Order by IdCompra Desc");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                idProy = this.miClase.EjecutaEscalar("Select idProyecto From DetCompra Where IdDetCompra=" + dgvDatos[7, i].Value.ToString() + "");
                idSubProy = this.miClase.EjecutaEscalar("Select idSubProyecto From DetCompra Where IdDetCompra=" + dgvDatos[7, i].Value.ToString() + "");
                Impuesto = this.miClase.EjecutaEscalarF("Select Impuesto From DetCompra Where IdDetCompra=" + dgvDatos[7, i].Value.ToString() + "");
                Precio = this.miClase.EjecutaEscalarF("Select precio  From DetCompra Where IdDetCompra=" + dgvDatos[7, i].Value.ToString() + "");
                Fecha = this.miClase.EjecutaEscalarDate("Select FechaVencimiento From Compra Where IdCompra=" + dgvDatos[9, i].Value.ToString() + "");
                strSql = "Insert Into DetCompra (idCompra,IdArticulo,Cantidad,RefNumero,Precio,RefCodigo,Notas,Impuesto,idProyecto,idSubProyecto,Vencimiento)"
                        + " values(" + idCompra + "," + dgvDatos[0, i].Value.ToString() + "," + dgvDatos[4, i].Value.ToString() + ","
                        + "'" + dgvDatos[1, i].Value.ToString() + "'," + Precio.ToString("###.##0.00", us) + ",'" + dgvDatos[5, i].Value.ToString() + "','" + dgvDatos[8, i].Value.ToString() + "',"
                        + "" + Impuesto + "," + idProy + "," + idSubProy + ",'" + Fecha.ToString("yyyyMMdd") + "')";
                miClase.EjecutaSql(strSql, false);
                strSql = "Update Compra Set Comprobante='" + Numero + "',Otro='" + Numero + "' Where idCompra=" + dgvDatos[9, i].Value.ToString() + "";
                miClase.EjecutaSql(strSql, false);
            }

            this.miClase.EjecutaSql("Exec FacturaTotal " + idCompra + "", false);

            MessageBox.Show("Transformacion Terminada", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (idCliente == 0)
            {
                MessageBox.Show("Solicitud Creada Sin Proveedor Favor Revisar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dt.Rows.Clear();
            this.txtNota.Text = "";
        }


    }
}
