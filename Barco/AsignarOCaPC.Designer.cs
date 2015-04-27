namespace Barco
{
    partial class AsignarOCaPC
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
            this.dgvPlanesCompra = new System.Windows.Forms.DataGridView();
            this.dgvOrdenesCompra = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnLiberarPC = new System.Windows.Forms.Button();
            this.btnInformativo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMuestraPC = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMuestraOC = new System.Windows.Forms.ComboBox();
            this.btnControl = new System.Windows.Forms.Button();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnDeshacer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanesCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesCompra)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlanesCompra
            // 
            this.dgvPlanesCompra.AllowUserToAddRows = false;
            this.dgvPlanesCompra.AllowUserToDeleteRows = false;
            this.dgvPlanesCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanesCompra.Location = new System.Drawing.Point(11, 35);
            this.dgvPlanesCompra.MultiSelect = false;
            this.dgvPlanesCompra.Name = "dgvPlanesCompra";
            this.dgvPlanesCompra.ReadOnly = true;
            this.dgvPlanesCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanesCompra.Size = new System.Drawing.Size(1064, 240);
            this.dgvPlanesCompra.TabIndex = 0;
            // 
            // dgvOrdenesCompra
            // 
            this.dgvOrdenesCompra.AllowUserToAddRows = false;
            this.dgvOrdenesCompra.AllowUserToDeleteRows = false;
            this.dgvOrdenesCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesCompra.Location = new System.Drawing.Point(11, 307);
            this.dgvOrdenesCompra.MultiSelect = false;
            this.dgvOrdenesCompra.Name = "dgvOrdenesCompra";
            this.dgvOrdenesCompra.ReadOnly = true;
            this.dgvOrdenesCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenesCompra.Size = new System.Drawing.Size(1064, 240);
            this.dgvOrdenesCompra.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "PLANES DE COMPRA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ORDENES DE COMPRA";
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(15, 585);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(129, 23);
            this.btnAsignar.TabIndex = 4;
            this.btnAsignar.Text = "&Asignar PC a OC";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnLiberarPC
            // 
            this.btnLiberarPC.Location = new System.Drawing.Point(796, 558);
            this.btnLiberarPC.Name = "btnLiberarPC";
            this.btnLiberarPC.Size = new System.Drawing.Size(129, 23);
            this.btnLiberarPC.TabIndex = 5;
            this.btnLiberarPC.Text = "&Liberar PC";
            this.btnLiberarPC.UseVisualStyleBackColor = true;
            this.btnLiberarPC.Click += new System.EventHandler(this.btnLiberarPC_Click);
            // 
            // btnInformativo
            // 
            this.btnInformativo.Location = new System.Drawing.Point(954, 558);
            this.btnInformativo.Name = "btnInformativo";
            this.btnInformativo.Size = new System.Drawing.Size(121, 23);
            this.btnInformativo.TabIndex = 6;
            this.btnInformativo.Text = "PC Informativo";
            this.btnInformativo.UseVisualStyleBackColor = true;
            this.btnInformativo.Click += new System.EventHandler(this.btnInformativo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(743, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Planes de Compra - MOSTRAR";
            // 
            // cmbMuestraPC
            // 
            this.cmbMuestraPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMuestraPC.FormattingEnabled = true;
            this.cmbMuestraPC.Items.AddRange(new object[] {
            "Todos",
            "Solo Asignados",
            "Solo Libres",
            "Solo Informativos"});
            this.cmbMuestraPC.Location = new System.Drawing.Point(947, 11);
            this.cmbMuestraPC.Name = "cmbMuestraPC";
            this.cmbMuestraPC.Size = new System.Drawing.Size(128, 21);
            this.cmbMuestraPC.TabIndex = 8;
            this.cmbMuestraPC.SelectedIndexChanged += new System.EventHandler(this.cmbMuestraPC_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 558);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fecha Revisión Dr.";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(170, 552);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(93, 20);
            this.dtpFecha.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(743, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ordenes de Compra - MOSTRAR";
            // 
            // cmbMuestraOC
            // 
            this.cmbMuestraOC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMuestraOC.Enabled = false;
            this.cmbMuestraOC.FormattingEnabled = true;
            this.cmbMuestraOC.Items.AddRange(new object[] {
            "Solo Libres",
            "Asignadas",
            "Todos"});
            this.cmbMuestraOC.Location = new System.Drawing.Point(954, 283);
            this.cmbMuestraOC.Name = "cmbMuestraOC";
            this.cmbMuestraOC.Size = new System.Drawing.Size(121, 21);
            this.cmbMuestraOC.TabIndex = 12;
            this.cmbMuestraOC.SelectedIndexChanged += new System.EventHandler(this.cmbMuestraOC_SelectedIndexChanged);
            // 
            // btnControl
            // 
            this.btnControl.Location = new System.Drawing.Point(954, 588);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(121, 23);
            this.btnControl.TabIndex = 13;
            this.btnControl.Text = "PC Aplica Control";
            this.btnControl.UseVisualStyleBackColor = true;
            this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // txtObservacion
            // 
            this.txtObservacion.Enabled = false;
            this.txtObservacion.Location = new System.Drawing.Point(301, 585);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(377, 43);
            this.txtObservacion.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(298, 568);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Observación:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(482, 558);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(61, 23);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(549, 558);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(54, 23);
            this.btnEditar.TabIndex = 17;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnDeshacer
            // 
            this.btnDeshacer.Location = new System.Drawing.Point(609, 558);
            this.btnDeshacer.Name = "btnDeshacer";
            this.btnDeshacer.Size = new System.Drawing.Size(69, 23);
            this.btnDeshacer.TabIndex = 18;
            this.btnDeshacer.Text = "Deshacer";
            this.btnDeshacer.UseVisualStyleBackColor = true;
            this.btnDeshacer.Click += new System.EventHandler(this.btnDeshacer_Click);
            // 
            // AsignarOCaPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 640);
            this.Controls.Add(this.btnDeshacer);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtObservacion);
            this.Controls.Add(this.btnControl);
            this.Controls.Add(this.cmbMuestraOC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbMuestraPC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInformativo);
            this.Controls.Add(this.btnLiberarPC);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOrdenesCompra);
            this.Controls.Add(this.dgvPlanesCompra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AsignarOCaPC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Orden de Compra a Plan de Compra";
            this.Load += new System.EventHandler(this.AsignarOCaPP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanesCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesCompra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlanesCompra;
        private System.Windows.Forms.DataGridView dgvOrdenesCompra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnLiberarPC;
        private System.Windows.Forms.Button btnInformativo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMuestraPC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMuestraOC;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnDeshacer;
    }
}