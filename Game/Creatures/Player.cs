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
        public Player(Location location, Vector2 vector2, Texture2D texture) : base(location, vector2, texture)
        {
            this.CreatureHandler = new PlayerHandler();
        }

        public override List<string> GetWhatReactingOn()
        {
            return new List<string> { "Up", "Down", "Left", "Right" };
        }
    }
}
