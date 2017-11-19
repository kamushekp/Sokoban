using System;
using System.Collections.Generic;
using NGame.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NGame.CreaturesHandlers;

namespace NGame.Creatures
{
    public class Box : ACreature
    {
        public Box(Location loc, Texture2D texture) : base(loc, texture)
        {
            this.CreatureHandler = new BoxHandler();
        }

        public override int GetDrawingPriority()
        {
            return 10;
        }

        public override List<String> GetWhatReactingOn()
        {
            return new List<String>();
        }
    }
}
