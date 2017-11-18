using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NGame.Creatures;
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

            var isRowsSameLenght = (rows.Select(x => x.Length).All(row => row == rows[0].Length));
            if (!isRowsSameLenght) throw new ArgumentException("Rows are not you wanted to", nameof(rows));

            var colCount = rows[0].Length;
            var rowCount = rows.Length;
            

            var result = new ACreature[colCount, rowCount];

            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    result[i, j] = CreateCreatureBySymbol(rows[j][i], i, j);                    
                }
            }

            return result;
        }

        private ACreature CreateCreatureBySymbol(char c, int i, int j)
        {

            switch (c)
            {
                case '#':
                    return new Box(new Location(i, j), textures[nameof(Box)]);

                case 'P':
                    return new Player(new Location(i, j), textures[nameof(Player)]);

                case '-':
                    return new Empty(new Location(i, j), textures[nameof(Empty)]);

                default:
                    throw new ArgumentException("Error code of creature. See tutorial", nameof(c));
            }
        }
    }
}
