using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    internal interface IEvent
    {
        public delegate void MyEventHandler(object sender, EventArgs e);
    }
}
