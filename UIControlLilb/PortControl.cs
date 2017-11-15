using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIControlLib
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControlLib"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:UIControlLib;assembly=UIControlLib"
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
    ///     <MyNamespace:PortControl/>
    ///
    /// </summary>
    public class PortControl : Control
    {
        static PortControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PortControl), new FrameworkPropertyMetadata(typeof(PortControl)));
        }

        public static readonly DependencyProperty PortTypeNameProperty = DependencyProperty.Register("PortTypeName", typeof(string), typeof(PortControl));
        public string PortTypeName
        {
            get { return (string)GetValue(PortTypeNameProperty); }
            set { SetValue(PortTypeNameProperty, value); }
        }

        public static readonly DependencyProperty PortNameProperty = DependencyProperty.Register("PortName", typeof(string), typeof(PortControl));
        public string PortName
        {
            get { return (string)GetValue(PortNameProperty); }
            set { SetValue(PortNameProperty, value); }
        }

        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(PortControl), new PropertyMetadata(0));
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly DependencyProperty FirstComboBoxContentProperty = DependencyProperty.Register("FirstComboBoxContent", typeof(IEnumerable<string>), typeof(PortControl));
        public IEnumerable<string> FirstComboBoxContent
        {
            get { return (IEnumerable<string>)GetValue(FirstComboBoxContentProperty); }
            set { SetValue(FirstComboBoxContentProperty, value); }
        }

        public static readonly DependencyProperty SecondComboBoxContentProperty = DependencyProperty.Register("SecondComboBoxContent", typeof(IEnumerable<string>), typeof(PortControl));
        public IEnumerable<string> SecondComboBoxContent
        {
            get { return (IEnumerable<string>)GetValue(SecondComboBoxContentProperty); }
            set { SetValue(SecondComboBoxContentProperty, value); }
        }

        public static readonly DependencyProperty ThirdComboBoxContentProperty = DependencyProperty.Register("ThirdComboBoxContent", typeof(IEnumerable<string>), typeof(PortControl));
        public IEnumerable<string> ThirdComboBoxContent
        {
            get { return (IEnumerable<string>)GetValue(ThirdComboBoxContentProperty); }
            set { SetValue(ThirdComboBoxContentProperty, value); }
        }

        public static readonly DependencyProperty FirstComboBoxVisibilityProperty = DependencyProperty.Register("FirstComboBoxVisibility", typeof(Visibility), typeof(PortControl));
        public Visibility FirstComboBoxVisibility
        {
            get { return (Visibility)GetValue(FirstComboBoxVisibilityProperty); }
            set { SetValue(FirstComboBoxVisibilityProperty, value); }
        }

        public static readonly DependencyProperty SecondComboBoxVisibilityProperty = DependencyProperty.Register("SecondComboBoxVisibility", typeof(Visibility), typeof(PortControl));
        public Visibility SecondComboBoxVisibility
        {
            get { return (Visibility)GetValue(SecondComboBoxVisibilityProperty); }
            set { SetValue(SecondComboBoxVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ThirdComboBoxVisibilityProperty = DependencyProperty.Register("ThirdComboBoxVisibility", typeof(Visibility), typeof(PortControl));
        public Visibility ThirdComboBoxVisibility
        {
            get { return (Visibility)GetValue(ThirdComboBoxVisibilityProperty); }
            set { SetValue(ThirdComboBoxVisibilityProperty, value); }
        }

    }
}
