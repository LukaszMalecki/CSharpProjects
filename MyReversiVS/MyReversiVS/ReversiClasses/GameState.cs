using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class GameState
{
    public const int Rows = 8;
    public const int Columns = 8;

    public Player[,] Board { get; }
    public Dictionary<Player, int> DiscCount { get; }
    public Player CurrentPlayer { get; private set; }
    public bool GameOver { get; private set; }
    public Player Winner { get; private set; }
    public Dictionary<Position, List<Position>> LegalMoves { get; private set; }

    public static List<Position> Corners { get; private set; } = new List<Position>() 
    { new Position(0, 0), new Position(0, 7), new Position(7, 0), new Position(7, 7)};

    public GameState()
    {

        Board = new Player[Rows, Columns];
        Board[3, 3] = Player.White;
        Board[3, 4] = Player.Black;
        Board[4, 3] = Player.Black;
        Board[4, 4] = Player.White;

        DiscCount = new Dictionary<Player, int>()
        {
            { Player.Black, 2 },
            { Player.White, 2 }
        };
        CurrentPlayer = Player.Black;

        LegalMoves = FindLegalMoves(CurrentPlayer);
    }

    public GameState( GameState other)
    {
        Board = other.Board.Clone() as Player[,];
        DiscCount = new Dictionary<Player, int>()
        {
            { Player.Black, other.DiscCount[Player.Black] },
            { Player.White, other.DiscCount[Player.White] }
        };
        CurrentPlayer = other.CurrentPlayer;
        GameOver = other.GameOver;
        Winner = other.Winner;

        //this list is not changing throughout the game, it's overwritten
        LegalMoves = other.LegalMoves;
        /*LegalMoves = new Dictionary<Position, List<Position>>();

        foreach(Position position in other.LegalMoves.Keys)
        {
            LegalMoves[position] = other.LegalMoves[position];
        }*/

    }


    public bool MakeMove(Position pos) //, out MoveInfo moveInfo)
    {
        if(!LegalMoves.ContainsKey(pos)) 
        {
            //moveInfo = null;
            return false; 
        }
        Player movePlayer = CurrentPlayer;
        List<Position> outflanked = LegalMoves[pos];

        Board[pos.Row, pos.Column] = movePlayer;
        FlipDiscs(outflanked);
        UpdateDiscCounts(movePlayer, outflanked.Count);
        PassTurn();

        //moveInfo = new MoveInfo { Player = movePlayer, Position = pos, Outflanked = outflanked };
        return true;
    }

    public IEnumerable<Position> OccupiedPositions()
    {
        for( int r = 0; r < Rows; r++)
        {
            for( int c = 0; c < Columns; c++)
            {
                if(Board[r, c] != Player.None)
                {
                    yield return new Position(r, c);
                }
            }
        }
    }

    private void FlipDiscs(List<Position> positions)
    {
        foreach(Position pos in positions) 
        {
            Board[pos.Row, pos.Column] = Board[pos.Row, pos.Column].Opponent();
        }
    }

    private void UpdateDiscCounts(Player movePlayer, int outflankedCount)
    {
        DiscCount[movePlayer] += outflankedCount + 1;
        DiscCount[movePlayer.Opponent()] -= outflankedCount;
    }

    private bool IsInsideBoard(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Columns;
    }

    private void ChangePlayer()
    {
        CurrentPlayer = CurrentPlayer.Opponent();
        LegalMoves = FindLegalMoves(CurrentPlayer);
    }

    private Player FindWinner()
    {
        if(DiscCount[Player.Black] > DiscCount[Player.White])
        {
            return Player.Black;
        }
        if(DiscCount[Player.Black] < DiscCount[Player.White])
        {
            return Player.White;
        }
        
        return Player.None;
        
    }

    private void PassTurn()
    {
        ChangePlayer();

        if( LegalMoves.Count > 0 ) 
        {
            return;
        }

        ChangePlayer();

        if (LegalMoves.Count == 0)
        {
            CurrentPlayer = Player.None;
            GameOver = true;
            Winner = FindWinner();
        }

    }

    //rDelta and cDelta can have values of {-1, 0, 1} and describe direction
    //the list itself containts positions of disc that will be outflanked
    private List<Position> OutflankedInDir(Position pos, Player player, int rDelta, int cDelta)
    {
        List<Position> outflanked = new List<Position>();
        int checkedRow = pos.Row + rDelta;
        int checkedCol = pos.Column + cDelta;

        while( IsInsideBoard(checkedRow, checkedCol) && Board[checkedRow, checkedCol] != Player.None)
        {
            if (Board[checkedRow, checkedCol] == player.Opponent()) 
            {
                outflanked.Add(new Position(checkedRow, checkedCol));
                checkedRow += rDelta;
                checkedCol += cDelta;
            }
            else
            {
                return outflanked;
            }
        }
        return new List<Position>();
    }

    private List<Position> Outflanked( Position pos, Player player)
    {
        List<Position> outflanked = new List<Position>();

        for( int rDelta = -1;  rDelta <= 1; rDelta++ )
        {
            for( int  cDelta = -1; cDelta <= 1; cDelta++ )
            {
                if( rDelta == 0  && cDelta == 0)
                {
                    continue;
                }

                outflanked.AddRange(OutflankedInDir(pos, player, rDelta, cDelta));
            }
        }

        return outflanked;
    }

    private bool IsMoveLegal( Player player, Position pos, out List<Position> outflanked )
    {
        if (Board[pos.Row, pos.Column] != Player.None)
        {
            outflanked = null;
            return false;
        }

        outflanked = Outflanked(pos, player);
        return outflanked.Count > 0;
    }

    public Dictionary<Position, List<Position>> FindLegalMoves( Player player ) 
    {
        Dictionary<Position,List<Position>> legalMoves = new Dictionary<Position,List<Position>>();

        for( int r = 0; r < Rows; r++)
        {
            for( int c  = 0; c < Columns; c++ )
            {
                Position pos = new Position( r, c );
                
                if( IsMoveLegal(player, pos, out List<Position> outflanked) )
                {
                    legalMoves[pos] = outflanked;
                }
            }
        }

        return legalMoves;
    }

    public void PrintCurrentPlayer()
    {
        Console.Out.WriteLine(CurrentPlayer.ToString());
    }
    public void PrintBoard( bool showPotentialMove = true )
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (Board[r,c] == Player.White)
                {
                    Console.Out.Write("W");
                    continue;
                }
                if (Board[r, c] == Player.Black)
                {
                    Console.Out.Write("B");
                    continue;
                }


                Position pos = new Position(r, c);
                if(showPotentialMove && LegalMoves.ContainsKey(pos) )
                {
                    Console.Out.Write(".");
                }
                else
                {
                    Console.Out.Write("#");
                }
            }
            Console.Out.Write("\n");
        }
    }

    public bool IsCorner(Position pos)
    {
        return Corners.Contains(pos);
    }

    public bool IsEdge(Position pos)
    {
        if( Corners.Contains(pos) ) 
        {
            return false;
        }
        return pos.Row == 0 || pos.Row == 7 || pos.Column == 0 || pos.Column == 7;
    }

    public List<Position> AdjacentPositions(Position pos)
    {
        List<Position> Adjacent = new List<Position>();

        for (int rDelta = -1; rDelta <= 1; rDelta++)
        {
            for (int cDelta = -1; cDelta <= 1; cDelta++)
            {
                if (rDelta == 0 && cDelta == 0)
                {
                    continue;
                }

                if( IsInsideBoard(rDelta, cDelta)) 
                {
                    Adjacent.Add(pos);
                }
            }
        }
        return Adjacent;
    }

    public bool IsAdjacentToCorner(Position pos)
    {
        foreach( Position corner in Corners ) 
        {
            if (AdjacentPositions(corner).Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public (int, int) AdjacentToEmptyCornersCount(Player player)
    {
        int retValue = 0;
        int retValueEnemy = 0;
        foreach (Position corner in Corners)
        {
            if (Board[corner.Row, corner.Column] != Player.None)
                continue;
            var adjacents = AdjacentPositions(corner);
            foreach( Position adjacent in adjacents )
            {
                if (Board[adjacent.Row, adjacent.Column] == player)
                    retValue++;
                else if (Board[adjacent.Row, adjacent.Column] == player.Opponent())
                    retValueEnemy++;
            }

        }
        return (retValue, retValueEnemy);

    }

    public (int, int) CornersCount(Player player)
    {
        int retValue = 0;
        int retValueEnemy = 0;
        foreach (Position corner in Corners)
        {
            if (Board[corner.Row, corner.Column] == player)
                retValue++;
            else if (Board[corner.Row, corner.Column] == player.Opponent())
                retValueEnemy++;

        }
        return (retValue, retValueEnemy);

    }

    public bool IsEdgeAdjacent(Position pos)
    {
        int r = pos.Row;
        int c = pos.Column;

        if( r > 0 && r < 7 && (c == 1 || c == 6))
        {
            return true;
        }
        if (c > 0 && c < 7 && (r == 1 || r == 6))
        {
            return true;
        }
        return false;
    }

    public (int, int) CountCenterPieces(Player player, int centerRadius = 2)
    {
        int retCount = 0;
        int retCountEnemy = 0;


        for( int i = 4 - centerRadius; i < 4 + centerRadius; i++ ) 
        {
            for( int j = 4 - centerRadius; j < 4 + centerRadius; j++ )
            {
                if (Board[i, j] == player)
                { 
                    retCount++; 
                }
                else if (Board[i, j] == player.Opponent())
                {
                    retCountEnemy++;
                }
            }
        }
        return (retCount, retCountEnemy);
    }

    public int ResultScore( Player player) 
    {
        int retScore = 0;

        if (!GameOver)
            return retScore;
        retScore = 10000;

        if( Winner == player)
        {
            return retScore;
        }
        if( Winner == player.Opponent())
        {
            return -retScore;
        }
        return 0;
    }

    public int DiscNumber()
    {
        return DiscCount[Player.White] + DiscCount[Player.Black];
    }
}
