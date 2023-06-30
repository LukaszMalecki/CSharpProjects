using MyReversiVS.ReversiClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS
{
    internal class StatisticsController
    {
        //AB - alpha beta
        public void ToFile(string fileName, int numberOfTests, int blackDepth, int whiteDepth, IStrategyReversi blackStrat1, 
            IStrategyReversi blackStratEnd, IStrategyReversi whiteStrat1, IStrategyReversi whiteStratEnd, bool blackAB, bool whiteAB, 
            bool blackRandom, bool whiteRandom)
        {
            Exception implementationProblem = new Exception();

            Result endResult = new Result();

            Player playerB = Player.Black;
            Player playerW = Player.White;


            for (int testNum = 0; testNum < numberOfTests; testNum++)
            {
                GameState myGame = new GameState();

                List<double> timesB = new List<double>();
                List<double> timesW = new List<double>();



                ISearchAlgorithm intelligenceB;
                ISearchAlgorithm intelligenceW;

                if (blackAB)
                {
                    intelligenceB = new AlphaBeta(playerB, myGame, blackStrat1, blackStratEnd, blackDepth, 44);
                }
                else
                {
                    intelligenceB = new MiniMax(playerB, myGame, blackStrat1, blackStratEnd, blackDepth, 44);
                }

                if (whiteAB)
                {
                    intelligenceW = new AlphaBeta(playerW, myGame, whiteStrat1, whiteStratEnd, whiteDepth, 44);
                }
                else
                {
                    intelligenceW = new MiniMax(playerW, myGame, whiteStrat1, whiteStratEnd, whiteDepth, 44);
                }

                int round = 0;

                DateTime present = DateTime.Now;

                while (!myGame.GameOver)
                {
                    //Console.WriteLine(round);

                    if (myGame.CurrentPlayer == playerB)
                    {
                        Position myMove = intelligenceB.FindMove(playerB, myGame, blackRandom);
                        if (myMove != null)
                        {
                            myGame.MakeMove(myMove);
                            //Console.WriteLine(string.Format("Processing time in seconds: {0}", intelligenceB.LastMoveTime));

                            timesB.Add(intelligenceB.LastMoveTime);
                        }
                        else
                        {
                            throw implementationProblem;
                        }
                    }
                    else if (myGame.CurrentPlayer == playerW)
                    {
                        Position myMove = intelligenceW.FindMove(playerW, myGame, whiteRandom);
                        if (myMove != null)
                        {
                            myGame.MakeMove(myMove);
                            //Console.WriteLine(string.Format("Processing time in seconds: {0}", intelligenceW.LastMoveTime));

                            timesW.Add(intelligenceW.LastMoveTime);
                        }
                        else
                        {
                            throw implementationProblem;
                        }
                    }
                    round = myGame.DiscNumber() - 4;
                }
                var processingTime = (DateTime.Now - present).TotalSeconds;

                endResult.Winners.Add(myGame.Winner);
                endResult.FullTimeSearchedB.Add(intelligenceB.ProcessingTime);
                endResult.FullTimeSearchedW.Add(intelligenceW.ProcessingTime);
                endResult.AverageTimeSearchedB.Add(Result.Average(timesB));
                endResult.AverageTimeSearchedW.Add(Result.Average(timesW));
                endResult.DeviationTimeSearchedB.Add(Result.StandardDeviation(timesB));
                endResult.DeviationTimeSearchedW.Add(Result.StandardDeviation(timesW));
                endResult.NodesSearchedB.Add(intelligenceB.NodesSearchedCount);
                endResult.NodesSearchedW.Add(intelligenceW.NodesSearchedCount);

                /*myGame.PrintBoard();
                Console.WriteLine("Winner: " + myGame.Winner.ToString());
                Console.WriteLine(string.Format("Score B-W: {0}:{1}", myGame.DiscCount[Player.Black], myGame.DiscCount[Player.White]));

                Console.WriteLine(string.Format("Processing time in seconds: {0}", processingTime));
                Console.WriteLine(string.Format("Total nodes searched by {0}: {1}", intelligenceB.MyPlayer, intelligenceB.NodesSearchedCount));
                Console.WriteLine(string.Format("Total nodes searched by {0}: {1}", intelligenceW.MyPlayer, intelligenceW.NodesSearchedCount));
                Console.WriteLine(string.Format("Total time spent searching by {0}: {1}", intelligenceB.MyPlayer, intelligenceB.ProcessingTime));
                Console.WriteLine(string.Format("Total time spent searching by {0}: {1}", intelligenceW.MyPlayer, intelligenceW.ProcessingTime));*/
            }

            //string path = Path.Combine(Environment.CurrentDirectory, @"TestResults\", fileName + ".txt");

            using (StreamWriter writetext = new StreamWriter(fileName + ".txt"))
            {
                writetext.WriteLine("PlayerColor;Strategy1;StrategyEnd;TestNum;WinRate;Depth;AvgFullTimeSearch;DeviationFullTimeSearch"
                    + ";AvgMoveTime;AvgDeviationMoveTime;AvgFullNodesSearched;DeviationFullNodesSearched");

                writetext.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                    playerB, blackStrat1, blackStratEnd, numberOfTests, endResult.WinRatio(playerB), blackDepth,
                    Result.Average(endResult.FullTimeSearchedB), Result.StandardDeviation(endResult.FullTimeSearchedB),
                    Result.Average(endResult.AverageTimeSearchedB), Result.Average(endResult.DeviationTimeSearchedB),
                    Result.AverageInt(endResult.NodesSearchedB), Result.StandardDeviationInt(endResult.NodesSearchedB)
                    ));

                writetext.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                    playerW, whiteStrat1, whiteStratEnd, numberOfTests, endResult.WinRatio(playerW), whiteDepth,
                    Result.Average(endResult.FullTimeSearchedW), Result.StandardDeviation(endResult.FullTimeSearchedW),
                    Result.Average(endResult.AverageTimeSearchedW), Result.Average(endResult.DeviationTimeSearchedW),
                    Result.AverageInt(endResult.NodesSearchedW), Result.StandardDeviationInt(endResult.NodesSearchedW)
                    ));
            }
        }
    }
}
