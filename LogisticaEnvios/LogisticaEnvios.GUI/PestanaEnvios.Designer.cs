namespace LogisticaEnvios.GUI
{
    partial class PestanaEnvios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.grpBuscar = new System.Windows.Forms.GroupBox();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.autoBuscar = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.grpRegistrar = new System.Windows.Forms.GroupBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.autoDestino = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblDestino = new System.Windows.Forms.Label();
            this.autoOrigen = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.txtDestinatario = new System.Windows.Forms.TextBox();
            this.lblDestinatario = new System.Windows.Forms.Label();
            this.txtRemitente = new System.Windows.Forms.TextBox();
            this.lblRemitente = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblVacia = new System.Windows.Forms.Label();
            this.lblEliminada = new System.Windows.Forms.Label();
            this.lblOcupada = new System.Windows.Forms.Label();
            this.lblExplicacion = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.dgvTabla = new System.Windows.Forms.DataGridView();
            this.colPosicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemitente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinatario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelIzquierdo.SuspendLayout();
            this.grpBuscar.SuspendLayout();
            this.grpRegistrar.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).BeginInit();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpBuscar);
            this.panelIzquierdo.Controls.Add(this.grpRegistrar);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpRegistrar
            //
            this.grpRegistrar.Controls.Add(this.btnRegistrar);
            this.grpRegistrar.Controls.Add(this.autoDestino);
            this.grpRegistrar.Controls.Add(this.lblDestino);
            this.grpRegistrar.Controls.Add(this.autoOrigen);
            this.grpRegistrar.Controls.Add(this.lblOrigen);
            this.grpRegistrar.Controls.Add(this.txtDestinatario);
            this.grpRegistrar.Controls.Add(this.lblDestinatario);
            this.grpRegistrar.Controls.Add(this.txtRemitente);
            this.grpRegistrar.Controls.Add(this.lblRemitente);
            this.grpRegistrar.Controls.Add(this.txtCodigo);
            this.grpRegistrar.Controls.Add(this.lblCodigo);
            this.grpRegistrar.Location = new System.Drawing.Point(8, 8);
            this.grpRegistrar.Name = "grpRegistrar";
            this.grpRegistrar.Size = new System.Drawing.Size(316, 320);
            this.grpRegistrar.TabIndex = 0;
            this.grpRegistrar.TabStop = false;
            this.grpRegistrar.Text = "Registrar envío";
            //
            // lblCodigo
            //
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 24);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(103, 15);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código de rastreo";
            //
            // txtCodigo
            //
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(12, 42);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PlaceholderText = "Ej. ENV-3001";
            this.txtCodigo.Size = new System.Drawing.Size(292, 23);
            this.txtCodigo.TabIndex = 1;
            //
            // lblRemitente
            //
            this.lblRemitente.AutoSize = true;
            this.lblRemitente.Location = new System.Drawing.Point(12, 72);
            this.lblRemitente.Name = "lblRemitente";
            this.lblRemitente.Size = new System.Drawing.Size(61, 15);
            this.lblRemitente.TabIndex = 2;
            this.lblRemitente.Text = "Remitente";
            //
            // txtRemitente
            //
            this.txtRemitente.Location = new System.Drawing.Point(12, 90);
            this.txtRemitente.Name = "txtRemitente";
            this.txtRemitente.Size = new System.Drawing.Size(292, 23);
            this.txtRemitente.TabIndex = 3;
            //
            // lblDestinatario
            //
            this.lblDestinatario.AutoSize = true;
            this.lblDestinatario.Location = new System.Drawing.Point(12, 120);
            this.lblDestinatario.Name = "lblDestinatario";
            this.lblDestinatario.Size = new System.Drawing.Size(71, 15);
            this.lblDestinatario.TabIndex = 4;
            this.lblDestinatario.Text = "Destinatario";
            //
            // txtDestinatario
            //
            this.txtDestinatario.Location = new System.Drawing.Point(12, 138);
            this.txtDestinatario.Name = "txtDestinatario";
            this.txtDestinatario.Size = new System.Drawing.Size(292, 23);
            this.txtDestinatario.TabIndex = 5;
            //
            // lblOrigen
            //
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Location = new System.Drawing.Point(12, 168);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(98, 15);
            this.lblOrigen.TabIndex = 6;
            this.lblOrigen.Text = "Ciudad de origen";
            //
            // autoOrigen
            //
            this.autoOrigen.Location = new System.Drawing.Point(12, 186);
            this.autoOrigen.Name = "autoOrigen";
            this.autoOrigen.Size = new System.Drawing.Size(292, 23);
            this.autoOrigen.TabIndex = 7;
            this.autoOrigen.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblDestino
            //
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(12, 216);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(101, 15);
            this.lblDestino.TabIndex = 8;
            this.lblDestino.Text = "Ciudad de destino";
            //
            // autoDestino
            //
            this.autoDestino.Location = new System.Drawing.Point(12, 234);
            this.autoDestino.Name = "autoDestino";
            this.autoDestino.Size = new System.Drawing.Size(292, 23);
            this.autoDestino.TabIndex = 9;
            this.autoDestino.TextoGuia = "Escriba el inicio del nombre";
            //
            // btnRegistrar
            //
            this.btnRegistrar.Location = new System.Drawing.Point(12, 272);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(292, 32);
            this.btnRegistrar.TabIndex = 10;
            this.btnRegistrar.Text = "Registrar envío";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            //
            // grpBuscar
            //
            this.grpBuscar.Controls.Add(this.txtDetalle);
            this.grpBuscar.Controls.Add(this.btnEliminar);
            this.grpBuscar.Controls.Add(this.btnBuscar);
            this.grpBuscar.Controls.Add(this.autoBuscar);
            this.grpBuscar.Controls.Add(this.lblBuscar);
            this.grpBuscar.Location = new System.Drawing.Point(8, 336);
            this.grpBuscar.Name = "grpBuscar";
            this.grpBuscar.Size = new System.Drawing.Size(316, 300);
            this.grpBuscar.TabIndex = 1;
            this.grpBuscar.TabStop = false;
            this.grpBuscar.Text = "Buscar o eliminar envío";
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(12, 24);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(232, 15);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Código o su inicio (el Trie sugiere códigos)";
            //
            // autoBuscar
            //
            this.autoBuscar.Location = new System.Drawing.Point(12, 42);
            this.autoBuscar.Name = "autoBuscar";
            this.autoBuscar.Size = new System.Drawing.Size(292, 23);
            this.autoBuscar.TabIndex = 1;
            this.autoBuscar.TextoGuia = "Ej. ENV-1";
            //
            // btnBuscar
            //
            this.btnBuscar.Location = new System.Drawing.Point(12, 74);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(140, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(164, 74);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(140, 30);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // txtDetalle
            //
            this.txtDetalle.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetalle.Location = new System.Drawing.Point(12, 114);
            this.txtDetalle.Multiline = true;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.ReadOnly = true;
            this.txtDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalle.Size = new System.Drawing.Size(292, 174);
            this.txtDetalle.TabIndex = 4;
            this.txtDetalle.TabStop = false;
            //
            // panelSuperior
            //
            this.panelSuperior.Controls.Add(this.lblVacia);
            this.panelSuperior.Controls.Add(this.lblEliminada);
            this.panelSuperior.Controls.Add(this.lblOcupada);
            this.panelSuperior.Controls.Add(this.lblExplicacion);
            this.panelSuperior.Controls.Add(this.lblResumen);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(345, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(931, 62);
            this.panelSuperior.TabIndex = 1;
            //
            // lblResumen
            //
            this.lblResumen.AutoSize = true;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResumen.Location = new System.Drawing.Point(10, 9);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(60, 19);
            this.lblResumen.TabIndex = 0;
            this.lblResumen.Text = "Envíos: 0";
            //
            // lblExplicacion
            //
            this.lblExplicacion.AutoSize = true;
            this.lblExplicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblExplicacion.Location = new System.Drawing.Point(10, 36);
            this.lblExplicacion.Name = "lblExplicacion";
            this.lblExplicacion.Size = new System.Drawing.Size(560, 15);
            this.lblExplicacion.TabIndex = 1;
            this.lblExplicacion.Text = "Direccionamiento abierto con sondeo lineal: h(k) = k mod m. Si el factor de carga pasa de 0.70 se hace rehashing (m = primer primo válido ≥ 2m + 1).";
            //
            // lblOcupada
            //
            this.lblOcupada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOcupada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.lblOcupada.Location = new System.Drawing.Point(643, 8);
            this.lblOcupada.Name = "lblOcupada";
            this.lblOcupada.Size = new System.Drawing.Size(90, 22);
            this.lblOcupada.TabIndex = 2;
            this.lblOcupada.Text = "Ocupada";
            this.lblOcupada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblEliminada
            //
            this.lblEliminada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEliminada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblEliminada.Location = new System.Drawing.Point(739, 8);
            this.lblEliminada.Name = "lblEliminada";
            this.lblEliminada.Size = new System.Drawing.Size(90, 22);
            this.lblEliminada.TabIndex = 3;
            this.lblEliminada.Text = "Eliminada";
            this.lblEliminada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblVacia
            //
            this.lblVacia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVacia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblVacia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVacia.Location = new System.Drawing.Point(835, 8);
            this.lblVacia.Name = "lblVacia";
            this.lblVacia.Size = new System.Drawing.Size(90, 22);
            this.lblVacia.TabIndex = 4;
            this.lblVacia.Text = "Vacía";
            this.lblVacia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // dgvTabla
            //
            this.dgvTabla.AllowUserToAddRows = false;
            this.dgvTabla.AllowUserToDeleteRows = false;
            this.dgvTabla.AllowUserToResizeRows = false;
            this.dgvTabla.BackgroundColor = System.Drawing.Color.White;
            this.dgvTabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPosicion,
            this.colEstado,
            this.colCodigo,
            this.colRemitente,
            this.colDestinatario,
            this.colOrigen,
            this.colDestino});
            this.dgvTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTabla.Location = new System.Drawing.Point(345, 62);
            this.dgvTabla.MultiSelect = false;
            this.dgvTabla.Name = "dgvTabla";
            this.dgvTabla.ReadOnly = true;
            this.dgvTabla.RowHeadersVisible = false;
            this.dgvTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTabla.Size = new System.Drawing.Size(931, 641);
            this.dgvTabla.TabIndex = 2;
            this.dgvTabla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabla_CellClick);
            //
            // colPosicion
            //
            this.colPosicion.HeaderText = "Posición";
            this.colPosicion.Name = "colPosicion";
            this.colPosicion.ReadOnly = true;
            this.colPosicion.Width = 70;
            //
            // colEstado
            //
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 90;
            //
            // colCodigo
            //
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            //
            // colRemitente
            //
            this.colRemitente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRemitente.HeaderText = "Remitente";
            this.colRemitente.Name = "colRemitente";
            this.colRemitente.ReadOnly = true;
            //
            // colDestinatario
            //
            this.colDestinatario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDestinatario.HeaderText = "Destinatario";
            this.colDestinatario.Name = "colDestinatario";
            this.colDestinatario.ReadOnly = true;
            //
            // colOrigen
            //
            this.colOrigen.HeaderText = "Origen";
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            this.colOrigen.Width = 130;
            //
            // colDestino
            //
            this.colDestino.HeaderText = "Destino";
            this.colDestino.Name = "colDestino";
            this.colDestino.ReadOnly = true;
            this.colDestino.Width = 130;
            //
            // PestanaEnvios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTabla);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaEnvios";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpBuscar.ResumeLayout(false);
            this.grpBuscar.PerformLayout();
            this.grpRegistrar.ResumeLayout(false);
            this.grpRegistrar.PerformLayout();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpRegistrar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblRemitente;
        private System.Windows.Forms.TextBox txtRemitente;
        private System.Windows.Forms.Label lblDestinatario;
        private System.Windows.Forms.TextBox txtDestinatario;
        private System.Windows.Forms.Label lblOrigen;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoOrigen;
        private System.Windows.Forms.Label lblDestino;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoDestino;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox grpBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.Label lblOcupada;
        private System.Windows.Forms.Label lblEliminada;
        private System.Windows.Forms.Label lblVacia;
        private System.Windows.Forms.DataGridView dgvTabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemitente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestinatario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestino;
    }
}
