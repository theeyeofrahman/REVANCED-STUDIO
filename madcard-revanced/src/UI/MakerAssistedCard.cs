using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

public sealed class MakerAssistedCardDialog : Form
{
    sealed class GuidedField
    {
        public ModCards.PropertyControl Schema;
        public CheckBox Include;
        public Control Input;
    }

    readonly string type;
    readonly Func<string, bool> idExists;
    readonly ComboBox rootChoice = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.SuggestAppend };
    readonly ComboBox templateChoice = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
    readonly TextBox name = new TextBox { Dock = DockStyle.Fill }, id = new TextBox { Dock = DockStyle.Fill }, description = new TextBox { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical };
    readonly TextBox preview = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
    readonly Label guidance = new Label { Dock = DockStyle.Fill };
    readonly FlowLayoutPanel guidedFields = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
    readonly List<GuidedField> inputs = new List<GuidedField>();
    readonly ModCards.PropertyControl[] schema;
    bool customId, loading;
    public JObject Card { get; private set; }

    public MakerAssistedCardDialog(string cardType, string initialRoot = null, Func<string, bool> exists = null, string initialTemplate = null)
    {
        type = cardType ?? throw new ArgumentNullException(nameof(cardType)); idExists = exists; schema = MakerTools.Schema(type);
        string display = Regex.Replace(type.Replace('_', ' '), "([a-z])([A-Z])", "$1 $2");
        Text = "Assisted " + display + " creation"; Size = new Size(1120, 820); MinimumSize = new Size(900, 680); StartPosition = FormStartPosition.CenterParent;
        AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96, 96);
        var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, Padding = new Padding(22) };
        page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56)); page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44));
        page.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); page.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); page.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        guidance.Text = "Start from a matching template or inherited root. Optional settings below are guided from this card type; use Advanced for nested structures and every other field.";
        page.Controls.Add(guidance, 0, 0); page.SetColumnSpan(guidance, 2);

        var left = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new Padding(0, 0, 16, 0) };
        left.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150)); left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int row = 0; row < 5; row++) left.RowStyles.Add(new RowStyle(SizeType.Absolute, row == 4 ? 110 : 46));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        AddBasic(left, 0, "Starting point", templateChoice); AddBasic(left, 1, "Inherited root", rootChoice); AddBasic(left, 2, "Display name", name); AddBasic(left, 3, "Card ID", id); AddBasic(left, 4, "Description", description);
        left.Controls.Add(new Label { Text = "Guided settings · check a field to override the inherited value", Dock = DockStyle.Fill, ForeColor = DossierStyle.Muted, TextAlign = ContentAlignment.MiddleLeft }, 0, 5);
        left.SetColumnSpan(left.GetControlFromPosition(0, 5), 2);
        var leftStack = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        leftStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 420)); leftStack.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        leftStack.Controls.Add(left, 0, 0); leftStack.Controls.Add(guidedFields, 0, 1); page.Controls.Add(leftStack, 0, 1);

        var review = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        review.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); review.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        review.Controls.Add(new Label { Text = "Live draft · review before opening", Dock = DockStyle.Fill }, 0, 0); review.Controls.Add(preview, 0, 1); page.Controls.Add(review, 1, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
        var create = new DossierButton { Text = "Open in full editor", Width = 200, Height = 42 };
        var cancel = new DossierButton { Text = "Cancel", Width = 100, Height = 42, DialogResult = DialogResult.Cancel };
        actions.Controls.Add(create); actions.Controls.Add(cancel); page.Controls.Add(actions, 0, 2); page.SetColumnSpan(actions, 2); Controls.Add(page); CancelButton = cancel;

        PopulateRoots(initialRoot); PopulateTemplates(); BuildGuidedFields();
        name.Text = "New " + display; id.Text = MakerAssistedCharacter.SuggestId(name.Text);
        name.TextChanged += (_, __) => { if (!customId) { loading = true; id.Text = MakerAssistedCharacter.SuggestId(name.Text); loading = false; } UpdateDraft(); };
        id.TextChanged += (_, __) => { if (!loading) customId = true; UpdateDraft(); };
        description.TextChanged += (_, __) => UpdateDraft(); rootChoice.SelectedIndexChanged += (_, __) => UpdateDraft(); rootChoice.TextUpdate += (_, __) => UpdateDraft();
        templateChoice.SelectedIndexChanged += (_, __) => ApplyTemplate();
        create.Click += (_, __) => { try { Card = Build(); if (idExists != null && idExists((string)Card["Filename"])) throw new FormatException("That ID is already used in this deck. Choose another ID."); DialogResult = DialogResult.OK; Close(); } catch (Exception ex) { guidance.Text = ex.Message; } };
        DossierStyle.Apply(this);
        if (!string.IsNullOrWhiteSpace(initialTemplate))
            templateChoice.SelectedItem = MakerPreset.All.FirstOrDefault(p => p.Type == type && string.Equals(p.Name, initialTemplate, StringComparison.OrdinalIgnoreCase));
        if (templateChoice.SelectedIndex < 0 && templateChoice.Items.Count > 0) templateChoice.SelectedIndex = 0;
        UpdateDraft();
    }

    static void AddBasic(TableLayoutPanel table, int row, string label, Control input)
    {
        table.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
        input.Margin = new Padding(3, 5, 3, 5); table.Controls.Add(input, 1, row);
    }

    void PopulateRoots(string initialRoot)
    {
        if (ModEnums.SC_DIR.TryGetValue(type, out var roots)) rootChoice.Items.AddRange(roots.Keys.Cast<object>().ToArray());
        if (!string.IsNullOrWhiteSpace(initialRoot) && rootChoice.Items.Contains(initialRoot)) rootChoice.SelectedItem = initialRoot;
        else if (rootChoice.Items.Count > 0) rootChoice.SelectedIndex = 0;
    }

    void PopulateTemplates()
    {
        templateChoice.Items.Add("Blank card · inherit from root");
        templateChoice.Items.AddRange(MakerPreset.All.Where(p => p.Type == type).Cast<object>().ToArray());
    }

    void BuildGuidedFields()
    {
        var eligible = schema.Where(IsGuided).OrderBy(p => Rank(p.PropName)).ThenBy(p => Array.IndexOf(schema, p)).Take(type == "Chatter" ? 6 : 8).ToArray();
        if (eligible.Length == 0)
        {
            guidedFields.Controls.Add(new Label { Text = "This card type has no simple guided settings. Its name and root are ready; use the full editor for its structured fields.", Width = 420, Height = 64 }); return;
        }
        foreach (var property in eligible)
        {
            var row = new TableLayoutPanel { ColumnCount = 3, RowCount = 1, Width = 470, Height = property.IsList ? 100 : 44, Margin = new Padding(2, 2, 8, 4) };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82)); row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135)); row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var include = new CheckBox { Text = "Set", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            var label = new Label { Text = Humanize(property.PropName), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            var input = CreateInput(property); input.Dock = DockStyle.Fill; input.Enabled = false;
            include.CheckedChanged += (_, __) => { input.Enabled = include.Checked; UpdateDraft(); };
            if (input is ComboBox combo) combo.SelectedIndexChanged += (_, __) => { if (combo.SelectedIndex > 0) include.Checked = true; UpdateDraft(); };
            if (input is TextBox text) text.TextChanged += (_, __) => UpdateDraft();
            if (input is NumericUpDown number) number.ValueChanged += (_, __) => UpdateDraft();
            if (input is CheckBox check) check.CheckedChanged += (_, __) => UpdateDraft();
            row.Controls.Add(include, 0, 0); row.Controls.Add(label, 1, 0); row.Controls.Add(input, 2, 0); guidedFields.Controls.Add(row);
            inputs.Add(new GuidedField { Schema = property, Include = include, Input = input });
        }
    }

    bool IsGuided(ModCards.PropertyControl p)
    {
        if (new[] { "Filename", "Root", "Name", "NameShort", "Description", "Priority" }.Contains(p.PropName)) return false;
        if (p.PropType == ModCards.PropType.Color || p.PropType == ModCards.PropType.Obj || p.Fixed) return false;
        if (p.IsList) return p.PropType == ModCards.PropType.Bool || p.PropType == ModCards.PropType.Int || p.PropType == ModCards.PropType.Float || p.PropType == ModCards.PropType.Str || p.PropType == ModCards.PropType.Ref;
        return true;
    }

    int Rank(string field)
    {
        string[] preferred = type == "Chatter" ? new[] { "Spawned", "Attacking", "Wounded", "Taunt" } :
            type == "Ranged" ? new[] { "Damage", "ROF", "Accuracy", "Ammo", "Stability", "ReloadTime" } :
            type == "Melee" ? new[] { "Damage", "MeleeAmmo", "MeleeDefense", "Range", "Speed" } :
            type.StartsWith("Stage_", StringComparison.Ordinal) ? new[] { "Description", "Enabled", "Music", "Background", "Faction", "Difficulty" } :
            new[] { "Description", "Enabled", "Level", "Damage", "Health", "Count", "Weight", "Cost", "Faction", "WeaponPref" };
        int rank = Array.FindIndex(preferred, x => string.Equals(x, field, StringComparison.OrdinalIgnoreCase));
        return rank < 0 ? preferred.Length + Math.Max(0, Array.IndexOf(schema, schema.FirstOrDefault(p => p.PropName == field))) : rank;
    }

    Control CreateInput(ModCards.PropertyControl property)
    {
        if (property.IsList)
            return new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical };
        switch (property.PropType)
        {
            case ModCards.PropType.Bool: return new CheckBox { Text = "On" };
            case ModCards.PropType.Int:
            case ModCards.PropType.Float: return new NumericUpDown { Minimum = -1000000, Maximum = 1000000, DecimalPlaces = property.PropType == ModCards.PropType.Int ? 0 : 2, Increment = property.PropType == ModCards.PropType.Int ? 1 : .1m };
            case ModCards.PropType.Ref:
                if (property.WriteNewValues || property.ObjectName == "Object") return new TextBox();
                var choice = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.SuggestAppend };
                choice.Items.Add("Choose a value"); choice.Items.AddRange(ReferenceOptions(property).Cast<object>().ToArray());
                choice.SelectedIndex = 0; return choice;
            default: return new TextBox();
        }
    }

    void ApplyTemplate()
    {
        if (!(templateChoice.SelectedItem is MakerPreset preset)) { UpdateDraft(); return; }
        loading = true; if (rootChoice.Items.Contains(preset.Root)) rootChoice.SelectedItem = preset.Root;
        name.Text = preset.Name; if (!customId) id.Text = MakerAssistedCharacter.SuggestId(name.Text); description.Text = preset.Summary;
        foreach (var field in inputs)
        {
            var value = preset.Values[field.Schema.PropName]; if (value == null) continue;
            field.Include.Checked = true;
            if (field.Input is NumericUpDown n && decimal.TryParse(value.ToString(), out var numeric)) n.Value = Math.Max(n.Minimum, Math.Min(n.Maximum, numeric));
            else if (field.Input is CheckBox c) c.Checked = value.Value<bool>();
            else if (field.Input is TextBox text) text.Text = value.Type == JTokenType.Array ? string.Join(Environment.NewLine, ((JArray)value).Select(x => x.ToString())) : value.ToString();
            else if (field.Input is ComboBox combo) combo.Text = value.ToString();
        }
        loading = false; UpdateDraft();
    }

    JObject Build()
    {
        if (string.IsNullOrWhiteSpace(name.Text)) throw new FormatException("Add a display name first.");
        string root = rootChoice.Text;
        if (rootChoice.Items.Count > 0 && (!rootChoice.Items.Cast<object>().Any(x => string.Equals(rootChoice.GetItemText(x), root, StringComparison.OrdinalIgnoreCase)) || !MakerStudio.Exists(type, root)))
            throw new FormatException("Choose a root from the suggestions.");
        var preset = templateChoice.SelectedItem as MakerPreset;
        JObject card = preset == null ? new JObject() : preset.Create(id.Text, name.Text, root);
        card["Filename"] = id.Text; if (!string.IsNullOrWhiteSpace(root)) card["Root"] = root;
        if (schema.Any(p => p.PropName == "Name")) card["Name"] = name.Text.Trim();
        if (schema.Any(p => p.PropName == "NameShort")) card["NameShort"] = name.Text.Trim();
        if (schema.Any(p => p.PropName == "Description")) card["Description"] = description.Text;
        foreach (var field in inputs.Where(x => x.Include.Checked))
        {
            if (field.Schema.IsList)
            {
                var lines = ((TextBox)field.Input).Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => x.Length > 0);
                var values = new List<JToken>();
                foreach (string line in lines)
                {
                    switch (field.Schema.PropType)
                    {
                        case ModCards.PropType.Str: values.Add(new JValue(line)); break;
                        case ModCards.PropType.Bool:
                            if (!bool.TryParse(line, out var boolean)) throw new FormatException(Humanize(field.Schema.PropName) + " needs true or false, one per line.");
                            values.Add(new JValue(boolean)); break;
                        case ModCards.PropType.Int:
                            if (!int.TryParse(line, NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer)) throw new FormatException(Humanize(field.Schema.PropName) + " needs whole numbers, one per line.");
                            values.Add(new JValue(integer)); break;
                        case ModCards.PropType.Float:
                            if (!double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedFloat) || double.IsNaN(parsedFloat) || double.IsInfinity(parsedFloat)) throw new FormatException(Humanize(field.Schema.PropName) + " needs finite numbers, one per line.");
                            values.Add(new JValue(parsedFloat)); break;
                        case ModCards.PropType.Ref:
                            var choices = ReferenceOptions(field.Schema).ToArray();
                            if (!field.Schema.WriteNewValues && choices.Length > 0 && !choices.Contains(line, StringComparer.OrdinalIgnoreCase)) throw new FormatException("Choose valid " + Humanize(field.Schema.ObjectName) + " entries, one per line.");
                            values.Add(new JValue(line)); break;
                    }
                }
                card[field.Schema.PropName] = new JArray(values); continue;
            }
            JToken value;
            if (field.Input is NumericUpDown number) value = field.Schema.PropType == ModCards.PropType.Int ? new JValue(decimal.ToInt32(number.Value)) : new JValue((double)number.Value);
            else if (field.Input is CheckBox check) value = new JValue(check.Checked);
            else if (field.Input is ComboBox combo)
            {
                if (combo.SelectedIndex == 0 || string.IsNullOrWhiteSpace(combo.Text)) continue;
                string exact = combo.Items.Cast<object>().Select(combo.GetItemText).FirstOrDefault(x => string.Equals(x, combo.Text, StringComparison.OrdinalIgnoreCase));
                if (exact == null) throw new FormatException("Choose a listed value for " + Humanize(field.Schema.PropName) + ".");
                value = new JValue(exact);
            }
            else value = new JValue(((TextBox)field.Input).Text);
            card = MakerTools.SetScalar(type, card, field.Schema.PropName, value);
        }
        if (!MakerStudio.ValidId(id.Text)) throw new FormatException("Use letters, numbers and underscores for the card ID.");
        return card;
    }

    void UpdateDraft()
    {
        if (loading) return;
        try { preview.Text = Build().ToString(); }
        catch (Exception ex) { preview.Text = ex.Message; }
    }

    static string Humanize(string value) => Regex.Replace(Regex.Replace(value ?? "", "([a-z])([A-Z])", "$1 $2"), "([A-Z])([A-Z][a-z])", "$1 $2");

    static IEnumerable<string> ReferenceOptions(ModCards.PropertyControl property)
    {
        if (property.Selection is string[] selected && selected.Length > 0) return selected;
        if (!string.IsNullOrEmpty(property.ObjectName) && ModEnums.SC_DIR.TryGetValue(property.ObjectName, out var cards)) return cards.Keys;
        if (!string.IsNullOrEmpty(property.ObjectName) && ModEnums.ENUMS.TryGetValue(property.ObjectName, out var enums)) return enums;
        if (!string.IsNullOrEmpty(property.ObjectName) && ModEnums.ASSETS.TryGetValue(property.ObjectName, out var assets)) return assets;
        return Enumerable.Empty<string>();
    }
}
