using System;
using System.Collections.Generic;

namespace Model.Pieces
{
    public class Frog : BasePiece
    {
        private Vector[] directions = { new Vector(1, 1),
            new Vector(-1, 1)
        };

        protected override char Char => '☊';

        public override BasePiece[][,] GetMoves(BasePiece[,] board)
        {
            int jumpCount = 0;
            var[] jumpLoc = new var[4];
            var boards = new List<BasePiece[,]>();
            jumpLoc[jumpCount] = Location;

            Action<Vector> baseDirections = d =>
            {
                jumpCount++;
                var looking = jumpLoc[jumpCount] + d;
                bool captured = board[looking.X, looking.Y] != null;
                if (IsOnBoard(looking) && board[looking.X, looking.Y]?.Color != Color)
                {
                    // Moving without jumping
                    if (!captured)
                    {
                        boards.Add(CloneBoardAndMove<Frog>(board, looking));
                    }

                    // Recursion for jumping multiple times
                    else if (captured && IsOnBoard(jumpLoc[jumpCount] + d) && board[looking.X, looking.Y]?.Color != Color)
                    {
                        looking += d;
                        boards.Add(CloneBoardAndMove<Frog>(board, looking));
                        foreach (Vector d in directions)
                        {
                            jumpLoc[jumpCount] = Looking;
                            baseDirections(d);
                        }
                        jumpCount--;
                    }
                }
            };

            foreach(Vector d in directions)
            {
                baseDirections(jumpLoc[jumpCount]);
            }

			return boards.ToArray(); 
        }
    }
}