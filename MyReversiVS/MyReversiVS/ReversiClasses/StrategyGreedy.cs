using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal class StrategyGreedy : IStrategyReversi
    {
        public StrategyGreedy() { }

        override public string ToString()
        {
            return "StrategyGreedy";
        }
        public double EvaluationFunction(Player player, GameState gameState)
        {
            if (gameState.GameOver)
            {
                return gameState.ResultScore(player);
            }
            double retValue = 0;

            double tempValue = gameState.DiscCount[player] - gameState.DiscCount[player.Opponent()];

            retValue += tempValue;

            return retValue;
        }
    }
}
