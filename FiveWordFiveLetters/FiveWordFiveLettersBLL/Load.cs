using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLettersBLL
{
    public class Load
    {
        public event EventHandler<int> Progress;

        protected virtual void OnPropertyChanged1(int index)
        {
            Progress?.Invoke(this, index);
        }
    }
}
