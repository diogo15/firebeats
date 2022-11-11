using NAudio.WaveFormRenderer;
using System.Drawing.Imaging;
using NAudio.Wave;
using NLayer.NAudioSupport;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Reflection.PortableExecutable;
using System.IO;

namespace Firebeats.Uploads.Services
{
    public class AnalizeSong
    {
        public MaxPeakProvider maxPeakProvider;
        public RmsPeakProvider rmsPeakProvider;
        public SamplingPeakProvider samplingPeakProvider;
        public AveragePeakProvider averagePeakProvider;
        public StandardWaveFormRendererSettings myRendererSettings;
        public WaveFormRenderer renderer;

        public AnalizeSong()
        {
            maxPeakProvider = new MaxPeakProvider();
            rmsPeakProvider = new RmsPeakProvider(200);
            samplingPeakProvider = new SamplingPeakProvider(200); 
            averagePeakProvider = new AveragePeakProvider(4); 
            myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = 640;
            myRendererSettings.TopHeight = 64;
            myRendererSettings.BottomHeight = 0;
            renderer = new WaveFormRenderer();
        }
        public async Task<bool> CreateGraph(string fileName) {

            var builder = new Mp3FileReaderBase.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            var reader = new Mp3FileReaderBase(fileName, builder);

            var image = renderer.Render(reader, maxPeakProvider, myRendererSettings);

            try
            {
                var imagename = System.IO.Path.GetFileNameWithoutExtension(fileName);

                image.Save(imagename, ImageFormat.Png);

                return true;
            }
            catch (Exception ex) {
                return false;
            }            

        }

    }
}
