using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLettersBLL
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int loadingProgress;
        public int LoadingProgress
        {
            get { return loadingProgress; }
            set
            {
                if (loadingProgress != value)
                {
                    loadingProgress = value;
                    OnPropertyChanged(nameof(LoadingProgress));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartLoading()
        {
            LoadingProgress = 0;
        }
    }
}
