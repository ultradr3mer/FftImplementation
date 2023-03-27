﻿using Fft.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fft.Wpf.Views
{
  /// <summary>
  /// Interaction logic for PlotView.xaml
  /// </summary>
  public partial class PlotView : Window
  {
    public PlotView()
    {
      InitializeComponent();

      this.DataContext = this.ViewModel = new PlotViewModel();
      this.ViewModel.Initialize();
    }

    internal PlotViewModel ViewModel { get; }

  }
}
