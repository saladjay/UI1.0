using System;
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
using UIControlLib;
using UILib.Cores;
using ExtendedString;
namespace UI1._0.MyPages
{
    /// <summary>
    /// SystemPage.xaml 的互動邏輯
    /// </summary>
    public partial class SystemPage : Page
    {
        ObservableCollection<DeviceInfo> DeviceCollection = new ObservableCollection<DeviceInfo>();
        public SystemPage()
        {
            InitializeComponent();
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"/MyImages/APP-01.png";
            NavigationButton testb = new NavigationButton(imagePath);
            NavigationButton button1 = new NavigationButton(ImagePath.Image1);
            NavigationButton button2 = new NavigationButton(ImagePath.Image2);
            NavigationButton button3 = new NavigationButton(ImagePath.Image3);
            NavigationButton button4 = new NavigationButton(ImagePath.Image4);
            DeviceInfo device1 = new DeviceInfo("VAS-2406R", 5) { Index=1,Device = button1, DeviceID = 1, IP = "192.168.2.10", ConnectState = true, FaultState = false };
            DeviceInfo device2 = new DeviceInfo("VAS-2406EX", 5) { Index = 1, Device = button2, DeviceID = 1, IP = "192.168.2.10", ConnectState = true, FaultState = false };
            DeviceInfo device3 = new DeviceInfo("VAS-2406BS", 5) { Index = 1, Device = button3, DeviceID = 1, IP = "192.168.2.10", ConnectState = true, FaultState = false };
            DeviceInfo device4 = new DeviceInfo("VAS-64RPM", 5) { Index = 1, Device = button4, DeviceID = 1, IP = "192.168.2.10", ConnectState = true, FaultState = false };
            DeviceCollection.Add(device1);
            DeviceCollection.Add(device2);
            DeviceCollection.Add(device3);
            DeviceCollection.Add(device4);
            dataGrid.ItemsSource = DeviceCollection;
        }
    }

    public sealed class ImagePath
    {
        private static readonly string _OriginalPath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string APP_01 = _OriginalPath.Append(@"/MyImages/APP-01.png");
        public static readonly string APP_02 = _OriginalPath.Append(@"/MyImages/APP-02.png");
        public static readonly string APP_03 = _OriginalPath.Append(@"/MyImages/APP-03.png");
        public static readonly string APP_04 = _OriginalPath.Append(@"/MyImages/APP-04.png");
        public static readonly string APP_05 = _OriginalPath.Append(@"/MyImages/APP-05.png");
        public static readonly string APP_06 = _OriginalPath.Append(@"/MyImages/APP-06.png");
        public static readonly string APP_07 = _OriginalPath.Append(@"/MyImages/APP-07.png");
        public static readonly string APP_08 = _OriginalPath.Append(@"/MyImages/APP-08.png");
        public static readonly string APP_09 = _OriginalPath.Append(@"/MyImages/APP-09.png");
        public static readonly string APP_10 = _OriginalPath.Append(@"/MyImages/APP-10.png");
        public static readonly string APP_11 = _OriginalPath.Append(@"/MyImages/APP-11.png");
        public static readonly string Image1 = _OriginalPath.Append(@"/MyImages/Image1.png");
        public static readonly string Image2 = _OriginalPath.Append(@"/MyImages/Image2.png");
        public static readonly string Image3 = _OriginalPath.Append(@"/MyImages/Image3.png");
        public static readonly string Image4 = _OriginalPath.Append(@"/MyImages/Image4.png");
    }
}
