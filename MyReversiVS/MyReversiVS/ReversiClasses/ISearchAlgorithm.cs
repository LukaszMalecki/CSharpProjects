using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal interface ISearchAlgorithm
    {
        Position FindMove(Player player, GameState gameState, bool IsRandom=false);

        public Player MyPlayer { get; set; }
        public GameState Game { get; set; }

        public IStrategyReversi Strategy { get; set; }
        public IStrategyReversi EndgameStrategy { get; set; }

        public int EndgameStart { get; set; }
        public int Depth { get; set; }

        public double ProcessingTime { get; set; }
        public double LastMoveTime { get; set; }

        public int NodesSearchedCount { get; set; }
    }
}
