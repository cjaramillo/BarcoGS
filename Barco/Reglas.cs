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
    public partial class Reglas : Form
    {
       
        public Reglas()
        {
            InitializeComponent();
        }

        Datos miClase = new Datos();

        private void Reglas_Load(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT reglaAlerta.idRegla as 'Nro Regla', ArticuloMarca.Marca, isnull(ArticuloGrupo.Grupo,'TODOS') as 'Grupo Articulo', " +
            " isnull(ArticuloModelo.Modelo,'TODOS') as 'Modelo Articulo', reglaAlerta.nombre as 'Nombre Regla', reglaAlerta.diasProduccion as 'Dias Producción', " +
            " reglaAlerta.avisoProduccion as 'Aviso Producción', reglaAlerta.diasRecepcion as 'Dias Recepción', reglaAlerta.avisoRecepcion as 'Aviso Recepción', " +
            " case when reglaAlerta.borrar=0 then 'No' else 'Si' end as Anulado FROM reglaAlerta " +
            " LEFT OUTER JOIN ArticuloModelo ON reglaAlerta.idModelo = ArticuloModelo.idModelo LEFT OUTER JOIN " +
            " ArticuloGrupo ON reglaAlerta.idGrupoArticulo = ArticuloGrupo.idGrupoArticulo LEFT OUTER JOIN " +
            " ArticuloMarca ON reglaAlerta.idMarca = ArticuloMarca.idMarca";
            miClase.LlenaGrid(dgvReglas, "reglaAlerta", sqlQuery);
            formatearDGV();
        }

        private void formatearDGV() 
        {
            dgvReglas.Columns[0].Width = 50; // nro Regla
            dgvReglas.Columns[1].Width = 200; // marca
            dgvReglas.Columns[2].Width = 100; // grupo articulo
            dgvReglas.Columns[3].Width = 100; // modelo articulo
            dgvReglas.Columns[4].Width = 200; // nombre regla
            dgvReglas.Columns[5].Width = 80; // dias produccion
            dgvReglas.Columns[6].Width = 80; // Aviso Producción
            dgvReglas.Columns[7].Width = 80; // Dias Recepción
            dgvReglas.Columns[8].Width = 80; // Aviso Recepción
            dgvReglas.Columns[9].Width = 80; // Anulado/Estado.
        }

    }
}
