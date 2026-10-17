using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using RevancedStudio;
using M = MadCardMaker;

static class MusicChecks
{
    static int checks;
    static void Check(bool value, string why) { if (!value) throw new Exception(why); checks++; }
    public static void Run(string folder)
    {
        Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
        M.DossierFeedback.MutedForTests = true; M.DossierMotion.SuppressForTests = true; M.DossierPreferences.SuppressPersistenceForTests = true;
        Directory.CreateDirectory(folder);
        string wave = Path.Combine(folder, "spectrum-check.wav"), settings = Path.Combine(folder, "music-check.json");
        var samples = new float[48000 * 2];
        for (int i = 0; i < samples.Length / 2; i++) samples[i * 2] = samples[i * 2 + 1] = (float)(.5 * Math.Sin(2 * Math.PI * 1000 * i / 48000));
        using (var writer = new WaveFileWriter(wave, WaveFormat.CreateIeeeFloatWaveFormat(48000, 2))) writer.WriteSamples(samples, 0, samples.Length);
        using (var decoded = new AudioFileReader(wave))
        {
            var gain = new VolumeSampleProvider(decoded) { Volume = .5f }; var tap = new MusicSpectrum(gain);
            var buffer = new float[4096]; int read, total = 0; float peak = 0;
            while ((read = tap.Read(buffer, 0, buffer.Length)) > 0) { total += read; for (int i = 0; i < read; i++) peak = Math.Max(peak, Math.Abs(buffer[i])); }
            Check(total == samples.Length, "Analysis dropped decoded audio samples");
            Check(Math.Abs(peak - .25) < .001, "Music volume changes the wrong audio level");
            var bands = tap.Bands(48); int strongest = Array.IndexOf(bands, bands.Max());
            Check(bands.Max() > .5, "Visualizer does not react to actual audio");
            double frequency = 40 * Math.Pow(16000d / 40, (strongest + .5) / 48);
            Check(frequency > 800 && frequency < 1250, "Spectrum assigns a 1 kHz signal to the wrong frequency band");
            decoded.CurrentTime = TimeSpan.FromSeconds(.5); Check(Math.Abs(decoded.CurrentTime.TotalSeconds - .5) < .01, "Audio position cannot seek");
            gain.Volume = 0; while (tap.Read(buffer, 0, buffer.Length) > 0) { }
            Check(tap.Bands(48).Max() == 0, "Visualizer should go silent when muted");
            decoded.CurrentTime = TimeSpan.Zero; gain.Volume = 1; tap.Analyze = false; Check(tap.Read(buffer, 0, buffer.Length) > 0, "Hidden visualizer interrupts playback");
        }
        Check(MusicPlayer.Supports("test.MP3") && !MusicPlayer.Supports("test.exe"), "Audio file filter is incorrect");
        using(var slider=new MusicSlider{Maximum=100}){slider.Value=200;Check(slider.Value==100,"Music slider does not clamp its range");int changed=0;slider.UserChanged+=(_,__)=>changed++;var key=typeof(MusicSlider).GetMethod("OnKeyDown",BindingFlags.NonPublic|BindingFlags.Instance);key.Invoke(slider,new object[]{new KeyEventArgs(Keys.Home)});key.Invoke(slider,new object[]{new KeyEventArgs(Keys.Right)});Check(slider.Value==2&&changed==2,"Music slider keyboard seeking is broken");}
        File.WriteAllText(settings, "{\"Volume\":250,\"Repeat\":\"unknown\",\"Tracks\":[null,\"\",\"missing.mp3\",\"missing.mp3\"]}");
        var recovered = MusicPreferences.Read(settings); Check(recovered.Volume == 100 && recovered.Repeat == "Off" && recovered.Tracks.Count == 1, "Stored playlist values are not sanitized");
        File.Delete(settings);
        using (var player = new MusicPlayer(settings))
        {
            player.AddPaths(new[] { wave, wave.ToUpperInvariant(), Path.Combine(folder, "absent.wav"), settings });
            Check(MusicPreferences.Read(settings).Tracks.Count == 1, "Playlist duplicates or missing files were added");
            var content = player.Controls.OfType<TableLayoutPanel>().First(); player.Controls.Remove(content);
            content.Dock = DockStyle.None; content.Size = new Size(980, 820); content.Visible = true; content.CreateControl(); content.PerformLayout();
            using (var picture = new Bitmap(content.Width, content.Height)) { content.DrawToBitmap(picture, new Rectangle(Point.Empty, content.Size)); picture.Save(Path.Combine(folder, "music.png")); }
            var list = (ListBox)typeof(MusicPlayer).GetField("playlist", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player);
            Check(list.Region != null && !list.Region.IsVisible(0, 0) && list.Region.IsVisible(list.Width / 2, list.Height / 2), "Playlist squircle shape clips the wrong region");
            player.Controls.Add(content); Check(!player.Visible, "Music checks unexpectedly showed a window");
        }
        Check(MusicPreferences.Read(settings).Tracks.Count == 1, "Playlist was not retained after closing");
        Console.WriteLine("PASS: " + checks + " music checks. Decoding, volume, seeking, spectrum, playlist persistence and hidden rendering verified. No audio device or window was opened.");
    }
}
