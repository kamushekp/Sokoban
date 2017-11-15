using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game.Logic;

namespace Game.creature_box
{
    public class BoxHandler : ICreatureHandler
    {

        public bool ChangeGameState(Logic.Game game, ICreature creature, UserComand comand)
        {
            var cmd = (KeyEventArgs)comand.Comand;

            #region moving
            if (cmd.KeyData == Keys.Down) Move(game, creature, 0, 1);
            #endregion

            return true;
        }

        private void Move(Logic.Game game, ICreature creature, int dx, int dy)
        {
            creature.Located.X += dx;
            creature.Located.Y += dy;
        }
    }
}
