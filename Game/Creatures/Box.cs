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
    }
}
