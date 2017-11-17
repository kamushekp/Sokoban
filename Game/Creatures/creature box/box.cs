using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGame.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NGame.creature_box
{
    public class Box : ACreature
    {

        public Box(Location loc, Vector2 vec, Texture2D texture) : base(loc, vec, texture)
        {
            this.CreatureHandler = new BoxHandler();
        }

        public override List<UserComand> GetWhatReactingOn()
        {
            return new List<UserComand>();
        }
    }
}
