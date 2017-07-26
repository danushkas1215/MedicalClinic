namespace MedicalClinic
{
    partial class PharmaceuticalCom
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
            this.txtComName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComAddress = new System.Windows.Forms.TextBox();
            this.txtComPhone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtComEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewRep = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRep)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Name";
            // 
            // txtComName
            // 
            this.txtComName.Location = new System.Drawing.Point(272, 32);
            this.txtComName.Name = "txtComName";
            this.txtComName.Size = new System.Drawing.Size(518, 22);
            this.txtComName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Company Address";
            // 
            // txtComAddress
            // 
            this.txtComAddress.Location = new System.Drawing.Point(272, 77);
            this.txtComAddress.Multiline = true;
            this.txtComAddress.Name = "txtComAddress";
            this.txtComAddress.Size = new System.Drawing.Size(518, 77);
            this.txtComAddress.TabIndex = 3;
            // 
            // txtComPhone
            // 
            this.txtComPhone.Location = new System.Drawing.Point(272, 180);
            this.txtComPhone.Name = "txtComPhone";
            this.txtComPhone.Size = new System.Drawing.Size(518, 22);
            this.txtComPhone.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Company Phone";
            // 
            // txtComEmail
            // 
            this.txtComEmail.Location = new System.Drawing.Point(272, 226);
            this.txtComEmail.Name = "txtComEmail";
            this.txtComEmail.Size = new System.Drawing.Size(518, 22);
            this.txtComEmail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Company Email";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(288, 448);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(369, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRep
            // 
            this.dataGridViewRep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRep.Location = new System.Drawing.Point(272, 273);
            this.dataGridViewRep.Name = "dataGridViewRep";
            this.dataGridViewRep.RowTemplate.Height = 24;
            this.dataGridViewRep.Size = new System.Drawing.Size(518, 150);
            this.dataGridViewRep.TabIndex = 10;
            // 
            // PharmaceuticalCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 677);
            this.Controls.Add(this.dataGridViewRep);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtComEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtComPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtComAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComName);
            this.Controls.Add(this.label1);
            this.Name = "PharmaceuticalCom";
            this.Text = "PharmaceuticalCom";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtComAddress;
        private System.Windows.Forms.TextBox txtComPhone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtComEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridViewRep;
    }
}