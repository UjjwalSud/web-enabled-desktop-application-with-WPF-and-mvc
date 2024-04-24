using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static WebView2 SystemWebView = new WebView2();

        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.Sleep(5000);
            SystemWebView.WebMessageReceived += SystemWebView_WebMessageReceived;
            SystemWebView.Source = new Uri("https://localhost:7144/");
            Grid.SetRow(SystemWebView, 0);

            browserWrapper.Children.Add(SystemWebView);
        }

        private void SystemWebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string commandString = e.TryGetWebMessageAsString();

            UICommander commander = JsonConvert.DeserializeObject<UICommander>(commandString);

            MessageBox.Show(string.Format("command is '{0}' and data is '{1}'", commander.command, commander.meta), "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

public class UICommander
{
    public string command { get; set; }
    public string meta { get; set; }
}