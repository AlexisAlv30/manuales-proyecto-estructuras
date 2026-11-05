namespace LogisticaEnvios.GUI
{
    partial class PestanaGrafo
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
            this.grpEliminar = new System.Windows.Forms.GroupBox();
            this.btnEliminarRuta = new System.Windows.Forms.Button();
            this.chkAmbosSentidos = new System.Windows.Forms.CheckBox();
            this.autoDestinoEliminar = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblDestinoEliminar = new System.Windows.Forms.Label();
            this.autoOrigenEliminar = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblOrigenEliminar = new System.Windows.Forms.Label();
            this.grpRuta = new System.Windows.Forms.GroupBox();
            this.btnRegistrarRuta = new System.Windows.Forms.Button();
            this.chkDobleSentido = new System.Windows.Forms.CheckBox();
            this.numTiempo = new System.Windows.Forms.NumericUpDown();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.numCosto = new System.Windows.Forms.NumericUpDown();
            this.lblCosto = new System.Windows.Forms.Label();
            this.autoDestinoRuta = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblDestinoRuta = new System.Windows.Forms.Label();
            this.autoOrigenRuta = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblOrigenRuta = new System.Windows.Forms.Label();
            this.grpCiudad = new System.Windows.Forms.GroupBox();
            this.btnRegistrarCiudad = new System.Windows.Forms.Button();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.chkPesos = new System.Windows.Forms.CheckBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAdyacencia = new System.Windows.Forms.Button();
            this.btnDFS = new System.Windows.Forms.Button();
            this.btnBFS = new System.Windows.Forms.Button();
            this.autoInicio = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblInicio = new System.Windows.Forms.Label();
            this.divisor = new System.Windows.Forms.SplitContainer();
            this.panelMapa = new LogisticaEnvios.GUI.PanelMapa();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.panelIzquierdo.SuspendLayout();
            this.grpEliminar.SuspendLayout();
            this.grpRuta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTiempo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).BeginInit();
            this.grpCiudad.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).BeginInit();
            this.divisor.Panel1.SuspendLayout();
            this.divisor.Panel2.SuspendLayout();
            this.divisor.SuspendLayout();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpEliminar);
            this.panelIzquierdo.Controls.Add(this.grpRuta);
            this.panelIzquierdo.Controls.Add(this.grpCiudad);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpCiudad
            //
            this.grpCiudad.Controls.Add(this.btnRegistrarCiudad);
            this.grpCiudad.Controls.Add(this.txtLongitud);
            this.grpCiudad.Controls.Add(this.lblLongitud);
            this.grpCiudad.Controls.Add(this.txtLatitud);
            this.grpCiudad.Controls.Add(this.lblLatitud);
            this.grpCiudad.Controls.Add(this.txtNombre);
            this.grpCiudad.Controls.Add(this.lblNombre);
            this.grpCiudad.Location = new System.Drawing.Point(8, 8);
            this.grpCiudad.Name = "grpCiudad";
            this.grpCiudad.Size = new System.Drawing.Size(316, 172);
            this.grpCiudad.TabIndex = 0;
            this.grpCiudad.TabStop = false;
            this.grpCiudad.Text = "Registrar ciudad";
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(12, 24);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(51, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(12, 42);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(292, 23);
            this.txtNombre.TabIndex = 1;
            //
            // lblLatitud
            //
            this.lblLatitud.AutoSize = true;
            this.lblLatitud.Location = new System.Drawing.Point(12, 72);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(103, 15);
            this.lblLatitud.TabIndex = 2;
            this.lblLatitud.Text = "Latitud (opcional)";
            //
            // txtLatitud
            //
            this.txtLatitud.Location = new System.Drawing.Point(12, 90);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.PlaceholderText = "Ej. 14.63";
            this.txtLatitud.Size = new System.Drawing.Size(140, 23);
            this.txtLatitud.TabIndex = 3;
            //
            // lblLongitud
            //
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Location = new System.Drawing.Point(164, 72);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(114, 15);
            this.lblLongitud.TabIndex = 4;
            this.lblLongitud.Text = "Longitud (opcional)";
            //
            // txtLongitud
            //
            this.txtLongitud.Location = new System.Drawing.Point(164, 90);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.PlaceholderText = "Ej. -90.51";
            this.txtLongitud.Size = new System.Drawing.Size(140, 23);
            this.txtLongitud.TabIndex = 5;
            //
            // btnRegistrarCiudad
            //
            this.btnRegistrarCiudad.Location = new System.Drawing.Point(12, 126);
            this.btnRegistrarCiudad.Name = "btnRegistrarCiudad";
            this.btnRegistrarCiudad.Size = new System.Drawing.Size(292, 32);
            this.btnRegistrarCiudad.TabIndex = 6;
            this.btnRegistrarCiudad.Text = "Registrar ciudad";
            this.btnRegistrarCiudad.UseVisualStyleBackColor = true;
            this.btnRegistrarCiudad.Click += new System.EventHandler(this.btnRegistrarCiudad_Click);
            //
            // grpRuta
            //
            this.grpRuta.Controls.Add(this.btnRegistrarRuta);
            this.grpRuta.Controls.Add(this.chkDobleSentido);
            this.grpRuta.Controls.Add(this.numTiempo);
            this.grpRuta.Controls.Add(this.lblTiempo);
            this.grpRuta.Controls.Add(this.numCosto);
            this.grpRuta.Controls.Add(this.lblCosto);
            this.grpRuta.Controls.Add(this.autoDestinoRuta);
            this.grpRuta.Controls.Add(this.lblDestinoRuta);
            this.grpRuta.Controls.Add(this.autoOrigenRuta);
            this.grpRuta.Controls.Add(this.lblOrigenRuta);
            this.grpRuta.Location = new System.Drawing.Point(8, 188);
            this.grpRuta.Name = "grpRuta";
            this.grpRuta.Size = new System.Drawing.Size(316, 246);
            this.grpRuta.TabIndex = 1;
            this.grpRuta.TabStop = false;
            this.grpRuta.Text = "Registrar ruta (consulta el Union-Find)";
            //
            // lblOrigenRuta
            //
            this.lblOrigenRuta.AutoSize = true;
            this.lblOrigenRuta.Location = new System.Drawing.Point(12, 24);
            this.lblOrigenRuta.Name = "lblOrigenRuta";
            this.lblOrigenRuta.Size = new System.Drawing.Size(98, 15);
            this.lblOrigenRuta.TabIndex = 0;
            this.lblOrigenRuta.Text = "Ciudad de origen";
            //
            // autoOrigenRuta
            //
            this.autoOrigenRuta.Location = new System.Drawing.Point(12, 42);
            this.autoOrigenRuta.Name = "autoOrigenRuta";
            this.autoOrigenRuta.Size = new System.Drawing.Size(292, 23);
            this.autoOrigenRuta.TabIndex = 1;
            this.autoOrigenRuta.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblDestinoRuta
            //
            this.lblDestinoRuta.AutoSize = true;
            this.lblDestinoRuta.Location = new System.Drawing.Point(12, 72);
            this.lblDestinoRuta.Name = "lblDestinoRuta";
            this.lblDestinoRuta.Size = new System.Drawing.Size(101, 15);
            this.lblDestinoRuta.TabIndex = 2;
            this.lblDestinoRuta.Text = "Ciudad de destino";
            //
            // autoDestinoRuta
            //
            this.autoDestinoRuta.Location = new System.Drawing.Point(12, 90);
            this.autoDestinoRuta.Name = "autoDestinoRuta";
            this.autoDestinoRuta.Size = new System.Drawing.Size(292, 23);
            this.autoDestinoRuta.TabIndex = 3;
            this.autoDestinoRuta.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblCosto
            //
            this.lblCosto.AutoSize = true;
            this.lblCosto.Location = new System.Drawing.Point(12, 120);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(60, 15);
            this.lblCosto.TabIndex = 4;
            this.lblCosto.Text = "Costo (Q)";
            //
            // numCosto
            //
            this.numCosto.DecimalPlaces = 2;
            this.numCosto.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numCosto.Location = new System.Drawing.Point(12, 138);
            this.numCosto.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numCosto.Name = "numCosto";
            this.numCosto.Size = new System.Drawing.Size(140, 23);
            this.numCosto.TabIndex = 5;
            //
            // lblTiempo
            //
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Location = new System.Drawing.Point(164, 120);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(88, 15);
            this.lblTiempo.TabIndex = 6;
            this.lblTiempo.Text = "Tiempo (horas)";
            //
            // numTiempo
            //
            this.numTiempo.DecimalPlaces = 2;
            this.numTiempo.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numTiempo.Location = new System.Drawing.Point(164, 138);
            this.numTiempo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTiempo.Name = "numTiempo";
            this.numTiempo.Size = new System.Drawing.Size(140, 23);
            this.numTiempo.TabIndex = 7;
            //
            // chkDobleSentido
            //
            this.chkDobleSentido.AutoSize = true;
            this.chkDobleSentido.Checked = true;
            this.chkDobleSentido.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDobleSentido.Location = new System.Drawing.Point(12, 172);
            this.chkDobleSentido.Name = "chkDobleSentido";
            this.chkDobleSentido.Size = new System.Drawing.Size(215, 19);
            this.chkDobleSentido.TabIndex = 8;
            this.chkDobleSentido.Text = "Ruta de doble sentido (ida y vuelta)";
            this.chkDobleSentido.UseVisualStyleBackColor = true;
            //
            // btnRegistrarRuta
            //
            this.btnRegistrarRuta.Location = new System.Drawing.Point(12, 200);
            this.btnRegistrarRuta.Name = "btnRegistrarRuta";
            this.btnRegistrarRuta.Size = new System.Drawing.Size(292, 32);
            this.btnRegistrarRuta.TabIndex = 9;
            this.btnRegistrarRuta.Text = "Registrar ruta";
            this.btnRegistrarRuta.UseVisualStyleBackColor = true;
            this.btnRegistrarRuta.Click += new System.EventHandler(this.btnRegistrarRuta_Click);
            //
            // grpEliminar
            //
            this.grpEliminar.Controls.Add(this.btnEliminarRuta);
            this.grpEliminar.Controls.Add(this.chkAmbosSentidos);
            this.grpEliminar.Controls.Add(this.autoDestinoEliminar);
            this.grpEliminar.Controls.Add(this.lblDestinoEliminar);
            this.grpEliminar.Controls.Add(this.autoOrigenEliminar);
            this.grpEliminar.Controls.Add(this.lblOrigenEliminar);
            this.grpEliminar.Location = new System.Drawing.Point(8, 442);
            this.grpEliminar.Name = "grpEliminar";
            this.grpEliminar.Size = new System.Drawing.Size(316, 196);
            this.grpEliminar.TabIndex = 2;
            this.grpEliminar.TabStop = false;
            this.grpEliminar.Text = "Eliminar ruta";
            //
            // lblOrigenEliminar
            //
            this.lblOrigenEliminar.AutoSize = true;
            this.lblOrigenEliminar.Location = new System.Drawing.Point(12, 24);
            this.lblOrigenEliminar.Name = "lblOrigenEliminar";
            this.lblOrigenEliminar.Size = new System.Drawing.Size(98, 15);
            this.lblOrigenEliminar.TabIndex = 0;
            this.lblOrigenEliminar.Text = "Ciudad de origen";
            //
            // autoOrigenEliminar
            //
            this.autoOrigenEliminar.Location = new System.Drawing.Point(12, 42);
            this.autoOrigenEliminar.Name = "autoOrigenEliminar";
            this.autoOrigenEliminar.Size = new System.Drawing.Size(292, 23);
            this.autoOrigenEliminar.TabIndex = 1;
            this.autoOrigenEliminar.TextoGuia = "Escriba el inicio del nombre";
            //
            // lblDestinoEliminar
            //
            this.lblDestinoEliminar.AutoSize = true;
            this.lblDestinoEliminar.Location = new System.Drawing.Point(12, 72);
            this.lblDestinoEliminar.Name = "lblDestinoEliminar";
            this.lblDestinoEliminar.Size = new System.Drawing.Size(101, 15);
            this.lblDestinoEliminar.TabIndex = 2;
            this.lblDestinoEliminar.Text = "Ciudad de destino";
            //
            // autoDestinoEliminar
            //
            this.autoDestinoEliminar.Location = new System.Drawing.Point(12, 90);
            this.autoDestinoEliminar.Name = "autoDestinoEliminar";
            this.autoDestinoEliminar.Size = new System.Drawing.Size(292, 23);
            this.autoDestinoEliminar.TabIndex = 3;
            this.autoDestinoEliminar.TextoGuia = "Escriba el inicio del nombre";
            //
            // chkAmbosSentidos
            //
            this.chkAmbosSentidos.AutoSize = true;
            this.chkAmbosSentidos.Checked = true;
            this.chkAmbosSentidos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAmbosSentidos.Location = new System.Drawing.Point(12, 124);
            this.chkAmbosSentidos.Name = "chkAmbosSentidos";
            this.chkAmbosSentidos.Size = new System.Drawing.Size(219, 19);
            this.chkAmbosSentidos.TabIndex = 4;
            this.chkAmbosSentidos.Text = "Eliminar también la ruta de regreso";
            this.chkAmbosSentidos.UseVisualStyleBackColor = true;
            //
            // btnEliminarRuta
            //
            this.btnEliminarRuta.Location = new System.Drawing.Point(12, 152);
            this.btnEliminarRuta.Name = "btnEliminarRuta";
            this.btnEliminarRuta.Size = new System.Drawing.Size(292, 32);
            this.btnEliminarRuta.TabIndex = 5;
            this.btnEliminarRuta.Text = "Eliminar ruta";
            this.btnEliminarRuta.UseVisualStyleBackColor = true;
            this.btnEliminarRuta.Click += new System.EventHandler(this.btnEliminarRuta_Click);
            //
            // panelSuperior
            //
            this.panelSuperior.Controls.Add(this.chkPesos);
            this.panelSuperior.Controls.Add(this.btnLimpiar);
            this.panelSuperior.Controls.Add(this.btnAdyacencia);
            this.panelSuperior.Controls.Add(this.btnDFS);
            this.panelSuperior.Controls.Add(this.btnBFS);
            this.panelSuperior.Controls.Add(this.autoInicio);
            this.panelSuperior.Controls.Add(this.lblInicio);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(345, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(931, 44);
            this.panelSuperior.TabIndex = 1;
            //
            // lblInicio
            //
            this.lblInicio.AutoSize = true;
            this.lblInicio.Location = new System.Drawing.Point(8, 14);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(96, 15);
            this.lblInicio.TabIndex = 0;
            this.lblInicio.Text = "Recorrido desde:";
            //
            // autoInicio
            //
            this.autoInicio.Location = new System.Drawing.Point(110, 11);
            this.autoInicio.Name = "autoInicio";
            this.autoInicio.Size = new System.Drawing.Size(190, 23);
            this.autoInicio.TabIndex = 1;
            this.autoInicio.TextoGuia = "Ciudad de inicio";
            //
            // btnBFS
            //
            this.btnBFS.Location = new System.Drawing.Point(308, 9);
            this.btnBFS.Name = "btnBFS";
            this.btnBFS.Size = new System.Drawing.Size(70, 27);
            this.btnBFS.TabIndex = 2;
            this.btnBFS.Text = "BFS";
            this.btnBFS.UseVisualStyleBackColor = true;
            this.btnBFS.Click += new System.EventHandler(this.btnBFS_Click);
            //
            // btnDFS
            //
            this.btnDFS.Location = new System.Drawing.Point(384, 9);
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(70, 27);
            this.btnDFS.TabIndex = 3;
            this.btnDFS.Text = "DFS";
            this.btnDFS.UseVisualStyleBackColor = true;
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            //
            // btnAdyacencia
            //
            this.btnAdyacencia.Location = new System.Drawing.Point(460, 9);
            this.btnAdyacencia.Name = "btnAdyacencia";
            this.btnAdyacencia.Size = new System.Drawing.Size(140, 27);
            this.btnAdyacencia.TabIndex = 4;
            this.btnAdyacencia.Text = "Lista de adyacencia";
            this.btnAdyacencia.UseVisualStyleBackColor = true;
            this.btnAdyacencia.Click += new System.EventHandler(this.btnAdyacencia_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Location = new System.Drawing.Point(606, 9);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 27);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // chkPesos
            //
            this.chkPesos.AutoSize = true;
            this.chkPesos.Checked = true;
            this.chkPesos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPesos.Location = new System.Drawing.Point(720, 13);
            this.chkPesos.Name = "chkPesos";
            this.chkPesos.Size = new System.Drawing.Size(154, 19);
            this.chkPesos.TabIndex = 6;
            this.chkPesos.Text = "Mostrar costo y tiempo";
            this.chkPesos.UseVisualStyleBackColor = true;
            this.chkPesos.CheckedChanged += new System.EventHandler(this.chkPesos_CheckedChanged);
            //
            // divisor
            //
            this.divisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divisor.Location = new System.Drawing.Point(345, 44);
            this.divisor.Name = "divisor";
            this.divisor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // divisor.Panel1
            //
            this.divisor.Panel1.Controls.Add(this.panelMapa);
            //
            // divisor.Panel2
            //
            this.divisor.Panel2.Controls.Add(this.txtResultado);
            this.divisor.Size = new System.Drawing.Size(931, 659);
            this.divisor.SplitterDistance = 480;
            this.divisor.TabIndex = 2;
            //
            // panelMapa
            //
            this.panelMapa.BackColor = System.Drawing.Color.White;
            this.panelMapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapa.Location = new System.Drawing.Point(0, 0);
            this.panelMapa.Name = "panelMapa";
            this.panelMapa.Size = new System.Drawing.Size(931, 480);
            this.panelMapa.TabIndex = 0;
            //
            // txtResultado
            //
            this.txtResultado.BackColor = System.Drawing.SystemColors.Window;
            this.txtResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultado.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtResultado.Location = new System.Drawing.Point(0, 0);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultado.Size = new System.Drawing.Size(931, 175);
            this.txtResultado.TabIndex = 0;
            this.txtResultado.TabStop = false;
            this.txtResultado.WordWrap = false;
            //
            // PestanaGrafo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.divisor);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaGrafo";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpEliminar.ResumeLayout(false);
            this.grpEliminar.PerformLayout();
            this.grpRuta.ResumeLayout(false);
            this.grpRuta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTiempo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCosto)).EndInit();
            this.grpCiudad.ResumeLayout(false);
            this.grpCiudad.PerformLayout();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.divisor.Panel1.ResumeLayout(false);
            this.divisor.Panel2.ResumeLayout(false);
            this.divisor.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisor)).EndInit();
            this.divisor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpCiudad;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.Button btnRegistrarCiudad;
        private System.Windows.Forms.GroupBox grpRuta;
        private System.Windows.Forms.Label lblOrigenRuta;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoOrigenRuta;
        private System.Windows.Forms.Label lblDestinoRuta;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoDestinoRuta;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.NumericUpDown numCosto;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.NumericUpDown numTiempo;
        private System.Windows.Forms.CheckBox chkDobleSentido;
        private System.Windows.Forms.Button btnRegistrarRuta;
        private System.Windows.Forms.GroupBox grpEliminar;
        private System.Windows.Forms.Label lblOrigenEliminar;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoOrigenEliminar;
        private System.Windows.Forms.Label lblDestinoEliminar;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoDestinoEliminar;
        private System.Windows.Forms.CheckBox chkAmbosSentidos;
        private System.Windows.Forms.Button btnEliminarRuta;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblInicio;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoInicio;
        private System.Windows.Forms.Button btnBFS;
        private System.Windows.Forms.Button btnDFS;
        private System.Windows.Forms.Button btnAdyacencia;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.CheckBox chkPesos;
        private System.Windows.Forms.SplitContainer divisor;
        private LogisticaEnvios.GUI.PanelMapa panelMapa;
        private System.Windows.Forms.TextBox txtResultado;
    }
}
