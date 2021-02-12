using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using System.IO.Compression;
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
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BrAIn_Client_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //why the fuck is there not a startup event like Micro soft penis states here wtf: https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.startup?view=net-5.0
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            const string key = "API IP + port";
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            float factor = ((float)bounds.Width <= bounds.Height) ? (float)(bounds.Width) / 1920 : (float)(bounds.Height) / 1080;
            autoscale.ScaleX = factor;
            autoscale.ScaleY = factor;
            async void getAPI()
            {
                if (localSettings.Values.GetType().GetMethod(key) == null)
                {
                    string text = await InputTextDialogAsync("Enter your BrAIn API IP address/port here (press cancel if you don't want to host this yourself):");
                    if (!(text == ""))
                    {
                        int count = text.Split('.').Length;
                        if (count != 4 && text.Contains(":"))
                        {
                            ContentDialog dialog = new ContentDialog();
                            dialog.Title = "Invalid IP address entered. Initializing restart.";
                            dialog.PrimaryButtonText = "Okkie!";
                            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                            {
                                getAPI();
                            }
                        }
                        else if (!(text.Contains(":")) && count == 4)
                        {
                            ContentDialog dialog = new ContentDialog();
                            dialog.Title = "No explicit port entered, defaulting to :3537";
                            dialog.PrimaryButtonText = "Okkie!";
                            dialog.SecondaryButtonText = "Re-enter";
                            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                            {
                                text += ":3537";
                                localSettings.Values[key] = text;
                            }
                            else
                                getAPI();
                        }
                        else if (text.Contains(":") && count == 4)
                        {
                            localSettings.Values[key] = text;
                        }
                        else
                        {
                            ContentDialog error = new ContentDialog();
                            error.Title = "You didn't enter the IPv4 *or* the port, please try again";
                            error.PrimaryButtonText = "Ok";
                            if (await error.ShowAsync() == ContentDialogResult.Primary)
                                getAPI();
                        }
                    }
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Title = "No explicit IP/port entered, defaulting to self-host.";
                        dialog.PrimaryButtonText = "Okkie!";
                        dialog.SecondaryButtonText = "Try again";
                        localSettings.Values[key] = "nope can do";
                        if (await dialog.ShowAsync() == ContentDialogResult.Secondary)
                            getAPI();
                        /*else
                        {
                            try
                            {
                                string selfhostedAPI = GetLocalIPAddress();
                                text = selfhostedAPI + ":3537; selfhost";
                                localSettings.Values[key] = text;
                                downloadAPI();
                            }
                            catch
                            {
                                ContentDialog error = new ContentDialog();
                                error.Title = "No IPv4 was found on your computer, are you sure you're connected to the internet? This app can only be used when connected to the internet.";
                                error.PrimaryButtonText = "Ok";
                                if (await error.ShowAsync() == ContentDialogResult.Primary)
                                    await ApplicationView.GetForCurrentView().TryConsolidateAsync();
                            }
                        }*/
                    }
                }
            }
            var type = localSettings.Values.GetType();
            if (type.GetMethod(key) == null)
                getAPI();
        }

        private async Task<string> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }

        public string AddQuotesIfRequired(string path)
        {
            return (!string.IsNullOrWhiteSpace(path)) ? (path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\""))) ? "\"" + path + "\"" : path : string.Empty;
        }

        async public void downloadAPI()
        {
            string whitespaceDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string testDir = Path.Combine(whitespaceDir, "BrAIn-Client");
            string curDir = AddQuotesIfRequired(whitespaceDir);
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Debug.WriteLine(userName);
            Debug.WriteLine(Path.Combine(curDir, "BrAIn-Client"));
            //Directory.CreateDirectory(Path.Combine(whitespaceDir, "BrAIn-Client")); 

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("cmd.exe", $"/k curl https://github.com/LucAngevare/BrAIn-API/releases/download/0.3.1-stripped-bin/BrAIn-API-bin-0.3.1-worker.zip -o \"{Path.Combine(curDir, "BrAIn-API-bin-0.3.1-worker.zip")}\"");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal", $"/C curl https://github.com/LucAngevare/BrAIn-API/releases/download/0.3.1-stripped-bin/BrAIn-API-bin-0.3.1-worker.zip -o \"{Path.Combine(curDir, "BrAIn-API-bin-0.3.1-worker.zip")}\"");
            }
            else
            {
                Process.Start("/bin/bash", $"/C curl https://github.com/LucAngevare/BrAIn-API/releases/download/0.3.1-stripped-bin/BrAIn-API-bin-0.3.1-worker.zip -o \"{Path.Combine(curDir, "BrAIn-API-bin-0.3.1-worker.zip")}\"");
            }

            //Process.Start("cmd.exe", $"/C curl https://github.com/LucAngevare/BrAIn-API/releases/download/0.3.1-stripped-bin/BrAIn-API-bin-0.3.1-worker.zip -o \"{Path.Combine(curDir, "BrAIn-API-bin-0.3.1-worker.zip")}\"");
            ProgressBar progressBar = new ProgressBar();
            progressBar.IsIndeterminate = true;
            progressBar.Height = 12;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = progressBar;
            dialog.Title = "Downloading BrAIn API. This may take a while";
            dialog.PrimaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                dialog.Hide();

            dialog.Hide();
            ContentDialog finished = new ContentDialog();
            finished.Title = "Done downloading BrAIn API! You may now close this dialog!";
            finished.PrimaryButtonText = "Close";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return host.AddressList[i].ToString();
                }
            }
            throw new Exception("No IPv4 found, API self-hosting not possible.");
        }

        public void tabButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch(btn.Name)
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
