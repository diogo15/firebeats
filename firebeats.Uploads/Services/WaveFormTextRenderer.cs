using System;
using System.Drawing;
using NAudio.Wave;
using NAudio.WaveFormRenderer;

namespace Firebeats.Uploads.Services
{
    public class WaveFormTextRenderer
    {
        public string Render(WaveStream waveStream, WaveFormRendererSettings settings)
        {
            return Render(waveStream, new MaxPeakProvider(), settings);
        }

        public string Render(WaveStream waveStream, IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            int bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8);
            var samples = waveStream.Length / (bytesPerSample);
            var samplesPerPixel = (int)(samples / settings.Width);
            var stepSize = settings.PixelsPerPeak + settings.SpacerPixels;
            peakProvider.Init(waveStream.ToSampleProvider(), samplesPerPixel * stepSize);
            return Render(peakProvider, settings);
        }

        private static string Render(IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            if (settings.DecibelScale)
                peakProvider = new MyPeak(peakProvider, 48);

            var b = "[";
                
            var midPoint = settings.TopHeight;

            int x = 0;
            var currentPeak = peakProvider.GetNextPeak();
            while (x < settings.Width)
            {
                var nextPeak = peakProvider.GetNextPeak();

                b += "[";
                b += (settings.TopHeight * currentPeak.Max).ToString() + ",";
                b += (settings.BottomHeight * currentPeak.Min).ToString() + "],";

                x++;

                currentPeak = nextPeak;
            
            }

            b = b.Remove(b.Length - 1);

            b += "]";

            return b;
        }


    }
}