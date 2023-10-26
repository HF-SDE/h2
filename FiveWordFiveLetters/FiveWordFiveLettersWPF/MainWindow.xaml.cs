using FiveWordFiveLettersBLL;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private BackgroundWorker _woker = new();
        private int _lengthOfWord = 5;
        private int _lengthOfWords = 5;
        private string _filePath;
        private List<string> _result;
        private int _currentProgress;
        private int _maximumProgress = 100;
        private readonly Algoritme _algoritme;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<int> MaximumProgressEvent;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _algoritme = new();
            _algoritme.Progress += ProgressChanged;
            MaximumProgressEvent += MaximumChanged;
            _woker = new()
            {
                WorkerSupportsCancellation = true
            };
            _woker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork!);
            _woker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted!);
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
            _woker.RunWorkerAsync();
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
            if (!IsValidFilePath(inputText) && !File.Exists(inputText))
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
        /// Handles the text changed event when the user enters or edits the "amountOfMatch" field.
        /// Attempts to parse the entered text as an integer, and sets the "lengthOfWord" variable accordingly.
        /// </summary>
        private void amountOfMatch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = int.TryParse(amountOfMatch.Text, out _lengthOfWord);
        }

        /// <summary>
        /// Handles the text changed event when the user enters or edits the "amountOfMatchWords" field.
        /// Attempts to parse the entered text as an integer and sets the "lengthOfWords" variable accordingly.
        /// </summary>
        private void amountOfMatchWords_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = int.TryParse(amountOfMatchWords.Text, out _lengthOfWords);
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
                File.WriteAllLines(saveFileDialog.FileName, _result);

        }

        /// <summary>
        /// Validates text input to allow only numeric characters in a text box.
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Updates the current progress value when a progress change event occurs.
        /// </summary>
        private void ProgressChanged(object? sender, int e)
        {
            CurrentProgress = e;
        }

        /// <summary>
        /// Updates the maximum progress value when a maximum progress change event occurs.
        /// </summary>
        private void MaximumChanged(object? sender, int e)
        {
            MaximumProgress = e;
        }

        /// <summary>
        /// Notifies subscribers that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void NotifyPropertyChange(string propertyName)
        {
            if (propertyName != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets the current progress value, and notifies subscribers when it changes.
        /// </summary>
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { _currentProgress = value; NotifyPropertyChange(nameof(CurrentProgress)); }
        }

        /// <summary>
        /// Gets or sets the maximum progress value, and notifies subscribers when it changes.
        /// </summary>
        public int MaximumProgress
        {
            get { return _maximumProgress; }
            set { _maximumProgress = value; NotifyPropertyChange(nameof(MaximumProgress)); }
        }

        /// <summary>
        /// Performs background work when the background worker is running.
        /// This method loads word data from a file and performs a multi-threaded binary operation.
        /// </summary>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<int, string> file = GetWords.GetWordBinary(_filePath, _lengthOfWord);
            MaximumProgress = 100;
            e.Result = _algoritme.MultiTheardBinary(file, _lengthOfWords);

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
        /// Checks if a given file path is a valid path to a text file with a '.txt' extension.
        /// </summary>
        /// <param name="filePath">The file path to be validated.</param>
        /// <returns>
        ///   <c>true</c> if the file path is valid and represents a '.txt' text file; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidFilePath(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                string pattern = @"^.+\.txt$";
                return Regex.IsMatch(filePath, pattern, RegexOptions.IgnoreCase);
            }
            return false;
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
    };

}
