using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UILib.Cores
{
    public interface IPort
    {
        string PortName { get; set; }
    }

    public class InputInfo : IPort
    {
        public string _PortName;
        public string PortName
        {
            get
            {
                return _PortName;
            }

            set
            {
                _PortName = value;
            }
        }

        public InputInfo(string portName)
        {
            _PortName = portName;
        }

        public InputInfo() : this("Input")
        {

        }
    }

    public class OutputInfo : IPort
    {
        public string _PortName;
        public string PortName
        {
            get
            {
                return _PortName;
            }

            set
            {
                _PortName = value;
            }
        }

        public OutputInfo(string portName)
        {
            _PortName = portName;
        }

        public OutputInfo() : this("Output")
        {

        }
    }
}
