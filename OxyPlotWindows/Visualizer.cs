using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyPlotWindows
{
  public class Visualizer
  {
    public static void ShowModel(PlotModel model)
    {
      var window = new PlotWindow(model);
      window.ShowDialog();
    }
  }
}
