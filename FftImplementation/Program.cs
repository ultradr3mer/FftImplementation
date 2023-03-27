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
      var n = 2048;
      var coefficients = new Complex[n];
      coefficients[26] = new Complex(1.0, 0.0);
      //coefficients[16] = new Complex(0.5, 0.0);
      //coefficients[17] = new Complex(0.5, 0.0);
      //coefficients[18] = new Complex(0.5, 0.0);
      //coefficients[30] = new Complex(0.7, 0.0);
      //coefficients[31] = new Complex(0.7, 0.0);
      //coefficients[32] = new Complex(0.7, 0.0);

      int i = 0;
      //DrawGraph(coefficients.Select(c => new DataPoint(i++, c.Real)));

      var fft = new Fft(n);
      var wave = fft.Transform(coefficients).ToArray();
      //var wave = Enumerable.Range(0, n).Select(index => new Complex(Math.Sin(index * 100.0 / n)
      //  + Math.Sin(index * 3.0 / n)
      //  + Math.Sin(index * 700.0 / n)
      //  + Math.Sin(index * 8000.0 / n), 0)).ToArray();

      i = 0;
      DrawGraph(wave.Select(c => new DataPoint(i++, c.Real)));

      var spectrum = fft.Inverse(wave);

      i = 0;
      DrawGraph(spectrum.Select(c => new DataPoint(i++, c.Real)));

      var wave2 = fft.Transform(spectrum);

      i = 0;
      DrawGraph(wave2.Select(c => new DataPoint(i++, c.Real)));
    }

    #endregion Methods
  }
}