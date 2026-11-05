namespace LogisticaEnvios.GUI
{
    partial class PestanaTrie
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
            this.grpBusqueda = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lstSugerencias = new System.Windows.Forms.ListBox();
            this.lblSugerencias = new System.Windows.Forms.Label();
            this.lblExistePrefijo = new System.Windows.Forms.Label();
            this.lblPalabraCompleta = new System.Windows.Forms.Label();
            this.txtPrefijo = new System.Windows.Forms.TextBox();
            this.lblPrefijo = new System.Windows.Forms.Label();
            this.rdoCodigos = new System.Windows.Forms.RadioButton();
            this.rdoCiudades = new System.Windows.Forms.RadioButton();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btnContraer = new System.Windows.Forms.Button();
            this.btnExpandir = new System.Windows.Forms.Button();
            this.lblArbol = new System.Windows.Forms.Label();
            this.trvTrie = new System.Windows.Forms.TreeView();
            this.panelIzquierdo.SuspendLayout();
            this.grpBusqueda.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpBusqueda);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpBusqueda
            //
            this.grpBusqueda.Controls.Add(this.lblTotal);
            this.grpBusqueda.Controls.Add(this.lstSugerencias);
            this.grpBusqueda.Controls.Add(this.lblSugerencias);
            this.grpBusqueda.Controls.Add(this.lblExistePrefijo);
            this.grpBusqueda.Controls.Add(this.lblPalabraCompleta);
            this.grpBusqueda.Controls.Add(this.txtPrefijo);
            this.grpBusqueda.Controls.Add(this.lblPrefijo);
            this.grpBusqueda.Controls.Add(this.rdoCodigos);
            this.grpBusqueda.Controls.Add(this.rdoCiudades);
            this.grpBusqueda.Location = new System.Drawing.Point(8, 8);
            this.grpBusqueda.Name = "grpBusqueda";
            this.grpBusqueda.Size = new System.Drawing.Size(316, 600);
            this.grpBusqueda.TabIndex = 0;
            this.grpBusqueda.TabStop = false;
            this.grpBusqueda.Text = "Búsqueda por prefijo";
            //
            // rdoCiudades
            //
            this.rdoCiudades.AutoSize = true;
            this.rdoCiudades.Checked = true;
            this.rdoCiudades.Location = new System.Drawing.Point(12, 24);
            this.rdoCiudades.Name = "rdoCiudades";
            this.rdoCiudades.Size = new System.Drawing.Size(114, 19);
            this.rdoCiudades.TabIndex = 0;
            this.rdoCiudades.TabStop = true;
            this.rdoCiudades.Text = "Trie de ciudades";
            this.rdoCiudades.UseVisualStyleBackColor = true;
            this.rdoCiudades.CheckedChanged += new System.EventHandler(this.rdoCiudades_CheckedChanged);
            //
            // rdoCodigos
            //
            this.rdoCodigos.AutoSize = true;
            this.rdoCodigos.Location = new System.Drawing.Point(12, 48);
            this.rdoCodigos.Name = "rdoCodigos";
            this.rdoCodigos.Size = new System.Drawing.Size(172, 19);
            this.rdoCodigos.TabIndex = 1;
            this.rdoCodigos.Text = "Trie de códigos de rastreo";
            this.rdoCodigos.UseVisualStyleBackColor = true;
            //
            // lblPrefijo
            //
            this.lblPrefijo.AutoSize = true;
            this.lblPrefijo.Location = new System.Drawing.Point(12, 80);
            this.lblPrefijo.Name = "lblPrefijo";
            this.lblPrefijo.Size = new System.Drawing.Size(220, 15);
            this.lblPrefijo.TabIndex = 2;
            this.lblPrefijo.Text = "Prefijo (las sugerencias salen al escribir)";
            //
            // txtPrefijo
            //
            this.txtPrefijo.Location = new System.Drawing.Point(12, 98);
            this.txtPrefijo.Name = "txtPrefijo";
            this.txtPrefijo.PlaceholderText = "Ej. san";
            this.txtPrefijo.Size = new System.Drawing.Size(292, 23);
            this.txtPrefijo.TabIndex = 3;
            this.txtPrefijo.TextChanged += new System.EventHandler(this.txtPrefijo_TextChanged);
            //
            // lblPalabraCompleta
            //
            this.lblPalabraCompleta.AutoSize = true;
            this.lblPalabraCompleta.Location = new System.Drawing.Point(12, 132);
            this.lblPalabraCompleta.Name = "lblPalabraCompleta";
            this.lblPalabraCompleta.Size = new System.Drawing.Size(200, 15);
            this.lblPalabraCompleta.TabIndex = 4;
            this.lblPalabraCompleta.Text = "¿Es una palabra completa? (Buscar): —";
            //
            // lblExistePrefijo
            //
            this.lblExistePrefijo.AutoSize = true;
            this.lblExistePrefijo.Location = new System.Drawing.Point(12, 152);
            this.lblExistePrefijo.Name = "lblExistePrefijo";
            this.lblExistePrefijo.Size = new System.Drawing.Size(200, 15);
            this.lblExistePrefijo.TabIndex = 5;
            this.lblExistePrefijo.Text = "¿Existe el prefijo? (EmpiezaCon): —";
            //
            // lblSugerencias
            //
            this.lblSugerencias.AutoSize = true;
            this.lblSugerencias.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSugerencias.Location = new System.Drawing.Point(12, 180);
            this.lblSugerencias.Name = "lblSugerencias";
            this.lblSugerencias.Size = new System.Drawing.Size(200, 15);
            this.lblSugerencias.TabIndex = 6;
            this.lblSugerencias.Text = "Sugerencias (SugerirPorPrefijo)";
            //
            // lstSugerencias
            //
            this.lstSugerencias.FormattingEnabled = true;
            this.lstSugerencias.IntegralHeight = false;
            this.lstSugerencias.ItemHeight = 15;
            this.lstSugerencias.Location = new System.Drawing.Point(12, 200);
            this.lstSugerencias.Name = "lstSugerencias";
            this.lstSugerencias.Size = new System.Drawing.Size(292, 356);
            this.lstSugerencias.TabIndex = 7;
            //
            // lblTotal
            //
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotal.Location = new System.Drawing.Point(12, 566);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(292, 20);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Palabras guardadas en el Trie: 0";
            //
            // panelSuperior
            //
            this.panelSuperior.Controls.Add(this.btnContraer);
            this.panelSuperior.Controls.Add(this.btnExpandir);
            this.panelSuperior.Controls.Add(this.lblArbol);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(345, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(931, 44);
            this.panelSuperior.TabIndex = 1;
            //
            // lblArbol
            //
            this.lblArbol.AutoSize = true;
            this.lblArbol.Location = new System.Drawing.Point(8, 14);
            this.lblArbol.Name = "lblArbol";
            this.lblArbol.Size = new System.Drawing.Size(480, 15);
            this.lblArbol.TabIndex = 0;
            this.lblArbol.Text = "Estructura interna del Trie: cada nodo es una letra y ✔ marca el final de una palabra.";
            //
            // btnExpandir
            //
            this.btnExpandir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpandir.Location = new System.Drawing.Point(711, 9);
            this.btnExpandir.Name = "btnExpandir";
            this.btnExpandir.Size = new System.Drawing.Size(105, 27);
            this.btnExpandir.TabIndex = 1;
            this.btnExpandir.Text = "Expandir todo";
            this.btnExpandir.UseVisualStyleBackColor = true;
            this.btnExpandir.Click += new System.EventHandler(this.btnExpandir_Click);
            //
            // btnContraer
            //
            this.btnContraer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContraer.Location = new System.Drawing.Point(822, 9);
            this.btnContraer.Name = "btnContraer";
            this.btnContraer.Size = new System.Drawing.Size(100, 27);
            this.btnContraer.TabIndex = 2;
            this.btnContraer.Text = "Contraer";
            this.btnContraer.UseVisualStyleBackColor = true;
            this.btnContraer.Click += new System.EventHandler(this.btnContraer_Click);
            //
            // trvTrie
            //
            this.trvTrie.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTrie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTrie.Font = new System.Drawing.Font("Consolas", 10F);
            this.trvTrie.HideSelection = false;
            this.trvTrie.Location = new System.Drawing.Point(345, 44);
            this.trvTrie.Name = "trvTrie";
            this.trvTrie.Size = new System.Drawing.Size(931, 659);
            this.trvTrie.TabIndex = 2;
            //
            // PestanaTrie
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trvTrie);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaTrie";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpBusqueda.ResumeLayout(false);
            this.grpBusqueda.PerformLayout();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpBusqueda;
        private System.Windows.Forms.RadioButton rdoCiudades;
        private System.Windows.Forms.RadioButton rdoCodigos;
        private System.Windows.Forms.Label lblPrefijo;
        private System.Windows.Forms.TextBox txtPrefijo;
        private System.Windows.Forms.Label lblPalabraCompleta;
        private System.Windows.Forms.Label lblExistePrefijo;
        private System.Windows.Forms.Label lblSugerencias;
        private System.Windows.Forms.ListBox lstSugerencias;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblArbol;
        private System.Windows.Forms.Button btnExpandir;
        private System.Windows.Forms.Button btnContraer;
        private System.Windows.Forms.TreeView trvTrie;
    }
}
