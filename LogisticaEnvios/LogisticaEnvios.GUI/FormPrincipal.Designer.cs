namespace LogisticaEnvios.GUI
{
    partial class FormPrincipal
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCargarEjemplo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCargarCarpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.separador1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCargarCiudades = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCargarRutas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCargarEnvios = new System.Windows.Forms.ToolStripMenuItem();
            this.separador2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.separador3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuVaciar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.barraEstado = new System.Windows.Forms.StatusStrip();
            this.lblResumen = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.pestanas = new System.Windows.Forms.TabControl();
            this.tabRastreo = new System.Windows.Forms.TabPage();
            this.pestanaRastreo = new LogisticaEnvios.GUI.PestanaRastreo();
            this.tabEnvios = new System.Windows.Forms.TabPage();
            this.pestanaEnvios = new LogisticaEnvios.GUI.PestanaEnvios();
            this.tabGrafo = new System.Windows.Forms.TabPage();
            this.pestanaGrafo = new LogisticaEnvios.GUI.PestanaGrafo();
            this.tabRutas = new System.Windows.Forms.TabPage();
            this.pestanaRutas = new LogisticaEnvios.GUI.PestanaRutas();
            this.tabRegiones = new System.Windows.Forms.TabPage();
            this.pestanaRegiones = new LogisticaEnvios.GUI.PestanaRegiones();
            this.tabTrie = new System.Windows.Forms.TabPage();
            this.pestanaTrie = new LogisticaEnvios.GUI.PestanaTrie();
            this.tabReportes = new System.Windows.Forms.TabPage();
            this.pestanaReportes = new LogisticaEnvios.GUI.PestanaReportes();
            this.menuPrincipal.SuspendLayout();
            this.barraEstado.SuspendLayout();
            this.pestanas.SuspendLayout();
            this.tabRastreo.SuspendLayout();
            this.tabEnvios.SuspendLayout();
            this.tabGrafo.SuspendLayout();
            this.tabRutas.SuspendLayout();
            this.tabRegiones.SuspendLayout();
            this.tabTrie.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.SuspendLayout();
            //
            // menuPrincipal
            //
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuAyuda});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1284, 24);
            this.menuPrincipal.TabIndex = 0;
            //
            // menuArchivo
            //
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCargarEjemplo,
            this.menuCargarCarpeta,
            this.separador1,
            this.menuCargarCiudades,
            this.menuCargarRutas,
            this.menuCargarEnvios,
            this.separador2,
            this.menuGuardar,
            this.menuGuardarComo,
            this.separador3,
            this.menuVaciar,
            this.menuSalir});
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Size = new System.Drawing.Size(60, 20);
            this.menuArchivo.Text = "&Archivo";
            //
            // menuCargarEjemplo
            //
            this.menuCargarEjemplo.Name = "menuCargarEjemplo";
            this.menuCargarEjemplo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuCargarEjemplo.Size = new System.Drawing.Size(290, 22);
            this.menuCargarEjemplo.Text = "Cargar datos predeterminados";
            this.menuCargarEjemplo.Click += new System.EventHandler(this.menuCargarEjemplo_Click);
            //
            // menuCargarCarpeta
            //
            this.menuCargarCarpeta.Name = "menuCargarCarpeta";
            this.menuCargarCarpeta.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuCargarCarpeta.Size = new System.Drawing.Size(290, 22);
            this.menuCargarCarpeta.Text = "Cargar carpeta de datos...";
            this.menuCargarCarpeta.Click += new System.EventHandler(this.menuCargarCarpeta_Click);
            //
            // separador1
            //
            this.separador1.Name = "separador1";
            this.separador1.Size = new System.Drawing.Size(287, 6);
            //
            // menuCargarCiudades
            //
            this.menuCargarCiudades.Name = "menuCargarCiudades";
            this.menuCargarCiudades.Size = new System.Drawing.Size(290, 22);
            this.menuCargarCiudades.Text = "Cargar archivo de ciudades...";
            this.menuCargarCiudades.Click += new System.EventHandler(this.menuCargarCiudades_Click);
            //
            // menuCargarRutas
            //
            this.menuCargarRutas.Name = "menuCargarRutas";
            this.menuCargarRutas.Size = new System.Drawing.Size(290, 22);
            this.menuCargarRutas.Text = "Cargar archivo de rutas...";
            this.menuCargarRutas.Click += new System.EventHandler(this.menuCargarRutas_Click);
            //
            // menuCargarEnvios
            //
            this.menuCargarEnvios.Name = "menuCargarEnvios";
            this.menuCargarEnvios.Size = new System.Drawing.Size(290, 22);
            this.menuCargarEnvios.Text = "Cargar archivo de envíos...";
            this.menuCargarEnvios.Click += new System.EventHandler(this.menuCargarEnvios_Click);
            //
            // separador2
            //
            this.separador2.Name = "separador2";
            this.separador2.Size = new System.Drawing.Size(287, 6);
            //
            // menuGuardar
            //
            this.menuGuardar.Name = "menuGuardar";
            this.menuGuardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuGuardar.Size = new System.Drawing.Size(290, 22);
            this.menuGuardar.Text = "Guardar datos";
            this.menuGuardar.Click += new System.EventHandler(this.menuGuardar_Click);
            //
            // menuGuardarComo
            //
            this.menuGuardarComo.Name = "menuGuardarComo";
            this.menuGuardarComo.Size = new System.Drawing.Size(290, 22);
            this.menuGuardarComo.Text = "Guardar en otra carpeta...";
            this.menuGuardarComo.Click += new System.EventHandler(this.menuGuardarComo_Click);
            //
            // separador3
            //
            this.separador3.Name = "separador3";
            this.separador3.Size = new System.Drawing.Size(287, 6);
            //
            // menuVaciar
            //
            this.menuVaciar.Name = "menuVaciar";
            this.menuVaciar.Size = new System.Drawing.Size(290, 22);
            this.menuVaciar.Text = "Vaciar todos los datos";
            this.menuVaciar.Click += new System.EventHandler(this.menuVaciar_Click);
            //
            // menuSalir
            //
            this.menuSalir.Name = "menuSalir";
            this.menuSalir.Size = new System.Drawing.Size(290, 22);
            this.menuSalir.Text = "Salir";
            this.menuSalir.Click += new System.EventHandler(this.menuSalir_Click);
            //
            // menuAyuda
            //
            this.menuAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAcercaDe});
            this.menuAyuda.Name = "menuAyuda";
            this.menuAyuda.Size = new System.Drawing.Size(53, 20);
            this.menuAyuda.Text = "A&yuda";
            //
            // menuAcercaDe
            //
            this.menuAcercaDe.Name = "menuAcercaDe";
            this.menuAcercaDe.Size = new System.Drawing.Size(180, 22);
            this.menuAcercaDe.Text = "Acerca del proyecto";
            this.menuAcercaDe.Click += new System.EventHandler(this.menuAcercaDe_Click);
            //
            // barraEstado
            //
            this.barraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblResumen,
            this.lblMensaje});
            this.barraEstado.Location = new System.Drawing.Point(0, 759);
            this.barraEstado.Name = "barraEstado";
            this.barraEstado.Size = new System.Drawing.Size(1284, 22);
            this.barraEstado.TabIndex = 2;
            //
            // lblResumen
            //
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(250, 17);
            this.lblResumen.Text = "Ciudades: 0   |   Envíos: 0   |   Regiones: 0";
            //
            // lblMensaje
            //
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(1019, 17);
            this.lblMensaje.Spring = true;
            this.lblMensaje.Text = "";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pestanas
            //
            this.pestanas.Controls.Add(this.tabRastreo);
            this.pestanas.Controls.Add(this.tabEnvios);
            this.pestanas.Controls.Add(this.tabGrafo);
            this.pestanas.Controls.Add(this.tabRutas);
            this.pestanas.Controls.Add(this.tabRegiones);
            this.pestanas.Controls.Add(this.tabTrie);
            this.pestanas.Controls.Add(this.tabReportes);
            this.pestanas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanas.Location = new System.Drawing.Point(0, 24);
            this.pestanas.Name = "pestanas";
            this.pestanas.Padding = new System.Drawing.Point(12, 5);
            this.pestanas.SelectedIndex = 0;
            this.pestanas.Size = new System.Drawing.Size(1284, 735);
            this.pestanas.TabIndex = 1;
            //
            // tabRastreo
            //
            this.tabRastreo.Controls.Add(this.pestanaRastreo);
            this.tabRastreo.Location = new System.Drawing.Point(4, 28);
            this.tabRastreo.Name = "tabRastreo";
            this.tabRastreo.Size = new System.Drawing.Size(1276, 703);
            this.tabRastreo.TabIndex = 0;
            this.tabRastreo.Text = "Rastrear envío";
            this.tabRastreo.UseVisualStyleBackColor = true;
            //
            // pestanaRastreo
            //
            this.pestanaRastreo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaRastreo.Location = new System.Drawing.Point(0, 0);
            this.pestanaRastreo.Name = "pestanaRastreo";
            this.pestanaRastreo.Size = new System.Drawing.Size(1276, 703);
            this.pestanaRastreo.TabIndex = 0;
            //
            // tabEnvios
            //
            this.tabEnvios.Controls.Add(this.pestanaEnvios);
            this.tabEnvios.Location = new System.Drawing.Point(4, 28);
            this.tabEnvios.Name = "tabEnvios";
            this.tabEnvios.Size = new System.Drawing.Size(1276, 703);
            this.tabEnvios.TabIndex = 1;
            this.tabEnvios.Text = "Envíos · Tabla Hash";
            this.tabEnvios.UseVisualStyleBackColor = true;
            //
            // pestanaEnvios
            //
            this.pestanaEnvios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaEnvios.Location = new System.Drawing.Point(0, 0);
            this.pestanaEnvios.Name = "pestanaEnvios";
            this.pestanaEnvios.Size = new System.Drawing.Size(1276, 703);
            this.pestanaEnvios.TabIndex = 0;
            //
            // tabGrafo
            //
            this.tabGrafo.Controls.Add(this.pestanaGrafo);
            this.tabGrafo.Location = new System.Drawing.Point(4, 28);
            this.tabGrafo.Name = "tabGrafo";
            this.tabGrafo.Size = new System.Drawing.Size(1276, 703);
            this.tabGrafo.TabIndex = 2;
            this.tabGrafo.Text = "Ciudades y rutas · Grafo";
            this.tabGrafo.UseVisualStyleBackColor = true;
            //
            // pestanaGrafo
            //
            this.pestanaGrafo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaGrafo.Location = new System.Drawing.Point(0, 0);
            this.pestanaGrafo.Name = "pestanaGrafo";
            this.pestanaGrafo.Size = new System.Drawing.Size(1276, 703);
            this.pestanaGrafo.TabIndex = 0;
            //
            // tabRutas
            //
            this.tabRutas.Controls.Add(this.pestanaRutas);
            this.tabRutas.Location = new System.Drawing.Point(4, 28);
            this.tabRutas.Name = "tabRutas";
            this.tabRutas.Size = new System.Drawing.Size(1276, 703);
            this.tabRutas.TabIndex = 3;
            this.tabRutas.Text = "Ruta óptima · Dijkstra";
            this.tabRutas.UseVisualStyleBackColor = true;
            //
            // pestanaRutas
            //
            this.pestanaRutas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaRutas.Location = new System.Drawing.Point(0, 0);
            this.pestanaRutas.Name = "pestanaRutas";
            this.pestanaRutas.Size = new System.Drawing.Size(1276, 703);
            this.pestanaRutas.TabIndex = 0;
            //
            // tabRegiones
            //
            this.tabRegiones.Controls.Add(this.pestanaRegiones);
            this.tabRegiones.Location = new System.Drawing.Point(4, 28);
            this.tabRegiones.Name = "tabRegiones";
            this.tabRegiones.Size = new System.Drawing.Size(1276, 703);
            this.tabRegiones.TabIndex = 4;
            this.tabRegiones.Text = "Regiones · Union-Find";
            this.tabRegiones.UseVisualStyleBackColor = true;
            //
            // pestanaRegiones
            //
            this.pestanaRegiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaRegiones.Location = new System.Drawing.Point(0, 0);
            this.pestanaRegiones.Name = "pestanaRegiones";
            this.pestanaRegiones.Size = new System.Drawing.Size(1276, 703);
            this.pestanaRegiones.TabIndex = 0;
            //
            // tabTrie
            //
            this.tabTrie.Controls.Add(this.pestanaTrie);
            this.tabTrie.Location = new System.Drawing.Point(4, 28);
            this.tabTrie.Name = "tabTrie";
            this.tabTrie.Size = new System.Drawing.Size(1276, 703);
            this.tabTrie.TabIndex = 5;
            this.tabTrie.Text = "Autocompletado · Trie";
            this.tabTrie.UseVisualStyleBackColor = true;
            //
            // pestanaTrie
            //
            this.pestanaTrie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaTrie.Location = new System.Drawing.Point(0, 0);
            this.pestanaTrie.Name = "pestanaTrie";
            this.pestanaTrie.Size = new System.Drawing.Size(1276, 703);
            this.pestanaTrie.TabIndex = 0;
            //
            // tabReportes
            //
            this.tabReportes.Controls.Add(this.pestanaReportes);
            this.tabReportes.Location = new System.Drawing.Point(4, 28);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.Size = new System.Drawing.Size(1276, 703);
            this.tabReportes.TabIndex = 6;
            this.tabReportes.Text = "Reportes";
            this.tabReportes.UseVisualStyleBackColor = true;
            //
            // pestanaReportes
            //
            this.pestanaReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanaReportes.Location = new System.Drawing.Point(0, 0);
            this.pestanaReportes.Name = "pestanaReportes";
            this.pestanaReportes.Size = new System.Drawing.Size(1276, 703);
            this.pestanaReportes.TabIndex = 0;
            //
            // FormPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 781);
            this.Controls.Add(this.pestanas);
            this.Controls.Add(this.barraEstado);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logística de envíos · Estructura de Datos II";
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.barraEstado.ResumeLayout(false);
            this.barraEstado.PerformLayout();
            this.pestanas.ResumeLayout(false);
            this.tabRastreo.ResumeLayout(false);
            this.tabEnvios.ResumeLayout(false);
            this.tabGrafo.ResumeLayout(false);
            this.tabRutas.ResumeLayout(false);
            this.tabRegiones.ResumeLayout(false);
            this.tabTrie.ResumeLayout(false);
            this.tabReportes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem menuCargarEjemplo;
        private System.Windows.Forms.ToolStripMenuItem menuCargarCarpeta;
        private System.Windows.Forms.ToolStripSeparator separador1;
        private System.Windows.Forms.ToolStripMenuItem menuCargarCiudades;
        private System.Windows.Forms.ToolStripMenuItem menuCargarRutas;
        private System.Windows.Forms.ToolStripMenuItem menuCargarEnvios;
        private System.Windows.Forms.ToolStripSeparator separador2;
        private System.Windows.Forms.ToolStripMenuItem menuGuardar;
        private System.Windows.Forms.ToolStripMenuItem menuGuardarComo;
        private System.Windows.Forms.ToolStripSeparator separador3;
        private System.Windows.Forms.ToolStripMenuItem menuVaciar;
        private System.Windows.Forms.ToolStripMenuItem menuSalir;
        private System.Windows.Forms.ToolStripMenuItem menuAyuda;
        private System.Windows.Forms.ToolStripMenuItem menuAcercaDe;
        private System.Windows.Forms.StatusStrip barraEstado;
        private System.Windows.Forms.ToolStripStatusLabel lblResumen;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
        private System.Windows.Forms.TabControl pestanas;
        private System.Windows.Forms.TabPage tabRastreo;
        private System.Windows.Forms.TabPage tabEnvios;
        private System.Windows.Forms.TabPage tabGrafo;
        private System.Windows.Forms.TabPage tabRutas;
        private System.Windows.Forms.TabPage tabRegiones;
        private System.Windows.Forms.TabPage tabTrie;
        private LogisticaEnvios.GUI.PestanaRastreo pestanaRastreo;
        private LogisticaEnvios.GUI.PestanaEnvios pestanaEnvios;
        private LogisticaEnvios.GUI.PestanaGrafo pestanaGrafo;
        private LogisticaEnvios.GUI.PestanaRutas pestanaRutas;
        private LogisticaEnvios.GUI.PestanaRegiones pestanaRegiones;
        private LogisticaEnvios.GUI.PestanaTrie pestanaTrie;
        private System.Windows.Forms.TabPage tabReportes;
        private LogisticaEnvios.GUI.PestanaReportes pestanaReportes;
    }
}
