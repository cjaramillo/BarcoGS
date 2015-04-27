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
    public partial class Transformar : Form
    {
        public Transformar()
        {
            InitializeComponent();
        }
        public int modo = 0;
        /*
         * 0= Transformar Req a orden de compra
         * 1= Desbloquear requisición
         * 2= OCS: Copiar cantidades a columna pedido.
         * */
        
        private String sqlQuery;
        private Datos miClase = new Datos();


        private void btnTransformar_Click(object sender, EventArgs e)
        {
            if (txtReq.Text.ToString().Length < 1) return;
            if (txtReq.Text.Contains("'"))
            {
                txtReq.Text = "";
                return;
            }
            txtReq.Text = txtReq.Text.Trim();
            switch (modo)
            {
                case 0:
                    {
                        string[,] paramIN = 
                        {
                           {"vNroReq",txtReq.Text.ToString()}
                        };
                        switch (Int32.Parse(miClase.EjecutaSP("sp_TransformaReq", paramIN, "@vRetorno", SqlDbType.Int)))
                        {
                            case -1:
                                {
                                    errorProvider1.SetError(txtReq, "Error al transformar (Requisicion marcada como procesada)");
                                } break;
                            case -2:
                                {
                                    errorProvider1.SetError(txtReq, "Al menos hay una línea de detalle sin revisar (columna vencimiento)");
                                } break;
                            case -3:
                                {
                                    errorProvider1.SetError(txtReq, "No hay ninguna línea de detalle pendiente para migrar.");
                                } break;
                            case -4:
                                {
                                    errorProvider1.SetError(txtReq, "No han especificado el tipo de importación en la requisición");
                                } break;
                            case -5:
                                {
                                    errorProvider1.SetError(txtReq, "No han especificado el destino de la importación en la requisición");
                                } break;
                            case 0:
                                {
                                    errorProvider1.SetError(txtReq, "No existe requisición");
                                } break;
                            case 10:
                                {
                                    errorProvider1.SetError(txtReq, "Existe más de una requisición con el número ingresado.");
                                } break;
                            case 1:
                                {
                                    MessageBox.Show("Transformación Procesada");
                                    txtReq.Text = "";
                                } break;
                            default:
                                {
                                    errorProvider1.SetError(txtReq, "Código de error no documentado. Favor comuniquese con sistemas.");
                                } break;
                        }
                    } break;
                case 1:
                    {
                        // Borrar la OC en caso de que exista
                        if (MessageBox.Show("Desea ejecutar la transacción?", "Confirmar Transaccion", MessageBoxButtons.YesNo) == DialogResult.No) return;
                        sqlQuery = @"delete from compra where idtipofactura=2 and numero='" + txtReq.Text + "'";
                        miClase.EjecutaSql(sqlQuery, false);

                        // En la req poner "no aplica"
                        sqlQuery = @"update Compra set idComprobante=25 where idtipofactura=36 and numero='" + txtReq.Text + "'";
                        miClase.EjecutaSql(sqlQuery, false);

                        // Detcompra.jm = null
                        sqlQuery = @"
                            update detcompra 
                            set detcompra.jm=null
                            from detcompra 
	                            left outer join compra on Detcompra.idCompra=Compra.idCompra
                            where compra.idTipofactura=36 and numero='" + txtReq.Text + "'";
                        miClase.EjecutaSql(sqlQuery, true);
                    } break;
                case 2:
                    {
                        // Verificar si la Orden de compra existe
                        sqlQuery = @"select count(*) from compra where borrar=0 and numero='" + txtReq.Text + "'";
                        short nroCasos=(short)miClase.EjecutaEscalar(sqlQuery);
                        if ( nroCasos < 1)
                        {
                            errorProvider1.SetError(txtReq, "OCS no encontrada");
                            return;
                        }
                        if (nroCasos > 1)
                        {
                            errorProvider1.SetError(txtReq, "Existe mas de una OCS con ese numero");
                            return;
                        }

                        // Verificar si es una OCS
                        sqlQuery = @"select count(*) from compra where borrar=0 and usuario='OrdenLotes' and numero='" + txtReq.Text + "'";
                        nroCasos = (short)miClase.EjecutaEscalar(sqlQuery);
                        if (nroCasos==1)
                        {
                            errorProvider1.SetError(txtReq, "Solo se aceptan OCS");
                            return;
                        }
                        // Procesar
                        if (MessageBox.Show("Desea ejecutar la transacción?", "Confirmar Transaccion", MessageBoxButtons.YesNo) == DialogResult.No) return;
                        sqlQuery = @"
                            update detcompra 
                            set detcompra.Pedido=detcompra.cantidad
                            from detcompra
                                left outer join compra on detcompra.idCompra=compra.idCompra
                            where compra.borrar=0 and compra.usuario<>'OrdenLotes' and compra.numero='" + txtReq.Text + "'";
                        miClase.EjecutaSql(sqlQuery, true);
                        this.Close();

                    } break;
            }
        }


        private void txtReq_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void Transformar_Load(object sender, EventArgs e)
        {
            // Cargar títulos
            switch (modo)
            {
                case 0:
                    {
                        lblDocumento.Text = "Requisición:";
                        this.Text = btnAccion.Text = "Transformar";
                    } break;
                case 1:
                    {
                        lblDocumento.Text = "Requisición:";
                        this.Text = btnAccion.Text = "Desbloquear";
                    } break;
                case 2:
                    {
                        lblDocumento.Text = "OCS:";
                        this.Text = btnAccion.Text = "Copiar";
                    } break;
            }
        }
    }
}
