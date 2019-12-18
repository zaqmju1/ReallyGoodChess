/* using System;
using System.Collections.Generic;

namespace Model.Pieces
{
    public class Frog : BasePiece
    {
        private Vector[] directions = { new Vector(1, 1),
            new Vector(-1, 1)
        };

        //protected override char Char => 'F';
        protected override char Char => '☊';

        public int jumpCount = 0;
        public Vector[] jumpLoc = null;
        public List<BasePiece[,]> boards = null;
        public BasePiece[,] curBoard = null;

        public override BasePiece[][,] GetMoves(BasePiece[,] board)
        {
            jumpCount = 0;
            jumpLoc = new Vector[4];
            boards = new List<BasePiece[,]>();
            jumpLoc[jumpCount] = Location;
            curBoard = board;

            foreach(Vector d in directions)
            {
                baseDirections(jumpLoc[jumpCount]);
            }

			return boards.ToArray(); 
        }

        public void baseDirections (Vector d)
        {
            var looking = jumpLoc[jumpCount] + d;
            if (IsOnBoard(looking) && curBoard[looking.X, looking.Y]?.Color != Color)
            {
                bool captured = curBoard[looking.X, looking.Y] != null;
                // Moving without jumping
                if (!captured)
                {
                    boards.Add(CloneBoardAndMove<Frog>(curBoard, looking));
                }

                // Recursion for jumping multiple times
                else if (captured && IsOnBoard(jumpLoc[jumpCount] + d) && curBoard[looking.X, looking.Y]?.Color != Color)
                {
                    looking += d;
                    boards.Add(CloneBoardAndMove<Frog>(curBoard, looking));
                    foreach (Vector dir in directions)
                    {
                        jumpCount++;
                        jumpLoc[jumpCount] = looking;
                        baseDirections(dir);
                    }
                    jumpCount--;
                }
            }
        }
    }
} */


using System;
using System.Collections.Generic;

namespace Model.Pieces
{
    public class Frog : BasePiece
    {
        protected override char Char => '☊';

        private Vector FrontRight => new Vector(Color == Color.White ? 1 : -1, Color == Color.White ? 1 : -1);
        private Vector FrontLeft => new Vector(Color == Color.White ? 1 : -1, Color == Color.White ? -1 : 1);

        public override BasePiece[][,] GetMoves(BasePiece[,] board)
        {
            var boards = new List<BasePiece[,]>();
            // Move Diagonally
            var forwardRight = Location + FrontRight;
            var forwardRightTwo = forwardRight + FrontRight;
            // Move diagonally twice when jumping
            var forwardLeft = Location + FrontLeft;
            var forwardLeftTwo = forwardRight + FrontLeft;

            // Can move one diagonally (but not capture)
            if (IsOnBoard(forwardRight) && board[forwardRight.X, forwardRight.Y] == null)
            {
                if (!IsOnBoard(forwardRightTwo))// if at the end (switch directions)
                {
                    FrontRight.X *= -1;
                    FrontLeft.X *= -1;
                }
                else
                {
                    boards.Add(CloneBoardAndMove<Frog>(board, forwardRight));
                }
            }
            if (IsOnBoard(forwardLeft) && board[forwardLeft.X, forwardLeft.Y] == null)
            {
                if (!IsOnBoard(forwardLeftTwo))// if at the end (switch directions)
                {
                    FrontRight.X *= -1;
                    FrontLeft.X *= -1;
                }
                else
                {
                    boards.Add(CloneBoardAndMove<Frog>(board, forwardLeft));
                }
            }

            // Capture diagonally one space left or right
            if (IsOnBoard(forwardLeftTwo)
                && board[forwardLeft.X, forwardLeft.Y] != null
                && board[forwardLeft.X, forwardLeft.Y].Color != Color
                && board[forwardLeftTwo.X, forwardLeftTwo.Y] == null)
            {
                boards.Add(CloneBoardAndJump<Frog>(board, forwardLeftTwo, forwardLeftTwo));
            }
            if (IsOnBoard(forwardRightTwo)
                && board[forwardRight.X, forwardRight.Y] != null
                && board[forwardRight.X, forwardRight.Y].Color != Color
                && board[forwardRightTwo.X, forwardRightTwo.Y] == null)
            {
                boards.Add(CloneBoardAndJump<Frog>(board, forwardRightTwo, forwardRightTwo));
            }

            return boards.ToArray();
        }
    }
}