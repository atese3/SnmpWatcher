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

namespace WatcherUI
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : UserControl
    {
        private Listener listener;
        private string newLine = string.Empty;
        private string _log = string.Empty;
        private static UserInterface _inst = null;
        public static UserInterface GetInst()
        {
            if (_inst == null)
            {
                _inst = new UserInterface();
            }
            return _inst;
        }
        public string Log
        {
            get
            {
                return _log;
            }
            set
            {
                _log = value;

                /// Dispatcher used for making it reachable from another thread 
                this.Dispatcher.Invoke(() =>
                {
                    this.richTextBox1.AppendText(value + Environment.NewLine + newLine);

                    /// To solve richtextbox new line problem after clearing all text
                    if (string.Equals(newLine, Environment.NewLine))
                    {
                        newLine = string.Empty;
                    }
                });
            }
        }
        public UserInterface()
        {
            InitializeComponent();
            _inst = this; // Singelton
            listener = new Listener();
            Logger.WriteLine("--------------------------- Logger Started ---------------------------");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /// Saving parameters to file for futher uses
            try
            {
                Parameters.Params.IP = txtIp.Text;
                Parameters.Params.Port = Int32.Parse(txtPort.Text);
                Parameters.Params.Content = txtContent.Text;
                Parameters.Params.Oid = txtOid.Text;
                Parameters.Params.Timeout = Int32.Parse(txtTimeout.Text);
                Parameters.SaveToFile();
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Form Paramater Save Exception: " + ex.Message);
            }

            try
            {
                listener.start(Parameters.Params.Timeout);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Process Start Exception: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CloseConsole();
        }

        private void CloseConsole()
        {
            try
            {
                listener.cycleTimer.Stop();
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Close Console Exception: " + ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            /// Clear Richtextbox
            this.richTextBox1.Document.Blocks.Clear();
            Paragraph.SetLineHeight(this.richTextBox1, 1);
            newLine = Environment.NewLine;
        }
    }
}
