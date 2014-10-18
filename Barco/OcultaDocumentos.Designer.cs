namespace Barco
{
    partial class OcultaDocumentos
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
            this.dgvDocumentos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRestaura = new System.Windows.Forms.Button();
            this.btnActualiza = new System.Windows.Forms.Button();
            this.btnOcultaTodas = new System.Windows.Forms.Button();
            this.btnOcultaEspecifica = new System.Windows.Forms.Button();
            this.cmbTipoFactura = new System.Windows.Forms.ComboBox();
            this.lblTipoDoc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOC = new System.Windows.Forms.TextBox();
            this.pbTutorial = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTutorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDocumentos
            // 
            this.dgvDocumentos.AllowUserToAddRows = false;
            this.dgvDocumentos.AllowUserToDeleteRows = false;
            this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentos.Location = new System.Drawing.Point(12, 26);
            this.dgvDocumentos.MultiSelect = false;
            this.dgvDocumentos.Name = "dgvDocumentos";
            this.dgvDocumentos.ReadOnly = true;
            this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocumentos.Size = new System.Drawing.Size(1127, 365);
            this.dgvDocumentos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lista de Documentos";
            // 
            // btnRestaura
            // 
            this.btnRestaura.Enabled = false;
            this.btnRestaura.Location = new System.Drawing.Point(18, 449);
            this.btnRestaura.Name = "btnRestaura";
            this.btnRestaura.Size = new System.Drawing.Size(75, 23);
            this.btnRestaura.TabIndex = 2;
            this.btnRestaura.Text = "Restaurar";
            this.btnRestaura.UseVisualStyleBackColor = true;
            this.btnRestaura.Click += new System.EventHandler(this.btnRestaura_Click);
            // 
            // btnActualiza
            // 
            this.btnActualiza.Enabled = false;
            this.btnActualiza.Location = new System.Drawing.Point(18, 485);
            this.btnActualiza.Name = "btnActualiza";
            this.btnActualiza.Size = new System.Drawing.Size(75, 23);
            this.btnActualiza.TabIndex = 3;
            this.btnActualiza.Text = "Actualizar";
            this.btnActualiza.UseVisualStyleBackColor = true;
            this.btnActualiza.Click += new System.EventHandler(this.btnActualiza_Click);
            // 
            // btnOcultaTodas
            // 
            this.btnOcultaTodas.Enabled = false;
            this.btnOcultaTodas.Location = new System.Drawing.Point(122, 449);
            this.btnOcultaTodas.Name = "btnOcultaTodas";
            this.btnOcultaTodas.Size = new System.Drawing.Size(114, 23);
            this.btnOcultaTodas.TabIndex = 4;
            this.btnOcultaTodas.Text = "Ocultar Todas";
            this.btnOcultaTodas.UseVisualStyleBackColor = true;
            this.btnOcultaTodas.Click += new System.EventHandler(this.btnOcultaTodas_Click);
            // 
            // btnOcultaEspecifica
            // 
            this.btnOcultaEspecifica.Enabled = false;
            this.btnOcultaEspecifica.Location = new System.Drawing.Point(122, 485);
            this.btnOcultaEspecifica.Name = "btnOcultaEspecifica";
            this.btnOcultaEspecifica.Size = new System.Drawing.Size(114, 23);
            this.btnOcultaEspecifica.TabIndex = 5;
            this.btnOcultaEspecifica.Text = "Ocultar Específica";
            this.btnOcultaEspecifica.UseVisualStyleBackColor = true;
            this.btnOcultaEspecifica.Click += new System.EventHandler(this.btnOcultaEspecifica_Click);
            // 
            // cmbTipoFactura
            // 
            this.cmbTipoFactura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoFactura.FormattingEnabled = true;
            this.cmbTipoFactura.Items.AddRange(new object[] {
            "Ordenes de Compra",
            "Pedidos Proveedor"});
            this.cmbTipoFactura.Location = new System.Drawing.Point(152, 410);
            this.cmbTipoFactura.Name = "cmbTipoFactura";
            this.cmbTipoFactura.Size = new System.Drawing.Size(220, 21);
            this.cmbTipoFactura.TabIndex = 6;
            this.cmbTipoFactura.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFactura_SelectedIndexChanged);
            // 
            // lblTipoDoc
            // 
            this.lblTipoDoc.AutoSize = true;
            this.lblTipoDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDoc.ForeColor = System.Drawing.Color.Red;
            this.lblTipoDoc.Location = new System.Drawing.Point(156, 9);
            this.lblTipoDoc.Name = "lblTipoDoc";
            this.lblTipoDoc.Size = new System.Drawing.Size(200, 13);
            this.lblTipoDoc.TabIndex = 7;
            this.lblTipoDoc.Text = "SELECCIONE TIPO DOCUMENTO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tipo de Documento";
            // 
            // txtOC
            // 
            this.txtOC.Enabled = false;
            this.txtOC.Location = new System.Drawing.Point(254, 487);
            this.txtOC.Name = "txtOC";
            this.txtOC.Size = new System.Drawing.Size(167, 20);
            this.txtOC.TabIndex = 9;
            // 
            // pbTutorial
            // 
            this.pbTutorial.Image = global::Barco.Properties.Resources.IMG_ocultaOC;
            this.pbTutorial.Location = new System.Drawing.Point(626, 426);
            this.pbTutorial.Name = "pbTutorial";
            this.pbTutorial.Size = new System.Drawing.Size(372, 107);
            this.pbTutorial.TabIndex = 10;
            this.pbTutorial.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(675, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "CONDICIONES PARA OCULTAR DOCUMENTOS";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // OcultaDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 554);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbTutorial);
            this.Controls.Add(this.txtOC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTipoDoc);
            this.Controls.Add(this.cmbTipoFactura);
            this.Controls.Add(this.btnOcultaEspecifica);
            this.Controls.Add(this.btnOcultaTodas);
            this.Controls.Add(this.btnActualiza);
            this.Controls.Add(this.btnRestaura);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDocumentos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OcultaDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ocultar Documentos";
            this.Load += new System.EventHandler(this.OcultaDocumentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTutorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDocumentos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRestaura;
        private System.Windows.Forms.Button btnActualiza;
        private System.Windows.Forms.Button btnOcultaTodas;
        private System.Windows.Forms.Button btnOcultaEspecifica;
        private System.Windows.Forms.ComboBox cmbTipoFactura;
        private System.Windows.Forms.Label lblTipoDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOC;
        private System.Windows.Forms.PictureBox pbTutorial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}