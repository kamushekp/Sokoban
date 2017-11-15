using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Logic;

namespace Game.creature_box
{
    public class Box : ICreature
    {
        private UserComand currentComand;
        private BoxHandler handler;

        public Box(int x, int y)
        {
            handler = new BoxHandler();
            Located = new Location(x, y);
            IsActive = true;
        }

        public Location Located
        {
            get; set;
        }

        public bool IsActive
        {
            get; set; 
        }

        public ICreatureHandler creatureHandler
        {
            get => handler;
        }

        public UserComand CurrentComand
        {
            get => currentComand;
            set => currentComand = value;
        }

        public List<UserComand> GetWhatReactingOn()
        {
            return new List<UserComand>();
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public string GetImageFileName()
        {
            return "Box0.png";
        }
    }
}
