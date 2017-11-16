using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public class UserComand
    {
        public EventArgs Comand { get;}
        public UserComand(EventArgs comand)
        {
            this.Comand = comand;
        }
    }
}
