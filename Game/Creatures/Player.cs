using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NGame.CreaturesHandlers;
using NGame.Logic;
using System;
using System.Collections.Generic;


namespace NGame.Creatures
{
    class Player : ACreature
    {
        public Player(Location location, Texture2D texture) : base(location, texture)
        {
            this.CreatureHandler = new PlayerHandler();
        }

        public override List<string> GetWhatReactingOn()
        {
            return new List<string> { "Up", "Down", "Left", "Right" };
        }

        public override int GetDrawingPriority()
        {
            return 10;
        }
    }
}
