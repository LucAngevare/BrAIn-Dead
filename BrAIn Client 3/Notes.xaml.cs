using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrAIn_Client_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Notes : Page
    {
        public Notes()
        {
            this.InitializeComponent();
        }
        public void tabButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Projects":
                    fullCanvas.Navigate(typeof(Projects)); break;
                case "Quick_Design":
                    fullCanvas.Navigate(typeof(Quick_Design)); break;
                case "Notes":
                    fullCanvas.Navigate(typeof(Notes)); break;
                case "ToDo":
                    fullCanvas.Navigate(typeof(ToDo)); break;
                default:
                    fullCanvas.Navigate(typeof(MainPage)); break;
            }
        }
    }
}
