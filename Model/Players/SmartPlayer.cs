using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Players
{
    public class SmartPlayer : BasePlayer
    {
        private static readonly Random R = new Random();
        private BasePiece[,] currentBoard;
        private int pawnCount;
        private int kingCount;
        private int frogCount;
        private int knightCount;
        private int bishopCount;
        private int rookCount;
        private int tempPawnCount;
        private int tempKingCount;
        private int tempFrogCount;
        private int tempKnightCount;
        private int tempBishopCount;
        private int tempRookCount;

        public override int ChooseMove(BasePiece[,] cB, BasePiece[][,] options)
        {
            currentBoard = cB;
            pawnCount = 0;
            kingCount = 0;
            frogCount = 0;
            knightCount = 0;
            bishopCount = 0;
            rookCount = 0;
            pieceCount();

            int bestIndex = 0;
            double bestScore = SmartScore(options[0]);

            for (int i = 1; i < options.Length; i++)
            {
                double score = SmartScore(options[i]);
                if (score > bestScore)
                {
                    bestIndex = i;
                    bestScore = score;
                }
            }

            return bestIndex;
        }

        virtual protected double SmartScore(BasePiece[,] options)
        {
            tempPawnCount = 0;
            tempKingCount = 0;
            tempFrogCount = 0;
            tempKnightCount = 0;
            tempBishopCount = 0;
            tempRookCount = 0;

            tempPieceCount(options);

            if (tempPawnCount < pawnCount)
            {
                return 50 + R.Next(0, 9);
            }
            else if (tempKingCount < kingCount)
            {
                return 60 + R.Next(0, 9);
            }
            else if (tempFrogCount < frogCount)
            {
                return 70 + R.Next(0, 9);
            }
            else if (tempKnightCount < knightCount)
            {
                return 80 + R.Next(0, 9);
            }
            else if (tempBishopCount < bishopCount)
            {
                return 90 + R.Next(0, 9);
            }
            else if (tempRookCount < rookCount)
            {
                return 100 + R.Next(0, 9);
            }

            return R.Next(0, 49);
        }

        virtual protected void pieceCount()
        {
            foreach(BasePiece p in currentBoard)
            {
                if (p != null)
                {
                    if (p.GetType().Name == "Pawn")
                    {
                        pawnCount += 1;
                    }
                    else if (p.GetType().Name == "King")
                    {
                        kingCount += 1;
                    }
                    else if (p.GetType().Name == "Frog")
                    {
                        frogCount += 1;
                    }
                    else if (p.GetType().Name == "Knight")
                    {
                        knightCount += 1;
                    }
                    else if (p.GetType().Name == "Bishop")
                    {
                        bishopCount += 1;
                    }
                    else if (p.GetType().Name == "Rook")
                    {
                        rookCount += 1;
                    }
                }
            }

            return;
        }

        virtual protected void tempPieceCount(BasePiece[,] tempBoard)
        {
            foreach (BasePiece p in tempBoard)
            {
                if (p != null)
                {
                    if (p.GetType().Name == "Pawn")
                    {
                        tempPawnCount += 1;
                    }
                    else if (p.GetType().Name == "King")
                    {
                        tempKingCount += 1;
                    }
                    else if (p.GetType().Name == "Frog")
                    {
                        tempFrogCount += 1;
                    }
                    else if (p.GetType().Name == "Knight")
                    {
                        tempKnightCount += 1;
                    }
                    else if (p.GetType().Name == "Bishop")
                    {
                        tempBishopCount += 1;
                    }
                    else if (p.GetType().Name == "Rook")
                    {
                        tempRookCount += 1;
                    }
                }
            }

            return;
        }
    }
}
