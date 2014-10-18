using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barco
{
    public partial class RestauraIBG : Form
    {
        private string sqlQuery;
        Datos miClase = new Datos();

        public RestauraIBG()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtIBG.Text.Length<1)
            {
                dgvRespaldo.Columns.Clear();
                errorProvider1.SetError(txtIBG,"Debe ingresar IBG");
                txtEstacion.Text = txtFechaRespaldo.Text = txtCosto.Text = "";
            }
            else
            {
                sqlQuery = @"select count(*) from Compra where idTipoFactura=9 and idSubProyecto=1 and Numero='" + txtIBG.Text.ToString() + "'";
                if (miClase.EjecutaEscalar(sqlQuery) < 1)
                {
                    dgvRespaldo.Columns.Clear();
                    errorProvider1.SetError(txtIBG, "El número de IBG no existe como ingreso de bodega");
                    txtEstacion.Text = txtFechaRespaldo.Text = txtCosto.Text = "";
                }
                else
                {
                    sqlQuery = @"select count(*) from bk_IBG where Numero='" + txtIBG.Text.ToString() + "'";
                    if (miClase.EjecutaEscalar(sqlQuery) < 1)
                    {
                        dgvRespaldo.Columns.Clear();
                        errorProvider1.SetError(txtIBG, "No existe respaldo de ese IBG");
                        txtEstacion.Text = txtFechaRespaldo.Text = txtCosto.Text = "";
                    }
                    else 
                    {
                        sqlQuery= @"
                            SELECT   bk_IBG.Cantidad, Articulo.Articulo, Articulo.Codigo, bk_IBG.Precio, bk_IBG.Vencimiento,bk_IBG.Flete,isnull(bk_IBG.Notas,'') as Notas
                            FROM     bk_IBG LEFT OUTER JOIN
                                         Articulo ON bk_IBG.idArticulo = Articulo.idArticulo
                            WHERE    bk_IBG.Numero='" + txtIBG.Text.ToString() + "'";
                        miClase.LlenaGrid(dgvRespaldo,"bk_IBG",sqlQuery);
                        estilizar(dgvRespaldo);
                        sqlQuery = @"
                            SELECT   top(1)bk_IBG.EstacionRespaldo
                            FROM     bk_IBG 
                            WHERE    Numero='" + txtIBG.Text.ToString() + "'";
                        txtEstacion.Text=miClase.EjecutaEscalarStr(sqlQuery);
                        
                        sqlQuery = @"
                            SELECT   top(1) cast(bk_IBG.fechaRespaldo as varchar(50))
                            FROM     bk_IBG 
                            WHERE    Numero='" + txtIBG.Text.ToString() + "'";
                        txtFechaRespaldo.Text = miClase.EjecutaEscalarStr(sqlQuery);


                        sqlQuery = @"
                            select round(SUM(Cantidad*precio+flete),2)
                            from bk_ibg
                            where numero='" + txtIBG.Text.ToString() + "'";
                        txtCosto.Text = miClase.EjecutaEscalarF(sqlQuery).ToString();
                    }
                }
            }
        }

        private void estilizar(System.Windows.Forms.DataGridView dgv)
        {
            // bk_IBG
            dgv.Columns[0].Width = 50; // Cantidad
            dgv.Columns[1].Width = 280; // Articulo
            dgv.Columns[2].Width = 120; // Codigo
            dgv.Columns[3].Width = 100; // Precio
            dgv.Columns[3].DefaultCellStyle.Format = "c";
            dgv.Columns[4].Width = 80; // Vencimiento
            dgv.Columns[4].DefaultCellStyle.Format = "d";
            dgv.Columns[5].Width = 80; // Flete
            dgv.Columns[5].DefaultCellStyle.Format = "c";
            dgv.Columns[6].Width = 300; // Notas
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtIBG.Text.Length < 1)
            {
                dgvRespaldo.Columns.Clear();
                errorProvider1.SetError(txtIBG, "Debe ingresar IBG");
                txtEstacion.Text = txtFechaRespaldo.Text = txtCosto.Text = "";
            }
            else
            {
                if (MessageBox.Show("Guardar datos de IBG en respaldo?", "Respaldar IBG", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    string[,] paramIN = 
                    {
                        {"vNumero",txtIBG.Text.ToString()},
                        {"vModo","0"}
                    };
                    string retorno = miClase.EjecutaSP("sp_RespaldosIBG", paramIN, "@retorno", SqlDbType.Int);
                    handlerMensajes(Int32.Parse(retorno));
                }
            }
        }



        private void handlerMensajes(int opcion)
        {
            string mensaje;
            switch (opcion)
            {
                case -1: mensaje = "El IBG ingresado no tiene el formato correcto: IBG-XXXXX"; break;
                case -2: mensaje = "El IBG no existe en el sistema como ingreso de bodega"; break;
                case -3: mensaje = "El IBG ingresado no posee ninguna línea en el detalle para respaldar"; break;
                case -4: mensaje = "Modo inválido"; break;
                case -5: mensaje = "No hay datos para restaurar"; break;
                case -6: mensaje = "Nro de registros en respaldo es diferente al nro de registros en servidor"; break;
                case -7: mensaje = "Se ha detectado incoherencias entre respaldo y origen. Intente restaurar con clic derecho"; break;
                case 1: mensaje = "IBG Almacenado"; break;
                case 2: mensaje = "IBG Restaurado"; break;
                default: mensaje = "Error no documentado"; break;
            }
            MessageBox.Show(mensaje,"Mensaje", MessageBoxButtons.OK);
        }

        
        private void btnRestaurar_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnRestaurar_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Restaurar ", btnRestaurar,5000);
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtIBG.Text.Length < 1)
            {
                dgvRespaldo.Columns.Clear();
                errorProvider1.SetError(txtIBG, "Debe ingresar IBG");
                txtEstacion.Text = txtFechaRespaldo.Text = txtCosto.Text = "";
            }
            else
            {
                if (MessageBox.Show("Restaurar IBG?", "Restaura IBG", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string[,] paramIN = 
                        {
                            {"vNumero",txtIBG.Text.ToString()},
                            {"vModo","1"}
                        };
                    string retorno = miClase.EjecutaSP("sp_RespaldosIBG", paramIN, "@retorno", SqlDbType.Int);
                    if (Int32.Parse(retorno) == -7 || Int32.Parse(retorno) == -6)
                    {
                        if (MessageBox.Show("Se ha detectado incoherencias entre respaldo y origen, desea restaurar sin verificar integridad?", "Restaura IBG", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            paramIN[1, 1] = "2";
                            retorno = miClase.EjecutaSP("sp_RespaldosIBG", paramIN, "@retorno", SqlDbType.Int);
                            handlerMensajes(Int32.Parse(retorno));
                        }
                    }
                    else
                    {
                        handlerMensajes(Int32.Parse(retorno));
                    }
                }
            }
        }
    }
}
