using NAudio.WaveFormRenderer;
using System.Drawing.Imaging;
using NAudio.Wave;
using NLayer.NAudioSupport;


namespace Firebeats.Uploads.Services
{
    public class AnalizeSong
    {
        public MaxPeakProvider maxPeakProvider;
        public RmsPeakProvider rmsPeakProvider;
        public SamplingPeakProvider samplingPeakProvider;
        public AveragePeakProvider averagePeakProvider;
        public StandardWaveFormRendererSettings myRendererSettings;
        public WaveFormTextRenderer renderer;

        public AnalizeSong()
        {
            this.maxPeakProvider = new MaxPeakProvider();
            this.rmsPeakProvider = new RmsPeakProvider(200);
            this.samplingPeakProvider = new SamplingPeakProvider(200);
            this.averagePeakProvider = new AveragePeakProvider(4);
            this.myRendererSettings = new StandardWaveFormRendererSettings();
            this.myRendererSettings.Width = 640;
            this.myRendererSettings.TopHeight = 32;
            this.myRendererSettings.BottomHeight = 0;
            this.renderer = new WaveFormTextRenderer();
            
        }
        public async Task<bool> CreateGraph(string file) {

            var builder = new Mp3FileReaderBase.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));
            var reader = new Mp3FileReaderBase(file, builder);


            try
            {
                
                var image = renderer.Render(reader, maxPeakProvider, myRendererSettings);
                var imagename = System.IO.Path.GetFileNameWithoutExtension(file) + ".png";

                image.Save(imagename, ImageFormat.Png);

                return true;
            }
            catch (Exception ex) {
                return false;
            }            

        }

    }
}
