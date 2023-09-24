namespace MapToolGUI
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
            this.SelectMap = new System.Windows.Forms.Button();
            this.MapFilelabel = new System.Windows.Forms.Label();
            this.MapFile = new System.Windows.Forms.TextBox();
            this.AddProtection = new System.Windows.Forms.Button();
            this.SpamAmount = new System.Windows.Forms.ComboBox();
            this.entspamlabel = new System.Windows.Forms.Label();
            this.REProtect = new System.Windows.Forms.CheckBox();
            this.deployprotect = new System.Windows.Forms.CheckBox();
            this.protectionslabel = new System.Windows.Forms.Label();
            this.EditProtect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SelectMap
            // 
            this.SelectMap.Location = new System.Drawing.Point(64, 35);
            this.SelectMap.Margin = new System.Windows.Forms.Padding(2);
            this.SelectMap.Name = "SelectMap";
            this.SelectMap.Size = new System.Drawing.Size(202, 21);
            this.SelectMap.TabIndex = 0;
            this.SelectMap.Text = "Select";
            this.SelectMap.UseVisualStyleBackColor = true;
            this.SelectMap.Click += new System.EventHandler(this.SelectMap_Click);
            // 
            // MapFilelabel
            // 
            this.MapFilelabel.AutoSize = true;
            this.MapFilelabel.Location = new System.Drawing.Point(13, 15);
            this.MapFilelabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MapFilelabel.Name = "MapFilelabel";
            this.MapFilelabel.Size = new System.Drawing.Size(47, 13);
            this.MapFilelabel.TabIndex = 1;
            this.MapFilelabel.Text = "MapFile:";
            // 
            // MapFile
            // 
            this.MapFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MapFile.Location = new System.Drawing.Point(64, 12);
            this.MapFile.Margin = new System.Windows.Forms.Padding(2);
            this.MapFile.Name = "MapFile";
            this.MapFile.ReadOnly = true;
            this.MapFile.Size = new System.Drawing.Size(202, 20);
            this.MapFile.TabIndex = 2;
            // 
            // AddProtection
            // 
            this.AddProtection.Enabled = false;
            this.AddProtection.Location = new System.Drawing.Point(16, 187);
            this.AddProtection.Margin = new System.Windows.Forms.Padding(2);
            this.AddProtection.Name = "AddProtection";
            this.AddProtection.Size = new System.Drawing.Size(252, 35);
            this.AddProtection.TabIndex = 3;
            this.AddProtection.Text = "Add Protection";
            this.AddProtection.UseVisualStyleBackColor = true;
            this.AddProtection.Click += new System.EventHandler(this.AddProtection_Click);
            // 
            // SpamAmount
            // 
            this.SpamAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpamAmount.FormattingEnabled = true;
            this.SpamAmount.Items.AddRange(new object[] {
            "0",
            "100",
            "1000",
            "5000",
            "10000",
            "20000",
            "30000",
            "50000",
            "80000",
            "100000"});
            this.SpamAmount.Location = new System.Drawing.Point(97, 160);
            this.SpamAmount.Margin = new System.Windows.Forms.Padding(2);
            this.SpamAmount.Name = "SpamAmount";
            this.SpamAmount.Size = new System.Drawing.Size(169, 21);
            this.SpamAmount.TabIndex = 4;
            // 
            // entspamlabel
            // 
            this.entspamlabel.AutoSize = true;
            this.entspamlabel.Location = new System.Drawing.Point(27, 160);
            this.entspamlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.entspamlabel.Name = "entspamlabel";
            this.entspamlabel.Size = new System.Drawing.Size(66, 13);
            this.entspamlabel.TabIndex = 5;
            this.entspamlabel.Text = "Entity Spam:";
            // 
            // REProtect
            // 
            this.REProtect.AutoSize = true;
            this.REProtect.Checked = true;
            this.REProtect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.REProtect.Location = new System.Drawing.Point(30, 89);
            this.REProtect.Name = "REProtect";
            this.REProtect.Size = new System.Drawing.Size(236, 17);
            this.REProtect.TabIndex = 6;
            this.REProtect.Text = "Protect RustEditData (IO/Loot/NPCs/Paths)";
            this.REProtect.UseVisualStyleBackColor = true;
            // 
            // deployprotect
            // 
            this.deployprotect.AutoSize = true;
            this.deployprotect.Checked = true;
            this.deployprotect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deployprotect.Location = new System.Drawing.Point(30, 112);
            this.deployprotect.Name = "deployprotect";
            this.deployprotect.Size = new System.Drawing.Size(212, 17);
            this.deployprotect.TabIndex = 7;
            this.deployprotect.Text = "Protect Deployables, Spawners, Entites";
            this.deployprotect.UseVisualStyleBackColor = true;
            // 
            // protectionslabel
            // 
            this.protectionslabel.AutoSize = true;
            this.protectionslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protectionslabel.Location = new System.Drawing.Point(27, 62);
            this.protectionslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.protectionslabel.Name = "protectionslabel";
            this.protectionslabel.Size = new System.Drawing.Size(89, 16);
            this.protectionslabel.TabIndex = 8;
            this.protectionslabel.Text = "Protections:";
            // 
            // EditProtect
            // 
            this.EditProtect.AutoSize = true;
            this.EditProtect.Checked = true;
            this.EditProtect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditProtect.Location = new System.Drawing.Point(30, 135);
            this.EditProtect.Name = "EditProtect";
            this.EditProtect.Size = new System.Drawing.Size(174, 17);
            this.EditProtect.TabIndex = 10;
            this.EditProtect.Text = "Protect Against Editors/Servers";
            this.EditProtect.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 229);
            this.Controls.Add(this.EditProtect);
            this.Controls.Add(this.protectionslabel);
            this.Controls.Add(this.deployprotect);
            this.Controls.Add(this.REProtect);
            this.Controls.Add(this.entspamlabel);
            this.Controls.Add(this.SpamAmount);
            this.Controls.Add(this.AddProtection);
            this.Controls.Add(this.MapFile);
            this.Controls.Add(this.MapFilelabel);
            this.Controls.Add(this.SelectMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MapProtection Tool 1.0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectMap;
        private System.Windows.Forms.Label MapFilelabel;
        private System.Windows.Forms.TextBox MapFile;
        private System.Windows.Forms.Button AddProtection;
        private System.Windows.Forms.ComboBox SpamAmount;
        private System.Windows.Forms.Label entspamlabel;
        private System.Windows.Forms.CheckBox REProtect;
        private System.Windows.Forms.CheckBox deployprotect;
        private System.Windows.Forms.Label protectionslabel;
        private System.Windows.Forms.CheckBox EditProtect;
    }
}

