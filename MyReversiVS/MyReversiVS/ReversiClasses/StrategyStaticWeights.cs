using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal class StrategyStaticWeights : IStrategyReversi
    {
        public StrategyStaticWeights() 
        {
            if( !isFilled ) 
            {
                Fill();
            }
        }

        override public string ToString()
        {
            return "StrategyStaticWeights";
        }

        private static bool isFilled = false;
        public static double[,] WeightsTable = new double[8, 8];

        private static void Fill()
        {
            List<int> rows = new List<int>() { 0, 7};
            foreach( int row in rows ) 
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            WeightsTable[row, j] = 4;
                            WeightsTable[row, 7-j] = WeightsTable[row, j];
                            break;
                        case 1:
                            WeightsTable[row, j] = -3;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        default:
                            WeightsTable[row, j] = 2;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                    }    
                }
            }
            rows = new List<int>() { 1, 6 };
            foreach (int row in rows)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            WeightsTable[row, j] = -3;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        case 1:
                            WeightsTable[row, j] = -4;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        default:
                            WeightsTable[row, j] = -1;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                    }
                }
            }

            rows = new List<int>() { 2, 5 };
            foreach (int row in rows)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            WeightsTable[row, j] = 2;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        case 1:
                            WeightsTable[row, j] = -1;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        case 2:
                            WeightsTable[row, j] = 1;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        default:
                            WeightsTable[row, j] = 0;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                    }
                }
            }

            rows = new List<int>() { 3, 4 };
            foreach (int row in rows)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            WeightsTable[row, j] = 2;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        case 1:
                            WeightsTable[row, j] = -1;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        case 2:
                            WeightsTable[row, j] = 0;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                        default:
                            WeightsTable[row, j] = 1;
                            WeightsTable[row, 7 - j] = WeightsTable[row, j];
                            break;
                    }
                }
            }

        }

        public double EvaluationFunction(Player player, GameState gameState)
        {
            if (gameState.GameOver)
            {
                return gameState.ResultScore(player);
            }
            double retValue = 0;

            for( int i = 0; i < 8; i++)
            {
                for( int j  = 0; j < 8; j++)
                {
                    if(gameState.Board[i,j] == player)
                    {
                        retValue += WeightsTable[i, j];
                    }
                }
            }

            return retValue;
        }
    }
}
