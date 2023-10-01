namespace StockManagementWithXml.Forms
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.label1 = new System.Windows.Forms.Label();
            this.AddStockButton = new System.Windows.Forms.Button();
            this.AddShelveButton = new System.Windows.Forms.Button();
            this.PartTypeManagementBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userManagementButton = new System.Windows.Forms.Button();
            this.actitivitiesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(172, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(571, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "PARÇA YÖNETİMİ UYGULAMASI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AddStockButton
            // 
            this.AddStockButton.BackColor = System.Drawing.Color.White;
            this.AddStockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddStockButton.Location = new System.Drawing.Point(29, 87);
            this.AddStockButton.Name = "AddStockButton";
            this.AddStockButton.Size = new System.Drawing.Size(191, 43);
            this.AddStockButton.TabIndex = 2;
            this.AddStockButton.Text = "Parça Yönetimi";
            this.AddStockButton.UseVisualStyleBackColor = false;
            this.AddStockButton.Click += new System.EventHandler(this.AddStockButton_Click);
            // 
            // AddShelveButton
            // 
            this.AddShelveButton.BackColor = System.Drawing.Color.White;
            this.AddShelveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddShelveButton.Location = new System.Drawing.Point(29, 362);
            this.AddShelveButton.Name = "AddShelveButton";
            this.AddShelveButton.Size = new System.Drawing.Size(153, 43);
            this.AddShelveButton.TabIndex = 4;
            this.AddShelveButton.Text = "Raf Yönetimi";
            this.AddShelveButton.UseVisualStyleBackColor = false;
            this.AddShelveButton.Click += new System.EventHandler(this.AddShelveButton_Click);
            // 
            // PartTypeManagementBtn
            // 
            this.PartTypeManagementBtn.BackColor = System.Drawing.Color.White;
            this.PartTypeManagementBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartTypeManagementBtn.Location = new System.Drawing.Point(29, 293);
            this.PartTypeManagementBtn.Name = "PartTypeManagementBtn";
            this.PartTypeManagementBtn.Size = new System.Drawing.Size(235, 43);
            this.PartTypeManagementBtn.TabIndex = 5;
            this.PartTypeManagementBtn.Text = "Parça Türü Yönetimi";
            this.PartTypeManagementBtn.UseVisualStyleBackColor = false;
            this.PartTypeManagementBtn.Click += new System.EventHandler(this.PartTypeManagementBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(390, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(519, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // userManagementButton
            // 
            this.userManagementButton.BackColor = System.Drawing.Color.White;
            this.userManagementButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userManagementButton.Location = new System.Drawing.Point(29, 221);
            this.userManagementButton.Name = "userManagementButton";
            this.userManagementButton.Size = new System.Drawing.Size(191, 49);
            this.userManagementButton.TabIndex = 16;
            this.userManagementButton.Text = "Kullanıcı Yönetimi";
            this.userManagementButton.UseVisualStyleBackColor = false;
            this.userManagementButton.Click += new System.EventHandler(this.userManagementButton_Click);
            // 
            // actitivitiesButton
            // 
            this.actitivitiesButton.BackColor = System.Drawing.Color.White;
            this.actitivitiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actitivitiesButton.Location = new System.Drawing.Point(29, 149);
            this.actitivitiesButton.Name = "actitivitiesButton";
            this.actitivitiesButton.Size = new System.Drawing.Size(191, 49);
            this.actitivitiesButton.TabIndex = 17;
            this.actitivitiesButton.Text = "Hareket İzleme";
            this.actitivitiesButton.UseVisualStyleBackColor = false;
            this.actitivitiesButton.Click += new System.EventHandler(this.actitivitiesButton_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(906, 476);
            this.Controls.Add(this.actitivitiesButton);
            this.Controls.Add(this.userManagementButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AddStockButton);
            this.Controls.Add(this.PartTypeManagementBtn);
            this.Controls.Add(this.AddShelveButton);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DMRLP YAZILIM";
            this.Load += new System.EventHandler(this.MainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddStockButton;
        private System.Windows.Forms.Button AddShelveButton;
        private System.Windows.Forms.Button PartTypeManagementBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button userManagementButton;
        private System.Windows.Forms.Button actitivitiesButton;
    }
}

