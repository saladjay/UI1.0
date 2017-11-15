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
using ExtendedString;
using UILib;
using UILib.Cores;
using System.Collections.ObjectModel;

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
    ///     [加入參考]->[專案]->[選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class DeviceViewGrid : Control
    {
        static DeviceViewGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceViewGrid), new FrameworkPropertyMetadata(typeof(DeviceViewGrid)));
        }

        //public string HeaderName { get; set; }

        public ExtendedItemsSourceComponent ItemsSource { get; set; }
        private ObservableCollection<DeviceInfo> _ItemsSource2;
        public ObservableCollection<DeviceInfo> ItemsSource2
        {
            get { return _ItemsSource2; }
            set
            {
                _ItemsSource2 = value;
                _ItemsSource2.CollectionChanged += _ItemsSource2_CollectionChanged;
            }
        }
        //public static readonly DependencyProperty DeviceColorProperty = DependencyProperty.Register("DeviceColor", typeof(SolidColorBrush), typeof(DeviceViewGrid));
        //public SolidColorBrush DeviceColor
        //{
        //    get { return (SolidColorBrush)GetValue(DeviceColorProperty); }
        //    set { SetValue(DeviceColorProperty, value); }
        //}
        //public static readonly DependencyProperty InterfaceColorProperty = DependencyProperty.Register("InterfaceColor", typeof(SolidColorBrush), typeof(DeviceViewGrid));
        //public SolidColorBrush InterfaceColor
        //{
        //    get { return (SolidColorBrush)GetValue(InterfaceColorProperty); }
        //    set { SetValue(InterfaceColorProperty, value); }
        //}

        public static readonly DependencyPropertyKey TopTreeViewPropertyKey = DependencyProperty.RegisterReadOnly("TopTreeView", typeof(Grid), typeof(DeviceViewGrid), new PropertyMetadata(default(Grid)));
        public static readonly DependencyProperty TopTreeViewProperty = TopTreeViewPropertyKey.DependencyProperty;
        public Grid TopTreeView { get { return (Grid)GetValue(TopTreeViewPropertyKey.DependencyProperty); } }

        public static readonly DependencyPropertyKey LeftTreeViewPropertyKey = DependencyProperty.RegisterReadOnly("LeftTreeView", typeof(Grid), typeof(DeviceViewGrid), new PropertyMetadata(default(Grid)));
        public static readonly DependencyProperty LeftTreeViewProperty = LeftTreeViewPropertyKey.DependencyProperty;
        public Grid LeftTreeView { get { return (Grid)GetValue(LeftTreeViewPropertyKey.DependencyProperty); } }

        public static readonly DependencyPropertyKey InterfaceViewPropertyKey = DependencyProperty.RegisterReadOnly("InterfaceView", typeof(Grid), typeof(DeviceViewGrid), new PropertyMetadata(default(Grid)));
        public static readonly DependencyProperty InterfaceViewProperty = InterfaceViewPropertyKey.DependencyProperty;
        public Grid InterfaceView { get { return (Grid)GetValue(InterfaceViewPropertyKey.DependencyProperty); } }

        private int _PositionIndex = 0;
        private List<int> _PositionHelper = new List<int>();
        private List<IDevice> _ConstructionInputHelper = new List<IDevice>();
        private List<IDevice> _ConstructionOutputHelper = new List<IDevice>();
        private List<CellState> _ColumnCellState = new List<CellState>();
        private List<CellState> _RowCellState = new List<CellState>();
        private SquareList<CellState> _ConnectionState = new SquareList<CellState>();
        private List<DeviceInfo> _InnerDeviceList = new List<DeviceInfo>();

        private List<Control> _RowControl = new List<Control>();
        private List<Control> _TopControl = new List<Control>();
        private List<List<Control>> _CellsControl = new List<List<Control>>();

        private Dictionary<int, int> _RowDictionary = new Dictionary<int, int>();
        private Dictionary<int, int> _ColumnDicitionary = new Dictionary<int, int>();


        private int _OldSelectedRow = -1;
        private int _OldSelectedColumn = -1;
        public DeviceViewGrid()
        {
            this.SetValue(LeftTreeViewPropertyKey, new Grid());
            this.SetValue(TopTreeViewPropertyKey, new Grid());
            this.SetValue(InterfaceViewPropertyKey, new Grid());
        }

        private void _ItemsSource2_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DeviceInfo device in e.NewItems)
                {
                    _InnerDeviceList.Add(device);
                    _ConstructionInputHelper.Add(device);
                    _ConstructionOutputHelper.Add(device);
                    for (int i = 0; i < device.InterfaceCount; i++)
                    {
                        _ConstructionInputHelper.Add(device.InputArray[i]);
                        _ConstructionOutputHelper.Add(device.OutputArray[i]);
                    }
                    for (int i = 0; i < device.InterfaceCount+1; i++)
                    {
                        _PositionHelper.Add(_PositionIndex++);
                        _RowCellState.Add(new CellState());
                        _ColumnCellState.Add(new CellState());
                    }
                    _ConnectionState.AddMany(device.InterfaceCount + 1);
                    AddNewDeviceToInterfaceView(device);
                    AddNewDeviceToTreeView(device);
                }
            }
            if (e.OldItems != null)
            {
                foreach (DeviceInfo device in e.OldItems)
                {
                    int index = _InnerDeviceList.IndexOf(device);
                    int removeIndex = _ConstructionInputHelper.IndexOf(device);
                    int count = device.InterfaceCount + 1;
                    for (int i = 0; i < count; i++)
                    {
                        _ConstructionInputHelper.RemoveAt(removeIndex);
                        _ConstructionOutputHelper.RemoveAt(removeIndex);
                        _ColumnCellState.RemoveAt(removeIndex);
                        _RowCellState.RemoveAt(removeIndex);
                        _PositionHelper.RemoveAt(removeIndex);
                    }
                    _ConnectionState.RemoveAtRange(removeIndex, count);
                    _InnerDeviceList.RemoveAt(index);
                    RemoveControl(removeIndex, count);
                }
            }
        }

        private void RemoveControl(int removeIndex,int count)
        {
            for (int i = 0; i < count; i++)
            {
                LeftTreeView.Children.RemoveAt(removeIndex);
                TopTreeView.Children.RemoveAt(removeIndex);
            }
            for (int i = 0; i < count; i++)
            {
                foreach (Control Cell in _CellsControl[removeIndex])
                {
                    InterfaceView.Children.Remove(Cell);
                }
                _CellsControl.RemoveAt(removeIndex);
            }
            foreach (List<Control> rowlist in _CellsControl)
            {
                for (int i = 0; i < count; i++)
                {
                    InterfaceView.Children.Remove(rowlist[removeIndex]);
                    rowlist.RemoveAt(removeIndex);
                }
            }
        }

        private void AddNewControlInGrid(Control control, int row, int column, Grid grid)
        {
            grid.Children.Add(control);
            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
        }

        private void AddNewDeviceToTreeView(DeviceInfo device)
        {
            for (int i = 0; i < device.InterfaceCount + 1; i++)
            {
                LeftTreeView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20, GridUnitType.Auto) });
                TopTreeView.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Auto) });
            }
            int index = _ConstructionInputHelper.IndexOf(device);
            for (int i = 0; i < device.InterfaceCount + 1; i++)
            {
                DeviceControlItem leftItem = DeviceItemFactoryCreateItem(_ConstructionInputHelper[index + i], index + i,_Direction.Left) as DeviceControlItem;
                AddNewControlInGrid(leftItem, _PositionHelper[index + i], 0, LeftTreeView);
                DeviceControlItem topItem = DeviceItemFactoryCreateItem(_ConstructionOutputHelper[index + i], index + i, _Direction.Top) as DeviceControlItem;
                AddNewControlInGrid(topItem, 0, _PositionHelper[index + i], TopTreeView);
            }
        }

        private Control DeviceItemFactoryCreateItem(IDevice a, int index, _Direction dockDirection)
        {
            Control Good = null;
            if (a is DeviceInfo)
            {
                DeviceInfo A = a as DeviceInfo;
                Good = new DeviceControlItem() { HeaderName = A.Name, Direction = dockDirection, Tag = A };
                if (dockDirection == _Direction.Left)
                {
                    Good.SetBinding(DeviceControlItem.OpenProperty, new Binding("SingleBool") { Source = _RowCellState[index] });
                    Good.SetBinding(DeviceControlItem.MouseSelectedProperty, new Binding("IsSelect") { Source = _RowCellState[index] });
                    //Good.SetBinding(DeviceControlItem.IsConnectedProperty, new Binding("IsConnected") { Source = _RowCellState[index] });
                    MultiBinding MBinding = new MultiBinding() { Mode = BindingMode.OneWay };
                    for (int i = 0; i < A.InterfaceCount; i++)
                    {
                        Binding binding = new Binding("IsConnected") { Source = _RowCellState[i + index + 1] };
                        MBinding.Bindings.Add(binding);
                    }
                    MBinding.Converter = Converter.BooleanOrConverter;
                    Good.SetBinding(DeviceControlItem.IsConnectedProperty, MBinding);
                }
                else
                {
                    Good.SetBinding(DeviceControlItem.OpenProperty, new Binding("SingleBool") { Source = _ColumnCellState[index] });
                    Good.SetBinding(DeviceControlItem.MouseSelectedProperty, new Binding("IsSelect") { Source = _ColumnCellState[index] });
                    //Good.SetBinding(DeviceControlItem.IsConnectedProperty, new Binding("IsConnected") { Source = _ColumnCellState[index] });

                    MultiBinding MBinding = new MultiBinding() { Mode = BindingMode.OneWay };
                    for (int i = 0; i < A.InterfaceCount; i++)
                    {
                        Binding binding = new Binding("IsConnected") { Source = _ColumnCellState[i + index + 1] };
                        MBinding.Bindings.Add(binding);
                    }
                    MBinding.Converter = Converter.BooleanOrConverter;
                    Good.SetBinding(DeviceControlItem.IsConnectedProperty, MBinding);
                }
                ((DeviceControlItem)Good).ExpandItems += ItemExpandHandler;
                ((DeviceControlItem)Good).SelectItem += ItemSelectHandler;
            }
            else if (a is InterfaceInfo)
            {
                InterfaceInfo A = a as InterfaceInfo;
                if (dockDirection == _Direction.Left)
                {
                    Good = new DeviceControlItem() { HeaderName = A.Name, Direction = _Direction.Left, FirstLevel = true };
                    Good.SetBinding(DeviceControlItem.VisibilityProperty, new Binding("SingleBool") { Source = _RowCellState[index], Converter = Converter.CellVisibilityConverterForSingle });
                    Good.SetBinding(DeviceControlItem.MouseSelectedProperty, new Binding("IsSelect") { Source = _RowCellState[index] });
                    Good.SetBinding(DeviceControlItem.IsConnectedProperty, new Binding("IsConnected") { Source = _RowCellState[index] });
                }
                else
                {
                    Good = new DeviceControlItem() { HeaderName = A.Name, Direction = _Direction.Top, FirstLevel = true };
                    Good.SetBinding(DeviceControlItem.VisibilityProperty, new Binding("SingleBool") { Source = _ColumnCellState[index], Converter = Converter.CellVisibilityConverterForSingle });
                    Good.SetBinding(DeviceControlItem.MouseSelectedProperty, new Binding("IsSelect") { Source = _ColumnCellState[index] });
                    Good.SetBinding(DeviceControlItem.IsConnectedProperty, new Binding("IsConnected") { Source = _ColumnCellState[index] });
                }
                ((DeviceControlItem)Good).SelectItem += ItemSelectHandler;
            }
            return Good;
        }

        private void ItemExpandHandler(bool open, DeviceControlItem source)
        {
            DeviceInfo tempDevice = source.Tag as DeviceInfo;
            int index = _ConstructionInputHelper.IndexOf(tempDevice);
            if (source.Direction == _Direction.Left)
            {
                for (int i = 0; i < tempDevice.InterfaceCount + 1; i++)
                {
                    _RowCellState[index + i].SingleBool = open;
                }
            }
            else
            {
                for (int i = 0; i < tempDevice.InterfaceCount + 1; i++)
                {
                    _ColumnCellState[index + i].SingleBool = open;
                }
            }
        }

        private void ItemSelectHandler(bool Selected, DeviceControlItem source)
        {
            DeviceInfo tempDevice = source.Tag as DeviceInfo;
            if (source.Direction == _Direction.Left)
            {
                int rowIndex = LeftTreeView.Children.IndexOf(source);
                if (rowIndex >= 0 && rowIndex <= _RowCellState.Count)
                {
                    SelectedRowChanged(rowIndex);
                }
            }
            else
            {
                int columnIndex = TopTreeView.Children.IndexOf(source);
                if (columnIndex >= 0 && columnIndex <= _ColumnCellState.Count)
                {
                    SelectedColumnChanged(columnIndex);
                }
            }
        }

        private void SelectedRowChanged(int index)
        {
            if (_OldSelectedRow != -1)
                _RowCellState[_OldSelectedRow].IsSelect = false;
            _RowCellState[index].IsSelect = true;
            _OldSelectedRow = index;
        }

        private void SelectedColumnChanged(int index)
        {
            if (_OldSelectedColumn != -1)
                _ColumnCellState[_OldSelectedColumn].IsSelect = false;
            _ColumnCellState[index].IsSelect = true;
            _OldSelectedColumn = index;
        }

        private void AddNewDeviceToInterfaceView(DeviceInfo device)
        {
            for (int i = 0; i < device.InterfaceCount + 1; i++)
            {
                InterfaceView.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Auto) });
                InterfaceView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20, GridUnitType.Auto) });
            }
            foreach (List<Control> rowList in _CellsControl)
            {
                int rowIndex = _CellsControl.IndexOf(rowList);
                IDevice leftTempDevice = _ConstructionInputHelper[rowIndex] as IDevice;
                int columnIndex = _ConstructionOutputHelper.IndexOf(device);
                for (int i = 0; i < device.InterfaceCount + 1; i++)
                {
                    IDevice topTempDevice = _ConstructionOutputHelper[columnIndex] as IDevice;
                    Control cellControl = CellFactoryCreateCell(leftTempDevice, topTempDevice, rowIndex, columnIndex);
                    rowList.Add(cellControl);
                    AddNewControlInGrid(cellControl, _PositionHelper[rowIndex], InterfaceView.ColumnDefinitions.Count + i - device.InterfaceCount - 1, InterfaceView);
                    columnIndex++;
                    //if (i == 0)
                    //{
                    //    int columnIndex = _ConstructionOutputHelper.IndexOf(device);
                    //    Control cellControl = CellFactoryCreateCell(leftTempDevice, device, rowIndex, columnIndex);
                    //    rowList.Add(cellControl);
                    //    AddNewControlInGrid(cellControl, _PositionHelper[rowIndex], InterfaceView.ColumnDefinitions.Count + i - device.InterfaceCount - 1, InterfaceView);
                    //}
                    //else
                    //{
                    //    int ColumnIndex = _ConstructionOutputHelper.IndexOf(device.OutputArray[i - 1]);
                    //    Control CellControl = CellFactoryCreateCell(leftTempDevice, device.OutputArray[i - 1], rowIndex, ColumnIndex);
                    //    rowList.Add(CellControl);
                    //    AddNewControlInGrid(CellControl, _PositionHelper[rowIndex], InterfaceView.ColumnDefinitions.Count + i - device.InterfaceCount - 1, InterfaceView);
                    //}
                }
            }
            for (int i = 0; i < device.InterfaceCount + 1; i++)
            {
                int rowIndex = _ConstructionInputHelper.IndexOf(device);
                List<Control> tempRow = new List<Control>();
                for (int columnIndex = 0; columnIndex < _ConstructionOutputHelper.Count; columnIndex++)
                {
                    Control cellControl = CellFactoryCreateCell(_ConstructionInputHelper[rowIndex + i], _ConstructionOutputHelper[columnIndex], _CellsControl.Count, columnIndex);
                    tempRow.Add(cellControl);
                    AddNewControlInGrid(cellControl, InterfaceView.RowDefinitions.Count - device.InterfaceCount - 1 + i, _PositionHelper[columnIndex], InterfaceView);
                }
                //if (i == 0)
                //{
                //    for (int columnIndex = 0; columnIndex < _ConstructionOutputHelper.Count; columnIndex++)
                //    {
                //        Control cellControl = CellFactoryCreateCell(device, _ConstructionOutputHelper[columnIndex], _CellsControl.Count, columnIndex);
                //        tempRow.Add(cellControl);
                //        AddNewControlInGrid(cellControl, InterfaceView.RowDefinitions.Count - device.InterfaceCount - 1 + i, _PositionHelper[columnIndex], InterfaceView);
                //    }
                //}
                //else
                //{
                //    for (int columnIndex = 0; columnIndex < _ConstructionOutputHelper.Count; columnIndex++)
                //    {
                //        Control cellControl = CellFactoryCreateCell(device.InputArray[i - 1], _ConstructionOutputHelper[columnIndex], _CellsControl.Count, columnIndex);
                //        tempRow.Add(cellControl);
                //        AddNewControlInGrid(cellControl, InterfaceView.RowDefinitions.Count - device.InterfaceCount - 1 + i, _PositionHelper[columnIndex], InterfaceView);
                //    }
                //}
                _CellsControl.Add(tempRow);
            }
        }

        public Control CellFactoryCreateCell(object a, object b, int Row, int Column)
        {
            Control Good = null;
            if (a is DeviceInfo && b is DeviceInfo)
            {
                DeviceInfo A = a as DeviceInfo;
                DeviceInfo B = b as DeviceInfo;
                Good = new DeviceControlButtonCell() { ObjectTag1 = A, ObjectTag2 = B };
                Good.ToolTip = string.Format(A.Name + "&&" + B.Name);
                Binding B1 = new Binding("SingleBool") { Source = _RowCellState[Row] };
                Binding B2 = new Binding("SingleBool") { Source = _ColumnCellState[Column] };
                MultiBinding MBinding = new MultiBinding() { Mode = BindingMode.OneWay };
                MBinding.Bindings.Add(B1);
                MBinding.Bindings.Add(B2);
                MBinding.Converter = Converter.CellStateConverter;
                Good.SetBinding(DeviceControlButtonCell.ChangedIconProperty, MBinding);

            }
            else if (a is DeviceInfo && b is InterfaceInfo)
            {
                DeviceInfo A = a as DeviceInfo;
                InterfaceInfo B = b as InterfaceInfo;
                Good = new DeviceControlDisplayCell() { IsCommon = false, ObjectTag1 = A, ObjectTag2 = B };
                Good.ToolTip = string.Format(A.Name);
                Good.SetBinding(DeviceControlDisplayCell.VisibilityProperty, new Binding("SingleBool") { Source = _ColumnCellState[Column], Converter = Converter.CellVisibilityConverterForSingle });
            }
            else if (a is InterfaceInfo && b is DeviceInfo)
            {
                InterfaceInfo A = a as InterfaceInfo;
                DeviceInfo B = b as DeviceInfo;
                Good = new DeviceControlDisplayCell() { IsCommon = false, ObjectTag1 = A, ObjectTag2 = B };
                Good.ToolTip = string.Format(B.Name);
                Good.SetBinding(DeviceControlDisplayCell.VisibilityProperty, new Binding("SingleBool") { Source = _RowCellState[Row], Converter = Converter.CellVisibilityConverterForSingle });
            }
            else if (a is InterfaceInfo && b is InterfaceInfo)
            {
                InterfaceInfo A = a as InterfaceInfo;
                InterfaceInfo B = b as InterfaceInfo;
                Good = new DeviceControlDisplayCell() { IsCommon = true, ObjectTag1 = A, ObjectTag2 = B };
                Good.ToolTip = string.Format(A.Name + "=>" + B.Name);
                Binding B1 = new Binding("SingleBool") { Source = _RowCellState[Row] };
                Binding B2 = new Binding("SingleBool") { Source = _ColumnCellState[Column] };
                MultiBinding MBinding1 = new MultiBinding() { Mode = BindingMode.OneWay };
                MBinding1.Bindings.Add(B1);
                MBinding1.Bindings.Add(B2);
                MBinding1.Converter = Converter.CellVisibilityConverter;
                Good.SetBinding(DeviceControlDisplayCell.VisibilityProperty, MBinding1);
                Good.SetBinding(DeviceControlDisplayCell.IsConnectedProperty, new Binding("IsConnected") { Source = _ConnectionState[Row, Column] });
                ((DeviceControlDisplayCell)Good).IsConnectedEvent += CellConncet;
            }
            if (Good == null)
                return Good;
            Good.Width = Good.Height = 20;
            if (Good is DeviceControlButtonCell)
            {
                ((DeviceControlButtonCell)Good).ExpandCell += CellBtnExpand;
                ((DeviceControlButtonCell)Good).IsMouseSelect += CellSelcect;
            }
            else if (Good is DeviceControlDisplayCell)
            {
                Binding SelectBD1 = new Binding("IsSelect") { Source = _RowCellState[Row] };
                Binding SelectBD2 = new Binding("IsSelect") { Source = _ColumnCellState[Column] };
                MultiBinding SelectMB = new MultiBinding() { Mode = BindingMode.OneWay };
                SelectMB.Bindings.Add(SelectBD1);
                SelectMB.Bindings.Add(SelectBD2);
                SelectMB.Converter = Converter.SelectConverter;
                Good.SetBinding(DeviceControlDisplayCell.IsSelectedProperty, SelectMB);

                ((DeviceControlDisplayCell)Good).IsMouseSelectEvent += CellSelcect;
            }
            return (Control)Good;

        }

        private void CellBtnExpand(bool IsOpen, DeviceControlButtonCell Source)
        {
            int rowIndex = _ConstructionInputHelper.IndexOf(Source.ObjectTag1);
            int columnIndex = _ConstructionOutputHelper.IndexOf(Source.ObjectTag2);
            for (int i = 0; i < ((DeviceInfo)Source.ObjectTag1).InterfaceCount + 1; i++)
            {
                _RowCellState[rowIndex + i].SingleBool = IsOpen;
            }
            for (int i = 0; i < ((DeviceInfo)Source.ObjectTag2).InterfaceCount + 1; i++)
            {
                _ColumnCellState[columnIndex + i].SingleBool = IsOpen;
            }
        }

        private void CellSelcect(bool IsSelect, Control Source)
        {
            int rowIndex = 0;
            int columnIndex = 0;
            if (Source is DeviceControlButtonCell)
            {
                rowIndex = _ConstructionInputHelper.IndexOf(((DeviceControlButtonCell)Source).ObjectTag1);
                columnIndex = _ConstructionOutputHelper.IndexOf(((DeviceControlButtonCell)Source).ObjectTag2);
            }
            else
            {
                rowIndex = _ConstructionInputHelper.IndexOf(((DeviceControlDisplayCell)Source).ObjectTag1);
                columnIndex = _ConstructionOutputHelper.IndexOf(((DeviceControlDisplayCell)Source).ObjectTag2);
            }
            SelectedRowChanged(rowIndex);
            SelectedColumnChanged(columnIndex);
        }

        private void CellConncet(bool IsConnect, Control Source)
        {
            int rowIndex = 0;
            int columnIndex = 0;
            rowIndex = _ConstructionInputHelper.IndexOf(((DeviceControlDisplayCell)Source).ObjectTag1);
            columnIndex = _ConstructionOutputHelper.IndexOf(((DeviceControlDisplayCell)Source).ObjectTag2);

            int TempColumn, TempRow;
            bool A = _RowDictionary.TryGetValue(rowIndex, out TempColumn);
            bool B = _ColumnDicitionary.TryGetValue(columnIndex, out TempRow);
            if (A)
            {
                if (TempColumn == columnIndex)
                {
                    _RowDictionary.Remove(rowIndex);
                    _ConnectionState[rowIndex, columnIndex].IsConnected = false;
                    _RowCellState[rowIndex].IsConnected = false;
                    //_ColumnCellState[columnIndex].IsConnected = false;
                }
                else
                {
                    _RowDictionary[rowIndex] = columnIndex;
                    _ConnectionState[rowIndex, TempColumn].IsConnected = false;
                    //_ColumnCellState[TempColumn].IsConnected = false;
                    _ConnectionState[rowIndex, columnIndex].IsConnected = true;
                    _ColumnCellState[columnIndex].IsConnected = true;
                }
                bool result = _ConnectionState[0, TempColumn].IsConnected;
                for (int i = 1; i < _ConnectionState.Rank; i++)
                {
                    result |= _ConnectionState[i, TempColumn].IsConnected;
                }
                _ColumnCellState[TempColumn].IsConnected = result;
            }
            else
            {
                _RowDictionary.Add(rowIndex, columnIndex);
                _ConnectionState[rowIndex, columnIndex].IsConnected = true;
                _RowCellState[rowIndex].IsConnected = true;
                _ColumnCellState[columnIndex].IsConnected = true;
            }
        }
    }
}

