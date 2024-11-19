using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class PlayerData
    {
        string level;
        string score;

        public PlayerData(string level, string score)
        {
            this.level = level;
            this.score = score;
        }
    }
}
