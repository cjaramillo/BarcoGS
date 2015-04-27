namespace Barco
{
    partial class InputDataOCS
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
            this.dgvOCS = new System.Windows.Forms.DataGridView();
            this.lblTitDGV = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSolAnticipo = new System.Windows.Forms.Button();
            this.btnFactPrepago = new System.Windows.Forms.Button();
            this.txtSPAnticipo = new System.Windows.Forms.TextBox();
            this.txtFactPrepago = new System.Windows.Forms.TextBox();
            this.txtFactFinal = new System.Windows.Forms.TextBox();
            this.cmbCargoIG = new System.Windows.Forms.ComboBox();
            this.txtVenceAnt = new System.Windows.Forms.TextBox();
            this.txtVencePrepago = new System.Windows.Forms.TextBox();
            this.txtPagoAnticipo = new System.Windows.Forms.TextBox();
            this.txtPagoPrepago = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dtpRecibCot = new System.Windows.Forms.DateTimePicker();
            this.dtpFinManufactT = new System.Windows.Forms.DateTimePicker();
            this.lblRecibCot = new System.Windows.Forms.Label();
            this.lblFinManufactT = new System.Windows.Forms.Label();
            this.lblSolCot = new System.Windows.Forms.Label();
            this.dtpSolCot = new System.Windows.Forms.DateTimePicker();
            this.lblApruebaCot1 = new System.Windows.Forms.Label();
            this.dtpApruebaCot = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFinManufactR = new System.Windows.Forms.Label();
            this.dtpFinManufactR = new System.Windows.Forms.DateTimePicker();
            this.lblLlegaPuertoIntl = new System.Windows.Forms.Label();
            this.dtpLlegaPuertoIntl = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtMsgAnticipo = new System.Windows.Forms.TextBox();
            this.dtpVenceAnticipo = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtValAnticipo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnProcesaAnticipo = new System.Windows.Forms.Button();
            this.dgvFactPrepago = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpDespachoFab = new System.Windows.Forms.DateTimePicker();
            this.lblDespachoFab = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBuscaFact = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnBorraAnticipo = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblHelp = new System.Windows.Forms.Label();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblNroOCS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOCS)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactPrepago)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOCS
            // 
            this.dgvOCS.AllowUserToAddRows = false;
            this.dgvOCS.AllowUserToDeleteRows = false;
            this.dgvOCS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOCS.Location = new System.Drawing.Point(12, 43);
            this.dgvOCS.MultiSelect = false;
            this.dgvOCS.Name = "dgvOCS";
            this.dgvOCS.ReadOnly = true;
            this.dgvOCS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOCS.Size = new System.Drawing.Size(952, 276);
            this.dgvOCS.StandardTab = true;
            this.dgvOCS.TabIndex = 0;
            this.dgvOCS.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOCS_CellDoubleClick);
            this.dgvOCS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvOCS_KeyPress);
            // 
            // lblTitDGV
            // 
            this.lblTitDGV.AutoSize = true;
            this.lblTitDGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitDGV.Location = new System.Drawing.Point(13, 24);
            this.lblTitDGV.Name = "lblTitDGV";
            this.lblTitDGV.Size = new System.Drawing.Size(202, 13);
            this.lblTitDGV.TabIndex = 1;
            this.lblTitDGV.Text = "Ordenes de Compra Simples - OCS";
            // 
            // btnSolAnticipo
            // 
            this.btnSolAnticipo.Location = new System.Drawing.Point(88, 30);
            this.btnSolAnticipo.Name = "btnSolAnticipo";
            this.btnSolAnticipo.Size = new System.Drawing.Size(111, 23);
            this.btnSolAnticipo.TabIndex = 21;
            this.btnSolAnticipo.Text = "Solicitar Anticipo";
            this.toolTip1.SetToolTip(this.btnSolAnticipo, "Solicitar un nuevo anticipo ligado a esta orden de compra.\r\nEsto implica que no s" +
        "e iniciará la manufactura mientras no se cancele el anticipo.");
            this.btnSolAnticipo.UseVisualStyleBackColor = true;
            this.btnSolAnticipo.Click += new System.EventHandler(this.btnSolAnticipo_Click);
            // 
            // btnFactPrepago
            // 
            this.btnFactPrepago.Location = new System.Drawing.Point(88, 60);
            this.btnFactPrepago.Name = "btnFactPrepago";
            this.btnFactPrepago.Size = new System.Drawing.Size(111, 23);
            this.btnFactPrepago.TabIndex = 22;
            this.btnFactPrepago.Text = "Ligar Fact. Prep.";
            this.toolTip1.SetToolTip(this.btnFactPrepago, "Ligar una factura de compra como prepago.\r\nEsto implica que si no se paga el prov" +
        "eedor no despachará la mercadería.");
            this.btnFactPrepago.UseVisualStyleBackColor = true;
            this.btnFactPrepago.Click += new System.EventHandler(this.btnFactPrepago_Click);
            // 
            // txtSPAnticipo
            // 
            this.txtSPAnticipo.Location = new System.Drawing.Point(89, 31);
            this.txtSPAnticipo.Name = "txtSPAnticipo";
            this.txtSPAnticipo.Size = new System.Drawing.Size(90, 20);
            this.txtSPAnticipo.TabIndex = 23;
            this.toolTip1.SetToolTip(this.txtSPAnticipo, "Número de SP de Anticipo");
            // 
            // txtFactPrepago
            // 
            this.txtFactPrepago.Location = new System.Drawing.Point(89, 61);
            this.txtFactPrepago.Name = "txtFactPrepago";
            this.txtFactPrepago.Size = new System.Drawing.Size(90, 20);
            this.txtFactPrepago.TabIndex = 28;
            this.toolTip1.SetToolTip(this.txtFactPrepago, "Número de factura prepago");
            this.txtFactPrepago.Enter += new System.EventHandler(this.txtFactPrepago_Enter);
            this.txtFactPrepago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFactPrepago_KeyPress);
            // 
            // txtFactFinal
            // 
            this.txtFactFinal.Location = new System.Drawing.Point(90, 89);
            this.txtFactFinal.Name = "txtFactFinal";
            this.txtFactFinal.Size = new System.Drawing.Size(111, 20);
            this.txtFactFinal.TabIndex = 34;
            this.toolTip1.SetToolTip(this.txtFactFinal, "Numero de factura final (referencial) contra la cual se debe cruzar este anticipo" +
        ".");
            this.txtFactFinal.Enter += new System.EventHandler(this.txtFactFinal_Enter);
            // 
            // cmbCargoIG
            // 
            this.cmbCargoIG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCargoIG.FormattingEnabled = true;
            this.cmbCargoIG.Location = new System.Drawing.Point(90, 27);
            this.cmbCargoIG.Name = "cmbCargoIG";
            this.cmbCargoIG.Size = new System.Drawing.Size(111, 21);
            this.cmbCargoIG.TabIndex = 31;
            this.toolTip1.SetToolTip(this.cmbCargoIG, "Lista de OCC en las cuales fue consolidada la presente OCS");
            // 
            // txtVenceAnt
            // 
            this.txtVenceAnt.Location = new System.Drawing.Point(227, 32);
            this.txtVenceAnt.Name = "txtVenceAnt";
            this.txtVenceAnt.Size = new System.Drawing.Size(99, 20);
            this.txtVenceAnt.TabIndex = 33;
            this.toolTip1.SetToolTip(this.txtVenceAnt, "Fecha máxima de pago de Anticipo");
            // 
            // txtVencePrepago
            // 
            this.txtVencePrepago.Location = new System.Drawing.Point(228, 61);
            this.txtVencePrepago.Name = "txtVencePrepago";
            this.txtVencePrepago.Size = new System.Drawing.Size(98, 20);
            this.txtVencePrepago.TabIndex = 34;
            this.toolTip1.SetToolTip(this.txtVencePrepago, "Fecha máxima de pago de factura prepago");
            // 
            // txtPagoAnticipo
            // 
            this.txtPagoAnticipo.Location = new System.Drawing.Point(344, 32);
            this.txtPagoAnticipo.Name = "txtPagoAnticipo";
            this.txtPagoAnticipo.Size = new System.Drawing.Size(99, 20);
            this.txtPagoAnticipo.TabIndex = 35;
            this.toolTip1.SetToolTip(this.txtPagoAnticipo, "Fecha de pago de anticipo");
            // 
            // txtPagoPrepago
            // 
            this.txtPagoPrepago.Location = new System.Drawing.Point(344, 61);
            this.txtPagoPrepago.Name = "txtPagoPrepago";
            this.txtPagoPrepago.Size = new System.Drawing.Size(99, 20);
            this.txtPagoPrepago.TabIndex = 36;
            this.toolTip1.SetToolTip(this.txtPagoPrepago, "Fecha de pago de factura tipo prepago");
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::Barco.Properties.Resources.borrar_ico;
            this.btnEliminar.Location = new System.Drawing.Point(180, 60);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(20, 21);
            this.btnEliminar.TabIndex = 37;
            this.toolTip1.SetToolTip(this.btnEliminar, "Eliminar la relación entre la factura prepago y la orden de compra");
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dtpRecibCot
            // 
            this.dtpRecibCot.Checked = false;
            this.dtpRecibCot.CustomFormat = "dd/mm/aaaa";
            this.dtpRecibCot.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRecibCot.Location = new System.Drawing.Point(130, 27);
            this.dtpRecibCot.Name = "dtpRecibCot";
            this.dtpRecibCot.ShowCheckBox = true;
            this.dtpRecibCot.Size = new System.Drawing.Size(99, 20);
            this.dtpRecibCot.TabIndex = 2;
            this.dtpRecibCot.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpRecibCot.Enter += new System.EventHandler(this.dtpRecibCot_Enter);
            // 
            // dtpFinManufactT
            // 
            this.dtpFinManufactT.Checked = false;
            this.dtpFinManufactT.CustomFormat = "dd/mm/aaaa";
            this.dtpFinManufactT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinManufactT.Location = new System.Drawing.Point(382, 27);
            this.dtpFinManufactT.Name = "dtpFinManufactT";
            this.dtpFinManufactT.ShowCheckBox = true;
            this.dtpFinManufactT.Size = new System.Drawing.Size(99, 20);
            this.dtpFinManufactT.TabIndex = 3;
            this.dtpFinManufactT.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFinManufactT.Enter += new System.EventHandler(this.dtpFinManufactT_Enter);
            // 
            // lblRecibCot
            // 
            this.lblRecibCot.AutoSize = true;
            this.lblRecibCot.Location = new System.Drawing.Point(127, 11);
            this.lblRecibCot.Name = "lblRecibCot";
            this.lblRecibCot.Size = new System.Drawing.Size(111, 13);
            this.lblRecibCot.TabIndex = 4;
            this.lblRecibCot.Text = "GS Recibe Cotización";
            // 
            // lblFinManufactT
            // 
            this.lblFinManufactT.AutoSize = true;
            this.lblFinManufactT.Location = new System.Drawing.Point(378, 11);
            this.lblFinManufactT.Name = "lblFinManufactT";
            this.lblFinManufactT.Size = new System.Drawing.Size(132, 13);
            this.lblFinManufactT.TabIndex = 5;
            this.lblFinManufactT.Text = "Fin Manufactura Tentativa";
            // 
            // lblSolCot
            // 
            this.lblSolCot.AutoSize = true;
            this.lblSolCot.Location = new System.Drawing.Point(3, 11);
            this.lblSolCot.Name = "lblSolCot";
            this.lblSolCot.Size = new System.Drawing.Size(111, 13);
            this.lblSolCot.TabIndex = 8;
            this.lblSolCot.Text = "GS Solicita Cotización";
            // 
            // dtpSolCot
            // 
            this.dtpSolCot.Checked = false;
            this.dtpSolCot.CustomFormat = "dd/mm/aaaa";
            this.dtpSolCot.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSolCot.Location = new System.Drawing.Point(6, 27);
            this.dtpSolCot.Name = "dtpSolCot";
            this.dtpSolCot.ShowCheckBox = true;
            this.dtpSolCot.Size = new System.Drawing.Size(99, 20);
            this.dtpSolCot.TabIndex = 7;
            this.dtpSolCot.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpSolCot.Enter += new System.EventHandler(this.dtpSolCot_Enter);
            // 
            // lblApruebaCot1
            // 
            this.lblApruebaCot1.AutoSize = true;
            this.lblApruebaCot1.Location = new System.Drawing.Point(251, 11);
            this.lblApruebaCot1.Name = "lblApruebaCot1";
            this.lblApruebaCot1.Size = new System.Drawing.Size(117, 13);
            this.lblApruebaCot1.TabIndex = 10;
            this.lblApruebaCot1.Text = "GS Aprueba Cotización";
            // 
            // dtpApruebaCot
            // 
            this.dtpApruebaCot.Checked = false;
            this.dtpApruebaCot.CustomFormat = "dd/mm/aaaa";
            this.dtpApruebaCot.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpApruebaCot.Location = new System.Drawing.Point(254, 27);
            this.dtpApruebaCot.Name = "dtpApruebaCot";
            this.dtpApruebaCot.ShowCheckBox = true;
            this.dtpApruebaCot.Size = new System.Drawing.Size(99, 20);
            this.dtpApruebaCot.TabIndex = 9;
            this.dtpApruebaCot.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpApruebaCot.Enter += new System.EventHandler(this.dtpApruebaCot_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Pagos GS";
            // 
            // lblFinManufactR
            // 
            this.lblFinManufactR.AutoSize = true;
            this.lblFinManufactR.Location = new System.Drawing.Point(521, 11);
            this.lblFinManufactR.Name = "lblFinManufactR";
            this.lblFinManufactR.Size = new System.Drawing.Size(109, 13);
            this.lblFinManufactR.TabIndex = 16;
            this.lblFinManufactR.Text = "Fin Manufactura Real";
            // 
            // dtpFinManufactR
            // 
            this.dtpFinManufactR.Checked = false;
            this.dtpFinManufactR.CustomFormat = "dd/mm/aaaa";
            this.dtpFinManufactR.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinManufactR.Location = new System.Drawing.Point(524, 27);
            this.dtpFinManufactR.Name = "dtpFinManufactR";
            this.dtpFinManufactR.ShowCheckBox = true;
            this.dtpFinManufactR.Size = new System.Drawing.Size(99, 20);
            this.dtpFinManufactR.TabIndex = 15;
            this.dtpFinManufactR.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFinManufactR.Enter += new System.EventHandler(this.dtpFinManufactR_Enter);
            // 
            // lblLlegaPuertoIntl
            // 
            this.lblLlegaPuertoIntl.AutoSize = true;
            this.lblLlegaPuertoIntl.Location = new System.Drawing.Point(773, 11);
            this.lblLlegaPuertoIntl.Name = "lblLlegaPuertoIntl";
            this.lblLlegaPuertoIntl.Size = new System.Drawing.Size(105, 13);
            this.lblLlegaPuertoIntl.TabIndex = 18;
            this.lblLlegaPuertoIntl.Text = "Llegada a Puerto Intl";
            // 
            // dtpLlegaPuertoIntl
            // 
            this.dtpLlegaPuertoIntl.Checked = false;
            this.dtpLlegaPuertoIntl.CustomFormat = "dd/mm/aaaa";
            this.dtpLlegaPuertoIntl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLlegaPuertoIntl.Location = new System.Drawing.Point(776, 27);
            this.dtpLlegaPuertoIntl.Name = "dtpLlegaPuertoIntl";
            this.dtpLlegaPuertoIntl.ShowCheckBox = true;
            this.dtpLlegaPuertoIntl.Size = new System.Drawing.Size(99, 20);
            this.dtpLlegaPuertoIntl.TabIndex = 17;
            this.dtpLlegaPuertoIntl.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpLlegaPuertoIntl.Enter += new System.EventHandler(this.dtpLlegaPuertoIntl_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Anticipo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Fact. Prepago:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnCancelar);
            this.panel3.Controls.Add(this.txtMsgAnticipo);
            this.panel3.Controls.Add(this.cmbCargoIG);
            this.panel3.Controls.Add(this.dtpVenceAnticipo);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.txtValAnticipo);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.txtFactFinal);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.btnProcesaAnticipo);
            this.panel3.Location = new System.Drawing.Point(521, 431);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(443, 117);
            this.panel3.TabIndex = 30;
            this.panel3.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(264, 86);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 42;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtMsgAnticipo
            // 
            this.txtMsgAnticipo.Enabled = false;
            this.txtMsgAnticipo.Location = new System.Drawing.Point(264, 61);
            this.txtMsgAnticipo.Name = "txtMsgAnticipo";
            this.txtMsgAnticipo.Size = new System.Drawing.Size(171, 20);
            this.txtMsgAnticipo.TabIndex = 41;
            // 
            // dtpVenceAnticipo
            // 
            this.dtpVenceAnticipo.CustomFormat = "dd/mm/aaaa";
            this.dtpVenceAnticipo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVenceAnticipo.Location = new System.Drawing.Point(336, 29);
            this.dtpVenceAnticipo.Name = "dtpVenceAnticipo";
            this.dtpVenceAnticipo.Size = new System.Drawing.Size(99, 20);
            this.dtpVenceAnticipo.TabIndex = 40;
            this.dtpVenceAnticipo.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpVenceAnticipo.Enter += new System.EventHandler(this.dtpVenceAnticipo_Enter);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(261, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 39;
            this.label15.Text = "Pagar Hasta:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "Valor Anticipo";
            // 
            // txtValAnticipo
            // 
            this.txtValAnticipo.Location = new System.Drawing.Point(90, 59);
            this.txtValAnticipo.Name = "txtValAnticipo";
            this.txtValAnticipo.Size = new System.Drawing.Size(111, 20);
            this.txtValAnticipo.TabIndex = 36;
            this.txtValAnticipo.Enter += new System.EventHandler(this.txtValAnticipo_Enter);
            this.txtValAnticipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValAnticipo_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Nro. Fact Final";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Cargar a IG:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(200, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Datos para Solicitud de Anticipos:";
            // 
            // btnProcesaAnticipo
            // 
            this.btnProcesaAnticipo.Location = new System.Drawing.Point(360, 86);
            this.btnProcesaAnticipo.Name = "btnProcesaAnticipo";
            this.btnProcesaAnticipo.Size = new System.Drawing.Size(75, 23);
            this.btnProcesaAnticipo.TabIndex = 0;
            this.btnProcesaAnticipo.Text = "Procesar";
            this.btnProcesaAnticipo.UseVisualStyleBackColor = true;
            this.btnProcesaAnticipo.Click += new System.EventHandler(this.btnProcesaAnticipo_Click);
            // 
            // dgvFactPrepago
            // 
            this.dgvFactPrepago.AllowUserToAddRows = false;
            this.dgvFactPrepago.AllowUserToDeleteRows = false;
            this.dgvFactPrepago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactPrepago.Location = new System.Drawing.Point(521, 431);
            this.dgvFactPrepago.MultiSelect = false;
            this.dgvFactPrepago.Name = "dgvFactPrepago";
            this.dgvFactPrepago.ReadOnly = true;
            this.dgvFactPrepago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFactPrepago.Size = new System.Drawing.Size(443, 117);
            this.dgvFactPrepago.StandardTab = true;
            this.dgvFactPrepago.TabIndex = 37;
            this.dgvFactPrepago.Visible = false;
            this.dgvFactPrepago.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFactPrepago_CellDoubleClick);
            this.dgvFactPrepago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvFactPrepago_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtpDespachoFab);
            this.panel1.Controls.Add(this.lblDespachoFab);
            this.panel1.Controls.Add(this.lblSolCot);
            this.panel1.Controls.Add(this.dtpRecibCot);
            this.panel1.Controls.Add(this.dtpFinManufactT);
            this.panel1.Controls.Add(this.lblRecibCot);
            this.panel1.Controls.Add(this.lblFinManufactT);
            this.panel1.Controls.Add(this.dtpSolCot);
            this.panel1.Controls.Add(this.dtpApruebaCot);
            this.panel1.Controls.Add(this.lblApruebaCot1);
            this.panel1.Controls.Add(this.dtpFinManufactR);
            this.panel1.Controls.Add(this.lblFinManufactR);
            this.panel1.Controls.Add(this.dtpLlegaPuertoIntl);
            this.panel1.Controls.Add(this.lblLlegaPuertoIntl);
            this.panel1.Location = new System.Drawing.Point(12, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 63);
            this.panel1.TabIndex = 31;
            this.panel1.Visible = false;
            // 
            // dtpDespachoFab
            // 
            this.dtpDespachoFab.Checked = false;
            this.dtpDespachoFab.CustomFormat = "dd/mm/aaaa";
            this.dtpDespachoFab.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDespachoFab.Location = new System.Drawing.Point(649, 27);
            this.dtpDespachoFab.Name = "dtpDespachoFab";
            this.dtpDespachoFab.ShowCheckBox = true;
            this.dtpDespachoFab.Size = new System.Drawing.Size(99, 20);
            this.dtpDespachoFab.TabIndex = 19;
            this.dtpDespachoFab.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpDespachoFab.Enter += new System.EventHandler(this.dtpDespachoFab_Enter);
            // 
            // lblDespachoFab
            // 
            this.lblDespachoFab.AutoSize = true;
            this.lblDespachoFab.Location = new System.Drawing.Point(646, 11);
            this.lblDespachoFab.Name = "lblDespachoFab";
            this.lblDespachoFab.Size = new System.Drawing.Size(94, 13);
            this.lblDespachoFab.TabIndex = 20;
            this.lblDespachoFab.Text = "Despacho Fábrica";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnFactPrepago);
            this.panel2.Controls.Add(this.btnBuscaFact);
            this.panel2.Controls.Add(this.txtPagoPrepago);
            this.panel2.Controls.Add(this.txtPagoAnticipo);
            this.panel2.Controls.Add(this.txtVencePrepago);
            this.panel2.Controls.Add(this.txtVenceAnt);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.btnSolAnticipo);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtFactPrepago);
            this.panel2.Controls.Add(this.txtSPAnticipo);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.btnBorraAnticipo);
            this.panel2.Location = new System.Drawing.Point(12, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(492, 117);
            this.panel2.TabIndex = 32;
            this.panel2.Visible = false;
            // 
            // btnBuscaFact
            // 
            this.btnBuscaFact.Image = global::Barco.Properties.Resources.buscar_ico;
            this.btnBuscaFact.Location = new System.Drawing.Point(179, 61);
            this.btnBuscaFact.Name = "btnBuscaFact";
            this.btnBuscaFact.Size = new System.Drawing.Size(20, 21);
            this.btnBuscaFact.TabIndex = 40;
            this.btnBuscaFact.UseVisualStyleBackColor = true;
            this.btnBuscaFact.Visible = false;
            this.btnBuscaFact.Click += new System.EventHandler(this.btnBuscaFact_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(225, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 13);
            this.label19.TabIndex = 32;
            this.label19.Text = "Vencimiento";
            // 
            // btnBorraAnticipo
            // 
            this.btnBorraAnticipo.Image = global::Barco.Properties.Resources.borrar_ico;
            this.btnBorraAnticipo.Location = new System.Drawing.Point(180, 31);
            this.btnBorraAnticipo.Name = "btnBorraAnticipo";
            this.btnBorraAnticipo.Size = new System.Drawing.Size(20, 21);
            this.btnBorraAnticipo.TabIndex = 38;
            this.toolTip1.SetToolTip(this.btnBorraAnticipo, "Eliminar el anticipo");
            this.btnBorraAnticipo.UseVisualStyleBackColor = true;
            this.btnBorraAnticipo.Visible = false;
            this.btnBorraAnticipo.Click += new System.EventHandler(this.btnBorraAnticipo_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.ForeColor = System.Drawing.Color.Red;
            this.lblHelp.Location = new System.Drawing.Point(238, 24);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(305, 13);
            this.lblHelp.TabIndex = 34;
            this.lblHelp.Text = "Realice doble clic sobre la orden de compra que desea trabajar";
            // 
            // btnUndo
            // 
            this.btnUndo.BackgroundImage = global::Barco.Properties.Resources.iconoDeshacer_22px;
            this.btnUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUndo.Location = new System.Drawing.Point(915, 379);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(30, 30);
            this.btnUndo.TabIndex = 35;
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Visible = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::Barco.Properties.Resources.Guardar_22px;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.Location = new System.Drawing.Point(915, 343);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 30);
            this.btnSave.TabIndex = 33;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblNroOCS
            // 
            this.lblNroOCS.AutoSize = true;
            this.lblNroOCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNroOCS.ForeColor = System.Drawing.Color.Blue;
            this.lblNroOCS.Location = new System.Drawing.Point(734, 24);
            this.lblNroOCS.Name = "lblNroOCS";
            this.lblNroOCS.Size = new System.Drawing.Size(94, 16);
            this.lblNroOCS.TabIndex = 36;
            this.lblNroOCS.Text = "IG-XXX-20XX";
            this.lblNroOCS.Visible = false;
            // 
            // InputDataOCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 560);
            this.Controls.Add(this.dgvFactPrepago);
            this.Controls.Add(this.lblNroOCS);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblTitDGV);
            this.Controls.Add(this.dgvOCS);
            this.Name = "InputDataOCS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso Fechas - Ordenes de Compra";
            this.Load += new System.EventHandler(this.InputDataOCS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOCS)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactPrepago)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOCS;
        private System.Windows.Forms.Label lblTitDGV;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtpFinManufactT;
        private System.Windows.Forms.Label lblRecibCot;
        private System.Windows.Forms.Label lblFinManufactT;
        private System.Windows.Forms.Label lblSolCot;
        private System.Windows.Forms.DateTimePicker dtpSolCot;
        private System.Windows.Forms.Label lblApruebaCot1;
        private System.Windows.Forms.DateTimePicker dtpApruebaCot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFinManufactR;
        private System.Windows.Forms.DateTimePicker dtpFinManufactR;
        private System.Windows.Forms.Label lblLlegaPuertoIntl;
        private System.Windows.Forms.DateTimePicker dtpLlegaPuertoIntl;
        private System.Windows.Forms.Button btnSolAnticipo;
        private System.Windows.Forms.Button btnFactPrepago;
        private System.Windows.Forms.TextBox txtSPAnticipo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFactPrepago;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnProcesaAnticipo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtValAnticipo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtFactFinal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpVenceAnticipo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dtpDespachoFab;
        private System.Windows.Forms.Label lblDespachoFab;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.ComboBox cmbCargoIG;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpRecibCot;
        private System.Windows.Forms.TextBox txtPagoPrepago;
        private System.Windows.Forms.TextBox txtPagoAnticipo;
        private System.Windows.Forms.TextBox txtVencePrepago;
        private System.Windows.Forms.TextBox txtVenceAnt;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblNroOCS;
        private System.Windows.Forms.TextBox txtMsgAnticipo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnBorraAnticipo;
        private System.Windows.Forms.DataGridView dgvFactPrepago;
        private System.Windows.Forms.Button btnBuscaFact;
    }
}