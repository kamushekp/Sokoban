using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    public interface ICreature
    {
        Location Located { get; }
        bool IsActive { get; }
        UserComand CurrentComand {get; set;}

        ICreatureHandler creatureHandler { get; }
        List<UserComand> GetWhatReactingOn();

        string GetImageFileName();
        int GetDrawingPriority();


    }
}
