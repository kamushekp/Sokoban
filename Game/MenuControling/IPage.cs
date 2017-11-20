using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.MenuControling
{
    public interface IPage
    {
        void Draw(SpriteBatch spriteBatch);
        int Update(KeyboardState currentKeyboardState);
    }
}
