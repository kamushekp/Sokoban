using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public class UserComand
    {
        public KeyboardState Comand { get; }

        public UserComand(KeyboardState comand)
        {
            this.Comand = comand;
        }
    }
}
