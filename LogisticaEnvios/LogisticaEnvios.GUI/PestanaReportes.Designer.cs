namespace LogisticaEnvios.GUI
{
    partial class PestanaReportes
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
            this.grpReportes = new System.Windows.Forms.GroupBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnExportarTxt = new System.Windows.Forms.Button();
            this.btnExportarCsv = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lstReportes = new System.Windows.Forms.ListBox();
            this.lblElegir = new System.Windows.Forms.Label();
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.divisor = new System.Windows.Forms.SplitContainer();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.txtResumen = new System.Windows.Forms.TextBox();
            this.lblResumen = new System.Windows.Forms.Label();
            this.panelIzquierdo.SuspendLayout();
            this.grpReportes.SuspendLayout();
            this.panelTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).BeginInit();
            this.divisor.Panel1.SuspendLayout();
            this.divisor.Panel2.SuspendLayout();
            this.divisor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpReportes);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpReportes
            //
            this.grpReportes.Controls.Add(this.lblAyuda);
            this.grpReportes.Controls.Add(this.btnExportarTxt);
            this.grpReportes.Controls.Add(this.btnExportarCsv);
            this.grpReportes.Controls.Add(this.btnActualizar);
            this.grpReportes.Controls.Add(this.lstReportes);
            this.grpReportes.Controls.Add(this.lblElegir);
            this.grpReportes.Location = new System.Drawing.Point(8, 8);
            this.grpReportes.Name = "grpReportes";
            this.grpReportes.Size = new System.Drawing.Size(316, 360);
            this.grpReportes.TabIndex = 0;
            this.grpReportes.TabStop = false;
            this.grpReportes.Text = "Reportes para toma de decisiones";
            //
            // lblElegir
            //
            this.lblElegir.AutoSize = true;
            this.lblElegir.Location = new System.Drawing.Point(12, 24);
            this.lblElegir.Name = "lblElegir";
            this.lblElegir.Size = new System.Drawing.Size(93, 15);
            this.lblElegir.TabIndex = 0;
            this.lblElegir.Text = "Elija un reporte:";
            //
            // lstReportes
            //
            this.lstReportes.FormattingEnabled = true;
            this.lstReportes.IntegralHeight = false;
            this.lstReportes.ItemHeight = 15;
            this.lstReportes.Location = new System.Drawing.Point(12, 44);
            this.lstReportes.Name = "lstReportes";
            this.lstReportes.Size = new System.Drawing.Size(292, 96);
            this.lstReportes.TabIndex = 1;
            this.lstReportes.SelectedIndexChanged += new System.EventHandler(this.lstReportes_SelectedIndexChanged);
            //
            // btnActualizar
            //
            this.btnActualizar.Location = new System.Drawing.Point(12, 150);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(292, 30);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "Actualizar reporte";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnExportarCsv
            //
            this.btnExportarCsv.Location = new System.Drawing.Point(12, 188);
            this.btnExportarCsv.Name = "btnExportarCsv";
            this.btnExportarCsv.Size = new System.Drawing.Size(292, 30);
            this.btnExportarCsv.TabIndex = 3;
            this.btnExportarCsv.Text = "Exportar este reporte a .csv...";
            this.btnExportarCsv.UseVisualStyleBackColor = true;
            this.btnExportarCsv.Click += new System.EventHandler(this.btnExportarCsv_Click);
            //
            // btnExportarTxt
            //
            this.btnExportarTxt.Location = new System.Drawing.Point(12, 226);
            this.btnExportarTxt.Name = "btnExportarTxt";
            this.btnExportarTxt.Size = new System.Drawing.Size(292, 30);
            this.btnExportarTxt.TabIndex = 4;
            this.btnExportarTxt.Text = "Exportar todos los reportes a .txt...";
            this.btnExportarTxt.UseVisualStyleBackColor = true;
            this.btnExportarTxt.Click += new System.EventHandler(this.btnExportarTxt_Click);
            //
            // lblAyuda
            //
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(12, 266);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(292, 84);
            this.lblAyuda.TabIndex = 5;
            this.lblAyuda.Text = "Los reportes se calculan con los datos actuales: la Tabla Hash aporta los envíos, el Union-Find las regiones y el Grafo las rutas (Dijkstra). Los archivos se guardan por defecto en la carpeta reportes del proyecto.";
            //
            // panelTitulo
            //
            this.panelTitulo.Controls.Add(this.lblDescripcion);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(345, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(931, 64);
            this.panelTitulo.TabIndex = 1;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(10, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(60, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reporte";
            //
            // lblDescripcion
            //
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDescripcion.Location = new System.Drawing.Point(10, 34);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(911, 28);
            this.lblDescripcion.TabIndex = 1;
            //
            // divisor
            //
            this.divisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisor.Location = new System.Drawing.Point(345, 64);
            this.divisor.Name = "divisor";
            this.divisor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // divisor.Panel1
            //
            this.divisor.Panel1.Controls.Add(this.dgvReporte);
            //
            // divisor.Panel2
            //
            this.divisor.Panel2.Controls.Add(this.txtResumen);
            this.divisor.Panel2.Controls.Add(this.lblResumen);
            this.divisor.Size = new System.Drawing.Size(931, 639);
            this.divisor.SplitterDistance = 480;
            this.divisor.TabIndex = 2;
            //
            // dgvReporte
            //
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AllowUserToDeleteRows = false;
            this.dgvReporte.AllowUserToResizeRows = false;
            this.dgvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvReporte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporte.Location = new System.Drawing.Point(0, 0);
            this.dgvReporte.MultiSelect = false;
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.RowHeadersVisible = false;
            this.dgvReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporte.Size = new System.Drawing.Size(931, 430);
            this.dgvReporte.TabIndex = 0;
            //
            // lblResumen
            //
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumen.Location = new System.Drawing.Point(0, 0);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblResumen.Size = new System.Drawing.Size(931, 28);
            this.lblResumen.TabIndex = 0;
            this.lblResumen.Text = "Resumen y recomendaciones";
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtResumen
            //
            this.txtResumen.BackColor = System.Drawing.SystemColors.Window;
            this.txtResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResumen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtResumen.Location = new System.Drawing.Point(0, 28);
            this.txtResumen.Multiline = true;
            this.txtResumen.Name = "txtResumen";
            this.txtResumen.ReadOnly = true;
            this.txtResumen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResumen.Size = new System.Drawing.Size(931, 177);
            this.txtResumen.TabIndex = 1;
            this.txtResumen.TabStop = false;
            //
            // PestanaReportes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.divisor);
            this.Controls.Add(this.panelTitulo);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaReportes";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpReportes.ResumeLayout(false);
            this.grpReportes.PerformLayout();
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.divisor.Panel1.ResumeLayout(false);
            this.divisor.Panel2.ResumeLayout(false);
            this.divisor.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).EndInit();
            this.divisor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpReportes;
        private System.Windows.Forms.Label lblElegir;
        private System.Windows.Forms.ListBox lstReportes;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnExportarCsv;
        private System.Windows.Forms.Button btnExportarTxt;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.SplitContainer divisor;
        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.TextBox txtResumen;
    }
}
