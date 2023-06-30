using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal class MiniMax : ISearchAlgorithm
    {
        public Player MyPlayer { get; set; }
        public GameState Game { get; set; }

        public IStrategyReversi Strategy { get; set; }
        public IStrategyReversi EndgameStrategy { get; set; }

        public int EndgameStart { get; set; }
        public int Depth { get; set; }

        public double ProcessingTime { get; set; }
        public double LastMoveTime { get; set; }

        public int NodesSearchedCount { get; set; }

        private IStrategyReversi currentStrategy;

        private Random random;
        public MiniMax(Player player, GameState game, IStrategyReversi strategy, IStrategyReversi endgameStrategy, int depth, int endgameStart=44)
        {
            MyPlayer = player;
            Game = game;
            Strategy = strategy;
            EndgameStrategy = endgameStrategy;
            Depth = depth;
            EndgameStart = endgameStart;

            currentStrategy = Strategy;

            random = new Random();

            NodesSearchedCount = 0;
            ProcessingTime = 0;
            LastMoveTime = 0;


        }

        public Position FindMove(Player player, GameState gameState, bool IsRandom=false)
        {
            MyPlayer = player;
            Game = gameState;
            SelectStrategy();

            if( gameState.CurrentPlayer != player ) 
            {
                return null;
            }

            if(gameState.LegalMoves.Count == 0) 
            {
                return null;
            }
            DateTime present = DateTime.Now;
            Dictionary<Position, double> legalValues = new Dictionary<Position, double>();

            foreach(var move in gameState.LegalMoves.Keys)
            {
                GameState afterMove = new GameState(Game);
                afterMove.MakeMove(move);
                NodesSearchedCount++;

                if( IsRandom ) 
                {
                    legalValues[move] = 1;
                }
                else
                    legalValues[move] = FindMoveRec(player.Opponent(), afterMove, Depth-1);
            }

            double maxVal = double.MinValue;

            List<Position> maxPositions = new List<Position>();

            foreach(var position in legalValues.Keys)
            {
                if (legalValues[position] > maxVal)
                {
                    maxPositions.Clear();
                    maxPositions.Add(position);
                    maxVal = legalValues[position];
                }
                else if (legalValues[position] == maxVal)
                {
                    maxPositions.Add(position);
                }
            }
            LastMoveTime = (DateTime.Now - present).TotalSeconds;
            ProcessingTime += LastMoveTime;

            if ( maxPositions.Count == 1) 
            {
                return maxPositions[0];
            }
            

            return maxPositions[random.Next(0, maxPositions.Count)];
        }

        //player - kogo tura
        public double FindMoveRec(Player player, GameState gameState, int curDepth) 
        {
            NodesSearchedCount++;
            if ( curDepth <= 0 || gameState.GameOver)
            {
                //return currentStrategy.EvaluationFunction(player, gameState);
                return currentStrategy.EvaluationFunction(MyPlayer, gameState);
            }
            //Jezeli jest skip turn
            if( player != gameState.CurrentPlayer ) 
            {
                return FindMoveRec(player.Opponent(), gameState, curDepth - 1);
            }

            if( player == MyPlayer ) 
            {
                double value = double.MinValue;

                foreach (var move in gameState.LegalMoves.Keys)
                {
                    GameState afterMove = new GameState(gameState);
                    afterMove.MakeMove(move);
                    value = Math.Max( value, FindMoveRec(player.Opponent(), afterMove, curDepth-1));
                }
                return value;

            }
            else
            {
                double value = double.MaxValue;

                foreach (var move in gameState.LegalMoves.Keys)
                {
                    GameState afterMove = new GameState(gameState);
                    afterMove.MakeMove(move);
                    value = Math.Min(value, FindMoveRec(player.Opponent(), afterMove, curDepth - 1));
                }
                return value;

            }
        }

        private void SelectStrategy()
        {
            if (Game.DiscNumber() >= EndgameStart)
                currentStrategy = EndgameStrategy;
            else
                currentStrategy = Strategy;
        }


    }
}
