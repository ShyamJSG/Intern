namespace LibraryMgmt
{
    partial class Issue
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
            this.submitButton = new System.Windows.Forms.Button();
            this.conname = new System.Windows.Forms.Label();
            this.bookname = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cnametxt = new System.Windows.Forms.TextBox();
            this.bnametxt = new System.Windows.Forms.TextBox();
            this.datepicker = new System.Windows.Forms.DateTimePicker();
            this.duecheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(127, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Book Issue";
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(171, 293);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(84, 30);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // conname
            // 
            this.conname.AutoSize = true;
            this.conname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conname.Location = new System.Drawing.Point(49, 90);
            this.conname.Name = "conname";
            this.conname.Size = new System.Drawing.Size(131, 20);
            this.conname.TabIndex = 2;
            this.conname.Text = "Customer Name";
            // 
            // bookname
            // 
            this.bookname.AutoSize = true;
            this.bookname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bookname.Location = new System.Drawing.Point(49, 133);
            this.bookname.Name = "bookname";
            this.bookname.Size = new System.Drawing.Size(96, 20);
            this.bookname.TabIndex = 3;
            this.bookname.Text = "Book Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Issue Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(49, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Due?";
            // 
            // cnametxt
            // 
            this.cnametxt.Location = new System.Drawing.Point(232, 90);
            this.cnametxt.Name = "cnametxt";
            this.cnametxt.Size = new System.Drawing.Size(100, 22);
            this.cnametxt.TabIndex = 7;
            // 
            // bnametxt
            // 
            this.bnametxt.Location = new System.Drawing.Point(232, 131);
            this.bnametxt.Name = "bnametxt";
            this.bnametxt.Size = new System.Drawing.Size(100, 22);
            this.bnametxt.TabIndex = 8;
            // 
            // datepicker
            // 
            this.datepicker.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.datepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datepicker.Location = new System.Drawing.Point(199, 184);
            this.datepicker.Name = "datepicker";
            this.datepicker.ShowUpDown = true;
            this.datepicker.Size = new System.Drawing.Size(185, 22);
            this.datepicker.TabIndex = 10;
            // 
            // duecheck
            // 
            this.duecheck.AutoSize = true;
            this.duecheck.Location = new System.Drawing.Point(232, 233);
            this.duecheck.Name = "duecheck";
            this.duecheck.Size = new System.Drawing.Size(53, 20);
            this.duecheck.TabIndex = 11;
            this.duecheck.Text = "Yes";
            this.duecheck.UseVisualStyleBackColor = true;
            // 
            // Issue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 371);
            this.Controls.Add(this.duecheck);
            this.Controls.Add(this.datepicker);
            this.Controls.Add(this.bnametxt);
            this.Controls.Add(this.cnametxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bookname);
            this.Controls.Add(this.conname);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.label1);
            this.Name = "Issue";
            this.Text = "Issue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label conname;
        private System.Windows.Forms.Label bookname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cnametxt;
        private System.Windows.Forms.TextBox bnametxt;
        private System.Windows.Forms.DateTimePicker datepicker;
        private System.Windows.Forms.CheckBox duecheck;
    }
}