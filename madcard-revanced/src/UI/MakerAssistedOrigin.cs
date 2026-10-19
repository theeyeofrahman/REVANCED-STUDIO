using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

public sealed class MakerOriginListPicker : UserControl
{
    readonly ComboBox search = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.SuggestAppend };
    readonly ListBox selected = new ListBox { Dock = DockStyle.Fill, IntegralHeight = false };
    public event EventHandler ValuesChanged;
    public string[] Values => selected.Items.Cast<string>().ToArray();

    public MakerOriginListPicker(IEnumerable<string> options)
    {
        Height = 140; MinimumSize = new Size(200, 110);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 2, Padding = Padding.Empty };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        search.Items.AddRange(options.Distinct(StringComparer.OrdinalIgnoreCase).Cast<object>().ToArray());
        var add = new DossierButton { Text = "Add", Dock = DockStyle.Fill, Height = 32 };
        var remove = new DossierButton { Text = "Remove", Dock = DockStyle.Fill, Height = 32 };
        layout.Controls.Add(search, 0, 0); layout.Controls.Add(add, 1, 0); layout.Controls.Add(remove, 2, 0);
        layout.Controls.Add(selected, 0, 1); layout.SetColumnSpan(selected, 3); Controls.Add(layout);
        add.Click += (_, __) => AddValue(); remove.Click += (_, __) => { if (selected.SelectedIndex >= 0) { selected.Items.RemoveAt(selected.SelectedIndex); ValuesChanged?.Invoke(this, EventArgs.Empty); } };
        search.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { AddValue(); e.SuppressKeyPress = true; } };
    }

    void AddValue()
    {
        string value = search.Text.Trim(); if (value.Length == 0) return;
        int index = search.FindStringExact(value);
        if (index < 0) { search.DroppedDown = true; return; }
        string canonical = search.GetItemText(search.Items[index]);
        if (!selected.Items.Cast<string>().Contains(canonical, StringComparer.OrdinalIgnoreCase)) selected.Items.Add(canonical);
        search.SelectedIndex = index; ValuesChanged?.Invoke(this, EventArgs.Empty);
    }
}

public sealed class MakerAssistedOriginDialog : Form
{
    readonly Func<string, bool> idExists;
    readonly TextBox name = new TextBox { Dock = DockStyle.Fill, Text = "New origin" }, shortName = new TextBox { Dock = DockStyle.Fill }, id = new TextBox { Dock = DockStyle.Fill }, description = new TextBox { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical };
    readonly ComboBox root = Choice(), linked = Choice(), icon = Choice(), swatch = Choice(), focus = Choice(), weakness = Choice(), bonusType = Choice();
    readonly NumericUpDown imprints = Number(0, 999, 0), amount = Number(-100, 100, 2);
    readonly CheckBox setImprints = new CheckBox { Text = "Set unlock imprints" }, expandNowhere = new CheckBox { Text = "Expand into Nowhere" }, percent = new CheckBox { Text = "Percent" }, good = new CheckBox { Text = "Show as good" }, replaceSkills = ReplaceOption("Replace inherited skills"), replaceArmor = ReplaceOption("Replace inherited armor"), replaceWeapons = ReplaceOption("Replace inherited warchest weapons"), replaceGear = ReplaceOption("Replace inherited warchest gear"), replaceBuffs = ReplaceOption("Replace inherited bonuses"), setAttributes = ReplaceOption("Set attribute bonuses");
    readonly NumericUpDown[] attributes = { Number(-100, 100, 0), Number(-100, 100, 0), Number(-100, 100, 0), Number(-100, 100, 0) };
    readonly MakerOriginListPicker skills, armor, weapons, gear;
    readonly TextBox buffName = new TextBox { Dock = DockStyle.Fill }, buffDescription = new TextBox { Dock = DockStyle.Fill }, preview = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Multiline = true, ScrollBars = ScrollBars.Vertical };
    readonly ListBox buffs = new ListBox { Dock = DockStyle.Fill, IntegralHeight = false };
    readonly List<JObject> bonusValues = new List<JObject>();
    readonly Label guidance = new Label { Dock = DockStyle.Fill, Text = "Origins shape a character's starting identity, skills, attributes, bonuses, and equipment. Leave optional groups unchecked to inherit them from the selected origin." };
    bool loading, customId;
    public JObject Card { get; private set; }

    public MakerAssistedOriginDialog(string initialRoot = null, Func<string, bool> exists = null)
    {
        idExists = exists;
        skills = new MakerOriginListPicker(Options("Skill")); armor = new MakerOriginListPicker(Options("Armor")); weapons = new MakerOriginListPicker(Options("Weapon")); gear = new MakerOriginListPicker(Options("Armor"));
        Text = "Assisted Origin creator"; Size = new Size(1220, 900); MinimumSize = new Size(980, 760); StartPosition = FormStartPosition.CenterParent; AutoScaleMode = AutoScaleMode.Dpi;
        var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, Padding = new Padding(22) };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61)); page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39));
        page.RowStyles.Add(new RowStyle(SizeType.Absolute, 64)); page.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); page.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        page.Controls.Add(guidance, 0, 0); page.SetColumnSpan(guidance, 2);
        var tabs = new TabControl { Dock = DockStyle.Fill };
        var profile = new TabPage("Identity"); var progression = new TabPage("Skills & bonuses"); var loadout = new TabPage("Starting gear");
        tabs.TabPages.AddRange(new[] { profile, progression, loadout });
        BuildProfile(profile, initialRoot); BuildProgression(progression); BuildLoadout(loadout); page.Controls.Add(tabs, 0, 1);
        var review = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        review.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); review.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        review.Controls.Add(new Label { Text = "Live Origin draft", Dock = DockStyle.Fill }, 0, 0); review.Controls.Add(preview, 0, 1); page.Controls.Add(review, 1, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
        var create = new DossierButton { Text = "Open in full editor", Width = 200, Height = 42 }; var cancel = new DossierButton { Text = "Cancel", Width = 100, Height = 42, DialogResult = DialogResult.Cancel };
        actions.Controls.Add(create); actions.Controls.Add(cancel); page.Controls.Add(actions, 0, 2); page.SetColumnSpan(actions, 2); Controls.Add(page); CancelButton = cancel;
        WireUpdates(this); name.TextChanged += (_, __) => { if (!customId) { loading = true; id.Text = MakerAssistedCharacter.SuggestId(name.Text); shortName.Text = Shorten(name.Text); loading = false; } };
        id.TextChanged += (_, __) => { if (!loading) customId = true; }; skills.ValuesChanged += (_, __) => { replaceSkills.Checked = true; UpdatePreview(); }; armor.ValuesChanged += (_, __) => { replaceArmor.Checked = true; UpdatePreview(); }; weapons.ValuesChanged += (_, __) => { replaceWeapons.Checked = true; UpdatePreview(); }; gear.ValuesChanged += (_, __) => { replaceGear.Checked = true; UpdatePreview(); };
        loading = true; id.Text = MakerAssistedCharacter.SuggestId(name.Text); shortName.Text = Shorten(name.Text); loading = false;
        setImprints.CheckedChanged += (_, __) => { imprints.Enabled = setImprints.Checked; UpdatePreview(); };
        setAttributes.CheckedChanged += (_, __) => { foreach (var value in attributes) value.Enabled = setAttributes.Checked; UpdatePreview(); };
        imprints.Enabled = setImprints.Checked; foreach (var value in attributes) value.Enabled = setAttributes.Checked;
        create.Click += (_, __) => { try { Card = Build(); if (idExists != null && idExists((string)Card["Filename"])) throw new FormatException("That ID is already used in this deck. Choose another ID."); DialogResult = DialogResult.OK; Close(); } catch (Exception ex) { guidance.Text = ex.Message; } };
        DossierStyle.Apply(this); UpdatePreview();
    }

    static ComboBox Choice() => new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.SuggestAppend };
    static NumericUpDown Number(decimal min, decimal max, int decimals) => new NumericUpDown { Dock = DockStyle.Fill, Minimum = min, Maximum = max, DecimalPlaces = decimals, Increment = decimals == 0 ? 1 : .05m };
    static CheckBox ReplaceOption(string label) => new CheckBox { Text = label, Dock = DockStyle.Fill };
    static IEnumerable<string> Options(string type)
    {
        if (ModEnums.SC_DIR.TryGetValue(type, out var cards)) return cards.Keys;
        if (ModEnums.ENUMS.TryGetValue(type, out var enums)) return enums;
        if (ModEnums.ASSETS.TryGetValue(type, out var assets)) return assets;
        return Enumerable.Empty<string>();
    }
    static void Fill(ComboBox combo, string type, bool inherit = true)
    {
        if (inherit) combo.Items.Add("Inherit from selected origin");
        combo.Items.AddRange(Options(type).Cast<object>().ToArray()); if (combo.Items.Count > 0) combo.SelectedIndex = 0;
    }
    void BuildProfile(TabPage tab, string initialRoot)
    {
        var grid = Grid(2, 9); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165)); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Add(grid, 0, "Starting origin", root); Fill(root, "Origin", false); if (!string.IsNullOrWhiteSpace(initialRoot) && root.Items.Contains(initialRoot)) root.SelectedItem = initialRoot;
        Add(grid, 1, "Display name", name); Add(grid, 2, "Short name", shortName); Add(grid, 3, "Card ID", id);
        Add(grid, 4, "Linked character", linked); Fill(linked, "Character");
        var imprintPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        setImprints.AutoSize = true; imprints.Width = 110; imprintPanel.Controls.Add(setImprints); imprintPanel.Controls.Add(imprints); Add(grid, 5, "Unlock cost", imprintPanel);
        Add(grid, 6, "Origin icon", icon); Fill(icon, "Sprite"); Add(grid, 7, "Color swatch", swatch); Fill(swatch, "Swatch");
        grid.Controls.Add(expandNowhere, 1, 8);
        var descriptionLabel = new Label { Text = "Description", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        var container = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 1, ColumnCount = 1 }; container.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        container.Controls.Add(description, 0, 0);
        var outer = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 2 }; outer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165)); outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); outer.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); outer.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        outer.Controls.Add(grid, 0, 0); outer.SetColumnSpan(grid, 2); outer.Controls.Add(descriptionLabel, 0, 1); outer.Controls.Add(container, 1, 1); tab.Controls.Add(outer);
    }
    void BuildProgression(TabPage tab)
    {
        var grid = Grid(2, 8); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175)); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Add(grid, 0, "Skill focus", focus); Fill(focus, "SkillTrees"); Add(grid, 1, "Skill weakness", weakness); Fill(weakness, "SkillTrees");
        grid.Controls.Add(new Label { Text = "Attribute bonuses (Strength, Dexterity, Endurance, Tactics)", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 2); grid.SetColumnSpan(grid.GetControlFromPosition(0, 2), 2); grid.Controls.Add(setAttributes, 0, 3); grid.SetColumnSpan(setAttributes, 2);
        var attributeGrid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
        string[] names = { "Strength", "Dexterity", "Endurance", "Tactics" }; for (int i = 0; i < names.Length; i++) { attributeGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); attributeGrid.Controls.Add(new Label { Text = names[i], Dock = DockStyle.Fill }, i, 0); attributeGrid.Controls.Add(attributes[i], i, 1); }
        grid.Controls.Add(attributeGrid, 0, 4); grid.SetColumnSpan(attributeGrid, 2);
        grid.Controls.Add(new Label { Text = "Special starting skills · select entries, then add them", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 5); grid.SetColumnSpan(grid.GetControlFromPosition(0, 5), 2);
        grid.Controls.Add(replaceSkills, 0, 6); grid.SetColumnSpan(replaceSkills, 2); grid.Controls.Add(skills, 0, 7); grid.SetColumnSpan(skills, 2);
        var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 }; page.RowStyles.Add(new RowStyle(SizeType.Percent, 55)); page.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
        page.Controls.Add(grid, 0, 0); page.Controls.Add(BuildBuffs(), 0, 1); tab.Controls.Add(page);
    }
    Control BuildBuffs()
    {
        var box = new GroupBox { Text = "Origin bonuses", Dock = DockStyle.Fill, Padding = new Padding(10) };
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4 };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90)); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95)); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        grid.Controls.Add(new Label { Text = "Bonus name", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0); grid.Controls.Add(buffName, 1, 0);
        grid.Controls.Add(new Label { Text = "Bonus type", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 2, 0); grid.Controls.Add(bonusType, 3, 0); Fill(bonusType, "BonusTypes", false);
        grid.Controls.Add(new Label { Text = "Amount", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 1); grid.Controls.Add(amount, 1, 1); grid.Controls.Add(percent, 2, 1); grid.Controls.Add(good, 3, 1);
        grid.Controls.Add(new Label { Text = "Description", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 2); grid.Controls.Add(buffDescription, 1, 2); grid.SetColumnSpan(buffDescription, 3);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill }; var add = new DossierButton { Text = "Add bonus", Width = 100, Height = 32 }; var remove = new DossierButton { Text = "Remove selected", Width = 140, Height = 32 };
        actions.Controls.Add(replaceBuffs); actions.Controls.Add(add); actions.Controls.Add(remove); grid.Controls.Add(actions, 0, 3); grid.SetColumnSpan(actions, 2); grid.Controls.Add(buffs, 2, 3); grid.SetColumnSpan(buffs, 2);
        add.Click += (_, __) => AddBonus(); remove.Click += (_, __) => { if (buffs.SelectedIndex >= 0) { bonusValues.RemoveAt(buffs.SelectedIndex); buffs.Items.RemoveAt(buffs.SelectedIndex); replaceBuffs.Checked = true; UpdatePreview(); } };
        box.Controls.Add(grid); return box;
    }
    void BuildLoadout(TabPage tab)
    {
        var grid = Grid(1, 8); grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        AddList(grid, 0, "Default starting armor", armor, replaceArmor);
        AddList(grid, 1, "Warchest weapons", weapons, replaceWeapons);
        AddList(grid, 2, "Warchest gear", gear, replaceGear);
        grid.Controls.Add(new Label { Text = "Uniform layouts, color pairs, and custom swatch colors are available in Advanced mode.", Dock = DockStyle.Fill, ForeColor = DossierStyle.Muted }, 0, 6);
        tab.Controls.Add(grid);
    }
    void AddList(TableLayoutPanel grid, int row, string title, MakerOriginListPicker picker, CheckBox replace)
    {
        var group = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(8) };
        group.RowStyles.Add(new RowStyle(SizeType.Absolute, 26)); group.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); group.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        group.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill }, 0, 0); group.Controls.Add(replace, 0, 1); group.Controls.Add(picker, 0, 2); grid.Controls.Add(group, 0, row);
    }
    static TableLayoutPanel Grid(int columns, int rows)
    {
        var grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = columns, RowCount = rows, Padding = new Padding(12), AutoScroll = true };
        for (int i = 0; i < rows; i++) grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows)); return grid;
    }
    static void Add(TableLayoutPanel grid, int row, string label, Control input)
    {
        grid.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, row); input.Margin = new Padding(3, 5, 3, 5); grid.Controls.Add(input, 1, row);
    }
    void AddBonus()
    {
        if (string.IsNullOrWhiteSpace(buffName.Text) || bonusType.SelectedIndex < 0) { guidance.Text = "Give each bonus a name and choose a bonus type."; return; }
        var value = new JObject { ["Name"] = buffName.Text.Trim(), ["Description"] = buffDescription.Text, ["Type"] = bonusType.Text, ["Amount"] = (double)amount.Value, ["AmPercent"] = percent.Checked, ["ShowAsGood"] = good.Checked };
        bonusValues.Add(value); buffs.Items.Add(value["Name"] + " · " + value["Type"] + " · " + amount.Value + (percent.Checked ? "%" : "")); replaceBuffs.Checked = true; buffName.Clear(); buffDescription.Clear(); UpdatePreview();
    }
    JObject Build()
    {
        if (string.IsNullOrWhiteSpace(name.Text)) throw new FormatException("Add an Origin display name.");
        if (string.IsNullOrWhiteSpace(root.Text) || !root.Items.Cast<object>().Select(root.GetItemText).Any(x => string.Equals(x, root.Text, StringComparison.OrdinalIgnoreCase)) || !MakerStudio.Exists("Origin", root.Text)) throw new FormatException("Choose an Origin root from the suggestions.");
        if (!MakerStudio.ValidId(id.Text)) throw new FormatException("Use letters, numbers and underscores for the card ID.");
        var card = new JObject { ["Filename"] = id.Text, ["Root"] = root.Text, ["Name"] = name.Text.Trim(), ["NameShort"] = string.IsNullOrWhiteSpace(shortName.Text) ? Shorten(name.Text) : shortName.Text.Trim(), ["Description"] = description.Text };
        if (setImprints.Checked) card["Imprints"] = (int)imprints.Value;
        AddReference(card, "LinkedCharacter", linked); AddReference(card, "Icon", icon); AddReference(card, "Swatch", swatch); AddReference(card, "SkillFocus", focus); AddReference(card, "SkillWeakness", weakness);
        if (setAttributes.Checked) card["AttributeBonuses"] = new JArray(attributes.Select(x => (JToken)new JValue((int)x.Value)));
        if (replaceSkills.Checked) card["SpecialSkills"] = new JArray(skills.Values);
        if (replaceArmor.Checked) card["DefaultArmor"] = new JArray(armor.Values);
        if (replaceWeapons.Checked) card["WarchestWeapons"] = new JArray(weapons.Values);
        if (replaceGear.Checked) card["WarchestGear"] = new JArray(gear.Values);
        if (replaceBuffs.Checked) card["Buffs"] = new JArray(bonusValues.Select(x => (JToken)x.DeepClone()));
        if (expandNowhere.Checked) card["ExpandNowhere"] = true;
        return card;
    }
    static void AddReference(JObject card, string property, ComboBox combo)
    {
        if (combo.SelectedIndex < 0) return;
        if (combo.SelectedIndex == 0 && string.Equals(combo.GetItemText(combo.Items[0]), "Inherit from selected origin", StringComparison.Ordinal)) return;
        string exact = combo.Items.Cast<object>().Select(combo.GetItemText).FirstOrDefault(x => string.Equals(x, combo.Text, StringComparison.OrdinalIgnoreCase));
        if (exact == null) throw new FormatException("Choose a listed value for " + property + "."); card[property] = exact;
    }
    static string Shorten(string value)
    {
        var words = Regex.Split((value ?? "").Trim(), "\\s+").Where(x => x.Length > 0).ToArray();
        if (words.Length == 0) return "Origin"; return words.Length == 1 ? words[0] : string.Join(" ", words.Take(2));
    }
    void UpdatePreview() { if (loading) return; try { preview.Text = Build().ToString(); } catch (Exception ex) { preview.Text = ex.Message; } }
    void WireUpdates(Control control)
    {
        if (control is TextBox text) text.TextChanged += (_, __) => UpdatePreview();
        else if (control is ComboBox combo) { combo.SelectedIndexChanged += (_, __) => UpdatePreview(); combo.TextUpdate += (_, __) => UpdatePreview(); }
        else if (control is NumericUpDown number) number.ValueChanged += (_, __) => UpdatePreview();
        else if (control is CheckBox check) check.CheckedChanged += (_, __) => UpdatePreview();
        foreach (Control child in control.Controls) WireUpdates(child);
    }
}
