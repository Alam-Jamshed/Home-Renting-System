namespace PROJECT___IN_PROGRESS
{
    partial class HouseDash
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewHouseDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EditHouseDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RemoveProperty = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ViewHouseDetail,
            this.EditHouseDetail,
            this.RemoveProperty});
            this.dataGridView1.Location = new System.Drawing.Point(0, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(677, 500);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 80F;
            this.Column1.HeaderText = "Serial No";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // ViewHouseDetail
            // 
            this.ViewHouseDetail.HeaderText = "";
            this.ViewHouseDetail.Name = "ViewHouseDetail";
            this.ViewHouseDetail.ReadOnly = true;
            this.ViewHouseDetail.Text = "View Detail";
            this.ViewHouseDetail.UseColumnTextForButtonValue = true;
            // 
            // EditHouseDetail
            // 
            this.EditHouseDetail.HeaderText = "";
            this.EditHouseDetail.Name = "EditHouseDetail";
            this.EditHouseDetail.ReadOnly = true;
            this.EditHouseDetail.Text = "Edit Detail";
            this.EditHouseDetail.UseColumnTextForButtonValue = true;
            // 
            // RemoveProperty
            // 
            this.RemoveProperty.HeaderText = "";
            this.RemoveProperty.Name = "RemoveProperty";
            this.RemoveProperty.ReadOnly = true;
            this.RemoveProperty.Text = "Remove Property";
            this.RemoveProperty.UseColumnTextForButtonValue = true;
            // 
            // HouseDash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(67)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(677, 500);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(752, 247);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HouseDash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HouseDash";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn ViewHouseDetail;
        private System.Windows.Forms.DataGridViewButtonColumn EditHouseDetail;
        private System.Windows.Forms.DataGridViewButtonColumn RemoveProperty;
    }
}