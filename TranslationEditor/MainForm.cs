using System.Xml;
using System;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using TranslationEditor;

namespace TranslateEditor
{
    public partial class MainForm : Form
    {
        private string _lang;
        private readonly string _langFolder;
        private string _langFileName;
        private readonly string[] _langEngJson;
        private readonly Dictionary<string, string> _langEngData;
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        public MainForm()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _langFolder = config.GetSection("LangFolder").Value ?? "translation";
            lblFolder.Text = _langFolder;

            if (!Directory.Exists(_langFolder))
            {
                MessageBox.Show(
                    $"Directory not found: '{_langFolder}'\n\n" +
                    $"Please specify it in the appsettings.json");

                return;
            }

            var langEng = config.GetSection("LangEng").Value ?? "english";
            _langEngJson = File.ReadAllLines(Path.Combine(_langFolder, $"{langEng}.json"));
            _langEngData = JsonSerializer.Deserialize<Dictionary<string, string>>(string.Join("\r\n", _langEngJson), jsonSerializerOptions) ?? [];

            _lang = config.GetSection("Lang").Value ?? "russian";
            var langs = (config.GetSection("Langs").Value ?? "czech,polish,russian,swedish").Split(',');
            cbLang.Items.AddRange(langs);
            cbLang.SelectedItem = _lang;
            SecondLang.HeaderText = _lang;
        }

        private void UpdateCount()
        {
            var updated = 0;
            foreach (DataGridViewRow row in gridLang.Rows)
            {
                var enValue = row.Cells[1].Value as string;
                var langValue = row.Cells[2].Value as string;
                var isUpdated = enValue?.Trim() != langValue?.Trim() && !string.IsNullOrWhiteSpace(langValue);
                if (isUpdated)
                    updated++;
            }

            lblCount.Text = $"Count: {gridLang.RowCount}/{updated} ({updated * 100 / gridLang.RowCount}%)";
        }

        private void gridLang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2)
                return;

            var key = gridLang.Rows[e.RowIndex].Cells[0].Value?.ToString();
            if (key == null)
                return;

            var dataIndex = -1;
            var isNew = false;
            string? lastKey = null;
            for (int i = e.RowIndex; i >= 0; i--)
            {
                lastKey = gridLang.Rows[i].Cells[0].Value?.ToString();
                if (lastKey == null) continue;

                for (int r = 0; r < _langJson.Count; r++)
                {
                    var row = _langJson[r];
                    if (row.Contains($"\"{lastKey}\""))
                    {
                        dataIndex = r;
                        break;
                    }
                }

                if (dataIndex != -1)
                    break;
            }

            if (lastKey != key)
            {
                isNew = true;
            }

            string langValue = gridLang.Rows[e.RowIndex].Cells[2].Value as string ?? string.Empty;
            string enValue = gridLang.Rows[e.RowIndex].Cells[1].Value as string ?? string.Empty;

            var isUpdated = enValue?.Trim() != langValue?.Trim() && !string.IsNullOrWhiteSpace(langValue);
            gridLang.Rows[e.RowIndex].Cells[2].Style.BackColor = isUpdated
                ? Color.LightGreen
                : Color.LightCoral;

            var dataRow = string.Empty;
            for (int i = 0; i < _langEngJson.Length; i++)
            {
                if (_langEngJson[i].Contains($"\"{key}\""))
                {
                    dataRow = _langEngJson[i].Replace($"\"{enValue!.Replace("\"", "\\\"")}\"", $"\"{langValue!.Replace("\"", "\\\"")}\"");
                    break;
                }
            }

            if (isNew)
                _langJson.Insert(dataIndex + 1, dataRow);
            else
                _langJson[dataIndex] = dataRow;

            File.WriteAllLines(_langFileName, _langJson);

            UpdateCount();
        }

        private int _searchRow;
        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            var search = tbSearch.Text;
            if (string.IsNullOrEmpty(search))
                return;

            if (e.KeyChar == (char)13)
            {
                for (int i = _searchRow + 1; i < gridLang.RowCount; i++)
                {
                    if (gridLang.Rows[i].Cells[0].Value?.ToString()?.Contains(search, StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        gridLang.Rows[i].Cells[0].Selected = true;
                        _searchRow = i;
                        gridLang.FirstDisplayedScrollingRowIndex = i;

                        return;
                    }

                    if (gridLang.Rows[i].Cells[1].Value?.ToString()?.Contains(search, StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        gridLang.Rows[i].Cells[1].Selected = true;
                        _searchRow = i;
                        gridLang.FirstDisplayedScrollingRowIndex = i;

                        return;
                    }

                    if (gridLang.Rows[i].Cells[2].Value?.ToString()?.Contains(search, StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        gridLang.Rows[i].Cells[2].Selected = true;
                        _searchRow = i;
                        gridLang.FirstDisplayedScrollingRowIndex = i;

                        return;
                    }
                }

                _searchRow = 0;
            }
        }

        private List<string> _langJson;
        private Dictionary<string, string>? _langData;
        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLang.SelectedItem == null)
                return;

            var lang = cbLang.SelectedItem as string;
            if (string.IsNullOrEmpty(lang))
                return;

            _lang = lang;
            SecondLang.HeaderText = _lang;

            _langFileName = Path.Combine(_langFolder, $"{_lang}.json");
            _langJson = [.. File.ReadAllLines(_langFileName)];
            _langData = JsonSerializer.Deserialize<Dictionary<string, string>>(string.Join("\r\n", _langJson), jsonSerializerOptions);

            if (_langData == null)
                return;

            gridLang.RowCount = _langEngData.Count;

            var i = 0;
            foreach (var l in _langEngData)
            {
                var enValue = l.Value;
                _langData.TryGetValue(l.Key, out var langValue);

                gridLang.Rows[i].Cells[0].Value = l.Key;
                gridLang.Rows[i].Cells[1].Value = enValue.Replace("\n", "\\n");
                gridLang.Rows[i].Cells[2].Value = langValue?.Replace("\n", "\\n");

                var isUpdated = enValue?.Trim() != langValue?.Trim() && !string.IsNullOrWhiteSpace(langValue);
                gridLang.Rows[i].Cells[2].Style.BackColor = isUpdated
                    ? Color.LightGreen
                    : Color.LightCoral;

                i++;
            }

            UpdateCount();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbSearch_KeyPress(tbSearch, new KeyPressEventArgs((char)13));
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var convertForm = new ConvertForm();
            convertForm.Owner = this;
            convertForm.Show();
        }
    }
}
