namespace LogisticaEnvios.GUI
{
    partial class PestanaRastreo
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
            this.grpPaso4 = new System.Windows.Forms.GroupBox();
            this.txtRutas = new System.Windows.Forms.TextBox();
            this.grpPaso3 = new System.Windows.Forms.GroupBox();
            this.lblPaso3 = new System.Windows.Forms.Label();
            this.grpPaso2 = new System.Windows.Forms.GroupBox();
            this.txtEnvio = new System.Windows.Forms.TextBox();
            this.grpPaso1 = new System.Windows.Forms.GroupBox();
            this.lblPaso1 = new System.Windows.Forms.Label();
            this.btnRastrear = new System.Windows.Forms.Button();
            this.autoCodigo = new LogisticaEnvios.GUI.CuadroAutocompletado();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.panelMapa = new LogisticaEnvios.GUI.PanelMapa();
            this.panelIzquierdo.SuspendLayout();
            this.grpPaso4.SuspendLayout();
            this.grpPaso3.SuspendLayout();
            this.grpPaso2.SuspendLayout();
            this.grpPaso1.SuspendLayout();
            this.SuspendLayout();
            //
            // panelIzquierdo
            //
            this.panelIzquierdo.AutoScroll = true;
            this.panelIzquierdo.Controls.Add(this.grpPaso4);
            this.panelIzquierdo.Controls.Add(this.grpPaso3);
            this.panelIzquierdo.Controls.Add(this.grpPaso2);
            this.panelIzquierdo.Controls.Add(this.grpPaso1);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 0);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(345, 703);
            this.panelIzquierdo.TabIndex = 0;
            //
            // grpPaso1
            //
            this.grpPaso1.Controls.Add(this.lblPaso1);
            this.grpPaso1.Controls.Add(this.btnRastrear);
            this.grpPaso1.Controls.Add(this.autoCodigo);
            this.grpPaso1.Controls.Add(this.lblCodigo);
            this.grpPaso1.Location = new System.Drawing.Point(8, 8);
            this.grpPaso1.Name = "grpPaso1";
            this.grpPaso1.Size = new System.Drawing.Size(316, 122);
            this.grpPaso1.TabIndex = 0;
            this.grpPaso1.TabStop = false;
            this.grpPaso1.Text = "Paso 1 · Trie: sugerir el código";
            //
            // lblCodigo
            //
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 24);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(198, 15);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código de rastreo (o solo su inicio)";
            //
            // autoCodigo
            //
            this.autoCodigo.Location = new System.Drawing.Point(12, 44);
            this.autoCodigo.Name = "autoCodigo";
            this.autoCodigo.Size = new System.Drawing.Size(190, 23);
            this.autoCodigo.TabIndex = 1;
            this.autoCodigo.TextoGuia = "Ej. ENV-10";
            //
            // btnRastrear
            //
            this.btnRastrear.Location = new System.Drawing.Point(210, 42);
            this.btnRastrear.Name = "btnRastrear";
            this.btnRastrear.Size = new System.Drawing.Size(94, 27);
            this.btnRastrear.TabIndex = 2;
            this.btnRastrear.Text = "Rastrear";
            this.btnRastrear.UseVisualStyleBackColor = true;
            this.btnRastrear.Click += new System.EventHandler(this.btnRastrear_Click);
            //
            // lblPaso1
            //
            this.lblPaso1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaso1.Location = new System.Drawing.Point(12, 76);
            this.lblPaso1.Name = "lblPaso1";
            this.lblPaso1.Size = new System.Drawing.Size(292, 36);
            this.lblPaso1.TabIndex = 3;
            this.lblPaso1.Text = "Mientras escribe, el Trie de códigos sugiere los envíos que empiezan así.";
            //
            // grpPaso2
            //
            this.grpPaso2.Controls.Add(this.txtEnvio);
            this.grpPaso2.Location = new System.Drawing.Point(8, 138);
            this.grpPaso2.Name = "grpPaso2";
            this.grpPaso2.Size = new System.Drawing.Size(316, 162);
            this.grpPaso2.TabIndex = 1;
            this.grpPaso2.TabStop = false;
            this.grpPaso2.Text = "Paso 2 · Tabla Hash: localizar el envío";
            //
            // txtEnvio
            //
            this.txtEnvio.BackColor = System.Drawing.SystemColors.Window;
            this.txtEnvio.Location = new System.Drawing.Point(12, 24);
            this.txtEnvio.Multiline = true;
            this.txtEnvio.Name = "txtEnvio";
            this.txtEnvio.ReadOnly = true;
            this.txtEnvio.Size = new System.Drawing.Size(292, 126);
            this.txtEnvio.TabIndex = 0;
            this.txtEnvio.TabStop = false;
            //
            // grpPaso3
            //
            this.grpPaso3.Controls.Add(this.lblPaso3);
            this.grpPaso3.Location = new System.Drawing.Point(8, 308);
            this.grpPaso3.Name = "grpPaso3";
            this.grpPaso3.Size = new System.Drawing.Size(316, 94);
            this.grpPaso3.TabIndex = 2;
            this.grpPaso3.TabStop = false;
            this.grpPaso3.Text = "Paso 3 · Union-Find: validar conectividad";
            //
            // lblPaso3
            //
            this.lblPaso3.Location = new System.Drawing.Point(12, 24);
            this.lblPaso3.Name = "lblPaso3";
            this.lblPaso3.Size = new System.Drawing.Size(292, 60);
            this.lblPaso3.TabIndex = 0;
            //
            // grpPaso4
            //
            this.grpPaso4.Controls.Add(this.txtRutas);
            this.grpPaso4.Location = new System.Drawing.Point(8, 410);
            this.grpPaso4.Name = "grpPaso4";
            this.grpPaso4.Size = new System.Drawing.Size(316, 222);
            this.grpPaso4.TabIndex = 3;
            this.grpPaso4.TabStop = false;
            this.grpPaso4.Text = "Paso 4 · Grafo: Dijkstra por costo y por tiempo";
            //
            // txtRutas
            //
            this.txtRutas.BackColor = System.Drawing.SystemColors.Window;
            this.txtRutas.Location = new System.Drawing.Point(12, 24);
            this.txtRutas.Multiline = true;
            this.txtRutas.Name = "txtRutas";
            this.txtRutas.ReadOnly = true;
            this.txtRutas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRutas.Size = new System.Drawing.Size(292, 186);
            this.txtRutas.TabIndex = 0;
            this.txtRutas.TabStop = false;
            //
            // panelMapa
            //
            this.panelMapa.BackColor = System.Drawing.Color.White;
            this.panelMapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapa.Location = new System.Drawing.Point(345, 0);
            this.panelMapa.Name = "panelMapa";
            this.panelMapa.Size = new System.Drawing.Size(931, 703);
            this.panelMapa.TabIndex = 1;
            //
            // PestanaRastreo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMapa);
            this.Controls.Add(this.panelIzquierdo);
            this.Name = "PestanaRastreo";
            this.Size = new System.Drawing.Size(1276, 703);
            this.panelIzquierdo.ResumeLayout(false);
            this.grpPaso4.ResumeLayout(false);
            this.grpPaso4.PerformLayout();
            this.grpPaso3.ResumeLayout(false);
            this.grpPaso2.ResumeLayout(false);
            this.grpPaso2.PerformLayout();
            this.grpPaso1.ResumeLayout(false);
            this.grpPaso1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.GroupBox grpPaso1;
        private System.Windows.Forms.Label lblCodigo;
        private LogisticaEnvios.GUI.CuadroAutocompletado autoCodigo;
        private System.Windows.Forms.Button btnRastrear;
        private System.Windows.Forms.Label lblPaso1;
        private System.Windows.Forms.GroupBox grpPaso2;
        private System.Windows.Forms.TextBox txtEnvio;
        private System.Windows.Forms.GroupBox grpPaso3;
        private System.Windows.Forms.Label lblPaso3;
        private System.Windows.Forms.GroupBox grpPaso4;
        private System.Windows.Forms.TextBox txtRutas;
        private LogisticaEnvios.GUI.PanelMapa panelMapa;
    }
}
