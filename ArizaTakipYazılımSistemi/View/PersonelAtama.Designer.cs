namespace ArizaTakipYazılımSistemi.View
{
    partial class PersonelAtama
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.arizaTakipDataSet1 = new ArizaTakipYazılımSistemi.arizaTakipDataSet1();
            this.arizaTakipDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.arizaTakipDataSet = new ArizaTakipYazılımSistemi.arizaTakipDataSet();
            this.arizaTakipDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ariza numarası:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(96, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ariza başlık:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(96, 42);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(219, 68);
            this.textBox2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Arıza konusu:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(96, 120);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(219, 144);
            this.textBox3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Personel seçiniz:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(96, 274);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // arizaTakipDataSet1
            // 
            this.arizaTakipDataSet1.DataSetName = "arizaTakipDataSet1";
            this.arizaTakipDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // arizaTakipDataSet1BindingSource
            // 
            this.arizaTakipDataSet1BindingSource.DataSource = this.arizaTakipDataSet1;
            this.arizaTakipDataSet1BindingSource.Position = 0;
            // 
            // arizaTakipDataSet
            // 
            this.arizaTakipDataSet.DataSetName = "arizaTakipDataSet";
            this.arizaTakipDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // arizaTakipDataSetBindingSource
            // 
            this.arizaTakipDataSetBindingSource.DataSource = this.arizaTakipDataSet;
            this.arizaTakipDataSetBindingSource.Position = 0;
            // 
            // PersonelAtama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 335);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "PersonelAtama";
            this.Text = "PersonelAtama";
            this.Load += new System.EventHandler(this.PersonelAtama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arizaTakipDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private arizaTakipDataSet1 arizaTakipDataSet1;
        private System.Windows.Forms.BindingSource arizaTakipDataSet1BindingSource;
        private arizaTakipDataSet arizaTakipDataSet;
        private System.Windows.Forms.BindingSource arizaTakipDataSetBindingSource;
    }
}