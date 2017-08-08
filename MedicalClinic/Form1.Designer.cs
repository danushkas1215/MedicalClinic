namespace MedicalClinic
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.companyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericNamesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.genericNamesListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.medicinesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.medicineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicineListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pToolStripMenuItem,
            this.medicinesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.pToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.pToolStripMenuItem.Text = "Patients";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // medicinesToolStripMenuItem
            // 
            this.medicinesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.genericNamesToolStripMenuItem,
            this.medicinesToolStripMenuItem1});
            this.medicinesToolStripMenuItem.Name = "medicinesToolStripMenuItem";
            this.medicinesToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.medicinesToolStripMenuItem.Text = "Medicines";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyToolStripMenuItem,
            this.companyListToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem1.Text = "Companies";
            // 
            // companyToolStripMenuItem
            // 
            this.companyToolStripMenuItem.Name = "companyToolStripMenuItem";
            this.companyToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.companyToolStripMenuItem.Text = "Company";
            this.companyToolStripMenuItem.Click += new System.EventHandler(this.companyToolStripMenuItem_Click_1);
            // 
            // companyListToolStripMenuItem
            // 
            this.companyListToolStripMenuItem.Name = "companyListToolStripMenuItem";
            this.companyListToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.companyListToolStripMenuItem.Text = "Company List";
            this.companyListToolStripMenuItem.Click += new System.EventHandler(this.companyListToolStripMenuItem_Click);
            // 
            // genericNamesToolStripMenuItem
            // 
            this.genericNamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genericNamesToolStripMenuItem1,
            this.genericNamesListToolStripMenuItem1});
            this.genericNamesToolStripMenuItem.Name = "genericNamesToolStripMenuItem";
            this.genericNamesToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.genericNamesToolStripMenuItem.Text = "Generics";
            // 
            // genericNamesToolStripMenuItem1
            // 
            this.genericNamesToolStripMenuItem1.Name = "genericNamesToolStripMenuItem1";
            this.genericNamesToolStripMenuItem1.Size = new System.Drawing.Size(210, 26);
            this.genericNamesToolStripMenuItem1.Text = "Generic Names";
            this.genericNamesToolStripMenuItem1.Click += new System.EventHandler(this.genericNamesToolStripMenuItem1_Click);
            // 
            // genericNamesListToolStripMenuItem1
            // 
            this.genericNamesListToolStripMenuItem1.Name = "genericNamesListToolStripMenuItem1";
            this.genericNamesListToolStripMenuItem1.Size = new System.Drawing.Size(210, 26);
            this.genericNamesListToolStripMenuItem1.Text = "Generic Names List";
            this.genericNamesListToolStripMenuItem1.Click += new System.EventHandler(this.genericNamesListToolStripMenuItem1_Click);
            // 
            // medicinesToolStripMenuItem1
            // 
            this.medicinesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medicineToolStripMenuItem,
            this.medicineListToolStripMenuItem});
            this.medicinesToolStripMenuItem1.Name = "medicinesToolStripMenuItem1";
            this.medicinesToolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.medicinesToolStripMenuItem1.Text = "Medicines";
            // 
            // medicineToolStripMenuItem
            // 
            this.medicineToolStripMenuItem.Name = "medicineToolStripMenuItem";
            this.medicineToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.medicineToolStripMenuItem.Text = "Medicine";
            this.medicineToolStripMenuItem.Click += new System.EventHandler(this.medicineToolStripMenuItem_Click);
            // 
            // medicineListToolStripMenuItem
            // 
            this.medicineListToolStripMenuItem.Name = "medicineListToolStripMenuItem";
            this.medicineListToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.medicineListToolStripMenuItem.Text = "Medicine List";
            this.medicineListToolStripMenuItem.Click += new System.EventHandler(this.medicineListToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 503);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericNamesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem genericNamesListToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem companyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem companyListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicinesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem medicineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicineListToolStripMenuItem;
    }
}

