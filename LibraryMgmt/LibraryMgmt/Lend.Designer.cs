namespace LibraryMgmt
{
    partial class Lend
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
            this.label1 = new System.Windows.Forms.Label();
            this.issueButton = new System.Windows.Forms.Button();
            this.returnButton = new System.Windows.Forms.Button();
            this.issueGrid = new System.Windows.Forms.DataGridView();
            this.backButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.issueGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lending Table";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // issueButton
            // 
            this.issueButton.Location = new System.Drawing.Point(277, 99);
            this.issueButton.Name = "issueButton";
            this.issueButton.Size = new System.Drawing.Size(106, 49);
            this.issueButton.TabIndex = 1;
            this.issueButton.Text = "Book Issue";
            this.issueButton.UseVisualStyleBackColor = true;
            this.issueButton.Click += new System.EventHandler(this.issueButton_Click);
            // 
            // returnButton
            // 
            this.returnButton.Location = new System.Drawing.Point(410, 99);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(101, 49);
            this.returnButton.TabIndex = 2;
            this.returnButton.Text = "Book Return";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // issueGrid
            // 
            this.issueGrid.AllowUserToAddRows = false;
            this.issueGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.issueGrid.Location = new System.Drawing.Point(175, 166);
            this.issueGrid.Name = "issueGrid";
            this.issueGrid.ReadOnly = true;
            this.issueGrid.RowHeadersWidth = 51;
            this.issueGrid.RowTemplate.Height = 24;
            this.issueGrid.Size = new System.Drawing.Size(449, 150);
            this.issueGrid.TabIndex = 3;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(655, 400);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "<<Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // Lend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.issueGrid);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.issueButton);
            this.Controls.Add(this.label1);
            this.Name = "Lend";
            this.Text = "Lend";
            ((System.ComponentModel.ISupportInitialize)(this.issueGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button issueButton;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.DataGridView issueGrid;
        private System.Windows.Forms.Button backButton;
    }
}