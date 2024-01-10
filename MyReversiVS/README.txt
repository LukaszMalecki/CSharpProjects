Version 1.0 build date: May 2023

The implementation of logic of reversi game credited to OttoBotCode:
https://www.youtube.com/watch?v=SgkjZQ5vhIg

My focus was on testing AI algorithm MiniMax and different strategies to play Reversi.

The program doesn't have any interface, so you have to change values directly in the Program.cs file.

The purpose of this program is to test how different strategies for MiniMax in Reversi fare against each other.

Each player has to specify early/midgame strategy and a separate endgame strategy.

Available strategies are: StrategyOne, StrategyEndgame, StrategyGreedy, StrategyStaticWeights.

You can specify depth of search in game tree, whether moves should be made at random, whether to use Alpha-Beta Pruning and how many tests you want to make.

The result of test is formated in such a way that it's easy to use in MS Excel.

We can see results of test from perspective of both Black and White player.

We can observe how much time did algorithm spend searching the game tree throughout the whole match on average (AvgFullTimeSearch) and how it deviates throughout the group of tests (DeviationFullTimeSearch).

We can observe how much time did algorithm spend on one move on average (AvgMoveTime) and average of how it deviated throughout the match (AvgDeviationMoveTime).

We can also see how many nodes were searched on average (AvgFullNodesSearched) and how it deviated throughtout the group of tests (DeviationFullNodesSearched).

We can see the Win ratio ranging from 0 to 1 (WinRate).


Example of the result file is:

test_StrategyOneEndVsItself.txt

Example of analysis on tests in excel file:

Test_Depths.xlsx

