using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NGame.CreaturesHandlers;
using NGame.Logic;
using System;
using System.Collections.Generic;


namespace NGame.Creatures
{
    class Empty : ACreature
    {
        public Empty(Location location, Texture2D texture) : base(location, texture)
        {
            this.CreatureHandler = new EmptyHandler();
        }
        public override List<string> GetWhatReactingOn()
        {
            return new List<string>();
        }
    }
}
