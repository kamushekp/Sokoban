using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Game.Logic;

namespace Game.NMapCreator
{
    public class MapCreator
    {
        public List<ICreature[,]> Maps { get; }
 
        public MapCreator()
        {
            Maps = new List<ICreature[,]>();
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


        private ICreature[,] CreateMap(string txtMap)
        {
            var rows = File.ReadAllLines(txtMap);
            var mapWidth = rows[0].Length;
            var mapLength = rows.Length;

            var result = new ICreature[mapWidth, mapLength];

            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapLength; j++)
                {
                    result[i, j] = CreateCreatureBySymbol(rows[i][j], i, j);
                }

            return result;
        }

        private ICreature CreateCreatureBySymbol(char c, int x, int y)
        {
            switch (c)
            {
                case '#':
                    return new creature_box.Box(x, y);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
