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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.patientsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.viewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doctorViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nursingViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicineOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientToolStripMenuItem,
            this.medicinesToolStripMenuItem,
            this.viewsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientToolStripMenuItem1,
            this.patientsListToolStripMenuItem,
            this.medicalRecordToolStripMenuItem});
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            this.patientToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.patientToolStripMenuItem.Text = "Patient Record";
            // 
            // patientToolStripMenuItem1
            // 
            this.patientToolStripMenuItem1.Name = "patientToolStripMenuItem1";
            this.patientToolStripMenuItem1.Size = new System.Drawing.Size(188, 26);
            this.patientToolStripMenuItem1.Text = "Patient";
            this.patientToolStripMenuItem1.Click += new System.EventHandler(this.patientToolStripMenuItem1_Click);
            // 
            // patientsListToolStripMenuItem
            // 
            this.patientsListToolStripMenuItem.Name = "patientsListToolStripMenuItem";
            this.patientsListToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.patientsListToolStripMenuItem.Text = "Patients List";
            this.patientsListToolStripMenuItem.Click += new System.EventHandler(this.patientsListToolStripMenuItem_Click);
            // 
            // medicalRecordToolStripMenuItem
            // 
            this.medicalRecordToolStripMenuItem.Name = "medicalRecordToolStripMenuItem";
            this.medicalRecordToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.medicalRecordToolStripMenuItem.Text = "Medical Record";
            this.medicalRecordToolStripMenuItem.Visible = false;
            this.medicalRecordToolStripMenuItem.Click += new System.EventHandler(this.medicalRecordToolStripMenuItem_Click);
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
            this.medicineToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.medicineToolStripMenuItem.Text = "Medicine";
            this.medicineToolStripMenuItem.Click += new System.EventHandler(this.medicineToolStripMenuItem_Click);
            // 
            // medicineListToolStripMenuItem
            // 
            this.medicineListToolStripMenuItem.Name = "medicineListToolStripMenuItem";
            this.medicineListToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.medicineListToolStripMenuItem.Text = "Medicine List";
            this.medicineListToolStripMenuItem.Click += new System.EventHandler(this.medicineListToolStripMenuItem_Click);
            // 
            // viewsToolStripMenuItem
            // 
            this.viewsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doctorViewToolStripMenuItem,
            this.nursingViewToolStripMenuItem});
            this.viewsToolStripMenuItem.Name = "viewsToolStripMenuItem";
            this.viewsToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.viewsToolStripMenuItem.Text = "Views";
            // 
            // doctorViewToolStripMenuItem
            // 
            this.doctorViewToolStripMenuItem.Name = "doctorViewToolStripMenuItem";
            this.doctorViewToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.doctorViewToolStripMenuItem.Text = "Doctor View";
            this.doctorViewToolStripMenuItem.Click += new System.EventHandler(this.doctorViewToolStripMenuItem_Click);
            // 
            // nursingViewToolStripMenuItem
            // 
            this.nursingViewToolStripMenuItem.Name = "nursingViewToolStripMenuItem";
            this.nursingViewToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.nursingViewToolStripMenuItem.Text = "Nursing View";
            this.nursingViewToolStripMenuItem.Click += new System.EventHandler(this.nursingViewToolStripMenuItem_Click);
            // 
            // medicineOrderToolStripMenuItem
            // 
            this.medicineOrderToolStripMenuItem.Name = "medicineOrderToolStripMenuItem";
            this.medicineOrderToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 503);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.ToolStripMenuItem medicineOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem medicalRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientsListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doctorViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nursingViewToolStripMenuItem;
    }
}

