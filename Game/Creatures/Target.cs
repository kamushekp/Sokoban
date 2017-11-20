using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using NGame.CreatureHandlers;
using NGame.Logic;

namespace NGame.Creatures
{
    class Target : ACreature
    {
        public Target(Location loc, Texture2D texture) : base(loc, texture)
        {
            this.CreatureHandler = new TargetHandler();
            this.IsActive = true;
        }
    }
}
