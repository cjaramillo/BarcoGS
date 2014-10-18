namespace Barco
{
    partial class ReportesImportaciones
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
            this.rbAuditaIBG = new System.Windows.Forms.RadioButton();
            this.rbAlertasProduccion = new System.Windows.Forms.RadioButton();
            this.rbAlertasEntrega = new System.Windows.Forms.RadioButton();
            this.rbPlanCompras = new System.Windows.Forms.RadioButton();
            this.rbReporteAnticipos = new System.Windows.Forms.RadioButton();
            this.rbAnticiposOCIG = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblIG = new System.Windows.Forms.Label();
            this.lblIbg = new System.Windows.Forms.Label();
            this.txtIG = new System.Windows.Forms.TextBox();
            this.txtIBG = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbComprasPagos = new System.Windows.Forms.ComboBox();
            this.chkAnticipos = new System.Windows.Forms.CheckBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.rbCruceAnticiposOCS = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // rbAuditaIBG
            // 
            this.rbAuditaIBG.AutoSize = true;
            this.rbAuditaIBG.Location = new System.Drawing.Point(12, 22);
            this.rbAuditaIBG.Name = "rbAuditaIBG";
            this.rbAuditaIBG.Size = new System.Drawing.Size(79, 17);
            this.rbAuditaIBG.TabIndex = 0;
            this.rbAuditaIBG.TabStop = true;
            this.rbAuditaIBG.Text = "Auditar IBG";
            this.rbAuditaIBG.UseVisualStyleBackColor = true;
            this.rbAuditaIBG.CheckedChanged += new System.EventHandler(this.rbAuditaIBG_CheckedChanged);
            // 
            // rbAlertasProduccion
            // 
            this.rbAlertasProduccion.AutoSize = true;
            this.rbAlertasProduccion.Location = new System.Drawing.Point(12, 59);
            this.rbAlertasProduccion.Name = "rbAlertasProduccion";
            this.rbAlertasProduccion.Size = new System.Drawing.Size(114, 17);
            this.rbAlertasProduccion.TabIndex = 1;
            this.rbAlertasProduccion.TabStop = true;
            this.rbAlertasProduccion.Text = "Alertas Producción";
            this.rbAlertasProduccion.UseVisualStyleBackColor = true;
            this.rbAlertasProduccion.CheckedChanged += new System.EventHandler(this.rbAlertasProduccion_CheckedChanged);
            // 
            // rbAlertasEntrega
            // 
            this.rbAlertasEntrega.AutoSize = true;
            this.rbAlertasEntrega.Location = new System.Drawing.Point(12, 96);
            this.rbAlertasEntrega.Name = "rbAlertasEntrega";
            this.rbAlertasEntrega.Size = new System.Drawing.Size(97, 17);
            this.rbAlertasEntrega.TabIndex = 2;
            this.rbAlertasEntrega.TabStop = true;
            this.rbAlertasEntrega.Text = "Alertas Entrega";
            this.rbAlertasEntrega.UseVisualStyleBackColor = true;
            this.rbAlertasEntrega.CheckedChanged += new System.EventHandler(this.rbAlertasEntrega_CheckedChanged);
            // 
            // rbPlanCompras
            // 
            this.rbPlanCompras.AutoSize = true;
            this.rbPlanCompras.Location = new System.Drawing.Point(12, 133);
            this.rbPlanCompras.Name = "rbPlanCompras";
            this.rbPlanCompras.Size = new System.Drawing.Size(105, 17);
            this.rbPlanCompras.TabIndex = 3;
            this.rbPlanCompras.TabStop = true;
            this.rbPlanCompras.Text = "Plan de Compras";
            this.rbPlanCompras.UseVisualStyleBackColor = true;
            this.rbPlanCompras.CheckedChanged += new System.EventHandler(this.rbPlanCompras_CheckedChanged);
            // 
            // rbReporteAnticipos
            // 
            this.rbReporteAnticipos.AutoSize = true;
            this.rbReporteAnticipos.Location = new System.Drawing.Point(12, 170);
            this.rbReporteAnticipos.Name = "rbReporteAnticipos";
            this.rbReporteAnticipos.Size = new System.Drawing.Size(107, 17);
            this.rbReporteAnticipos.TabIndex = 4;
            this.rbReporteAnticipos.TabStop = true;
            this.rbReporteAnticipos.Text = "Compras y Pagos";
            this.rbReporteAnticipos.UseVisualStyleBackColor = true;
            this.rbReporteAnticipos.CheckedChanged += new System.EventHandler(this.rbReporteAnticipos_CheckedChanged);
            // 
            // rbAnticiposOCIG
            // 
            this.rbAnticiposOCIG.AutoSize = true;
            this.rbAnticiposOCIG.Location = new System.Drawing.Point(12, 207);
            this.rbAnticiposOCIG.Name = "rbAnticiposOCIG";
            this.rbAnticiposOCIG.Size = new System.Drawing.Size(94, 17);
            this.rbAnticiposOCIG.TabIndex = 5;
            this.rbAnticiposOCIG.TabStop = true;
            this.rbAnticiposOCIG.Text = "Anticipos a PP";
            this.rbAnticiposOCIG.UseVisualStyleBackColor = true;
            this.rbAnticiposOCIG.CheckedChanged += new System.EventHandler(this.rbAnticiposOCIG_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblIG);
            this.groupBox1.Controls.Add(this.lblIbg);
            this.groupBox1.Controls.Add(this.txtIG);
            this.groupBox1.Controls.Add(this.txtIBG);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpHasta);
            this.groupBox1.Controls.Add(this.dtpDesde);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(220, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 165);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parámetros";
            // 
            // lblIG
            // 
            this.lblIG.AutoSize = true;
            this.lblIG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIG.Location = new System.Drawing.Point(48, 118);
            this.lblIG.Name = "lblIG";
            this.lblIG.Size = new System.Drawing.Size(18, 13);
            this.lblIG.TabIndex = 7;
            this.lblIG.Text = "IG";
            // 
            // lblIbg
            // 
            this.lblIbg.AutoSize = true;
            this.lblIbg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIbg.Location = new System.Drawing.Point(48, 76);
            this.lblIbg.Name = "lblIbg";
            this.lblIbg.Size = new System.Drawing.Size(25, 13);
            this.lblIbg.TabIndex = 6;
            this.lblIbg.Text = "IBG";
            // 
            // txtIG
            // 
            this.txtIG.Location = new System.Drawing.Point(102, 111);
            this.txtIG.Name = "txtIG";
            this.txtIG.Size = new System.Drawing.Size(140, 20);
            this.txtIG.TabIndex = 5;
            // 
            // txtIBG
            // 
            this.txtIBG.Location = new System.Drawing.Point(102, 74);
            this.txtIBG.Name = "txtIBG";
            this.txtIBG.Size = new System.Drawing.Size(140, 20);
            this.txtIBG.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(153, 37);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(89, 20);
            this.dtpHasta.TabIndex = 1;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(32, 37);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(89, 20);
            this.dtpDesde.TabIndex = 0;
            // 
            // cmbComprasPagos
            // 
            this.cmbComprasPagos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComprasPagos.FormattingEnabled = true;
            this.cmbComprasPagos.Items.AddRange(new object[] {
            "Todos",
            "Con Saldos",
            "Sin Saldos"});
            this.cmbComprasPagos.Location = new System.Drawing.Point(220, 206);
            this.cmbComprasPagos.Name = "cmbComprasPagos";
            this.cmbComprasPagos.Size = new System.Drawing.Size(121, 21);
            this.cmbComprasPagos.TabIndex = 7;
            // 
            // chkAnticipos
            // 
            this.chkAnticipos.AutoSize = true;
            this.chkAnticipos.Location = new System.Drawing.Point(371, 207);
            this.chkAnticipos.Name = "chkAnticipos";
            this.chkAnticipos.Size = new System.Drawing.Size(143, 17);
            this.chkAnticipos.TabIndex = 8;
            this.chkAnticipos.Text = "&Solo Anticipos desde OC";
            this.chkAnticipos.UseVisualStyleBackColor = true;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(133, 287);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 9;
            this.btnGenerar.Text = "&Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(344, 287);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // rbCruceAnticiposOCS
            // 
            this.rbCruceAnticiposOCS.AutoSize = true;
            this.rbCruceAnticiposOCS.Location = new System.Drawing.Point(12, 239);
            this.rbCruceAnticiposOCS.Name = "rbCruceAnticiposOCS";
            this.rbCruceAnticiposOCS.Size = new System.Drawing.Size(124, 17);
            this.rbCruceAnticiposOCS.TabIndex = 11;
            this.rbCruceAnticiposOCS.TabStop = true;
            this.rbCruceAnticiposOCS.Text = "Cruce Anticipos OCS";
            this.rbCruceAnticiposOCS.UseVisualStyleBackColor = true;
            this.rbCruceAnticiposOCS.CheckedChanged += new System.EventHandler(this.rbCruceAnticiposOCS_CheckedChanged);
            // 
            // ReportesImportaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 345);
            this.Controls.Add(this.rbCruceAnticiposOCS);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.chkAnticipos);
            this.Controls.Add(this.cmbComprasPagos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbAnticiposOCIG);
            this.Controls.Add(this.rbReporteAnticipos);
            this.Controls.Add(this.rbPlanCompras);
            this.Controls.Add(this.rbAlertasEntrega);
            this.Controls.Add(this.rbAlertasProduccion);
            this.Controls.Add(this.rbAuditaIBG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportesImportaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de Reporte";
            this.Load += new System.EventHandler(this.ReportesImportaciones_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbAuditaIBG;
        private System.Windows.Forms.RadioButton rbAlertasProduccion;
        private System.Windows.Forms.RadioButton rbAlertasEntrega;
        private System.Windows.Forms.RadioButton rbPlanCompras;
        private System.Windows.Forms.RadioButton rbReporteAnticipos;
        private System.Windows.Forms.RadioButton rbAnticiposOCIG;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblIG;
        private System.Windows.Forms.Label lblIbg;
        private System.Windows.Forms.TextBox txtIG;
        private System.Windows.Forms.TextBox txtIBG;
        private System.Windows.Forms.ComboBox cmbComprasPagos;
        private System.Windows.Forms.CheckBox chkAnticipos;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton rbCruceAnticiposOCS;
    }
}