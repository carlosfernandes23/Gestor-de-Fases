namespace Gestor_de_Fases
{
    partial class labelnomeficheiro
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
            this.Mover = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ResizeForm1 = new Guna.UI2.WinForms.Guna2ResizeForm(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.Borda = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.Minimizar = new Guna.UI2.WinForms.Guna2ControlBox();
            this.Fechar = new Guna.UI2.WinForms.Guna2ControlBox();
            this.labelNomeFile = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ButtonClean = new WiLBiT.WiLBiTButton();
            this.ButtonGerarOF = new WiLBiT.WiLBiTButton();
            this.ButtonTakePrint = new WiLBiT.WiLBiTButton();
            this.cbA3 = new Guna.UI2.WinForms.Guna2CheckBox();
            this.cbA4 = new Guna.UI2.WinForms.Guna2CheckBox();
            this.Print = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Print)).BeginInit();
            this.SuspendLayout();
            // 
            // Mover
            // 
            this.Mover.DockIndicatorTransparencyValue = 0.6D;
            this.Mover.TargetControl = this;
            this.Mover.UseTransparentDrag = true;
            // 
            // guna2ResizeForm1
            // 
            this.guna2ResizeForm1.TargetForm = this;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2AnimateWindow1
            // 
            this.guna2AnimateWindow1.TargetForm = this;
            // 
            // Borda
            // 
            this.Borda.AnimateWindow = true;
            this.Borda.AnimationInterval = 300;
            this.Borda.BorderRadius = 20;
            this.Borda.ContainerControl = this;
            this.Borda.DockIndicatorColor = System.Drawing.Color.Silver;
            this.Borda.DockIndicatorTransparencyValue = 0.8D;
            this.Borda.DragStartTransparencyValue = 0.7D;
            this.Borda.ShadowColor = System.Drawing.Color.Silver;
            this.Borda.TransparentWhileDrag = true;
            // 
            // Minimizar
            // 
            this.Minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimizar.BackColor = System.Drawing.Color.Transparent;
            this.Minimizar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Minimizar.BorderRadius = 6;
            this.Minimizar.BorderThickness = 1;
            this.Minimizar.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.Minimizar.FillColor = System.Drawing.Color.WhiteSmoke;
            this.Minimizar.HoverState.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Minimizar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Minimizar.HoverState.IconColor = System.Drawing.Color.WhiteSmoke;
            this.Minimizar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Minimizar.Location = new System.Drawing.Point(733, 12);
            this.Minimizar.Name = "Minimizar";
            this.Minimizar.PressedColor = System.Drawing.Color.WhiteSmoke;
            this.Minimizar.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Minimizar.ShadowDecoration.Depth = 70;
            this.Minimizar.Size = new System.Drawing.Size(25, 20);
            this.Minimizar.TabIndex = 14;
            // 
            // Fechar
            // 
            this.Fechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fechar.BackColor = System.Drawing.Color.Transparent;
            this.Fechar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Fechar.BorderRadius = 6;
            this.Fechar.BorderThickness = 1;
            this.Fechar.FillColor = System.Drawing.Color.WhiteSmoke;
            this.Fechar.HoverState.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.Fechar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Fechar.HoverState.IconColor = System.Drawing.Color.WhiteSmoke;
            this.Fechar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Fechar.Location = new System.Drawing.Point(763, 12);
            this.Fechar.Name = "Fechar";
            this.Fechar.PressedColor = System.Drawing.Color.WhiteSmoke;
            this.Fechar.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.Fechar.ShadowDecoration.Depth = 70;
            this.Fechar.Size = new System.Drawing.Size(25, 20);
            this.Fechar.TabIndex = 13;
            // 
            // labelNomeFile
            // 
            this.labelNomeFile.BackColor = System.Drawing.Color.Transparent;
            this.labelNomeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelNomeFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.labelNomeFile.Location = new System.Drawing.Point(13, 22);
            this.labelNomeFile.Name = "labelNomeFile";
            this.labelNomeFile.Size = new System.Drawing.Size(45, 22);
            this.labelNomeFile.TabIndex = 15;
            this.labelNomeFile.Text = "label1";
            this.labelNomeFile.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonClean
            // 
            this.ButtonClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.ButtonClean.BorderColor = System.Drawing.Color.DarkGray;
            this.ButtonClean.BorderRadius = 6;
            this.ButtonClean.BorderSize = 0;
            this.ButtonClean.FlatAppearance.BorderSize = 0;
            this.ButtonClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.ButtonClean.ForeColor = System.Drawing.Color.White;
            this.ButtonClean.Location = new System.Drawing.Point(332, 9);
            this.ButtonClean.Name = "ButtonClean";
            this.ButtonClean.Size = new System.Drawing.Size(104, 35);
            this.ButtonClean.TabIndex = 30;
            this.ButtonClean.Text = "Limpar";
            this.ButtonClean.UseVisualStyleBackColor = false;
            this.ButtonClean.Click += new System.EventHandler(this.ButtonClean_Click);
            // 
            // ButtonGerarOF
            // 
            this.ButtonGerarOF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.ButtonGerarOF.BorderColor = System.Drawing.Color.DarkGray;
            this.ButtonGerarOF.BorderRadius = 6;
            this.ButtonGerarOF.BorderSize = 0;
            this.ButtonGerarOF.FlatAppearance.BorderSize = 0;
            this.ButtonGerarOF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonGerarOF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.ButtonGerarOF.ForeColor = System.Drawing.Color.White;
            this.ButtonGerarOF.Location = new System.Drawing.Point(511, 9);
            this.ButtonGerarOF.Name = "ButtonGerarOF";
            this.ButtonGerarOF.Size = new System.Drawing.Size(100, 35);
            this.ButtonGerarOF.TabIndex = 31;
            this.ButtonGerarOF.Text = "Gerar OF";
            this.ButtonGerarOF.UseVisualStyleBackColor = false;
            this.ButtonGerarOF.Click += new System.EventHandler(this.ButtonGerarOF_Click);
            // 
            // ButtonTakePrint
            // 
            this.ButtonTakePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.ButtonTakePrint.BorderColor = System.Drawing.Color.DarkGray;
            this.ButtonTakePrint.BorderRadius = 6;
            this.ButtonTakePrint.BorderSize = 0;
            this.ButtonTakePrint.FlatAppearance.BorderSize = 0;
            this.ButtonTakePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonTakePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.ButtonTakePrint.ForeColor = System.Drawing.Color.White;
            this.ButtonTakePrint.Location = new System.Drawing.Point(617, 9);
            this.ButtonTakePrint.Name = "ButtonTakePrint";
            this.ButtonTakePrint.Size = new System.Drawing.Size(100, 35);
            this.ButtonTakePrint.TabIndex = 32;
            this.ButtonTakePrint.Text = "Print";
            this.ButtonTakePrint.UseVisualStyleBackColor = false;
            this.ButtonTakePrint.Click += new System.EventHandler(this.ButtonTakePrint_Click);
            // 
            // cbA3
            // 
            this.cbA3.AutoSize = true;
            this.cbA3.BackColor = System.Drawing.Color.Transparent;
            this.cbA3.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA3.CheckedState.BorderRadius = 3;
            this.cbA3.CheckedState.BorderThickness = 1;
            this.cbA3.CheckedState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.cbA3.CheckMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbA3.Location = new System.Drawing.Point(454, 30);
            this.cbA3.Name = "cbA3";
            this.cbA3.Size = new System.Drawing.Size(44, 21);
            this.cbA3.TabIndex = 81;
            this.cbA3.Text = "A3";
            this.cbA3.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA3.UncheckedState.BorderRadius = 3;
            this.cbA3.UncheckedState.BorderThickness = 1;
            this.cbA3.UncheckedState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.cbA3.UseVisualStyleBackColor = false;
            // 
            // cbA4
            // 
            this.cbA4.AutoSize = true;
            this.cbA4.BackColor = System.Drawing.Color.Transparent;
            this.cbA4.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA4.CheckedState.BorderRadius = 3;
            this.cbA4.CheckedState.BorderThickness = 1;
            this.cbA4.CheckedState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.cbA4.CheckMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbA4.Location = new System.Drawing.Point(454, 9);
            this.cbA4.Name = "cbA4";
            this.cbA4.Size = new System.Drawing.Size(44, 21);
            this.cbA4.TabIndex = 80;
            this.cbA4.Text = "A4";
            this.cbA4.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(93)))), ((int)(((byte)(109)))));
            this.cbA4.UncheckedState.BorderRadius = 3;
            this.cbA4.UncheckedState.BorderThickness = 1;
            this.cbA4.UncheckedState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.cbA4.UseVisualStyleBackColor = false;
            // 
            // Print
            // 
            this.Print.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Print.BackColor = System.Drawing.Color.Chocolate;
            this.Print.Location = new System.Drawing.Point(12, 77);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(776, 411);
            this.Print.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Print.TabIndex = 82;
            this.Print.TabStop = false;
            // 
            // labelnomeficheiro
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.cbA3);
            this.Controls.Add(this.cbA4);
            this.Controls.Add(this.ButtonTakePrint);
            this.Controls.Add(this.ButtonGerarOF);
            this.Controls.Add(this.ButtonClean);
            this.Controls.Add(this.labelNomeFile);
            this.Controls.Add(this.Minimizar);
            this.Controls.Add(this.Fechar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "labelnomeficheiro";
            this.TransparencyKey = System.Drawing.Color.Chocolate;
            this.Load += new System.EventHandler(this.labelnomeficheiro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Print)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl Mover;
        private Guna.UI2.WinForms.Guna2ResizeForm guna2ResizeForm1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2BorderlessForm Borda;
        private Guna.UI2.WinForms.Guna2ControlBox Minimizar;
        private Guna.UI2.WinForms.Guna2ControlBox Fechar;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelNomeFile;
        private WiLBiT.WiLBiTButton ButtonTakePrint;
        private WiLBiT.WiLBiTButton ButtonGerarOF;
        private WiLBiT.WiLBiTButton ButtonClean;
        private Guna.UI2.WinForms.Guna2CheckBox cbA3;
        private Guna.UI2.WinForms.Guna2CheckBox cbA4;
        private System.Windows.Forms.PictureBox Print;
    }
}