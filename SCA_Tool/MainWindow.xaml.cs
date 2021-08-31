using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace SCA_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileHelper fileHelper = new FileHelper();
        public MainWindow()
        {
            InitializeComponent();
            ProgressStatus.Visibility = Visibility.Collapsed;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void fileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();
        }

        private void SelectAFile_Click(object sender, RoutedEventArgs e)
        {
            ProgressStatus.Visibility = Visibility.Visible;
            SelectAFile.IsEnabled = false;
            ProgressStatus.Text = "Scanning Directory";

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                var task = Task.Run(() =>
                {
                    if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        List<ExternalSoftware> externalSoftware = fileHelper.GetSoftwareUsedInApplication(fbd.SelectedPath);

                        foreach (var item in externalSoftware)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                this.softwareList.Items.Add(item);
                            });
                        }
                    }
                });

                task.ContinueWith((t) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        SelectAFile.IsEnabled = true;
                        ProgressStatus.Text = "Scan Complete";
                    });
                });
            }
        }
    }
}