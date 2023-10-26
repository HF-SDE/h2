using FiveWordFiveLettersBLL;
using FiveWordFiveLettersWPF.libs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Label = System.Windows.Controls.Label;

namespace FiveWordFiveLettersWPF
{
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _worker = new();
        private readonly ViewModel _viewModel = new();
        private readonly Utils _utils = new();
        private int _lengthOfWord = 5;
        private int _lengthOfWordsCombination = 5;
        private string? _filePath;
        private List<string>? _result;
        private readonly Algoritme _algoritme = new();

        public event EventHandler<int>? MaximumProgressEvent;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            _algoritme.Progress += ProgressChanged;
            MaximumProgressEvent += MaximumChanged;
            _worker = new()
            {
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork!);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted!);
        }

        /// <summary>
        /// Handles the click event of the "Open File" button, allowing the user to select a text file.
        /// </summary>
        private void BtnOpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressChanged(null, 0);
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            if (openFileDialog.ShowDialog() == true) fileName.Text = openFileDialog.FileName;
        }

        private void FileName_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                fileName.Text = files[0];
            }
        }

        /// <summary>
        /// Handles the click event of the "Load" button, initiating data loading and processing.
        /// </summary>
        private void BtnLoad_OnClick(object sender, EventArgs e)
        {
            if (fileName.Text == "")
            {
                IsItNotSetted("");
                return;
            }
            matched.Content = $"{matched.Content.ToString()?.Split(" ")[0]}";
            ProgressChanged(null, 10);
            btnLoadData.IsEnabled = false;
            _filePath = fileName.Text;
            pbStatus.IsIndeterminate = true;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the text changed event when the user enters or edits the file name in the input field.
        /// Validates the input and updates UI elements accordingly.
        /// </summary>
        private void FileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = fileName.Text;
            if (inputText == "")
            {
                IsItNotSetted("Select a file first");
                return;
            }
            if (!_utils.IsValidFilePath(inputText) && !File.Exists(inputText))
            {
                IsItNotSetted("Invalid .txt file or does not exist.");
                return;
            }
            fileName.ClearValue(TextBox.BorderBrushProperty);
            matched.Content = matched.Content.ToString()?.Split(" ")[0];
            matched.ClearValue(Label.ForegroundProperty);
            btnLoadData.IsEnabled = true;
        }

        /// <summary>
        /// Handles the event when the "Dark Mode" option is checked.
        /// This method updates the application settings to set the color mode to "Dark" and saves the settings.
        /// </summary>
        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Dark";
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handles the event when the "Light Mode" option is checked.
        /// This method updates the application settings to set the color mode to "Light" and saves the settings.
        /// </summary>
        private void LightMode_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Light";
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handles the text changed event when the user enters or edits the "amountOfMatch" field.
        /// Attempts to parse the entered text as an integer, and sets the "lengthOfWord" variable accordingly.
        /// </summary>
        private void LengthOfLetters_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = int.TryParse(amountOfMatch.Text, out _lengthOfWord);
        }

        /// <summary>
        /// Handles the text changed event when the user enters or edits the "amountOfMatchWords" field.
        /// Attempts to parse the entered text as an integer and sets the "lengthOfWords" variable accordingly.
        /// </summary>
        private void LengthOfMatchWords_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = int.TryParse(amountOfMatchWords.Text, out _lengthOfWordsCombination);
        }

        /// <summary>
        /// Handles the click event of the "Save File" button, allowing the user to save data to a text file.
        /// </summary>
        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Text file (*.txt)|*.txt"
            };
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, _result!);

        }

        /// <summary>
        /// Displays an error message and visually indicates that a required action is not properly set.
        /// </summary>
        /// <param name="text">The error message to be displayed.</param>
        internal void IsItNotSetted(string text)
        {
            matched.Content = text;
            matched.Foreground = Brushes.Red;
            fileName.BorderBrush = Brushes.Red;
            fileName.BorderThickness = new Thickness(2);
            btnLoadData.IsEnabled = false;
        }

        /// <summary>
        /// Validates text input to allow only numeric characters in a text box.
        /// </summary>
        internal void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Performs background work when the background worker is running.
        /// This method loads word data from a file and performs a multi-threaded binary operation.
        /// </summary>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<int, string> file = GetWords.GetWordBinary(_filePath!, _lengthOfWord);
            _viewModel.MaximumProgress = 100;
            e.Result = _algoritme.MultiTheardBinary(file, _lengthOfWordsCombination);

        }

        /// <summary>
        /// Handles the completion of background work when the background worker has finished its task.
        /// This method updates UI elements, such as displaying the count of results, enabling buttons.
        /// </summary>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _result = (List<string>)e.Result!;
            matched.Content = $"{matched.Content.ToString()?.Split(" ")[0]} {_result?.Count}";
            btnSaveFile.IsEnabled = true;
            btnLoadData.IsEnabled = true;
            pbStatus.IsIndeterminate = false;
            pbStatus.Value = 100;
        }

        /// <summary>
        /// Updates the current progress value when a progress change event occurs.
        /// </summary>
        private void ProgressChanged(object? sender, int e)
        {
            _viewModel.CurrentProgress = e;
        }

        /// <summary>
        /// Updates the maximum progress value when a maximum progress change event occurs.
        /// </summary>
        private void MaximumChanged(object? sender, int e)
        {
            _viewModel.MaximumProgress = e;
        }
    };

}
