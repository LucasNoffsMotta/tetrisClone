using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.ExternalAPI.Models
{
    public class Player
    {
        public string? Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public DateTime DateTime { get; set; }
    }
}
