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
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using static GalaSoft.MvvmLight.Threading.DispatcherHelper;
using System.Linq;
using Windows.Storage;

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
        public IEnumerable<StorageFolder> Folders { get; private set; }
        /// <summary>
        /// The <see cref="Info" /> property's name.
        /// </summary>
        public const string InfoPropertyName = "Info";

        private string _info = string.Empty;

        /// <summary>
        /// Sets and gets the Info property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Info
        {
            get
            {
                return _info;
            }

            set
            {
                if (_info == value)
                {
                    return;
                }

                _info = value;
                RaisePropertyChanged(() => Info);
            }
        }
        JSDrawApp currentApp;
        private ImageSharpDrawingInstaller imageSharpEngine;
        
        

        public StorageFolder SelectedItem { get; set; }

        public RelayCommand Load => new RelayCommand(async () =>
       {
           Info = string.Empty;
           if (SelectedItem == null)
           {
               return;
           }
           //string rootFolder = SelectedItem.FullName;
           imageSharpEngine = new ImageSharpDrawingInstaller();
           //imageSharpEngine.TextureLoader.Add(loadTexture);
           imageSharpEngine.SetTextureRoot(SelectedItem);
           imageSharpEngine.SetFontRoot(SelectedItem);

           JavaScriptHostingConfig config = new JavaScriptHostingConfig();
           config.ModuleFileLoaders.Add(loadModule);
           config.AddPlugin(imageSharpEngine);
           currentApp = await JavaScriptHosting.Default.GetModuleClassAsync<JSDrawApp>("app", "App", config);
           await currentApp.InitAsync();
           Info = "Loaded";
       });


        private string loadModule(string name)
        {
            var t = loadModuleAsync(name);
            t.Wait();
            return t.Result;
        }

        private async Task<string> loadModuleAsync(string name)
        {
            using (var file=await SelectedItem.OpenStreamForReadAsync($"{name}.js"))
            {
                using (var reader = new StreamReader(file))
                {
                    return reader.ReadToEnd();
                }
            } 
        }

        public RelayCommand Run => new RelayCommand(async () =>
           {
               Stopwatch stopwatch = new Stopwatch();
               stopwatch.Start();
               currentApp.Draw();
               stopwatch.Stop();
               Info = $"draw cost {stopwatch.ElapsedMilliseconds} ms";
               await updateOutput();
           });
        private async Task updateOutput()
        {
            var stream = new MemoryStream();
            imageSharpEngine.LastDrawingSurface.Image.SaveAsBmp(stream);
            stream.Position = 0;
            var bii = await BitmapFactory.FromStream(stream);
            ImageSharpOutput = bii;
            RaisePropertyChanged(nameof(ImageSharpOutput));
        }

        public RelayCommand ChooseRoot =>new RelayCommand(async ()=>
        {
            FolderPicker picker = new FolderPicker();
            picker.FileTypeFilter.Add("*");
            var result=await picker.PickSingleFolderAsync();
            if (result!=null)
            {
                Folders = await result.GetFoldersAsync();

                RaisePropertyChanged(nameof(Folders));

            }
            

        });

        
        public ImageSource ImageSharpOutput { get; private set; }

        
    }
}