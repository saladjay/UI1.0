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
using UIControlLib;
namespace UI1._0.MyPages
{
    /// <summary>
    /// GeneralPage.xaml 的互動邏輯
    /// </summary>
    public partial class GeneralPage : Page
    {
        List<string> test = new List<string>();
        public GeneralPage()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                test.Add(i.ToString());
            }
            foreach (PortControl item in InputGrid.Children)
            {
                item.FirstComboBoxContent = test;
            }
        }
    }

    public class viewmodel 
    {
        public string[] _InputComboBox;
        public string[] InputComboBox
        {
            get { return _InputComboBox; }
        }

        public viewmodel()
        {
            _InputComboBox = new string[5];
            for (int i = 0; i < 5; i++)
            {
                _InputComboBox[i] = "abc";
            }
        }
    }
}
