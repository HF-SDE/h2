using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLettersWPF.libs
{
    public class ViewModel : INotifyPropertyChanged
    {
        private int _maximumProgress = 100;
        private int _currentProgress = 0;

        /// <summary>
        /// Gets or sets the maximum progress value, and notifies subscribers when it changes.
        /// </summary>
        public int MaximumProgress
        {
            get { return _maximumProgress; }
            set
            {
                if (_maximumProgress != value)
                {
                    _maximumProgress = value;
                    NotifyPropertyChange(nameof(MaximumProgress));
                }
            }
        }

        /// <summary>
        /// Gets or sets the current progress value, and notifies subscribers when it changes.
        /// </summary>
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                if (_currentProgress != value)
                {
                    _currentProgress = value;
                    NotifyPropertyChange(nameof(CurrentProgress));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies subscribers that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void NotifyPropertyChange(string propertyName)
        {
            if (propertyName != null) PropertyChanged!(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
