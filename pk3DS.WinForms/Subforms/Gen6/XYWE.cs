using pk3DS.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using pk3DS.Core.Randomizers;

namespace pk3DS.WinForms;

public partial class XYWE : Form
{
    public XYWE()
    {
        InitializeComponent();
        All_Species =
        [
            CB_Grass1, CB_Grass2, CB_Grass3, CB_Grass4, CB_Grass5, CB_Grass6, CB_Grass7, CB_Grass8, CB_Grass9, CB_Grass10, CB_Grass11, CB_Grass12,
            CB_Yellow1, CB_Yellow2, CB_Yellow3, CB_Yellow4, CB_Yellow5, CB_Yellow6, CB_Yellow7, CB_Yellow8, CB_Yellow9, CB_Yellow10, CB_Yellow11, CB_Yellow12,
            CB_Purple1, CB_Purple2, CB_Purple3, CB_Purple4, CB_Purple5, CB_Purple6, CB_Purple7, CB_Purple8, CB_Purple9, CB_Purple10, CB_Purple11, CB_Purple12,
            CB_Red1, CB_Red2, CB_Red3, CB_Red4, CB_Red5, CB_Red6, CB_Red7, CB_Red8, CB_Red9, CB_Red10, CB_Red11, CB_Red12,
            CB_RT1, CB_RT2, CB_RT3, CB_RT4, CB_RT5, CB_RT6, CB_RT7, CB_RT8, CB_RT9, CB_RT10, CB_RT11, CB_RT12,
            CB_Surf1, CB_Surf2, CB_Surf3, CB_Surf4, CB_Surf5,
            CB_RockSmash1, CB_RockSmash2, CB_RockSmash3, CB_RockSmash4, CB_RockSmash5,
            CB_Old1, CB_Old2, CB_Old3,
            CB_Good1, CB_Good2, CB_Good3,
            CB_Super1, CB_Super2, CB_Super3,
            CB_HordeA1, CB_HordeA2, CB_HordeA3, CB_HordeA4, CB_HordeA5,
            CB_HordeB1, CB_HordeB2, CB_HordeB3, CB_HordeB4, CB_HordeB5,
            CB_HordeC1, CB_HordeC2, CB_HordeC3, CB_HordeC4, CB_HordeC5,
        ];
        All_Min =
        [
            NUP_GrassMin1, NUP_GrassMin2, NUP_GrassMin3, NUP_GrassMin4, NUP_GrassMin5, NUP_GrassMin6, NUP_GrassMin7, NUP_GrassMin8, NUP_GrassMin9, NUP_GrassMin10, NUP_GrassMin11, NUP_GrassMin12,
            NUP_YellowMin1, NUP_YellowMin2, NUP_YellowMin3, NUP_YellowMin4, NUP_YellowMin5, NUP_YellowMin6, NUP_YellowMin7, NUP_YellowMin8, NUP_YellowMin9, NUP_YellowMin10, NUP_YellowMin11, NUP_YellowMin12,
            NUP_PurpleMin1, NUP_PurpleMin2, NUP_PurpleMin3, NUP_PurpleMin4, NUP_PurpleMin5, NUP_PurpleMin6, NUP_PurpleMin7, NUP_PurpleMin8, NUP_PurpleMin9, NUP_PurpleMin10, NUP_PurpleMin11, NUP_PurpleMin12,
            NUP_RedMin1, NUP_RedMin2, NUP_RedMin3, NUP_RedMin4, NUP_RedMin5, NUP_RedMin6, NUP_RedMin7, NUP_RedMin8, NUP_RedMin9, NUP_RedMin10, NUP_RedMin11, NUP_RedMin12,
            NUP_RTMin1, NUP_RTMin2, NUP_RTMin3, NUP_RTMin4, NUP_RTMin5, NUP_RTMin6, NUP_RTMin7, NUP_RTMin8, NUP_RTMin9, NUP_RTMin10, NUP_RTMin11, NUP_RTMin12,
            NUP_SurfMin1, NUP_SurfMin2, NUP_SurfMin3, NUP_SurfMin4, NUP_SurfMin5,
            NUP_RockSmashMin1, NUP_RockSmashMin2, NUP_RockSmashMin3, NUP_RockSmashMin4, NUP_RockSmashMin5,
            NUP_OldMin1, NUP_OldMin2, NUP_OldMin3,
            NUP_GoodMin1, NUP_GoodMin2, NUP_GoodMin3,
            NUP_SuperMin1, NUP_SuperMin2, NUP_SuperMin3,
            NUP_HordeAMin1, NUP_HordeAMin2, NUP_HordeAMin3, NUP_HordeAMin4, NUP_HordeAMin5,
            NUP_HordeBMin1, NUP_HordeBMin2, NUP_HordeBMin3, NUP_HordeBMin4, NUP_HordeBMin5,
            NUP_HordeCMin1, NUP_HordeCMin2, NUP_HordeCMin3, NUP_HordeCMin4, NUP_HordeCMin5,
        ];
        All_Max =
        [
            NUP_GrassMax1, NUP_GrassMax2, NUP_GrassMax3, NUP_GrassMax4, NUP_GrassMax5, NUP_GrassMax6, NUP_GrassMax7, NUP_GrassMax8, NUP_GrassMax9, NUP_GrassMax10, NUP_GrassMax11, NUP_GrassMax12,
            NUP_YellowMax1, NUP_YellowMax2, NUP_YellowMax3, NUP_YellowMax4, NUP_YellowMax5, NUP_YellowMax6, NUP_YellowMax7, NUP_YellowMax8, NUP_YellowMax9, NUP_YellowMax10, NUP_YellowMax11, NUP_YellowMax12,
            NUP_PurpleMax1, NUP_PurpleMax2, NUP_PurpleMax3, NUP_PurpleMax4, NUP_PurpleMax5, NUP_PurpleMax6, NUP_PurpleMax7, NUP_PurpleMax8, NUP_PurpleMax9, NUP_PurpleMax10, NUP_PurpleMax11, NUP_PurpleMax12,
            NUP_RedMax1, NUP_RedMax2, NUP_RedMax3, NUP_RedMax4, NUP_RedMax5, NUP_RedMax6, NUP_RedMax7, NUP_RedMax8, NUP_RedMax9, NUP_RedMax10, NUP_RedMax11, NUP_RedMax12,
            NUP_RTMax1, NUP_RTMax2, NUP_RTMax3, NUP_RTMax4, NUP_RTMax5, NUP_RTMax6, NUP_RTMax7, NUP_RTMax8, NUP_RTMax9, NUP_RTMax10, NUP_RTMax11, NUP_RTMax12,
            NUP_SurfMax1, NUP_SurfMax2, NUP_SurfMax3, NUP_SurfMax4, NUP_SurfMax5,
            NUP_RockSmashMax1, NUP_RockSmashMax2, NUP_RockSmashMax3, NUP_RockSmashMax4, NUP_RockSmashMax5,
            NUP_OldMax1, NUP_OldMax2, NUP_OldMax3,
            NUP_GoodMax1, NUP_GoodMax2, NUP_GoodMax3,
            NUP_SuperMax1, NUP_SuperMax2, NUP_SuperMax3,
            NUP_HordeAMax1, NUP_HordeAMax2, NUP_HordeAMax3, NUP_HordeAMax4, NUP_HordeAMax5,
            NUP_HordeBMax1, NUP_HordeBMax2, NUP_HordeBMax3, NUP_HordeBMax4, NUP_HordeBMax5,
            NUP_HordeCMax1, NUP_HordeCMax2, NUP_HordeCMax3, NUP_HordeCMax4, NUP_HordeCMax5,
        ];
        All_Form =
        [
            NUP_GrassForme1, NUP_GrassForme2, NUP_GrassForme3, NUP_GrassForme4, NUP_GrassForme5, NUP_GrassForme6, NUP_GrassForme7, NUP_GrassForme8, NUP_GrassForme9, NUP_GrassForme10, NUP_GrassForme11, NUP_GrassForme12,
            NUP_YellowForme1, NUP_YellowForme2, NUP_YellowForme3, NUP_YellowForme4, NUP_YellowForme5, NUP_YellowForme6, NUP_YellowForme7, NUP_YellowForme8, NUP_YellowForme9, NUP_YellowForme10, NUP_YellowForme11, NUP_YellowForme12,
            NUP_PurpleForme1, NUP_PurpleForme2, NUP_PurpleForme3, NUP_PurpleForme4, NUP_PurpleForme5, NUP_PurpleForme6, NUP_PurpleForme7, NUP_PurpleForme8, NUP_PurpleForme9, NUP_PurpleForme10, NUP_PurpleForme11, NUP_PurpleForme12,
            NUP_RedForme1, NUP_RedForme2, NUP_RedForme3, NUP_RedForme4, NUP_RedForme5, NUP_RedForme6, NUP_RedForme7, NUP_RedForme8, NUP_RedForme9, NUP_RedForme10, NUP_RedForme11, NUP_RedForme12,
            NUP_RTForme1, NUP_RTForme2, NUP_RTForme3, NUP_RTForme4, NUP_RTForme5, NUP_RTForme6, NUP_RTForme7, NUP_RTForme8, NUP_RTForme9, NUP_RTForme10, NUP_RTForme11, NUP_RTForme12,
            NUP_SurfForme1, NUP_SurfForme2, NUP_SurfForme3, NUP_SurfForme4, NUP_SurfForme5,
            NUP_RockSmashForme1, NUP_RockSmashForme2, NUP_RockSmashForme3, NUP_RockSmashForme4, NUP_RockSmashForme5,
            NUP_OldForme1, NUP_OldForme2, NUP_OldForme3,
            NUP_GoodForme1, NUP_GoodForme2, NUP_GoodForme3,
            NUP_SuperForme1, NUP_SuperForme2, NUP_SuperForme3,
            NUP_HordeAForme1, NUP_HordeAForme2, NUP_HordeAForme3, NUP_HordeAForme4, NUP_HordeAForme5,
            NUP_HordeBForme1, NUP_HordeBForme2, NUP_HordeBForme3, NUP_HordeBForme4, NUP_HordeBForme5,
            NUP_HordeCForme1, NUP_HordeCForme2, NUP_HordeCForme3, NUP_HordeCForme4, NUP_HordeCForme5,
        ];
        formlist =
        [
            "Unown-A - 0",
            "Unown-B - 1",
            "Unown-C - 2",
            "Unown-D - 3",
            "Unown-E - 4",
            "Unown-F - 5",
            "Unown-G - 6",
            "Unown-H - 7",
            "Unown-I - 8",
            "Unown-J - 9",
            "Unown-K - 10",
            "Unown-L - 11",
            "Unown-M - 12",
            "Unown-N - 13",
            "Unown-O - 14",
            "Unown-P - 15",
            "Unown-Q - 16",
            "Unown-R - 17",
            "Unown-S - 18",
            "Unown-T - 19",
            "Unown-U - 20",
            "Unown-V - 21",
            "Unown-W - 22",
            "Unown-X - 23",
            "Unown-Y - 24",
            "Unown-Z - 25",
            "Unown-! - 26",
            "Unown-? - 27",
            "",
            "Castform-Normal - 0",
            "Castform-Sunny - 1",
            "Castform-Rainy - 2",
            "Castform-Snowy - 3",
            "",
            "Deoxys-Normal - 0",
            "Deoxys-Attack - 1",
            "Deoxys-Defense - 2",
            "Deoxys-Speed - 3",
            "",
            "Burmy-Plant Cloak - 0",
            "Burmy-Sandy Cloak - 1",
            "Burmy-Trash Cloak - 2",
            "",
            "Wormadam-Plant Cloak - 0",
            "Wormadam-Sandy Cloak - 1",
            "Wormadam-Trash Cloak - 2",
            "",
            "Cherrim-Overcast - 0",
            "Cherrim-Sunshine - 1",
            "",
            "Shellos-West Sea - 0",
            "Shellos-East Sea - 1",
            "",
            "Gastrodon-West Sea - 0",
            "Gastrodon-East Sea - 1",
            "",
            "Rotom-Normal - 0",
            "Rotom-Heat - 1",
            "Rotom-Wash - 2",
            "Rotom-Frost - 3",
            "Rotom-Fan - 4",
            "Rotom-Mow - 5",
            "",
            "Giratina-Altered - 0",
            "Giratina-Origin - 1",
            "",
            "Shaymin-Land - 0",
            "Shaymin-Sky - 1",
            "",
            "Arceus-Normal - 0",
            "Arceus-Fighting - 1",
            "Arceus-Flying - 2",
            "Arceus-Poison - 3",
            "Arceus-Ground - 4",
            "Arceus-Rock - 5",
            "Arceus-Bug - 6",
            "Arceus-Ghost - 7",
            "Arceus-Steel - 8",
            "Arceus-Fire - 9",
            "Arceus-Water - 10",
            "Arceus-Grass - 11",
            "Arceus-Electric - 12",
            "Arceus-Psychic - 13",
            "Arceus-Ice - 14",
            "Arceus-Dragon - 15",
            "Arceus-Dark - 16",
            "Arceus-Fairy - 17",
            "",
            "Basculin-Red-Striped - 0",
            "Basculin-Blue-Striped - 1",
            "",
            "Darmanitan-Standard Mode - 0",
            "Darmanitan-Zen Mode - 1",
            "",
            "Deerling-Spring - 0",
            "Deerling-Summer - 1",
            "Deerling-Autumn - 2",
            "Deerling-Winter - 3",
            "",
            "Sawsbuck-Spring - 0",
            "Sawsbuck-Summer - 1",
            "Sawsbuck-Autumn - 2",
            "Sawsbuck-Winter - 3",
            "",
            "Tornadus-Incarnate - 0",
            "Tornadus-Therian - 1",
            "",
            "Thundurus-Incarnate - 0",
            "Thundurus-Therian - 1",
            "",
            "Landorus-Incarnate - 0",
            "Landorus-Therian - 1",
            "",
            "Kyurem-Normal - 0",
            "Kyurem-White - 1",
            "Kyurem-Black - 2",
            "",
            "Keldeo-Ordinary - 0",
            "Keldeo-Resolute - 1",
            "",
            "Meloetta-Aria - 0",
            "Meloetta-Pirouette - 1",
            "",
            "Genesect-Normal - 0",
            "Genesect-Water - 1",
            "Genesect-Electric - 2",
            "Genesect-Fire - 3",
            "Genesect-Ice - 4",
            "",
            "Flabebe-Red - 0",
            "Flabebe-Yellow - 1",
            "Flabebe-Orange - 2",
            "Flabebe-Blue - 3",
            "Flabebe-White - 4",
            "",
            "Floette-Red - 0",
            "Floette-Yellow - 1",
            "Floette-Orange - 2",
            "Floette-Blue - 3",
            "Floette-White - 4",
            "Floette-Eternal - 5",
            "",
            "Florges-Red - 0",
            "Florges-Yellow - 1",
            "Florges-Orange - 2",
            "Florges-Blue - 3",
            "Florges-White - 4",
            "",
            "Furfrou-Natural - 0",
            "Furfrou-Heart - 1",
            "Furfrou-Star - 2",
            "Furfrou-Diamond - 3",
            "Furfrou-Deputante - 4",
            "Furfrou-Matron - 5",
            "Furfrou-Dandy - 6",
            "Furfrou-La Reine- 7",
            "Furfrou-Kabuki - 8",
            "Furfrou-Pharaoh - 9",
            "",
            "Aegislash-Shield - 0",
            "Aegislash-Blade - 0",
            "",
            "Vivillon-Icy Snow - 0",
            "Vivillon-Polar - 1",
            "Vivillon-Tundra - 2",
            "Vivillon-Continental  - 3",
            "Vivillon-Garden - 4",
            "Vivillon-Elegant - 5",
            "Vivillon-Meadow - 6",
            "Vivillon-Modern  - 7",
            "Vivillon-Marine - 8",
            "Vivillon-Archipelago - 9",
            "Vivillon-High-Plains - 10",
            "Vivillon-Sandstorm - 11",
            "Vivillon-River - 12",
            "Vivillon-Monsoon - 13",
            "Vivillon-Savannah  - 14",
            "Vivillon-Sun - 15",
            "Vivillon-Ocean - 16",
            "Vivillon-Jungle - 17",
            "Vivillon-Fancy - 18",
            "Vivillon-Poké Ball - 19",
            "",
            "Pumpkaboo-Small - 0",
            "Pumpkaboo-Average - 1",
            "Pumpkaboo-Large - 2",
            "Pumpkaboo-Super - 3",
            "",
            "Gourgeist-Small - 0",
            "Gourgeist-Average - 1",
            "Gourgeist-Large - 2",
            "Gourgeist-Super - 3",
            "",
            "Megas-Normal - 0",
            "Megas-Mega (X) - 1",
            "Megas-Mega (Y) - 2",
        ];
        Load_XYWE();
        SetupImportButton();
        OpenQuick(Directory.GetFiles("encdata"));
        RandSettings.GetFormSettings(this, GB_Tweak.Controls);
    }

    private readonly ComboBox[] All_Species;
    private readonly NumericUpDown[] All_Min;
    private readonly NumericUpDown[] All_Max;
    private readonly NumericUpDown[] All_Form;
    private string[] specieslist = [];
    private readonly string[] formlist = [];
    private string[] metXY_00000 = [];
    private byte[] zonedata = [];
    private string[] LocationNames = [];
    private string[] encdatapaths;
    private string[] filepaths;
    private Button B_Import;
    private Button B_ExportNames;

    private static readonly int[] Rates12 = [10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 4, 1];
    private static readonly int[] Rates5 = [50, 30, 15, 4, 1];
    private static readonly int[] Rates3 = [60, 35, 5];
    private static readonly int[] RatesHordeA = [60, 60, 60, 60, 60];
    private static readonly int[] RatesHordeB = [35, 35, 35, 35, 35];
    private static readonly int[] RatesHordeC = [5, 5, 5, 5, 5];

    private const int OffGrass = 0;
    private const int OffYellow = 12;
    private const int OffPurple = 24;
    private const int OffRed = 36;
    private const int OffRT = 48;
    private const int OffSurf = 60;
    private const int OffRock = 65;
    private const int OffOld = 70;
    private const int OffGood = 73;
    private const int OffSuper = 76;
    private const int OffHordeA = 79;
    private const int OffHordeB = 84;
    private const int OffHordeC = 89;

    private void SetupImportButton()
    {
        B_Dump.Text = "Dump JSON";
        B_Import = new Button
        {
            Text = "Import JSON",
            Location = new Point(B_Dump.Right + 6, B_Dump.Top),
            Size = B_Dump.Size,
            Enabled = false,
            UseVisualStyleBackColor = true
        };
        B_Import.Click += B_Import_Click;
        Controls.Add(B_Import);

        B_ExportNames = new Button
        {
            Text = "Export Names",
            Location = new Point(B_Import.Right + 6, B_Import.Top),
            Size = B_Dump.Size,
            Enabled = false,
            UseVisualStyleBackColor = true
        };
        B_ExportNames.Click += B_ExportNames_Click;
        Controls.Add(B_ExportNames);
    }

    private void Load_XYWE()
    {
        specieslist = Main.Config.GetText(TextName.SpeciesNames);
        specieslist[0] = "---";

        CB_FormeList.Items.AddRange(formlist);

        // Clear & Reset Data
        for (int i = 0; i < All_Max.Length; i++)
        {
            All_Species[i].Items.Clear();
            All_Species[i].Items.AddRange(specieslist);
            All_Species[i].SelectedIndex = 0;
        }
    }

    private void OpenQuick(string[] encdata)
    {
        encdatapaths = encdata;
        Array.Sort(encdatapaths);
        filepaths = new string[encdatapaths.Length - 1];
        Array.Copy(encdatapaths, 1, filepaths, 0, filepaths.Length);
        metXY_00000 = Main.Config.GetText(TextName.metlist_000000);
        zonedata = File.ReadAllBytes(encdatapaths[0]);
        LocationNames = new string[filepaths.Length];
        for (int f = 0; f < filepaths.Length; f++)
        {
            string name = Path.GetFileNameWithoutExtension(filepaths[f]);

            int LocationNum = Convert.ToInt16(name[4..]);
            int indNum = (LocationNum * 56) + 0x1C;
            string LocationName = metXY_00000[zonedata[indNum] + (0x100 * (zonedata[indNum + 1] & 1))];
            LocationNames[f] = LocationNum.ToString("000") + " - " + LocationName;
        }
        CB_LocationID.DataSource = LocationNames;
        B_Save.Enabled = B_Dump.Enabled = B_Randomize.Enabled = true;
        if (B_Import != null) B_Import.Enabled = true;
        if (B_ExportNames != null) B_ExportNames.Enabled = true;
        CB_LocationID.Enabled = true;
        CB_LocationID_SelectedIndexChanged(null, null);
    }

    private bool HasData()
    {
        for (int i = 0; i < All_Max.Length; i++)
        {
            if (All_Species[i].SelectedIndex > 0) { return true; }
            if (All_Form[i].Value > 0) { return true; }
            if (All_Min[i].Value > 0) { return true; }
            if (All_Max[i].Value > 0) { return true; }
        }
        return false;
    }

    private void ParseEncounterTable(byte[] ed)
    {
        // 12,12,12,12,12
        // 5,5
        // 3,3,3
        // 5,5,5,
        byte[] slot = new byte[4];
        const int offset = 0x0;

        // read data into form
        for (int i = 0; i < All_Max.Length; i++)
        {
            // Fetch Data
            Array.Copy(ed, offset + (i * 4), slot, 0, 4);
            int[] data = ParseSlot(slot);

            // Load Data
            All_Species[i].SelectedIndex = data[0];
            All_Form[i].Value = data[1];
            All_Min[i].Value = data[2];
            All_Max[i].Value = data[3];
        }

#if DUMPER
            int r = CB_LocationID.SelectedIndex * 56 + 0x1C;
            int loc = zonedata[r] + 0x100 * (zonedata[r + 1] & 1);
            byte[] edata = BitConverter.GetBytes((ushort)loc).Concat(ed).ToArray();

            if (!Directory.Exists("encounter_xy"))
                Directory.CreateDirectory("encounter_xy");
            File.WriteAllBytes(Path.Combine("encounter_xy", loc.ToString("000") + CB_LocationID.SelectedIndex.ToString("000") + ".bin"), edata);
#endif
    }

    private static int[] ParseSlot(byte[] slot)
    {
        int index = BitConverter.ToUInt16(slot, 0) & 0x7FF;
        int form = BitConverter.ToUInt16(slot, 0) >> 11;
        int min = slot[2];
        int max = slot[3];
        return [index, form, min, max];
    }

    public string ConvertSlotToString(byte[] slot)
    {
        int index = BitConverter.ToUInt16(slot, 0) & 0x7FF;
        if (index == 0) return "";
        int form = BitConverter.ToUInt16(slot, 0) >> 11;
        int min = slot[2];
        int max = slot[3];
        string species = specieslist[index];
        if (form > 0) species += "-" + form;
        return $"{species},{min},{max},";
    }

    private void CB_LocationID_SelectedIndexChanged(object sender, EventArgs e)
    {
        int f = CB_LocationID.SelectedIndex;
        int offset;
        using (var s = File.OpenRead(filepaths[f]))
        using (var br = new BinaryReader(s))
        {
            br.BaseStream.Seek(0x10, SeekOrigin.Begin);
            offset = br.ReadInt32() + 0x10;
            int length = (int)br.BaseStream.Length - offset;
            if (length < 0x178) //no encounters in this map
            {
                ClearData();
                return;
            }
        }

        byte[] filedata = File.ReadAllBytes(filepaths[f]);

        byte[] encounterdata = new byte[0x178];
        Array.Copy(filedata, offset, encounterdata, 0, 0x178);
        ParseEncounterTable(encounterdata);
    }

    private void ClearData()
    {
        for (int i = 0; i < All_Max.Length; i++)
        {
            // Load Data
            All_Species[i].SelectedIndex = 0;
            All_Form[i].Value = 0;
            All_Min[i].Value = 0;
            All_Max[i].Value = 0;
        }
    }

    private static byte[] MakeSlotData(int species, int f, int lo, int hi)
    {
        byte[] data = new byte[4];
        Array.Copy(BitConverter.GetBytes(Convert.ToUInt16((Convert.ToUInt16(f) << 11) + Convert.ToUInt16(species))), 0, data, 0, 2);
        data[2] = (byte)lo;
        data[3] = (byte)hi;
        return data;
    }

    private byte[] MakeEncounterData()
    {
        byte[] ed = new byte[0x178];
        const int offset = 0x0;
        for (int i = 0; i < All_Max.Length; i++)
        {
            byte[] data = MakeSlotData(All_Species[i].SelectedIndex, (int)All_Form[i].Value, (int)All_Min[i].Value, (int)All_Max[i].Value);
            Array.Copy(data, 0, ed, offset + (i * 4), 4);
        }
        return ed;
    }

    private static byte[] ConcatArrays(byte[] b1, byte[] b2)
    {
        return [..b1, ..b2];
    }

    private void B_Save_Click(object sender, EventArgs e)
    {
        int f = CB_LocationID.SelectedIndex;
        string filepath = filepaths[f];
        byte[] filedata = File.ReadAllBytes(filepath);
        int offset = BitConverter.ToInt32(filedata, 0x10) + 0x10;
        byte[] preoffset;
        if (offset < filedata.Length)
        {
            preoffset = new byte[offset];
            Array.Copy(filedata, preoffset, offset);
        }
        else
        {
            preoffset = new byte[filedata.Length];
            Array.Copy(filedata, preoffset, filedata.Length);
            //overwrite offset so the game actually looks at the data
            Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(filedata.Length)), 0, preoffset, 0x10, 4);
        }
        byte[] encdata = [];
        if (HasData()) { encdata = MakeEncounterData(); }
        byte[] newdata = ConcatArrays(preoffset, encdata);
        File.WriteAllBytes(filepath, newdata);
    }

    private void PreloadTabs(object sender, EventArgs e)
    {
        for (int i = 0; i < TabControl_EncounterData.TabPages.Count; i++)
        {
            TabControl_EncounterData.TabPages[i].Show();
            Console.WriteLine("Loading Tab " + (i + 1).ToString("0") + ".");
        }
        TabControl_EncounterData.TabPages[0].Show();
    }

    internal static uint Random32() => Util.Random32();

    private void B_Randomize_Click(object sender, EventArgs e)
    {
        if (WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Randomize all? Cannot undo.", "Double check Randomization settings in the Horde tab.") != DialogResult.Yes) return;

        Enabled = false;

        // Calculate % diff we will apply to each level
        decimal leveldiff = (100 + NUD_LevelAmp.Value) / 100;

        var rand = new SpeciesRandomizer(Main.Config)
        {
            G1 = CHK_G1.Checked,
            G2 = CHK_G2.Checked,
            G3 = CHK_G3.Checked,
            G4 = CHK_G4.Checked,
            G5 = CHK_G5.Checked,
            G6 = CHK_G6.Checked,

            L = CHK_L.Checked,
            E = CHK_E.Checked,
            Shedinja = false,

            rBST = CHK_BST.Checked,
        };
        rand.Initialize();

        for (int i = 0; i < CB_LocationID.Items.Count; i++) // for every location
        {
            CB_LocationID.SelectedIndex = i;
            if (!HasData()) continue; // Don't randomize if doesn't have data.

            // Assign Levels
            if (CHK_Level.Checked)
            {
                for (int l = 0; l < All_Max.Length; l++)
                    All_Min[l].Value = All_Max[l].Value = All_Max[l].Value <= 1 ? All_Max[l].Value : Math.Max(1, Math.Min(100, (int)(leveldiff * All_Max[l].Value)));
            }

            // If Distinct Hordes are selected, homogenize
            int hordeslot = 0;
            for (int slot = 0; slot < All_Max.Length; slot++)
            {
                if (All_Species[slot].SelectedIndex == 0) continue;
                if (CHK_HomogeneousHordes.Checked && slot >= All_Max.Length - 15)
                {
                    int shift = hordeslot % 5;
                    hordeslot++;
                    if (shift != 0)
                    {
                        All_Species[slot].SelectedIndex = All_Species[slot - shift].SelectedIndex;
                        All_Form[slot].Value = All_Form[slot - shift].Value;
                        continue;
                    }
                }

                int species = rand.GetRandomSpecies(All_Species[slot].SelectedIndex);
                All_Species[slot].SelectedIndex = species;
                SetRandomForm(slot, species);
            }
            B_Save_Click(sender, e);
        }
        Enabled = true;
        WinFormsUtil.Alert("Randomized all Wild Encounters according to specification!", "Press the Dump Tables button to view the new Wild Encounter information!");
    }

    private void SetRandomForm(int slot, int species)
    {
        if (CHK_MegaForm.Checked && Main.SpeciesStat[species].FormeCount > 1 && Legal.Mega_XY.Contains((ushort)species))
            All_Form[slot].Value = Random32() % Main.SpeciesStat[species].FormeCount; // Slot-Random
        else if (species is 666 or 665 or 664) // Vivillon
            All_Form[slot].Value = Random32() % 20;
        else if (species == 386) // Deoxys
            All_Form[slot].Value = Random32() % 4;
        else if (species == 201) // Unown
            All_Form[slot].Value = 31;
        else if (species == 550) // Basculin
            All_Form[slot].Value = Random32() % 2;
        else if (species is 412 or 413) // Wormadam
            All_Form[slot].Value = Random32() % 3;
        else if (species is 422 or 423) // Gastrodon
            All_Form[slot].Value = Random32() % 2;
        else if (species is 585 or 586) // Sawsbuck
            All_Form[slot].Value = Random32() % 4;
        else if (species is 669 or 671) // Flabebe/Florges
            All_Form[slot].Value = Random32() % 5;
        else if (species == 670) // Floette
            All_Form[slot].Value = Random32() % 6;
        else if (species is 710 or 711) // Pumpkaboo
            All_Form[slot].Value = Random32() % 4;
        else
            All_Form[slot].Value = 0;
    }

    private void B_Dump_Click(object sender, EventArgs e)
    {
        var data = new EncounterJson { Game = "XY" };
        int originalIndex = CB_LocationID.SelectedIndex;
        for (int i = 0; i < LocationNames.Length; i++)
        {
            CB_LocationID.SelectedIndex = i;
            data.Locations.Add(BuildLocationJson(i));
        }
        if (originalIndex >= 0 && originalIndex < LocationNames.Length)
            CB_LocationID.SelectedIndex = originalIndex;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        string json = JsonSerializer.Serialize(data, options);

        using var save = new SaveFileDialog
        {
            FileName = "XY_Encounters.json",
            Filter = "JSON File|*.json",
            Title = "Export Encounter Tables as JSON"
        };
        if (save.ShowDialog() != DialogResult.OK) return;
        File.WriteAllText(save.FileName, json);
        WinFormsUtil.Alert("Exported all encounter tables to JSON.");
    }


    private void B_ExportNames_Click(object sender, EventArgs e)
    {
        if (specieslist == null || specieslist.Length == 0)
        {
            WinFormsUtil.Alert("Species list not loaded.");
            return;
        }

        using var save = new SaveFileDialog
        {
            FileName = "Pokemon_Names.txt",
            Filter = "Text File|*.txt",
            Title = "Export Pokémon Names"
        };
        if (save.ShowDialog() != DialogResult.OK) return;

        // One name per line, skip the empty "---" entry at index 0 if desired, but include all for completeness
        var names = specieslist.Where(n => !string.IsNullOrWhiteSpace(n) && n != "---").ToArray();
        File.WriteAllLines(save.FileName, names);
        WinFormsUtil.Alert($"Exported {names.Length} Pokémon names.");
    }

    private void B_Import_Click(object sender, EventArgs e)
    {
        using var open = new OpenFileDialog
        {
            Filter = "JSON File|*.json",
            Title = "Import Encounter Tables from JSON"
        };
        if (open.ShowDialog() != DialogResult.OK) return;

        string json = File.ReadAllText(open.FileName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        EncounterJson data;
        try
        {
            data = JsonSerializer.Deserialize<EncounterJson>(json, options);
        }
        catch (Exception ex)
        {
            WinFormsUtil.Error("Failed to parse JSON: " + ex.Message);
            return;
        }

        if (data?.Locations == null || data.Locations.Count == 0)
        {
            WinFormsUtil.Alert("No locations found in JSON.");
            return;
        }

        int originalIndex = CB_LocationID.SelectedIndex;
        int applied = 0;
        foreach (var loc in data.Locations)
        {
            int idx = FindLocationIndex(loc);
            if (idx < 0) continue;
            CB_LocationID.SelectedIndex = idx;
            ApplyLocation(loc);
            B_Save_Click(null, EventArgs.Empty);
            applied++;
        }
        if (originalIndex >= 0 && originalIndex < LocationNames.Length)
            CB_LocationID.SelectedIndex = originalIndex;

        WinFormsUtil.Alert($"Imported encounters for {applied} location(s).");
    }

    private LocationJson BuildLocationJson(int index)
    {
        var loc = new LocationJson { Index = index };
        string fullName = LocationNames[index];
        var parts = fullName.Split([" - "], 2, StringSplitOptions.None);
        loc.Id = int.TryParse(parts[0], out int id) ? id : index;
        loc.Name = parts.Length > 1 ? parts[1] : fullName;
        loc.HasEncounters = HasData();
        if (!loc.HasEncounters) return loc;

        loc.Slots.Grass = BuildSlots(OffGrass, 12, Rates12);
        loc.Slots.YellowFlowers = BuildSlots(OffYellow, 12, Rates12);
        loc.Slots.PurpleFlowers = BuildSlots(OffPurple, 12, Rates12);
        loc.Slots.RedFlowers = BuildSlots(OffRed, 12, Rates12);
        loc.Slots.RoughTerrain = BuildSlots(OffRT, 12, Rates12);
        loc.Slots.Surfing = BuildSlots(OffSurf, 5, Rates5);
        loc.Slots.RockSmash = BuildSlots(OffRock, 5, Rates5);
        loc.Slots.OldRod = BuildSlots(OffOld, 3, Rates3);
        loc.Slots.GoodRod = BuildSlots(OffGood, 3, Rates3);
        loc.Slots.SuperRod = BuildSlots(OffSuper, 3, Rates3);
        loc.Slots.HordeA = BuildSlots(OffHordeA, 5, RatesHordeA);
        loc.Slots.HordeB = BuildSlots(OffHordeB, 5, RatesHordeB);
        loc.Slots.HordeC = BuildSlots(OffHordeC, 5, RatesHordeC);
        return loc;
    }

    private List<SlotJson> BuildSlots(int offset, int count, int[] rates)
    {
        var list = new List<SlotJson>(count);
        for (int i = 0; i < count; i++)
        {
            int idx = offset + i;
            string species = All_Species[idx].Text;
            if (string.IsNullOrWhiteSpace(species) || species == "---")
                species = "---";
            list.Add(new SlotJson
            {
                Species = species,
                Form = (int)All_Form[idx].Value,
                MinLevel = (int)All_Min[idx].Value,
                MaxLevel = (int)All_Max[idx].Value,
                Rate = rates[i]
            });
        }
        return list;
    }

    private int FindLocationIndex(LocationJson loc)
    {
        string idPrefix = loc.Id.ToString("000");
        for (int i = 0; i < LocationNames.Length; i++)
        {
            if (LocationNames[i].StartsWith(idPrefix + " - ") || LocationNames[i].StartsWith(idPrefix))
                return i;
        }
        if (loc.Index >= 0 && loc.Index < LocationNames.Length)
            return loc.Index;
        return -1;
    }

    private void ApplyLocation(LocationJson loc)
    {
        if (!loc.HasEncounters)
        {
            ClearData();
            return;
        }

        // First pass: collect every species in this location that successfully matches
        var validSpecies = new List<int>();
        CollectValidSpecies(loc.Slots.Grass, validSpecies);
        CollectValidSpecies(loc.Slots.YellowFlowers, validSpecies);
        CollectValidSpecies(loc.Slots.PurpleFlowers, validSpecies);
        CollectValidSpecies(loc.Slots.RedFlowers, validSpecies);
        CollectValidSpecies(loc.Slots.RoughTerrain, validSpecies);
        CollectValidSpecies(loc.Slots.Surfing, validSpecies);
        CollectValidSpecies(loc.Slots.RockSmash, validSpecies);
        CollectValidSpecies(loc.Slots.OldRod, validSpecies);
        CollectValidSpecies(loc.Slots.GoodRod, validSpecies);
        CollectValidSpecies(loc.Slots.SuperRod, validSpecies);
        CollectValidSpecies(loc.Slots.HordeA, validSpecies);
        CollectValidSpecies(loc.Slots.HordeB, validSpecies);
        CollectValidSpecies(loc.Slots.HordeC, validSpecies);

        // If nothing matched at all, we have no safe fallback — leave as none (caller can decide)
        // Otherwise use the collected valid ones for any unmatched slots
        ApplySlots(loc.Slots.Grass, OffGrass, validSpecies);
        ApplySlots(loc.Slots.YellowFlowers, OffYellow, validSpecies);
        ApplySlots(loc.Slots.PurpleFlowers, OffPurple, validSpecies);
        ApplySlots(loc.Slots.RedFlowers, OffRed, validSpecies);
        ApplySlots(loc.Slots.RoughTerrain, OffRT, validSpecies);
        ApplySlots(loc.Slots.Surfing, OffSurf, validSpecies);
        ApplySlots(loc.Slots.RockSmash, OffRock, validSpecies);
        ApplySlots(loc.Slots.OldRod, OffOld, validSpecies);
        ApplySlots(loc.Slots.GoodRod, OffGood, validSpecies);
        ApplySlots(loc.Slots.SuperRod, OffSuper, validSpecies);
        ApplySlots(loc.Slots.HordeA, OffHordeA, validSpecies);
        ApplySlots(loc.Slots.HordeB, OffHordeB, validSpecies);
        ApplySlots(loc.Slots.HordeC, OffHordeC, validSpecies);
    }

    private void CollectValidSpecies(List<SlotJson> slots, List<int> valid)
    {
        if (slots == null) return;
        foreach (var s in slots)
        {
            int idx = FindSpeciesIndex(s.Species);
            if (idx > 0 && !valid.Contains(idx))
                valid.Add(idx);
        }
    }

    private void ApplySlots(List<SlotJson> slots, int offset, List<int> validFallback)
    {
        if (slots == null) return;
        int maxSlots = Math.Min(slots.Count, All_Species.Length - offset);
        for (int i = 0; i < maxSlots; i++)
        {
            int idx = offset + i;
            var s = slots[i];
            int speciesIdx = FindSpeciesIndex(s.Species);

            // If unmatched, grab another valid species from this location
            if (speciesIdx <= 0 && validFallback.Count > 0)
            {
                // Simple round-robin / pick based on slot index so it's deterministic
                speciesIdx = validFallback[i % validFallback.Count];
            }

            All_Species[idx].SelectedIndex = speciesIdx;
            All_Form[idx].Value = Math.Clamp(s.Form, 0, (int)All_Form[idx].Maximum);
            All_Min[idx].Value = Math.Clamp(s.MinLevel, (int)All_Min[idx].Minimum, (int)All_Min[idx].Maximum);
            All_Max[idx].Value = Math.Clamp(s.MaxLevel, (int)All_Max[idx].Minimum, (int)All_Max[idx].Maximum);
        }
    }

    private int FindSpeciesIndex(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name == "---")
            return 0;

        // Exact
        int idx = Array.IndexOf(specieslist, name);
        if (idx >= 0) return idx;

        // Case-insensitive
        idx = Array.FindIndex(specieslist, x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase));
        if (idx >= 0) return idx;

        // Closest match by Levenshtein (only if reasonably close)
        int bestIdx = 0;
        int bestDist = int.MaxValue;
        string target = name.ToLowerInvariant();
        for (int i = 1; i < specieslist.Length; i++) // skip ---
        {
            if (string.IsNullOrEmpty(specieslist[i])) continue;
            int dist = LevenshteinDistance(target, specieslist[i].ToLowerInvariant());
            if (dist < bestDist)
            {
                bestDist = dist;
                bestIdx = i;
            }
        }

        // Accept if distance is small relative to name length (typos of 1-2 chars usually)
        int maxAccept = Math.Max(1, name.Length / 5);
        if (bestDist <= maxAccept)
            return bestIdx;

        // No good match → none
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
        public string Game { get; set; } = "XY";
        public List<LocationJson> Locations { get; set; } = [];
    }

    private class LocationJson
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool HasEncounters { get; set; }
        public SlotsJson Slots { get; set; } = new();
    }

    private class SlotsJson
    {
        public List<SlotJson> Grass { get; set; } = [];
        public List<SlotJson> YellowFlowers { get; set; } = [];
        public List<SlotJson> PurpleFlowers { get; set; } = [];
        public List<SlotJson> RedFlowers { get; set; } = [];
        public List<SlotJson> RoughTerrain { get; set; } = [];
        public List<SlotJson> Surfing { get; set; } = [];
        public List<SlotJson> RockSmash { get; set; } = [];
        public List<SlotJson> OldRod { get; set; } = [];
        public List<SlotJson> GoodRod { get; set; } = [];
        public List<SlotJson> SuperRod { get; set; } = [];
        public List<SlotJson> HordeA { get; set; } = [];
        public List<SlotJson> HordeB { get; set; } = [];
        public List<SlotJson> HordeC { get; set; } = [];
    }

    private class SlotJson
    {
        public string Species { get; set; } = "";
        public int Form { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int Rate { get; set; }
    }

    private void ModifyLevels(object sender, EventArgs e)
    {
        if (WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Modify all current Level ranges?", "Cannot undo.") != DialogResult.Yes) return;

        // Disable Interface while modifying
        Enabled = false;

        // Calculate % diff we will apply to each level
        decimal leveldiff = NUD_LevelAmp.Value;

        // Cycle through each location to modify levels
        for (int i = 0; i < CB_LocationID.Items.Count; i++) // for every location
        {
            // Load location
            CB_LocationID.SelectedIndex = i;

            // Amp Levels
            for (int l = 0; l < All_Max.Length; l++)
            {
                if (All_Min[l].Value > 1)
                    All_Min[l].Value = All_Max[l].Value = Randomizer.GetModifiedLevel((int)All_Max[l].Value, leveldiff);
            }

            // Save Changes
            B_Save_Click(sender, e);
        }
        // Enable Interface... modification complete.
        Enabled = true;
        WinFormsUtil.Alert("Modified all Level ranges according to specification!", "Press the Dump Tables button to view the new Level ranges!");
    }

    private void XYWE_FormClosing(object sender, FormClosingEventArgs e)
    {
        RandSettings.SetFormSettings(this, GB_Tweak.Controls);
    }
}