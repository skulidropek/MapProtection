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
            this.SelectMap.Location = new System.Drawing.Point(128, 67);
            this.SelectMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelectMap.Name = "SelectMap";
            this.SelectMap.Size = new System.Drawing.Size(404, 40);
            this.SelectMap.TabIndex = 0;
            this.SelectMap.Text = "Select";
            this.SelectMap.UseVisualStyleBackColor = true;
            this.SelectMap.Click += new System.EventHandler(this.SelectMap_Click);
            // 
            // MapFilelabel
            // 
            this.MapFilelabel.AutoSize = true;
            this.MapFilelabel.Location = new System.Drawing.Point(26, 29);
            this.MapFilelabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MapFilelabel.Name = "MapFilelabel";
            this.MapFilelabel.Size = new System.Drawing.Size(95, 25);
            this.MapFilelabel.TabIndex = 1;
            this.MapFilelabel.Text = "MapFile:";
            // 
            // MapFile
            // 
            this.MapFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MapFile.Location = new System.Drawing.Point(128, 23);
            this.MapFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MapFile.Name = "MapFile";
            this.MapFile.ReadOnly = true;
            this.MapFile.Size = new System.Drawing.Size(400, 31);
            this.MapFile.TabIndex = 2;
            // 
            // AddProtection
            // 
            this.AddProtection.Enabled = false;
            this.AddProtection.Location = new System.Drawing.Point(32, 360);
            this.AddProtection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddProtection.Name = "AddProtection";
            this.AddProtection.Size = new System.Drawing.Size(504, 67);
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
            this.SpamAmount.Location = new System.Drawing.Point(194, 308);
            this.SpamAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SpamAmount.Name = "SpamAmount";
            this.SpamAmount.Size = new System.Drawing.Size(334, 33);
            this.SpamAmount.TabIndex = 4;
            this.SpamAmount.SelectedIndexChanged += new System.EventHandler(this.SpamAmount_SelectedIndexChanged);
            // 
            // entspamlabel
            // 
            this.entspamlabel.AutoSize = true;
            this.entspamlabel.Location = new System.Drawing.Point(54, 308);
            this.entspamlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.entspamlabel.Name = "entspamlabel";
            this.entspamlabel.Size = new System.Drawing.Size(133, 25);
            this.entspamlabel.TabIndex = 5;
            this.entspamlabel.Text = "Entity Spam:";
            // 
            // REProtect
            // 
            this.REProtect.AutoSize = true;
            this.REProtect.Checked = true;
            this.REProtect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.REProtect.Location = new System.Drawing.Point(60, 171);
            this.REProtect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.REProtect.Name = "REProtect";
            this.REProtect.Size = new System.Drawing.Size(455, 29);
            this.REProtect.TabIndex = 6;
            this.REProtect.Text = "Protect RustEditData (IO/Loot/NPCs/Paths)";
            this.REProtect.UseVisualStyleBackColor = true;
            // 
            // deployprotect
            // 
            this.deployprotect.AutoSize = true;
            this.deployprotect.Checked = true;
            this.deployprotect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deployprotect.Location = new System.Drawing.Point(60, 215);
            this.deployprotect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.deployprotect.Name = "deployprotect";
            this.deployprotect.Size = new System.Drawing.Size(422, 29);
            this.deployprotect.TabIndex = 7;
            this.deployprotect.Text = "Protect Deployables, Spawners, Entites";
            this.deployprotect.UseVisualStyleBackColor = true;
            // 
            // protectionslabel
            // 
            this.protectionslabel.AutoSize = true;
            this.protectionslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protectionslabel.Location = new System.Drawing.Point(54, 119);
            this.protectionslabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.protectionslabel.Name = "protectionslabel";
            this.protectionslabel.Size = new System.Drawing.Size(160, 30);
            this.protectionslabel.TabIndex = 8;
            this.protectionslabel.Text = "Protections:";
            // 
            // EditProtect
            // 
            this.EditProtect.AutoSize = true;
            this.EditProtect.Checked = true;
            this.EditProtect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditProtect.Location = new System.Drawing.Point(60, 260);
            this.EditProtect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.EditProtect.Name = "EditProtect";
            this.EditProtect.Size = new System.Drawing.Size(343, 29);
            this.EditProtect.TabIndex = 10;
            this.EditProtect.Text = "Protect Against Editors/Servers";
            this.EditProtect.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 435);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MapProtection Tool 1.0.2";
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

