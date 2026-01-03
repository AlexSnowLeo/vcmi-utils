using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;

namespace TranslationEditor
{
    public partial class MainForm : Form
    {
        private string _lang;
        private string _langEng;
        private readonly string _langFolder;
        private readonly string? _langEnFolder;
        private string _langFileName;
        private string[] _langEngJson;
        private readonly bool _useSeparateFolders;
        private Dictionary<string, string> _langEngData;
        private readonly TranslationSettings _translationStat;
        private string _translationStatFileName = string.Empty;

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
            var translationStatName = _langFolder.Split("\\").Where(x => !string.IsNullOrEmpty(x)).Last();
            _translationStatFileName = $"{translationStatName}.settings.json";
            if (File.Exists(_translationStatFileName))
            {
                var json = File.ReadAllText(_translationStatFileName);
                _translationStat = JsonSerializer.Deserialize<TranslationSettings>(json) 
                    ?? new TranslationSettings { Name = translationStatName };
            } 
            else
            {
                _translationStat = new TranslationSettings { Name = translationStatName };

                var json = JsonSerializer.Serialize(_translationStat);
                File.WriteAllText(_translationStatFileName, json);
            }


            if (!Directory.Exists(_langFolder))
            {
                MessageBox.Show(
                    $"Directory not found: '{_langFolder}'\n\n" +
                    $"Please specify it in the appsettings.json");

                return;
            }

            _useSeparateFolders = config.GetSection("UseSeparateFolders").Value == "True";
            if (_useSeparateFolders)
            {
                _langEnFolder = config.GetSection("LangEnFolder").Value;
                if (string.IsNullOrEmpty(_langEnFolder))
                {
                    MessageBox.Show("Please specify the 'LangEnFolder' in the appsettings.json");
                    return;
                }

                if (!Directory.Exists(_langEnFolder))
                {
                    MessageBox.Show(
                        $"Directory not found: '{_langEnFolder}'\n\n" +
                        $"Please specify the 'LangEnFolder' in the appsettings.json");

                    return;
                }
            }

            _langEng = config.GetSection("LangEng").Value ?? "english";
            _langEngJson = File.ReadAllLines(Path.Combine(_useSeparateFolders ? _langEnFolder : _langFolder, $"{_langEng}.json"));
            _langEngData = JsonSerializer.Deserialize<Dictionary<string, string>>(string.Join("\r\n", _langEngJson), jsonSerializerOptions) ?? [];

            var font = config.GetSection("EditorFont").Value ?? "Consolas";
            var fontSize = float.Parse(config.GetSection("EditorFontSize").Value ?? "11");
            gridLang.RowTemplate.DefaultCellStyle.Font = new Font(font, fontSize);
            gridLang.DefaultCellStyle.Font = new Font(font, fontSize);
            gridLang.DefaultCellStyle.Font = new Font(font, fontSize);

            _lang = config.GetSection("Lang").Value ?? "russian";

            string[] langs = [];
            if (config.GetSection("Langs").Value == null)
            {
                langs = Directory.GetFiles(_langFolder, "*.json", SearchOption.TopDirectoryOnly)
                    .Select(x => Path.GetFileNameWithoutExtension(x))
                    .ToArray();
            }
            else
            {
                langs = (config.GetSection("Langs").Value ?? "czech,polish,russian,swedish").Split(',');
            }
                
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
            langValue = langValue.Replace("\n", "\\n").Replace("\r", "").Replace("Ч", "-").Replace("Ђ", "\"").Replace("ї", "\"");
            gridLang.Rows[e.RowIndex].Cells[2].Value = langValue;

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

            _translationStat.LastRow = e.RowIndex;
            var json = JsonSerializer.Serialize(_translationStat);
            File.WriteAllText(_translationStatFileName, json);

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

            /*if (_useSeparateFolders)
            {
                _langEng = lang;
                _langEngJson = File.ReadAllLines(Path.Combine(_langEnFolder!, $"{_langEng}.json"));
                _langEngData = JsonSerializer.Deserialize<Dictionary<string, string>>(string.Join("\r\n", _langEngJson), jsonSerializerOptions) ?? [];
            }*/

            _langFileName = Path.Combine(_langFolder, $"{_lang}.json");
            _langJson = [.. File.ReadAllLines(_langFileName)];
            _langData = JsonSerializer.Deserialize<Dictionary<string, string>>(string.Join("\r\n", _langJson), jsonSerializerOptions);

            if (_langData == null)
                return;

            gridLang.RowCount = _langEngData.Count;

            gridLang.RowHeadersWidth = gridLang.RowCount >= 1000 ? 60 : gridLang.RowCount >= 100 ? 50 : 40;

            var i = 0;
            foreach (var l in _langEngData)
            {
                var enValue = l.Value;
                _langData.TryGetValue(l.Key, out var langValue);

                gridLang.Rows[i].Cells[0].Value = l.Key;
                gridLang.Rows[i].Cells[1].Value = enValue.Replace("\n", "\\n");
                gridLang.Rows[i].Cells[2].Value = langValue?.Replace("\n", "\\n");
                gridLang.Rows[i].HeaderCell.Value = (i + 1).ToString();

                var isUpdated = enValue?.Trim() != langValue?.Trim() && !string.IsNullOrWhiteSpace(langValue);
                gridLang.Rows[i].Cells[2].Style.BackColor = isUpdated
                    ? Color.LightGreen
                    : Color.LightCoral;

                i++;
            }

            if (_translationStat.LastRow < gridLang.RowCount)
            {
                gridLang.FirstDisplayedScrollingRowIndex = _translationStat.LastRow;
                gridLang.Rows[_translationStat.LastRow].Cells[2].Selected = true;
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

        private void btnUppercase_Click(object sender, EventArgs e)
        {
            if (_langData == null)
                return;

            var upcaseForm = new UppercaseForm();
            upcaseForm.Owner = this;
            var result = upcaseForm.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            var word = upcaseForm.textWord.Text;
            var wordsBefore = upcaseForm.textWordsBefore.Text.Split(',');

            //var test = CapitalizeWords("совет повелителей драконов и остальных невинных лазурных драконих, павших жертвой некромантов", word, wordsBefore);
            //return;

            var count = 0;
            var i = 0;
            foreach (var data in _langData)
            {
                var processedData = CapitalizeWords(data.Value, word, wordsBefore);
                if (processedData != data.Value)
                {
                    count++;

                    var dataRow = string.Empty;
                    for (int k = 0; k < _langEngJson.Length; k++)
                    {
                        if (_langJson[k].Contains($"\"{data.Key}\""))
                        {
                            _langJson[k] = _langJson[k].Replace($"\"{data.Value.Replace("\"", "\\\"")}\"", $"\"{processedData.Replace("\"", "\\\"")}\"");
                            break;
                        }
                    }

                    _langData[data.Key] = processedData;
                    gridLang.Rows[i].Cells[2].Value = processedData.Replace("\n", "\\n");
                    gridLang.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                }

                i++;
            }

            if (count > 0)
                File.WriteAllLines(_langFileName, _langJson);
        }

        private static string CapitalizeWords(string text, string word, string[] wordsBefore)
        {
            var words = text.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i + 1].ToLower().StartsWith(word))
                {
                    if (wordsBefore.Any(x => words[i].ToLower().StartsWith(x)))
                    {
                        words[i + 1] = char.ToUpper(words[i + 1][0]) + words[i + 1].Substring(1);
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                    }
                }
            }

            return string.Join(" ", words);
        }

        private void btnCreateFromOrig_Click(object sender, EventArgs e)
        {
            var newLangJson = new List<string>(_langEngJson);

            foreach (DataGridViewRow row in gridLang.Rows)
            {
                var key = row.Cells[0].Value?.ToString();
                var enValue = row.Cells[1].Value?.ToString() ?? string.Empty;
                var langValue = row.Cells[2].Value?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(langValue))
                    langValue = enValue;

                var dataRow = string.Empty;
                for (int i = 0; i < newLangJson.Count; i++)
                {
                    if (newLangJson[i].Contains($"\"{key}\""))
                    {
                        newLangJson[i] = newLangJson[i].Replace($"\"{enValue!.Replace("\"", "\\\"")}\"", $"\"{langValue!.Replace("\"", "\\\"")}\"");
                        break;
                    }
                }
            }

            File.WriteAllLines(_langFileName + ".new", newLangJson);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var idx = gridLang.SelectedCells.Count > 0 ? gridLang.SelectedCells[0].RowIndex : 0;

            gridLang.ClearSelection();

            for (int i = idx + 1; i < gridLang.Rows.Count; i++)
            {
                var row = gridLang.Rows[i];
                if (row.Cells[2].Style.BackColor == Color.LightCoral)
                {
                    row.Selected = true;
                    gridLang.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            var idx = gridLang.SelectedCells.Count > 0 ? gridLang.SelectedCells[0].RowIndex : 0;

            gridLang.ClearSelection();

            for (int i = idx - 1; i >= 0; i--)
            {
                var row = gridLang.Rows[i];
                if (row.Cells[2].Style.BackColor == Color.LightCoral)
                {
                    row.Selected = true;
                    gridLang.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }
    }
}
