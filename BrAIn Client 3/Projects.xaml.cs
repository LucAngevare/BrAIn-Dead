using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System.Diagnostics;
using System.Runtime.InteropServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrAIn_Client_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Projects : Page
    {
        public Projects()
        {
            this.InitializeComponent();
        }

        public void tabButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Quick_Design":
                    fullCanvas.Navigate(typeof(Quick_Design)); break;
                case "MainPage":
                    fullCanvas.Navigate(typeof(MainPage)); break;
                case "Notes":
                    fullCanvas.Navigate(typeof(Notes)); break;
                case "ToDo":
                    fullCanvas.Navigate(typeof(ToDo)); break;
                default:
                    fullCanvas.Navigate(typeof(MainPage)); break;
            }
        }

        private void tabButton5_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://lucangevare.github.io/";
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
