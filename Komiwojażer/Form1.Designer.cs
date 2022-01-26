namespace Komiwojażer
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mapa = new System.Windows.Forms.PictureBox();
            this.draw_path_button = new System.Windows.Forms.Button();
            this.clear_vertices_button = new System.Windows.Forms.Button();
            this.selsect_start_position_button = new System.Windows.Forms.Button();
            this.select_destination_button = new System.Windows.Forms.Button();
            this.show_NN = new System.Windows.Forms.CheckBox();
            this.show_Proxy = new System.Windows.Forms.CheckBox();
            this.show_Precise = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.show_NN_path_length = new System.Windows.Forms.TextBox();
            this.show_proxy_path_length = new System.Windows.Forms.TextBox();
            this.show_precise_path_length = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapa)).BeginInit();
            this.SuspendLayout();
            // 
            // mapa
            // 
            this.mapa.Image = ((System.Drawing.Image)(resources.GetObject("mapa.Image")));
            this.mapa.Location = new System.Drawing.Point(4, 15);
            this.mapa.Margin = new System.Windows.Forms.Padding(4);
            this.mapa.Name = "mapa";
            this.mapa.Size = new System.Drawing.Size(1509, 818);
            this.mapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mapa.TabIndex = 0;
            this.mapa.TabStop = false;
            this.mapa.Click += new System.EventHandler(this.mapa_Click);
            this.mapa.Paint += new System.Windows.Forms.PaintEventHandler(this.mapa_Paint);
            // 
            // draw_path_button
            // 
            this.draw_path_button.Location = new System.Drawing.Point(1518, 179);
            this.draw_path_button.Margin = new System.Windows.Forms.Padding(2);
            this.draw_path_button.Name = "draw_path_button";
            this.draw_path_button.Size = new System.Drawing.Size(310, 46);
            this.draw_path_button.TabIndex = 3;
            this.draw_path_button.Text = "Rysuj ścieżke";
            this.draw_path_button.UseVisualStyleBackColor = true;
            this.draw_path_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // clear_vertices_button
            // 
            this.clear_vertices_button.Location = new System.Drawing.Point(1518, 126);
            this.clear_vertices_button.Margin = new System.Windows.Forms.Padding(2);
            this.clear_vertices_button.Name = "clear_vertices_button";
            this.clear_vertices_button.Size = new System.Drawing.Size(310, 50);
            this.clear_vertices_button.TabIndex = 4;
            this.clear_vertices_button.Text = "Wyczyść punkty docelowe";
            this.clear_vertices_button.UseVisualStyleBackColor = true;
            this.clear_vertices_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // selsect_start_position_button
            // 
            this.selsect_start_position_button.Location = new System.Drawing.Point(1518, 15);
            this.selsect_start_position_button.Margin = new System.Windows.Forms.Padding(2);
            this.selsect_start_position_button.Name = "selsect_start_position_button";
            this.selsect_start_position_button.Size = new System.Drawing.Size(310, 50);
            this.selsect_start_position_button.TabIndex = 5;
            this.selsect_start_position_button.Text = "Wybierz punkt startowy";
            this.selsect_start_position_button.UseVisualStyleBackColor = true;
            this.selsect_start_position_button.Click += new System.EventHandler(this.selsect_start_position_button_Click);
            // 
            // select_destination_button
            // 
            this.select_destination_button.Location = new System.Drawing.Point(1518, 68);
            this.select_destination_button.Margin = new System.Windows.Forms.Padding(2);
            this.select_destination_button.Name = "select_destination_button";
            this.select_destination_button.Size = new System.Drawing.Size(310, 50);
            this.select_destination_button.TabIndex = 6;
            this.select_destination_button.Text = "Wybierz punkty docelowe";
            this.select_destination_button.UseVisualStyleBackColor = true;
            this.select_destination_button.Click += new System.EventHandler(this.select_destination_button_Click);
            // 
            // show_NN
            // 
            this.show_NN.AutoSize = true;
            this.show_NN.Checked = true;
            this.show_NN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_NN.Location = new System.Drawing.Point(1518, 338);
            this.show_NN.Margin = new System.Windows.Forms.Padding(4);
            this.show_NN.Name = "show_NN";
            this.show_NN.Size = new System.Drawing.Size(192, 29);
            this.show_NN.TabIndex = 7;
            this.show_NN.Text = "Najbliższy Sąsiad";
            this.show_NN.UseVisualStyleBackColor = true;
            this.show_NN.CheckedChanged += new System.EventHandler(this.show_NN_CheckedChanged);
            // 
            // show_Proxy
            // 
            this.show_Proxy.AutoSize = true;
            this.show_Proxy.Checked = true;
            this.show_Proxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_Proxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.show_Proxy.Location = new System.Drawing.Point(1518, 373);
            this.show_Proxy.Margin = new System.Windows.Forms.Padding(4);
            this.show_Proxy.Name = "show_Proxy";
            this.show_Proxy.Size = new System.Drawing.Size(88, 29);
            this.show_Proxy.TabIndex = 8;
            this.show_Proxy.Text = "Proxy";
            this.show_Proxy.UseVisualStyleBackColor = true;
            this.show_Proxy.CheckedChanged += new System.EventHandler(this.show_Proxy_CheckedChanged);
            // 
            // show_Precise
            // 
            this.show_Precise.AutoSize = true;
            this.show_Precise.Checked = true;
            this.show_Precise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_Precise.ForeColor = System.Drawing.Color.Orange;
            this.show_Precise.Location = new System.Drawing.Point(1518, 410);
            this.show_Precise.Margin = new System.Windows.Forms.Padding(4);
            this.show_Precise.Name = "show_Precise";
            this.show_Precise.Size = new System.Drawing.Size(103, 29);
            this.show_Precise.TabIndex = 9;
            this.show_Precise.Text = "Precise";
            this.show_Precise.UseVisualStyleBackColor = true;
            this.show_Precise.CheckedChanged += new System.EventHandler(this.show_Precise_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1566, 942);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(310, 46);
            this.button1.TabIndex = 10;
            this.button1.Text = "Wyjście";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1566, 875);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(310, 46);
            this.button2.TabIndex = 11;
            this.button2.Text = "Powrót";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // show_NN_path_length
            // 
            this.show_NN_path_length.Location = new System.Drawing.Point(1717, 336);
            this.show_NN_path_length.Name = "show_NN_path_length";
            this.show_NN_path_length.ReadOnly = true;
            this.show_NN_path_length.Size = new System.Drawing.Size(132, 29);
            this.show_NN_path_length.TabIndex = 12;
            this.show_NN_path_length.Text = "0";
            // 
            // show_proxy_path_length
            // 
            this.show_proxy_path_length.Location = new System.Drawing.Point(1717, 373);
            this.show_proxy_path_length.Name = "show_proxy_path_length";
            this.show_proxy_path_length.ReadOnly = true;
            this.show_proxy_path_length.Size = new System.Drawing.Size(132, 29);
            this.show_proxy_path_length.TabIndex = 13;
            this.show_proxy_path_length.Text = "0";
            // 
            // show_precise_path_length
            // 
            this.show_precise_path_length.Location = new System.Drawing.Point(1717, 410);
            this.show_precise_path_length.Name = "show_precise_path_length";
            this.show_precise_path_length.ReadOnly = true;
            this.show_precise_path_length.Size = new System.Drawing.Size(132, 29);
            this.show_precise_path_length.TabIndex = 14;
            this.show_precise_path_length.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1684, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Długość marszruty [m]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1923, 1050);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.show_precise_path_length);
            this.Controls.Add(this.show_proxy_path_length);
            this.Controls.Add(this.show_NN_path_length);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.show_Precise);
            this.Controls.Add(this.show_Proxy);
            this.Controls.Add(this.show_NN);
            this.Controls.Add(this.select_destination_button);
            this.Controls.Add(this.selsect_start_position_button);
            this.Controls.Add(this.clear_vertices_button);
            this.Controls.Add(this.draw_path_button);
            this.Controls.Add(this.mapa);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mapa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mapa;
        private System.Windows.Forms.Button draw_path_button;
        private System.Windows.Forms.Button clear_vertices_button;
        private System.Windows.Forms.Button selsect_start_position_button;
        private System.Windows.Forms.Button select_destination_button;
        private System.Windows.Forms.CheckBox show_NN;
        private System.Windows.Forms.CheckBox show_Proxy;
        private System.Windows.Forms.CheckBox show_Precise;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox show_NN_path_length;
        private System.Windows.Forms.TextBox show_proxy_path_length;
        private System.Windows.Forms.TextBox show_precise_path_length;
        private System.Windows.Forms.Label label1;
    }
}

