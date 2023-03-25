using OxyPlot;
using OxyPlot.Series;
using OxyPlotWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FftImplementation
{
  internal class Program
  {
    #region Methods

    private static void DrawGraph(IEnumerable<DataPoint> data)
    {
      var model = new PlotModel { Title = "Example 1" };
      var lineSeries = new LineSeries();
      lineSeries.Points.AddRange(data);
      model.Series.Add(lineSeries);
      Visualizer.ShowModel(model);
    }

    [STAThread]
    private static void Main(string[] args)
    {
      var n = 256;
      var coefficients = new Complex[n];
      coefficients[10] = new Complex(1.0, 0.0);
      coefficients[15] = new Complex(0.5, 0.0);

      int i = 0;
      DrawGraph(coefficients.Select(c => new DataPoint(i++, c.Real)));

      var fft = new Fft(n);
      var wave = fft.Transform(coefficients);

      i = 0;
      DrawGraph(wave.Select(c => new DataPoint(i++, c.Real)));

      var wave2 = fft.Transform(wave);

      i = 0;
      DrawGraph(wave2.Reverse().Select(c => new DataPoint(i++, c.Real)));
    }

    #endregion Methods
  }
}