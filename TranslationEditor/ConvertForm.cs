using System.Text;

namespace TranslationEditor
{
    public partial class ConvertForm : Form
    {
        public ConvertForm()
        {
            InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encodings = Encoding.GetEncodings();

            foreach (var encoding in encodings)
            {
                cbFromEncoding.Items.Add(encoding.Name);
                cbToEncoding.Items.Add(encoding.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbFromEncoding.SelectedIndex == 0 || cbToEncoding.SelectedIndex == 0 || string.IsNullOrEmpty(richTextFrom.Text))
                return;

            var fromEncoding = Encoding.GetEncoding(cbFromEncoding.SelectedItem.ToString());
            var toEncoding = Encoding.GetEncoding(cbToEncoding.SelectedItem.ToString());

            var bytes = fromEncoding.GetBytes(richTextFrom.Text);
            richTextTo.Text = toEncoding.GetString(bytes);
        }
    }
}
