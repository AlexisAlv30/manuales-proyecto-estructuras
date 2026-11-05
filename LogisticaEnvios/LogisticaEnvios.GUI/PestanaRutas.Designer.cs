namespace LogisticaEnvios.GUI
{
    partial class PestanaRutas
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
            this.grpResultado = new System.Windows.Forms.GroupBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.grpConsulta = new System.Windows.Forms.GroupBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.rdoTiempo = new System.Windows.Forms.RadioButton();
            this.rdoCosto = new System.Windows.Forms.RadioButton();
            this.autoDestino = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblDestino = new System.Windows.Forms.Label();
            this.autoOrigen = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.divisor = new System.Windows.Forms.SplitContainer();
            this.panelMapa = new LogisticaEnvios.GUI.PanelMapa();
            this.dgvDijkstra = new System.Windows.Forms.DataGridView();
            this.colCiudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAnterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnCamino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTabla = new System.Windows.Forms.Label();
            this.panelIzquierdo.SuspendLayout();
            this.grpResultado.SuspendLayout();
            this.grpConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).BeginInit();
            this.divisor.Panel1.SuspendLayout();
            this.divisor.Panel2.SuspendLayout();
            this.divisor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDijkstra)).BeginInit();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpResultado);
            this.panelIzquierdo.Controls.Add(this.grpConsulta);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpConsulta
            //
            this.grpConsulta.Controls.Add(this.lblAyuda);
            this.grpConsulta.Controls.Add(this.btnLimpiar);
            this.grpConsulta.Controls.Add(this.btnCalcular);
            this.grpConsulta.Controls.Add(this.rdoTiempo);
            this.grpConsulta.Controls.Add(this.rdoCosto);
            this.grpConsulta.Controls.Add(this.autoDestino);
            this.grpConsulta.Controls.Add(this.lblDestino);
            this.grpConsulta.Controls.Add(this.autoOrigen);
            this.grpConsulta.Controls.Add(this.lblOrigen);
            this.grpConsulta.Location = new System.Drawing.Point(8, 8);
            this.grpConsulta.Name = "grpConsulta";
            this.grpConsulta.Size = new System.Drawing.Size(316, 264);
            this.grpConsulta.TabIndex = 0;
            this.grpConsulta.TabStop = false;
            this.grpConsulta.Text = "Calcular ruta entre dos ciudades";
            //
            // lblOrigen
            //
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Location = new System.Drawing.Point(12, 24);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(98, 15);
            this.lblOrigen.TabIndex = 0;
            this.lblOrigen.Text = "Ciudad de origen";
            //
            // autoOrigen
            //
            this.autoOrigen.Location = new System.Drawing.Point(12, 42);
            this.autoOrigen.Name = "autoOrigen";
            this.autoOrigen.Size = new System.Drawing.Size(292, 23);
            this.autoOrigen.TabIndex = 1;
            this.autoOrigen.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblDestino
            //
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(12, 72);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(101, 15);
            this.lblDestino.TabIndex = 2;
            this.lblDestino.Text = "Ciudad de destino";
            //
            // autoDestino
            //
            this.autoDestino.Location = new System.Drawing.Point(12, 90);
            this.autoDestino.Name = "autoDestino";
            this.autoDestino.Size = new System.Drawing.Size(292, 23);
            this.autoDestino.TabIndex = 3;
            this.autoDestino.TextoGuia = "Escriba el inicio del nombre";
            //
            // rdoCosto
            //
            this.rdoCosto.AutoSize = true;
            this.rdoCosto.Checked = true;
            this.rdoCosto.Location = new System.Drawing.Point(12, 124);
            this.rdoCosto.Name = "rdoCosto";
            this.rdoCosto.Size = new System.Drawing.Size(212, 19);
            this.rdoCosto.TabIndex = 4;
            this.rdoCosto.TabStop = true;
            this.rdoCosto.Text = "Ruta más económica (menor costo)";
            this.rdoCosto.UseVisualStyleBackColor = true;
            this.rdoCosto.CheckedChanged += new System.EventHandler(this.rdoCosto_CheckedChanged);
            //
            // rdoTiempo
            //
            this.rdoTiempo.AutoSize = true;
            this.rdoTiempo.Location = new System.Drawing.Point(12, 148);
            this.rdoTiempo.Name = "rdoTiempo";
            this.rdoTiempo.Size = new System.Drawing.Size(203, 19);
            this.rdoTiempo.TabIndex = 5;
            this.rdoTiempo.Text = "Ruta más rápida (menor tiempo)";
            this.rdoTiempo.UseVisualStyleBackColor = true;
            //
            // btnCalcular
            //
            this.btnCalcular.Location = new System.Drawing.Point(12, 180);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(186, 32);
            this.btnCalcular.TabIndex = 6;
            this.btnCalcular.Text = "Calcular ruta";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Location = new System.Drawing.Point(206, 180);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 32);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // lblAyuda
            //
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(12, 220);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(292, 34);
            this.lblAyuda.TabIndex = 8;
            this.lblAyuda.Text = "También puede hacer clic en el mapa: primero en el origen y luego en el destino.";
            //
            // grpResultado
            //
            this.grpResultado.Controls.Add(this.txtResultado);
            this.grpResultado.Location = new System.Drawing.Point(8, 280);
            this.grpResultado.Name = "grpResultado";
            this.grpResultado.Size = new System.Drawing.Size(316, 300);
            this.grpResultado.TabIndex = 1;
            this.grpResultado.TabStop = false;
            this.grpResultado.Text = "Resultado (Union-Find + Dijkstra)";
            //
            // txtResultado
            //
            this.txtResultado.BackColor = System.Drawing.SystemColors.Window;
            this.txtResultado.Location = new System.Drawing.Point(12, 24);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(292, 264);
            this.txtResultado.TabIndex = 0;
            this.txtResultado.TabStop = false;
            //
            // divisor
            //
            this.divisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisor.Location = new System.Drawing.Point(345, 0);
            this.divisor.Name = "divisor";
            this.divisor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // divisor.Panel1
            //
            this.divisor.Panel1.Controls.Add(this.panelMapa);
            //
            // divisor.Panel2
            //
            this.divisor.Panel2.Controls.Add(this.dgvDijkstra);
            this.divisor.Panel2.Controls.Add(this.lblTabla);
            this.divisor.Size = new System.Drawing.Size(931, 703);
            this.divisor.SplitterDistance = 440;
            this.divisor.TabIndex = 1;
            //
            // panelMapa
            //
            this.panelMapa.BackColor = System.Drawing.Color.White;
            this.panelMapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapa.Location = new System.Drawing.Point(0, 0);
            this.panelMapa.Name = "panelMapa";
            this.panelMapa.Size = new System.Drawing.Size(931, 440);
            this.panelMapa.TabIndex = 0;
            //
            // lblTabla
            //
            this.lblTabla.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTabla.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTabla.Location = new System.Drawing.Point(0, 0);
            this.lblTabla.Name = "lblTabla";
            this.lblTabla.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblTabla.Size = new System.Drawing.Size(931, 28);
            this.lblTabla.TabIndex = 0;
            this.lblTabla.Text = "Tabla de Dijkstra: menor distancia desde el origen hasta cada ciudad";
            this.lblTabla.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvDijkstra
            //
            this.dgvDijkstra.AllowUserToAddRows = false;
            this.dgvDijkstra.AllowUserToDeleteRows = false;
            this.dgvDijkstra.AllowUserToResizeRows = false;
            this.dgvDijkstra.BackgroundColor = System.Drawing.Color.White;
            this.dgvDijkstra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDijkstra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDijkstra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCiudad,
            this.colDistancia,
            this.colAnterior,
            this.colEnCamino});
            this.dgvDijkstra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDijkstra.Location = new System.Drawing.Point(0, 28);
            this.dgvDijkstra.MultiSelect = false;
            this.dgvDijkstra.Name = "dgvDijkstra";
            this.dgvDijkstra.ReadOnly = true;
            this.dgvDijkstra.RowHeadersVisible = false;
            this.dgvDijkstra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDijkstra.Size = new System.Drawing.Size(931, 231);
            this.dgvDijkstra.TabIndex = 1;
            //
            // colCiudad
            //
            this.colCiudad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCiudad.HeaderText = "Ciudad";
            this.colCiudad.Name = "colCiudad";
            this.colCiudad.ReadOnly = true;
            //
            // colDistancia
            //
            this.colDistancia.HeaderText = "Distancia mínima";
            this.colDistancia.Name = "colDistancia";
            this.colDistancia.ReadOnly = true;
            this.colDistancia.Width = 140;
            //
            // colAnterior
            //
            this.colAnterior.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAnterior.HeaderText = "Ciudad anterior";
            this.colAnterior.Name = "colAnterior";
            this.colAnterior.ReadOnly = true;
            //
            // colEnCamino
            //
            this.colEnCamino.HeaderText = "¿En el camino?";
            this.colEnCamino.Name = "colEnCamino";
            this.colEnCamino.ReadOnly = true;
            this.colEnCamino.Width = 120;
            //
            // PestanaRutas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.divisor);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaRutas";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpResultado.ResumeLayout(false);
            this.grpResultado.PerformLayout();
            this.grpConsulta.ResumeLayout(false);
            this.grpConsulta.PerformLayout();
            this.divisor.Panel1.ResumeLayout(false);
            this.divisor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).EndInit();
            this.divisor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDijkstra)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpConsulta;
        private System.Windows.Forms.Label lblOrigen;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoOrigen;
        private System.Windows.Forms.Label lblDestino;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoDestino;
        private System.Windows.Forms.RadioButton rdoCosto;
        private System.Windows.Forms.RadioButton rdoTiempo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.SplitContainer divisor;
        private LogisticaEnvios.GUI.PanelMapa panelMapa;
        private System.Windows.Forms.Label lblTabla;
        private System.Windows.Forms.DataGridView dgvDijkstra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCiudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistancia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAnterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnCamino;
    }
}
