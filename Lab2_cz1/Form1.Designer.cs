namespace Lab2_cz2
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
            this.pictureLoaded = new System.Windows.Forms.PictureBox();
            this.pictureEdited = new System.Windows.Forms.PictureBox();
            this.bLoadFile = new System.Windows.Forms.Button();
            this.bInsert = new System.Windows.Forms.Button();
            this.textBoxStringToHide = new System.Windows.Forms.TextBox();
            this.bReadMes = new System.Windows.Forms.Button();
            this.bSaveImg = new System.Windows.Forms.Button();
            this.textBoxAESKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyStegon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdited)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureLoaded
            // 
            this.pictureLoaded.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureLoaded.Location = new System.Drawing.Point(12, 92);
            this.pictureLoaded.Name = "pictureLoaded";
            this.pictureLoaded.Size = new System.Drawing.Size(407, 333);
            this.pictureLoaded.TabIndex = 0;
            this.pictureLoaded.TabStop = false;
            // 
            // pictureEdited
            // 
            this.pictureEdited.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureEdited.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureEdited.Location = new System.Drawing.Point(440, 92);
            this.pictureEdited.Name = "pictureEdited";
            this.pictureEdited.Size = new System.Drawing.Size(407, 333);
            this.pictureEdited.TabIndex = 1;
            this.pictureEdited.TabStop = false;
            // 
            // bLoadFile
            // 
            this.bLoadFile.Location = new System.Drawing.Point(12, 8);
            this.bLoadFile.Name = "bLoadFile";
            this.bLoadFile.Size = new System.Drawing.Size(75, 26);
            this.bLoadFile.TabIndex = 2;
            this.bLoadFile.Text = "Wczytaj plik";
            this.bLoadFile.UseVisualStyleBackColor = true;
            this.bLoadFile.Click += new System.EventHandler(this.bLoadFile_Click);
            // 
            // bInsert
            // 
            this.bInsert.Location = new System.Drawing.Point(12, 36);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(75, 24);
            this.bInsert.TabIndex = 3;
            this.bInsert.Text = "Wstaw";
            this.bInsert.UseVisualStyleBackColor = true;
            this.bInsert.Click += new System.EventHandler(this.bInsert_Click);
            // 
            // textBoxStringToHide
            // 
            this.textBoxStringToHide.Location = new System.Drawing.Point(328, 14);
            this.textBoxStringToHide.Name = "textBoxStringToHide";
            this.textBoxStringToHide.Size = new System.Drawing.Size(519, 20);
            this.textBoxStringToHide.TabIndex = 4;
            // 
            // bReadMes
            // 
            this.bReadMes.Location = new System.Drawing.Point(93, 36);
            this.bReadMes.Name = "bReadMes";
            this.bReadMes.Size = new System.Drawing.Size(75, 24);
            this.bReadMes.TabIndex = 5;
            this.bReadMes.Text = "Odczytaj";
            this.bReadMes.UseVisualStyleBackColor = true;
            this.bReadMes.Click += new System.EventHandler(this.bReadMes_Click);
            // 
            // bSaveImg
            // 
            this.bSaveImg.Location = new System.Drawing.Point(93, 8);
            this.bSaveImg.Name = "bSaveImg";
            this.bSaveImg.Size = new System.Drawing.Size(75, 26);
            this.bSaveImg.TabIndex = 6;
            this.bSaveImg.Text = "Zapisz";
            this.bSaveImg.UseVisualStyleBackColor = true;
            this.bSaveImg.Click += new System.EventHandler(this.bSaveImg_Click);
            // 
            // textBoxAESKey
            // 
            this.textBoxAESKey.Location = new System.Drawing.Point(328, 40);
            this.textBoxAESKey.Name = "textBoxAESKey";
            this.textBoxAESKey.Size = new System.Drawing.Size(519, 20);
            this.textBoxAESKey.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tekst do osadzenia";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Haslo";
            // 
            // textBoxKeyStegon
            // 
            this.textBoxKeyStegon.Location = new System.Drawing.Point(328, 66);
            this.textBoxKeyStegon.Name = "textBoxKeyStegon";
            this.textBoxKeyStegon.Size = new System.Drawing.Size(519, 20);
            this.textBoxKeyStegon.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Klucz stegonograficzny";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 63);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(156, 25);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Wyczysc";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 428);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxKeyStegon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAESKey);
            this.Controls.Add(this.bSaveImg);
            this.Controls.Add(this.bReadMes);
            this.Controls.Add(this.textBoxStringToHide);
            this.Controls.Add(this.bInsert);
            this.Controls.Add(this.bLoadFile);
            this.Controls.Add(this.pictureEdited);
            this.Controls.Add(this.pictureLoaded);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdited)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureLoaded;
        private System.Windows.Forms.PictureBox pictureEdited;
        private System.Windows.Forms.Button bLoadFile;
        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.TextBox textBoxStringToHide;
        private System.Windows.Forms.Button bReadMes;
        private System.Windows.Forms.Button bSaveImg;
        private System.Windows.Forms.TextBox textBoxAESKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxKeyStegon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClear;
    }
}

