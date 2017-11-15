using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Game.Logic
{
    class UserComand
    {
        public EventArgs Comand { get;}
        public UserComand(EventArgs comand)
        {
            this.Comand = comand;
        }
    }
}
