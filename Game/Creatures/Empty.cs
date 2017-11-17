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
        public Empty(Location location, Vector2 vector2, Texture2D texture) : base(location, vector2, texture)
        {
            this.CreatureHandler = new EmptyHandler();
        }
        public override List<string> GetWhatReactingOn()
        {
            throw new NotImplementedException();
        }
    }
}
