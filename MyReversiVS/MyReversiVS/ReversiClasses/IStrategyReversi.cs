using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal interface IStrategyReversi
    {
        double EvaluationFunction(Player player, GameState gameState);

        string ToString();
    }
}
