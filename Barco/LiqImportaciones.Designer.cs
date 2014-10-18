namespace Barco
{
    partial class LiqImportaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiqImportaciones));
            this.lblArticulo = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.btnGenArticulo = new System.Windows.Forms.Button();
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.bnControl = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ds1 = new System.Data.DataSet();
            this.lblNro = new System.Windows.Forms.Label();
            this.dgvImportaciones = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnFactura = new System.Windows.Forms.Button();
            this.btnAsiento = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGeneraAsiento = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnDeshacer = new System.Windows.Forms.Button();
            this.bs1 = new System.Windows.Forms.BindingSource(this.components);
            this.bs2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bnControl)).BeginInit();
            this.bnControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImportaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblArticulo
            // 
            this.lblArticulo.AutoSize = true;
            this.lblArticulo.Location = new System.Drawing.Point(9, 19);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(44, 13);
            this.lblArticulo.TabIndex = 0;
            this.lblArticulo.Text = "Artículo";
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(805, 12);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(121, 20);
            this.txtNumero.TabIndex = 1;
            // 
            // btnGenArticulo
            // 
            this.btnGenArticulo.Enabled = false;
            this.btnGenArticulo.Location = new System.Drawing.Point(206, 9);
            this.btnGenArticulo.Name = "btnGenArticulo";
            this.btnGenArticulo.Size = new System.Drawing.Size(121, 23);
            this.btnGenArticulo.TabIndex = 2;
            this.btnGenArticulo.Text = "Generar Artículo";
            this.btnGenArticulo.UseVisualStyleBackColor = true;
            this.btnGenArticulo.Click += new System.EventHandler(this.btnGenArticulo_Click);
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.Enabled = false;
            this.cmbArticulo.FormattingEnabled = true;
            this.cmbArticulo.Location = new System.Drawing.Point(69, 11);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(121, 21);
            this.cmbArticulo.TabIndex = 3;
            // 
            // bnControl
            // 
            this.bnControl.AddNewItem = null;
            this.bnControl.CountItem = this.bindingNavigatorCountItem;
            this.bnControl.DeleteItem = null;
            this.bnControl.Dock = System.Windows.Forms.DockStyle.None;
            this.bnControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bnControl.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.bnControl.Location = new System.Drawing.Point(12, 474);
            this.bnControl.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnControl.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnControl.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnControl.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnControl.Name = "bnControl";
            this.bnControl.PositionItem = this.bindingNavigatorPositionItem;
            this.bnControl.Size = new System.Drawing.Size(211, 25);
            this.bnControl.TabIndex = 8;
            this.bnControl.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(37, 22);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Número total de elementos";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Posición";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Posición actual";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ds1
            // 
            this.ds1.DataSetName = "NewDataSet";
            // 
            // lblNro
            // 
            this.lblNro.AutoSize = true;
            this.lblNro.Location = new System.Drawing.Point(755, 15);
            this.lblNro.Name = "lblNro";
            this.lblNro.Size = new System.Drawing.Size(44, 13);
            this.lblNro.TabIndex = 9;
            this.lblNro.Text = "Numero";
            // 
            // dgvImportaciones
            // 
            this.dgvImportaciones.AllowUserToAddRows = false;
            this.dgvImportaciones.AllowUserToDeleteRows = false;
            this.dgvImportaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImportaciones.Location = new System.Drawing.Point(12, 71);
            this.dgvImportaciones.MultiSelect = false;
            this.dgvImportaciones.Name = "dgvImportaciones";
            this.dgvImportaciones.ReadOnly = true;
            this.dgvImportaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImportaciones.Size = new System.Drawing.Size(914, 400);
            this.dgvImportaciones.TabIndex = 4;
            this.dgvImportaciones.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvImportaciones_RowsAdded);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnFactura
            // 
            this.btnFactura.Location = new System.Drawing.Point(332, 477);
            this.btnFactura.Name = "btnFactura";
            this.btnFactura.Size = new System.Drawing.Size(23, 22);
            this.btnFactura.TabIndex = 13;
            this.btnFactura.Text = "F";
            this.btnFactura.UseVisualStyleBackColor = true;
            // 
            // btnAsiento
            // 
            this.btnAsiento.Location = new System.Drawing.Point(357, 477);
            this.btnAsiento.Name = "btnAsiento";
            this.btnAsiento.Size = new System.Drawing.Size(23, 22);
            this.btnAsiento.TabIndex = 14;
            this.btnAsiento.Text = "A";
            this.btnAsiento.UseVisualStyleBackColor = true;
            // 
            // btnGeneraAsiento
            // 
            this.btnGeneraAsiento.Enabled = false;
            this.btnGeneraAsiento.Location = new System.Drawing.Point(334, 9);
            this.btnGeneraAsiento.Name = "btnGeneraAsiento";
            this.btnGeneraAsiento.Size = new System.Drawing.Size(121, 23);
            this.btnGeneraAsiento.TabIndex = 19;
            this.btnGeneraAsiento.Text = "Generar Asiento";
            this.btnGeneraAsiento.UseVisualStyleBackColor = true;
            // 
            // btnExcel
            // 
            this.btnExcel.Enabled = false;
            this.btnExcel.Location = new System.Drawing.Point(470, 9);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(68, 23);
            this.btnExcel.TabIndex = 20;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Image = global::Barco.Properties.Resources.borrar_ico;
            this.btnBorrar.Location = new System.Drawing.Point(282, 477);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(23, 22);
            this.btnBorrar.TabIndex = 21;
            this.btnBorrar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::Barco.Properties.Resources.editar_ico;
            this.btnEditar.Location = new System.Drawing.Point(254, 477);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(23, 22);
            this.btnEditar.TabIndex = 18;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Barco.Properties.Resources.imprimir_ico;
            this.btnImprimir.Location = new System.Drawing.Point(382, 477);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.TabIndex = 16;
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Barco.Properties.Resources.buscar_ico;
            this.btnBuscar.Location = new System.Drawing.Point(307, 477);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(23, 22);
            this.btnBuscar.TabIndex = 15;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::Barco.Properties.Resources.nuevo_ico;
            this.btnNuevo.Location = new System.Drawing.Point(229, 477);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.TabIndex = 11;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNuevo_MouseUp);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Mover primero";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Mover anterior";
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Mover siguiente";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Mover último";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::Barco.Properties.Resources.guardar_ico;
            this.btnGuardar.Location = new System.Drawing.Point(229, 477);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnDeshacer
            // 
            this.btnDeshacer.Image = global::Barco.Properties.Resources.deshacer_ico;
            this.btnDeshacer.Location = new System.Drawing.Point(253, 477);
            this.btnDeshacer.Name = "btnDeshacer";
            this.btnDeshacer.Size = new System.Drawing.Size(23, 22);
            this.btnDeshacer.TabIndex = 17;
            this.btnDeshacer.UseVisualStyleBackColor = true;
            this.btnDeshacer.Visible = false;
            this.btnDeshacer.Click += new System.EventHandler(this.btnDeshacer_Click);
            // 
            // LiqImportaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 529);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnGeneraAsiento);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnAsiento);
            this.Controls.Add(this.btnFactura);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.lblNro);
            this.Controls.Add(this.bnControl);
            this.Controls.Add(this.dgvImportaciones);
            this.Controls.Add(this.cmbArticulo);
            this.Controls.Add(this.btnGenArticulo);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblArticulo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnDeshacer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LiqImportaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liquidar Importaciones";
            this.Load += new System.EventHandler(this.LiqImportaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bnControl)).EndInit();
            this.bnControl.ResumeLayout(false);
            this.bnControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImportaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArticulo;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Button btnGenArticulo;
        private System.Windows.Forms.ComboBox cmbArticulo;
        private System.Windows.Forms.BindingNavigator bnControl;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bs1;
        private System.Data.DataSet ds1;
        private System.Windows.Forms.Label lblNro;
        private System.Windows.Forms.BindingSource bs2;
        private System.Windows.Forms.DataGridView dgvImportaciones;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnAsiento;
        private System.Windows.Forms.Button btnFactura;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnDeshacer;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnGeneraAsiento;
        private System.Windows.Forms.Button btnBorrar;
    }
}