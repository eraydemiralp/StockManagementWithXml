namespace StockManagementWithXml.Forms
{
    partial class PartTypeManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartTypeManagement));
            this.partTypeTextError = new System.Windows.Forms.ErrorProvider(this.components);
            this.partTypeDataGridView = new System.Windows.Forms.DataGridView();
            this.addPartTypeButton = new System.Windows.Forms.Button();
            this.partTypeTextBox = new System.Windows.Forms.TextBox();
            this.partTypeLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.partTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.partTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partTypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.partTypeTextError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partTypeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.partTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // partTypeTextError
            // 
            this.partTypeTextError.ContainerControl = this;
            // 
            // partTypeDataGridView
            // 
            this.partTypeDataGridView.AllowUserToAddRows = false;
            this.partTypeDataGridView.AllowUserToDeleteRows = false;
            this.partTypeDataGridView.AutoGenerateColumns = false;
            this.partTypeDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.partTypeDataGridView.BackgroundColor = System.Drawing.Color.DarkTurquoise;
            this.partTypeDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.partTypeDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.partTypeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.partTypeDataGridView.ColumnHeadersHeight = 35;
            this.partTypeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.partTypeIdDataGridViewTextBoxColumn,
            this.partTypeNameDataGridViewTextBoxColumn});
            this.partTypeDataGridView.DataSource = this.partTypeBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Coral;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.partTypeDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.partTypeDataGridView.Location = new System.Drawing.Point(403, 31);
            this.partTypeDataGridView.Name = "partTypeDataGridView";
            this.partTypeDataGridView.ReadOnly = true;
            this.partTypeDataGridView.Size = new System.Drawing.Size(359, 420);
            this.partTypeDataGridView.TabIndex = 10;
            this.partTypeDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.partTypeDataGridView_CellContentClick);
            // 
            // addPartTypeButton
            // 
            this.addPartTypeButton.BackColor = System.Drawing.Color.Lime;
            this.addPartTypeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPartTypeButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.addPartTypeButton.Location = new System.Drawing.Point(166, 81);
            this.addPartTypeButton.Name = "addPartTypeButton";
            this.addPartTypeButton.Size = new System.Drawing.Size(175, 53);
            this.addPartTypeButton.TabIndex = 10;
            this.addPartTypeButton.Text = "Ekle";
            this.addPartTypeButton.UseVisualStyleBackColor = false;
            this.addPartTypeButton.Click += new System.EventHandler(this.addPartTypeButton_Click);
            // 
            // partTypeTextBox
            // 
            this.partTypeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partTypeTextBox.Location = new System.Drawing.Point(166, 26);
            this.partTypeTextBox.Name = "partTypeTextBox";
            this.partTypeTextBox.Size = new System.Drawing.Size(215, 29);
            this.partTypeTextBox.TabIndex = 9;
            // 
            // partTypeLabel
            // 
            this.partTypeLabel.AutoSize = true;
            this.partTypeLabel.BackColor = System.Drawing.Color.BurlyWood;
            this.partTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partTypeLabel.Location = new System.Drawing.Point(22, 31);
            this.partTypeLabel.Name = "partTypeLabel";
            this.partTypeLabel.Size = new System.Drawing.Size(113, 24);
            this.partTypeLabel.TabIndex = 2;
            this.partTypeLabel.Text = "Parça Türü";
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Red;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.deleteButton.Location = new System.Drawing.Point(166, 140);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(175, 54);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Sil";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(752, 21);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(10, 20);
            this.idTextBox.TabIndex = 13;
            this.idTextBox.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 306);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 214);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.updateButton.Location = new System.Drawing.Point(166, 200);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(175, 48);
            this.updateButton.TabIndex = 21;
            this.updateButton.Text = "Güncelle";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // partTypeBindingSource
            // 
            this.partTypeBindingSource.DataSource = typeof(StockManagementWithXml.Model.PartType);
            // 
            // partTypeIdDataGridViewTextBoxColumn
            // 
            this.partTypeIdDataGridViewTextBoxColumn.DataPropertyName = "PartTypeId";
            this.partTypeIdDataGridViewTextBoxColumn.HeaderText = "PartTypeId";
            this.partTypeIdDataGridViewTextBoxColumn.Name = "partTypeIdDataGridViewTextBoxColumn";
            this.partTypeIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.partTypeIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // partTypeNameDataGridViewTextBoxColumn
            // 
            this.partTypeNameDataGridViewTextBoxColumn.DataPropertyName = "PartTypeName";
            this.partTypeNameDataGridViewTextBoxColumn.HeaderText = "Parça Türü";
            this.partTypeNameDataGridViewTextBoxColumn.Name = "partTypeNameDataGridViewTextBoxColumn";
            this.partTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // PartTypeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(787, 520);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.partTypeDataGridView);
            this.Controls.Add(this.addPartTypeButton);
            this.Controls.Add(this.partTypeTextBox);
            this.Controls.Add(this.partTypeLabel);
            this.MaximizeBox = false;
            this.Name = "PartTypeManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parça Türü Yönetimi";
            this.Load += new System.EventHandler(this.PartTypeManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.partTypeTextError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partTypeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.partTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider partTypeTextError;
        private System.Windows.Forms.DataGridView partTypeDataGridView;
        private System.Windows.Forms.Button addPartTypeButton;
        private System.Windows.Forms.TextBox partTypeTextBox;
        private System.Windows.Forms.Label partTypeLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.BindingSource partTypeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn partTypeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partTypeNameDataGridViewTextBoxColumn;
    }
}