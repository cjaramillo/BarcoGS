using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Barco.Controllers;

namespace Barco
{
    public partial class InputDataOCS : Form
    {
        private Datos d = new Datos();
        private String sqlQuery;
        private C_inputOCS ci_ocs = new C_inputOCS(); // Controlador
        private int idOCS;

        public InputDataOCS()
        {
            InitializeComponent();
        }

        private void estilo(DataGridView dgv, short nroDGV)
        {
            switch (nroDGV)
            {
                case 1:
                    {
                        dgv.Columns[0].Name = "idCompra";
                        dgv.Columns[0].Visible = false;
                        dgv.Columns[1].Name = "idCliente";
                        dgv.Columns[1].Visible = false;
                        dgv.Columns[2].Name = "idAnticipo";
                        dgv.Columns[2].Visible = false;
                        dgv.Columns[3].Name = "Numero";
                        dgv.Columns[3].Width = 100; // Numero
                        dgv.Columns[4].Name = "Fecha";
                        dgv.Columns[4].Width = 100; // Fecha
                        dgv.Columns[5].Name = "Proveedor";
                        dgv.Columns[5].Width = 120; // Proveedor
                        dgv.Columns[6].Name = "Estacion";
                        dgv.Columns[6].Width = 80; // Estación
                        dgv.Columns[7].Name = "Usuario";
                        dgv.Columns[7].Width = 100; // Usuario
                        dgv.Columns[8].Name = "Total";
                        dgv.Columns[8].DefaultCellStyle.Format = "F";
                        dgv.Columns[8].Width = 80; // Total
                        dgv.Columns[9].Name = "SPAnticipo";
                        dgv.Columns[9].Width = 100; // Sp Anticipo
                        dgv.Columns[10].Name = "ValorAnticipo";
                        dgv.Columns[10].DefaultCellStyle.Format = "F";
                        dgv.Columns[10].Width = 80; // Valor Anticipo
                        dgv.Columns[10].Name = "ValorAnticipo";
                        dgv.Columns[11].Name = "Notas";
                        dgv.Columns[11].Width = 200; // Notas
                    } break;
                case 2:
                    {
                        dgv.Columns[0].Name = "idCompra";
                        dgv.Columns[0].Visible = false;
                        dgv.Columns[1].Name = "idCliente";
                        dgv.Columns[1].Visible = false;
                        dgv.Columns[2].Name = "Numero";
                        dgv.Columns[2].Width = 100; // Numero
                        dgv.Columns[3].Name = "Fecha";
                        dgv.Columns[3].Width = 100; // Fecha
                        dgv.Columns[4].Name = "Proveedor";
                        dgv.Columns[4].Width = 120; // Proveedor
                        dgv.Columns[5].Name = "Total";
                        dgv.Columns[5].DefaultCellStyle.Format = "F";
                        dgv.Columns[6].Name = "Notas";
                        dgv.Columns[6].Width = 200; // Notas
                    } break;
            }
        }


        private void slide(short mode)
        {
            // 1 abrir; 2 cerrar
            if (mode != 1 && mode != 2) return;
            for (short i = 1; i < 229; i++)
            {
                if (mode == 1) ++dgvOCS.Height;
                if (mode == 2) --dgvOCS.Height;
            }
            return;
        }



        private void InputDataOCS_Load(object sender, EventArgs e)
        {
            resetForm();
            dgvOCS.Height = 505;
            sqlQuery = @"
                            SELECT		Compra.idCompra, Compra.idCliente,controlAnticiposOCS.idAnticipo, Compra.Numero, CONVERT(CHAR(10), Compra.Fecha, 103) AS Fecha, 
			                            Cliente.Nombre as Proveedor, Compra.Estacion, Compra.Usuario, Compra.Total, isnull(controlAnticiposOCS.nroSP,'') as SpAnticipo, 
			                            isnull(controlAnticiposOCS.valorAnticipo,0) as ValorAnticipo, ISNULL(Compra.Notas, '') AS Notas
                            FROM        Compra LEFT OUTER JOIN
                                                  controlAnticiposOCS ON Compra.idCompra = controlAnticiposOCS.idCompraOCS LEFT OUTER JOIN
                                                  Cliente ON Compra.idCliente = Cliente.idCliente
                            WHERE		(Compra.idTipoFactura = 2) AND (Compra.Usuario <> 'OrdenLotes') AND (Compra.Borrar = 0) 
			                            and compra.idComprobante<>33 and compra.Numero not like '%SEGURIDAD%'
                            ORDER BY    Compra.Fecha desc
                            ";
            d.LlenaGrid(dgvOCS, "Compra", sqlQuery);
            estilo(dgvOCS, 1);
        }


        private void dgvOCS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            if (dgvOCS.Height != 505) return;
            slide(2);
            lblNroOCS.Text = dgvOCS.SelectedRows[0].Cells[3].Value.ToString();
            lblNroOCS.Visible = true;
            dgvOCS.Enabled = false;
            panel1.Visible = true;
            panel2.Visible = true;
            btnSave.Visible = true;
            btnUndo.Visible = true;
            lblHelp.Text = "Presione descartar o guardar para cerrar la OCS";
            // Paso el Panel al controlador para que lo llene x referencia
            idOCS = Int32.Parse(dgvOCS.SelectedRows[0].Cells[0].Value.ToString());
            ci_ocs.fillPanel(panel1, 1, idOCS);
            ci_ocs.fillPanel(panel2, 2, idOCS);
            panel3.Visible = false;
        }


        private void resetForm()
        {
            dtpSolCot.Checked = false;
            dtpRecibCot.Checked = false;
            dtpApruebaCot.Checked = false;
            dtpFinManufactT.Checked = false;
            dtpFinManufactR.Checked = false;
            dtpDespachoFab.Checked = false;
            dtpLlegaPuertoIntl.Checked = false;

            lblSolCot.ForeColor = System.Drawing.Color.Black;
            lblRecibCot.ForeColor = System.Drawing.Color.Black;
            lblApruebaCot1.ForeColor = System.Drawing.Color.Black;
            lblFinManufactT.ForeColor = System.Drawing.Color.Black;
            lblFinManufactR.ForeColor = System.Drawing.Color.Black;
            lblDespachoFab.ForeColor = System.Drawing.Color.Black;
            lblLlegaPuertoIntl.ForeColor = System.Drawing.Color.Black;

        }


        private void btnUndo_Click(object sender, EventArgs e)
        {
            dgvOCS.Enabled = true;
            slide(1);
            lblNroOCS.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            btnSave.Visible = false;
            btnUndo.Visible = false;
            resetForm();
            lblHelp.Text = "Realice doble clic sobre la orden de compra que desea trabajar";
            panel3.Visible = false;
        }


        private void borrarDataP3()
        {
            // Borra los valores de cada control del panel3:
            txtValAnticipo.Text = "";
            txtFactFinal.Text = "";
            txtMsgAnticipo.Text = "";
            dtpVenceAnticipo.Value = new DateTime(2000, 01, 01);
            errorProvider1.Clear();
        }


        private void btnSolAnticipo_Click(object sender, EventArgs e) // PENDIENTE
        {
            borrarDataP3();
            // Cuando se da clic en este botón todas las fechas del panel1 deben guardarse:
            // Validar orden cronológico de las fechas:
            if (!validarOrdenFechas())
            {
                return;
            }
            // Guardar las fechas del panel1:
            // Trabajo solo con los dtp que tienen dato (dtp.Checked)
            // Paso al controlador todo el panel para que lo guarde.
            if (!ci_ocs.saveData(panel1, idOCS))
            {
                MessageBox.Show("Error al guardar el bloque de fechas.");
            }

            // Verificar que tengan el check habilitado:
            if (!dtpSolCot.Checked)
            {
                errorProvider1.SetError(dtpSolCot, "Ingrese Fecha");
                return;
            }

            if (!dtpRecibCot.Checked)
            {
                errorProvider1.SetError(dtpRecibCot, "Ingrese Fecha");
                return;
            }

            if (!dtpApruebaCot.Checked)
            {
                errorProvider1.SetError(dtpApruebaCot, "Ingrese Fecha");
                return;
            }
            btnSolAnticipo.Enabled = false;
            // Borrar código de error en Compra.Departamento en caso de que existiere algo raro ahí escrito.
            ci_ocs.borrarError(idOCS);
            panel3.Visible = true;
            new Datos().LlenaCombo(cmbCargoIG, "Articulo", "Articulo", "idArticulo",
                    @"Articulo.Articulo like 'IG-%' and substring(Articulo.Articulo,7,1)='/' 
                    and LEN(articulo.Articulo)=10 and articulo.dVenta=1 and Articulo.Descontinuado=0", false);

        }


        private void txtValAnticipo_KeyPress(object sender, KeyPressEventArgs e) // Para que acepte solo números
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }


        private void btnProcesaAnticipo_Click(object sender, EventArgs e) // PENDIENTE
        {
            errorProvider1.Clear();

            // Validar que todos los campos requeridos tengan datos.
            float valorAnticipo;
            if (!float.TryParse(txtValAnticipo.Text, out valorAnticipo))
            {
                errorProvider1.SetError(txtValAnticipo, "Ingrese una cantidad válida");
                return;
            }

            // Validar que exista un número de factura final:
            if (txtFactFinal.Text.Trim().Length < 1)
            {
                errorProvider1.SetError(txtFactFinal, "Ingrese un número referencial para la factura final");
                return;
            }

            // Validar que la fecha de pago no sea menor que la fecha de aprobación de la cotización:
            if (dtpApruebaCot.Value > dtpVenceAnticipo.Value)
            {
                errorProvider1.SetError(dtpVenceAnticipo, "La fecha máxima de pago no puede ser menor a la fecha de aprobación de la cotización");
                return;
            }
            // Pasar panel al controlador para que guarde y genere el anticipo.
            if (MessageBox.Show("Está seguro?", "Solicitar Anticipos", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            ci_ocs.anticipos(panel3, idOCS);

            // Si el texto que tengo en el txtMsgAnticipo empieza con SP entonces el proceso fue exitoso:
            // Caso contrario debe ser interpretado como un código de error:
            if (!txtMsgAnticipo.Text.StartsWith("SP-"))
            {
                if (txtMsgAnticipo.Text.CompareTo("ERROR PP") == 0)
                {
                    errorProvider1.SetError(txtMsgAnticipo, "Favor crear el proveedor PP para solicitar anticipos");
                    return;
                }
                if (txtMsgAnticipo.Text.CompareTo("ERROR ISD") == 0)
                {
                    errorProvider1.SetError(txtMsgAnticipo, "No se encontró el artículo asociado a la IG");
                    return;
                }
                if (txtMsgAnticipo.Text.CompareTo("ERROR IG") == 0) // Esto jamás debería ocurrir xq la entrada ya no es manual sino a través de un ComboBox.
                {
                    errorProvider1.SetError(txtMsgAnticipo, "El formato de IG no cumple con el formato IG-###-20##");
                    return;
                }
                errorProvider1.SetError(txtMsgAnticipo, "Error no documentado. Consulte con Sistemas.");
                return;
            }
            // Si llega hasta acá todo está ok. Entonces ocultaré el panel de anticipos.
            MessageBox.Show("Anticipo Solicitado: " + txtMsgAnticipo.Text);
            panel3.Visible = false;

            // Guardo todo y me la saco.
            btnSave_Click(null, null);


            /*
            // Recargar DGV:
            dgvOCS.Enabled = true;
            loadDGV();
            dgvOCS.Enabled = false;

            // Recargo paneles 1 y 2 para que ahora se vea reflejada la transacción:
            ci_ocs.fillPanel(panel1, 1, idOCS);
            ci_ocs.fillPanel(panel2, 2, idOCS);
             * 
             */

        }


        private bool validarOrdenFechas()  // ok.
        {
            // No valido la fecha tentativa, porque es solo esto: TENTATIVA:
            DateTimePicker dtp;
            Control.ControlCollection icc = panel1.Controls;
            DateTime[] fechas = new DateTime[7];
            short flagArray = 0;
            String[] controls = new String[]{
                "dtpSolCot",
                "dtpRecibCot",
                "dtpApruebaCot",
                "dtpFinManufactR",
                "dtpDespachoFab",
                "dtpLlegaPuertoIntl"
            };

            for (int i = 0; i < controls.Length; i++)
            {
                dtp = (DateTimePicker)icc.Find(controls[i], false)[0];
                if (dtp.Checked)
                {
                    fechas[flagArray++] = dtp.Value;
                }
            }
            // Aquí ya tengo todas las fechas metidas en el array, tengo que recorrerlo para comparar:
            for (int i = 0; i < flagArray; i++)
            {
                for (int j = i; j < flagArray; j++)
                {
                    if (fechas[i] > fechas[j])
                    {
                        errorProvider1.SetError(dtpSolCot, "Favor revise las secuencias en fechas; el proceso debe cumplir un orden cronológico.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validar orden cronológico de las fechas:
            if (!validarOrdenFechas())
            {
                return;
            }
            // Guardar las fechas del panel1:
            // Trabajo solo con los dtp que tienen dato (dtp.Checked)
            // Paso al controlador todo el panel para que lo guarde.

            if (!ci_ocs.saveData(panel1, idOCS))
            {
                MessageBox.Show("Error al guardar el panel de fechas");
            }
            // Ocultar botones y paneles
            btnSave.Visible = false;
            btnUndo.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            lblNroOCS.Visible = false;
            slide(1); // Cerrar DGV
            dgvOCS.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            borrarDataP3();
            panel3.Visible = false;
            btnSolAnticipo.Enabled = true;
        }


        #region "BorrarErrorProvider"
        private void txtValAnticipo_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtFactFinal_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpApruebaCot2_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpVenceAnticipo_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpSolCot_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpRecibCot_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpFinManufactT_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpFinManufactR_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpDespachoFab_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpLlegaPuertoIntl_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void dtpApruebaCot_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }


        #endregion

        private void btnBorraAnticipo_Click(object sender, EventArgs e)
        {
            // Borrar el anticipo solitado desde la OCS
            //DialogResult.No
            if (MessageBox.Show("Está seguro?", "Eliminar Anticipo", MessageBoxButtons.YesNo).CompareTo(DialogResult.No) == 0)
            {
                return;
            }
            short code = ci_ocs.borrarAnticipo(idOCS);
            if (code == 1)
            {
                MessageBox.Show("Procesado");
                btnSave_Click(null, null); // Guardo y me la saco.
                return;
            }
            switch (code)
            {
                case -1:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Registro no consta en tabla de anticipos - Comuníquese con sistemas");
                    } break;
                case -10:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un pago en la factura de anticipo. Consulte con contabilidad");
                    } break;
                case -11:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un asiento de la factura de anticipo. Consulte con contabilidad");
                    } break;
                case -12:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe una retención de la factura de anticipo. Consulte con contabilidad");
                    } break;
                case -20:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un pago en la factura de ISD. Consulte con contabilidad");
                    } break;
                case -21:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un asiento de la factura de ISD. Consulte con contabilidad");
                    } break;
                case -22:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe una retención de la factura de ISD. Consulte con contabilidad");
                    } break;
                case -30:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un pago en la factura final. Consulte con contabilidad");
                    } break;
                case -31:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe un asiento de la factura final. Consulte con contabilidad");
                    } break;
                case -32:
                    {
                        errorProvider1.SetError(btnBorraAnticipo, "Existe una retención de la factura final. Consulte con contabilidad");
                    } break;
            }
        }

        private void btnFactPrepago_Click(object sender, EventArgs e)
        {
            btnFactPrepago.Visible = false;
            btnBuscaFact.Visible = true;
            txtFactPrepago.Visible = true;
            txtFactPrepago.Enabled = true;
            txtFactPrepago.Text = "";
            txtFactPrepago.Focus();
        }



        private void btnBuscaFact_Click(object sender, EventArgs e)
        {
            if (txtFactPrepago.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtFactPrepago, "Ingrese numero de documento");
                return;
            }
            if (txtFactPrepago.Text.Contains("'"))
            {
                errorProvider1.SetError(txtFactPrepago, "No se admite comilla simple");
                return;
            }

            /*
            String sqlMain = @"
                select	Compra.idCompra
                from	Compra 
		                left outer join Cliente on compra.idCliente=Cliente.idCliente
                where	Compra.idTipoFactura=4 and compra.Borrar=0 and compra.Fecha>='20150101' 
            ";
            */
            
            String sqlMain = @"
                select	Compra.idCompra, Compra.idCliente,  Compra.Numero, Compra.Fecha,Cliente.Nombre as Proveedor, Compra.Total, Compra.Notas
                from	Compra 
		                left outer join Cliente on compra.idCliente=Cliente.idCliente
                where	Compra.idTipoFactura=4 and compra.Borrar=0 and compra.Fecha>='20150101' 
            ";
            
            /*
            String sqlMain = @"
                select	Compra.idCompra, Compra.idCliente,  Compra.Numero, Compra.Fecha,Cliente.Nombre as Proveedor, Compra.Total, Compra.Notas
                from	Compra 
		                left outer join Cliente on compra.idCliente=Cliente.idCliente
                where	Compra.idTipoFactura=4 and compra.Borrar=0 and compra.Fecha>='20150101' 
						and compra.idCompra not in (
								select idCompraPrep from TiemposOCS where idCompraPrep is not null
							) 
            ";
            */

            if (txtFactPrepago.Text.Contains("%"))
            {
                sqlQuery =  sqlMain+ " and Compra.Numero like '" + txtFactPrepago.Text.Trim() + "' ";
            }
            else
            {
                sqlQuery = sqlMain + " and Compra.Numero='" + txtFactPrepago.Text.Trim() + "' ";
            }

            if (d.EjecutaEscalar("select count(*) from (" + sqlQuery + ") a") < 1)
            {
                errorProvider1.SetError(txtFactPrepago, "No se ha encontrado documento");
                return;
            }
            /*
            // Verificar si la factura de compra ya se encuentra ligada a otra OCS.
            sqlQuery="select count(*) from TiemposOCS where idCompraPrep="d.EjecutaEscalar(sqlQuery);
            if(d.EjecutaEscalar(sqlQuery)>0)
            {
                // La factura prepago se encuentra ligada a otra OCS.
                errorProvider1.SetError(txtFactPrepago, "Esta factura ya se encuentra ");
                return;
            }
            */

            dgvFactPrepago.Visible = true;
            d.LlenaGrid(dgvFactPrepago, "Compra", sqlQuery);
            estilo(dgvFactPrepago, 2);
        }

        private void dgvFactPrepago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) // ESC
            {
                dgvFactPrepago.Visible = false;
                btnBuscaFact.Visible = false;
                txtFactPrepago.Visible = false;
                btnFactPrepago.Visible = true;
            }
            if (e.KeyChar == (char)13) // Enter
            {
                //Lo mismo que cuando se da dobe clic:
                dgvFactPrepago_CellDoubleClick(sender, null);
            }
        }

        private void txtFactPrepago_Enter(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtFactPrepago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter
            {
                btnBuscaFact_Click(sender, e);
                dgvFactPrepago.Focus();
            }
        }

        private void dgvFactPrepago_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Realizar el ligue:
            if (MessageBox.Show("Desea confirmar esta dependencia?", "Confirmacion", MessageBoxButtons.YesNo).CompareTo(DialogResult.No) == 0)
                return;
            int idCompraPrep = Int32.Parse(dgvFactPrepago.SelectedRows[0].Cells[0].Value.ToString());

            // Verificar que la factura prepago no se encuentre ligada a otra OCS:
            sqlQuery= "select count(*) from TiemposOCS where idCompraPrep="+idCompraPrep;
            if (d.EjecutaEscalar(sqlQuery)>0)
            {
                sqlQuery = @"
                    select Numero from compra 
                    where idCompra in (
                        select idCompra from TiemposOCS where idCompraPrep=" + idCompraPrep + ")";
                MessageBox.Show("Factura ya se encuentra ligada a la OCS: " + d.EjecutaEscalarStr(sqlQuery),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                dgvFactPrepago.Visible = false;
                ci_ocs.fillPanel(panel2, 2, idOCS);
                return;
            }

            // Si no existe en TiemposOCS lo crea.
            sqlQuery = "select count(*) from tiemposOCS where idCompra=" + idOCS;
            if (d.EjecutaEscalar(sqlQuery) < 1)
                sqlQuery = "insert into tiemposOCS (idCompra, idCompraPrep) values (" + idOCS + "," + idCompraPrep + ")";
            else
                sqlQuery = "update tiemposOCS set idCompraPrep=" + idCompraPrep + " where idCompra=" + idOCS;
            d.EjecutaSql(sqlQuery, false);
            dgvFactPrepago.Visible = false;
            ci_ocs.fillPanel(panel2, 2, idOCS);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Eliminar el ligue entre OCS y fact. prepago:
            if (MessageBox.Show("Eliminar dependencia?", "Confirmacion", MessageBoxButtons.YesNo).CompareTo(DialogResult.No) == 0)
                return;
            sqlQuery = "update tiemposOCS set idCompraPrep=null where idCompra=" + idOCS;
            d.EjecutaSql(sqlQuery, false);
            dgvFactPrepago.Visible = false;
            ci_ocs.fillPanel(panel2, 2, idOCS);
        }

        private void dgvOCS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) // ESC
            {
                btnUndo_Click(sender, null);
            }
        }
    }
}
