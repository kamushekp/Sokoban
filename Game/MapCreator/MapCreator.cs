using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NGame.creature_box;
using NGame.Logic;

namespace NGame.NMapCreator
{
    public class MapCreator
    {
        private GraphicsDeviceManager graphics;
        private Dictionary<string, Texture2D> textures;
        private int textureWidth;
        private int textureHeight;

        public List<ACreature[,]> Maps { get; }
 
        public MapCreator(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> textures)
        {

            this.textureWidth = textures[nameof(Box)].Width;
            this.textureHeight = textures[nameof(Box)].Height;

            this.graphics = graphics;
            this.textures = textures;

            Maps = new List<ACreature[,]>();
            CreateWorld();
        }

        private void CreateWorld()
        {
            var dir = Environment.CurrentDirectory;
            var pathToMaps = dir.Substring(0, dir.LastIndexOf("Game") + 4) + "\\Maps";
            foreach(var file in Directory.GetFiles(pathToMaps))
            {
                Maps.Add(CreateMap(file));
            }

        }


        private ACreature[,] CreateMap(string txtMap)
        {
            var rows = File.ReadAllLines(txtMap);
            var rowCount = rows[0].Length;
            var colCount = rows.Length;
            

            var result = new ACreature[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = CreateCreatureBySymbol(rows[i][j], i, j);                    
                }
            }

            return result;
        }

        private ACreature CreateCreatureBySymbol(char c, int i, int j)
        {

            var x = (i + 1) * textureWidth - textureWidth / 2;
            var y = (j + 1) * textureHeight - textureHeight / 2;

            switch (c)
            {
                case '#':
                return new Box(new Location(i,j), new Vector2(x, y), textures[nameof(Box)]);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
