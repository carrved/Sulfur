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
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using System.IO;
using sxlib;
using sxlib.Specialized;

namespace Sulfur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SxLibWPF SXLib;

        public MainWindow()
        {
           // synx warning
            string messageBoxText = "This exploit assumes you have Synapse X installed.";
            string caption = "Sulfur";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);

            // init window
            InitializeComponent();

            // syntax highlighting for script editor
            ScriptEditor.TextArea.TextView.LinkTextBackgroundBrush = Brushes.Transparent;
            ScriptEditor.TextArea.TextView.LinkTextForegroundBrush = Brushes.LightSalmon;
            ScriptEditor.TextArea.TextView.LinkTextUnderline = false;
            Stream stream = File.OpenRead("./bin/lua.xshd");
            XmlTextReader reader = new XmlTextReader(stream);
            ScriptEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }


        private void MB_Loaded(object sender, RoutedEventArgs e)
        {
            this.SXLib = SxLib.InitializeWPF(this, Directory.GetCurrentDirectory());
            SXLib.LoadEvent += new SxLibWPF.SynLoadDelegate(this.SynapseLoadEvent);
            SXLib.AttachEvent += new SxLibWPF.SynAttachDelegate(this.SynapseAttachEvent);
            SXLib.Load();
        }

        private void SynapseAttachEvent(SxLibBase.SynAttachEvents evnt, object _)
        {
            MainWindow mainWindow = this;
            switch (evnt)
            {
                case SxLibBase.SynAttachEvents.UPDATING_DLLS:
                    mainWindow.ConsoleTxt.Text = "Updating DLLs... \n";
                    break;
                case SxLibBase.SynAttachEvents.SCANNING:
                    mainWindow.ConsoleTxt.Text = "Scanning... \n";
                    break;
                case SxLibBase.SynAttachEvents.REINJECTING:
                    mainWindow.ConsoleTxt.Text = "Reinjecting... \n";
                    break;
                case SxLibBase.SynAttachEvents.READY:
                    mainWindow.ConsoleTxt.Text = "Ready! \n";
                    break;
                case SxLibBase.SynAttachEvents.PROC_DELETION:
                    mainWindow.ConsoleTxt.Text = "Roblox process has been lost! \n";
                    break;
                case SxLibBase.SynAttachEvents.PROC_CREATION:
                    mainWindow.ConsoleTxt.Text = "Roblox process detected! \n";
                    break;
                case SxLibBase.SynAttachEvents.NOT_UPDATED:
                    mainWindow.ConsoleTxt.Text = "Not updated! Check Synapse X discord server for a new update. \n";
                    break;
                case SxLibBase.SynAttachEvents.NOT_RUNNING_LATEST_VER_UPDATING:
                    mainWindow.ConsoleTxt.Text = "Not latest version! Updating... \n";
                    break;
                case SxLibBase.SynAttachEvents.NOT_INJECTED:
                    mainWindow.ConsoleTxt.Text = "Not injected! \n";
                    break;
                case SxLibBase.SynAttachEvents.INJECTING:
                    mainWindow.ConsoleTxt.Text = "Injecting... \n";
                    break;
                case SxLibBase.SynAttachEvents.FAILED_TO_UPDATE:
                    mainWindow.ConsoleTxt.Text = "Failed to update! \n";
                    break;
                case SxLibBase.SynAttachEvents.FAILED_TO_FIND:
                    mainWindow.ConsoleTxt.Text = "Failed to find Roblox process! \n";
                    break;
                case SxLibBase.SynAttachEvents.FAILED_TO_ATTACH:
                    mainWindow.ConsoleTxt.Text = "Failed to inject! \n";
                    break;
                case SxLibBase.SynAttachEvents.CHECKING_WHITELIST:
                    mainWindow.ConsoleTxt.Text = "Checking whitelist... \n";
                    break;
                case SxLibBase.SynAttachEvents.CHECKING:
                    mainWindow.ConsoleTxt.Text = "Checking... \n";
                    break;
                case SxLibBase.SynAttachEvents.ALREADY_INJECTED:
                    mainWindow.ConsoleTxt.Text = "Already injected! \n";
                    break;
            }
        }

        private void SynapseLoadEvent(SxLibBase.SynLoadEvents evnt, object _)
        {
            MainWindow mainWindow = this;
            switch (evnt)
            {
                case SxLibBase.SynLoadEvents.NOT_LOGGED_IN:
                    mainWindow.ConsoleTxt.Text = "Not logged in! \n";
                    break;
                case SxLibBase.SynLoadEvents.NOT_UPDATED:
                    mainWindow.ConsoleTxt.Text = "Not updated! \n";
                    break;
                case SxLibBase.SynLoadEvents.FAILED_TO_VERIFY:
                    mainWindow.ConsoleTxt.Text = "Failed to verify! \n";
                    break;
                case SxLibBase.SynLoadEvents.FAILED_TO_DOWNLOAD:
                    mainWindow.ConsoleTxt.Text = "Failed to download! \n";
                    break;
                case SxLibBase.SynLoadEvents.UNAUTHORIZED_HWID:
                    mainWindow.ConsoleTxt.Text = "Unauthorized HWID! \n";
                    break;
                case SxLibBase.SynLoadEvents.CHECKING_WL:
                    mainWindow.ConsoleTxt.Text = "Checking whitelist... \n";
                    break;
                case SxLibBase.SynLoadEvents.DOWNLOADING_DATA:
                    mainWindow.ConsoleTxt.Text = "Downloading data... \n";
                    break;
                case SxLibBase.SynLoadEvents.CHECKING_DATA:
                    mainWindow.ConsoleTxt.Text = "Checking data... \n";
                    break;
                case SxLibBase.SynLoadEvents.DOWNLOADING_DLLS:
                    mainWindow.ConsoleTxt.Text = "Downloading DLLS... \n";
                    break;
                case SxLibBase.SynLoadEvents.READY:
                    mainWindow.ConsoleTxt.Text = "READY! \n";
                    break;
            }
        }

        private void MB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // draggable window
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // SE
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            // SE
            this.WindowState = WindowState.Minimized;
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            // SE
            SXLib.Execute(ScriptEditor.Text);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // opens "Open Script" dialog
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Nullable<bool> result = openFileDialog.ShowDialog();
            openFileDialog.Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Open Script";
            if (result == true)
            {
                ScriptEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // SE
            ScriptEditor.Text = "";
        }

        private void Attach_Click(object sender, RoutedEventArgs e)
        {
            SXLib.Attach();
        }

        private void ScriptHub_Click(object sender, RoutedEventArgs e)
        {
            // SE
            ScriptHub.IsEnabled = true;
            ScriptHub scripthub = new ScriptHub();
            scripthub.ShowDialog();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // script list
            this.List.Items.Clear();
            foreach (FileInfo fileInfo in new DirectoryInfo("./Scripts").GetFiles("*.txt"))
            {
                this.List.Items.Add(fileInfo.Name);
            }
            foreach (FileInfo fileInfo2 in new DirectoryInfo("./Scripts").GetFiles("*.lua"))
            {
                this.List.Items.Add(fileInfo2.Name);
            }
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // script to editor when script selected in list
            bool flag = this.List.SelectedIndex != -1;
            if (flag)
            {
                ScriptEditor.Text = File.ReadAllText("scripts\\" + this.List.SelectedItem.ToString());
            }
        }
    }
}
