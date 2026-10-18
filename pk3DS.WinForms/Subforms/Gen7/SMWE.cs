using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

using pk3DS.Core;
using pk3DS.Core.CTR;
using pk3DS.Core.Randomizers;

namespace pk3DS.WinForms;

public partial class SMWE : Form
{
    public SMWE(LazyGARCFile ed, LazyGARCFile zd, LazyGARCFile wd)
    {
        InitializeComponent();

        PB_DayIcon.Image = Properties.Resources.sun;
        PB_NightIcon.Image = Properties.Resources.moon;
        PB_DayIcon.SizeMode = PictureBoxSizeMode.CenterImage;
        PB_NightIcon.SizeMode = PictureBoxSizeMode.CenterImage;

        font = L_Location.Font;

        speciesList[0] = "(None)";
        var locationList = Main.Config.GetText(TextName.metlist_000000);
        locationList = GetGoodLocationList(locationList);

        nup_spec = LoadFormeNUD();
        cb_spec = LoadSpeciesComboBoxes();
        rate_spec = LoadRateNUD();

        encdata = ed;
        var areas = Area7.GetArray(ed, zd, wd, locationList);
        Areas = [.. areas.OrderBy(a => a.Zones[0].Name)];

        LoadData();
        RandSettings.GetFormSettings(this, GB_Tweak.Controls);

        var weather = string.Format("If weather is active, create a random number.{0}If 0, use slot 0.{0}If <= 10, use slot 1.{0}Else, pick an SOS table and a slot.", Environment.NewLine);
        new ToolTip().SetToolTip(L_AddSOS, weather);
        var sos = new[] { L_SOS1, L_SOS2, L_SOS3, L_SOS4, L_SOS5, L_SOS6, L_SOS7 };
        var rates = new[] { 1, 1, 1, 10, 10, 10, 67 };
        for (int i = 0; i < sos.Length; i++)
            new ToolTip().SetToolTip(sos[i], $"Table Selection Rate: {rates[i]}%");

        // ExportEncounters("um", "uu");

        SetupJsonButtons();
    }

    private Button B_Import;
    private Button B_ExportNames;

    private void SetupJsonButtons()
    {
        B_Dump.Text = "Dump JSON";

        // Place Import to the left of Dump JSON so it stays on-screen
        B_Import = new Button
        {
            Text = "Import JSON",
            Location = new Point(B_Dump.Left - B_Dump.Width - 6, B_Dump.Top),
            Size = B_Dump.Size,
            UseVisualStyleBackColor = true
        };
        B_Import.Click += B_Import_Click;
        Controls.Add(B_Import);

        // Place Export Names further right (past the existing Export Tables button)
        B_ExportNames = new Button
        {
            Text = "Export Names",
            Location = new Point(B_Dump.Right + 6 + 114, B_Dump.Top), // 114 ≈ Export Tables width + gap
            Size = B_Dump.Size,
            UseVisualStyleBackColor = true
        };
        B_ExportNames.Click += B_ExportNames_Click;
        Controls.Add(B_ExportNames);
    }

    private NumericUpDown[] LoadRateNUD()
    {
        var list = new[] { NUP_Rate1, NUP_Rate2, NUP_Rate3, NUP_Rate4, NUP_Rate5, NUP_Rate6, NUP_Rate7, NUP_Rate8, NUP_Rate9, NUP_Rate10 };
        foreach (var nup in list)
            nup.ValueChanged += UpdateEncounterRate;
        return list;
    }

    private ComboBox[][] LoadSpeciesComboBoxes()
    {
        var list = new[] {
            [CB_Enc01, CB_Enc02, CB_Enc03, CB_Enc04, CB_Enc05, CB_Enc06, CB_Enc07, CB_Enc08, CB_Enc09, CB_Enc10],
            [CB_Enc11, CB_Enc12, CB_Enc13, CB_Enc14, CB_Enc15, CB_Enc16, CB_Enc17, CB_Enc18, CB_Enc19, CB_Enc20],
            [CB_Enc21, CB_Enc22, CB_Enc23, CB_Enc24, CB_Enc25, CB_Enc26, CB_Enc27, CB_Enc28, CB_Enc29, CB_Enc30],
            [CB_Enc31, CB_Enc32, CB_Enc33, CB_Enc34, CB_Enc35, CB_Enc36, CB_Enc37, CB_Enc38, CB_Enc39, CB_Enc40],
            [CB_Enc41, CB_Enc42, CB_Enc43, CB_Enc44, CB_Enc45, CB_Enc46, CB_Enc47, CB_Enc48, CB_Enc49, CB_Enc50],
            [CB_Enc51, CB_Enc52, CB_Enc53, CB_Enc54, CB_Enc55, CB_Enc56, CB_Enc57, CB_Enc58, CB_Enc59, CB_Enc60],
            [CB_Enc61, CB_Enc62, CB_Enc63, CB_Enc64, CB_Enc65, CB_Enc66, CB_Enc67, CB_Enc68, CB_Enc69, CB_Enc70],
            [CB_Enc71, CB_Enc72, CB_Enc73, CB_Enc74, CB_Enc75, CB_Enc76, CB_Enc77, CB_Enc78, CB_Enc79, CB_Enc80],
            new[] {CB_WeatherEnc1, CB_WeatherEnc2, CB_WeatherEnc3, CB_WeatherEnc4, CB_WeatherEnc5, CB_WeatherEnc6},
        };
        foreach (var cb_l in list)
        {
            foreach (var cb in cb_l)
            {
                cb.Items.AddRange(speciesList);
                cb.SelectedIndex = 0;
                cb.SelectedIndexChanged += UpdateSpeciesForm;
            }
        }

        return list;
    }

    private NumericUpDown[][] LoadFormeNUD()
    {
        var list = new[] {
            [NUP_Forme01, NUP_Forme02, NUP_Forme03, NUP_Forme04, NUP_Forme05, NUP_Forme06, NUP_Forme07, NUP_Forme08, NUP_Forme09, NUP_Forme10,
            ],
            [NUP_Forme11, NUP_Forme12, NUP_Forme13, NUP_Forme14, NUP_Forme15, NUP_Forme16, NUP_Forme17, NUP_Forme18, NUP_Forme19, NUP_Forme20,
            ],
            [NUP_Forme21, NUP_Forme22, NUP_Forme23, NUP_Forme24, NUP_Forme25, NUP_Forme26, NUP_Forme27, NUP_Forme28, NUP_Forme29, NUP_Forme30,
            ],
            [NUP_Forme31, NUP_Forme32, NUP_Forme33, NUP_Forme34, NUP_Forme35, NUP_Forme36, NUP_Forme37, NUP_Forme38, NUP_Forme39, NUP_Forme40,
            ],
            [NUP_Forme41, NUP_Forme42, NUP_Forme43, NUP_Forme44, NUP_Forme45, NUP_Forme46, NUP_Forme47, NUP_Forme48, NUP_Forme49, NUP_Forme50,
            ],
            [NUP_Forme51, NUP_Forme52, NUP_Forme53, NUP_Forme54, NUP_Forme55, NUP_Forme56, NUP_Forme57, NUP_Forme58, NUP_Forme59, NUP_Forme60,
            ],
            [NUP_Forme61, NUP_Forme62, NUP_Forme63, NUP_Forme64, NUP_Forme65, NUP_Forme66, NUP_Forme67, NUP_Forme68, NUP_Forme69, NUP_Forme70,
            ],
            [NUP_Forme71, NUP_Forme72, NUP_Forme73, NUP_Forme74, NUP_Forme75, NUP_Forme76, NUP_Forme77, NUP_Forme78, NUP_Forme79, NUP_Forme80,
            ],
            new [] { NUP_WeatherForme1, NUP_WeatherForme2, NUP_WeatherForme3, NUP_WeatherForme4, NUP_WeatherForme5, NUP_WeatherForme6 },
        };

        foreach (var nup_l in list)
        {
            foreach (var nup in nup_l)
                nup.ValueChanged += UpdateSpeciesForm;
        }

        return list;
    }

    private readonly Area7[] Areas;
    private readonly LazyGARCFile encdata;
    private readonly string[] speciesList = Main.Config.GetText(TextName.SpeciesNames);
    private readonly Font font;
    private readonly NumericUpDown[][] nup_spec;
    private readonly ComboBox[][] cb_spec;
    private readonly NumericUpDown[] rate_spec;

    private int TotalEncounterRate => rate_spec.Sum(nup => (int)nup.Value);
    private bool loadingdata;
    private EncounterTable CurrentTable;

    private void LoadData()
    {
        loadingdata = true;

        CB_LocationID.Items.Clear();
        CB_LocationID.Items.AddRange(Areas.Select(a => a.Name).ToArray());

        CB_SlotRand.SelectedIndex = 0;
        CB_LocationID.SelectedIndex = 0;

        loadingdata = false;
        ChangeMap(null, null);
    }

    private void ChangeMap(object sender, EventArgs e)
    {
        loadingdata = true;
        CB_TableID.Items.Clear();
        if (Areas[CB_LocationID.SelectedIndex].HasTables)
        {
            for (int i = 0; i < Areas[CB_LocationID.SelectedIndex].Tables.Count; i += 2)
            {
                CB_TableID.Items.Add($"{(i / 2) + 1} (Day)");
                CB_TableID.Items.Add($"{(i / 2) + 1} (Night)");
            }
        }
        else
        {
            CB_TableID.Items.Add("(None)");
        }

        CB_TableID.SelectedIndex = 0;
        loadingdata = false;
        UpdatePanel(sender, e);
    }

    private void UpdatePanel(object sender, EventArgs e)
    {
        if (loadingdata)
            return;
        loadingdata = true;

        var Map = Areas[CB_LocationID.SelectedIndex];
        GB_Encounters.Enabled = Map.HasTables;
        if (!Map.HasTables)
        {
            loadingdata = false;
            return;
        }
        CurrentTable = new EncounterTable(Map.Tables[CB_TableID.SelectedIndex].Data);
        LoadTable(CurrentTable);

        loadingdata = false;
        RefreshTableImages(Map);
    }

    private void RefreshTableImages(Area7 Map)
    {
        int base_id = CB_TableID.SelectedIndex / 2;
        base_id *= 2;
        PB_DayTable.Image = Map.Tables[base_id].GetTableImg(font);
        PB_NightTable.Image = Map.Tables[base_id + 1].GetTableImg(font);
    }

    private void LoadTable(EncounterTable table)
    {
        NUP_Min.Value = table.MinLevel;
        NUP_Max.Minimum = table.MinLevel;
        NUP_Max.Value = table.MaxLevel;
        for (int slot = 0; slot < table.Encounter7s.Length; slot++)
        {
            for (int i = 0; i < table.Encounter7s[slot].Length; i++)
            {
                var sl = table.Encounter7s[slot];
                if (slot == 8)
                    sl = table.AdditionalSOS;
                rate_spec[i].Value = table.Rates[i];
                cb_spec[slot][i].SelectedIndex = (int)sl[i].Species;
                nup_spec[slot][i].Value = (int)sl[i].Forme;
            }
        }
    }

    private void UpdateMinMax(object sender, EventArgs e)
    {
        if (loadingdata)
            return;
        loadingdata = true;
        int min = (int)NUP_Min.Value;
        int max = (int)NUP_Max.Value;
        if (max < min)
        {
            max = min;
            NUP_Max.Value = max;
            NUP_Max.Minimum = min;
        }
        CurrentTable.MinLevel = min;
        CurrentTable.MaxLevel = max;
        loadingdata = false;
    }

    private void UpdateSpeciesForm(object sender, EventArgs e)
    {
        if (loadingdata)
            return;

        var cur_pb = CB_TableID.SelectedIndex % 2 == 0 ? PB_DayTable : PB_NightTable;
        var cur_img = cur_pb.Image;

        object[][] source = sender is NumericUpDown ? nup_spec : cb_spec;
        int table = Array.FindIndex(source, t => t.Contains(sender));
        int slot = Array.IndexOf(source[table], sender);

        var cb_l = cb_spec[table];
        var nup_l = nup_spec[table];
        var species = (uint)cb_l[slot].SelectedIndex;
        var form = (uint)nup_l[slot].Value;
        if (table == 8)
        {
            CurrentTable.AdditionalSOS[slot].Species = species;
            CurrentTable.AdditionalSOS[slot].Forme = form;
        }
        CurrentTable.Encounter7s[table][slot].Species = species;
        CurrentTable.Encounter7s[table][slot].Forme = form;

        using (var g = Graphics.FromImage(cur_img))
        {
            int x = 40 * slot;
            int y = 30 * (table + 1);
            if (table == 8)
            {
                x = (40 * slot) + 60;
                y = 270;
            }
            var pnt = new Point(x, y);
            g.SetClip(new Rectangle(pnt.X, pnt.Y, 40, 30), CombineMode.Replace);
            g.Clear(Color.Transparent);

            var enc = CurrentTable.Encounter7s[table][slot];
            g.DrawImage(enc.Species == 0 ? Properties.Resources.empty : WinFormsUtil.GetSprite((int)enc.Species, (int)enc.Forme, 0, 0, Main.Config), pnt);
        }

        cur_pb.Image = cur_img;
    }

    private void UpdateEncounterRate(object sender, EventArgs e)
    {
        if (loadingdata)
            return;

        var cur_pb = CB_TableID.SelectedIndex % 2 == 0 ? PB_DayTable : PB_NightTable;
        var cur_img = cur_pb.Image;

        int slot = Array.IndexOf(rate_spec, sender);
        int rate = (int)((NumericUpDown)sender).Value;
        CurrentTable.Rates[slot] = rate;

        using (var g = Graphics.FromImage(cur_img))
        {
            var pnt = new PointF((40 * slot) + 10, 10);
            g.SetClip(new Rectangle((int)pnt.X, (int)pnt.Y, 40, 14), CombineMode.Replace);
            g.Clear(Color.Transparent);
            g.DrawString($"{rate}%", font, Brushes.Black, pnt);
        }

        cur_pb.Image = cur_img;

        var sum = TotalEncounterRate;
        GB_Encounters.Text = $"Encounters ({sum}%)";
    }

    private byte[] CopyTable;
    private int CopyCount;

    private void B_Copy_Click(object sender, EventArgs e)
    {
        var Map = Areas[CB_LocationID.SelectedIndex];
        if (!Map.HasTables)
        {
            WinFormsUtil.Alert("No tables to copy.");
            return;
        }
        CurrentTable.Write();
        CopyTable = (byte[])CurrentTable.Data.Clone();
        CopyCount = CurrentTable.Encounter7s[0].Count(z => z.Species != 0);
        B_Paste.Enabled = B_PasteAll.Enabled = true;
        WinFormsUtil.Alert("Copied table data.");
    }

    private void B_Paste_Click(object sender, EventArgs e)
    {
        var Map = Areas[CB_LocationID.SelectedIndex];
        if (!Map.HasTables)
        {
            WinFormsUtil.Alert("No table to paste to.");
            return;
        }
        CurrentTable.Reset(CopyTable);
        loadingdata = true;
        LoadTable(CurrentTable);
        var area = Areas[CB_LocationID.SelectedIndex];
        area.Tables[CB_TableID.SelectedIndex] = CurrentTable;
        loadingdata = false;
        RefreshTableImages(Map);
        System.Media.SystemSounds.Asterisk.Play();
    }

    private void B_PasteAll_Click(object sender, EventArgs e)
    {
        var Map = Areas[CB_LocationID.SelectedIndex];
        if (!Map.HasTables)
        {
            WinFormsUtil.Alert("No table to paste to.");
            return;
        }
        B_Paste_Click(sender, e);
        foreach (var t in Map.Tables.Where(t => CopyCount == t.Encounter7s[0].Count(z => z.Species != 0)))
            t.Reset(CopyTable);
    }

    private void B_Save_Click(object sender, EventArgs e)
    {
        var sum = TotalEncounterRate;
        if (sum != 100 && sum != 0)
        {
            WinFormsUtil.Error("Encounter rates must add up to either 0% or 100%.");
            return;
        }

        CurrentTable.Write();
        var area = Areas[CB_LocationID.SelectedIndex];
        area.Tables[CB_TableID.SelectedIndex] = CurrentTable;

        // Set data back to GARC
        encdata[area.FileNumber] = Area7.GetDayNightTableBinary(area.Tables);
    }

    private void B_Export_Click(object sender, EventArgs e)
    {
        B_Save_Click(sender, e);

        Directory.CreateDirectory("encdata");
        foreach (var Map in Areas)
        {
            var packed = Area7.GetDayNightTableBinary(Map.Tables);
            File.WriteAllBytes(Path.Combine("encdata", Map.FileNumber.ToString()), packed);
        }
        WinFormsUtil.Alert("Exported all tables!");
    }

    private void DumpTables(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            FileName = "SM_Encounters.json",
            Filter = "JSON|*.json",
            Title = "Export Encounter Tables (JSON)"
        };
        if (sfd.ShowDialog() != DialogResult.OK)
            return;

        var data = new EncounterJson
        {
            Game = "SM",
            Areas = Areas.Select(BuildAreaJson).ToList()
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(sfd.FileName, json);
        WinFormsUtil.Alert($"Exported {data.Areas.Count} areas to JSON.");
    }

    private AreaJson BuildAreaJson(Area7 area)
    {
        var aj = new AreaJson
        {
            FileNumber = area.FileNumber,
            Name = area.Name,
            HasTables = area.HasTables,
            Tables = area.Tables.Select(BuildTableJson).ToList()
        };
        return aj;
    }

    private TableJson BuildTableJson(EncounterTable table)
    {
        var tj = new TableJson
        {
            MinLevel = table.MinLevel,
            MaxLevel = table.MaxLevel,
            Rates = (int[])table.Rates.Clone(),
            BaseSlots = BuildSlots(table.Encounter7s[0]),
            SosTables = table.Encounter7s.Skip(1).Take(7).Select(BuildSlots).ToList(),
            WeatherSlots = BuildSlots(table.AdditionalSOS)
        };
        return tj;
    }

    private List<SlotJson> BuildSlots(Encounter7[] slots)
    {
        return slots.Select(s => new SlotJson
        {
            Species = speciesList[(int)s.Species],
            Form = (int)s.Forme
        }).ToList();
    }

    private void B_ExportNames_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            FileName = "Pokemon_Names.txt",
            Filter = "Text File|*.txt",
            Title = "Export Pokémon Names"
        };
        if (sfd.ShowDialog() != DialogResult.OK) return;

        var names = speciesList.Where(n => !string.IsNullOrWhiteSpace(n) && n != "(None)").ToArray();
        File.WriteAllLines(sfd.FileName, names);
        WinFormsUtil.Alert($"Exported {names.Length} Pokémon names.");
    }

    private void B_Import_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Filter = "JSON|*.json",
            Title = "Import Encounter Tables (JSON)"
        };
        if (ofd.ShowDialog() != DialogResult.OK) return;

        EncounterJson data;
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            data = JsonSerializer.Deserialize<EncounterJson>(File.ReadAllText(ofd.FileName), options);
        }
        catch (Exception ex)
        {
            WinFormsUtil.Error("Failed to parse JSON", ex.Message);
            return;
        }

        if (data?.Areas == null)
        {
            WinFormsUtil.Alert("Invalid JSON format.");
            return;
        }

        int applied = 0;
        foreach (var aj in data.Areas)
        {
            var area = Areas.FirstOrDefault(a => a.FileNumber == aj.FileNumber);
            if (area == null || !area.HasTables || aj.Tables == null)
                continue;

            // Collect valid species across the whole area for fallback
            var valid = new List<int>();
            foreach (var tj in aj.Tables)
                CollectValid(tj, valid);

            for (int i = 0; i < Math.Min(area.Tables.Count, aj.Tables.Count); i++)
            {
                ApplyTable(area.Tables[i], aj.Tables[i], valid);
                area.Tables[i].Write();
            }

            encdata[area.FileNumber] = Area7.GetDayNightTableBinary(area.Tables);
            applied++;
        }

        // Refresh current view
        UpdatePanel(null, null);
        WinFormsUtil.Alert($"Imported and applied {applied} areas.");
    }

    private void CollectValid(TableJson tj, List<int> valid)
    {
        if (tj == null) return;
        void AddSlots(List<SlotJson> slots)
        {
            if (slots == null) return;
            foreach (var s in slots)
            {
                int idx = FindSpeciesIndex(s.Species);
                if (idx > 0 && !valid.Contains(idx))
                    valid.Add(idx);
            }
        }
        AddSlots(tj.BaseSlots);
        if (tj.SosTables != null)
            foreach (var sos in tj.SosTables)
                AddSlots(sos);
        AddSlots(tj.WeatherSlots);
    }

    private void ApplyTable(EncounterTable table, TableJson tj, List<int> validFallback)
    {
        if (tj == null) return;
        table.MinLevel = tj.MinLevel;
        table.MaxLevel = tj.MaxLevel;
        if (tj.Rates != null && tj.Rates.Length == table.Rates.Length)
            Array.Copy(tj.Rates, table.Rates, table.Rates.Length);

        ApplySlots(table.Encounter7s[0], tj.BaseSlots, validFallback);
        if (tj.SosTables != null)
        {
            for (int i = 0; i < Math.Min(7, tj.SosTables.Count); i++)
                ApplySlots(table.Encounter7s[i + 1], tj.SosTables[i], validFallback);
        }
        ApplySlots(table.AdditionalSOS, tj.WeatherSlots, validFallback);
    }

    private void ApplySlots(Encounter7[] target, List<SlotJson> source, List<int> validFallback)
    {
        if (target == null || source == null) return;
        int count = Math.Min(target.Length, source.Count);
        for (int i = 0; i < count; i++)
        {
            var s = source[i];
            int idx = FindSpeciesIndex(s.Species);
            if (idx <= 0 && validFallback.Count > 0)
                idx = validFallback[i % validFallback.Count];
            target[i].Species = (uint)idx;
            target[i].Forme = (uint)Math.Max(0, s.Form);
        }
    }

    private int FindSpeciesIndex(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name == "(None)" || name == "---")
            return 0;

        int idx = Array.IndexOf(speciesList, name);
        if (idx >= 0) return idx;

        idx = Array.FindIndex(speciesList, x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase));
        if (idx >= 0) return idx;

        int bestIdx = 0;
        int bestDist = int.MaxValue;
        string target = name.ToLowerInvariant();
        for (int i = 1; i < speciesList.Length; i++)
        {
            if (string.IsNullOrEmpty(speciesList[i])) continue;
            int dist = LevenshteinDistance(target, speciesList[i].ToLowerInvariant());
            if (dist < bestDist)
            {
                bestDist = dist;
                bestIdx = i;
            }
        }

        int maxAccept = Math.Max(1, name.Length / 5);
        if (bestDist <= maxAccept)
            return bestIdx;

        return 0;
    }

    private static int LevenshteinDistance(string a, string b)
    {
        if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;
        if (string.IsNullOrEmpty(b)) return a.Length;

        int n = a.Length;
        int m = b.Length;
        var d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        return d[n, m];
    }

    // JSON models
    private class EncounterJson
    {
        public string Game { get; set; } = "SM";
        public List<AreaJson> Areas { get; set; } = [];
    }

    private class AreaJson
    {
        public int FileNumber { get; set; }
        public string Name { get; set; } = "";
        public bool HasTables { get; set; }
        public List<TableJson> Tables { get; set; } = [];
    }

    private class TableJson
    {
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int[] Rates { get; set; } = [];
        public List<SlotJson> BaseSlots { get; set; } = [];
        public List<List<SlotJson>> SosTables { get; set; } = [];
        public List<SlotJson> WeatherSlots { get; set; } = [];
    }

    private class SlotJson
    {
        public string Species { get; set; } = "";
        public int Form { get; set; }
    }

    // Randomization & Bulk Modification
    private void B_Randomize_Click(object sender, EventArgs e)
    {
        if (DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Randomize all? Cannot undo.", "Double check Randomization settings at the bottom left."))
            return;

        Enabled = false;
        ExecuteRandomization();
        UpdatePanel(null, null);
        Enabled = true;

        WinFormsUtil.Alert("Randomized all Wild Encounters according to specification!", "Press the Dump Tables button to view the new Wild Encounter information!");
    }

    private void ExecuteRandomization()
    {
        var rnd = new SpeciesRandomizer(Main.Config)
        {
            G1 = CHK_G1.Checked,
            G2 = CHK_G2.Checked,
            G3 = CHK_G3.Checked,
            G4 = CHK_G4.Checked,
            G5 = CHK_G5.Checked,
            G6 = CHK_G6.Checked,
            G7 = CHK_G7.Checked,

            E = CHK_E.Checked,
            L = CHK_L.Checked,
            rBST = CHK_BST.Checked,
        };
        rnd.Initialize();
        var form = new FormRandomizer(Main.Config)
        {
            AllowMega = CHK_MegaForm.Checked,
            AllowAlolanForm = true,
        };
        var wild7 = new Wild7Randomizer
        {
            RandSpec = rnd,
            RandForm = form,
            TableRandomizationOption = CB_SlotRand.SelectedIndex,
            LevelAmplifier = NUD_LevelAmp.Value,
            ModifyLevel = CHK_Level.Checked,
        };
        wild7.Execute(Areas, encdata);
    }

    private void CopySOS_Click(object sender, EventArgs e)
    {
        if (WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Copy initial species to SOS slots?", "Cannot undo.") != DialogResult.Yes)
            return;

        // first table is copied to all other tables except weather (last)
        for (int i = 1; i < nup_spec.Length - 1; i++)
        {
            for (int s = 0; s < nup_spec[i].Length; s++) // slot copy
            {
                nup_spec[i][s].Value = nup_spec[0][s].Value;
                cb_spec[i][s].SelectedIndex = cb_spec[0][s].SelectedIndex;
            }
        }
        WinFormsUtil.Alert("All initial species copied to SOS slots!");
    }

    private void ModifyAllLevelRanges(object sender, EventArgs e)
    {
        if (DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Modify all current Level ranges?", "Cannot undo."))
            return;

        // Disable Interface while modifying
        Enabled = false;

        // Cycle through each location to modify levels
        var amp = NUD_LevelAmp.Value;
        foreach (var area in Areas)
        {
            var tables = area.Tables;
            foreach (var table in tables)
            {
                table.MinLevel = Randomizer.GetModifiedLevel(table.MinLevel, amp);
                table.MaxLevel = Randomizer.GetModifiedLevel(table.MaxLevel, amp);
                table.Write();
            }
            encdata[area.FileNumber] = Area7.GetDayNightTableBinary(tables);
        }

        // Enable Interface... modification complete.
        Enabled = true;
        WinFormsUtil.Alert("Modified all Level ranges according to specification!", "Press the Dump Tables button to view the new Level ranges!");

        UpdatePanel(sender, e);
    }

    // Utility
    /// <summary>
    /// Moves the sub-location names into the location name string entry.
    /// </summary>
    /// <param name="list">Raw location list</param>
    /// <returns>Cleaned location list</returns>
    public static string[] GetGoodLocationList(string[] list)
    {
        var bad = list;
        var good = (string[])bad.Clone();
        for (int i = 0; i < bad.Length; i += 2)
        {
            var nextLoc = bad[i + 1];
            if (!string.IsNullOrWhiteSpace(nextLoc) && nextLoc[0] != '[')
                good[i] += $" ({nextLoc})";
            if (i > 0 && !string.IsNullOrWhiteSpace(good[i]) && good.Take(i - 1).Contains(good[i]))
                good[i] += $" ({good.Take(i - 1).Count(s => s == good[i]) + 1})";
        }
        return good;
    }

    public void ExportEncounters(string gameID, string ident, bool sm)
    {
        var reg = Gen7SlotDumper.GetRegularBinary(Areas, sm);
        var sos = Gen7SlotDumper.GetSOSBinary(Areas, Main.Config.Personal, sm);

        File.WriteAllBytes($"encounter_{gameID}.pkl", Mini.PackMini(reg, ident));
        File.WriteAllBytes($"encounter_{gameID}_sos.pkl", Mini.PackMini(sos, ident));
    }

    private void SMWE_FormClosing(object sender, FormClosingEventArgs e)
    {
        RandSettings.SetFormSettings(this, GB_Tweak.Controls);
    }
}

public static class Extensions
{
    public static Bitmap GetTableImg(this EncounterTable table, Font font)
    {
        var img = new Bitmap(10 * 40, 10 * 30);
        using var g = Graphics.FromImage(img);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        for (int i = 0; i < table.Rates.Length; i++)
            g.DrawString($"{table.Rates[i]}%", font, Brushes.Black, new PointF((40 * i) + 10, 10));
        g.DrawString("Weather: ", font, Brushes.Black, new PointF(10, 280));

        // Draw Sprites
        for (int i = 0; i < table.Encounter7s.Length - 1; i++)
        {
            for (int j = 0; j < table.Encounter7s[i].Length; j++)
            {
                var slot = table.Encounter7s[i][j];
                var sprite = GetSprite((int)slot.Species, (int)slot.Forme);
                g.DrawImage(sprite, new Point(40 * j, 30 * (i + 1)));
            }
        }

        for (int i = 0; i < table.AdditionalSOS.Length; i++)
        {
            var slot = table.AdditionalSOS[i];
            var sprite = GetSprite((int)slot.Species, (int)slot.Forme);
            g.DrawImage(sprite, new Point((40 * i) + 60, 270));
        }

        static Bitmap GetSprite(int species, int form)
        {
            return species == 0
                ? Properties.Resources.empty
                : WinFormsUtil.GetSprite(species, form, 0, 0, Main.Config);
        }

        return img;
    }
}