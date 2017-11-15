using System;
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
using UI1._0.MyPages;
using UIControlLib;

namespace UI1._0
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, Page> PageDictionary = new Dictionary<int, Page>();
        private Stack<int> PageStack = new Stack<int>();
        public static readonly DependencyProperty OpenCommandProperty =
    DependencyProperty.Register("OpenCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(null));
        public RoutedCommand OpenCommand
        {
            get { return (RoutedCommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }
        public MainWindow()
        {            
            InitializeComponent();
            //bind command
            this.OpenCommand = new RoutedCommand();
            var bin = new CommandBinding(OpenCommand);
            bin.Executed += Bin_Executed;
            this.CommandBindings.Add(bin);
            SystemPage systemPage = new SystemPage();
            GeneralPage generalPage = new GeneralPage();
            AudioPage audioPage = new AudioPage();
            ZoneSettingPage zoneSettingPage = new ZoneSettingPage();
            SurveilancePage surveilancePage = new SurveilancePage();
            EventPage eventPage = new EventPage();
            PriorityPage priorityPage = new PriorityPage();
            LogPage logPage = new LogPage();
            ErrorListPage errorListPage = new ErrorListPage();
            List<int> PageIDList = new List<int>();
            PageIDList.Add(systemPage.GetHashCode());
            PageIDList.Add(generalPage.GetHashCode());
            PageIDList.Add(audioPage.GetHashCode());
            PageIDList.Add(zoneSettingPage.GetHashCode());
            PageIDList.Add(surveilancePage.GetHashCode());
            PageIDList.Add(eventPage.GetHashCode());
            PageIDList.Add(priorityPage.GetHashCode());
            PageIDList.Add(logPage.GetHashCode());
            PageIDList.Add(errorListPage.GetHashCode());
            PageDictionary.Add(systemPage.GetHashCode(), systemPage);
            PageDictionary.Add(generalPage.GetHashCode(), generalPage);
            PageDictionary.Add(audioPage.GetHashCode(), audioPage);
            PageDictionary.Add(zoneSettingPage.GetHashCode(), zoneSettingPage);
            PageDictionary.Add(surveilancePage.GetHashCode(), surveilancePage);
            PageDictionary.Add(eventPage.GetHashCode(), eventPage);
            PageDictionary.Add(priorityPage.GetHashCode(), priorityPage);
            PageDictionary.Add(logPage.GetHashCode(), logPage);
            PageDictionary.Add(errorListPage.GetHashCode(), errorListPage);
            foreach (Control btn in NavigationBar.Children)
            {
                if (!(btn is RadioButton))
                    continue;
                int index = NavigationBar.Children.IndexOf(btn);
                if(index<PageIDList.Count)
                {
                    btn.Tag = PageIDList[index];
                }
            }
            PageContext.Content = systemPage;
            PageContext.Navigating += PageContext_Navigating;
            PageContext.Navigated += PageContext_Navigated;
        }

        private void PageContext_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is SystemPage)
            {
                RadioButton btn = NavigationBar.Children[0] as RadioButton;
                btn.IsChecked = true;
            }
            else if (e.Content is GeneralPage)
            {
                RadioButton btn = NavigationBar.Children[1] as RadioButton;
                btn.IsChecked = true;
            }
        }

        private void PageContext_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Content is SystemPage)
                Debug.WriteLine("systemPage");
            else if (e.Content is GeneralPage)
                Debug.WriteLine("generalPage");
        }

        private void Bin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.Source as RadioButton;
            if (btn.Tag == null ||(int)btn.Tag == 0)
            {
                this.PageContext.Source = default(Uri);
            }
            else
                this.PageContext.Content = PageDictionary[(int)btn.Tag];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PageContext.NavigationService.CanGoBack)
                PageContext.NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PageContext.NavigationService.CanGoForward)
                PageContext.NavigationService.GoForward();
        }
    }
}
