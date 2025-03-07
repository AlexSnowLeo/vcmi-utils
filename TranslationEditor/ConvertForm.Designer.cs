namespace TranslationEditor
{
    partial class ConvertForm
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            richTextFrom = new RichTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            richTextTo = new RichTextBox();
            panel1 = new Panel();
            button1 = new Button();
            cbToEncoding = new ComboBox();
            cbFromEncoding = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 30);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(richTextFrom);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(richTextTo);
            splitContainer1.Size = new Size(884, 712);
            splitContainer1.SplitterDistance = 343;
            splitContainer1.TabIndex = 5;
            // 
            // richTextFrom
            // 
            richTextFrom.ContextMenuStrip = contextMenuStrip1;
            richTextFrom.DetectUrls = false;
            richTextFrom.Dock = DockStyle.Fill;
            richTextFrom.Location = new Point(0, 0);
            richTextFrom.Name = "richTextFrom";
            richTextFrom.Size = new Size(884, 343);
            richTextFrom.TabIndex = 0;
            richTextFrom.Text = "";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // richTextTo
            // 
            richTextTo.ContextMenuStrip = contextMenuStrip1;
            richTextTo.DetectUrls = false;
            richTextTo.Dock = DockStyle.Fill;
            richTextTo.Location = new Point(0, 0);
            richTextTo.Name = "richTextTo";
            richTextTo.Size = new Size(884, 365);
            richTextTo.TabIndex = 1;
            richTextTo.Text = "";
            // 
            // panel1
            // 
            panel1.Controls.Add(cbToEncoding);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(cbFromEncoding);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(884, 30);
            panel1.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(349, 1);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Convert";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // cbToEncoding
            // 
            cbToEncoding.FormattingEnabled = true;
            cbToEncoding.Location = new Point(176, 2);
            cbToEncoding.Name = "cbToEncoding";
            cbToEncoding.Size = new Size(167, 23);
            cbToEncoding.TabIndex = 7;
            // 
            // cbFromEncoding
            // 
            cbFromEncoding.FormattingEnabled = true;
            cbFromEncoding.Location = new Point(3, 3);
            cbFromEncoding.Name = "cbFromEncoding";
            cbFromEncoding.Size = new Size(167, 23);
            cbFromEncoding.TabIndex = 5;
            // 
            // ConvertForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 742);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Name = "ConvertForm";
            Text = "Convert encoding";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SplitContainer splitContainer1;
        private RichTextBox richTextFrom;
        private ContextMenuStrip contextMenuStrip1;
        private RichTextBox richTextTo;
        private Panel panel1;
        private ComboBox cbToEncoding;
        private Button button1;
        private ComboBox cbFromEncoding;
    }
}