using OxyPlot;
using System;
using System.Windows;

namespace OxyPlotWindows
{
  /// <summary>
  /// Interaktionslogik für PlotWindow.xaml
  /// </summary>
  public partial class PlotWindow : Window
  {
    #region Constructors

    public PlotWindow(PlotModel plotModel)
    {
      InitializeComponent();
      PlotModel = plotModel;
      this.DataContext = this;
    }

    #endregion Constructors

    #region Properties

    public PlotModel PlotModel { get; }

    #endregion Properties
  }
}