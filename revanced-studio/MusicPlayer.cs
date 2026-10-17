using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using M = MadCardMaker;

namespace RevancedStudio;

public sealed class MusicPreferences
{
    public List<string> Tracks { get; set; } = new List<string>();
    public int Volume { get; set; } = 25;
    public bool Shuffle { get; set; }
    public string Repeat { get; set; } = "Off";
    public static MusicPreferences Read(string path)
    {
        try
        {
            var value = JsonConvert.DeserializeObject<MusicPreferences>(File.ReadAllText(path)) ?? new MusicPreferences();
            value.Tracks = (value.Tracks ?? new List<string>()).Where(p => !string.IsNullOrWhiteSpace(p)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            value.Volume = Math.Max(0, Math.Min(100, value.Volume));
            if (!new[] { "Off", "Playlist", "Track" }.Contains(value.Repeat)) value.Repeat = "Off";
            return value;
        }
        catch { return new MusicPreferences(); }
    }
}

public sealed class MusicPlayer : Form
{
    static readonly HashSet<string> Extensions = new HashSet<string>(new[] { ".mp3", ".wav", ".aif", ".aiff", ".wma", ".m4a", ".aac" }, StringComparer.OrdinalIgnoreCase);
    readonly string settingsPath;
    readonly MusicPreferences preferences;
    readonly ListBox playlist = new ListBox { Dock = DockStyle.Fill, HorizontalScrollbar = true, AccessibleName = "Music playlist" };
    readonly Label title = new Label { Dock = DockStyle.Fill, Text = "Your editing soundtrack", AutoEllipsis = true, TextAlign = ContentAlignment.MiddleLeft };
    readonly Label status = new Label { Dock = DockStyle.Fill, AutoEllipsis = true, Text = "Add local music or drop audio files here. Double-click a track to play." };
    readonly Label time = new Label { AutoSize = true, Text = "0:00 / 0:00" };
    readonly MusicSlider seek = new MusicSlider { Dock = DockStyle.Fill, Maximum = 1000, AccessibleName = "Track position" };
    readonly MusicSlider volume = new MusicSlider { Width = 160, Height = 44, Maximum = 100, AccessibleName = "Music volume" };
    readonly Button shuffle = new M.DossierButton { Width = 140, Height = 44 };
    readonly Button repeat = new M.DossierButton { Width = 175, Height = 44, AccessibleName = "Repeat mode" };
    readonly SpectrumView visualizer = new SpectrumView { Dock = DockStyle.Fill };
    readonly Timer timer = new Timer { Interval = 33 };
    readonly Random random = new Random();
    readonly List<int> history = new List<int>();
    readonly Button play;
    AudioFileReader reader;
    WaveOutEvent output;
    MusicSpectrum analyzer;
    VolumeSampleProvider gain;
    int current = -1;
    bool seeking, updating, shutdown;

    public MusicPlayer(string path = null)
    {
        settingsPath = path ?? Path.Combine(StudioState.DirectoryPath, "music.json");
        preferences = MusicPreferences.Read(settingsPath);
        Text = "Studio Music"; Size = new Size(920, 820); MinimumSize = new Size(860, 680);
        StartPosition = FormStartPosition.CenterParent; AutoScaleMode = AutoScaleMode.Dpi; AutoScaleDimensions = new SizeF(96, 96);
        AllowDrop = true;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(24), ColumnCount = 1, RowCount = 7 };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        foreach (int height in new[] { 48, 190, 50, 54, 54 }) root.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        root.Controls.Add(title, 0, 0); root.Controls.Add(visualizer, 0, 1);
        var timeline = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        timeline.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); timeline.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155));
        time.Anchor = AnchorStyles.Left; timeline.Controls.Add(seek, 0, 0); timeline.Controls.Add(time, 1, 0); root.Controls.Add(timeline, 0, 2);
        var transport = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        transport.Controls.Add(ActionButton("Previous", () => Previous(), 110));
        play = ActionButton("Play", Toggle, 110); transport.Controls.Add(play);
        transport.Controls.Add(ActionButton("Stop", Stop, 100)); transport.Controls.Add(ActionButton("Next", () => Next(true), 100));
        var volumeLabel = new Label { Text = "Volume", AutoSize = true, Margin = new Padding(14, 12, 0, 0) };
        transport.Controls.Add(volumeLabel); volume.Value = preferences.Volume; transport.Controls.Add(volume); root.Controls.Add(transport, 0, 3);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        actions.Controls.Add(ActionButton("Add music", AddMusic, 130)); actions.Controls.Add(ActionButton("Remove", Remove, 110));
        actions.Controls.Add(ActionButton("Clear list", Clear, 110)); shuffle.Text = "Shuffle: " + (preferences.Shuffle ? "On" : "Off"); shuffle.Margin = new Padding(8, 0, 8, 4); actions.Controls.Add(shuffle);
        repeat.Text = "Repeat: " + preferences.Repeat; actions.Controls.Add(repeat);
        root.Controls.Add(actions, 0, 4); root.Controls.Add(playlist, 0, 5); root.Controls.Add(status, 0, 6); Controls.Add(root);
        RefreshPlaylist();
        M.StudioModule.Style(this);
        title.Font = M.DossierFonts.Create(M.DossierTheme.Load().HeadingFont, 18, FontStyle.Bold);
        foreach (Control c in new Control[] { playlist, status, time, volumeLabel, shuffle, repeat, play }) c.Font = M.DossierFonts.Create(M.DossierTheme.Load().FontFamily, 11);
        visualizer.BackColor = ColorTranslator.FromHtml(M.DossierTheme.Load().Input);
        playlist.DoubleClick += (_, __) => { if (playlist.SelectedIndex >= 0) StartTrack(playlist.SelectedIndex); };
        playlist.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { if (playlist.SelectedIndex >= 0) StartTrack(playlist.SelectedIndex); e.SuppressKeyPress = true; } else if (e.KeyCode == Keys.Delete) Remove(); };
        volume.ValueChanged += (_, __) => { if (gain != null) gain.Volume = volume.Value / 100f; preferences.Volume = volume.Value; };
        shuffle.Click += (_, __) => { preferences.Shuffle = !preferences.Shuffle; shuffle.Text = "Shuffle: " + (preferences.Shuffle ? "On" : "Off"); };
        repeat.Click += (_, __) => { var modes = new[] { "Off", "Playlist", "Track" }; preferences.Repeat = modes[(Array.IndexOf(modes, preferences.Repeat) + 1) % modes.Length]; repeat.Text = "Repeat: " + preferences.Repeat; };
        seek.DragStarted += (_, __) => seeking = true;
        seek.UserChanged += (_, __) => { seeking = false; Seek(); };
        timer.Tick += (_, __) => TickPlayer();
        VisibleChanged += (_, __) => { if (analyzer != null) analyzer.Analyze = Visible; if (Visible) timer.Start(); else timer.Stop(); };
        FormClosing += (_, e) => { if (!shutdown && e.CloseReason == CloseReason.UserClosing) { e.Cancel = true; Hide(); Persist(); } };
        DragEnter += (_, e) => e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        DragDrop += (_, e) => AddPaths((string[])e.Data.GetData(DataFormats.FileDrop));
    }

    Button ActionButton(string text, Action action, int width)
    {
        var button = new M.DossierButton { Text = text, Width = width, Height = 44, Margin = new Padding(0, 0, 8, 4) };
        button.Click += (_, __) => { try { action(); } catch (Exception e) { status.Text = e.Message; } }; return button;
    }
    public static bool Supports(string path) => Extensions.Contains(Path.GetExtension(path));
    public void AddPaths(IEnumerable<string> paths)
    {
        int added = 0, skipped = 0;
        foreach (string path in paths)
        {
            if (!File.Exists(path) || !Supports(path)) { skipped++; continue; }
            string full = Path.GetFullPath(path);
            if (preferences.Tracks.Contains(full, StringComparer.OrdinalIgnoreCase)) continue;
            preferences.Tracks.Add(full); added++;
        }
        RefreshPlaylist(); Persist(); status.Text = $"Added {added} tracks." + (skipped > 0 ? $" Skipped {skipped} missing or unsupported files." : " Select a track and press Play.");
    }
    void AddMusic()
    {
        using (var browser = new M.DossierFileExplorer(M.DossierBrowseMode.Open, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), filters: "Audio files|*.mp3;*.wav;*.aif;*.aiff;*.wma;*.m4a;*.aac|All files|*.*") { Multiselect = true })
            if (browser.ShowDialog(this) == DialogResult.OK) AddPaths(browser.SelectedPaths);
    }
    void RefreshPlaylist()
    {
        int selected = playlist.SelectedIndex; playlist.BeginUpdate(); playlist.Items.Clear();
        foreach (string path in preferences.Tracks) playlist.Items.Add((File.Exists(path) ? "" : "[Missing] ") + Path.GetFileName(path));
        playlist.EndUpdate(); if (playlist.Items.Count > 0) playlist.SelectedIndex = Math.Max(0, Math.Min(selected, playlist.Items.Count - 1));
    }
    void Toggle()
    {
        if (output == null) { if (playlist.Items.Count > 0) StartTrack(Math.Max(0, playlist.SelectedIndex)); else status.Text = "Add music to get started."; return; }
        if (output.PlaybackState == PlaybackState.Playing) { output.Pause(); play.Text = "Resume"; status.Text = "Paused"; }
        else { output.Play(); play.Text = "Pause"; status.Text = "Playing · " + Path.GetFileName(preferences.Tracks[current]); }
    }
    void StartTrack(int index, bool remember = true)
    {
        if (index < 0 || index >= preferences.Tracks.Count) return;
        ReleaseAudio();
        try
        {
            reader = new AudioFileReader(preferences.Tracks[index]);
            gain = new VolumeSampleProvider(reader) { Volume = volume.Value / 100f };
            analyzer = new MusicSpectrum(gain) { Analyze = Visible };
            var device = new WaveOutEvent { DesiredLatency = 120 };
            output = device; device.Init(new SampleToWaveProvider(analyzer));
            device.PlaybackStopped += (_, e) =>
            {
                if (shutdown || !IsHandleCreated || IsDisposed) return;
                try { BeginInvoke((Action)(() => { if (shutdown || IsDisposed || !ReferenceEquals(device, output)) return; if (e.Exception != null) { Stop(); status.Text = "Audio stopped: " + e.Exception.Message; } else Next(false); })); }
                catch (InvalidOperationException) { }
            };
            if (remember && current >= 0) history.Add(current);
            current = index; playlist.SelectedIndex = index; title.Text = Path.GetFileNameWithoutExtension(preferences.Tracks[index]);
            device.Play(); play.Text = "Pause"; status.Text = "Playing · " + Path.GetFileName(preferences.Tracks[index]);
        }
        catch (Exception e) { ReleaseAudio(); play.Text = "Play"; status.Text = "Could not play this track: " + e.Message; }
    }
    void Stop() { ReleaseAudio(); seek.Value = 0; time.Text = "0:00 / 0:00"; play.Text = "Play"; visualizer.SetBands(null); status.Text = "Stopped"; }
    void ReleaseAudio()
    {
        var device = output; output = null;
        if (device != null) { try { device.Stop(); } finally { device.Dispose(); } }
        reader?.Dispose(); reader = null; analyzer = null; gain = null;
    }
    void Next(bool manual)
    {
        int count = preferences.Tracks.Count;
        if (count == 0) { Stop(); return; }
        if (!manual && preferences.Repeat == "Track") { StartTrack(Math.Max(0, current), false); return; }
        int next = current + 1;
        if (preferences.Shuffle && count > 1) { next = random.Next(count - 1); if (next >= current) next++; }
        else if (next >= count) { if (manual || preferences.Repeat == "Playlist") next = 0; else { Stop(); status.Text = "Playlist finished"; return; } }
        StartTrack(next);
    }
    void Previous()
    {
        if (reader != null && reader.CurrentTime.TotalSeconds > 3) { reader.CurrentTime = TimeSpan.Zero; return; }
        int previous = history.Count > 0 ? history[history.Count - 1] : Math.Max(0, current - 1);
        if (history.Count > 0) history.RemoveAt(history.Count - 1); StartTrack(previous, false);
    }
    void Seek()
    {
        if (updating || reader == null) return;
        try { reader.CurrentTime = TimeSpan.FromTicks((long)(reader.TotalTime.Ticks * seek.Value / 1000d)); }
        catch (Exception e) { status.Text = "Could not seek: " + e.Message; }
    }
    static string Stamp(TimeSpan t) => ((int)t.TotalMinutes).ToString() + ":" + t.Seconds.ToString("00");
    void TickPlayer()
    {
        if (reader != null)
        {
            if (!seeking && reader.TotalTime.Ticks > 0) { updating = true; seek.Value = (int)Math.Max(0, Math.Min(1000, reader.CurrentTime.Ticks * 1000d / reader.TotalTime.Ticks)); updating = false; }
            time.Text = Stamp(reader.CurrentTime) + " / " + Stamp(reader.TotalTime);
        }
        visualizer.SetBands(output?.PlaybackState == PlaybackState.Playing ? analyzer?.Bands(48) : null);
    }
    void Remove()
    {
        int index = playlist.SelectedIndex; if (index < 0) return;
        if (index == current) { Stop(); current = -1; title.Text = "Your editing soundtrack"; } else if (index < current) current--;
        preferences.Tracks.RemoveAt(index); history.Clear(); RefreshPlaylist(); Persist();
    }
    void Clear() { Stop(); current = -1; history.Clear(); preferences.Tracks.Clear(); RefreshPlaylist(); title.Text = "Your editing soundtrack"; Persist(); }
    void Persist() { try { M.MakerHelp.Write(settingsPath, JsonConvert.SerializeObject(preferences, Formatting.Indented)); } catch (Exception e) { status.Text = "Music preferences could not be saved: " + e.Message; } }
    protected override void Dispose(bool disposing)
    {
        if (disposing && !shutdown) { shutdown = true; timer.Stop(); timer.Dispose(); ReleaseAudio(); Persist(); }
        base.Dispose(disposing);
    }
}

public sealed class SpectrumView : Control
{
    readonly float[] levels = new float[48];
    readonly Color accent = ColorTranslator.FromHtml(M.DossierTheme.Load().Accent);
    public SpectrumView() { SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true); AccessibleName = "Live audio spectrum, low to high frequencies"; }
    public void SetBands(float[] bands)
    {
        for (int i = 0; i < levels.Length; i++) { float target = bands == null ? 0 : bands[i]; levels[i] += (target - levels[i]) * (target > levels[i] ? .7f : .22f); }
        Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); if (Width < 20 || Height < 30) return;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var outline = new Pen(Color.FromArgb(60, accent)))
        using (var shape = Squircle(new RectangleF(1, 1, Width - 3, Height - 3), 22)) e.Graphics.DrawPath(outline, shape);
        float unit = (Width - 40f) / levels.Length;
        for (int i = 0; i < levels.Length; i++)
        {
            float height = Math.Max(3, levels[i] * (Height - 50));
            var bounds = new RectangleF(20 + i * unit, Height - 24 - height, Math.Max(2, unit - 3), height);
            using (var path = Squircle(bounds, Math.Min(5, bounds.Width / 2)))
            using (var fill = new LinearGradientBrush(new RectangleF(bounds.X, 0, bounds.Width, Height), Color.FromArgb(245, 191, 179), accent, 90)) e.Graphics.FillPath(fill, path);
        }
    }
    internal static GraphicsPath Squircle(RectangleF r, float radius)
    {
        float a = Math.Max(1, Math.Min(radius, Math.Min(r.Width, r.Height) / 2)); float k = a * .18f;
        var p = new GraphicsPath(); p.StartFigure();
        p.AddLine(r.Left + a, r.Top, r.Right - a, r.Top); p.AddBezier(r.Right - a, r.Top, r.Right - k, r.Top, r.Right, r.Top + k, r.Right, r.Top + a);
        p.AddLine(r.Right, r.Top + a, r.Right, r.Bottom - a); p.AddBezier(r.Right, r.Bottom - a, r.Right, r.Bottom - k, r.Right - k, r.Bottom, r.Right - a, r.Bottom);
        p.AddLine(r.Right - a, r.Bottom, r.Left + a, r.Bottom); p.AddBezier(r.Left + a, r.Bottom, r.Left + k, r.Bottom, r.Left, r.Bottom - k, r.Left, r.Bottom - a);
        p.AddLine(r.Left, r.Bottom - a, r.Left, r.Top + a); p.AddBezier(r.Left, r.Top + a, r.Left, r.Top + k, r.Left + k, r.Top, r.Left + a, r.Top); p.CloseFigure(); return p;
    }
}
