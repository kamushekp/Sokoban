using Microsoft.Xna.Framework.Graphics;
using NGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Creatures
{
    public class Wall : ACreature
    {
        public Wall(Location loc, Texture2D texture) : base(loc, texture)
        {

        }
    }
}
