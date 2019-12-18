using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Game
	{
		public Stack<BasePiece[,]> History = new Stack<BasePiece[,]>();
		public Queue<BasePlayer> Players = new Queue<BasePlayer>();
		public BasePiece[,] CurrentBoard => History.Peek();
		public BasePiece[,] PreviousBoard()
		{
			if (History.Count < 2) return null;
			var current = History.Pop();
			var previous = History.Peek();
			History.Push(current);
			return previous;
		}

		public double? TakeATurn()
		{
			var moves = GetMoves();
			var player = GetNextPlayer();
			if (moves.Length == 0)
			{
				return FinalBoardCheck();
			}
			int moveIndex = player.ChooseMove(CurrentBoard, moves);
			History.Push(moves[moveIndex]);
			return null;
		}

		public Color Turn => Players.Peek().Color;
		private BasePlayer GetNextPlayer()
		{
			var nextPlayer = Players.Dequeue();
			Players.Enqueue(nextPlayer);
			return nextPlayer;
		}

		private BasePiece[][,] GetMoves()
		{
			List<BasePiece[,]> options = new List<BasePiece[,]>();
			var currentBoard = History.Peek();

			foreach (var piece in currentBoard)
			{
				if (piece?.Color == Turn)
				{
					options.AddRange(piece.GetMoves(currentBoard));
				}
			}

			return options.ToArray();
		}

		// null means game isn't over.
		// 0 is black win, 1 is white win, .5 is draw
		public double? ChechWinner()
		{
			if (History.Count > 2000) return .5;

			foreach (BasePiece p in CurrentBoard)
			{
				if (p?.Color == Turn) return null;
			}

			return (int)Turn;
		}

		public double FinalBoardCheck()
		{
			int blackCount = 0;
			int whiteCount = 0;
			foreach (BasePiece p in CurrentBoard)
			{
				if (p != null)
				{
					if (p.Color == Color.White)
					{
						whiteCount += 1;
					} else
					{
						blackCount += 1;
					}
				}
			}

			if (blackCount > whiteCount)
			{
				return 0;
			}
			else if (whiteCount > blackCount)
			{
				return 1;
			}

			return .5;
		}
	}
}
