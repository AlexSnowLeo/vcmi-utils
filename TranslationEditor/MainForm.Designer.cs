namespace TranslateEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            statusStrip = new StatusStrip();
            lblFolder = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            lblCount = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            toolStripLabel2 = new ToolStripLabel();
            cbLang = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            tbSearch = new ToolStripTextBox();
            btnSearch = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnConvert = new ToolStripButton();
            gridLang = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            FirstLang = new DataGridViewTextBoxColumn();
            SecondLang = new DataGridViewTextBoxColumn();
            statusStrip.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLang).BeginInit();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblFolder, toolStripStatusLabel2, lblCount });
            statusStrip.Location = new Point(0, 793);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1287, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // lblFolder
            // 
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(30, 17);
            lblFolder.Text = "data";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(10, 17);
            toolStripStatusLabel2.Text = "|";
            // 
            // lblCount
            // 
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(43, 17);
            lblCount.Text = "Count:";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel2, cbLang, toolStripSeparator1, tbSearch, btnSearch, toolStripSeparator2, btnConvert });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1287, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(41, 22);
            toolStripLabel2.Text = "LANG:";
            // 
            // cbLang
            // 
            cbLang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLang.FlatStyle = FlatStyle.Standard;
            cbLang.Name = "cbLang";
            cbLang.Size = new Size(75, 25);
            cbLang.SelectedIndexChanged += cbLang_SelectedIndexChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tbSearch
            // 
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(200, 25);
            tbSearch.KeyPress += tbSearch_KeyPress;
            // 
            // btnSearch
            // 
            btnSearch.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.ImageTransparentColor = Color.Magenta;
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(46, 22);
            btnSearch.Text = "Search";
            btnSearch.ToolTipText = "Search in current file";
            btnSearch.Click += btnSearch_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // btnConvert
            // 
            btnConvert.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnConvert.Image = (Image)resources.GetObject("btnConvert.Image");
            btnConvert.ImageTransparentColor = Color.Magenta;
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(53, 22);
            btnConvert.Text = "Convert";
            btnConvert.Click += btnConvert_Click;
            // 
            // gridLang
            // 
            gridLang.AllowUserToAddRows = false;
            gridLang.AllowUserToDeleteRows = false;
            gridLang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridLang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridLang.Columns.AddRange(new DataGridViewColumn[] { Key, FirstLang, SecondLang });
            gridLang.Dock = DockStyle.Fill;
            gridLang.Location = new Point(0, 25);
            gridLang.MultiSelect = false;
            gridLang.Name = "gridLang";
            gridLang.RowHeadersVisible = false;
            gridLang.Size = new Size(1287, 768);
            gridLang.TabIndex = 2;
            gridLang.CellEndEdit += gridLang_CellEndEdit;
            // 
            // Key
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Key.DefaultCellStyle = dataGridViewCellStyle1;
            Key.FillWeight = 250F;
            Key.Frozen = true;
            Key.HeaderText = "key";
            Key.Name = "Key";
            Key.Width = 250;
            // 
            // FirstLang
            // 
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            FirstLang.DefaultCellStyle = dataGridViewCellStyle2;
            FirstLang.FillWeight = 500F;
            FirstLang.Frozen = true;
            FirstLang.HeaderText = "english";
            FirstLang.Name = "FirstLang";
            FirstLang.Width = 500;
            // 
            // SecondLang
            // 
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            SecondLang.DefaultCellStyle = dataGridViewCellStyle3;
            SecondLang.FillWeight = 500F;
            SecondLang.HeaderText = "lang";
            SecondLang.Name = "SecondLang";
            SecondLang.Width = 500;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1287, 815);
            Controls.Add(gridLang);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip);
            Name = "MainForm";
            Text = "VCMI Translation Editor";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridLang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStrip toolStrip1;
        private DataGridView gridLang;
        private ToolStripStatusLabel lblFolder;
        private ToolStripTextBox tbSearch;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox cbLang;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSearch;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel lblCount;
        private ToolStripButton btnConvert;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn FirstLang;
        private DataGridViewTextBoxColumn SecondLang;
    }
}
