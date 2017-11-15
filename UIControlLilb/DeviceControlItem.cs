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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UILib.Cores;

namespace UIControlLib
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControlLilb"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControlLilb;assembly=UIControlLilb"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:DeviceControlItem/>
    ///
    /// </summary>
    public class DeviceControlItem : Control
    {
        static DeviceControlItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceControlItem), new FrameworkPropertyMetadata(typeof(DeviceControlItem)));
        }

        #region style switch
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(_Direction), typeof(DeviceControlItem));

        public _Direction Direction
        {
            get { return (_Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty FirstLevelProperty = DependencyProperty.Register("FirstLevel", typeof(bool), typeof(DeviceControlItem), new PropertyMetadata(false));
        public bool FirstLevel
        {
            get { return (bool)GetValue(FirstLevelProperty); }
            set { SetValue(FirstLevelProperty, value); }
        }
        #endregion

        #region Header
        public static readonly DependencyProperty HeaderNameProperty = DependencyProperty.Register("HeaderName", typeof(string), typeof(DeviceControlItem));
        public string HeaderName
        {
            get { return (string)GetValue(HeaderNameProperty); }
            set { SetValue(HeaderNameProperty, value); }
        }
        #endregion

        #region expand and collapse
        public static readonly DependencyProperty OpenProperty = DependencyProperty.Register("Open", typeof(bool), typeof(DeviceControlItem), new PropertyMetadata(false, OnOpenChanged));

        private static void OnOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DeviceControlItem tempItem = d as DeviceControlItem;
            if (tempItem == null)
            {
                return;
            }
            tempItem.ExpandItems?.Invoke((bool)e.NewValue, tempItem);
        }

        public bool Open
        {
            get { return (bool)GetValue(OpenProperty); }
            set { SetValue(OpenProperty, value); }
        }
        #endregion

        #region select and unselect
        public static readonly DependencyProperty MouseSelectedProperty = DependencyProperty.Register("MouseSelected", typeof(bool), typeof(DeviceControlItem),
            new PropertyMetadata(false));
        public bool MouseSelected
        {
            get { return (bool)GetValue(MouseSelectedProperty); }
            set { SetValue(MouseSelectedProperty, value); }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            SelectItem?.Invoke(true, this);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
        }
        #endregion
        #region IsConnected
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register("IsConnected", typeof(bool), typeof(DeviceControlItem),
            new PropertyMetadata(false, OnIsConnectedChanged));

        private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DeviceControlItem TempItem = d as DeviceControlItem;
            //TempItem.HookVisible = (bool)e.NewValue ? true : false;
        }

        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(MouseSelectedProperty, value); }
        }

        //public static readonly DependencyProperty HookVisibleProperty = DependencyProperty.Register("HookVisible", typeof(bool), typeof(DeviceControlItem));
        //public bool HookVisible
        //{
        //    get { return (bool)GetValue(HookVisibleProperty); }
        //    set { SetValue(HookVisibleProperty, value); }
        //}
        #endregion

        public delegate void ExpandItemsHandler(bool IsOpen, DeviceControlItem Source);
        public event ExpandItemsHandler ExpandItems;
        public delegate void SelectItemHandler(bool IsSelect, DeviceControlItem Source);
        public event SelectItemHandler SelectItem;

    }

}

