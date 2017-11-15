using System;
using System.Collections.Generic;
using System.IO;
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
    ///     <MyNamespace:NavigationButton/>
    ///
    /// </summary>
    public class NavigationButton : Button
    {
        static NavigationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationButton), new FrameworkPropertyMetadata(typeof(NavigationButton)));
        }

        public static readonly DependencyPropertyKey ImageContentPropertyKey = DependencyProperty.RegisterReadOnly("ImageContent", typeof(Image), typeof(NavigationButton),new PropertyMetadata(default(Image)));
        public static readonly DependencyProperty ImageContentProperty = ImageContentPropertyKey.DependencyProperty;
        public Image ImageContent
        {
            get { return (Image)GetValue(ImageContentPropertyKey.DependencyProperty); }
        }

        public FrameworkElement PointedContent { get; set; }
        public ContentControl ContentContainer { get; set; }

        protected override void OnClick()
        {
            if (ContentContainer != null && PointedContent != null)
            {
                ContentContainer.Content = PointedContent;
            }
            base.OnClick();
        }

        public NavigationButton(string imagePath)
        {
            SetValue(ImageContentPropertyKey, new Image());

               //if(System.Text.RegularExpressions.Regex.IsMatch(path,@"[]"))
            //need to finish the recognition of string by regular expression.find out whether the path is relative or absolute.
            if (File.Exists(imagePath))
            {
                using (BinaryReader loader = new BinaryReader(File.Open(imagePath, FileMode.Open)))
                {
                    FileInfo fileInfo = new FileInfo(imagePath);
                    int length = (int)fileInfo.Length;
                    byte[] buffer = new byte[length];
                    buffer = loader.ReadBytes((int)fileInfo.Length);
                    loader.Dispose();
                    loader.Close();

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(buffer);
                    bitmapImage.EndInit();
                    ImageContent.Source = bitmapImage;
                    GC.Collect();
                }
            }
            else
            {
                throw new FileNotFoundException("can not find the image");
            }
        }

        public NavigationButton()
        {
            SetValue(ImageContentPropertyKey, new Image());
        }
    }
}
