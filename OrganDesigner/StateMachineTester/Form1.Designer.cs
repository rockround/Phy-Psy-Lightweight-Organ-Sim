namespace StateMachineTester
{
    partial class SimRate
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DTemp = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BTemp = new System.Windows.Forms.Label();
            this.CTemp = new System.Windows.Forms.Label();
            this.ATemp = new System.Windows.Forms.Label();
            this.Predicted = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SimRateVal = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SimRateVal)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.playClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(469, 300);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.pauseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "B";
            // 
            // DTemp
            // 
            this.DTemp.AutoSize = true;
            this.DTemp.Location = new System.Drawing.Point(365, 216);
            this.DTemp.Name = "DTemp";
            this.DTemp.Size = new System.Drawing.Size(13, 13);
            this.DTemp.TabIndex = 5;
            this.DTemp.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "D";
            // 
            // BTemp
            // 
            this.BTemp.AutoSize = true;
            this.BTemp.Location = new System.Drawing.Point(498, 160);
            this.BTemp.Name = "BTemp";
            this.BTemp.Size = new System.Drawing.Size(13, 13);
            this.BTemp.TabIndex = 8;
            this.BTemp.Text = "0";
            // 
            // CTemp
            // 
            this.CTemp.AutoSize = true;
            this.CTemp.Location = new System.Drawing.Point(365, 107);
            this.CTemp.Name = "CTemp";
            this.CTemp.Size = new System.Drawing.Size(13, 13);
            this.CTemp.TabIndex = 7;
            this.CTemp.Text = "0";
            // 
            // ATemp
            // 
            this.ATemp.AutoSize = true;
            this.ATemp.Location = new System.Drawing.Point(229, 160);
            this.ATemp.Name = "ATemp";
            this.ATemp.Size = new System.Drawing.Size(13, 13);
            this.ATemp.TabIndex = 6;
            this.ATemp.Text = "0";
            // 
            // Predicted
            // 
            this.Predicted.AutoSize = true;
            this.Predicted.Location = new System.Drawing.Point(384, 373);
            this.Predicted.Name = "Predicted";
            this.Predicted.Size = new System.Drawing.Size(13, 13);
            this.Predicted.TabIndex = 10;
            this.Predicted.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Predicted";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "SimRate";
            // 
            // SimRateVal
            // 
            this.SimRateVal.Location = new System.Drawing.Point(277, 21);
            this.SimRateVal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SimRateVal.Name = "SimRateVal";
            this.SimRateVal.Size = new System.Drawing.Size(120, 20);
            this.SimRateVal.TabIndex = 14;
            this.SimRateVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SimRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SimRateVal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Predicted);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BTemp);
            this.Controls.Add(this.CTemp);
            this.Controls.Add(this.ATemp);
            this.Controls.Add(this.DTemp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "SimRate";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SimRateVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label BTemp;
        private System.Windows.Forms.Label CTemp;
        private System.Windows.Forms.Label ATemp;
        private System.Windows.Forms.Label Predicted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown SimRateVal;
    }
}

