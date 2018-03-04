namespace project3
{
    partial class ResearchCitations
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
            this.researchListBox = new System.Windows.Forms.ListBox();
            this.researchDtlLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // researchListBox
            // 
            this.researchListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.researchListBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.researchListBox.FormattingEnabled = true;
            this.researchListBox.HorizontalScrollbar = true;
            this.researchListBox.ItemHeight = 16;
            this.researchListBox.Location = new System.Drawing.Point(28, 79);
            this.researchListBox.Name = "researchListBox";
            this.researchListBox.Size = new System.Drawing.Size(1090, 368);
            this.researchListBox.TabIndex = 0;
            // 
            // researchDtlLbl
            // 
            this.researchDtlLbl.AutoSize = true;
            this.researchDtlLbl.Font = new System.Drawing.Font("Verdana", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.researchDtlLbl.ForeColor = System.Drawing.Color.Red;
            this.researchDtlLbl.Location = new System.Drawing.Point(22, 19);
            this.researchDtlLbl.Name = "researchDtlLbl";
            this.researchDtlLbl.Size = new System.Drawing.Size(107, 32);
            this.researchDtlLbl.TabIndex = 1;
            this.researchDtlLbl.Text = "label1";
            // 
            // ResearchCitations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 495);
            this.Controls.Add(this.researchDtlLbl);
            this.Controls.Add(this.researchListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResearchCitations";
            this.Text = "ResearchCitations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox researchListBox;
        private System.Windows.Forms.Label researchDtlLbl;
    }
}