﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UILib.Cores;

namespace UI1._0.MyPages
{
    /// <summary>
    /// ZoneSettingPage.xaml 的互動邏輯
    /// </summary>
    public partial class ZoneSettingPage : Page
    {
        ObservableCollection<IPort> _Ports = new ObservableCollection<IPort>();
        public ZoneSettingPage()
        {
            InitializeComponent();
            zoneViewGrid.ItemsSource = _Ports;
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
            _Ports.Add(new InputInfo());
            _Ports.Add(new OutputInfo());
        }
    }
}
