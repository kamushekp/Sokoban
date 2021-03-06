﻿using System;
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

        public Rectangle CurrentMapRectangle { get; set; }
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
            var pathToMaps = FindFolderMaps(dir);
            foreach(var file in Directory.GetFiles(pathToMaps))
            {
                Maps.Add(CreateMap(file));
            }
        }
        private string FindFolderMaps(string path)
        {

            int i = 0;
            while (i < 15)
            {
                var directories = Directory.GetDirectories(path);

                foreach (var dir in directories)
                {
                    if (dir.Contains("Maps"))
                    {
                        return dir;
                    }
                }
                path = Directory.GetParent(path).ToString();
                i++;
            }
            return "";
        }

        private ACreature[,] CreateMap(string txtMap)
        {
            var rows = File.ReadAllLines(txtMap);

            //TODO:переписать эту дичь
            var rowCount = rows.Length;
            var colCount = rows.Select(x => x.Length).Max();

            var withWalls = new ACreature[colCount + 2, rowCount + 2];

            for (int i = 0; i < colCount + 2; i++)
            {
                for (int j = 0; j < rowCount + 2; j++)
                {
                    if (i == 0 || i == colCount + 1 || j == 0 || j == rowCount + 1)
                    {
                        withWalls[i, j] = CreateCreatureBySymbol('w', i, j);
                    }
                    else
                    {
                        if (j - 1 < 0 || j - 1 > rows.Length - 1 || i - 1 < 0 || i - 1 > rows[j - 1].Length - 1)
                            withWalls[i, j] = CreateCreatureBySymbol('w', i, j);
                        else
                            withWalls[i, j] = CreateCreatureBySymbol(rows[j - 1][i - 1], i, j);
                    }
                }
            }

            return withWalls;
        }

        private ACreature CreateCreatureBySymbol(char c, int i, int j)
        {

            switch (c)
            {
                case '#':
                    return new Box(new Location(i, j), textures[nameof(Box)]);

                case 'P':
                    return new Player(new Location(i, j), 
                        new Dictionary<string, Texture2D>
                        {
                            { "Left", textures["Left"] },
                            { "Right", textures["Right"] },
                            { "Up", textures["Up"] },
                            { "Down", textures["Down"] }
                        });

                case '-':
                    return null;
                    

                case 't':
                    return new Target(new Location(i, j), textures[nameof(Target)]);

                default:
                    return new Wall(new Location(i, j), textures[nameof(Wall)]);
            }
        }
    }
}
