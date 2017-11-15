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

        public Location Located
        {
            get => throw new NotImplementedException();
            set => Located = value;
        }

        public bool IsActive
        {
            get => false;
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
            throw new NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new NotImplementedException();
        }

        public string GetImageFileName()
        {
            throw new NotImplementedException();
        }

    }
}
