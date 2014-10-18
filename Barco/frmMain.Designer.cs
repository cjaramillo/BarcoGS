namespace Barco
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mPrincipal = new System.Windows.Forms.MenuStrip();
            this.mMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mOrdenDeCompraLotes = new System.Windows.Forms.ToolStripMenuItem();
            this.mSolicitarAnticipos = new System.Windows.Forms.ToolStripMenuItem();
            this.mReglas = new System.Windows.Forms.ToolStripMenuItem();
            this.mOcultarMostrarOC = new System.Windows.Forms.ToolStripMenuItem();
            this.mAsignarOCAPlanCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.mAuditoríaImportaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respaldaYRestauraIBGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liquidarImportacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.mGenerarReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.mPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // mPrincipal
            // 
            this.mPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenu,
            this.mReportes});
            this.mPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mPrincipal.Name = "mPrincipal";
            this.mPrincipal.Size = new System.Drawing.Size(1214, 24);
            this.mPrincipal.TabIndex = 1;
            this.mPrincipal.Text = "menuStrip1";
            // 
            // mMenu
            // 
            this.mMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOrdenDeCompraLotes,
            this.mSolicitarAnticipos,
            this.mReglas,
            this.mOcultarMostrarOC,
            this.mAsignarOCAPlanCompras,
            this.mAuditoríaImportaciones,
            this.testToolStripMenuItem,
            this.respaldaYRestauraIBGToolStripMenuItem,
            this.liquidarImportacionesToolStripMenuItem});
            this.mMenu.Name = "mMenu";
            this.mMenu.Size = new System.Drawing.Size(50, 20);
            this.mMenu.Text = "Menu";
            // 
            // mOrdenDeCompraLotes
            // 
            this.mOrdenDeCompraLotes.Name = "mOrdenDeCompraLotes";
            this.mOrdenDeCompraLotes.Size = new System.Drawing.Size(239, 22);
            this.mOrdenDeCompraLotes.Text = "Orden de Compra Lotes";
            this.mOrdenDeCompraLotes.Click += new System.EventHandler(this.mOrdenDeCompraLotes_Click);
            // 
            // mSolicitarAnticipos
            // 
            this.mSolicitarAnticipos.Name = "mSolicitarAnticipos";
            this.mSolicitarAnticipos.Size = new System.Drawing.Size(239, 22);
            this.mSolicitarAnticipos.Text = "Solicitar Anticipos";
            this.mSolicitarAnticipos.Click += new System.EventHandler(this.mSolicitarAnticipos_Click);
            // 
            // mReglas
            // 
            this.mReglas.Name = "mReglas";
            this.mReglas.Size = new System.Drawing.Size(239, 22);
            this.mReglas.Text = "Reglas";
            this.mReglas.Click += new System.EventHandler(this.mReglas_Click);
            // 
            // mOcultarMostrarOC
            // 
            this.mOcultarMostrarOC.Name = "mOcultarMostrarOC";
            this.mOcultarMostrarOC.Size = new System.Drawing.Size(239, 22);
            this.mOcultarMostrarOC.Text = "Ocultar-Mostrar Documentos";
            this.mOcultarMostrarOC.Click += new System.EventHandler(this.mOcultarMostrarOC_Click);
            // 
            // mAsignarOCAPlanCompras
            // 
            this.mAsignarOCAPlanCompras.Name = "mAsignarOCAPlanCompras";
            this.mAsignarOCAPlanCompras.Size = new System.Drawing.Size(239, 22);
            this.mAsignarOCAPlanCompras.Text = "Asignar OC a Plan Compras";
            this.mAsignarOCAPlanCompras.Click += new System.EventHandler(this.mAsignarOCAPlanCompras_Click);
            // 
            // mAuditoríaImportaciones
            // 
            this.mAuditoríaImportaciones.Name = "mAuditoríaImportaciones";
            this.mAuditoríaImportaciones.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mAuditoríaImportaciones.Size = new System.Drawing.Size(239, 22);
            this.mAuditoríaImportaciones.Text = "Auditoría Importaciones";
            this.mAuditoríaImportaciones.Click += new System.EventHandler(this.mAuditoríaImportaciones_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.testToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // respaldaYRestauraIBGToolStripMenuItem
            // 
            this.respaldaYRestauraIBGToolStripMenuItem.Name = "respaldaYRestauraIBGToolStripMenuItem";
            this.respaldaYRestauraIBGToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.respaldaYRestauraIBGToolStripMenuItem.Text = "Respalda y Restaura IBG";
            this.respaldaYRestauraIBGToolStripMenuItem.Click += new System.EventHandler(this.respaldaYRestauraIBGToolStripMenuItem_Click);
            // 
            // liquidarImportacionesToolStripMenuItem
            // 
            this.liquidarImportacionesToolStripMenuItem.Name = "liquidarImportacionesToolStripMenuItem";
            this.liquidarImportacionesToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.liquidarImportacionesToolStripMenuItem.Text = "Liquidar Importaciones";
            this.liquidarImportacionesToolStripMenuItem.Click += new System.EventHandler(this.liquidarImportacionesToolStripMenuItem_Click);
            // 
            // mReportes
            // 
            this.mReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGenerarReportes});
            this.mReportes.Name = "mReportes";
            this.mReportes.Size = new System.Drawing.Size(65, 20);
            this.mReportes.Text = "Reportes";
            // 
            // mGenerarReportes
            // 
            this.mGenerarReportes.Name = "mGenerarReportes";
            this.mGenerarReportes.Size = new System.Drawing.Size(164, 22);
            this.mGenerarReportes.Text = "Generar Reportes";
            this.mGenerarReportes.Click += new System.EventHandler(this.mGenerarReportes_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Barco.Properties.Resources.barco;
            this.ClientSize = new System.Drawing.Size(1214, 632);
            this.Controls.Add(this.mPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mPrincipal;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Importaciones -";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mPrincipal.ResumeLayout(false);
            this.mPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mMenu;
        private System.Windows.Forms.ToolStripMenuItem mReportes;
        private System.Windows.Forms.ToolStripMenuItem mOrdenDeCompraLotes;
        private System.Windows.Forms.ToolStripMenuItem mSolicitarAnticipos;
        private System.Windows.Forms.ToolStripMenuItem mReglas;
        private System.Windows.Forms.ToolStripMenuItem mOcultarMostrarOC;
        private System.Windows.Forms.ToolStripMenuItem mAsignarOCAPlanCompras;
        private System.Windows.Forms.ToolStripMenuItem mGenerarReportes;
        private System.Windows.Forms.ToolStripMenuItem mAuditoríaImportaciones;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respaldaYRestauraIBGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liquidarImportacionesToolStripMenuItem;
    }
}

