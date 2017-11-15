using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UILib.Cores
{
    public class CellState : NotificationObject
    {
        private bool singlebool;
        public bool SingleBool
        {
            get { return singlebool; }
            set
            {
                singlebool = value;
                this.OnPropertyChanged("SingleBool");
            }
        }

        private bool isseclect;
        public bool IsSelect
        {
            get { return isseclect; }
            set
            {
                isseclect = value;
                this.OnPropertyChanged("IsSelect");
            }
        }

        private bool _IsConnected;
        public bool IsConnected
        {
            get { return _IsConnected; }
            set
            {
                _IsConnected = value;
                this.OnPropertyChanged("IsConnected");
            }
        }
    }

    public enum _Direction
    {
        Left,
        Top
    }
}
