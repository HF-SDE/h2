using FiveWordFiveLettersBLL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using Label = System.Windows.Controls.Label;

namespace FiveWordFiveLettersWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker1 = new();
        int lengthOfWords;
        string filePath;
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker1 = new()
            {
                WorkerSupportsCancellation = true
            };
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

        }

        private void BtnOpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
                fileName.Text = openFileDialog.FileName;
        }

        private void BtnLoad_OnClick(object sender, EventArgs e)
        {
            if (fileName.Text == "")
            {
                IsItNotSetted("");
                return;
            }
            matched.Content = "Loading...";
            lengthOfWords =  int.Parse(amountOfMatch.Text);
            filePath = fileName.Text;
            backgroundWorker1.RunWorkerAsync();
        }

        private void FileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = fileName.Text;
            if (inputText == "")
            {
                IsItNotSetted("Select a file first");
                return;
            }
            if (!IsValidFilePath(inputText) && !File.Exists(inputText))
            {
                IsItNotSetted("Invalid .txt file or does not exist.");
                return;
            }
            fileName.ClearValue(TextBox.BorderBrushProperty);
            matched.Content = "";
            matched.ClearValue(Label.ForegroundProperty);
            fileName.BorderThickness = new Thickness(0);
            btnLoadData.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Dictionary<int, string> file = GetWords.GetWordBinary(fileName.Text, int.Parse(amountOfMatch.Text));
        //    e.Result = Algoritme.MultiTheardBinary(file);
        //}

        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    matched.Content = e.Result;
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            Dictionary<int, string> file = GetWords.GetWordBinary(filePath, lengthOfWords);

            // Start the time-consuming operation.
            e.Result = Algoritme.MultiTheardBinary(file);

        }

        // This event handler demonstrates how to interpret
        // the outcome of the asynchronous operation implemented
        // in the DoWork event handler.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            matched.Content = e.Result;
            //if (e.Cancelled)
            //{
            //    // The user canceled the operation.
            //    MessageBox.Show("Operation was canceled");
            //}
            //else if (e.Error != null)
            //{
            //    // There was an error during the operation.
            //    string msg = String.Format("An error occurred: {0}", e.Error.Message);
            //    MessageBox.Show(msg);
            //}
            //else
            //{
            //    // The operation completed normally.
            //    string msg = String.Format("Result = {0}", e.Result);
            //    MessageBox.Show(msg);
            //}
        }

        internal void IsItNotSetted(string text)
        {
            matched.Content = text;
            matched.Foreground = Brushes.Red;
            fileName.BorderBrush = Brushes.Red;
            fileName.BorderThickness = new Thickness(2);
            btnLoadData.IsEnabled = false;
        }



        internal static bool IsValidFilePath(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                // Use a regular expression to match .txt files
                string pattern = @"^.+\.txt$";
                return Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase);
            }
            return false;
        }
    }
}
