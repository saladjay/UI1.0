﻿using System;
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
    ///     <MyNamespace:DeviceControlCell/>
    ///
    /// </summary>
    public class DeviceControlButtonCell : ToggleButton
    {
        static DeviceControlButtonCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceControlButtonCell), new FrameworkPropertyMetadata(typeof(DeviceControlButtonCell)));
        }

        public IDevice ObjectTag1 { get; set; }
        public IDevice ObjectTag2 { get; set; }
        #region Expand and collapse
        public static readonly DependencyProperty ChangedIconProperty = DependencyProperty.Register("ChangedIcon", typeof(bool), typeof(DeviceControlButtonCell), new PropertyMetadata(false));

        public bool ChangedIcon
        {
            get { return (bool)GetValue(ChangedIconProperty); }
            set { SetValue(ChangedIconProperty, value); }
        }

        public delegate void ExpandCellHandler(bool Expand, DeviceControlButtonCell Source);
        public event ExpandCellHandler ExpandCell;
        private bool IsOpen = false;
        protected override void OnClick()
        {
            IsOpen = !IsOpen;
            ExpandCell?.Invoke(IsOpen, this);
            base.OnClick();
        }
        #endregion

        #region MouseOver
        public delegate void IsMouseOverHandler(bool IsMouseSelect, DeviceControlButtonCell Source);
        public event IsMouseOverHandler IsMouseSelect;
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            IsMouseSelect?.Invoke(true, this);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsMouseSelect?.Invoke(false, this);
            base.OnMouseLeave(e);
        }
        #endregion
    }
}
