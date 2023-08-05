namespace NSOEndingTreeMaker
{
    partial class UsedStreamWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsedStreamWindow));
            this.UsedStream_ListView = new System.Windows.Forms.ListView();
            this.Stream = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DayIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // UsedStream_ListView
            // 
            this.UsedStream_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Stream,
            this.DayIndex});
            this.UsedStream_ListView.HideSelection = false;
            this.UsedStream_ListView.Location = new System.Drawing.Point(13, 13);
            this.UsedStream_ListView.Name = "UsedStream_ListView";
            this.UsedStream_ListView.Size = new System.Drawing.Size(292, 425);
            this.UsedStream_ListView.TabIndex = 0;
            this.UsedStream_ListView.UseCompatibleStateImageBehavior = false;
            this.UsedStream_ListView.View = System.Windows.Forms.View.Details;
            // 
            // Stream
            // 
            this.Stream.Text = "Stream Topic And Level";
            this.Stream.Width = 165;
            // 
            // DayIndex
            // 
            this.DayIndex.Text = "Day Streamed";
            this.DayIndex.Width = 122;
            // 
            // UsedStreamWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 450);
            this.Controls.Add(this.UsedStream_ListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsedStreamWindow";
            this.Text = "Streamed List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsedStreamWindow_FormClosing);
            this.Load += new System.EventHandler(this.UsedStreamWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView UsedStream_ListView;
        private System.Windows.Forms.ColumnHeader Stream;
        private System.Windows.Forms.ColumnHeader DayIndex;
    }
}