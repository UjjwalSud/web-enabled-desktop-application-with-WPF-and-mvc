using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace WpfAppDotNetFrameWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.Sleep(5000);
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Loaded += (sender, e) => SuppressScriptErrors(webBrowser, true);
            webBrowser.ObjectForScripting = new ScriptingHelper();
            webBrowser.Navigate(new Uri("https://localhost:7144/"));
            this.Content = webBrowser;
        }

        public void SuppressScriptErrors(WebBrowser webBrowser, bool suppress)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                {
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { suppress });
                }
            }
        }
    }
}

[ComVisible(true)]
public class ScriptingHelper
{
    public void Notify(string message)
    {
        MessageBox.Show("Message from web page: " + message);
    }
}

