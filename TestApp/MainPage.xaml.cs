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

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new()
            {
                Owner = Window.GetWindow(this)
            };
            testWindow.ShowDialog();
        }

        private void EditTestButton_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new()
            {
                Owner = Window.GetWindow(this)
            };
            testWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            FrameContext.MainWindowFrame.Navigate(new LoginPage());
        }
    }
}
