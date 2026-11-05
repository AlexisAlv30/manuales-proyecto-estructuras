namespace LogisticaEnvios.GUI
{
    partial class PestanaRegiones
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
            this.lblExplicacion = new System.Windows.Forms.Label();
            this.grpVerificar = new System.Windows.Forms.GroupBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.autoCiudad2 = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblCiudad2 = new System.Windows.Forms.Label();
            this.autoCiudad1 = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblCiudad1 = new System.Windows.Forms.Label();
            this.divisor = new System.Windows.Forms.SplitContainer();
            this.dgvRegiones = new System.Windows.Forms.DataGridView();
            this.colColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepresentante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIntegrantes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRegiones = new System.Windows.Forms.Label();
            this.dgvArreglos = new System.Windows.Forms.DataGridView();
            this.colIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCiudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPadre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRango = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegionCiudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblArreglos = new System.Windows.Forms.Label();
            this.panelIzquierdo.SuspendLayout();
            this.grpVerificar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).BeginInit();
            this.divisor.Panel1.SuspendLayout();
            this.divisor.Panel2.SuspendLayout();
            this.divisor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArreglos)).BeginInit();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.lblExplicacion);
            this.panelIzquierdo.Controls.Add(this.grpVerificar);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpVerificar
            //
            this.grpVerificar.Controls.Add(this.txtResultado);
            this.grpVerificar.Controls.Add(this.btnVerificar);
            this.grpVerificar.Controls.Add(this.autoCiudad2);
            this.grpVerificar.Controls.Add(this.lblCiudad2);
            this.grpVerificar.Controls.Add(this.autoCiudad1);
            this.grpVerificar.Controls.Add(this.lblCiudad1);
            this.grpVerificar.Location = new System.Drawing.Point(8, 8);
            this.grpVerificar.Name = "grpVerificar";
            this.grpVerificar.Size = new System.Drawing.Size(316, 312);
            this.grpVerificar.TabIndex = 0;
            this.grpVerificar.TabStop = false;
            this.grpVerificar.Text = "¿Están en la misma región de distribución?";
            //
            // lblCiudad1
            //
            this.lblCiudad1.AutoSize = true;
            this.lblCiudad1.Location = new System.Drawing.Point(12, 24);
            this.lblCiudad1.Name = "lblCiudad1";
            this.lblCiudad1.Size = new System.Drawing.Size(85, 15);
            this.lblCiudad1.TabIndex = 0;
            this.lblCiudad1.Text = "Primera ciudad";
            //
            // autoCiudad1
            //
            this.autoCiudad1.Location = new System.Drawing.Point(12, 42);
            this.autoCiudad1.Name = "autoCiudad1";
            this.autoCiudad1.Size = new System.Drawing.Size(292, 23);
            this.autoCiudad1.TabIndex = 1;
            this.autoCiudad1.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblCiudad2
            //
            this.lblCiudad2.AutoSize = true;
            this.lblCiudad2.Location = new System.Drawing.Point(12, 72);
            this.lblCiudad2.Name = "lblCiudad2";
            this.lblCiudad2.Size = new System.Drawing.Size(89, 15);
            this.lblCiudad2.TabIndex = 2;
            this.lblCiudad2.Text = "Segunda ciudad";
            //
            // autoCiudad2
            //
            this.autoCiudad2.Location = new System.Drawing.Point(12, 90);
            this.autoCiudad2.Name = "autoCiudad2";
            this.autoCiudad2.Size = new System.Drawing.Size(292, 23);
            this.autoCiudad2.TabIndex = 3;
            this.autoCiudad2.TextoGuia = "Escriba el inicio del nombre";
            //
            // btnVerificar
            //
            this.btnVerificar.Location = new System.Drawing.Point(12, 126);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(292, 32);
            this.btnVerificar.TabIndex = 4;
            this.btnVerificar.Text = "Verificar con Find";
            this.btnVerificar.UseVisualStyleBackColor = true;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            //
            // txtResultado
            //
            this.txtResultado.BackColor = System.Drawing.SystemColors.Window;
            this.txtResultado.Location = new System.Drawing.Point(12, 168);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.Size = new System.Drawing.Size(292, 132);
            this.txtResultado.TabIndex = 5;
            this.txtResultado.TabStop = false;
            //
            // lblExplicacion
            //
            this.lblExplicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblExplicacion.Location = new System.Drawing.Point(12, 330);
            this.lblExplicacion.Name = "lblExplicacion";
            this.lblExplicacion.Size = new System.Drawing.Size(308, 150);
            this.lblExplicacion.TabIndex = 1;
            this.lblExplicacion.Text = "Una región de distribución es un grupo de ciudades unidas por alguna cadena de rutas.\r\n\r\nUnion une dos regiones (unión por rango) cada vez que se registra una ruta.\r\n\r\nFind busca el representante de una ciudad y comprime el camino: después de cada consulta la ciudad queda apuntando directo a su representante.";
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
            this.divisor.Panel1.Controls.Add(this.dgvRegiones);
            this.divisor.Panel1.Controls.Add(this.lblRegiones);
            //
            // divisor.Panel2
            //
            this.divisor.Panel2.Controls.Add(this.dgvArreglos);
            this.divisor.Panel2.Controls.Add(this.lblArreglos);
            this.divisor.Size = new System.Drawing.Size(931, 703);
            this.divisor.SplitterDistance = 250;
            this.divisor.TabIndex = 1;
            //
            // lblRegiones
            //
            this.lblRegiones.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRegiones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegiones.Location = new System.Drawing.Point(0, 0);
            this.lblRegiones.Name = "lblRegiones";
            this.lblRegiones.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblRegiones.Size = new System.Drawing.Size(931, 28);
            this.lblRegiones.TabIndex = 0;
            this.lblRegiones.Text = "Regiones de distribución";
            this.lblRegiones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvRegiones
            //
            this.dgvRegiones.AllowUserToAddRows = false;
            this.dgvRegiones.AllowUserToDeleteRows = false;
            this.dgvRegiones.AllowUserToResizeRows = false;
            this.dgvRegiones.BackgroundColor = System.Drawing.Color.White;
            this.dgvRegiones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRegiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegiones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColor,
            this.colRegion,
            this.colRepresentante,
            this.colCantidad,
            this.colIntegrantes});
            this.dgvRegiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRegiones.Location = new System.Drawing.Point(0, 28);
            this.dgvRegiones.Name = "dgvRegiones";
            this.dgvRegiones.ReadOnly = true;
            this.dgvRegiones.RowHeadersVisible = false;
            this.dgvRegiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegiones.Size = new System.Drawing.Size(931, 222);
            this.dgvRegiones.TabIndex = 1;
            //
            // colColor
            //
            this.colColor.HeaderText = "";
            this.colColor.Name = "colColor";
            this.colColor.ReadOnly = true;
            this.colColor.Width = 30;
            //
            // colRegion
            //
            this.colRegion.HeaderText = "Región";
            this.colRegion.Name = "colRegion";
            this.colRegion.ReadOnly = true;
            this.colRegion.Width = 80;
            //
            // colRepresentante
            //
            this.colRepresentante.HeaderText = "Representante (raíz)";
            this.colRepresentante.Name = "colRepresentante";
            this.colRepresentante.ReadOnly = true;
            this.colRepresentante.Width = 160;
            //
            // colCantidad
            //
            this.colCantidad.HeaderText = "Ciudades";
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.ReadOnly = true;
            this.colCantidad.Width = 80;
            //
            // colIntegrantes
            //
            this.colIntegrantes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIntegrantes.HeaderText = "Integrantes";
            this.colIntegrantes.Name = "colIntegrantes";
            this.colIntegrantes.ReadOnly = true;
            //
            // lblArreglos
            //
            this.lblArreglos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblArreglos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblArreglos.Location = new System.Drawing.Point(0, 0);
            this.lblArreglos.Name = "lblArreglos";
            this.lblArreglos.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblArreglos.Size = new System.Drawing.Size(931, 28);
            this.lblArreglos.TabIndex = 0;
            this.lblArreglos.Text = "Arreglos internos del Union-Find: padre y rango";
            this.lblArreglos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dgvArreglos
            //
            this.dgvArreglos.AllowUserToAddRows = false;
            this.dgvArreglos.AllowUserToDeleteRows = false;
            this.dgvArreglos.AllowUserToResizeRows = false;
            this.dgvArreglos.BackgroundColor = System.Drawing.Color.White;
            this.dgvArreglos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvArreglos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArreglos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndice,
            this.colCiudad,
            this.colPadre,
            this.colRango,
            this.colRegionCiudad});
            this.dgvArreglos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArreglos.Location = new System.Drawing.Point(0, 28);
            this.dgvArreglos.Name = "dgvArreglos";
            this.dgvArreglos.ReadOnly = true;
            this.dgvArreglos.RowHeadersVisible = false;
            this.dgvArreglos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArreglos.Size = new System.Drawing.Size(931, 421);
            this.dgvArreglos.TabIndex = 1;
            //
            // colIndice
            //
            this.colIndice.HeaderText = "Índice";
            this.colIndice.Name = "colIndice";
            this.colIndice.ReadOnly = true;
            this.colIndice.Width = 70;
            //
            // colCiudad
            //
            this.colCiudad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCiudad.HeaderText = "Ciudad";
            this.colCiudad.Name = "colCiudad";
            this.colCiudad.ReadOnly = true;
            //
            // colPadre
            //
            this.colPadre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPadre.HeaderText = "padre[i]";
            this.colPadre.Name = "colPadre";
            this.colPadre.ReadOnly = true;
            //
            // colRango
            //
            this.colRango.HeaderText = "rango[i]";
            this.colRango.Name = "colRango";
            this.colRango.ReadOnly = true;
            this.colRango.Width = 80;
            //
            // colRegionCiudad
            //
            this.colRegionCiudad.HeaderText = "Región";
            this.colRegionCiudad.Name = "colRegionCiudad";
            this.colRegionCiudad.ReadOnly = true;
            this.colRegionCiudad.Width = 80;
            //
            // PestanaRegiones
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.divisor);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaRegiones";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpVerificar.ResumeLayout(false);
            this.grpVerificar.PerformLayout();
            this.divisor.Panel1.ResumeLayout(false);
            this.divisor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).EndInit();
            this.divisor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArreglos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpVerificar;
        private System.Windows.Forms.Label lblCiudad1;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoCiudad1;
        private System.Windows.Forms.Label lblCiudad2;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoCiudad2;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.SplitContainer divisor;
        private System.Windows.Forms.Label lblRegiones;
        private System.Windows.Forms.DataGridView dgvRegiones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepresentante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntegrantes;
        private System.Windows.Forms.Label lblArreglos;
        private System.Windows.Forms.DataGridView dgvArreglos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCiudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPadre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRango;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegionCiudad;
    }
}
