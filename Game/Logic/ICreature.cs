using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGame.Logic
{
    public interface ICreature
    {
        Vector2 Location { get; }
        bool IsActive { get; }
        UserComand CurrentComand {get; set;}
        
        //ICreatureHandler CreatureHandler { get; }
        List<UserComand> GetWhatReactingOn();

        int GetDrawingPriority();

        void Draw(SpriteBatch spriteBatch);


    }
}
