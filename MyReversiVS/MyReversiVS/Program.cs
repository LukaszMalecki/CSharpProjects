using System;
using System.Threading;
using System.Linq;
using MyReversiVS.ReversiClasses;

namespace MyReversiVS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test2();

            StatisticsController statisticsController = new StatisticsController();

            /*statisticsController.ToFile("speedTest2", 50, 4, 4, new StrategyOne(), new StrategyEndgame(), new StrategyGreedy(), new StrategyGreedy(),
                true, true, false, true);*/

            IStrategyReversi stratA = new StrategyOne();
            IStrategyReversi stratB = new StrategyEndgame();

            statisticsController.ToFile("test_StrategyOneEndVsItself", 25, 3, 3, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test");

            /*
            statisticsController.ToFile("test_depthMiniMax_7", 1, 7, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test6");
            */

            /*
            statisticsController.ToFile("test_depthAlphaBeta_1", 100, 1, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test1_AB");

            statisticsController.ToFile("test_depthAlphaBeta_2", 50, 2, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test2_AB");

            statisticsController.ToFile("test_depthAlphaBeta_3", 20, 3, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test3_AB");
            statisticsController.ToFile("test_depthAlphaBeta_4", 5, 4, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test4_AB");
            statisticsController.ToFile("test_depthAlphaBeta_5", 1, 5, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test5_AB");
            statisticsController.ToFile("test_depthAlphaBeta_6", 1, 6, 1, stratA, stratB, stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test6_AB");


            statisticsController.ToFile("test_depthMiniMax_1", 100, 1, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test1");

            statisticsController.ToFile("test_depthMiniMax_2", 50, 2, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test2");

            statisticsController.ToFile("test_depthMiniMax_3", 20, 3, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test3");
            statisticsController.ToFile("test_depthMiniMax_4", 5, 4, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test4");
            statisticsController.ToFile("test_depthMiniMax_5", 1, 5, 1, stratA, stratB, stratA, stratB,
                false, true, false, false);
            Console.WriteLine("End of Test5");
            */


            /*
            statisticsController.ToFile("test_StratOneOne1_rev", 50, 4, 4, stratA, stratB, new StrategyOne(), new StrategyEndgame(),
                true, true, false, false);
            Console.WriteLine("End of Test1_rev");

            statisticsController.ToFile("test_StratOneOne2", 50, 4, 4, new StrategyGreedy(), new StrategyGreedy(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test2");

            statisticsController.ToFile("test_StratOneOne2_rev", 50, 4, 4, stratA, stratB, new StrategyGreedy(), new StrategyGreedy(),
                true, true, false, false);
            Console.WriteLine("End of Test2_rev");

            statisticsController.ToFile("test_StratOneOne3", 50, 4, 4, new StrategyStaticWeights(), new StrategyStaticWeights(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test3");

            statisticsController.ToFile("test_StratOneOne3_rev", 50, 4, 4, stratA, stratB, new StrategyStaticWeights(), new StrategyStaticWeights(),
                true, true, false, false);
            Console.WriteLine("End of Test3_rev");

            statisticsController.ToFile("test_StratOneOne4", 50, 4, 4, new StrategyOne(), new StrategyOne(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test4");

            statisticsController.ToFile("test_StratOneOne4_rev", 50, 4, 4, stratA, stratB, new StrategyOne(), new StrategyOne(),
                true, true, false, false);
            Console.WriteLine("End of Test4_rev");
            */

            /*
            statisticsController.ToFile("test_StratOneOne1", 50, 4, 4, new StrategyOne(), new StrategyEndgame(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test1");

            statisticsController.ToFile("test_StratOneOne1_rev", 50, 4, 4, stratA, stratB, new StrategyOne(), new StrategyEndgame(),
                true, true, false, false);
            Console.WriteLine("End of Test1_rev");

            statisticsController.ToFile("test_StratOneOne2", 50, 4, 4, new StrategyGreedy(), new StrategyGreedy(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test2");

            statisticsController.ToFile("test_StratOneOne2_rev", 50, 4, 4, stratA, stratB, new StrategyGreedy(), new StrategyGreedy(),
                true, true, false, false);
            Console.WriteLine("End of Test2_rev");

            statisticsController.ToFile("test_StratOneOne3", 50, 4, 4, new StrategyStaticWeights(), new StrategyStaticWeights(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test3");

            statisticsController.ToFile("test_StratOneOne3_rev", 50, 4, 4, stratA, stratB, new StrategyStaticWeights(), new StrategyStaticWeights(),
                true, true, false, false);
            Console.WriteLine("End of Test3_rev");

            statisticsController.ToFile("test_StratOneOne4", 50, 4, 4, new StrategyOne(), new StrategyOne(), stratA, stratB,
                true, true, false, false);
            Console.WriteLine("End of Test4");

            statisticsController.ToFile("test_StratOneOne4_rev", 50, 4, 4, stratA, stratB, new StrategyOne(), new StrategyOne(),
                true, true, false, false);
            Console.WriteLine("End of Test4_rev");
            */
        }

        static void Test1()
        {
            Console.WriteLine("Hello World!");
            GameState myGame = new GameState();
            GameState gameCopy = new GameState(myGame);

            foreach (var x in gameCopy.LegalMoves.Values)
            {
                foreach (var y in x)
                {
                    Console.Write(y.ToString() + " ");
                }
                Console.WriteLine("");
            }
            gameCopy.PrintBoard();

            //Console.WriteLine("Hello World! 2");
            for (int i = 0; i < 10; i++)
            {
                //MoveInfo moveInfo;
                myGame.MakeMove(myGame.LegalMoves.First().Key); //, out moveInfo);
            }
            /*while (!myGame.GameOver)
            {
                MoveInfo moveInfo;
                //myGame.PrintCurrentPlayer();
                //myGame.PrintBoard();
                //Console.WriteLine();
                myGame.MakeMove(myGame.LegalMoves.First().Key, out moveInfo);
                //Thread.Sleep(500);
            }*/
            foreach (var x in gameCopy.LegalMoves.Values)
            {
                foreach (var y in x)
                {
                    Console.Write(y.ToString() + " ");
                }
                Console.WriteLine("");
            }
            gameCopy.PrintBoard();
            myGame.PrintBoard();
            Console.WriteLine("Winner: " + myGame.Winner.ToString());
        }

        static void Test2()
        {
            Exception implementationProblem = new Exception();

            GameState myGame = new GameState();

            Player playerA = Player.Black;
            Player playerB = Player.White;

            //MiniMax intelligenceA = new MiniMax(playerA, myGame, new StrategyOne(), new StrategyEndgame(), 4, 44);
            AlphaBeta intelligenceA = new AlphaBeta(playerA, myGame, new StrategyOne(), new StrategyEndgame(), 4, 44);
            //MiniMax intelligenceA = new MiniMax(playerA, myGame, new StrategyGreedy(), new StrategyGreedy(), 4, 44);
            //MiniMax intelligenceB = new MiniMax(playerB, myGame, new StrategyStaticWeights(), new StrategyStaticWeights(), 5, 44);
            AlphaBeta intelligenceB = new AlphaBeta(playerB, myGame, new StrategyStaticWeights(), new StrategyStaticWeights(), 4, 44);
            int round = 0;

            DateTime present = DateTime.Now;

            while ( !myGame.GameOver)
            {
                Console.WriteLine(round);

                if(myGame.CurrentPlayer == playerA )
                {
                    Position myMove = intelligenceA.FindMove(playerA, myGame);
                    if (myMove != null) 
                    {
                        myGame.MakeMove(myMove);
                        Console.WriteLine(string.Format("Processing time in seconds: {0}", intelligenceA.LastMoveTime));
                    }
                    else
                    {
                        throw implementationProblem;
                    }
                }
                else if (myGame.CurrentPlayer == playerB)
                {
                    Position myMove = intelligenceB.FindMove(playerB, myGame);
                    if (myMove != null)
                    {
                        myGame.MakeMove(myMove);
                        Console.WriteLine(string.Format("Processing time in seconds: {0}", intelligenceB.LastMoveTime));
                    }
                    else
                    {
                        throw implementationProblem;
                    }
                }
                round = myGame.DiscNumber() - 4;
            }
            var processingTime = (DateTime.Now - present).TotalSeconds;


            myGame.PrintBoard();
            Console.WriteLine("Winner: " + myGame.Winner.ToString());
            Console.WriteLine(string.Format("Score B-W: {0}:{1}", myGame.DiscCount[Player.Black], myGame.DiscCount[Player.White]));

            Console.WriteLine(string.Format("Processing time in seconds: {0}", processingTime));
            Console.WriteLine(string.Format("Total nodes searched by {0}: {1}", intelligenceA.MyPlayer ,intelligenceA.NodesSearchedCount));
            Console.WriteLine(string.Format("Total nodes searched by {0}: {1}", intelligenceB.MyPlayer, intelligenceB.NodesSearchedCount));
            Console.WriteLine(string.Format("Total time spent searching by {0}: {1}", intelligenceA.MyPlayer, intelligenceA.ProcessingTime));
            Console.WriteLine(string.Format("Total time spent searching by {0}: {1}", intelligenceB.MyPlayer, intelligenceB.ProcessingTime));

        }
    }
}
