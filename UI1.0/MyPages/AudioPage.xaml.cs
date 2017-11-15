using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using UIControlLib;
namespace UI1._0.MyPages
{
    /// <summary>
    /// AudioPage.xaml 的互動邏輯
    /// </summary>
    public partial class AudioPage : Page
    {
        private ObservableCollection<DeviceInfo> _DeviceCollection = new ObservableCollection<DeviceInfo>();
        public AudioPage()
        {
            InitializeComponent();
            deviceViewGrid.ItemsSource2 = _DeviceCollection;
            Debug.WriteLine(deviceViewGrid.IsLoaded);
            _DeviceCollection.Add(new DeviceInfo("first", 2));
            _DeviceCollection.RemoveAt(0);
            _DeviceCollection.Add(new DeviceInfo("first", 2));
            _DeviceCollection.Add(new DeviceInfo("second", 2));
            _DeviceCollection.Add(new DeviceInfo("third", 4));
            _DeviceCollection.RemoveAt(2);
            _DeviceCollection.Add(new DeviceInfo("third", 1));
            _DeviceCollection.Add(new DeviceInfo("third", 4));
            _DeviceCollection.Add(new DeviceInfo("third", 4));
            _DeviceCollection.RemoveAt(3);
            _DeviceCollection.RemoveAt(1);
        }
    }
}
