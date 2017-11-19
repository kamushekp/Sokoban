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

        public Player(Location location, Dictionary<string, Texture2D> textures) : base(location, null)
        {
            CreatureHandler = new PlayerHandler();
            CurrentAnimation = new Animation(textures["Left"], Vector2.Zero, 52, 69, 2, 10, Color.White, 1f, true);

            Animations = new Dictionary<string, Animation>
            {
                {"Left", CurrentAnimation},
                {"Right", new Animation(textures["Right"], Vector2.Zero, 52, 69, 2, 10, Color.White, 1f, true)},
                {"Down", new Animation(textures["Up"], Vector2.Zero, 47, 69, 3, 10, Color.White, 1f, true)},
                {"Up", new Animation(textures["Down"], Vector2.Zero, 47, 69, 2, 10, Color.White, 1f, true)},
            };

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
