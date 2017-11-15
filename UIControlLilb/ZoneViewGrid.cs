using System;
using System.Collections;
using System.Collections.Generic;
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
using UILib;
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
    ///     <MyNamespace:ZoneViewGrid/>
    ///
    /// </summary>
    public class ZoneViewGrid : Grid
    {
        static ZoneViewGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoneViewGrid), new FrameworkPropertyMetadata(typeof(ZoneViewGrid)));
        }

        private ExtendedItemsSourceComponent _ItemsSource = new ExtendedItemsSourceComponent();
        public IEnumerable ItemsSource
        {
            get { return _ItemsSource.ItemsSource; }
            set
            {
                _ItemsSource.ItemsSource = value;
                _ItemsSource.ItemsSourceCollectionChanged += _ItemsSource_ItemsSourceCollectionChanged;
            }
        }

        public ZoneViewGrid()
        {
            _ItemsSource.ItemsSourceChanged += _ItemsSource_ItemsSourceChanged;
            RowDefinition R1 = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
            ColumnDefinition C1 = new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) };
            this.RowDefinitions.Add(R1);
            this.ColumnDefinitions.Add(C1);
        }

        private void _ItemsSource_ItemsSourceChanged(IEnumerable OldSource, IEnumerable NewSource)
        {
            while(this.RowDefinitions.Count>1)
            {
                RowDefinitions.RemoveAt(1);
            }
            while(this.ColumnDefinitions.Count>1)
            {
                ColumnDefinitions.RemoveAt(1);
            }
            this.Children.Clear();
        }

        private void _ItemsSource_ItemsSourceCollectionChanged(bool AddOrRemove, object item)
        {
            if (AddOrRemove)
            {
                if (item is InputInfo)
                {
                    RowDefinition newRow = new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) };
                    this.RowDefinitions.Add(newRow);
                    StackPanel header = HeaderFactory(((IPort)item).PortName, ControlDirection.Left);
                    Children.Add(header);
                    int rowIndex = this.RowDefinitions.Count - 1;
                    Grid.SetColumn(header, 0);
                    Grid.SetRow(header, rowIndex);
                    for (int i = 1; i < this.ColumnDefinitions.Count; i++)
                    {
                        Button button = CellFactory();
                        this.Children.Add(button);
                        Grid.SetColumn(button, i);
                        Grid.SetRow(button, rowIndex);
                    }
                }
                else
                {
                    ColumnDefinition newColumn = new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) };
                    this.ColumnDefinitions.Add(newColumn);
                    StackPanel header = HeaderFactory(((IPort)item).PortName, ControlDirection.Top);
                    Children.Add(header);
                    int columnIndex = this.ColumnDefinitions.Count - 1;
                    Grid.SetColumn(header, columnIndex);
                    Grid.SetRow(header, 0);
                    for (int i = 1; i < this.RowDefinitions.Count; i++)
                    {
                        Button button = CellFactory();
                        this.Children.Add(button);
                        Grid.SetColumn(button, columnIndex);
                        Grid.SetRow(button, i);
                    }
                }
            }
            else
            {

            }
        }

        private StackPanel HeaderFactory(string name, ControlDirection direction)
        {
            StackPanel Header = new StackPanel();
            if (direction == ControlDirection.Left)
            {
                Header.Orientation = Orientation.Horizontal;
                RowButton Btn = new RowButton() { Content = name, Width = 60 };
                Line L = new Line() { X1 = 0, X2 = 30, Y1 = 2, Y2 = 2, Height = 5, Width = 30, StrokeThickness = 1, VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, Stroke = new SolidColorBrush(Colors.White) };
                Header.Children.Add(Btn);
                Header.Children.Add(L);
            }
            else if (direction == ControlDirection.Top)
            {
                Header.Orientation = Orientation.Vertical;
                ColumnButton Btn = new ColumnButton() { Content = name, Height = 60 };
                Line L = new Line() { X1 = 2, X2 = 2, Y1 = 0, Y2 = 30, Height = 30, Width = 5, StrokeThickness = 1, VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, Stroke = new SolidColorBrush(Colors.White) };
                Header.Children.Add(Btn);
                Header.Children.Add(L);
            }
            return Header;
        }

        private Button CellFactory(string ToolTipStr = null)
        {
            RowButton button = new RowButton() { ToolTip = ToolTipStr, Width = 40, Height = 40 };
            button.Click += Button_Click;
            return button;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            AttachedPropertyButton.SetActivited(Btn, !AttachedPropertyButton.GetActivited(Btn));
        }

    }

    public enum ControlDirection
    {
        emtpy,
        Left,
        Top
    }
}
