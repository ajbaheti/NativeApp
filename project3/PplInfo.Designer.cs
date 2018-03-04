namespace project3
{
    partial class PplInfo
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
            this.pplLabel = new System.Windows.Forms.Label();
            this.pplTitleLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.emailLbl = new System.Windows.Forms.Label();
            this.phoneLbl = new System.Windows.Forms.Label();
            this.officeLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pplLabel
            // 
            this.pplLabel.AutoSize = true;
            this.pplLabel.Font = new System.Drawing.Font("Verdana", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pplLabel.ForeColor = System.Drawing.Color.Red;
            this.pplLabel.Location = new System.Drawing.Point(74, 22);
            this.pplLabel.Name = "pplLabel";
            this.pplLabel.Size = new System.Drawing.Size(84, 25);
            this.pplLabel.TabIndex = 0;
            this.pplLabel.Text = "label1";
            // 
            // pplTitleLbl
            // 
            this.pplTitleLbl.AutoSize = true;
            this.pplTitleLbl.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pplTitleLbl.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.pplTitleLbl.Location = new System.Drawing.Point(314, 29);
            this.pplTitleLbl.Name = "pplTitleLbl";
            this.pplTitleLbl.Size = new System.Drawing.Size(94, 18);
            this.pplTitleLbl.TabIndex = 1;
            this.pplTitleLbl.Text = "pplTitleLbl";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(79, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(179, 178);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // emailLbl
            // 
            this.emailLbl.AutoSize = true;
            this.emailLbl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.Location = new System.Drawing.Point(327, 96);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(0, 16);
            this.emailLbl.TabIndex = 3;
            // 
            // phoneLbl
            // 
            this.phoneLbl.AutoSize = true;
            this.phoneLbl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneLbl.Location = new System.Drawing.Point(327, 138);
            this.phoneLbl.Name = "phoneLbl";
            this.phoneLbl.Size = new System.Drawing.Size(0, 16);
            this.phoneLbl.TabIndex = 4;
            // 
            // officeLbl
            // 
            this.officeLbl.AutoSize = true;
            this.officeLbl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.officeLbl.Location = new System.Drawing.Point(327, 188);
            this.officeLbl.Name = "officeLbl";
            this.officeLbl.Size = new System.Drawing.Size(0, 16);
            this.officeLbl.TabIndex = 5;
            // 
            // PplInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 300);
            this.Controls.Add(this.officeLbl);
            this.Controls.Add(this.phoneLbl);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pplTitleLbl);
            this.Controls.Add(this.pplLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PplInfo";
            this.Text = "PplInfo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pplLabel;
        private System.Windows.Forms.Label pplTitleLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.Label phoneLbl;
        private System.Windows.Forms.Label officeLbl;
    }
}