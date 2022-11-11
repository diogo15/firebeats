using System;
using NAudio.Wave;
using NAudio.WaveFormRenderer;

namespace Firebeats.Uploads.Services
{
    public class MyPeak : IPeakProvider
    {
        private readonly IPeakProvider sourceProvider;

        private readonly double dynamicRange;

        public MyPeak(IPeakProvider sourceProvider, double dynamicRange)
        {
            this.sourceProvider = sourceProvider;
            this.dynamicRange = dynamicRange;
        }

        public void Init(ISampleProvider reader, int samplesPerPixel)
        {
            throw new NotImplementedException();
        }

        public PeakInfo GetNextPeak()
        {
            PeakInfo nextPeak = sourceProvider.GetNextPeak();
            double num = 20.0 * Math.Log10(nextPeak.Max);
            if (num < 0.0 - dynamicRange)
            {
                num = 0.0 - dynamicRange;
            }

            float num2 = (float)((dynamicRange + num) / dynamicRange);
            return new PeakInfo(0f - num2, num2);
        }
    }
}
