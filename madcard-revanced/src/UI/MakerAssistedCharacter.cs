using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MadCardMaker;

public static class MakerAssistedCharacter
{
    public static string SuggestId(string name)
    {
        string id = Regex.Replace((name ?? "").Trim(), "[^A-Za-z0-9_]+", "_").Trim('_');
        return "My_" + (id.Length == 0 ? "Character" : id);
    }
    public static JObject Create(MakerPreset role, string root, string id, string name, string description, decimal health, int level, decimal scale, decimal tacBar, string chatter, string faction = null, string voiceSet = null, string weapon = null, string offHand = null, string animationSet = null, bool enabled = true, string weaponStyle = null, string sidearm = null)
    {
        if (role == null || role.Type != "Character") throw new FormatException("Choose a character role.");
        if (string.IsNullOrWhiteSpace(name)) throw new FormatException("Give your character a display name.");
        if (health <= 0 || health > 10000 || level < 0 || level > 1000 || scale < .25m || scale > 3 || tacBar < 0 || tacBar > 10) throw new FormatException("One of the character stats is outside the supported range.");
        var card = role.Create(id, name.Trim(), root);
        card["Description"] = description ?? ""; card["Health"] = health; card["Level"] = level; card["Scale"] = scale; card["TacBar"] = tacBar; card["Enabled"] = enabled;
        if (!string.IsNullOrWhiteSpace(faction)) card["Faction"] = faction;
        if (!string.IsNullOrWhiteSpace(voiceSet)) card["VoiceSet"] = voiceSet;
        if (!string.IsNullOrWhiteSpace(weapon)) card["DefaultWeapon"] = weapon;
        if (!string.IsNullOrWhiteSpace(offHand)) card["DefaultOffHand"] = offHand;
        if (!string.IsNullOrWhiteSpace(animationSet)) card["AnimationSet"] = animationSet;
        if (!string.IsNullOrWhiteSpace(weaponStyle)) card["WeaponPref"] = weaponStyle;
        if (!string.IsNullOrWhiteSpace(sidearm)) card["DefaultSidearm"] = sidearm;
        if (!string.IsNullOrEmpty(chatter))
        {
            if (!MakerStudio.Exists("Chatter", chatter)) throw new FormatException("Choose chatter from the loaded database.");
            card["TextChatter"] = chatter;
        }
        return card;
    }
}

public sealed class MakerAssistedCharacterDialog : Form
{
    public JObject Card { get; private set; }
    readonly ComboBox role = Choice(), appearance = Choice(), chatter = Choice();
    readonly ComboBox faction = Choice(), voiceSet = Choice(), weapon = Choice(), offHand = Choice(), sidearm = Choice(), weaponStyle = Choice(), animationSet = Choice();
    readonly CheckBox enabled = new CheckBox { Text = "Enable this character", Checked = true, Dock = DockStyle.Fill };
    readonly CheckBox showMore = new CheckBox { Text = "Show more character options", Dock = DockStyle.Fill };
    readonly TextBox name = new TextBox { Dock = DockStyle.Fill, Text = "New character" }, id = new TextBox { Dock = DockStyle.Fill }, description = new TextBox { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical };
    readonly NumericUpDown health = Number(1, 10000, 1), level = Number(0, 1000, 0), scale = Number(.25m, 3, 2), tacBar = Number(0, 10, 2);
    readonly TextBox preview = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Multiline = true, ScrollBars = ScrollBars.Vertical };
    readonly Label guidance = new Label { Dock = DockStyle.Fill, Text = "Choose a role, then customize the basics. Appearance inherits the selected character's model, clothes and equipment. You can edit every field in the full editor next." };
    bool loading, customId;
    static ComboBox Choice() => new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteSource = AutoCompleteSource.ListItems, AutoCompleteMode = AutoCompleteMode.SuggestAppend };
    static NumericUpDown Number(decimal min, decimal max, int decimals) => new NumericUpDown { Dock = DockStyle.Fill, Minimum = min, Maximum = max, DecimalPlaces = decimals, Increment = decimals == 0 ? 1 : .05m };

    public MakerAssistedCharacterDialog(Func<string,bool> idExists = null)
    {
        Text = "Assisted character creation"; Size = new Size(1120, 860); MinimumSize = new Size(900, 720); StartPosition = FormStartPosition.CenterParent;
        AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96, 96);
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, Padding = new Padding(24) };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57)); root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); root.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        root.Controls.Add(guidance, 0, 0); root.SetColumnSpan(guidance, 2);
        var fields = new TableLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, ColumnCount = 2, RowCount = 12, Padding = new Padding(0, 0, 18, 0) };
        fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155)); fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        string[] labels = { "Starting role", "Display name", "Card ID", "Appearance / root", "Health", "Level", "Body scale", "TAC bar", "Speech bubbles", "Description" };
        Control[] controls = { role, name, id, appearance, health, level, scale, tacBar, chatter, description };
        for (int i = 0; i < controls.Length; i++)
        {
            fields.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 9 ? 112 : 44));
            fields.Controls.Add(new Label { Text = labels[i], Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, i);
            controls[i].AccessibleName = labels[i]; controls[i].Margin = new Padding(3, 5, 3, 5); fields.Controls.Add(controls[i], 1, i);
        }
        fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); fields.Controls.Add(showMore, 1, 10);
        var advanced = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, RowCount = 9, Visible = false, Padding = new Padding(8) };
        advanced.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145)); advanced.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        string[] extraLabels = { "Faction", "Voice set", "Weapon style", "Starting weapon", "Off-hand weapon", "Sidearm", "Animation set" };
        ComboBox[] extraChoices = { faction, voiceSet, weaponStyle, weapon, offHand, sidearm, animationSet };
        for (int i = 0; i < extraChoices.Length; i++)
        {
            advanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            advanced.Controls.Add(new Label { Text = extraLabels[i], Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, i);
            extraChoices[i].Margin = new Padding(3, 5, 3, 5); advanced.Controls.Add(extraChoices[i], 1, i);
        }
        advanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); advanced.Controls.Add(enabled, 1, 7);
        var suggestions = new DossierButton { Text = "Use suggestions from appearance", Dock = DockStyle.Fill, Height = 40 };
        advanced.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); advanced.Controls.Add(suggestions, 0, 8); advanced.SetColumnSpan(suggestions, 2);
        suggestions.Click += (_, __) => { ApplySuggestions(); UpdateDraft(); };
        fields.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); fields.Controls.Add(advanced, 0, 11); fields.SetColumnSpan(advanced, 2);
        showMore.CheckedChanged += (_, __) => { advanced.Visible = showMore.Checked; UpdateDraft(); };
        root.Controls.Add(fields, 0, 1);
        var review = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        review.RowStyles.Add(new RowStyle(SizeType.Absolute, 42)); review.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        review.Controls.Add(new Label { Text = "Live draft · review before opening", Dock = DockStyle.Fill }, 0, 0); review.Controls.Add(preview, 0, 1); root.Controls.Add(review, 1, 1);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
        var create = new DossierButton { Text = "Open in full editor", Width = 200, Height = 44 };
        var cancel = new DossierButton { Text = "Cancel", Width = 100, Height = 44, DialogResult = DialogResult.Cancel };
        actions.Controls.Add(create); actions.Controls.Add(cancel); root.Controls.Add(actions, 0, 2); root.SetColumnSpan(actions, 2); Controls.Add(root); CancelButton = cancel;
        role.Items.AddRange(MakerPreset.All.Where(p => p.Type == "Character").Cast<object>().ToArray());
        if (ModEnums.SC_DIR.TryGetValue("Character", out var roots)) appearance.Items.AddRange(roots.Keys.Cast<object>().ToArray());
        Populate(faction, "Factions"); Populate(voiceSet, "VoiceSet"); Populate(weaponStyle, "WeaponCategories"); Populate(weapon, "Weapon"); Populate(offHand, "Weapon"); Populate(sidearm, "Weapon"); Populate(animationSet, "AnimationProfile");
        chatter.Items.Add("Inherit from appearance"); if (ModEnums.SC_DIR.TryGetValue("Chatter", out var lines)) chatter.Items.AddRange(lines.Keys.Cast<object>().ToArray()); chatter.SelectedIndex = 0;
        role.SelectedIndexChanged += (_, __) =>
        {
            var preset = (MakerPreset)role.SelectedItem; loading = true; appearance.SelectedItem = preset.Root;
            health.Value = (decimal?)preset.Values["Health"] ?? 10; level.Value = (int?)preset.Values["Level"] ?? 1;
            scale.Value = (decimal?)preset.Values["Scale"] ?? 1; tacBar.Value = (decimal?)preset.Values["TacBar"] ?? .5m;
            faction.SelectedIndex = voiceSet.SelectedIndex = weaponStyle.SelectedIndex = weapon.SelectedIndex = offHand.SelectedIndex = sidearm.SelectedIndex = animationSet.SelectedIndex = 0; enabled.Checked = true;
            description.Text = preset.Summary; loading = false; UpdateDraft();
        };
        name.TextChanged += (_, __) => { if (!customId) { loading = true; id.Text = MakerAssistedCharacter.SuggestId(name.Text); loading = false; } UpdateDraft(); };
        id.TextChanged += (_, __) => { if (!loading) customId = true; UpdateDraft(); };
        description.TextChanged += (_, __) => UpdateDraft(); appearance.SelectedIndexChanged += (_, __) => UpdateDraft(); chatter.SelectedIndexChanged += (_, __) => UpdateDraft();
        foreach (var number in new[] { health, level, scale, tacBar }) number.ValueChanged += (_, __) => UpdateDraft();
        foreach (var choice in new[] { faction, voiceSet, weaponStyle, weapon, offHand, sidearm, animationSet }) choice.SelectedIndexChanged += (_, __) => UpdateDraft(); enabled.CheckedChanged += (_, __) => UpdateDraft();
        create.Click += (_, __) => { try { Card = Build(); if(idExists!=null&&idExists((string)Card["Filename"]))throw new FormatException("That ID is already used in this deck. Choose another ID."); DialogResult = DialogResult.OK; Close(); } catch (Exception e) { guidance.Text = e.Message; } };
        loading = true; id.Text = MakerAssistedCharacter.SuggestId(name.Text); loading = false;
        DossierStyle.Apply(this); role.SelectedIndex = 0;
    }
    void Populate(ComboBox choice, string type)
    {
        choice.Items.Add("Inherit from starting role");
        if (ModEnums.SC_DIR.TryGetValue(type, out var entries)) choice.Items.AddRange(entries.Keys.Cast<object>().ToArray());
        else if (ModEnums.ENUMS.TryGetValue(type, out var options)) choice.Items.AddRange(options.Cast<object>().ToArray());
        choice.SelectedIndex = 0;
    }
    string Selected(ComboBox choice, string label, bool inherited = true)
    {
        for (int i = 0; i < choice.Items.Count; i++)
        {
            string item = choice.GetItemText(choice.Items[i]);
            if (!string.Equals(item, choice.Text, StringComparison.OrdinalIgnoreCase)) continue;
            if (inherited && i == 0) return null;
            choice.SelectedIndex = i;
            return item;
        }
        if (string.IsNullOrWhiteSpace(choice.Text) && inherited) return null;
        throw new FormatException("Choose a listed " + label + " or use its autocomplete suggestion.");
    }
    MakerPreset SelectedRole()
    {
        if (role.SelectedItem is MakerPreset selected && string.Equals(role.Text, selected.Name, StringComparison.OrdinalIgnoreCase)) return selected;
        var exact = MakerPreset.All.FirstOrDefault(p => string.Equals(p.Name, role.Text, StringComparison.OrdinalIgnoreCase) && p.Type == "Character");
        return exact ?? throw new FormatException("Choose a starting role from the suggestions.");
    }
    void ApplySuggestions()
    {
        if (string.IsNullOrWhiteSpace(appearance.Text) || !ModEnums.SC_DIR.TryGetValue("Character", out var characters) || !characters.TryGetValue(appearance.Text, out var source))
        {
            guidance.Text = "Choose a valid appearance first. Suggestions are read from that character's existing settings.";
            return;
        }
        Suggest(faction, source, "Faction"); Suggest(voiceSet, source, "VoiceSet"); Suggest(weaponStyle, source, "WeaponPref");
        Suggest(weapon, source, "DefaultWeapon"); Suggest(offHand, source, "DefaultOffHand"); Suggest(sidearm, source, "DefaultSidearm"); Suggest(animationSet, source, "AnimationSet");
        guidance.Text = "Suggested values were copied from the selected appearance where available. You can change any of them.";
    }
    static void Suggest(ComboBox choice, JObject source, string property)
    {
        string value = (string)source[property]; if (string.IsNullOrWhiteSpace(value)) return;
        for (int i = 1; i < choice.Items.Count; i++) if (string.Equals(choice.GetItemText(choice.Items[i]), value, StringComparison.OrdinalIgnoreCase)) { choice.SelectedIndex = i; return; }
    }
    JObject Build()
    {
        var selectedRole = SelectedRole();
        string selectedAppearance = Selected(appearance, "appearance", false);
        string selectedChatter = Selected(chatter, "speech bubble set");
        return MakerAssistedCharacter.Create(selectedRole, selectedAppearance, id.Text, name.Text, description.Text, health.Value, (int)level.Value, scale.Value, tacBar.Value, selectedChatter,
            Selected(faction, "faction"), Selected(voiceSet, "voice set"), Selected(weapon, "starting weapon"), Selected(offHand, "off-hand weapon"), Selected(animationSet, "animation set"), enabled.Checked,
            Selected(weaponStyle, "weapon style"), Selected(sidearm, "sidearm"));
    }
    void UpdateDraft() { if (loading || role.SelectedItem == null) return; try { preview.Text = Build().ToString(); } catch (Exception e) { preview.Text = e.Message; } }
}
