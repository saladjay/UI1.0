using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExtendedString;
namespace UILib.Cores
{
    public interface IDevice
    {
        string Name { get; }
    }

    public class DeviceInfo:NotificationObject,IDevice
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int DeviceID { get; set; }

        private bool _ConnectionState;
        public bool ConnectState
        {
            get { return _ConnectionState; }
            set
            {
                _ConnectionState = value;
                OnPropertyChanged(() => ConnectState);
            }
        }

        private bool _FaultState;
        public bool FaultState
        {
            get { return _FaultState; }
            set
            {
                _FaultState = value;
                OnPropertyChanged(() => FaultState);
            }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                OnPropertyChanged(() => Index);
            }
        }

        public Button Device { get; set; }
        public int InterfaceCount { get; private set; }
        public InterfaceInfo[] InputArray { get; private set; }
        public InterfaceInfo[] OutputArray { get; private set; }
        public DeviceInfo(string name,int interfaceCount)
        {
            Name = name;
            InterfaceCount = interfaceCount;
            InputArray = new InterfaceInfo[interfaceCount];
            OutputArray = new InterfaceInfo[interfaceCount];
            for (int i = 0; i < interfaceCount; i++)
            {
                InputArray[i] = new InterfaceInfo(string.Format("Input {0}", (i + 1)));
                OutputArray[i] = new InterfaceInfo(string.Format("Output {0}", (i + 1)));
            }
        }
    }

    public class InterfaceInfo : IDevice
    {
        public string Name { get; private set; }
        public InterfaceInfo(string name)
        {
            Name = name;
        }
    }
}
