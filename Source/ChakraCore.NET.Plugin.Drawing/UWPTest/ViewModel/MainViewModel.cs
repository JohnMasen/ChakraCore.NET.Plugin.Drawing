using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ChakraCore.NET.Hosting;
using ChakraCore.NET.Plugin.Drawing.ImageSharp;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using SixLabors.ImageSharp;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using static GalaSoft.MvvmLight.Threading.DispatcherHelper;

namespace UWPTest.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public IEnumerable<DirectoryInfo> Folders { get; private set; }
        public string Info { get; private set; }
        JSDrawApp currentApp;
        private ImageSharpDrawingInstaller imageSharpEngine;
        private void RefreshFolders()
        {
            DirectoryInfo info = new DirectoryInfo("Scripts");
            Folders = info.GetDirectories();
            RaisePropertyChanged(nameof(Folders));
        }
        public MainViewModel()
        {
            RefreshFolders();
        }

        public DirectoryInfo SelectedItem { get; set; }

        public RelayCommand Load  => new RelayCommand(() => 
        {
            if (SelectedItem==null)
            {
                return;
            }
            string rootFolder = SelectedItem.FullName;
            imageSharpEngine = new ImageSharpDrawingInstaller(rootFolder);
            
            JavaScriptHostingConfig config = new JavaScriptHostingConfig();
            config.AddModuleFolder(rootFolder);
            config.AddPlugin(imageSharpEngine);
            currentApp = JavaScriptHosting.Default.GetModuleClass<JSDrawApp>("app", "App", config);
            currentApp.Init();
        });


        public RelayCommand Run => new RelayCommand(async () =>
           {
               Stopwatch stopwatch = new Stopwatch();
               stopwatch.Start();
               currentApp.Draw();
               stopwatch.Stop();
               Info = $"draw cost {stopwatch.ElapsedMilliseconds} ms";
               await updateOutput();
               await notifyChangedOnUI(nameof(Info));
           });
        private async Task updateOutput()
        {
            var stream = new MemoryStream();
            imageSharpEngine.LastDrawingSurface.Image.SaveAsBmp(stream);
            stream.Position = 0;
            var bii = await BitmapFactory.FromStream(stream);
            ImageSharpOutput = bii;
            await notifyChangedOnUI(nameof(ImageSharpOutput));
        }
        

        
        public ImageSource ImageSharpOutput { get; private set; }

        private async Task notifyChangedOnUI(string propertyName)
        {
            await UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                RaisePropertyChanged(propertyName);
            });
        }
    }
}