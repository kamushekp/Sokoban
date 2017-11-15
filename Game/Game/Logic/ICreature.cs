using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    interface ICreature
    {
        Location Located { get; }
        bool IsActive { get; }

        string GetImageFileName();
        int GetDrawingPriority();
        List<IInnerComand> GetWhatCanDo();
        List<UserComand> GetWhatReactingOn();

    }
}
