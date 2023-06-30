using System.Collections.Generic;

public class MoveInfo
{
    public Player Player { get; set; }
    public Position Position { get; set; }
    public List<Position> Outflanked { get; set; } //list of positions that were/would be unflanked because of this move
}
