namespace Barco
{
    partial class RestauraIBG
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
            this.lblIBG = new System.Windows.Forms.Label();
            this.txtIBG = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvRespaldo = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.lblRespaldo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblEstacion = new System.Windows.Forms.Label();
            this.txtFechaRespaldo = new System.Windows.Forms.TextBox();
            this.txtEstacion = new System.Windows.Forms.TextBox();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.lblCosto = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespaldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIBG
            // 
            this.lblIBG.AutoSize = true;
            this.lblIBG.Location = new System.Drawing.Point(12, 47);
            this.lblIBG.Name = "lblIBG";
            this.lblIBG.Size = new System.Drawing.Size(68, 13);
            this.lblIBG.TabIndex = 0;
            this.lblIBG.Text = "Número IBG:";
            // 
            // txtIBG
            // 
            this.txtIBG.Location = new System.Drawing.Point(97, 40);
            this.txtIBG.Name = "txtIBG";
            this.txtIBG.Size = new System.Drawing.Size(100, 20);
            this.txtIBG.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(488, 37);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvRespaldo
            // 
            this.dgvRespaldo.AllowUserToAddRows = false;
            this.dgvRespaldo.AllowUserToDeleteRows = false;
            this.dgvRespaldo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRespaldo.Location = new System.Drawing.Point(15, 105);
            this.dgvRespaldo.MultiSelect = false;
            this.dgvRespaldo.Name = "dgvRespaldo";
            this.dgvRespaldo.ReadOnly = true;
            this.dgvRespaldo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRespaldo.Size = new System.Drawing.Size(1020, 358);
            this.dgvRespaldo.TabIndex = 3;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(569, 37);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(75, 23);
            this.btnRestaurar.TabIndex = 4;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(223, 38);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(125, 23);
            this.btnCargar.TabIndex = 5;
            this.btnCargar.Text = "Mostrar Respaldo";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lblRespaldo
            // 
            this.lblRespaldo.AutoSize = true;
            this.lblRespaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRespaldo.Location = new System.Drawing.Point(12, 89);
            this.lblRespaldo.Name = "lblRespaldo";
            this.lblRespaldo.Size = new System.Drawing.Size(118, 13);
            this.lblRespaldo.TabIndex = 6;
            this.lblRespaldo.Text = "Datos del Respaldo";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(18, 499);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(88, 13);
            this.lblFecha.TabIndex = 7;
            this.lblFecha.Text = "Fecha Respaldo:";
            // 
            // lblEstacion
            // 
            this.lblEstacion.AutoSize = true;
            this.lblEstacion.Location = new System.Drawing.Point(774, 499);
            this.lblEstacion.Name = "lblEstacion";
            this.lblEstacion.Size = new System.Drawing.Size(99, 13);
            this.lblEstacion.TabIndex = 8;
            this.lblEstacion.Text = "Estación Respaldo:";
            // 
            // txtFechaRespaldo
            // 
            this.txtFechaRespaldo.Location = new System.Drawing.Point(114, 492);
            this.txtFechaRespaldo.Name = "txtFechaRespaldo";
            this.txtFechaRespaldo.ReadOnly = true;
            this.txtFechaRespaldo.Size = new System.Drawing.Size(189, 20);
            this.txtFechaRespaldo.TabIndex = 9;
            // 
            // txtEstacion
            // 
            this.txtEstacion.Location = new System.Drawing.Point(879, 492);
            this.txtEstacion.Name = "txtEstacion";
            this.txtEstacion.ReadOnly = true;
            this.txtEstacion.Size = new System.Drawing.Size(156, 20);
            this.txtEstacion.TabIndex = 10;
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(604, 496);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.ReadOnly = true;
            this.txtCosto.Size = new System.Drawing.Size(112, 20);
            this.txtCosto.TabIndex = 11;
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Location = new System.Drawing.Point(506, 499);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(95, 13);
            this.lblCosto.TabIndex = 12;
            this.lblCosto.Text = "Costo Importación:";
            // 
            // RestauraIBG
            // 
            this.AcceptButton = this.btnCargar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 524);
            this.Controls.Add(this.lblCosto);
            this.Controls.Add(this.txtCosto);
            this.Controls.Add(this.txtEstacion);
            this.Controls.Add(this.txtFechaRespaldo);
            this.Controls.Add(this.lblEstacion);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblRespaldo);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.dgvRespaldo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtIBG);
            this.Controls.Add(this.lblIBG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RestauraIBG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restaurar IBG";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespaldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIBG;
        private System.Windows.Forms.TextBox txtIBG;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvRespaldo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TextBox txtEstacion;
        private System.Windows.Forms.TextBox txtFechaRespaldo;
        private System.Windows.Forms.Label lblEstacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblRespaldo;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}