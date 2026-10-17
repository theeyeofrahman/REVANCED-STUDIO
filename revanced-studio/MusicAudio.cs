using System;
using NAudio.Dsp;
using NAudio.Wave;

namespace RevancedStudio;

// Analysis taps the decoded samples on the playback thread; painting reads a small snapshot.
public sealed class MusicSpectrum : ISampleProvider
{
    const int Size = 2048;
    readonly ISampleProvider source;
    readonly Complex[] fft = new Complex[Size];
    readonly float[] window = new float[Size];
    readonly float[] spectrum = new float[Size / 2];
    readonly object gate = new object();
    int position, channel;
    float mono;
    public volatile bool Analyze = true;
    public WaveFormat WaveFormat => source.WaveFormat;

    public MusicSpectrum(ISampleProvider source)
    {
        this.source = source;
        for (int i = 0; i < Size; i++) window[i] = (float)FastFourierTransform.HannWindow(i, Size);
    }

    public int Read(float[] buffer, int offset, int count)
    {
        int read = source.Read(buffer, offset, count);
        if (!Analyze) { position = channel = 0; mono = 0; return read; }
        for (int i = offset; i < offset + read; i++)
        {
            mono += buffer[i];
            if (++channel < WaveFormat.Channels) continue;
            fft[position].X = mono / channel * window[position];
            fft[position].Y = 0; mono = 0; channel = 0;
            if (++position != Size) continue;
            position = 0;
            FastFourierTransform.FFT(true, 11, fft);
            lock (gate)
                for (int j = 0; j < spectrum.Length; j++)
                    spectrum[j] = (float)Math.Sqrt(fft[j].X * fft[j].X + fft[j].Y * fft[j].Y);
        }
        return read;
    }

    public float[] Bands(int count)
    {
        var bands = new float[count];
        double nyquist = WaveFormat.SampleRate / 2d;
        double upper = Math.Min(16000, nyquist);
        lock (gate)
            for (int i = 0; i < count; i++)
            {
                int first = Math.Max(1, (int)(40 * Math.Pow(upper / 40, i / (double)count) / nyquist * spectrum.Length));
                int last = Math.Min(spectrum.Length - 1, Math.Max(first, (int)(40 * Math.Pow(upper / 40, (i + 1d) / count) / nyquist * spectrum.Length)));
                float peak = 0;
                for (int j = first; j <= last; j++) peak = Math.Max(peak, spectrum[j]);
                bands[i] = (float)Math.Max(0, Math.Min(1, (20 * Math.Log10(Math.Max(1e-7, peak)) + 72) / 66));
            }
        return bands;
    }
}
