namespace TranslationEditor
{
    partial class UppercaseForm
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
            textWord = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textWordsBefore = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // textWord
            // 
            textWord.Location = new Point(12, 25);
            textWord.Name = "textWord";
            textWord.Size = new Size(199, 23);
            textWord.TabIndex = 0;
            textWord.Text = "дракон";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 1;
            label1.Text = "Uppercase word:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(223, 7);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 3;
            label2.Text = "and word(s) before:";
            // 
            // textWordsBefore
            // 
            textWordsBefore.Location = new Point(223, 25);
            textWordsBefore.Multiline = true;
            textWordsBefore.Name = "textWordsBefore";
            textWordsBefore.Size = new Size(383, 51);
            textWordsBefore.TabIndex = 2;
            textWordsBefore.Text = "красн,черн,чёрн,зелён,зелен,золот,костян,призрачн,кристальн,лазурн,ржав,сказочн";
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(188, 88);
            button1.Name = "button1";
            button1.Size = new Size(99, 31);
            button1.TabIndex = 4;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.OK;
            button2.Location = new Point(293, 88);
            button2.Name = "button2";
            button2.Size = new Size(99, 31);
            button2.TabIndex = 5;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // UppercaseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 131);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(textWordsBefore);
            Controls.Add(label1);
            Controls.Add(textWord);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "UppercaseForm";
            Text = "Uppercase";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        public TextBox textWord;
        public TextBox textWordsBefore;
    }
}