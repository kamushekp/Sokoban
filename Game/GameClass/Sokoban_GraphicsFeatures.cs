using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGame.Logic
{
    public sealed partial class Sokoban
    {
        private ContentManager content;

        private Dictionary<string, Texture2D> textures; //текстуры сущностей
        private int currentTextureSize; //размер ящика (для этих текстур = 64)

        private int windowHeight;
        private int windowWidth;

        private Texture2D[] backgrounds;// текстуры полов уровней
     
        private Vector2[,] currentLocations; //местоположения существ в пикселях

       
    }
}
