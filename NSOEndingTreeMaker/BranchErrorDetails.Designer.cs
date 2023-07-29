namespace NSOEndingTreeMaker
{
    partial class BranchErrorDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchErrorDetails));
            this.ErrorIcon = new System.Windows.Forms.PictureBox();
            this.ErrorIntro = new System.Windows.Forms.Label();
            this.ErrorConfirm = new System.Windows.Forms.Button();
            this.ErrorList = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionOrBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorList)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorIcon
            // 
            this.ErrorIcon.Location = new System.Drawing.Point(12, 19);
            this.ErrorIcon.Name = "ErrorIcon";
            this.ErrorIcon.Size = new System.Drawing.Size(32, 32);
            this.ErrorIcon.TabIndex = 0;
            this.ErrorIcon.TabStop = false;
            // 
            // ErrorIntro
            // 
            this.ErrorIntro.Location = new System.Drawing.Point(59, 19);
            this.ErrorIntro.Name = "ErrorIntro";
            this.ErrorIntro.Size = new System.Drawing.Size(339, 23);
            this.ErrorIntro.TabIndex = 1;
            this.ErrorIntro.Text = "Could not save ending branch. More details are found below.";
            // 
            // ErrorConfirm
            // 
            this.ErrorConfirm.Location = new System.Drawing.Point(729, 377);
            this.ErrorConfirm.Name = "ErrorConfirm";
            this.ErrorConfirm.Size = new System.Drawing.Size(75, 23);
            this.ErrorConfirm.TabIndex = 3;
            this.ErrorConfirm.Text = "OK";
            this.ErrorConfirm.UseVisualStyleBackColor = true;
            this.ErrorConfirm.Click += new System.EventHandler(this.ErrorConfirm_Click);
            // 
            // ErrorList
            // 
            this.ErrorList.AllowUserToAddRows = false;
            this.ErrorList.AllowUserToDeleteRows = false;
            this.ErrorList.AllowUserToResizeRows = false;
            this.ErrorList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ErrorList.BackgroundColor = System.Drawing.Color.White;
            this.ErrorList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.ActionOrBranch,
            this.ErrorMessage});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1, 1, 1, 10);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorList.DefaultCellStyle = dataGridViewCellStyle4;
            this.ErrorList.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.ErrorList.Location = new System.Drawing.Point(12, 67);
            this.ErrorList.Name = "ErrorList";
            this.ErrorList.ReadOnly = true;
            this.ErrorList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ErrorList.RowHeadersVisible = false;
            this.ErrorList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ErrorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ErrorList.Size = new System.Drawing.Size(792, 304);
            this.ErrorList.TabIndex = 4;
            this.ErrorList.Leave += new System.EventHandler(this.ErrorList_Leave);
            // 
            // Type
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Type.DefaultCellStyle = dataGridViewCellStyle1;
            this.Type.HeaderText = "Branch";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 150;
            // 
            // ActionOrBranch
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ActionOrBranch.DefaultCellStyle = dataGridViewCellStyle2;
            this.ActionOrBranch.HeaderText = "Action";
            this.ActionOrBranch.Name = "ActionOrBranch";
            this.ActionOrBranch.ReadOnly = true;
            this.ActionOrBranch.Width = 200;
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorMessage.DefaultCellStyle = dataGridViewCellStyle3;
            this.ErrorMessage.HeaderText = "Error Details";
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.ReadOnly = true;
            // 
            // BranchErrorDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 418);
            this.Controls.Add(this.ErrorList);
            this.Controls.Add(this.ErrorConfirm);
            this.Controls.Add(this.ErrorIntro);
            this.Controls.Add(this.ErrorIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BranchErrorDetails";
            this.RightToLeftLayout = true;
            this.Text = "Save Failed";
            this.Load += new System.EventHandler(this.BranchErrorDetails_Load);
            this.Click += new System.EventHandler(this.BranchErrorDetails_Click);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ErrorIcon;
        private System.Windows.Forms.Button ErrorConfirm;
        private System.Windows.Forms.DataGridView ErrorList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionOrBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorMessage;
        internal System.Windows.Forms.Label ErrorIntro;
    }
}