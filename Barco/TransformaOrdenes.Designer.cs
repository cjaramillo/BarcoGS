namespace Barco
{
    partial class TransformaOrdenes
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
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.cmbProv = new System.Windows.Forms.ComboBox();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnTranformacion = new System.Windows.Forms.Button();
            this.btnLimpia = new System.Windows.Forms.Button();
            this.lblNota = new System.Windows.Forms.Label();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.lblSol = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.chkFechaIngreso = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPago = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFechas
            // 
            this.chkFechas.AutoSize = true;
            this.chkFechas.Checked = true;
            this.chkFechas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFechas.Location = new System.Drawing.Point(22, 40);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(57, 17);
            this.chkFechas.TabIndex = 3;
            this.chkFechas.Text = "Desde";
            this.chkFechas.UseVisualStyleBackColor = true;
            this.chkFechas.CheckedChanged += new System.EventHandler(this.chkFechas_CheckedChanged);
            // 
            // cmbProv
            // 
            this.cmbProv.FormattingEnabled = true;
            this.cmbProv.Location = new System.Drawing.Point(22, 9);
            this.cmbProv.Name = "cmbProv";
            this.cmbProv.Size = new System.Drawing.Size(504, 21);
            this.cmbProv.TabIndex = 1;
            this.cmbProv.Text = "cmbProv";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Items.AddRange(new object[] {
            "Este Año",
            "Este Mes",
            "El Mes Pasado"});
            this.cmbPeriodo.Location = new System.Drawing.Point(382, 36);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(144, 21);
            this.cmbPeriodo.TabIndex = 8;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Enabled = false;
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(134, 35);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(98, 20);
            this.dtpDesde.TabIndex = 4;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Enabled = false;
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(134, 61);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(98, 20);
            this.dtpHasta.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Periodo";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(562, 3);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(113, 23);
            this.btnFiltrar.TabIndex = 2;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnTranformacion
            // 
            this.btnTranformacion.Location = new System.Drawing.Point(704, 3);
            this.btnTranformacion.Name = "btnTranformacion";
            this.btnTranformacion.Size = new System.Drawing.Size(83, 23);
            this.btnTranformacion.TabIndex = 9;
            this.btnTranformacion.Text = "Transformar";
            this.btnTranformacion.UseVisualStyleBackColor = true;
            this.btnTranformacion.Click += new System.EventHandler(this.btnTranformacion_Click);
            // 
            // btnLimpia
            // 
            this.btnLimpia.Location = new System.Drawing.Point(562, 36);
            this.btnLimpia.Name = "btnLimpia";
            this.btnLimpia.Size = new System.Drawing.Size(113, 23);
            this.btnLimpia.TabIndex = 10;
            this.btnLimpia.Text = "Limpiar Contenido";
            this.btnLimpia.UseVisualStyleBackColor = true;
            this.btnLimpia.Click += new System.EventHandler(this.btnLimpia_Click);
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.Location = new System.Drawing.Point(705, 41);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(111, 13);
            this.lblNota.TabIndex = 11;
            this.lblNota.Text = "Notas para la solicitud";
            // 
            // txtNota
            // 
            this.txtNota.Location = new System.Drawing.Point(822, 38);
            this.txtNota.Multiline = true;
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(405, 55);
            this.txtNota.TabIndex = 12;
            // 
            // lblSol
            // 
            this.lblSol.AutoSize = true;
            this.lblSol.Location = new System.Drawing.Point(819, 8);
            this.lblSol.Name = "lblSol";
            this.lblSol.Size = new System.Drawing.Size(47, 13);
            this.lblSol.TabIndex = 13;
            this.lblSol.Text = "Solicitud";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(887, 6);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(154, 20);
            this.txtNumero.TabIndex = 14;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Location = new System.Drawing.Point(22, 125);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(653, 463);
            this.dgvOrdenes.TabIndex = 15;
            this.dgvOrdenes.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrdenes_CellMouseClick);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(704, 125);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(728, 570);
            this.dgvDatos.TabIndex = 16;
            // 
            // chkFechaIngreso
            // 
            this.chkFechaIngreso.AutoSize = true;
            this.chkFechaIngreso.Location = new System.Drawing.Point(315, 66);
            this.chkFechaIngreso.Name = "chkFechaIngreso";
            this.chkFechaIngreso.Size = new System.Drawing.Size(192, 17);
            this.chkFechaIngreso.TabIndex = 17;
            this.chkFechaIngreso.Text = "Filtrar por Fecha Ingreso y Estación";
            this.chkFechaIngreso.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ordenes de Compra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(701, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Artículos";
            // 
            // cmbPago
            // 
            this.cmbPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPago.FormattingEnabled = true;
            this.cmbPago.Items.AddRange(new object[] {
            "Cheque",
            "Cruce",
            "Transferencia",
            "Tarjeta Crédito",
            "Retención Fuente"});
            this.cmbPago.Location = new System.Drawing.Point(1078, 3);
            this.cmbPago.Name = "cmbPago";
            this.cmbPago.Size = new System.Drawing.Size(149, 21);
            this.cmbPago.TabIndex = 20;
            this.cmbPago.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOrange;
            this.button1.Location = new System.Drawing.Point(36, 647);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 24);
            this.button1.TabIndex = 21;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(312, 591);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "LEYENDA";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Yellow;
            this.button2.Location = new System.Drawing.Point(36, 620);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 24);
            this.button2.TabIndex = 23;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(84, 649);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(532, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "Orden de compra sin fecha de revisión (aprobar cotización -> inicia producción)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(84, 623);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(449, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "Orden de compra no se encuentra ligada a ningún plan de compras";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Cyan;
            this.button3.Location = new System.Drawing.Point(36, 675);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 24);
            this.button3.TabIndex = 26;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(84, 681);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "Orden de compra consolidada";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 712);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(651, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "No se muestran órdenes de compra procesadas o transformadas a pedido proveedor";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(797, 712);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(405, 23);
            this.pBar.TabIndex = 29;
            this.pBar.Visible = false;
            // 
            // TransformaOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 865);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbPago);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkFechaIngreso);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.dgvOrdenes);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblSol);
            this.Controls.Add(this.txtNota);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.btnLimpia);
            this.Controls.Add(this.btnTranformacion);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.cmbPeriodo);
            this.Controls.Add(this.cmbProv);
            this.Controls.Add(this.chkFechas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TransformaOrdenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transforma Ordenes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransformaOrdenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.ComboBox cmbProv;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnTranformacion;
        private System.Windows.Forms.Button btnLimpia;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.Label lblSol;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.CheckBox chkFechaIngreso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPago;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar pBar;
    }
}