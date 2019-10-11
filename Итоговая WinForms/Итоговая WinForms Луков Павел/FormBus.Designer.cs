namespace Итоговая_WinForms_Луков_Павел
{
    partial class FormBus
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
            this.components = new System.ComponentModel.Container();
            this.LNumber = new System.Windows.Forms.Label();
            this.LBrand = new System.Windows.Forms.Label();
            this.LYearIssue = new System.Windows.Forms.Label();
            this.LStateNumber = new System.Windows.Forms.Label();
            this.TBNumber = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.busBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.brandBusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.busBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brandBusBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LNumber
            // 
            this.LNumber.AutoSize = true;
            this.LNumber.Location = new System.Drawing.Point(13, 9);
            this.LNumber.Name = "LNumber";
            this.LNumber.Size = new System.Drawing.Size(47, 13);
            this.LNumber.TabIndex = 0;
            this.LNumber.Text = "Номер: ";
            // 
            // LBrand
            // 
            this.LBrand.AutoSize = true;
            this.LBrand.Location = new System.Drawing.Point(13, 35);
            this.LBrand.Name = "LBrand";
            this.LBrand.Size = new System.Drawing.Size(46, 13);
            this.LBrand.TabIndex = 1;
            this.LBrand.Text = "Марка: ";
            // 
            // LYearIssue
            // 
            this.LYearIssue.AutoSize = true;
            this.LYearIssue.Location = new System.Drawing.Point(12, 85);
            this.LYearIssue.Name = "LYearIssue";
            this.LYearIssue.Size = new System.Drawing.Size(77, 13);
            this.LYearIssue.TabIndex = 2;
            this.LYearIssue.Text = "Год выпуска: ";
            // 
            // LStateNumber
            // 
            this.LStateNumber.AutoSize = true;
            this.LStateNumber.Location = new System.Drawing.Point(12, 59);
            this.LStateNumber.Name = "LStateNumber";
            this.LStateNumber.Size = new System.Drawing.Size(69, 13);
            this.LStateNumber.TabIndex = 3;
            this.LStateNumber.Text = "Гос. номер: ";
            // 
            // TBNumber
            // 
            this.TBNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.busBindingSource, "Id", true));
            this.TBNumber.Location = new System.Drawing.Point(93, 6);
            this.TBNumber.Name = "TBNumber";
            this.TBNumber.ReadOnly = true;
            this.TBNumber.Size = new System.Drawing.Size(100, 20);
            this.TBNumber.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.brandBusBindingSource, "Title", true));
            this.textBox2.Location = new System.Drawing.Point(93, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.busBindingSource, "StateNumber", true));
            this.textBox3.Location = new System.Drawing.Point(93, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.busBindingSource, "YearIssue", true));
            this.textBox4.Location = new System.Drawing.Point(93, 82);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Location = new System.Drawing.Point(37, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(266, 241);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 270);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(305, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 241);
            this.button2.TabIndex = 10;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 241);
            this.button1.TabIndex = 9;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.toolStripMenuItem1.Text = "Изменить";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "|*.png;*.bmp;*.jpg";
            this.openFileDialog1.InitialDirectory = "D:\\";
            // 
            // busBindingSource
            // 
            this.busBindingSource.DataSource = typeof(Итоговая_WinForms_Луков_Павел.Models.Bus);
            // 
            // brandBusBindingSource
            // 
            this.brandBusBindingSource.DataSource = typeof(Итоговая_WinForms_Луков_Павел.Models.BrandBus);
            // 
            // FormBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 382);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.TBNumber);
            this.Controls.Add(this.LStateNumber);
            this.Controls.Add(this.LYearIssue);
            this.Controls.Add(this.LBrand);
            this.Controls.Add(this.LNumber);
            this.MaximizeBox = false;
            this.Name = "FormBus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автобус";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.busBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brandBusBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LNumber;
        private System.Windows.Forms.Label LBrand;
        private System.Windows.Forms.Label LYearIssue;
        private System.Windows.Forms.Label LStateNumber;
        private System.Windows.Forms.TextBox TBNumber;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.BindingSource busBindingSource;
        private System.Windows.Forms.BindingSource brandBusBindingSource;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}