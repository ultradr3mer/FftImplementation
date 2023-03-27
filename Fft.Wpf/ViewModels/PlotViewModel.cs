using NAudio.Dsp;
using NAudio.Wave;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fft.Wpf.ViewModels
{
  internal class PlotViewModel : INotifyPropertyChanged
  {
    private PlotModel plotModel;

    private List<float> buffer = new List<float>();
    private Services.Fft fft;

    public PlotModel PlotModel
    {
      get => plotModel; set
      {
        if (plotModel != value)
        {
          plotModel = value;
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlotViewModel.PlotModel)));
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

        public PlotViewModel()
        {
      this.fft = new Services.Fft(2048);
        }

    internal void Initialize()
    {
      WaveStream reader = new WaveFileReader(@"D:\Music\Intoxication.wav");
      var inputStream = new WaveChannel32(reader);
      inputStream.Sample += volumeStream_Sample;
      WaveChannel32 volumeStream = new WaveChannel32(inputStream);

      WaveOutEvent player = new WaveOutEvent();

      player.Init(volumeStream);
      player.Play();
    }

    private void volumeStream_Sample(object sender, SampleEventArgs e)
    {
      buffer.Add(e.Right);
      if (buffer.Count == 2048) 
      {
        var copy = buffer.Select(v => new System.Numerics.Complex(v, 0)).ToArray();
        buffer = new List<float>();
        Task.Factory.StartNew(() =>
        {
          var spectrum = fft.Transform(copy);
          int i = 0;
          var plot = spectrum.Select(c => new DataPoint(i++, c.Real));

          System.Windows.Application.Current.Dispatcher.Invoke((Action)(() => { DrawGraph(plot); }));
        });

      }
    }

    private void DrawGraph(IEnumerable<DataPoint> data)
    {
      var model = new PlotModel { Title = "Example 1" };
      var lineSeries = new LineSeries();
      lineSeries.Points.AddRange(data);
      model.Series.Add(lineSeries);
      this.PlotModel = model;
    }

  }
}
