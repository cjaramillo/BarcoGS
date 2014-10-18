using Barco.CAD;
using Barco.CAD.Clases;
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
    public partial class Test : Form
    {

        AccesoDatosOCS adocs = new AccesoDatosOCS();

        public Test()
        {
            InitializeComponent();
        }
        
        private void Test_Load(object sender, EventArgs e)
        {
            adocs.llenaGrid(dgvTest);
            adocs.estilo(dgvTest);
        }

        private void dgvTest_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adocs.pintarCeldas(dgvTest);
        }

        private void Test_Shown(object sender, EventArgs e)
        {
            adocs.pintarCeldas(dgvTest);
            txtServer.Text = Datos.strServidor;
            txtBDD.Text = Datos.strBase;
        }
    }
}
