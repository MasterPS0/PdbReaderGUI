using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Text.Json;

namespace PdbReaderPro
{
    public class MainForm : Form
    {
        TextBox txtPath;
        Button btnBrowse, btnExtract, btnSaveTxt, btnSaveJson, btnClear;
        DataGridView grid;
        PictureBox preview;
        Label lblPreview;

        List<string> pngLinks = new();
        List<string> jsonLinks = new();
        List<string> pkgLinks = new();
        List<string> names = new();

        public MainForm()
        {
            Text = "PDB Reader PRO";
            Size = new Size(1250, 650);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9);

            txtPath = new TextBox()
            {
                Left = 12,
                Top = 15,
                Width = 700,
                Height = 28
            };

            btnBrowse = new Button() { Text = "Browse", Left = 725, Top = 13, Width = 100, Height = 30 };
            btnExtract = new Button() { Text = "Extract", Left = 835, Top = 13, Width = 100, Height = 30 };
            btnSaveTxt = new Button() { Text = "Save TXT", Left = 945, Top = 13, Width = 100, Height = 30 };
            btnSaveJson = new Button() { Text = "Save JSON", Left = 1055, Top = 13, Width = 100, Height = 30 };
            btnClear = new Button() { Text = "Clear", Left = 1165, Top = 13, Width = 70, Height = 30 };

            btnBrowse.Click += Browse;
            btnExtract.Click += Extract;
            btnSaveTxt.Click += SaveTxt;
            btnSaveJson.Click += SaveJson;
            btnClear.Click += ClearAll;

            grid = new DataGridView()
            {
                Left = 12,
                Top = 60,
                Width = 880,
                Height = 530,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            grid.Columns.Add("Name", "Name / Content ID");
            grid.Columns.Add("PNG", "PNG");
            grid.Columns.Add("JSON", "JSON");
            grid.Columns.Add("PKG", "PKG");

            grid.Columns["Name"].Width = 220;
            grid.Columns["PNG"].Width = 250;
            grid.Columns["JSON"].Width = 250;
            grid.Columns["PKG"].Width = 250;

            lblPreview = new Label()
            {
                Text = "Preview 128 x 128",
                Left = 920,
                Top = 60,
                Width = 250,
                Height = 25,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            preview = new PictureBox()
            {
                Left = 920,
                Top = 90,
                Width = 128,
                Height = 128,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            grid.SelectionChanged += ShowImage;

            Controls.Add(txtPath);
            Controls.Add(btnBrowse);
            Controls.Add(btnExtract);
            Controls.Add(btnSaveTxt);
            Controls.Add(btnSaveJson);
            Controls.Add(btnClear);
            Controls.Add(grid);
            Controls.Add(lblPreview);
            Controls.Add(preview);
        }

        void Browse(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "PDB Files (*.pdb)|*.pdb|All Files (*.*)|*.*",
                Title = "Select PDB File"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                txtPath.Text = ofd.FileName;
        }

        void Extract(object sender, EventArgs e)
        {
            if (!File.Exists(txtPath.Text))
            {
                MessageBox.Show("The file is not found");
                return;
            }

            byte[] data = File.ReadAllBytes(txtPath.Text);
            var strings = ExtractStrings(data);

            var allLinks = strings
                .SelectMany(s => Regex.Matches(
                    s,
                    @"https?:\/\/[^\s""'<>\\]+?\.(png|json|pkg)(\?[^\s""'<>\\]*)?|[A-Za-z0-9_\-\.\/]+?\.(png|json|pkg)",
                    RegexOptions.IgnoreCase
                ).Cast<Match>())
                .Select(m => CleanUrl(m.Value))
                .Distinct()
                .ToList();

            pngLinks = allLinks
                .Where(x => x.Contains(".png", StringComparison.OrdinalIgnoreCase))
                .ToList();

            jsonLinks = allLinks
                .Where(x => x.Contains(".json", StringComparison.OrdinalIgnoreCase))
                .ToList();

            pkgLinks = allLinks
                .Where(x => x.Contains(".pkg", StringComparison.OrdinalIgnoreCase))
                .ToList();

            names = strings
                .SelectMany(s => Regex.Matches(
                    s,
                    @"[A-Z]{2}\d{4}-[A-Z0-9]{9}_\d{2}-[A-Z0-9_\-]+",
                    RegexOptions.IgnoreCase
                ).Cast<Match>().Select(m => m.Value))
                .Distinct()
                .ToList();

            grid.Rows.Clear();

            int max = Math.Max(
                Math.Max(names.Count, pngLinks.Count),
                Math.Max(jsonLinks.Count, pkgLinks.Count)
            );

            for (int i = 0; i < max; i++)
            {
                grid.Rows.Add(
                    names.ElementAtOrDefault(i) ?? "",
                    pngLinks.ElementAtOrDefault(i) ?? "",
                    jsonLinks.ElementAtOrDefault(i) ?? "",
                    pkgLinks.ElementAtOrDefault(i) ?? ""
                );
            }

            MessageBox.Show($"Extraction \nPNG: {pngLinks.Count}\nJSON: {jsonLinks.Count}\nPKG: {pkgLinks.Count}");
        }

        void SaveTxt(object sender, EventArgs e)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("No data to save");
                return;
            }

            SaveFileDialog sfd = new()
            {
                Filter = "Text File (*.txt)|*.txt",
                FileName = "pdb_result.txt"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            StringBuilder sb = new();

            foreach (DataGridViewRow row in grid.Rows)
            {
                sb.AppendLine("====================================");
                sb.AppendLine("Name / Content ID: " + GetCell(row, "Name"));
                sb.AppendLine("PNG: " + GetCell(row, "PNG"));
                sb.AppendLine("JSON: " + GetCell(row, "JSON"));
                sb.AppendLine("PKG: " + GetCell(row, "PKG"));
                sb.AppendLine();
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("The TXT file was saved successfully");
        }

        void SaveJson(object sender, EventArgs e)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("No data to save");
                return;
            }

            SaveFileDialog sfd = new()
            {
                Filter = "JSON File (*.json)|*.json",
                FileName = "pdb_result.json"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            List<PdbItem> items = new();

            foreach (DataGridViewRow row in grid.Rows)
            {
                items.Add(new PdbItem
                {
                    Name = GetCell(row, "Name"),
                    PNG = GetCell(row, "PNG"),
                    JSON = GetCell(row, "JSON"),
                    PKG = GetCell(row, "PKG")
                });
            }

            string json = JsonSerializer.Serialize(
                items,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(sfd.FileName, json, Encoding.UTF8);
            MessageBox.Show("JSON saved successfully");
        }

        void ClearAll(object sender, EventArgs e)
        {
            txtPath.Clear();
            grid.Rows.Clear();
            preview.Image = null;
            pngLinks.Clear();
            jsonLinks.Clear();
            pkgLinks.Clear();
            names.Clear();
        }

        void ShowImage(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null)
                return;

            string url = GetCell(grid.CurrentRow, "PNG");

            if (string.IsNullOrWhiteSpace(url))
            {
                preview.Image = null;
                return;
            }

            try
            {
                using WebClient wc = new();
                byte[] imgData = wc.DownloadData(url);
                using MemoryStream ms = new(imgData);

                Image img = Image.FromStream(ms);
                preview.Image = new Bitmap(img, 128, 128);
            }
            catch
            {
                preview.Image = null;
            }
        }

        static string GetCell(DataGridViewRow row, string columnName)
        {
            return row.Cells[columnName].Value?.ToString() ?? "";
        }

        static List<string> ExtractStrings(byte[] data)
        {
            List<string> result = new();
            StringBuilder sb = new();

            foreach (byte b in data)
            {
                if (b >= 32 && b <= 126)
                {
                    sb.Append((char)b);
                }
                else
                {
                    if (sb.Length >= 4)
                        result.Add(sb.ToString());

                    sb.Clear();
                }
            }

            if (sb.Length >= 4)
                result.Add(sb.ToString());

            return result;
        }

        static string CleanUrl(string url)
        {
            url = url.Replace("\\/", "/").Trim();

            char[] badEnd =
            {
                '"', '\'', ',', ';', '}', ']', ')', ' ', '\0'
            };

            while (url.Length > 0 && badEnd.Contains(url[^1]))
                url = url[..^1];

            return url;
        }
    }

    public class PdbItem
    {
        public string Name { get; set; } = "";
        public string PNG { get; set; } = "";
        public string JSON { get; set; } = "";
        public string PKG { get; set; } = "";
    }
}